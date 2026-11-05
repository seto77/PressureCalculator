本稿はコードから Claude が起こした下書き（2026-11-05）。著者未査読。

# 圧力スケール一覧（実装準拠）

- 対象コード: PressureCalculator `master` @ `1676a57`、Crystallography サブモジュール @ `bd3caf4`（`.gitmodules` が指す commit。PressureCalculator 側の gitlink と一致）。
- 行番号の表記: `FormMain.cs:1068` は `PressureCalculator/FormMain.cs` の 1068 行目、`EOS.cs:350` は `Crystallography/EOS.cs` の 350 行目、`Designer:1012` は `PressureCalculator/FormMain.designer.cs` の 1012 行目。
- パラメータの数値はコードの表記をそのまま写した（`-1.41E-2` などの指数表記もそのまま）。
- 「出典」欄はコード中のコメント・UI ラベル・既存マニュアル・リポジトリ同梱 PDF（`references/`）に書かれているものだけ。書誌情報（誌名・巻・頁）はコードに無いので書いていない。
- 「適用範囲」欄は、UI ラベルにある記述と、数表の格子範囲（=これを外れると外挿）だけを書いた。原論文の適用範囲は確認していない。

## 0. 共通の約束事

### 0.1 単位と入力

| 量 | 単位 | 実装箇所 |
|---|---|---|
| 格子定数 a, a₀ | Å（UI 入力） | `Designer`（例 `numericalTextBoxGoldA.FooterText = "Å"` Designer:1829） |
| 体積 V, V₀ | Å³（UI 入力。Corundum, Re, Mo） | Designer:2837, 3181, 3318 |
| 圧力 | GPa | 各出力欄 `FooterText = "GPa"` |
| 温度（EOS） | K のみ（`numericBoxTemperature`, `numericBoxTemperature0`。℃ 切替は無い） | Designer:3499, 3524 |
| 温度（ルビー） | K または ℃（`radioButtonTempUnitK/C`）。℃ の場合は内部で +273.15 | `FormMain.cs:920, 971` |
| ルビー波長 | nm | Designer:1258, 1320, 1339 |
| ダイヤモンド波数 | cm⁻¹ | Designer:1463, 1482 |

- 立方晶の物質（Au, Pt, NaCl B1/B2, MgO, Ar, Pb）は a を入力し、FormMain 側で $V=a^3$、$V_0=a_0^3$ を作る（例 `FormMain.cs:1060–1061`）。
- 一部の関数には `a / 10`（nm）を渡している（Au Jamieson/Anderson/Tsuchiya、Pt Jamieson/Holmes/Matsui、NaCl Brown、Ar）。比しか使わない関数では単位は結果に影響しない。Ar の 2 関数はモル体積に換算するので nm 前提である（§3.8）。

### 0.2 温度と基準状態

- UI の **Temperature**（`numericBoxTemperature`、既定 300 K, Designer:3515）は全 EOS スケールに渡される。
- UI の **T₀**（`numericBoxTemperature0`、既定 300 K, Designer:3540）を使うのは **Pt Matsui (09)**（`FormMain.cs:1103`）と **Pb Strässle+(14)**（`FormMain.cs:1394` `var t0 = numericBoxTemperature0.Value`）の 2 つだけ。
- それ以外の熱的スケールの基準温度はコード内で固定されている: 300 K（`MieGruneisen(..., 300, ...)` の第 6 引数、Tange の `300.0`/`t0 = 300.0`、Anderson/Holmes/Ueda の `(T - 300)`、Dubrovinsky の `T0 = 300`、Mo Zhao の `T0 = 300`）、Ar Jephcoat は `T0 = 4`（`EOS.cs:1386`）。
- UI の a₀/V₀ を使わず、コード内の固定値を基準体積とするスケールがある（NaCl B2 の全 5 スケール、Ar の 2 スケール、Re の Anz/Sakai/Dub）。下表の「V₀ の扱い」欄を参照。
- 熱膨張を V₀(T) として明示的に扱うのは Dubrovinsky98（Al₂O₃）、Zhao+00（Mo）、Strässle+14（Pb）だけ。他の熱的スケールは $P(V,T)=P_{300}(V)+\Delta P_{th}(V,T)$ 型で、V₀ は 300 K の値とみなしている。

### 0.3 共通の式（実装どおり）

以下 $x \equiv V_0/V$、$y \equiv (V/V_0)^{1/3}$。

**3 次 Birch–Murnaghan**（`EOS.BirchMurnaghan3rd`, `EOS.cs:270–271`。4 引数版 `EOS.cs:261–262` は $V_0/V$ を作って委譲）

$$P_{\rm BM3}=\tfrac32K_0\left(x^{7/3}-x^{5/3}\right)\left[1-\tfrac34(4-K_0')\left(x^{2/3}-1\right)\right]$$

**4 次 Birch–Murnaghan**（`EOS.BirchMurnaghan4th`, `EOS.cs:273–276`）

$$P_{\rm BM4}=\tfrac32K_0\,x^{5/3}\left(x^{2/3}-1\right)\left[1+\tfrac34(K_0'-4)\left(x^{2/3}-1\right)+\frac{9K_0K_0''+9K_0'^2-63K_0'+143}{24}\left(x^{2/3}-1\right)^2\right]$$

（Euler 歪み $f=(x^{2/3}-1)/2$ で書いた標準形 $3K_0f(1+2f)^{5/2}[1+\tfrac32(K'-4)f+\tfrac32(K_0K''+(K'-4)(K'-3)+\tfrac{35}{9})f^2]$ と代数的に一致することを確認した。）

**Vinet**（`EOS.Vinet`, `EOS.cs:314–318`）

$$P_{\rm V}=3K_0\,\frac{1-y}{y^2}\exp\!\left[\tfrac32(K_0'-1)(1-y)\right]$$

**Mie–Grüneisen–Debye 熱圧力（べき乗型 γ）**（`EOS.MieGruneisen` 9 引数版, `EOS.cs:191–205`）

$$\gamma=\gamma_0\left(\frac{V}{V_0}\right)^{q},\quad \theta=\theta_0\exp\!\left(\frac{\gamma_0-\gamma}{q}\right),\quad E(T)=9nRT\left(\frac{T}{\theta}\right)^3\!\int_0^{\theta/T}\!\frac{z^3}{e^z-1}dz$$

$$\Delta P_{th}=\frac{\gamma}{V_m}\,[E(T)-E(T_0)],\qquad V_m=\frac{V N_A}{Z}$$

- 引数順は `(z, n, theta0, gamma0, q, t0, v0, t, v)`。v, v0 は **Å³/単位胞**（コード中の XML コメントは「nm^3」「v: Target temperature」「t: Target volume」と書かれているが、実際の単位換算 `v * 6.0221367 / z` と `/ 100` の組み合わせは Å³ で整合する。§疑問点 Q-11 参照）。
- R は `UniversalConstants.R = 8.3144621`（`UniversalConstants.cs:114`）。
- 積分 `integ(x)` は中点則 100000 分割（`EOS.cs:333–346`）。
- 零点エネルギーは含まない（$E(T)-E(T_0)$ の差なので、同じ θ を使う限り打ち消す形）。

**Tange 型（Al'tshuler 型 γ）**（`EOS.MgO_Tange_Vinet` `EOS.cs:348–372`, `EOS.MgO_Tange_BM` `EOS.cs:374–397`）

$$\gamma=\gamma_0\left[1+a\left(\left(\tfrac{V}{V_0}\right)^b-1\right)\right],\quad \theta=\theta_0\left(\tfrac{V}{V_0}\right)^{-(1-a)\gamma_0}\exp\!\left(-\frac{\gamma-\gamma_0}{b}\right)$$

$$\Delta P_{th}=\frac{\gamma}{V_m}\cdot 9nR\,[T\,e(\theta/T)-300\,e(\theta/300)]\times10^{-9}$$

ここで $e(u)$ は $u^{-3}\int_0^u z^3/(e^z-1)dz$ の 6 次多項式近似（`EOS.cs:363–366`, `388–391`。係数 `-0.0000013397592, 0.000035022653, -0.00031034588, 0.00018490174, 0.016520035, -0.12495878, 0.33333333`）。R は `8.3145`、$N_A$ は `6.0221`（`v0mol = 6.0221 / z * v0 * 1.0E-7`）。**この多項式は θ/T ≳ 7 で破綻する**（§疑問点 Q-3）。

**温度依存 K・V₀ 型**（`EOS.GetPressure` の `ThermalPressure.T_dependence_BM` 分岐, `EOS.cs:160–176`）

$$K_T=K_0+\frac{\partial K}{\partial T}(T-T_0),\quad V_0(T)=V_0\exp\!\left[A\cdot10^{-5}(T-T_0)+\frac{B\cdot10^{-9}}{2}(T^2-T_0^2)-C\left(\frac1T-\frac1{T_0}\right)\right]$$

の後、$P=P_{\rm BM3/BM4/Vinet}(K_T,K_0',[K_0''],V_0(T)/V)$。

**数表＋スプライン法**（`EOS.InitializeSpline` `EOS.cs:1406–1419`, `EOS.GetPressureFromSplineMethods` `EOS.cs:1422–1429`, `Spline.GetValue` `Mathematics/Spline.cs`）

1. 表の各温度列について、圧縮率 x（第 1 列）に対する自然 3 次スプラインを作る（NaN の点は除外）。
2. 与えられた x で各温度の P を求め、その (T, P) 列に対して再び自然 3 次スプラインを作り、T で評価する。
3. 節点の外側は端の区間の **1 次式で外挿** する（`Spline.GetSpline` の端点条件で両端区間の 3 次・2 次係数を 0 に固定）。
4. Au/Pt Jamieson, Brown, Tsuchiya, Zha, Au/Pt Yokoo, Ross Ar はスプライン係数が `private static Spline[] SplineXxx = [ new(...) ]` としてソースに焼き込まれており、遅延初期化 `InitializeSpline` は実行されない（`SplineXxx == null` にならないため）。焼き込み係数と表の整合は、Python で表から作り直したスプラインと C# 出力が 3×10⁻⁸ GPa 以内で一致することで確認した（§4）。

## 1. ルビー蛍光スケール

実装: `FormMain.calcRubyPressure`（`FormMain.cs:1002–1017`）。$r=R1/R1_0$、$\Delta=(R1-R1_0)/R1_0$。

| UI ラベル | 式（実装どおり） | A の既定値（UI で編集可） | 固定値（コード内） | 出典（コード/UI に書かれたもの） | 適用範囲（UI 記載） |
|---|---|---|---|---|---|
| Mao (1978) | $P=A\,(r^{5}-1)/5$ （`FormMain.cs:1006`） | 1904（`numericBoxMaoA.Value = 1904D`, Designer:1012） | 指数 5 | UI ラベル「Mao (1978)」（Designer:1141） | 記載なし |
| Mao-quasi (1986) | $P=A\,(r^{7.665}-1)/7.665$ （`FormMain.cs:1009`） | 1904（Designer:1031） | 指数 7.665 | UI ラベル（Designer:1131）。同梱 `references/Syassen 2008HPR.pdf` 表 6 に「A0=1904, B=7.665, Cu, Ag; Ar; 80 GPa, Mao [15]」 | 記載なし |
| Mao-hydro (2000) | $P=A\,(r^{7.715}-1)/7.715$ （`FormMain.cs:1012`） | 1904（Designer:1069） | 指数 7.715 | UI ラベルのみ（Designer:1121）。同梱 PDF には 7.715 の出典なし | 記載なし |
| Shen et al. (2020) | $P=A\,(\Delta+5.63\,\Delta^2)$ （`FormMain.cs:1016`） | 1870（Designer:1050） | B = 5.63 | UI ラベル（Designer:1111）。同梱 `references/Shen_et_al_HPR_2020_Ruby.pdf`: $P=A(\Delta\lambda/\lambda_0)[1+B(\Delta\lambda/\lambda_0)]$, A=1870 GPa (fixed), B=5.63(3) と一致 | 記載なし（同 PDF では室温・150 GPa まで） |

- 指数（5, 7.665, 7.715）と Shen の B（5.63）は UI では編集できない。編集できるのは A だけ。
- UI のグループ見出しは「Δ=R1-R1₀」（Designer:981）だが、計算は相対値 $\Delta=(R1-R1_0)/R1_0$（`FormMain.cs:1014`）。式は Shen et al. PDF と一致しており、ラベルの表記のほうが不正確（Q-9）。

### 1.1 温度補正（Ragan et al., 1992）

実装: `FormMain.calcRaganParameter`（`FormMain.cs:917–922`）、`FormMain.calcR1_0`（`FormMain.cs:969–979`）。

1. 参照条件（R1_ref, T_ref）から定数 $c$ を求める:
   $c = 10^7/R1_{\rm ref} - 4.49\times10^{-2}T_{\rm ref} + 4.81\times10^{-4}T_{\rm ref}^2 - 3.71\times10^{-7}T_{\rm ref}^3$ （`FormMain.cs:921`、結果は `numericBoxRubyRagan`、既定 14423, Designer:1191）
2. 「Ragan の式から計算」が ON のとき、測定温度 T における R1₀:
   $R1_0(T) = 10^7 / (c + 4.49\times10^{-2}T - 4.81\times10^{-4}T^2 + 3.71\times10^{-7}T^3)$ [nm] （`FormMain.cs:975`）

- 係数は同梱 `references/Ragan et al 1992JAP.pdf` の式 (3) $R_1(T)=14423+4.49\times10^{-2}T-4.81\times10^{-4}T^2+3.71\times10^{-7}T^3$ cm⁻¹ と一致。定数項 14423 を参照条件で置き換えて（＝ゼロ点を較正して）使う実装になっている。
- 適用範囲: UI ラベルは「50-600K」（Designer:1173）。同 PDF の要旨では「15 to 600 K」でフィット（要確認 R-3）。
- 温度は ℃ 選択時に +273.15 して K に直す（`FormMain.cs:920, 971`）。

## 2. ダイヤモンドラマンエッジスケール

実装: `FormMain.textBoxNu_TextChanged`（`FormMain.cs:780–808`）。$x=(\nu-\nu_0)/\nu_0$、ν と ν₀ は cm⁻¹。

| UI ラベル | 式（実装どおり） | 係数（既定値／編集可否） | 出典 | 適用範囲（UI 記載） |
|---|---|---|---|---|
| Akahama (2004): | $P=A+B\nu+C\times10^{-4}\nu^2$ （**ν は絶対値**、`FormMain.cs:789`） | A=66.9, B=-0.5281, C=3.585（Designer:423, 433, 443。編集可） | UI ラベル（Designer:1524） | 記載なし |
| Akahama (2006): | $P=K_0x\left[1+\tfrac12(K_0'-1)x\right]$ （`FormMain.cs:792`） | K0=547, K0'=3.75（Designer:1491, 1501。編集可） | UI ラベル（Designer:1554）。同梱 `references/Akahama2006.pdf` 式 (3)、ν₀ = 1334 cm⁻¹、K0 = 547(11) GPa, K0′ = 3.75(20) と一致 | 記載なし |
| Fratanduono+ (2021, <300GPa) | **コード**: $P=530.77x+753.83x^2$ （`FormMain.cs:794`）／**UI ラベル**: `503.77 × Δν/ν0 + 753.83 × (Δν/ν0)²`（Designer:1544） | 固定 | UI ラベル、`Version.cs` 履歴 ver1.002「Added two diamond Raman equations (Fratanduono et al., 2021)」 | < 300 GPa |
| Fratanduono+ (2021, >200GPa) | $P=199.49-852.78x+3103.8x^2$ （`FormMain.cs:796`） | 固定 | 同上（Designer:1534） | > 200 GPa |

- ν₀ の既定値は 1334（`textBoxDiamondRamanNu0.Text = "1334"`, Designer:1453）。レジストリ値 `textBoxAkahama2006Nu0.Text` があればそれで上書き（`FormMain.cs:118`）。ν₀ は Akahama 2006 と Fratanduono の 2 式だけが使い、Akahama 2004 は使わない。
- **Fratanduono (<300 GPa) の 1 次係数がコード（530.77）と UI ラベル・既存マニュアル（503.77）で食い違う**（Q-1）。どちらが原論文どおりかは原論文が同梱されていないので未確認。差は x=0.2（ν≈1600 cm⁻¹）で約 5.4 GPa。

## 3. 状態方程式（EOS）スケール

各表の「式」は §0.3 の略記を使う。$x_J\equiv1-V/V_0$（数表スケールの横軸）。

### 3.1 金（Gold） — `FormMain.Gold`（`FormMain.cs:1058–1086`）

入力 a₀ 既定 4.07825 Å（Designer:1870）。全スケールが UI の a₀ を使う。

| UI ラベル | 式 | パラメータ（コードの表記のまま） | 実装箇所 | 出典（コード/UI） | 適用範囲 |
|---|---|---|---|---|---|
| Jamieson (82) | 数表＋スプライン、$x_J=1-(a/a_0)^3$ | 表 `AuJamieson` | `EOS.Au_Jamieson` `EOS.cs:758–765`、表 `EOS.cs:689–738` | UI ラベル、`#region AuJamiesonの定義` | 表の格子: $x_J$ = -0.010〜0.225、T = 200〜1500 K（100 K 刻み）。外側は 1 次外挿 |
| Anderson (89) | BM3 ＋ $(0.00714 + {\rm KTperT}\cdot\ln(V_0/V))(T-300)$ | `KT = 166.65`, `KTperT = -0.0115`, `KTperP = 5.4823`, 定数 `0.00714` | `EOS.Au_Anderson` `EOS.cs:426–436` | UI ラベル、コメント「Andersonの状態方程式」 | 記載なし |
| Sim (02) | BM3 ＋ MGD（べき乗型 γ） | K0 `167`, K0' `5.0`; Z `4`, n `1`, θ0 `170`, γ0 `2.97`, q `1.0`, T0 `300` | `FormMain.cs:1068` | UI ラベル | 記載なし |
| Tsuchiya (03) | 数表＋スプライン、$x_J=1-(a/a_0)^3$ | 表 `Tsuchiya` | `EOS.Au_Tsuchiya` `EOS.cs:1036–1043`、表 `EOS.cs:1005–1024` | UI ラベル | $x_J$ = 0〜0.34、T = 300, 500, 1000, 1500, 2000, 2500 K |
| Yokoo (09) | 数表＋スプライン、$x_J=1-V/V_0$ | 表 `Table_Au_Yokoo` | `EOS.AuYokoo` `EOS.cs:1232–1238`、表 `EOS.cs:1196–1218` | UI ラベル、`#region YokooのAuのテーブル` | $x_J$ = 0〜0.40、T = 0, 300, 500, 1000, 1500, 2000, 2500, 3000 K（2500/3000 K 列の低圧縮側は NaN） |
| Fratanduono (21) | Vinet（熱圧力なし） | `k0 = 170.09`, `k_prime = 5.880` | `EOS.Au_Fratanduono_Vinet` `EOS.cs:404–410` | UI ラベル、コメント「Au (Fratanduono et al., 2021) Thermal pressureなし」 | 記載なし。**T を無視**（常に室温等温線） |

- Jamieson (82) は $a=a_0$, T = 300 K で 0.05 GPa を返す（表 `EOS.cs:693` の x=0.000, 300 K の値が `00.05`）。表の V₀ がどの温度の値かはコードに記載がない（R-5）。

### 3.2 白金（Platinum） — `FormMain.Pt`（`FormMain.cs:1089–1118`）

入力 a₀ 既定 3.9231 Å（Designer:2150）。

| UI ラベル | 式 | パラメータ | 実装箇所 | 出典 | 適用範囲 |
|---|---|---|---|---|---|
| Jamieson (82) | 数表＋スプライン、$x_J=1-(a/a_0)^3$ | 表 `PtJamieson` | `EOS.Pt_Jamieson` `EOS.cs:829–836`、表 `EOS.cs:769–809` | UI ラベル | $x_J$ = -0.010〜0.180、T = 200〜1500 K |
| Holmes (89) | $3B_T\frac{1-X}{X^2}\exp[1.5(B'_T-1)(1-X)] + {\rm AlfaT}\cdot B_T(T-300)/10000$、$X=a/a_0$ | `BT = 266`, `BprimeT = 5.81`, `AlfaT = 0.261` | `EOS.Pt_Holmez` `EOS.cs:501–509` | UI ラベル、コメント「Pt Holmesの状態方程式」 | 記載なし |
| Matsui (09) | Vinet ＋ MGD（べき乗型 γ, n=1）＋ $P_{el}(T)$ | `K0T = 273`, `Kprime0T = 5.20`, `gamma0 = 2.70`, `q = 1.10`, `theta0 = 230`, Z = 4（`/ 4`）; $P_{el}=$ `-1.01162E-11*T^3 + 1.89796E-07*T^2 + 6.73266E-05*T - 3.70065E-02` | `EOS.Pt_Matsui` `EOS.cs:517–544` | UI ラベル、XML コメント「Pt(Matsui2009)の状態方程式」、$P_{el}$ のコメント「PelT (the difference from the value at 300 K of 0.04 GPa) =0.21, 1.60, and 3.78 GPa at 1000, 3000, and 5000 K」を多項式近似 | 記載なし。**T₀ に UI 値を使う唯一の白金スケール** |
| Yokoo (09) | 数表＋スプライン、$x_J=1-V/V_0$ | 表 `Table_Pt_Yokoo` | `EOS.PtYokoo` `EOS.cs:1313–1320`、表 `EOS.cs:1277–1299` | UI ラベル | $x_J$ = 0〜0.40、T = 0〜3000 K（8 列） |
| Fratanduono (21) | Vinet（熱圧力なし） | `k0 = 259.7`, `k_prime = 5.839` | `EOS.Pt_Fratanduono_Vinet` `EOS.cs:417–423` | UI ラベル、コメント「Pt (Fratanduono et al., 2021) Thermal pressureなし」 | 記載なし。**T を無視** |

- Matsui (09) の $P_{el}$ は絶対温度 T だけの関数で、T₀ を変えても差し引かれない（$P_{el}(300\,{\rm K})\approx -1.7\times10^{-8}$ GPa）（Q-7）。

### 3.3 NaCl B1 — `FormMain.NaClB1`（`FormMain.cs:1196–1212`）

入力 a₀ 既定 5.639 Å（Designer:2297）。

| UI ラベル | 式 | パラメータ | 実装箇所 | 出典 | 適用範囲 |
|---|---|---|---|---|---|
| Brown (99) | 数表＋スプライン、$x_J=1-(a/a_0)^3$ | 表 `Brown` | `EOS.NaCl_Brown` `EOS.cs:994–1001`、表 `EOS.cs:913–978` | UI ラベル、`#region double[][] Brownの定義` | $x_J$ = -0.1699〜0.3197、T = 300〜1200 K（100 K 刻み）。膨張側（$x_J<0$）は高温列だけに値があり、他は NaN |
| Matsui (12) | BM4（$f$ 形式で直接記述）＋ MGD（べき乗型 γ） | `Kt0 = 23.7`, `KpT0 = 5.14`, `KppT0 = -0.392`; MGD: Z `4`, n `2`, θ0 `279`, γ0 `1.56`, q `0.96`, T0 `300` | `EOS.NaClB1_Matsui` `EOS.cs:481–499` | UI ラベル、コメント「Matui et al (2012) Am Min」 | 記載なし |

- `EOS.NaClB1_Sata`（`EOS.cs:444–449`）と `EOS.NaClB1_Skelton`（`EOS.cs:1183–1188`）は存在するが UI からは呼ばれない。

### 3.4 NaCl B2 — `FormMain.NaClB2`（`FormMain.cs:1168–1192`）

**a₀ 欄は無効化（`Enabled = false`, Designer:2526）されており、どのスケールも UI の a₀ を使わない。** a は Å のまま渡す。

| UI ラベル | 式 | パラメータ | V₀ の扱い | 実装箇所 | 出典 | T |
|---|---|---|---|---|---|---|
| Sata (02) (Pt) | $P=P_r(V/V_r)^{-2/3}\exp[-(3K_r/P_r-2)((V/V_r)^{1/3}-1)]$ | `Pr = 31.14`, `Kr = 143.5`（`FormMain.cs:1174`）, $V_r$ = `27.17` | 固定 $V_r$=27.17 Å³ | `EOS.NaClB2_Sata` `EOS.cs:438–442` | UI ラベル、コメント「SataさんのNaClB2の状態方程式」 | 無視 |
| Sata (02) (Mg) | 同上 | `Pr = 32.15`, `Kr = 141.0`（`FormMain.cs:1176`） | 同上 | 同上 | UI ラベル「(Mg)」、FormMain コメント「Sata MgO」 | 無視 |
| Ueda+(08) | Vinet ＋ `0.00468 * (T - 300)` | `Kt0 = 28.45`, `KpT0 = 5.16` | 固定 `V0 = 24.76 / 6.0221367 * 10`（= 41.115 Å³） | `EOS.NaClB2_Ueda` `EOS.cs:465–477` | UI ラベル、コメント「Ueda et al (2008) J app phis」 | 1 次の熱圧力 |
| Sakai+(11) BM | BM3 | `47.00`, `4.10` | 固定 `37.73` Å³ | `EOS.NaClB2_SakaiBM` `EOS.cs:452–456` | UI ラベル、コメント「Sakaiさんの状態方程式」 | 無視 |
| Sakai+(11) Vinet | Vinet | `40.40`, `5.04` | 固定 `37.73` Å³ | `EOS.NaClB2_SakaiVinet` `EOS.cs:458–462` | 同上 | 無視 |

- Sata の式は「基準点 $(V_r,P_r)$ で体積弾性率が $K_r$」になる 2 パラメータ形（$-V\,dP/dV|_{V_r}=K_r$ を確認した）。原論文の式形との照合は未実施（R-6）。
- Sakai の V₀ = 37.73 Å³ と Ueda の V₀ = 41.115 Å³ は 9 % 異なる（R-7）。

### 3.5 ペリクレース（MgO） — `FormMain.MgO`（`FormMain.cs:1140–1165`）

入力 a₀ 既定 4.2112 Å（Designer:2790）。全スケールが UI の a₀ を使う。

| UI ラベル | 式 | 等温部 | 熱圧力部 | 実装箇所 | 出典 |
|---|---|---|---|---|---|
| Jackson (98) | BM3 ＋ MGD（べき乗型 γ） | K0 `162.5`, K0' `4.13` | Z `4`, n `2`, θ0 `673`, γ0 `1.41`, q `1.3`, T0 `300` | `FormMain.cs:1146` | UI ラベル（FormMain 内の名前は `Jacson`） |
| Dewaele (00) | BM3 ＋ MGD | K0 `161`, K0' `3.94` | Z `4`, n `2`, θ0 `800`, γ0 `1.45`, q `0.8`, T0 `300` | `FormMain.cs:1148` | UI ラベル |
| Aizawa (06) | BM3 ＋ MGD | K0 `160`, K0' `4.15` | Z `4`, n `2`, θ0 `773`, γ0 `1.41`, q `0.7`, T0 `300` | `FormMain.cs:1150` | UI ラベル |
| Tange (09) Vinet | Vinet ＋ Tange 型 | `k0 = 160.63`, `k_prime = 4.367` | `theta0 = 761`, `gamma0 = 1.442`, `a = 0.138`, `b = 5.4`, `n = 2`, `z = 4`, T0 300（固定） | `EOS.MgO_Tange_Vinet` `EOS.cs:348–372` | UI ラベル |
| Tange (09) BM | BM3 ＋ Tange 型 | `k0 = 160.64`, `k_prime = 4.221` | `theta0 = 761`, `gamma0 = 1.431`, `a = 0.29`, `b = 3.5`, `n = 2`, `z = 4`, `t0 = 300.0` | `EOS.MgO_Tange_BM` `EOS.cs:374–397` | UI ラベル |

- 適用範囲の記載はコードに無い。Tange (09) の 2 スケールは Debye エネルギーの多項式近似のため **T ≲ 110 K（V/V₀=1 で θ/T>7）では熱圧力が誤る**（Q-3）。
- `EOS.MgO_Jamieson`（`EOS.cs:902–909`, 表 `EOS.cs:840–897`）は存在するが UI からは呼ばれない。

### 3.6 コランダム（Corundum, Al₂O₃） — `FormMain.Corundum`（`FormMain.cs:1215–1229`）

入力 V₀ 既定 255.89472 Å³（Designer:2889）。

| UI ラベル | 式 | パラメータ | 実装箇所 | 出典 |
|---|---|---|---|---|
| Dubrovinsky (98) | BM3（$K_T$, $V_0(T)$ 使用） | `T0 = 300`, `KTperT = -0.020`, `KTperP = 4.88`, `KT = 258 + KTperT * (T - T0)`, `a = 2.6E-5`, `b = 1.81E-9`, `c = -0.67`; $V_0(T)=V_0\exp[a(T-T_0)+\frac b2(T^2-T_0^2)-c(\frac1T-\frac1{T_0})]$ | `EOS.Corundum_Dubrovinsky` `EOS.cs:547–559` | UI ラベル、コメント「Dubrovinskyの状態方程式」 |

### 3.7 レニウム（Re） — `FormMain.Re`（`FormMain.cs:1232–1254`）

入力 V₀ 既定 29.42795 Å³（Designer:3233）。

| UI ラベル | 式 | パラメータ | V₀ の扱い | 実装箇所 | 出典 | T |
|---|---|---|---|---|---|---|
| Zha et al. (04) | 数表＋スプライン、$x_J=1-V/V_0$ | 表 `Zha` | UI の V₀ | `EOS.Re_Zha` `EOS.cs:1083–1090`、表 `EOS.cs:1047–1069` | UI ラベル、`#region double[][] Zha et al. レニウムの定義` | 表: $x_J$ = 0〜0.20、T = 300〜3000 K（7 列） |
| Anz (##) | Vinet | `352.6`, `4.56` | **固定 `29.467`** | `FormMain.cs:1240` | UI ラベル「Anz (##)」のみ（年なし） | 無視 |
| Sakai. (##) | Vinet | `358`, `4.8` | **固定 `29.47`** | `FormMain.cs:1242` | UI ラベル「Sakai. (##)」のみ | 無視 |
| Dub. (##) | BM4 | `342`, `6.15`, `-0.029` | **固定 `29.46`** | `FormMain.cs:1244` | UI ラベル「Dub. (##)」のみ | 無視 |

- UI の V₀ 既定値（29.42795）と固定 V₀ が異なるため、$V=V_0^{\rm UI}$ でも Anz 0.469、Sakai 0.513、Dub 0.374 GPa を返す（§4）。

### 3.8 アルゴン（Ar） — `FormMain.Ar`（`FormMain.cs:1121–1137`）

**a₀ 欄（`numericBoxArA0`）の値はどちらのスケールにも使われない**（Designer に `Value` の設定も無い）。a は nm に直して渡す。

| UI ラベル | 式 | パラメータ | 実装箇所 | 出典 | T |
|---|---|---|---|---|---|
| Ross et al. (86) | モル体積 $V_m=N_A a^3/4$ [cm³/mol] に対する P の自然スプライン | 表 `Ross`（$V_m$ = 19〜4.5 cm³/mol、P = 1.6〜689.2 GPa、16 点、見出し温度 `273`） | `EOS.Ar_Ross` `EOS.cs:1367–1371`、表 `EOS.cs:1343–1360` | UI ラベル、コメント「Ar Ross et al. 1986 / cm^3/mol, GPa」 | 無視（等温線 1 本） |
| Jephcoat (98) | BM3 ＋ $\gamma/V\,[E(T)-E(T_0)]$（θ 一定, n=1） | `K0T = 3.03`, `Kprime0T = 7.24`, `gamma0 = 0.5`, `gamma1 = 2.20`（$\gamma=\gamma_0+\gamma_1V/V_0$）, `theta0 = 93.3`, `Z = 4`, `V0 = 22.557 / 100`（=22.557 cm³/mol）, **`T0 = 4`**, R `8.31451` | `EOS.Ar_Jephcoat` `EOS.cs:1373–1396` | UI ラベル | 基準 4 K |

- Ross の表外（$V_m$>19 cm³/mol、すなわち a ≳ 5.02 Å、P<1.6 GPa）は 1 次外挿。

### 3.9 モリブデン（Mo） — `FormMain.Mo`（`FormMain.cs:1257–1289`）。**UI 上は非表示**（`groupBoxMo.Visible = false`, Designer:3251）

入力 V₀ 既定 31.14 Å³（Designer:3370）。計算自体は毎回実行される（`FormMain.cs:1053`）。

| UI ラベル | 式 | パラメータ | 実装箇所 |
|---|---|---|---|
| Huang+(16)MGD | BM3 ＋ MGD（べき乗型 γ） | K0 `255`, K0' `4.25`; Z `2`, n `1`, θ0 `470`, γ0 `2.01`, q `0.6`, T0 `300` | `FormMain.cs:1264` |
| Zhao+(00) | `EOS.GetPressure`（`T_dependence_BM` + `BM4`） | `Z = 2`, `K0 = 268`, `Kp0 = 3.81`, `Kpp0 = -1.41E-2`, `KperT = -2.13E-2`, `A = 1.31`, `B = 11.2`, `T0 = 300`（C は未設定 = 0） | `FormMain.cs:1267–1282`、`EOS.cs:160–176` |

### 3.10 鉛（Pb） — `FormMain.Pb`（`FormMain.cs:1383–1411`）。**UI 上は非表示**（`groupBoxPb.Visible = false`, Designer:3386）

入力 a₀ 既定 4.95059 Å（Designer:3470）、T₀ に UI 値を使う。

| UI ラベル | 式 | パラメータ | 実装箇所 |
|---|---|---|---|
| Strässle+(14) | $a_0(T)=a_0^{\rm UI}\cdot a_0^{\rm tab}(T)/a_0^{\rm tab}(T_0)$、$X=a/a_0(T)$、$P=3B(T)\frac{1-X}{X^2}\exp[1.5(B'(T)-1)(1-X)]$ | 表 `PbVinet`（T = 0〜300 K, 20 K 刻み; B: `48.3298`〜`41.2000`, B′: `5.4511`〜`5.7245`）、表 `PbA0`（T = 0〜310 K, 5 K 刻み; `4.91366`〜`4.95110` Å）。いずれも `MathNet.Numerics.Interpolate.Linear`（範囲外は 1 次外挿） | 表 `FormMain.cs:1292–1310`, `1313–1379`、式 `FormMain.cs:1396–1404` |

- 出典はラベル「Strässle+(14)」（Designer:3446）と返り値名 `"Strassle14"` のみ。

## 4. 代表値での計算値（C# 実行値と Python 独立実装の比較）

### 4.1 方法

- **C# 側**: Linux 上に .NET 8 SDK を入れ、`Crystallography/EOS.cs`・`Mathematics/Spline.cs`・`ExtensionMethods.cs`（`MathnetEx` 部分）をそのまま、`PointD`/`Profile`/`UniversalConstants` を最小スタブにしてコンソールアプリとしてビルドした。FormMain 側の式（Sim, Jackson など FormMain に直書きのもの、Pb、ルビー、ダイヤモンド）は FormMain.cs の該当行をそのまま貼った。ハーネスは `docs/drafts/tools/cs_harness/` に置いた（リポジトリのビルド対象外。拡張子 `.txt`）。
- **Python 側**: `docs/drafts/tools/py_eos.py`。式は本稿 §0–3 の数式から書き起こし、数表は EOS.cs/FormMain.cs から読み取って scipy の自然 3 次スプライン（端は 1 次外挿）で補間、Debye 積分は `scipy.integrate.quad`、Tange 型だけはコードと同じ多項式を使った。
- 入力: 各物質の UI 既定 a₀/V₀、T₀ = 300 K。全点の比較は `docs/drafts/tools/compare.csv`（567 点）。

### 4.2 結果の要約

- 全スケールで |C# − Python| ≤ 4×10⁻⁴ GPa。差が 10⁻⁴ GPa 程度残るのは MGD 熱圧力を含むスケールだけで、原因は C# の `integ` の和が `i = 1 … num`（中点が 1 区間ずれる）であること（Q-10）。数表スケールの差は ≤ 3×10⁻⁸ GPa で、焼き込みスプライン係数は表と整合している。
- 以下の表は C# の値（GPa）。最終列は全試験点（T = 300/1000/2000 K、V/V₀ = 1.00〜0.80）での最大差。

**T = 300 K、V/V₀ 依存（P / GPa）**

| スケール | V/V₀=0.95 | 0.90 | 0.85 | 0.80 | max\|C#−Py\| |
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
| Mo Huang16（非表示） | 14.588 | 33.629 | 58.619 | 91.667 | 2.8e-04 |
| Mo Zhao00（非表示） | 15.157 | 34.503 | 59.317 | 91.350 | 1.4e-13 |
| Pb Strassle14（非表示） | 2.445 | 5.844 | 10.555 | 17.087 | 5.0e-14 |

（Re Anz/Sakai/Dub の V/V₀ は UI の V₀ = 29.42795 Å³ に対する比。）

**温度依存（P / GPa、V₀ は UI 既定値＝300 K 値とみなす）**

| スケール | V/V₀=1.00, 1000 K | 0.90, 1000 K | 1.00, 2000 K | 0.90, 2000 K |
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
| Re Anz / Sakai / Dub | （T 非依存。300 K と同値） | | | |
| Mo Huang16 | 3.611 | 37.328 | 8.928 | 42.860 |
| Mo Zhao00 | 3.710 | 38.032 | 11.137 | 46.151 |

**Pb（T₀ = 300 K、a₀ = 4.95059 Å）**: V/V₀=1.00 で 10 K: −0.994、100 K: −0.730、400 K（表外・外挿）: 0.335 GPa。V/V₀=0.90 で 10 K: 5.050、100 K: 5.268 GPa。

**NaCl B2（入力 a / Å、T = 300 K。最右列のみ 2000 K）**

| スケール | a=3.00 | 2.93 | 2.85 | 2.75 | 2.93 @2000 K |
|---|---:|---:|---:|---:|---:|
| Sata02 (Pt) | 32.053 | 44.254 | 64.070 | 101.977 | 44.254 |
| Sata02 (MgO) | 33.047 | 44.922 | 63.892 | 99.460 | 44.922 |
| Ueda08 | 33.421 | 45.930 | 64.705 | 97.114 | 53.886 |
| Sakai11 BM | 31.352 | 44.006 | 63.029 | 96.142 | 44.006 |
| Sakai11 Vinet | 30.310 | 43.191 | 62.701 | 96.689 | 43.191 |

**Ar（入力 a / Å）**

| スケール | a=4.80, 300 K | 4.40, 300 K | 4.0786, 300 K | 3.60, 300 K | 4.0786, 1000 K |
|---|---:|---:|---:|---:|---:|
| Jephcoat98 | 3.462 | 12.146 | 32.839 | 145.385 | 35.393 |
| Ross86 | 3.107 | 11.404 | 31.674 | 143.380 | 31.674 |

**ルビー（R1₀ = 694.25 nm、A は UI 既定値）**

| スケール | R1=697 nm | 700 | 705 | 710 | 720 |
|---|---:|---:|---:|---:|---:|
| Mao (1978) | 7.602 | 16.033 | 30.409 | 45.200 | 76.057 |
| Mao-quasi (1986) | 7.642 | 16.212 | 31.049 | 46.604 | 79.987 |
| Mao-hydro (2000) | 7.643 | 16.215 | 31.061 | 46.631 | 80.064 |
| Shen et al. (2020) | 7.572 | 16.210 | 31.480 | 47.842 | 83.843 |

**Ragan 補正: 参照 R1 = 694.25 nm @ 297.15 K で較正したときの R1₀(T)**: 50 K: 693.264、100 K: 693.314、200 K: 693.667、400 K: 695.015、600 K: 696.508 nm（較正定数 c = 14423.428）。

**ダイヤモンド（ν₀ = 1334 cm⁻¹、係数は UI 既定値）**

| スケール | ν=1334 | 1400 | 1500 | 1600 | 1800 | 2000 |
|---|---:|---:|---:|---:|---:|---:|
| Akahama (2004) | 0.385 | 30.220 | 81.375 | 139.700 | 277.860 | 444.700 |
| Akahama (2006) | 0.000 | 28.904 | 79.714 | 138.977 | 282.861 | 460.558 |
| Fratanduono (<300 GPa)、コードの 530.77 | 0.000 | 28.105 | 77.721 | 135.808 | 277.400 | 452.880 |
| （参考）同、UI ラベルの 503.77 で計算した場合 | 0.000 | 26.769 | 74.361 | 130.425 | 267.968 | 439.400 |
| Fratanduono (>200 GPa) | 199.490 | 164.896 | 141.434 | 152.854 | 280.343 | 547.364 |

- Fratanduono (>200 GPa) は x=0 で 199.49 GPa、x≈0.137（ν≈1517 cm⁻¹）で極小 140.9 GPa を取る 2 次式であり、200 GPa 未満の領域では意味を持たない（UI ラベルどおり >200 GPa でのみ使う）。

## 要確認事項

- R-1 Mao-hydro (2000) の指数 7.715 と「2000」年の出典がコード・同梱 PDF のどこにも無い。
- R-2 Mao (1978)/Mao-quasi/Mao-hydro の A を 3 つとも 1904 にしているのは意図どおりか。
- R-3 Ragan の適用範囲: UI は「50-600K」、同梱 PDF 要旨は「15 to 600 K」。50 K にした理由。
- R-4 Fratanduono (<300 GPa) の 1 次係数 530.77（コード）と 503.77（UI・既存マニュアル）のどちらが正しいか（Q-1）。原論文は同梱されていない。
- R-5 Jamieson (82) の Au/Pt 表で x=0, 300 K が 0.05 GPa になっている。表の V₀ の基準温度（293 K など）をコードに明記すべきか。
- R-6 NaCl B2 Sata (02) の式形（$V_r$=27.17 Å³ を基準にした 2 パラメータ形）が原論文どおりか。
- R-7 NaCl B2 の基準体積: Sakai 37.73 Å³ と Ueda 41.115 Å³（24.76 cm³/mol）の差。
- R-8 Re の Anz/Sakai/Dub の出典（UI ラベルが「(##)」のまま）と、UI の V₀ を使わず固定値を使う意図。
- R-9 Au Anderson (89) の熱圧力項 $(0.00714-0.0115\ln(V_0/V))(T-300)$ の係数の意味（変数名 `KTperT` が ln 項の係数に使われている）。
- R-10 Pt Holmes (89) の `AlfaT = 0.261` は α×10⁴ /K の意味か（式は `AlfaT * BT * (T - 300) / 10000`）。
- R-11 Ar Jephcoat (98) の基準温度 4 K と、Ross (86) 表の見出し温度 273 の扱い。
- R-12 Mo・Pb の欄が非表示（`Visible = false`）なのは意図どおりか（既存マニュアルは利用可能スケールとして掲載）。
- R-13 各スケールの適用圧力・温度範囲（コードに記載なし）。
- R-14 文献の書誌情報（誌名・巻・頁）をマニュアルに載せるか。載せる場合はコード外の情報なので著者が確認する必要がある。
