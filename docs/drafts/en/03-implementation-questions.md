This is a draft written by Claude from the source code (2026-11-05). Not yet reviewed by the author.

# Implementation questions

- Code basis: PressureCalculator `master` @ `1676a57`, Crystallography @ `bd3caf4`. Line references as in [Pressure scales](02-pressure-scales.md).
- No code was changed. The items below are suspected problems; how (or whether) to fix them is the author's decision.
- "How checked": **run** = values checked with the C# harness built from EOS.cs etc. on Linux and with Python; **read** = inferred from reading the code (not confirmed on Windows); **measured** = confirmed by accessing the live site.
- Severity is Claude's judgement (high: displayed pressure or a feature is wrong; medium: wrong under some conditions; low: labels/maintainability).

| ID | Severity | Summary | How checked |
|---|---|---|---|
| Q-1 | high | Linear coefficient of Fratanduono (<300 GPa): code 530.77 vs UI label 503.77 | read, run |
| Q-2 | high | In the English UI, F1 / "Online manual" opens a 404 page for every mode | measured |
| Q-3 | medium | The Debye-energy polynomial of MgO Tange (09) diverges at low T (θ/T ≳ 7) | run |
| Q-4 | medium | "Gaussian blur σ" weights use integer division and are not Gaussian | read |
| Q-5 | medium | Defaults on first launch (no registry values): read order and key names | read |
| Q-6 | medium | With "Same as reference" checked, the measurement T does not follow later changes of the reference T | read |
| Q-7 | low | Pt Matsui (09) electronic thermal pressure $P_{el}$ is not referenced to T₀ | read, run |
| Q-8 | low | Diamond boxes pop up an error dialog on every intermediate input (empty, "-") | read |
| Q-9 | low | Ruby caption "Δ=R1-R1₀" does not match the implementation (relative shift) | read |
| Q-10 | low | Summation index of the Debye integral `integ` is shifted by one step | run |
| Q-11 | low | XML doc comment of `EOS.MieGruneisen` gives wrong units/argument descriptions | read |
| Q-12 | medium | CSV export: column misalignment and possible exception | read |
| Q-13 | medium | The file-watch timer blocks the UI thread for 0.4 s every second | read |
| Q-14 | low | In ruby mode a double-click both fits a Lorentzian and copies to the clipboard | read |
| Q-15 | low | Gas constant and Avogadro number differ between scales | read |

---

### Q-1 Fratanduono (<300 GPa) coefficient mismatch (high)

- Code: `textBoxDiamondFratandunoLow.Text = (530.77 * nuPerNu0 + 753.83 * nuPerNu0 * nuPerNu0)` (`FormMain.cs:794`)
- UI label: `"Fratanduono+ (2021, <300GPa):  P = 503.77 × Δν/ν0 + 753.83 × (Δν/ν0)² = "` (Designer:1544)
- The existing manual (`docs/src/*/2-diamond-raman.md`, 11 languages) also says 503.77.
- Effect: at ν = 1400/1500/1600/1800 cm⁻¹ the code gives 28.105/77.721/135.808/277.400 GPa (530.77) versus 26.769/74.361/130.425/267.968 GPa (503.77). Only the linear term differs: $27\,x$ GPa (x = Δν/ν₀).
- The original paper is not bundled, so which one is correct could not be decided.

### Q-2 F1 help 404 in English (high)

- `HelpUrlResolver` in the `FormMain` constructor (`FormMain.cs:47–52`) returns `https://seto77.github.io/PressureCalculator/{HelpPage}/` for English (comment: "en はサイトルート直下", i.e. English at the site root).
- `docs/mkdocs.yml`, however, does not use the static-i18n plugin; English pages are in the nav as `en/1-ruby-fluorescence.md` etc. (`docs_dir: src`, `use_directory_urls: true`). `language_switch.js` also says "English content sits in an `en/` subfolder".
- Checked on 2026-11-05: `…/PressureCalculator/1-ruby-fluorescence/` → **404**, `…/PressureCalculator/en/1-ruby-fluorescence/` → 200, `…/PressureCalculator/ja/1-ruby-fluorescence/` → 200.
- `mkdocs-redirects` is listed in `docs/requirements.txt` but not enabled in `mkdocs.yml` `plugins`.
- Options (author's decision): add `en/` in the resolver, or add redirects with mkdocs-redirects.

### Q-3 MgO Tange (09) thermal pressure at low T (medium)

- `EOS.MgO_Tange_Vinet`/`MgO_Tange_BM` approximate the Debye energy function $e(u)=u^{-3}\int_0^u z^3/(e^z-1)dz$ by a 6th-order polynomial (`EOS.cs:363–366`, `388–391`).
- Compared with the exact value in Python: relative error < 10⁻⁴ up to u = 6, −4 % at u = 7.6, sign reversal at u = 10 (−0.0203 vs +0.0064), −1.58 vs +0.0019 at u = 15.
- For MgO (θ₀ = 761 K) at V/V₀ = 1, T ≈ 100 K corresponds to u ≈ 7.6. Thermal pressure of Tange (09) Vinet at V/V₀ = 1:

  | T | θ/T | polynomial (implemented) | exact Debye function |
  |---|---:|---:|---:|
  | 200 K | 3.81 | −0.4153 GPa | −0.4153 GPa |
  | 100 K | 7.61 | −0.6431 | −0.6419 |
  | 77 K | 9.88 | −0.6934 | −0.6590 |
  | 50 K | 15.22 | −2.3899 | −0.6670 |
  | 10 K | 76.10 | −34755.5 | −0.6688 |

- The EOS Temperature box has no minimum (`NumericBox.Minimum` defaults to −∞ and the Designer sets none), so even values ≤ 0 K are accepted, and in low-temperature DAC work only the two Tange boxes go badly wrong. The other MGD scales (Jackson, Dewaele, Aizawa) integrate numerically and are not affected.

### Q-4 "Gaussian blur σ" uses integer division (medium)

- Weight `Math.Exp(-j * j / n / n)` (`FormMain.cs:456–457`, `504–505`) with `int j` and `int n`. `-j * j / n / n` is evaluated in integer arithmetic, so the weight is the step function $\exp(-\lfloor j^2/n^2\rfloor)$ (truncation toward zero).
- Example with σ = 8 (default): weight 1 for |j| ≤ 7, e⁻¹ for 8–11, e⁻² for 12–13, …; the window is always cut at ±50 points.
- Even with real division, $e^{-j^2/\sigma^2}$ is a Gaussian with standard deviation $\sigma/\sqrt2$, i.e. off by √2 from the label "σ". The unit is data points, not axis units.
- Effect: the peak position hardly changes, but the amount of smoothing differs from what the label suggests.

### Q-5 Defaults on first launch (medium)

Reading `FormMain.ReadInitialRegistry` (`FormMain.cs:96–130`) for the case where `HKCU\Software\Crystallography\PressureCalculator` has no values (first launch):

1. Mode: `radioButtonDiamondRaman`, `radioButtonEOS`, `radioButtonRubyFluorescence` are set in this order with default `"True"` (`FormMain.cs:111–113`). All three are in `flowLayoutPanel1` (Designer:845–847), so the last one, **ruby fluorescence**, wins — not the Designer default (`radioButtonDiamondRaman.Checked = true`, Designer:819).
2. Temperature unit: K then °C, both default `"True"` (`FormMain.cs:123–124`), both in `groupBoxMao` (Designer:914–915), so **°C** is selected and the value 297.15 in the reference/measurement T boxes is then read as **297.15 °C**.
3. Reference R1: `numericBoxRubyRefR1.Text = GetValue("numericBoxRubyRefR1.Text", numericBoxRubyRefT.Text)` (`FormMain.cs:121`) passes the **temperature box text** ("297.15") as default, so the reference R1 becomes **297.15 nm**. This value is saved on exit (`FormMain.cs:150`) and persists until the user corrects it.
4. Key-name mismatch: it reads `"numericBoxRubyR1_0.Text"` (`FormMain.cs:119`) but saves `"textBoxMaoRamda0.Text"` (`FormMain.cs:148`), both for `numericBoxRubyRefR1`; `numericBoxRubyR1_0` (the measurement-condition R1₀) itself is neither saved nor restored.

- Because of 3, checking "Calculate from Ragan's equation" should give R1₀ ≈ 297 nm and a large negative ruby pressure (not run).
- Needs confirmation on Windows (R-Q5).

### Q-6 "Same as reference" does not track (medium)

- `checkBoxRubyTemeratureSameAsRef_CheckedChanged` (`FormMain.cs:925–930`) copies `numericBoxRubyT.Value = numericBoxRubyRefT.Value` only when the check state changes.
- `numericBoxRubyRefT_ValueChanged` (`FormMain.cs:911`) only calls `calcRaganParameter()` and does not update the measurement T.
- By default the box is checked and the measurement-T box is disabled (Designer:1223, 1242), so after changing the reference T the measurement T stays at the old value and cannot be edited.

### Q-7 Pt Matsui (09) $P_{el}$ (low)

- `double Pel = -1.01162E-11 * T * T * T + 1.89796E-07 * T * T + 6.73266E-05 * T - 3.70065E-02;` (`EOS.cs:540`) depends only on the absolute T; nothing is subtracted at T₀ (the UI value passed at `FormMain.cs:1103`). $P_{el}(300\,{\rm K})=-1.7\times10^{-8}$ GPa, so this is fine for T₀ = 300 K, but for T₀ ≠ 300 K the lattice thermal pressure (referenced to T₀) and the electronic one (referenced to 300 K) are inconsistent.
- $P_{el}$ has no volume dependence (it is a fit in T only to the numbers in the comment).

### Q-8 Error dialog in the diamond boxes (low)

- `textBoxNu_TextChanged` (`FormMain.cs:780–808`) is attached to `TextChanged` of ν, ν₀ and the Akahama 2004/2006 coefficient boxes (Designer:424, 434, 444, 1454, 1473, 1492, 1502) and shows a MessageBox whenever `Convert.ToDouble` fails — also while retyping a coefficient (empty box, a lone "-", …).

### Q-9 Ruby caption (low)

- Caption `groupBox4.Text = "Pressure calculation, where x = R1/R1₀ ,  f(x, y) = (x^y-1)/y ,  Δ=R1-R1₀"` (Designer:981); Shen box `numericBoxShenA.FooterText = "(Δ+5.63 Δ²)="` (Designer:1038).
- The implemented Δ is $(R1-R1_0)/R1_0$ (`FormMain.cs:1014`), which matches the bundled Shen et al. PDF; the caption should probably read "Δ=(R1-R1₀)/R1₀".

### Q-10 Index of `integ` (low)

- `for (int i = 1; i <= num; i++) { z = (i + 0.5) * x / num; ... }` (`EOS.cs:339–343`). A midpoint rule would use `i = 0 … num-1`; the current code integrates over $[x/n,\,x(1+1/n)]$, a relative error of order $10^{-5}$.
- Effect: ≤ 4×10⁻⁴ GPa at 2000 K for the scales with an MGD term (Sim, Matsui09, Matsui12, Jackson, Dewaele, Aizawa, Huang, Jephcoat); this accounts for the C#/Python residuals in §4. Negligible in practice.

### Q-11 Doc comment of `EOS.MieGruneisen` (low)

- The XML comment (`EOS.cs:179–190`) says `v0` "Stamdard volume (nm^3)", `v` "Target temperature", `t` "Target volume (nm^3)". In the code `t` is the temperature and `v` the volume, and the conversion `v * 6.0221367 / z` with `/ 100` (to GPa) is consistent only with **Å³**; FormMain passes Å³.

### Q-12 CSV export (medium)

`menuItemExport_Click` (`FormMain.cs:1448–1489`):

1. Data rows are `$"{smoothPt[i].X},{originalPt[i].Y},{smoothPt[i].Y}"` (`FormMain.cs:1469`). With running-average smoothing, `OriginalSmooth` has N−n+1 points and averaged X (`FormMain.cs:431–441`), so "Y (original)" is written next to an X shifted by (n−1)/2 points. With Gaussian smoothing X is unchanged.
2. The loop that appends the fit-curve columns, `for (int i = 0; i < textList.Count && (i < r1Pt.Length || i < r2Pt.Length); i++) textList[i + 1] += ...` (`FormMain.cs:1472–1476`), writes to `i + 1`; if the fit curve (`range * 1.8 / (range / 200)` ≈ 360 points) has at least as many points as there are data rows, `i + 1 == textList.Count` can throw (spectra with fewer than ≈360 points).
3. In ruby mode "Y(R1)" is the R1 component plus R1 background and "Y(R2)" the R2 component plus R2 background; the summed curve (pink line in the graph) is not exported.

### Q-13 File-watch timer (medium)

- `timer` (Interval 1000 ms, Designer:3606–3607) is started unconditionally in `FormMain_Load` (`FormMain.cs:71`). Once a file has been loaded, `timer_Tick` (`FormMain.cs:1528–1558`) calls `Thread.Sleep(200)` twice on the UI thread every second (`FormMain.cs:1534`, `1556`), freezing the UI for ≈0.4 s/s even when the file is unchanged.
- "Reload the file if it is updated" is not saved and is always on at startup (Designer:3575).

### Q-14 Double-click in ruby mode (low)

- `MouseDoubleClick2` of both graphs is wired to `graphControlBottom_MouseDoubleClick2` (Designer:660, 727). In ruby mode it fits a Lorentzian (no background) by grid search to the points within ±Fitting Range of the click and overwrites the R1 box (`FormMain.cs:617–702`).
- It always returns `false`, so GraphControl's default action (tab-separated copy of the displayed profile to the clipboard, `GraphControl.cs:1638–1648`) runs as well.
- In diamond mode it returns `false` immediately (no Lorentzian fit, clipboard copy only).

### Q-15 Physical constants (low)

- R: `UniversalConstants.R = 8.3144621` (9-argument MGD, Pt Matsui), `8.31451` (`EOS.GetPressure`, Ar Jephcoat), `8.3145` (Tange). N_A: `6.0221367` (mostly), `6.0221` (Tange). Relative differences ≤ 6×10⁻⁶, numerically negligible.

## Items to confirm

- R-Q1 The correct coefficient for Q-1 (check the paper).
- R-Q2 Fix Q-2 in the resolver or on the site?
- R-Q3 Q-3: add a lower temperature limit for Tange (09) in the UI, or replace the polynomial by numerical integration?
- R-Q5 Items 1–4 of Q-5 are inferred from the code. Needs a check on Windows with the registry key deleted.
- R-Q12 Whether item 2 of Q-12 (exception) actually occurs for spectra with fewer than ≈360 points.
- R-Q13 Whether the UI freeze of Q-13 is noticeable.
