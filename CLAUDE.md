\# Project Context: Legacy VB.NET Excel-DNA Add-In

This is a legacy Excel-DNA add-in built using VB.NET (Visual Basic .NET) targeting the .NET Framework 4.8.1.



\## Build Commands

\- Build Solution: `msbuild MySolution.sln /t:Build /p:Configuration=Debug`

\- Clean Solution: `msbuild MySolution.sln /t:Clean`



\## Language \& Framework Rules

\- \*\*Language\*\*: Strictly use VB.NET syntax (`Dim`, `Sub`, `Function`, `Imports`, `Handles`). Never write C#.

\- \*\*Target\*\*: .NET Framework 4.8.1 (Do not use modern .NET Core / .NET 5+ features).

\- \*\*Case-Insensitivity\*\*: VB.NET is case-insensitive, but keep variable and attribute casings consistent.



\## Excel-DNA Implementation Standards

\- \*\*UDFs (User Defined Functions)\*\*: Expose Excel formulas inside a `Public Module` instead of a Class so methods are naturally static and accessible to Excel.

\- \*\*Attributes\*\*: Every Excel-exposed function must be explicitly decorated using standard VB.NET attribute syntax:

&#x20; ```vb

&#x20; <ExcelFunction(Description:="Your function description")>

&#x20; Public Function MyFunction(ByVal input As String) As String

&#x20; ```

\- \*\*COM Reference Management\*\*: Code running on the main thread behaves like VBA. \*\*Do not\*\* write unnecessary boilerplate calls to `Marshal.ReleaseComObject` or `FinalReleaseComObject` unless explicitly targeting background worker threads.

\- \*\*Thread Safety\*\*: All Excel COM object model modifications must run on the main Excel thread. Use `ExcelAsyncUtil.QueueAsMacro` to safely transition asynchronous or background-thread results back onto the main Excel thread.

\- \*\*Configuration\*\*: Changes affecting the add-in definition must be mirrored in the project's root `.dna` configuration file (e.g., `ExplicitExports="true"` to prevent un-annotated methods from becoming UDFs).



