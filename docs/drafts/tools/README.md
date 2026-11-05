This is a draft written by Claude from the source code (2026-11-05). Not yet reviewed by the author.

# Verification tools / 検証用ツール

Used for §4 of `../ja/02-pressure-scales.md` / `../en/02-pressure-scales.md`. Not part of the application build (C# files carry a `.txt` extension so that no project picks them up).

| File | Content |
|---|---|
| `cs_harness/Program.cs.txt` | C# console harness. Calls the Crystallography `EOS` functions and pastes the FormMain expressions verbatim; writes `material,scale,input,T,P` lines |
| `cs_harness/Stubs.cs.txt` | Minimal `PointD` / `Profile` / `UniversalConstants` (values copied from Crystallography) |
| `cs_harness/EosHarness.csproj.txt` | Project file (net8.0, MathNet.Numerics 5.0.0) |
| `cs_harness/pba0.txt` | Pb a₀(T) table extracted from `FormMain.cs:1316–1378` |
| `cs_harness/cs_out.csv` | Output of the harness (567 lines) |
| `py_eos.py` | Independent Python implementation (numpy, scipy). Usage: `python3 py_eos.py <Crystallography/EOS.cs> <PressureCalculator/FormMain.cs> cs_out.csv compare.csv` |
| `compare.csv` | C# vs Python for every point (`P_cs`, `P_py`, `diff`) |
| `mk_tables.py` | Builds the Markdown tables of §4 from `compare.csv` |

## Rebuilding the C# harness (Linux, .NET 8 SDK)

```sh
mkdir harness && cd harness
dotnet new console -n EosHarness -o . --force
dotnet add package MathNet.Numerics --version 5.0.0
sed 's/^using System.Windows.Forms;//' <Crystallography>/EOS.cs > EOS.cs
{ sed -n 1,7p <Crystallography>/Mathematics/Spline.cs; sed -n '121,$p' <Crystallography>/Mathematics/Spline.cs; } > Spline.cs   # Spline class only
{ echo 'using MathNet.Numerics.LinearAlgebra; using MathNet.Numerics.LinearAlgebra.Double;'; sed -n 20,87p <Crystallography>/ExtensionMethods.cs | sed 's/^#region.*//'; echo '}'; } > MathnetEx.cs
cp <this folder>/cs_harness/Program.cs.txt Program.cs
cp <this folder>/cs_harness/Stubs.cs.txt Stubs.cs
cp <this folder>/cs_harness/pba0.txt .
dotnet build -c Release && dotnet bin/Release/net8.0/EosHarness.dll > cs_out.csv
```

Crystallography commit used: `bd3caf4fb058af18c89b878c9c10b0941f7590ea`.
