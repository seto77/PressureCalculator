This is a draft written by Claude from the source code (2026-11-05). Not yet reviewed by the author.

# Pressure scales (as implemented)

- Code basis: PressureCalculator `master` @ `1676a57`, Crystallography submodule @ `bd3caf4` (the commit referenced by the gitlink).
- Line references: `FormMain.cs:1068` = line 1068 of `PressureCalculator/FormMain.cs`; `EOS.cs:350` = line 350 of `Crystallography/EOS.cs`; `Designer:1012` = line 1012 of `PressureCalculator/FormMain.designer.cs`.
- Parameter values are copied verbatim from the code (including exponent notation such as `-1.41E-2`).
- The "Source" column only quotes what is written in code comments, UI labels, the existing manual, or the PDFs shipped in `references/`. Bibliographic details (journal, volume, pages) are not in the code and are not given.
- The "Range" column only gives what a UI label states and the grid of tabulated data (outside of which the code extrapolates). The validity ranges in the original papers were not checked.

## 0. Common conventions

### 0.1 Units and inputs

| Quantity | Unit | Where |
|---|---|---|
| Lattice parameter a, a₀ | Å (UI input) | e.g. `numericalTextBoxGoldA.FooterText = "Å"` Designer:1829 |
| Volume V, V₀ | Å³ (UI input; corundum, Re, Mo) | Designer:2837, 3181, 3318 |
| Pressure | GPa | every output box, `FooterText = "GPa"` |
| Temperature (EOS) | K only (`numericBoxTemperature`, `numericBoxTemperature0`; no °C switch) | Designer:3499, 3524 |
| Temperature (ruby) | K or °C (`radioButtonTempUnitK/C`); °C is converted with +273.15 | `FormMain.cs:920, 971` |
| Ruby wavelength | nm | Designer:1258, 1320, 1339 |
| Diamond wavenumber | cm⁻¹ | Designer:1463, 1482 |

- For cubic materials (Au, Pt, NaCl B1/B2, MgO, Ar, Pb) the user enters a; FormMain forms $V=a^3$ and $V_0=a_0^3$ (e.g. `FormMain.cs:1060–1061`).
- Some functions receive `a / 10` (nm): Au Jamieson/Anderson/Tsuchiya, Pt Jamieson/Holmes/Matsui, NaCl Brown, Ar. Functions that only use ratios are unit-independent; the two Ar functions convert to molar volume and therefore assume nm (§3.8).

### 0.2 Temperature and reference state

- The UI **Temperature** (`numericBoxTemperature`, default 300 K, Designer:3515) is passed to every EOS scale.
- The UI **T₀** (`numericBoxTemperature0`, default 300 K, Designer:3540) is used only by **Pt Matsui (09)** (`FormMain.cs:1103`) and **Pb Strässle+(14)** (`FormMain.cs:1394`, `var t0 = numericBoxTemperature0.Value`).
- All other thermal scales have their reference temperature hard-coded: 300 K (6th argument of `MieGruneisen(..., 300, ...)`, `300.0`/`t0 = 300.0` in Tange, `(T - 300)` in Anderson/Holmes/Ueda, `T0 = 300` in Dubrovinsky and in Mo Zhao), and `T0 = 4` for Ar Jephcoat (`EOS.cs:1386`).
- Some scales ignore the UI a₀/V₀ and use a hard-coded reference volume (all five NaCl B2 scales, both Ar scales, Re Anz/Sakai/Dub). See the "V₀" column below.
- Only Dubrovinsky98 (Al₂O₃), Zhao+00 (Mo) and Strässle+14 (Pb) treat thermal expansion explicitly as V₀(T). The other thermal scales are of the form $P(V,T)=P_{300}(V)+\Delta P_{th}(V,T)$ with V₀ taken as the 300 K value.

### 0.3 Common formulae (as implemented)

With $x \equiv V_0/V$ and $y \equiv (V/V_0)^{1/3}$:

**Third-order Birch–Murnaghan** (`EOS.BirchMurnaghan3rd`, `EOS.cs:270–271`; the 4-argument overload at `EOS.cs:261–262` forms $V_0/V$ and delegates)

$$P_{\rm BM3}=\tfrac32K_0\left(x^{7/3}-x^{5/3}\right)\left[1-\tfrac34(4-K_0')\left(x^{2/3}-1\right)\right]$$

**Fourth-order Birch–Murnaghan** (`EOS.BirchMurnaghan4th`, `EOS.cs:273–276`)

$$P_{\rm BM4}=\tfrac32K_0\,x^{5/3}\left(x^{2/3}-1\right)\left[1+\tfrac34(K_0'-4)\left(x^{2/3}-1\right)+\frac{9K_0K_0''+9K_0'^2-63K_0'+143}{24}\left(x^{2/3}-1\right)^2\right]$$

(Checked to be algebraically identical to the standard Eulerian form $3K_0f(1+2f)^{5/2}[1+\tfrac32(K'-4)f+\tfrac32(K_0K''+(K'-4)(K'-3)+\tfrac{35}{9})f^2]$ with $f=(x^{2/3}-1)/2$.)

**Vinet** (`EOS.Vinet`, `EOS.cs:314–318`)

$$P_{\rm V}=3K_0\,\frac{1-y}{y^2}\exp\!\left[\tfrac32(K_0'-1)(1-y)\right]$$

**Mie–Grüneisen–Debye thermal pressure, power-law γ** (`EOS.MieGruneisen`, 9-argument overload, `EOS.cs:191–205`)

$$\gamma=\gamma_0\left(\frac{V}{V_0}\right)^{q},\quad \theta=\theta_0\exp\!\left(\frac{\gamma_0-\gamma}{q}\right),\quad E(T)=9nRT\left(\frac{T}{\theta}\right)^3\!\int_0^{\theta/T}\!\frac{z^3}{e^z-1}dz$$

$$\Delta P_{th}=\frac{\gamma}{V_m}\,[E(T)-E(T_0)],\qquad V_m=\frac{V N_A}{Z}$$

- Argument order `(z, n, theta0, gamma0, q, t0, v0, t, v)`. v and v0 are **Å³ per unit cell** (the XML doc comment says "nm^3", "v: Target temperature", "t: Target volume", but the conversion `v * 6.0221367 / z` together with `/ 100` is consistent with Å³; see Q-11).
- R = `UniversalConstants.R = 8.3144621` (`UniversalConstants.cs:114`).
- `integ(x)` is a 100000-step midpoint sum (`EOS.cs:333–346`).
- No zero-point energy (it cancels in $E(T)-E(T_0)$ for the same θ).

**Tange type (Al'tshuler γ)** (`EOS.MgO_Tange_Vinet` `EOS.cs:348–372`, `EOS.MgO_Tange_BM` `EOS.cs:374–397`)

$$\gamma=\gamma_0\left[1+a\left(\left(\tfrac{V}{V_0}\right)^b-1\right)\right],\quad \theta=\theta_0\left(\tfrac{V}{V_0}\right)^{-(1-a)\gamma_0}\exp\!\left(-\frac{\gamma-\gamma_0}{b}\right)$$

$$\Delta P_{th}=\frac{\gamma}{V_m}\cdot 9nR\,[T\,e(\theta/T)-300\,e(\theta/300)]\times10^{-9}$$

where $e(u)$ is a 6th-order polynomial approximation of $u^{-3}\int_0^u z^3/(e^z-1)dz$ (`EOS.cs:363–366`, `388–391`; coefficients `-0.0000013397592, 0.000035022653, -0.00031034588, 0.00018490174, 0.016520035, -0.12495878, 0.33333333`). R = `8.3145`, $N_A$ = `6.0221` (`v0mol = 6.0221 / z * v0 * 1.0E-7`). **The polynomial breaks down for θ/T ≳ 7** (Q-3).

**Temperature-dependent K and V₀** (`EOS.GetPressure`, branch `ThermalPressure.T_dependence_BM`, `EOS.cs:160–176`)

$$K_T=K_0+\frac{\partial K}{\partial T}(T-T_0),\quad V_0(T)=V_0\exp\!\left[A\cdot10^{-5}(T-T_0)+\frac{B\cdot10^{-9}}{2}(T^2-T_0^2)-C\left(\frac1T-\frac1{T_0}\right)\right]$$

followed by $P=P_{\rm BM3/BM4/Vinet}(K_T,K_0',[K_0''],V_0(T)/V)$.

**Tabulated data + splines** (`EOS.InitializeSpline` `EOS.cs:1406–1419`, `EOS.GetPressureFromSplineMethods` `EOS.cs:1422–1429`, `Spline.GetValue` in `Mathematics/Spline.cs`)

1. For each temperature column, a natural cubic spline in the compression x (first column) is built (NaN entries skipped).
2. At the given x, P is evaluated for every tabulated T, and a second natural cubic spline in T is built and evaluated at T.
3. Outside the knots the end segment is **extrapolated linearly** (`Spline.GetSpline` fixes the cubic and quadratic coefficients of both end segments to 0).
4. For Au/Pt Jamieson, Brown, Tsuchiya, Zha, Au/Pt Yokoo and Ross Ar the spline coefficients are baked into the source as `private static Spline[] SplineXxx = [ new(...) ]`, so the lazy `InitializeSpline` is never executed. Consistency between the baked coefficients and the tables was confirmed: splines rebuilt from the tables in Python agree with the C# output to within 3×10⁻⁸ GPa (§4).

## 1. Ruby fluorescence scales

Implementation: `FormMain.calcRubyPressure` (`FormMain.cs:1002–1017`). $r=R1/R1_0$, $\Delta=(R1-R1_0)/R1_0$.

| UI label | Formula (as implemented) | Default A (editable) | Fixed in code | Source (code/UI) | Range (UI) |
|---|---|---|---|---|---|
| Mao (1978) | $P=A\,(r^{5}-1)/5$ (`FormMain.cs:1006`) | 1904 (`numericBoxMaoA.Value = 1904D`, Designer:1012) | exponent 5 | UI label "Mao (1978)" (Designer:1141) | not stated |
| Mao-quasi (1986) | $P=A\,(r^{7.665}-1)/7.665$ (`FormMain.cs:1009`) | 1904 (Designer:1031) | exponent 7.665 | UI label (Designer:1131); bundled `references/Syassen 2008HPR.pdf`, Table 6: "A0=1904, B=7.665, Cu, Ag; Ar; 80 GPa, Mao [15]" | not stated |
| Mao-hydro (2000) | $P=A\,(r^{7.715}-1)/7.715$ (`FormMain.cs:1012`) | 1904 (Designer:1069) | exponent 7.715 | UI label only (Designer:1121); no source for 7.715 in the bundled PDFs | not stated |
| Shen et al. (2020) | $P=A\,(\Delta+5.63\,\Delta^2)$ (`FormMain.cs:1016`) | 1870 (Designer:1050) | B = 5.63 | UI label (Designer:1111); matches bundled `references/Shen_et_al_HPR_2020_Ruby.pdf`: $P=A(\Delta\lambda/\lambda_0)[1+B(\Delta\lambda/\lambda_0)]$, A = 1870 GPa (fixed), B = 5.63(3) | not stated (the PDF: room temperature, up to 150 GPa) |

- The exponents (5, 7.665, 7.715) and Shen's B (5.63) are not editable in the UI; only A is.
- The group caption reads "Δ=R1-R1₀" (Designer:981), but the code uses the relative shift $\Delta=(R1-R1_0)/R1_0$ (`FormMain.cs:1014`). The formula matches the Shen et al. PDF; the caption is the inaccurate part (Q-9).

### 1.1 Temperature correction (Ragan et al., 1992)

Implementation: `FormMain.calcRaganParameter` (`FormMain.cs:917–922`), `FormMain.calcR1_0` (`FormMain.cs:969–979`).

1. A constant $c$ is obtained from the reference condition (R1_ref, T_ref):
   $c = 10^7/R1_{\rm ref} - 4.49\times10^{-2}T_{\rm ref} + 4.81\times10^{-4}T_{\rm ref}^2 - 3.71\times10^{-7}T_{\rm ref}^3$ (`FormMain.cs:921`; stored in `numericBoxRubyRagan`, default 14423, Designer:1191)
2. With "Calculate from Ragan's equation" checked, R1₀ at the measurement temperature T is
   $R1_0(T) = 10^7 / (c + 4.49\times10^{-2}T - 4.81\times10^{-4}T^2 + 3.71\times10^{-7}T^3)$ [nm] (`FormMain.cs:975`)

- The coefficients match Eq. (3) of the bundled `references/Ragan et al 1992JAP.pdf`, $R_1(T)=14423+4.49\times10^{-2}T-4.81\times10^{-4}T^2+3.71\times10^{-7}T^3$ cm⁻¹. The implementation replaces the constant 14423 by a value calibrated from the reference condition (i.e. it re-zeros the curve).
- Range: the UI label says "50-600K" (Designer:1173); the abstract of the bundled PDF says the fit covers "15 to 600 K" (R-3).
- °C input is converted with +273.15 (`FormMain.cs:920, 971`).

## 2. Diamond Raman edge scales

Implementation: `FormMain.textBoxNu_TextChanged` (`FormMain.cs:780–808`). $x=(\nu-\nu_0)/\nu_0$, ν and ν₀ in cm⁻¹.

| UI label | Formula (as implemented) | Coefficients (default / editable) | Source | Range (UI) |
|---|---|---|---|---|
| Akahama (2004): | $P=A+B\nu+C\times10^{-4}\nu^2$ (**absolute ν**, `FormMain.cs:789`) | A=66.9, B=-0.5281, C=3.585 (Designer:423, 433, 443; editable) | UI label (Designer:1524) | not stated |
| Akahama (2006): | $P=K_0x\left[1+\tfrac12(K_0'-1)x\right]$ (`FormMain.cs:792`) | K0=547, K0'=3.75 (Designer:1491, 1501; editable) | UI label (Designer:1554); matches Eq. (3) of bundled `references/Akahama2006.pdf` with ν₀ = 1334 cm⁻¹, K0 = 547(11) GPa, K0′ = 3.75(20) | not stated |
| Fratanduono+ (2021, <300GPa) | **code**: $P=530.77x+753.83x^2$ (`FormMain.cs:794`) / **UI label**: `503.77 × Δν/ν0 + 753.83 × (Δν/ν0)²` (Designer:1544) | fixed | UI label; `Version.cs` ver1.002 "Added two diamond Raman equations (Fratanduono et al., 2021)" | < 300 GPa |
| Fratanduono+ (2021, >200GPa) | $P=199.49-852.78x+3103.8x^2$ (`FormMain.cs:796`) | fixed | same (Designer:1534) | > 200 GPa |

- Default ν₀ is 1334 (`textBoxDiamondRamanNu0.Text = "1334"`, Designer:1453), overwritten by the registry value `textBoxAkahama2006Nu0.Text` if present (`FormMain.cs:118`). ν₀ is used by Akahama 2006 and the two Fratanduono formulae, not by Akahama 2004.
- **The linear coefficient of Fratanduono (<300 GPa) differs between code (530.77) and UI label / existing manual (503.77)** (Q-1). The original paper is not bundled, so which one is correct was not checked. The difference is ≈5.4 GPa at x = 0.2 (ν ≈ 1600 cm⁻¹).

## 3. Equation-of-state (EOS) scales

The "Formula" column uses the shorthand of §0.3. $x_J\equiv1-V/V_0$ (abscissa of the tabulated scales).

### 3.1 Gold — `FormMain.Gold` (`FormMain.cs:1058–1086`)

Default a₀ = 4.07825 Å (Designer:1870). All scales use the UI a₀.

| UI label | Formula | Parameters (verbatim) | Implementation | Source (code/UI) | Range |
|---|---|---|---|---|---|
| Jamieson (82) | table + splines, $x_J=1-(a/a_0)^3$ | table `AuJamieson` | `EOS.Au_Jamieson` `EOS.cs:758–765`, table `EOS.cs:689–738` | UI label, `#region AuJamiesonの定義` | grid: $x_J$ = -0.010 to 0.225, T = 200 to 1500 K (100 K step); linear extrapolation outside |
| Anderson (89) | BM3 + $(0.00714 + {\rm KTperT}\cdot\ln(V_0/V))(T-300)$ | `KT = 166.65`, `KTperT = -0.0115`, `KTperP = 5.4823`, constant `0.00714` | `EOS.Au_Anderson` `EOS.cs:426–436` | UI label, comment "Andersonの状態方程式" | not stated |
| Sim (02) | BM3 + MGD (power-law γ) | K0 `167`, K0' `5.0`; Z `4`, n `1`, θ0 `170`, γ0 `2.97`, q `1.0`, T0 `300` | `FormMain.cs:1068` | UI label | not stated |
| Tsuchiya (03) | table + splines, $x_J=1-(a/a_0)^3$ | table `Tsuchiya` | `EOS.Au_Tsuchiya` `EOS.cs:1036–1043`, table `EOS.cs:1005–1024` | UI label | $x_J$ = 0 to 0.34, T = 300, 500, 1000, 1500, 2000, 2500 K |
| Yokoo (09) | table + splines, $x_J=1-V/V_0$ | table `Table_Au_Yokoo` | `EOS.AuYokoo` `EOS.cs:1232–1238`, table `EOS.cs:1196–1218` | UI label, `#region YokooのAuのテーブル` | $x_J$ = 0 to 0.40, T = 0, 300, 500, 1000, 1500, 2000, 2500, 3000 K (low-compression rows are NaN in the 2500/3000 K columns) |
| Fratanduono (21) | Vinet, no thermal pressure | `k0 = 170.09`, `k_prime = 5.880` | `EOS.Au_Fratanduono_Vinet` `EOS.cs:404–410` | UI label, comment "Au (Fratanduono et al., 2021) Thermal pressureなし" | not stated; **ignores T** (always the room-temperature isotherm) |

- Jamieson (82) returns 0.05 GPa at $a=a_0$, T = 300 K (the x = 0.000, 300 K entry at `EOS.cs:693` is `00.05`). The temperature to which the table's V₀ refers is not stated in the code (R-5).

### 3.2 Platinum — `FormMain.Pt` (`FormMain.cs:1089–1118`)

Default a₀ = 3.9231 Å (Designer:2150).

| UI label | Formula | Parameters | Implementation | Source | Range |
|---|---|---|---|---|---|
| Jamieson (82) | table + splines, $x_J=1-(a/a_0)^3$ | table `PtJamieson` | `EOS.Pt_Jamieson` `EOS.cs:829–836`, table `EOS.cs:769–809` | UI label | $x_J$ = -0.010 to 0.180, T = 200 to 1500 K |
| Holmes (89) | $3B_T\frac{1-X}{X^2}\exp[1.5(B'_T-1)(1-X)] + {\rm AlfaT}\cdot B_T(T-300)/10000$, $X=a/a_0$ | `BT = 266`, `BprimeT = 5.81`, `AlfaT = 0.261` | `EOS.Pt_Holmez` `EOS.cs:501–509` | UI label, comment "Pt Holmesの状態方程式" | not stated |
| Matsui (09) | Vinet + MGD (power-law γ, n = 1) + $P_{el}(T)$ | `K0T = 273`, `Kprime0T = 5.20`, `gamma0 = 2.70`, `q = 1.10`, `theta0 = 230`, Z = 4 (`/ 4`); $P_{el}=$ `-1.01162E-11*T^3 + 1.89796E-07*T^2 + 6.73266E-05*T - 3.70065E-02` | `EOS.Pt_Matsui` `EOS.cs:517–544` | UI label, XML comment "Pt(Matsui2009)の状態方程式"; $P_{el}$ is a polynomial fit to the comment "PelT (the difference from the value at 300 K of 0.04 GPa) =0.21, 1.60, and 3.78 GPa at 1000, 3000, and 5000 K" | not stated; **the only Pt scale that uses the UI T₀** |
| Yokoo (09) | table + splines, $x_J=1-V/V_0$ | table `Table_Pt_Yokoo` | `EOS.PtYokoo` `EOS.cs:1313–1320`, table `EOS.cs:1277–1299` | UI label | $x_J$ = 0 to 0.40, T = 0 to 3000 K (8 columns) |
| Fratanduono (21) | Vinet, no thermal pressure | `k0 = 259.7`, `k_prime = 5.839` | `EOS.Pt_Fratanduono_Vinet` `EOS.cs:417–423` | UI label, comment "Pt (Fratanduono et al., 2021) Thermal pressureなし" | not stated; **ignores T** |

- In Matsui (09), $P_{el}$ depends on the absolute T only and is not referenced to T₀ ($P_{el}(300\,{\rm K})\approx -1.7\times10^{-8}$ GPa) (Q-7).

### 3.3 NaCl B1 — `FormMain.NaClB1` (`FormMain.cs:1196–1212`)

Default a₀ = 5.639 Å (Designer:2297).

| UI label | Formula | Parameters | Implementation | Source | Range |
|---|---|---|---|---|---|
| Brown (99) | table + splines, $x_J=1-(a/a_0)^3$ | table `Brown` | `EOS.NaCl_Brown` `EOS.cs:994–1001`, table `EOS.cs:913–978` | UI label, `#region double[][] Brownの定義` | $x_J$ = -0.1699 to 0.3197, T = 300 to 1200 K (100 K step); on the expansion side ($x_J<0$) only the high-temperature columns have values, the rest are NaN |
| Matsui (12) | BM4 (written directly in $f$) + MGD (power-law γ) | `Kt0 = 23.7`, `KpT0 = 5.14`, `KppT0 = -0.392`; MGD: Z `4`, n `2`, θ0 `279`, γ0 `1.56`, q `0.96`, T0 `300` | `EOS.NaClB1_Matsui` `EOS.cs:481–499` | UI label, comment "Matui et al (2012) Am Min" | not stated |

- `EOS.NaClB1_Sata` (`EOS.cs:444–449`) and `EOS.NaClB1_Skelton` (`EOS.cs:1183–1188`) exist but are not called from the UI.

### 3.4 NaCl B2 — `FormMain.NaClB2` (`FormMain.cs:1168–1192`)

**The a₀ box is disabled (`Enabled = false`, Designer:2526); no scale uses the UI a₀.** a is passed in Å.

| UI label | Formula | Parameters | V₀ | Implementation | Source | T |
|---|---|---|---|---|---|---|
| Sata (02) (Pt) | $P=P_r(V/V_r)^{-2/3}\exp[-(3K_r/P_r-2)((V/V_r)^{1/3}-1)]$ | `Pr = 31.14`, `Kr = 143.5` (`FormMain.cs:1174`), $V_r$ = `27.17` | fixed $V_r$ = 27.17 Å³ | `EOS.NaClB2_Sata` `EOS.cs:438–442` | UI label, comment "SataさんのNaClB2の状態方程式" | ignored |
| Sata (02) (Mg) | same | `Pr = 32.15`, `Kr = 141.0` (`FormMain.cs:1176`) | same | same | UI label "(Mg)", FormMain comment "Sata MgO" | ignored |
| Ueda+(08) | Vinet + `0.00468 * (T - 300)` | `Kt0 = 28.45`, `KpT0 = 5.16` | fixed `V0 = 24.76 / 6.0221367 * 10` (= 41.115 Å³) | `EOS.NaClB2_Ueda` `EOS.cs:465–477` | UI label, comment "Ueda et al (2008) J app phis" | linear thermal pressure |
| Sakai+(11) BM | BM3 | `47.00`, `4.10` | fixed `37.73` Å³ | `EOS.NaClB2_SakaiBM` `EOS.cs:452–456` | UI label, comment "Sakaiさんの状態方程式" | ignored |
| Sakai+(11) Vinet | Vinet | `40.40`, `5.04` | fixed `37.73` Å³ | `EOS.NaClB2_SakaiVinet` `EOS.cs:458–462` | same | ignored |

- Sata's expression is a two-parameter form whose bulk modulus at the reference point $(V_r,P_r)$ equals $K_r$ (verified: $-V\,dP/dV|_{V_r}=K_r$). Not compared with the original paper (R-6).
- Sakai's V₀ = 37.73 Å³ and Ueda's V₀ = 41.115 Å³ differ by 9 % (R-7).

### 3.5 Periclase (MgO) — `FormMain.MgO` (`FormMain.cs:1140–1165`)

Default a₀ = 4.2112 Å (Designer:2790). All scales use the UI a₀.

| UI label | Formula | Isothermal part | Thermal part | Implementation | Source |
|---|---|---|---|---|---|
| Jackson (98) | BM3 + MGD (power-law γ) | K0 `162.5`, K0' `4.13` | Z `4`, n `2`, θ0 `673`, γ0 `1.41`, q `1.3`, T0 `300` | `FormMain.cs:1146` | UI label (named `Jacson` in FormMain) |
| Dewaele (00) | BM3 + MGD | K0 `161`, K0' `3.94` | Z `4`, n `2`, θ0 `800`, γ0 `1.45`, q `0.8`, T0 `300` | `FormMain.cs:1148` | UI label |
| Aizawa (06) | BM3 + MGD | K0 `160`, K0' `4.15` | Z `4`, n `2`, θ0 `773`, γ0 `1.41`, q `0.7`, T0 `300` | `FormMain.cs:1150` | UI label |
| Tange (09) Vinet | Vinet + Tange type | `k0 = 160.63`, `k_prime = 4.367` | `theta0 = 761`, `gamma0 = 1.442`, `a = 0.138`, `b = 5.4`, `n = 2`, `z = 4`, T0 300 (fixed) | `EOS.MgO_Tange_Vinet` `EOS.cs:348–372` | UI label |
| Tange (09) BM | BM3 + Tange type | `k0 = 160.64`, `k_prime = 4.221` | `theta0 = 761`, `gamma0 = 1.431`, `a = 0.29`, `b = 3.5`, `n = 2`, `z = 4`, `t0 = 300.0` | `EOS.MgO_Tange_BM` `EOS.cs:374–397` | UI label |

- No range is stated in the code. Because of the polynomial approximation of the Debye energy, **the two Tange (09) scales give wrong thermal pressure below ≈110 K (θ/T > 7 at V/V₀ = 1)** (Q-3).
- `EOS.MgO_Jamieson` (`EOS.cs:902–909`, table `EOS.cs:840–897`) exists but is not called from the UI.

### 3.6 Corundum (Al₂O₃) — `FormMain.Corundum` (`FormMain.cs:1215–1229`)

Default V₀ = 255.89472 Å³ (Designer:2889).

| UI label | Formula | Parameters | Implementation | Source |
|---|---|---|---|---|
| Dubrovinsky (98) | BM3 with $K_T$ and $V_0(T)$ | `T0 = 300`, `KTperT = -0.020`, `KTperP = 4.88`, `KT = 258 + KTperT * (T - T0)`, `a = 2.6E-5`, `b = 1.81E-9`, `c = -0.67`; $V_0(T)=V_0\exp[a(T-T_0)+\frac b2(T^2-T_0^2)-c(\frac1T-\frac1{T_0})]$ | `EOS.Corundum_Dubrovinsky` `EOS.cs:547–559` | UI label, comment "Dubrovinskyの状態方程式" |

### 3.7 Rhenium (Re) — `FormMain.Re` (`FormMain.cs:1232–1254`)

Default V₀ = 29.42795 Å³ (Designer:3233).

| UI label | Formula | Parameters | V₀ | Implementation | Source | T |
|---|---|---|---|---|---|---|
| Zha et al. (04) | table + splines, $x_J=1-V/V_0$ | table `Zha` | UI V₀ | `EOS.Re_Zha` `EOS.cs:1083–1090`, table `EOS.cs:1047–1069` | UI label, `#region double[][] Zha et al. レニウムの定義` | grid: $x_J$ = 0 to 0.20, T = 300 to 3000 K (7 columns) |
| Anz (##) | Vinet | `352.6`, `4.56` | **fixed `29.467`** | `FormMain.cs:1240` | UI label "Anz (##)" only (no year) | ignored |
| Sakai. (##) | Vinet | `358`, `4.8` | **fixed `29.47`** | `FormMain.cs:1242` | UI label "Sakai. (##)" only | ignored |
| Dub. (##) | BM4 | `342`, `6.15`, `-0.029` | **fixed `29.46`** | `FormMain.cs:1244` | UI label "Dub. (##)" only | ignored |

- Because the default UI V₀ (29.42795) differs from the fixed V₀, these return 0.469 (Anz), 0.513 (Sakai) and 0.374 GPa (Dub) at $V=V_0^{\rm UI}$ (§4).

### 3.8 Argon (Ar) — `FormMain.Ar` (`FormMain.cs:1121–1137`)

**The a₀ box (`numericBoxArA0`) is not used by either scale** (and has no `Value` set in the Designer). a is passed in nm.

| UI label | Formula | Parameters | Implementation | Source | T |
|---|---|---|---|---|---|
| Ross et al. (86) | natural spline of P against molar volume $V_m=N_A a^3/4$ [cm³/mol] | table `Ross` ($V_m$ = 19 to 4.5 cm³/mol, P = 1.6 to 689.2 GPa, 16 points, header temperature `273`) | `EOS.Ar_Ross` `EOS.cs:1367–1371`, table `EOS.cs:1343–1360` | UI label, comment "Ar Ross et al. 1986 / cm^3/mol, GPa" | ignored (single isotherm) |
| Jephcoat (98) | BM3 + $\gamma/V\,[E(T)-E(T_0)]$ (constant θ, n = 1) | `K0T = 3.03`, `Kprime0T = 7.24`, `gamma0 = 0.5`, `gamma1 = 2.20` ($\gamma=\gamma_0+\gamma_1V/V_0$), `theta0 = 93.3`, `Z = 4`, `V0 = 22.557 / 100` (= 22.557 cm³/mol), **`T0 = 4`**, R `8.31451` | `EOS.Ar_Jephcoat` `EOS.cs:1373–1396` | UI label | reference 4 K |

- Outside the Ross table ($V_m$ > 19 cm³/mol, i.e. a ≳ 5.02 Å, P < 1.6 GPa) the spline is extrapolated linearly.

### 3.9 Molybdenum (Mo) — `FormMain.Mo` (`FormMain.cs:1257–1289`). **Hidden in the UI** (`groupBoxMo.Visible = false`, Designer:3251)

Default V₀ = 31.14 Å³ (Designer:3370). The values are still computed on every input (`FormMain.cs:1053`).

| UI label | Formula | Parameters | Implementation |
|---|---|---|---|
| Huang+(16)MGD | BM3 + MGD (power-law γ) | K0 `255`, K0' `4.25`; Z `2`, n `1`, θ0 `470`, γ0 `2.01`, q `0.6`, T0 `300` | `FormMain.cs:1264` |
| Zhao+(00) | `EOS.GetPressure` (`T_dependence_BM` + `BM4`) | `Z = 2`, `K0 = 268`, `Kp0 = 3.81`, `Kpp0 = -1.41E-2`, `KperT = -2.13E-2`, `A = 1.31`, `B = 11.2`, `T0 = 300` (C not set = 0) | `FormMain.cs:1267–1282`, `EOS.cs:160–176` |

### 3.10 Lead (Pb) — `FormMain.Pb` (`FormMain.cs:1383–1411`). **Hidden in the UI** (`groupBoxPb.Visible = false`, Designer:3386)

Default a₀ = 4.95059 Å (Designer:3470); uses the UI T₀.

| UI label | Formula | Parameters | Implementation |
|---|---|---|---|
| Strässle+(14) | $a_0(T)=a_0^{\rm UI}\cdot a_0^{\rm tab}(T)/a_0^{\rm tab}(T_0)$, $X=a/a_0(T)$, $P=3B(T)\frac{1-X}{X^2}\exp[1.5(B'(T)-1)(1-X)]$ | table `PbVinet` (T = 0 to 300 K, 20 K step; B `48.3298` to `41.2000`, B′ `5.4511` to `5.7245`), table `PbA0` (T = 0 to 310 K, 5 K step; `4.91366` to `4.95110` Å); both via `MathNet.Numerics.Interpolate.Linear` (linear extrapolation outside) | tables `FormMain.cs:1292–1310`, `1313–1379`; formula `FormMain.cs:1396–1404` |

- The only source information is the label "Strässle+(14)" (Designer:3446) and the return tag `"Strassle14"`.

## 4. Values at representative inputs (C# run vs independent Python implementation)

### 4.1 Method

- **C#**: .NET 8 SDK on Linux; `Crystallography/EOS.cs`, `Mathematics/Spline.cs` and the `MathnetEx` part of `ExtensionMethods.cs` compiled unchanged into a console app with minimal stubs for `PointD`/`Profile`/`UniversalConstants`. Expressions that live in FormMain (Sim, Jackson, … , Pb, ruby, diamond) were pasted verbatim from FormMain.cs. The harness is in `docs/drafts/tools/cs_harness/` (outside the solution, `.txt` extension).
- **Python**: `docs/drafts/tools/py_eos.py`. Formulae were written from §0–3 of this document; tables are parsed from EOS.cs/FormMain.cs and interpolated with scipy natural cubic splines (linear extrapolation at the ends); the Debye integral uses `scipy.integrate.quad`; only the Tange type uses the same polynomial as the code.
- Inputs: UI default a₀/V₀ of each material, T₀ = 300 K. All 567 points are in `docs/drafts/tools/compare.csv`.

### 4.2 Summary

- |C# − Python| ≤ 4×10⁻⁴ GPa for every scale. Residuals of order 10⁻⁴ GPa only appear for scales with an MGD thermal term; they are explained by the summation index of the C# `integ` (`i = 1 … num`, i.e. the midpoints are shifted by one step; Q-10). Tabulated scales agree to ≤ 3×10⁻⁸ GPa, so the baked spline coefficients are consistent with the tables.
- The tables below give the C# values (GPa). The last column is the maximum difference over all test points (T = 300/1000/2000 K, V/V₀ = 1.00 to 0.80).

**T = 300 K, dependence on V/V₀ (P / GPa)**

| Scale | V/V₀=0.95 | 0.90 | 0.85 | 0.80 | max\|C#−Py\| |
|---|---:|---:|---:|---:|---:|
| Au Jamieson82 | 9.970 | 23.960 | 42.710 | 67.590 | 2.5e-08 |
| Au Anderson89 | 9.838 | 23.435 | 42.276 | 68.529 | 4.3e-13 |
| Au Sim02 | 9.739 | 22.913 | 40.809 | 65.291 | 3.7e-04 |
| Au Tsuchiya03 | 9.971 | 24.120 | 44.066 | 72.110 | 7.5e-11 |
| Au Yokoo09 | 9.976 | 23.960 | 43.532 | 71.050 | 1.0e-10 |
| Au Fratanduono21 | 10.134 | 24.320 | 44.113 | 71.730 | 1.7e-13 |
| Pt Jamieson82 | 15.900 | 37.650 | 67.540 | 107.536 | 1.8e-08 |
| Pt Holmes89 | 15.820 | 37.896 | 68.606 | 111.335 | 5.8e-13 |
| Pt Matsui09 | 15.986 | 37.685 | 67.095 | 107.011 | 3.8e-04 |
| Pt Yokoo09 | 16.206 | 38.280 | 68.423 | 109.850 | 1.2e-10 |
| Pt Fratanduono21 | 15.457 | 37.054 | 67.135 | 109.038 | 2.8e-13 |
| NaCl B1 Brown99 | 1.399 | 3.305 | 5.895 | 9.413 | 8.5e-09 |
| NaCl B1 Matsui12 | 1.385 | 3.257 | 5.778 | 9.169 | 1.5e-04 |
| MgO Jackson98 | 9.267 | 21.291 | 36.979 | 57.597 | 3.0e-04 |
| MgO Dewaele00 | 9.136 | 20.878 | 36.046 | 55.781 | 3.0e-04 |
| MgO Aizawa06 | 9.130 | 20.987 | 36.471 | 56.845 | 3.0e-04 |
| MgO Tange09Vinet | 9.209 | 21.237 | 36.960 | 57.570 | 2.1e-04 |
| MgO Tange09BM | 9.183 | 21.152 | 36.838 | 57.551 | 2.1e-04 |
| Al₂O₃ Dubrovinsky98 | 15.001 | 35.178 | 62.448 | 99.569 | 0 |
| Re Zha04 | 20.730 | 48.110 | 84.490 | 133.190 | 2.5e-10 |
| Re Anz | 20.902 | 47.820 | 83.293 | 130.160 | 7.1e-15 |
| Re Sakai | 21.402 | 49.225 | 86.277 | 135.720 | 7.1e-15 |
| Re Dub | 21.041 | 50.463 | 92.369 | 152.319 | 2.8e-13 |
| Mo Huang16 (hidden) | 14.588 | 33.629 | 58.619 | 91.667 | 2.8e-04 |
| Mo Zhao00 (hidden) | 15.157 | 34.503 | 59.317 | 91.350 | 1.4e-13 |
| Pb Strassle14 (hidden) | 2.445 | 5.844 | 10.555 | 17.087 | 5.0e-14 |

(For Re Anz/Sakai/Dub, V/V₀ is relative to the UI V₀ = 29.42795 Å³.)

**Temperature dependence (P / GPa; V₀ = UI default, taken as the 300 K value)**

| Scale | V/V₀=1.00, 1000 K | 0.90, 1000 K | 1.00, 2000 K | 0.90, 2000 K |
|---|---:|---:|---:|---:|
| Au Jamieson82 | 5.450 | 29.340 | 13.153 | 37.122 |
| Au Anderson89 | 4.998 | 27.585 | 12.138 | 33.514 |
| Au Sim02 | 5.054 | 27.947 | 12.303 | 35.192 |
| Au Tsuchiya03 | 5.350 | 28.850 | 13.040 | 35.660 |
| Au Yokoo09 | 4.990 | 28.590 | 12.180 | 35.290 |
| Au Fratanduono21 | 0.000 | 24.320 | 0.000 | 24.320 |
| Pt Jamieson82 | 4.620 | 42.210 | 11.211 | 48.728 |
| Pt Holmes89 | 4.860 | 42.756 | 11.802 | 49.699 |
| Pt Matsui09 | 5.351 | 42.950 | 13.316 | 50.831 |
| Pt Yokoo09 | 5.370 | 43.280 | 13.150 | 50.560 |
| Pt Fratanduono21 | 0.000 | 37.054 | 0.000 | 37.054 |
| NaCl B1 Brown99 | 2.026 | 5.283 | 4.907 | 8.097 |
| NaCl B1 Matsui12 | 1.992 | 5.248 | 4.869 | 8.136 |
| MgO Jackson98 | 4.074 | 25.153 | 10.260 | 31.125 |
| MgO Dewaele00 | 4.073 | 24.908 | 10.405 | 31.340 |
| MgO Aizawa06 | 3.986 | 24.982 | 10.150 | 31.313 |
| MgO Tange09Vinet | 4.087 | 25.384 | 10.394 | 31.940 |
| MgO Tange09BM | 4.056 | 25.140 | 10.315 | 31.442 |
| Al₂O₃ Dubrovinsky98 | 4.446 | 40.481 | 11.485 | 49.057 |
| Re Zha04 | 4.810 | 52.960 | 12.420 | 59.930 |
| Re Anz / Sakai / Dub | (T-independent; same as 300 K) | | | |
| Mo Huang16 | 3.611 | 37.328 | 8.928 | 42.860 |
| Mo Zhao00 | 3.710 | 38.032 | 11.137 | 46.151 |

**Pb (T₀ = 300 K, a₀ = 4.95059 Å)**: at V/V₀ = 1.00: −0.994 (10 K), −0.730 (100 K), 0.335 GPa (400 K, outside the tables, extrapolated). At V/V₀ = 0.90: 5.050 (10 K), 5.268 GPa (100 K).

**NaCl B2 (input a / Å, T = 300 K; last column 2000 K)**

| Scale | a=3.00 | 2.93 | 2.85 | 2.75 | 2.93 @2000 K |
|---|---:|---:|---:|---:|---:|
| Sata02 (Pt) | 32.053 | 44.254 | 64.070 | 101.977 | 44.254 |
| Sata02 (MgO) | 33.047 | 44.922 | 63.892 | 99.460 | 44.922 |
| Ueda08 | 33.421 | 45.930 | 64.705 | 97.114 | 53.886 |
| Sakai11 BM | 31.352 | 44.006 | 63.029 | 96.142 | 44.006 |
| Sakai11 Vinet | 30.310 | 43.191 | 62.701 | 96.689 | 43.191 |

**Ar (input a / Å)**

| Scale | a=4.80, 300 K | 4.40, 300 K | 4.0786, 300 K | 3.60, 300 K | 4.0786, 1000 K |
|---|---:|---:|---:|---:|---:|
| Jephcoat98 | 3.462 | 12.146 | 32.839 | 145.385 | 35.393 |
| Ross86 | 3.107 | 11.404 | 31.674 | 143.380 | 31.674 |

**Ruby (R1₀ = 694.25 nm, A = UI default)**

| Scale | R1=697 nm | 700 | 705 | 710 | 720 |
|---|---:|---:|---:|---:|---:|
| Mao (1978) | 7.602 | 16.033 | 30.409 | 45.200 | 76.057 |
| Mao-quasi (1986) | 7.642 | 16.212 | 31.049 | 46.604 | 79.987 |
| Mao-hydro (2000) | 7.643 | 16.215 | 31.061 | 46.631 | 80.064 |
| Shen et al. (2020) | 7.572 | 16.210 | 31.480 | 47.842 | 83.843 |

**Ragan correction: R1₀(T) after calibrating with reference R1 = 694.25 nm at 297.15 K**: 693.264 (50 K), 693.314 (100 K), 693.667 (200 K), 695.015 (400 K), 696.508 nm (600 K); calibrated constant c = 14423.428.

**Diamond (ν₀ = 1334 cm⁻¹, UI default coefficients)**

| Scale | ν=1334 | 1400 | 1500 | 1600 | 1800 | 2000 |
|---|---:|---:|---:|---:|---:|---:|
| Akahama (2004) | 0.385 | 30.220 | 81.375 | 139.700 | 277.860 | 444.700 |
| Akahama (2006) | 0.000 | 28.904 | 79.714 | 138.977 | 282.861 | 460.558 |
| Fratanduono (<300 GPa), code coefficient 530.77 | 0.000 | 28.105 | 77.721 | 135.808 | 277.400 | 452.880 |
| (reference) same with the UI-label coefficient 503.77 | 0.000 | 26.769 | 74.361 | 130.425 | 267.968 | 439.400 |
| Fratanduono (>200 GPa) | 199.490 | 164.896 | 141.434 | 152.854 | 280.343 | 547.364 |

- Fratanduono (>200 GPa) is a quadratic with 199.49 GPa at x = 0 and a minimum of 140.9 GPa at x ≈ 0.137 (ν ≈ 1517 cm⁻¹); it is meaningless below 200 GPa (use it only above 200 GPa, as the UI label says).

## Items to confirm

- R-1 No source for the exponent 7.715 or the year "2000" of Mao-hydro is given in the code or the bundled PDFs.
- R-2 Is A = 1904 intended for all three Mao formulae?
- R-3 Ragan range: UI says "50-600K", the bundled PDF abstract says "15 to 600 K". Why 50 K?
- R-4 Which linear coefficient of Fratanduono (<300 GPa) is correct, 530.77 (code) or 503.77 (UI, manual)? (Q-1) The paper is not bundled.
- R-5 Jamieson (82) Au/Pt tables give 0.05 GPa at x = 0, 300 K. Should the reference temperature of the table V₀ (e.g. 293 K) be documented in the code?
- R-6 Is the NaCl B2 Sata (02) expression (two-parameter form referenced to $V_r$ = 27.17 Å³) as in the paper?
- R-7 NaCl B2 reference volumes: Sakai 37.73 Å³ vs Ueda 41.115 Å³ (24.76 cm³/mol).
- R-8 Sources of Re Anz/Sakai/Dub (labels still "(##)") and the intent of using fixed V₀ instead of the UI V₀.
- R-9 Meaning of the coefficients in the Au Anderson (89) thermal term $(0.00714-0.0115\ln(V_0/V))(T-300)$ (the variable `KTperT` is used as the coefficient of the log term).
- R-10 Pt Holmes (89): does `AlfaT = 0.261` mean α×10⁴ /K? (formula `AlfaT * BT * (T - 300) / 10000`)
- R-11 Ar Jephcoat (98) reference temperature 4 K, and the header temperature 273 of the Ross (86) table.
- R-12 Are the Mo and Pb groups intentionally hidden (`Visible = false`)? The existing manual lists them as available.
- R-13 Validity ranges in P and T of each scale (not stated in the code).
- R-14 Whether to give bibliographic details in the manual. They are not in the code and would need to be checked by the author.
