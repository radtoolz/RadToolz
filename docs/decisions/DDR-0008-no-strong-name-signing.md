DDR-0008: Do not enable strong-name signing
Status: Accepted
Date: 2026-07-12
Task: Supply-chain hygiene review follow-up (informational, no hard trigger)

Context
  RadToolz.vbproj currently builds with SignAssembly=false. An external
  hygiene review flagged this as worth a conscious decision rather than an
  unexamined default, given the add-in is packed and Authenticode-signed at
  release. Strong-name signing's practical benefits - GAC installability,
  side-by-side versioning, and reference-time identity checks for other
  strong-named assemblies that reference this one - do not apply here:
  RadToolz is packed into an .xll and loaded by Excel-DNA's own loader, not
  installed into the GAC, and no other .NET assembly references it by
  strong name. Its remaining benefit, a tamper-evidence check at CLR load
  time, is weak in practice (the private key is typically just a repo/build
  artifact, easily used to re-sign a tampered build, and isn't tied to any
  verified publisher identity) - Microsoft's own guidance is explicit that a
  strong name is not a security or trust boundary. The add-in already
  carries a real integrity/trust control: Authenticode signing at release,
  via a CA-issued certificate tied to a verified publisher identity and
  checked by Windows/SmartScreen when a user downloads the file.

Decision
  Leave SignAssembly=false; no change to RadToolz.vbproj. Authenticode
  signing at release remains the sole, and sufficient, integrity/trust
  control for the shipped .xll.

Alternatives Considered
  1. Enable strong-name signing (SignAssembly=true plus a .snk key) -
     rejected: adds a build-time key-management requirement for benefits
     (GAC install, reference-time identity checks) that don't apply to a
     single packed add-in with no other .NET consumers, and its
     tamper-evidence is weaker than, and redundant with, the Authenticode
     signing already applied at release.
  2. Leave the setting as-is without recording a decision - rejected: an
     unexamined default is exactly what the hygiene review flagged; writing
     this down costs nothing and closes the finding.

Consequences
  Positive: closes the "worth a conscious decision" finding with no added
    build complexity or key-management overhead; Authenticode signing
    remains the clear, sole integrity control for the shipped binary.
  Negative / accepted trade-offs: none material. If a future scenario
    introduces another .NET consumer that needs to reference RadToolz's
    assembly by strong name, or a need for GAC installation, this decision
    should be revisited via a new DDR rather than assumed to still hold.
  Regression implications: none - no code or build configuration change.
  Compatibility implications (UDF surface, persisted formats, .dna): none.
