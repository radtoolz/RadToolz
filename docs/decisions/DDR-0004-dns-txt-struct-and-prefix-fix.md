DDR-0004: Correct DnsRecordTxt.stringCount width and GetTxtRecord prefix-strip
Status: Accepted
Date: 2026-07-10
Task: DNS interop latent-bug fixes (Large, hard-trigger: interop boundary)

Context
  DnsRecordTxt marshals a native DNS_RECORD/DNS_TXT_DATA-shaped result via
  Marshal.PtrToStructure. Its stringCount field was declared UShort, but the
  native field (dwStringCount) is a DWORD (4 bytes) per the Windows DNS API
  (windns.h). The struct's own comment claimed the managed layout "matches
  the native struct's field order and size exactly" - false for this field.
  Separately, GetTxtRecord stripped "version=".Length characters instead of
  the caller-supplied prefix.Length, correct only because today's sole
  caller always passes "version=".

Decision
  Change stringCount to UInteger to match the native DWORD, and strip
  prefix.Length instead of the hardcoded string in GetTxtRecord.

Alternatives Considered
  1. Fix only the comment, leave the field mistyped - rejected because
     leaving a known-wrong field width in a struct whose own comment says
     "PtrToStructure trusts this shape completely" is exactly the latent
     bug that comment warns against.
  2. Add explicit <MarshalAs> attributes for extra robustness - rejected as
     scope creep; plain value fields already marshal correctly for
     Sequential layout, no field here needs it.

Consequences
  Positive: struct field genuinely matches native layout; the existing
  header comment's "matches ... exactly" claim is now true instead of
  false. GetTxtRecord is now correct for any future prefix, not just
  "version=".
  Negative / accepted trade-offs: none material.
  Regression implications: none expected - verified by hand that
  pStringArray's byte offset (40, on x64 natural alignment) is unchanged by
  the stringCount width change (2-byte field padded to 40 vs. 4-byte field
  padded to 40); prefix.Length fix is behavior-identical for the only
  existing caller (RTZUpdate, RadToolzFunctions.vb:1425, always passes
  prefix:="version=").
  Compatibility implications (UDF surface, persisted formats, .dna): none -
  private module-internal structure/function, not part of the UDF surface,
  .dna unaffected.
