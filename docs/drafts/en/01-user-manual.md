This is a draft written by Claude from the source code (2026-11-05). Not yet reviewed by the author.

# PressureCalculator user manual (code-based draft)

- Code basis: PressureCalculator `master` @ `1676a57` (displayed version ver1.009 (2025/10/16)), Crystallography @ `bd3caf4`, Crystallography.Controls @ `eca409d`.
- UI strings are the actual English strings (Designer text, identical to the `en` column of `PressureCalculatorLocalizationData.cs`).
- Formulae and parameters of the pressure scales are in [Pressure scales](02-pressure-scales.md); this page covers operation and the meaning of the settings only.

## 1. Windows and dialogs

| Window | Kind | Implementation | Content |
|---|---|---|---|
| Main window | `FormMain` (derived from `Crystallography.Controls.FormBase`) | `FormMain.cs`, `FormMain.designer.cs` | The only window of the application; three modes (Diamond Raman / Ruby Fluorescence / EOS) |
| Language change | MessageBox | `FormMain.cs:196–204` | "Switching the display language to "…" requires restarting PressureCalculator. Unsaved work will be lost. Restart now?" |
| Restart failed | MessageBox | `FormMain.cs:228–235` | "Failed to restart PressureCalculator." |
| Invalid number | MessageBox | `FormMain.cs:802–806` | "Please enter a valid number." (a diamond box could not be parsed) |
| Cannot open file | MessageBox | `FormMain.cs:1510–1514`, `1548–1552` | "Cannot open the file." |
| (non-interactive) smoke test | command line `--smoke [output file]` | `Program.cs:20–28` | Writes `arch=…` and `baseDir=…` to a file (default `smoke-result.txt`) and exits. For CI |
| (non-interactive) screenshots | command line `--capture <output folder> <culture>` | `Program.cs:30–35`, `GuiCapture.cs` | Starts in the given language, saves the ruby/diamond/EOS modes as `FormMain-ruby.png` etc. and exits; settings are not saved to the registry |

The window title is "Pressure Calculator  ver1.009 (2025/10/16)" (`FormMain.cs:58`) and becomes "Pressure Calculator    <file name>" after loading a file (`FormMain.cs:302`). `FormBase` appends "(F1: Help)" at the right end of the title.

## 2. Main window

<!-- screenshot: whole main window (ruby fluorescence mode, spectrum loaded and fitted, English UI) -->

### 2.1 Menu

| Menu | Item | Action | Implementation |
|---|---|---|---|
| File | Load | Open-file dialog; loads a spectrum and runs smoothing and fitting | `menuItemFileRead_Click` `FormMain.cs:1493–1517` |
| | Export as CSV | Saves the displayed spectrum and fit curves as CSV (§7) | `menuItemExport_Click` `FormMain.cs:1448–1489` |
| | Reload the file if it is updated | When checked, checks the loaded file's last-write time every second and reloads it if newer. On by default; not saved | `FormMain.cs:1520–1558`, Designer:3575–3577, 3606–3607 |
| Language | language names (native) | Confirms, then restarts in the chosen language; saved as registry value `Culture` | `FormMain.cs:164–240` |
| Help | Online manual | Opens the same URL as F1 (page of the current mode) in the default browser | `FormMain.cs:252–257`, `47–52`, `863–864` |

- With the English UI, the F1 / Online manual URL of each mode page returns 404 ([Q-2](03-implementation-questions.md)).

### 2.2 Mode selection and common controls

- Radio buttons at the top: **Diamond Raman** / **Ruby Fluorescence** / **EOS** (`radioButtonMode_CheckedChanged` `FormMain.cs:810–870`).
  - Diamond Raman: two graphs (top: original spectrum, bottom: first derivative) and the group "Pressure calculation from the Raman edge". Axis unit label `cm^-1`.
  - Ruby Fluorescence: bottom graph only (top pane collapsed to zero height) and the group "Pressure calculation from the ruby fluorescence". Axis unit label `nm`.
  - EOS: graphs hidden; a panel with one input group per material.
- **Number of decimal places** (0–10, default 4): display digits of all pressure outputs in every mode (`FormMain.cs:1563–1611`, Designer:874–887).
- Default mode: Diamond Raman in the Designer, but overwritten from the registry at startup; on first launch it is probably Ruby Fluorescence ([Q-5](03-implementation-questions.md)).

## 3. Loading a spectrum

- File → Load, or drag and drop **one** file onto the window (multiple files are ignored, `FormMain.cs:1418–1435`).
- Format (`readFile` / `ConvertUnknownFileToProfileData`, `FormMain.cs:262–417`):
  1. Read all lines as UTF-8, drop empty lines.
  2. Try the separators "`,`", "space", "tab" in this order. For each:
     - pick the longest block of **≥ 100 consecutive lines** having the same number (≥ 2) of numeric fields;
     - X = a column with a constant positive step (step > 1×10⁻⁵) over **≥ 100 points**;
     - Y = the remaining column with the largest variance.
  3. If all three fail, a simple comma parser: lines with ≥ 3 fields give (field 1, field 3) if field 1 has ≥ 3 characters, else (field 2, field 3); lines with 2 fields give (field 1, field 2).
  - The extension does not matter. X is taken as nm in ruby mode and cm⁻¹ in diamond mode (no conversion).
- After loading, smoothing → (diamond only) differentiation → peak fitting → pressure calculation run automatically.

## 4. Graph operation (common GraphControl of Crystallography.Controls)

Implementation: `GraphControl.pictureBox_MouseDown` (`GraphControl.cs:1604–1651`), `pictureBox_MouseUp` (`1755–1818`).

| Action | Effect |
|---|---|
| **Right**-button drag | Zoom into the rectangle (X and Y) |
| Small (< 3 px) right-button drag / right click | Zoom out by 1.5× |
| Middle-button drag | Pan |
| Left double-click | Copies the displayed profile as "X\tY" text to the clipboard. **In ruby mode it first fits a Lorentzian to the points within ±Fitting Range of the click and overwrites the R1 box** (`FormMain.cs:617–702`) |

- Every change of the display range re-runs the peak fit (`FormMain.cs:873–891`). **Only the points inside the displayed range are fitted**, so zoom such that the wanted peak is the maximum (ruby) / the derivative minimum (diamond) in the displayed range.
- In diamond mode the X ranges of the two graphs are linked.

## 5. Smoothing

<!-- screenshot: smoothing controls in diamond Raman mode (original-spectrum side and first-derivative side) -->

| Control | Default | Meaning | Implementation |
|---|---|---|---|
| Running Average | 10 (min 1) | n-point simple moving average; X is averaged too, so N−n+1 points remain | `FormMain.cs:427–441`, `476–490` |
| Gaussian blur σ | 8 | Weight $\exp(-\lfloor j^2/\sigma^2\rfloor)$, j = −50…50 (in data points); 0 = no smoothing. Step-like weights because of integer division ([Q-4](03-implementation-questions.md)) | `FormMain.cs:442–462`, `491–509` |

- The two are **mutually exclusive**: clicking (MouseDown on) one of the boxes activates it and makes the other read-only (grey) (`FormMain.cs:720–775`). By default Gaussian blur is active in both rows (Designer:384, 398).
- In diamond mode there are separate settings for "Original spectrum" and "First Differentiation". In ruby mode the settings of the bottom row (the "First Differentiation" row in the Designer, titled "Ruby Fluorescence" in this mode) apply to the fitted profile.

## 6. Peak fitting and "Fitting Information"

Implementation: `SearchBandEdge` (`FormMain.cs:516–614`), `FittingPeak.FitMultiPeaksThread` (`Crystallography/PeakSearch.cs:407–`).

| | Ruby Fluorescence | Diamond Raman |
|---|---|---|
| Profile fitted | smoothed spectrum | first derivative (forward difference) of the smoothed spectrum, smoothed again and **sign-inverted** |
| Start centre | maximum within the displayed range = R1; R2 at R1 − 1.5 nm | derivative minimum within the displayed range |
| Model | two pseudo-Voigt (R1, R2) + linear background | one pseudo-Voigt + linear background |
| Points used | [R1 − 1.5 − range, R1 + range] | [centre − range, centre + range] |
| Start FWHM | range / 2 | range |
| Result goes to | R1 box (`numericBoxRubyR1`) | ν box (`textBoxDiamondRamanNu`) |

- **Fitting Range** (range): integer, min 1, step 1 (Designer:798–802). Kept per mode (code defaults: ruby 3, diamond 6; `FormMain.cs:32–33`) and saved in the registry. Unit = axis unit (nm / cm⁻¹).
- **Fitting Information** shows for each peak `X` (centre), `FWHM`, `η` (pseudo-Voigt mixing) and `Background Y = intercept + slope * X` (`FormMain.cs:552`, `589–590`).
- Curves in the graph: diamond = red (fit); ruby = light green (R1), light cyan (R2), light pink (R1 + R2 + R2 background).

## 7. CSV export

File → Export as CSV (`FormMain.cs:1448–1489`); works only when the bottom graph has data.

- Ruby: header `X, Y (original), Y (smoothed),,X(R1),Y(R1),Y(R1_Bg),,X (R2),Y(R2), Y(R2_Bg)`
- Diamond: header `X, Y (original), Y (smoothed),, X(Edge),Y(Edge)`
- The X of the data rows is the smoothed X; with running-average smoothing "Y (original)" is shifted by (n−1)/2 points ([Q-12](03-implementation-questions.md)).
- Y(Edge) is the fit curve for the sign-inverted derivative (converted for display, background not subtracted).

## 8. Ruby Fluorescence mode

<!-- screenshot: right-hand panel in ruby mode (Reference condition, Measurement condition, Temperature dependency, Pressure calculation groups) -->

### 8.1 Procedure

1. Load a spectrum (or type the R1 wavelength [nm] into **R1**). After loading, the fit of §6 updates the R1 box.
2. In the **Measurement condition** group, set **R1₀=R1(T,P=0)**, the zero-pressure R1 at the measurement temperature (default 694.65 nm).
   - If the measurement temperature differs from the reference temperature, use **Temperature dependency (Ragan et al., 1992)**: enter the reference condition (step 3), enter **T =** of the measurement condition, and check **Calculate from Ragan's equation**; R1₀ is then computed (and the box becomes read-only).
3. **Reference condition** group: the R1 measured at zero pressure and its temperature, **R1(T=  , P=0)** (default 694.25 nm, 297.15 K). **Set the current R1** copies the current R1 box into the reference R1 (press it with a zero-pressure spectrum loaded). Every change of the reference condition recomputes the Ragan constant (the numeric box in the temperature-dependency group).
4. The **Pressure calculation** group shows the pressure (GPa) for the four formulae. The A coefficient of each is editable (exponents are fixed).

### 8.2 Settings

| UI text | Internal name | Default | Meaning | Implementation |
|---|---|---|---|---|
| R1 | `numericBoxRubyR1` | 694.65 nm | Measured R1; overwritten by the fit | Designer:1337–1350 |
| Reference condition: R1(T=  , P=0) = | `numericBoxRubyRefR1` | 694.25 nm | Zero-pressure R1 at the reference temperature | Designer:1318–1331 |
| Reference condition: temperature (box on the same line) | `numericBoxRubyRefT` | 297.15 K | Reference temperature | Designer:1300–1312 |
| Set the current R1 | `buttonRubyRefR1Set` | — | Copies R1 → reference R1 | `FormMain.cs:909` |
| Measurement condition: T = | `numericBoxRubyT` | 297.15 K (disabled by default) | Measurement temperature | Designer:1222–1236 |
| Same as reference | `checkBoxRubyTemeratureSameAsRef` | on | When checked, copies the reference T into the measurement T and disables that box; does not follow later changes ([Q-6](03-implementation-questions.md)) | `FormMain.cs:925–930` |
| R1₀=R1(T,P=0) | `numericBoxRubyR1_0` | 694.65 nm | Zero-pressure R1 used for the pressure | Designer:1256–1269 |
| Calculate from Ragan's equation | `checkBoxRubyR1_0CalculatedFromRagan` | off | Computes R1₀ from Ragan's equation | `FormMain.cs:937–979` |
| R1(T, P=0) = ( [c] + 4.49×10⁻²×T − … )⁻¹ ×10⁷ [nm] | `numericBoxRubyRagan` | 14423 | Constant c of Ragan's equation; recomputed from the reference condition, can also be typed | `FormMain.cs:917–922` |
| Temperature unit K / ℃ | `radioButtonTempUnitK/C` | K (Designer); possibly ℃ on first launch ([Q-5](03-implementation-questions.md)) | In ℃, +273.15 internally; lower limit switches to 0 K / −273.15 ℃ | `FormMain.cs:1019–1035` |
| P= [A] f(x, 5)= etc. | `numericBoxMaoA` etc. | 1904 / 1904 / 1904 / 1870 | Coefficient A (GPa) of each formula | Designer:1000–1069 |

## 9. Diamond Raman mode

<!-- screenshot: diamond Raman mode (top: original spectrum; bottom: first derivative with the red fit; right: Pressure calculation from the Raman edge) -->

### 9.1 Procedure

1. Load a Raman spectrum of the diamond anvil.
2. In the bottom graph (first derivative), zoom with a right-button drag so that the derivative minimum belonging to the high-frequency edge is the minimum of the displayed range. The fitted edge goes into **ν** (it can also be typed).
3. Check **ν0** (zero-pressure edge, default 1334 cm⁻¹).
4. The pressure (GPa) of the four formulae is shown.

### 9.2 Settings

| UI text | Internal name | Default | Meaning | Implementation |
|---|---|---|---|---|
| ν | `textBoxDiamondRamanNu` | 0 | Measured edge [cm⁻¹] | Designer:1472 |
| ν0 | `textBoxDiamondRamanNu0` | 1334 | Zero-pressure edge; used by Akahama (2006) and the two Fratanduono formulae; saved in the registry | Designer:1453, `FormMain.cs:118, 147` |
| Akahama (2004): P = [A] + [B] ν+ [C] E-4 ν^2= | `textBoxAkahama2004A/B/C` | 66.9 / -0.5281 / 3.585 | Quadratic in ν (absolute value) | Designer:423–443 |
| Akahama (2006): P = K0 [547] × Δν/ν0 × [1+1/2 (K0' [3.75] -1)*Δν/ν0]= | `textBoxAkahama2006K0/K0Prime` | 547 / 3.75 | | Designer:1491, 1501 |
| Fratanduono+ (2021, <300GPa) | — | fixed | Label coefficient 503.77 ≠ computed coefficient 530.77 ([Q-1](03-implementation-questions.md)) | `FormMain.cs:794` |
| Fratanduono+ (2021, >200GPa) | — | fixed | Quadratic that is meaningless below 200 GPa | `FormMain.cs:796` |

- If a coefficient box or ν cannot be parsed, "Please enter a valid number." appears every time ([Q-8](03-implementation-questions.md)).

## 10. EOS mode

<!-- screenshot: EOS mode (Gold, Platinum, NaCl B1, NaCl B2, Periclase, Corundum, Ar, Re groups) -->

### 10.1 Procedure

1. Enter **Temperature** (measurement temperature, K, default 300) and **T₀** (reference temperature, K, default 300) at the top.
2. In the group of the material used, enter the reference lattice parameter **a₀** (Å) and the measured **a** (Å); for Corundum and Re, **V₀** and **V** (Å³).
3. Every input recomputes all materials and scales (GPa) (`textBox_TextChanged` `FormMain.cs:1043–1055`).

### 10.2 Conventions to be aware of

- **Among the visible scales only Pt Matsui (09) uses T₀.** The other thermal scales use a hard-coded reference of 300 K (Ar Jephcoat: 4 K); a₀/V₀ are meant to be 300 K values.
- Scales that ignore T: Au/Pt Fratanduono (21), NaCl B2 Sata (02) and Sakai+(11), Ar Ross et al. (86), Re Anz, Sakai., Dub.
- Scales that ignore the UI a₀/V₀: all NaCl B2 scales (a₀ box disabled), both Ar scales (a₀ box unused, no default value set), Re Anz, Sakai., Dub. (fixed V₀).
- Tabulated scales (Jamieson, Tsuchiya, Yokoo, Brown, Zha, Ross) extrapolate linearly outside their tables.
- The Mo (Huang+(16)MGD, Zhao+(00)) and Pb (Strässle+(14)) groups are hidden (`Visible = false`) but computed internally.
- Details: [Pressure scales](02-pressure-scales.md).

### 10.3 Inputs and defaults per material

| Group | Inputs | Defaults | Scales shown |
|---|---|---|---|
| Gold | a₀, a [Å] | 4.07825 / 4.07825 | Jamieson (82), Anderson (89), Sim (02), Tsuchiya (03), Yokoo (09), Fratanduono (21) |
| Platinum | a₀, a [Å] | 3.9231 / 3.9231 | Jamieson (82), Holmes (89), Matsui (09), Yokoo (09), Fratanduono (21) |
| NaCl B1 | a₀, a [Å] | 5.639 / 5.639 | Brown (99), Matsui (12) |
| NaCl B2 | a [Å] (a₀ disabled) | a = 2.93 | Sata (02) (Pt), Sata (02) (Mg), Ueda+(08), Sakai+(11) BM, Sakai+(11) Vinet |
| Periclase | a₀, a [Å] | 4.2112 / 4.2112 | Jackson (98), Dewaele (00), Aizawa (06), Tange (09) Vinet, Tange (09) BM |
| Corundum | V₀, V [Å³] | 255.89472 / 255.89472 | Dubrovinsky (98) |
| Ar | a [Å] (a₀ unused) | a = 4.0786 | Ross et al. (86), Jephcoat (98) |
| Re | V₀, V [Å³] | 29.42795 / 29.42795 | Zha et al. (04), Anz (##), Sakai. (##), Dub. (##) |
| Mo (hidden) | V₀, V [Å³] | 31.14 / 31.14 | Huang+(16)MGD, Zhao+(00) |
| Pb (hidden) | a₀, a [Å] | 4.95059 / 4.95059 | Strässle+(14) |

Defaults from Designer:1845, 1870, 2125, 2150, 2272, 2297, 2508, 2754, 2790, 2853, 2889, 2978, 3197, 3233, 3334, 3370, 3434, 3470.

## 11. Saved settings (registry)

Key: `HKEY_CURRENT_USER\Software\Crystallography\PressureCalculator` (`FormMain.cs:96–157`, `Program.cs:40–47`). Saved on exit (not when run with `--capture`).

| Value name | Content |
|---|---|
| `formMainWidth`, `formMainHeight`, `formMainLocationX`, `formMainLocationY` | Window size and position |
| `radioButtonDiamondRaman.Checked`, `radioButtonEOS.Checked`, `radioButtonRubyFluorescence.Checked` | Mode |
| `fittingRangeRuby`, `fittingRangeDiamond` | Fitting Range per mode |
| `textBoxAkahama2006Nu0.Text` | ν0 |
| `numericBoxRubyRefR1.Text`, `numericBoxRubyRefT.Text` | Reference R1 and reference T |
| `textBoxMaoRamda0.Text` | Reference R1 again under another name (read back as `numericBoxRubyR1_0.Text`, a different name: [Q-5](03-implementation-questions.md)) |
| `radioButtonTempUnitK.Checked`, `radioButtonTempUnitC.Checked` | Temperature unit |
| `Culture` | UI language (read by `Program.Main` at startup) |

Not saved: measurement R1₀ and T, all coefficients (A, Akahama coefficients), all EOS inputs, smoothing, number of decimal places, file watching.

## 12. What the application does not do

- No inverse calculation from pressure to lattice parameter/volume, no pressure-unit conversion, no batch output/saving of all scales (`EOS.InverseThirdBirchMurnaghan` exists in the library but is unused).
- No °C option for EOS (K only).

## Items to confirm

- R-1-1 How much of the file-format detection of §3 (≥ 100-line blocks, X-column test `Math.Abs(... ) < 10`) should be documented for users?
- R-1-2 §5 "in ruby mode the bottom-row smoothing applies" is from reading `CalcSmoothingAndDifferentiation` (in ruby mode the bottom profile is the raw data smoothed with the bottom-row settings). The top-row result is used in ruby mode only for the "Y (smoothed)" export column. Which controls are visible on screen needs checking on Windows.
- R-1-3 First-launch defaults (mode, temperature unit, reference R1) need checking on Windows ([Q-5](03-implementation-questions.md)).
- R-1-4 The Ar a₀ box has no default value (probably shows 0); check on Windows.
- R-1-5 Screenshots must be taken on Windows with `--capture` (can the existing three images in `docs/src/assets/cap-*-auto/` be reused?).
- R-1-6 The reference-condition temperature box seems to be overlaid on the HeaderText "R1(T=                , P=0) = "; the actual layout needs checking.
