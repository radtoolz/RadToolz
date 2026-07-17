DDR-0007: Fix pStringArray base-address computation in GetTxtRecord
Status: Accepted
Date: 2026-07-12
Task: RTZUpdate crash fix (Large, hard trigger: interop boundary)

Context
  GetTxtRecord (WindowsDNSFunctions.vb) walks a DNS_TXT_DATA record's string
  array via Marshal.ReadIntPtr(txtRecord.pStringArray, i * IntPtr.Size). The
  native pStringArray field is a C flexible array member (PSTR
  pStringArray[1]) whose storage begins at the field's own offset in the
  record - so the value Marshal.PtrToStructure copies into that field is
  already pStringArray(0) itself (a pointer to the first TXT string), not
  the array's base address. Reading further pointer-sized offsets from that
  already-dereferenced value walks into the first string's own text instead
  of the array, handing PtrToStringUni a bogus address. This is an
  AccessViolationException, which the CLR treats as a corrupted-state
  exception and does not deliver to an ordinary Catch (both GetTxtRecord's
  Try/Catch and RTZUpdate's On Error GoTo are bypassed) - Excel terminates
  immediately with no error dialog. Confirmed via git history that this
  exact line predates every change in this session and in DDR-0004/DDR-0005/
  DDR-0006 - it only surfaced once radtoolz.com's "version=" TXT record
  went live and GetTxtRecord's success path actually executed the loop body
  for the first time. Reproduced live in Release x64 via RTZUpdate().

Decision
  Recompute the string array's real base address from the original native
  record pointer (currentRecord) plus the field's marshaled offset
  (Marshal.OffsetOf), and read each i-th string pointer from that address
  instead of from txtRecord.pStringArray.

Alternatives Considered
  1. Change the struct/marshaling to describe pStringArray as a proper
     array (e.g. a fixed-size MarshalAs array) - rejected: the array's
     length (stringCount) isn't known until after the struct is already
     read, so PtrToStructure cannot marshal a variable-length trailing
     array directly; the offset-based read is the standard, minimal
     pattern for a C flexible array member.
  2. Enable [HandleProcessCorruptedStateExceptions] so the existing
     Catch ex As Exception could at least degrade gracefully instead of
     crashing - rejected: that masks a real memory-safety bug instead of
     fixing it, and broadens exception handling process-wide (or
     assembly-wide) to catch any future corrupted-state error, not just
     this one - a much larger and riskier change than a localized address
     fix for a narrow, understood root cause.
  3. Do nothing / register as debt - rejected per
     section_26_technical_debt_handling's carve-out: debt with credible
     crash potential is an immediate Stop-and-Ask, not a register entry,
     and this is a live, reproduced crash in a shipped UDF.

Consequences
  Positive: RTZUpdate no longer crashes Excel once the DNS query succeeds
    and returns real TXT data; the fix is general (correct for any
    stringCount) rather than a workaround for today's single-string record.
  Negative / accepted trade-offs: one extra local variable and
    Marshal.OffsetOf call per linked-list node walked - negligible;
    GetTxtRecord runs only when RTZUpdate is explicitly invoked, not on a
    recalculation-critical path.
  Regression implications: none expected - the query-failure and
    no-matching-record paths are untouched; only the per-string pointer
    read within the success path changed, and the new address computation
    was verified by hand against the native DNS_TXT_DATA layout for every
    index including i = 0. User has since rebuilt and confirmed the crash
    no longer reproduces; recommend one more live retest against a real
    DNS response as final confirmation.
  Compatibility implications (UDF surface, persisted formats, .dna): none -
    GetTxtRecord is a private module-internal helper, not part of the UDF
    surface; no .dna change.

Related: DDR-0004 (same file and function - fixed stringCount's field width
  and the prefix-strip length on 2026-07-10 but did not catch this separate
  base-address defect).
