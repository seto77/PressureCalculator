This is a draft written by Claude from the source code (2026-11-05). Not yet reviewed by the author.

# Existing manual vs. current code

## Existing documentation inventoried

| Document | Location | Status |
|---|---|---|
| Online manual (MkDocs Material, 11 languages × 5 pages) | `docs/src/{index.md, en/, ja/, de/, …}/`, config `docs/mkdocs.yml`, published to GitHub Pages by `pages.yml` | Compared here (read English `docs/src/en/*.md` + `docs/src/index.md` and Japanese `docs/src/ja/*.md`; the other 9 languages were assumed to be translations of the same text and not read) |
| Portable-package README | `README-PORTABLE.txt` | Installation only; no conflict with the code |
| Version history | `History` in `PressureCalculator/Version.cs` | Last entry ver1.009 (2025/10/16) |
| Reference PDFs | `references/` (Ragan et al 1992 JAP, Akahama 2006, Shen et al HPR 2020, Syassen 2008 HPR) | Used to check formulae ([Pressure scales](02-pressure-scales.md)) |
| GitHub Wiki | `https://github.com/seto77/PressureCalculator.wiki.git` | Could not be cloned (authentication prompt; probably no wiki) |
| Top-level README.md | — | Does not exist |

Code basis: PressureCalculator `master` @ `1676a57`, Crystallography @ `bd3caf4`, Crystallography.Controls @ `eca409d`.

## A. Statements that conflict with the code

The "Manual" column quotes the English version (`docs/src/en/…`); the Japanese version says the same.

| # | Priority | Manual | Current code | Evidence |
|---|---|---|---|---|
| A-1 | high | 2-diamond-raman: Fratanduono et al. (2021, <300 GPa) **P = 503.77 x + 753.83 x²** | Computes **530.77** x + 753.83 x² (the UI label says 503.77, like the manual) | `FormMain.cs:794`, Designer:1544. [Q-1](03-implementation-questions.md) |
| A-2 | high | 3-equation-of-states lists **Mo** (Zhao+ (2000), Huang+ (2016) MGD) and **Pb** (Strässle+ (2014)) | The Mo and Pb groups are **hidden** (computed, but not shown) | `groupBoxMo.Visible = false` Designer:3251, `groupBoxPb.Visible = false` Designer:3386 |
| A-3 | high | index / Online help: "Press F1 (or Help → Online manual) to open the page for the current mode" | In the English UI every mode page URL is a 404 (other languages work) | `FormMain.cs:47–52`; checked 2026-11-05. [Q-2](03-implementation-questions.md) |
| A-4 | medium | 4-spectra-and-fitting / Graphs: "Drag a rectangle with the **left** mouse button to zoom; a very small drag zooms out step by step" | Zoom is a **right**-button drag; a small (< 3 px) right-button drag zooms out by 1.5×; middle-button drag pans; a left single click selects a vertical line | `Crystallography.Controls/GraphControl.cs:1604–1651` (MouseDown), `1755–1818` (MouseUp) |
| A-5 | medium | 4-spectra-and-fitting / Graphs: "Double-clicking copies the displayed profile data to the clipboard" | True in diamond mode. **In ruby mode it first fits a Lorentzian around the clicked point and overwrites the R1 box**, then copies | `FormMain.cs:617–702`, `GraphControl.cs:1636–1648`. [Q-14](03-implementation-questions.md) |
| A-6 | medium | 4-spectra-and-fitting / Smoothing: "two **independent** filters (Running Average, Gaussian blur σ)" | The two are **mutually exclusive**: the one clicked becomes active, the other read-only. Default: Gaussian blur (σ = 8) active. σ is in data points and the weights are step-like because of integer division | `FormMain.cs:421–510`, `720–775`, Designer:384, 398, 523, 580. [Q-4](03-implementation-questions.md) |
| A-7 | medium | 4-spectra-and-fitting / Fitting: "Fitting Range: **width** of the region around the peak (or derivative minimum) used for fitting" | Only points **inside the current X display range** are used; the start centre is the maximum (ruby) or the derivative minimum (diamond) within that range. Fitting Range is a **half-width** around the start centre (ruby: [R1−1.5−range, R1+range], covering the R2 start at R1 − 1.5 nm) and also sets the start FWHM (ruby range/2, diamond range). Integer (min 1, step 1) | `FormMain.cs:516–614` (`SearchBandEdge`), `Crystallography/PeakSearch.cs:53–59, 440–450` |
| A-8 | medium | 3-equation-of-states / Workflow 1: "Enter the measurement temperature and the reference temperature T₀. Thermal equations of state use them" | **T₀ is used only by Pt Matsui (09) and Pb (hidden).** All other thermal scales have a hard-coded reference of 300 K (Ar Jephcoat: 4 K) | [Pressure scales §0.2](02-pressure-scales.md) |
| A-9 | medium | 3-equation-of-states / Workflow 2: "For each standard material, enter a₀ and a" | NaCl B2 a₀ is disabled and unused; Ar a₀ is unused; Re V₀ is used only by Zha (Anz/Sakai/Dub use fixed V₀) | Designer:2526, `EOS.cs:438–477, 1367–1396`, `FormMain.cs:1240–1244` |
| A-10 | medium | 3-equation-of-states / Workflow 2: "For corundum and rhenium, V₀ and V are entered" | Mo (hidden) also takes volumes. Correct but incomplete | Designer:3318, 3354 |
| A-11 | medium | 3-equation-of-states / table: Re "Zha et al. (2004) **and others**" | Four boxes: Zha et al. (04), Anz (##), Sakai. (##), Dub. (##); the last three have "##" instead of a year in the UI | Designer:3103, 3138, 3173, 3209 |
| A-12 | medium | 3-equation-of-states / Workflow 1: "room-temperature scales ignore the difference" | T is ignored by Au/Pt Fratanduono, NaCl B2 Sata/Sakai, Ar Ross, Re Anz/Sakai/Dub; the manual does not say which scales are thermal | [Pressure scales §3](02-pressure-scales.md) |
| A-13 | low | 1-ruby-fluorescence: $P=\frac{A}{y}[(R1/R1_0)^y-1]$, table "Mao (1978) A=1904, y=5 / Mao-quasi y=7.665 / Mao-hydro y=7.715", "the coefficients are editable" | Only A is editable; y and Shen's 5.63 are fixed in code. Default A is 1904 for all three Mao formulae, 1870 for Shen | `FormMain.cs:1006–1016`, Designer:1012, 1031, 1050, 1069 |
| A-14 | low | 1-ruby-fluorescence / Workflow 3: "Set the reference condition (the R1 wavelength R1₀ at zero pressure). Set the current R1 copies the fitted R1 into the reference box" | The "Reference condition" is the pair **R1(T_ref, P=0), T_ref**, used to calibrate the constant c of Ragan's equation. The R1₀ used for pressure is the `R1₀=R1(T,P=0)` box in the **"Measurement condition"** group (default 694.65 nm); it is computed from the reference only when "Calculate from Ragan's equation" is checked. Otherwise the reference condition does not affect the pressure | `FormMain.cs:909–1017`, Designer:1205, 1259, 1269, 1282 |
| A-15 | low | 1-ruby-fluorescence / Temperature: "Same as reference: assume the measurement temperature equals the reference temperature" | Copies the value only at the moment the box is checked; later changes of the reference T are not followed | [Q-6](03-implementation-questions.md) |
| A-16 | low | 1-ruby-fluorescence / Temperature: applicable 50-600 K | Matches the UI label; the bundled Ragan PDF fits 15–600 K | Designer:1173, `references/Ragan et al 1992JAP.pdf` |
| A-17 | low | 2-diamond-raman / Workflow 2: "the edge appears as a minimum of the derivative; fitted within the Fitting Range" | A pseudo-Voigt + linear background is fitted to the **sign-inverted** derivative and its centre is the edge. The derivative is a forward difference of the smoothed spectrum, placed at the midpoint of adjacent points | `FormMain.cs:530–565`, `Crystallography/Profile.cs:329–348` |
| A-18 | low | 2-diamond-raman / table: Akahama (2004) "polynomial in the edge position" | $P = A + B\nu + C\times10^{-4}\nu^2$ with **absolute** ν (not Δν; ν₀ unused); defaults A=66.9, B=-0.5281, C=3.585 | `FormMain.cs:789`, Designer:423–443 |
| A-19 | low | 4-spectra-and-fitting / Loading: "the first numeric column is the horizontal axis and the following column the intensity" | Tries separators `,` → space → tab; (1) picks the longest block of ≥ 100 consecutive lines with the same number (≥ 2) of numbers, (2) X = a column with a constant positive step over ≥ 100 points, (3) Y = the remaining column with the largest variance. If all fail, a simple comma parser: for ≥ 3 fields, (field 1, field 3) if field 1 has ≥ 3 characters, else (field 2, field 3). Encoding fixed to UTF-8 | `FormMain.cs:262–417` (`readFile`, `ConvertUnknownFileToProfileData`) |
| A-20 | low | 4-spectra-and-fitting / Export: "the displayed spectra (original, smoothed, fitted curves)" | Columns: "X, Y (original), Y (smoothed),, X(R1), Y(R1), Y(R1_Bg),, X (R2), Y(R2), Y(R2_Bg)" (ruby) or "X, Y (original), Y (smoothed),, X(Edge), Y(Edge)" (diamond); misaligned with running-average smoothing | `FormMain.cs:1448–1489`. [Q-12](03-implementation-questions.md) |
| A-21 | low | index / Installation: the MSI needs .NET Desktop Runtime 10 | `TargetFramework` is `net10.0-windows`; consistent | `PressureCalculator.csproj:7` |
| A-22 | low | index: settings under `HKEY_CURRENT_USER\Software\Crystallography\PressureCalculator` | Consistent (`FormMain.cs:100, 134`, `Program.cs`) | — |

## B. Features and conventions in the code that the manual does not describe

| # | Content | Evidence |
|---|---|---|
| B-1 | **Number of decimal places** (`numericBoxDecimalPlaces`, 0–10, default 4) applies to every output box in all three modes (the manual mentions it only in the spectra chapter) | `FormMain.cs:1563–1611`, Designer:874–887 |
| B-2 | The **A value** of each ruby formula (default 1904/1904/1904/1870) and the Ragan constant (default 14423, recomputed from the reference condition) are editable | Designer:1000–1069, 1178–1191 |
| B-3 | The **K/°C** switch exists only in ruby mode (EOS is K only). In °C, +273.15 is added internally and the lower limit switches between 0 K and −273.15 °C | `FormMain.cs:1019–1035` |
| B-4 | Defaults on first launch (inferred): ruby mode, °C, reference R1 = 297.15 nm | [Q-5](03-implementation-questions.md) |
| B-5 | What is saved: window position/size, mode, Fitting Range (ruby and diamond separately), ν₀, reference R1 and T, temperature unit, UI language. Not saved: measurement R1₀, coefficients, EOS inputs, smoothing, file watching | `FormMain.cs:132–157` |
| B-6 | Fitting Range is kept separately per mode (code defaults: ruby 3 nm, diamond 6 cm⁻¹; the Designer value 4 is overwritten on mode change) | `FormMain.cs:32–33, 816, 842, 893–900` |
| B-7 | Only a single dropped file is loaded (multiple files are ignored) | `FormMain.cs:1418–1435` |
| B-8 | File watching polls every second by last-write time; not saved, always on at startup | `FormMain.cs:1520–1558`, Designer:3575, 3606–3607 |
| B-9 | Non-interactive modes: `--smoke [output file]` (writes architecture and base folder, exits); `--capture <output folder> <culture>` (saves PNG screenshots of the three modes, exits, does not save settings) | `Program.cs:16–34`, `GuiCapture.cs` |
| B-10 | The window title shows version and date (`Version.VersionAndDate`), and the file name after loading | `FormMain.cs:58`, `302` |
| B-11 | There is **no** inverse calculation (P → a/V) and no pressure-unit conversion (`EOS.InverseThirdBirchMurnaghan` exists in the library but is not used by the UI) | `EOS.cs:287–309`, FormMain |
| B-12 | Formulae, parameter values, reference temperatures and V₀ handling of each scale (the manual gives only some formulae and the scale names) | [Pressure scales](02-pressure-scales.md) |

## C. Version history (Version.cs)

- The latest `Version.History` entry is "ver1.009 (2025/10/16) Improved the EOS functions." (ver1.008 has the same date and text). `AssemblyVersion`/`FileVersion` are `2025.10.16.0829` (`PressureCalculator.csproj:9–10`).
- The code, however, contains many 2026 change comments (`260703Cl` … `260805Cl`: FormBase base class, F1 help, 11 languages, `--smoke`/`--capture`, DPI mode, the space in the version string, …) that are not in the history. The manual (11 languages, F1 help, arm64 builds) already assumes them.
- ver1.002 "Added two diamond Raman equations (Fratanduono et al., 2021)" and ver1.004 "Fitting information is now displayed" are reflected in the manual (but see A-1).

## Items to confirm

- R-0-1 A-1: which is authoritative — code, label or manual?
- R-0-2 A-2: will Mo/Pb be shown in the UI? If not, remove them from the manual or mark them as hidden.
- R-0-3 A-4/A-5 are common GraphControl behaviour (Crystallography.Controls); fix together with the sibling applications' manuals?
- R-0-4 Are the other 9 language versions identical in content to English/Japanese (not read here)?
- R-0-5 Add the 2026 changes to the Version.cs history? (Changing the version is a code change and was not done here.)
- R-0-6 Does a wiki exist (clone failed with an authentication prompt)?
