本稿はコードから Claude が起こした下書き（2026-11-05）。著者未査読。

# 実装上の疑問点

- 対象: PressureCalculator `master` @ `1676a57`、Crystallography @ `bd3caf4`。行番号は [圧力スケール一覧](02-pressure-scales.md) と同じ表記。
- コードは一切変更していない。ここに挙げたものは「誤りの疑い」であって、修正方針は著者判断。
- 「確認方法」欄: **実行** = Linux 上で EOS.cs 等をビルドした C# ハーネス・Python で値を確認、**読解** = コードを読んでの推定（実機 Windows では未確認）、**実測** = 外部に実アクセスして確認。
- 重大度は Claude の主観（高: 表示される圧力値・機能が誤る／中: 条件次第で誤る／低: 表示・保守性）。

| ID | 重大度 | 内容 | 確認方法 |
|---|---|---|---|
| Q-1 | 高 | Fratanduono (<300 GPa) の 1 次係数がコード 530.77 と UI ラベル 503.77 で不一致 | 読解・実行 |
| Q-2 | 高 | 英語 UI で F1／「Online manual」を押すと各モードのページが 404 | 実測 |
| Q-3 | 中 | MgO Tange (09) の Debye エネルギー多項式が低温（θ/T ≳ 7）で発散 | 実行 |
| Q-4 | 中 | 「Gaussian blur σ」の重みが整数除算でガウス関数になっていない | 読解 |
| Q-5 | 中 | 初回起動時（レジストリ未作成時）の既定値の読み込み順・キー名の問題 | 読解 |
| Q-6 | 中 | 「参照条件と同じ」が ON のまま参照温度を変えても測定温度が追従しない | 読解 |
| Q-7 | 低 | Pt Matsui (09) の電子熱圧力 $P_{el}$ が T₀ に依存しない | 読解・実行 |
| Q-8 | 低 | ダイヤモンド欄の入力途中（空欄・「-」など）でも毎回エラーダイアログ | 読解 |
| Q-9 | 低 | ルビー欄見出しの「Δ=R1-R1₀」は実装（相対値）と不一致 | 読解 |
| Q-10 | 低 | Debye 積分 `integ` の和の添字が 1 区間ずれている | 実行 |
| Q-11 | 低 | `EOS.MieGruneisen` の XML コメントの単位・引数説明が実装と不一致 | 読解 |
| Q-12 | 中 | CSV エクスポートの列ずれ・例外の可能性 | 読解 |
| Q-13 | 中 | ファイル監視タイマが UI スレッドで毎秒 0.4 s 停止する | 読解 |
| Q-14 | 低 | ルビーモードのダブルクリックで「ローレンツ当てはめ」と「クリップボードコピー」が同時に起こる | 読解 |
| Q-15 | 低 | 気体定数・アボガドロ数の値がスケールごとに異なる | 読解 |

---

### Q-1 Fratanduono (<300 GPa) の係数不一致（高）

- コード: `textBoxDiamondFratandunoLow.Text = (530.77 * nuPerNu0 + 753.83 * nuPerNu0 * nuPerNu0)`（`FormMain.cs:794`）
- UI ラベル: `"Fratanduono+ (2021, <300GPa):  P = 503.77 × Δν/ν0 + 753.83 × (Δν/ν0)² = "`（Designer:1544）
- 既存マニュアル（`docs/src/*/2-diamond-raman.md`、11 言語）も 503.77。
- 影響: ν = 1400/1500/1600/1800 cm⁻¹ で 28.105/77.721/135.808/277.400 GPa（530.77）に対し 26.769/74.361/130.425/267.968 GPa（503.77）。差は 1 次項のみで $27\,x$ GPa（x=Δν/ν₀）。
- 原論文が同梱されていないため、どちらが正しいかは判定できない。

### Q-2 英語版の F1 ヘルプが 404（高）

- `FormMain` コンストラクタの `HelpUrlResolver`（`FormMain.cs:47–52`）は、英語なら `https://seto77.github.io/PressureCalculator/{HelpPage}/` を返す（コメント「en はサイトルート直下」）。
- 一方 `docs/mkdocs.yml` は static-i18n プラグインを使っておらず、英語ページは `en/1-ruby-fluorescence.md` などとして nav に入っている（`docs_dir: src`、`use_directory_urls: true`）。`language_switch.js` のコメントにも「English content sits in an `en/` subfolder」とある。
- 2026-11-05 に実アクセスで確認: `…/PressureCalculator/1-ruby-fluorescence/` → **404**、`…/PressureCalculator/en/1-ruby-fluorescence/` → 200、`…/PressureCalculator/ja/1-ruby-fluorescence/` → 200。
- `docs/requirements.txt` に `mkdocs-redirects` があるが `mkdocs.yml` の `plugins` には入っていない。
- 選択肢（著者判断）: resolver を `en/` 付きにする、または mkdocs-redirects でリダイレクトを置く。

### Q-3 MgO Tange (09) の低温での熱圧力破綻（中）

- `EOS.MgO_Tange_Vinet`/`MgO_Tange_BM` は Debye エネルギー関数 $e(u)=u^{-3}\int_0^u z^3/(e^z-1)dz$ を 6 次多項式で近似（`EOS.cs:363–366`, `388–391`）。
- Python で厳密値と比較: u = 6 までは相対誤差 < 10⁻⁴、u = 7.6 で −4 %、u = 10 で符号反転（−0.0203 対 +0.0064）、u = 15 で −1.58 対 +0.0019。
- MgO（θ₀ = 761 K）では V/V₀ = 1 で T ≈ 100 K が u ≈ 7.6 に当たる。Tange (09) Vinet の熱圧力（V/V₀ = 1）:

  | T | θ/T | 多項式（実装） | 厳密な Debye 関数 |
  |---|---:|---:|---:|
  | 200 K | 3.81 | −0.4153 GPa | −0.4153 GPa |
  | 100 K | 7.61 | −0.6431 | −0.6419 |
  | 77 K | 9.88 | −0.6934 | −0.6590 |
  | 50 K | 15.22 | −2.3899 | −0.6670 |
  | 10 K | 76.10 | −34755.5 | −0.6688 |

- EOS の Temperature 欄には最小値が設定されていない（`NumericBox.Minimum` の既定は −∞、Designer に設定なし）ので 0 K 以下も入力でき、低温 DAC 実験で MgO を使うと Tange の 2 欄だけが大きく外れる。他の MGD スケール（Jackson, Dewaele, Aizawa）は数値積分なので問題ない。

### Q-4 「Gaussian blur σ」が整数除算（中）

- 重み `Math.Exp(-j * j / n / n)`（`FormMain.cs:456–457`, `504–505`）で、`j` も `n` も `int`。`-j * j / n / n` は整数演算になり、重みは $\exp(-\lfloor j^2/n^2\rfloor)$（0 方向への切り捨て）の階段関数になる。
- 例: σ = 8（既定）のとき、|j| ≤ 7 の点は重み 1、8〜11 は e⁻¹、12〜13 は e⁻²、… となる。窓は常に ±50 点で打ち切り。
- 仮に整数除算を直しても、重み $e^{-j^2/\sigma^2}$ は標準偏差 $\sigma/\sqrt2$ のガウス関数であり、「σ」の呼び名とは √2 ずれる。単位はデータ点数（軸の単位ではない）。
- 影響: ピーク位置はほぼ変わらないが、平滑化の度合いがラベルの意味と異なる。

### Q-5 初回起動時の既定値（中）

`FormMain.ReadInitialRegistry`（`FormMain.cs:96–130`）について、レジストリキー `HKCU\Software\Crystallography\PressureCalculator` に値が無い状態（初回起動）を読解すると:

1. モード: `radioButtonDiamondRaman`、`radioButtonEOS`、`radioButtonRubyFluorescence` の順に、既定値 `"True"` で `Checked = true` を設定する（`FormMain.cs:111–113`）。3 つは同じ `flowLayoutPanel1` 内（Designer:845–847）なので最後の **ルビー蛍光** が選ばれる。Designer の既定（`radioButtonDiamondRaman.Checked = true`, Designer:819）とは異なる。
2. 温度単位: K → ℃ の順に既定値 `"True"`（`FormMain.cs:123–124`）。同じ `groupBoxMao` 内（Designer:914–915）なので **℃** が選ばれる。このとき参照温度・測定温度欄の値 297.15 は **297.15 ℃** と解釈される。
3. 参照 R1: `numericBoxRubyRefR1.Text = GetValue("numericBoxRubyRefR1.Text", numericBoxRubyRefT.Text)`（`FormMain.cs:121`）。既定値に **温度欄のテキスト**（"297.15"）を渡しているので、参照 R1 が **297.15 nm** になる。終了時にこの値が保存される（`FormMain.cs:150`）ので、ユーザーが直すまで残る。
4. キー名の不一致: 読み込みは `"numericBoxRubyR1_0.Text"`（`FormMain.cs:119`）、保存は `"textBoxMaoRamda0.Text"`（`FormMain.cs:148`）。どちらも `numericBoxRubyRefR1` を対象にしており、`numericBoxRubyR1_0`（測定条件の R1₀）自体は保存も復元もされない。

- 3 の結果、「Ragan の式から計算」を ON にすると R1₀ ≈ 297 nm となり、ルビー圧力が大きな負値になるはず（未実行）。
- 実機での確認が必要（R-Q5）。

### Q-6 「参照条件と同じ」の追従（中）

- `checkBoxRubyTemeratureSameAsRef_CheckedChanged`（`FormMain.cs:925–930`）はチェックが変わった瞬間だけ `numericBoxRubyT.Value = numericBoxRubyRefT.Value` を代入する。
- `numericBoxRubyRefT_ValueChanged`（`FormMain.cs:911`）は `calcRaganParameter()` を呼ぶだけで測定温度を更新しない。
- 既定ではチェック ON・測定温度欄は無効（Designer:1223, 1242）なので、参照温度を変えても測定温度は古い値のまま固定され、ユーザーは編集もできない。

### Q-7 Pt Matsui (09) の $P_{el}$（低）

- `double Pel = -1.01162E-11 * T * T * T + 1.89796E-07 * T * T + 6.73266E-05 * T - 3.70065E-02;`（`EOS.cs:540`）は絶対温度 T だけの関数で、T₀（UI 値、`FormMain.cs:1103` で渡される）を使った差し引きがない。$P_{el}(300\,{\rm K})=-1.7\times10^{-8}$ GPa なので T₀ = 300 K なら問題ないが、T₀ ≠ 300 K を入れると格子熱圧力（T₀ 基準）と電子熱圧力（300 K 基準）の基準が揃わない。
- また $P_{el}$ には体積依存がない（コメントの数値を T だけで多項式近似したもの）。

### Q-8 ダイヤモンド欄の入力エラーダイアログ（低）

- `textBoxNu_TextChanged`（`FormMain.cs:780–808`）は ν、ν₀、Akahama 2004/2006 の係数欄の `TextChanged` すべてに結び付いており（Designer:424, 434, 444, 1454, 1473, 1492, 1502）、`Convert.ToDouble` が失敗するたびに MessageBox を出す。係数を打ち直す途中（空欄、「-」だけ等）でもダイアログが出る。

### Q-9 ルビー欄の見出し（低）

- 見出し `groupBox4.Text = "Pressure calculation, where x = R1/R1₀ ,  f(x, y) = (x^y-1)/y ,  Δ=R1-R1₀"`（Designer:981）。Shen 欄 `numericBoxShenA.FooterText = "(Δ+5.63 Δ²)="`（Designer:1038）。
- 実装の Δ は $(R1-R1_0)/R1_0$（`FormMain.cs:1014`）。式は同梱 Shen et al. PDF と一致するので、見出しの表記を「Δ=(R1-R1₀)/R1₀」にすべきと思われる。

### Q-10 `integ` の添字（低）

- `for (int i = 1; i <= num; i++) { z = (i + 0.5) * x / num; ... }`（`EOS.cs:339–343`）。中点則なら `i = 0 … num-1`。現状は区間 $[x/n,\,x(1+1/n)]$ を積分しており、相対誤差は約 $10^{-5}$ 程度。
- 影響: MGD 熱圧力を含むスケール（Sim, Matsui09, Matsui12, Jackson, Dewaele, Aizawa, Huang, Jephcoat）で 2000 K のとき ≤ 4×10⁻⁴ GPa（§4 の C#/Python 差はこれでほぼ説明できる）。実用上は無視できる。

### Q-11 `EOS.MieGruneisen` のコメント（低）

- XML コメント（`EOS.cs:179–190`）は `v0`「Stamdard volume (nm^3)」、`v`「Target temperature」、`t`「Target volume (nm^3)」と書いているが、実装上 `t` は温度、`v` は体積で、単位は `v * 6.0221367 / z` と `/ 100`（GPa）の組で整合するのは **Å³**。呼び出し側（FormMain）も Å³ を渡している。

### Q-12 CSV エクスポート（中）

`menuItemExport_Click`（`FormMain.cs:1448–1489`）:

1. データ行は `$"{smoothPt[i].X},{originalPt[i].Y},{smoothPt[i].Y}"`（`FormMain.cs:1469`）。移動平均を選んでいると `OriginalSmooth` は点数が N−n+1 で X も平均化されている（`FormMain.cs:431–441`）ため、「Y (original)」が (n−1)/2 点ずれた X に並ぶ。ガウスぼかしなら X はそのまま。
2. フィット曲線の列を足すループ `for (int i = 0; i < textList.Count && (i < r1Pt.Length || i < r2Pt.Length); i++) textList[i + 1] += ...`（`FormMain.cs:1472–1476`）は `i + 1` に書くので、フィット曲線の点数（`range * 1.8 / (range / 200)` ≈ 360 点）以上のデータ行が無いと `i + 1 == textList.Count` で例外になりうる（データが 360 点未満の場合）。
3. ルビーモードで「Y(R1)」は R1 成分＋R1 のバックグラウンド、「Y(R2)」は R2 成分＋R2 のバックグラウンドで、合成曲線（グラフの桃色線）は出力されない。

### Q-13 ファイル監視タイマ（中）

- `timer`（Interval 1000 ms, Designer:3606–3607）は `FormMain_Load` で無条件に `Start()` され（`FormMain.cs:71`）、ファイルを一度読み込むと `timer_Tick`（`FormMain.cs:1528–1558`）が毎秒 `Thread.Sleep(200)` を 2 回、UI スレッドで実行する（`FormMain.cs:1534`, `1556`）。ファイルが更新されていなくても約 0.4 s/s UI が止まる。
- 「ファイルが更新されたら再読み込み」はレジストリに保存されず、起動時は常に ON（Designer:3575）。

### Q-14 ルビーモードのダブルクリック（低）

- 上下のグラフの `MouseDoubleClick2` に `graphControlBottom_MouseDoubleClick2` が結び付く（Designer:660, 727）。ルビーモードでは、クリック点 ±Fitting Range の点に対してローレンツ関数（バックグラウンドなし）を格子探索で当てはめ、R1 欄を上書きする（`FormMain.cs:617–702`）。
- この関数は常に `false` を返すので、GraphControl 側の既定動作（表示中プロファイルのクリップボードへのタブ区切りコピー、`GraphControl.cs:1638–1648`）も続けて実行される。
- ダイヤモンドモードでは先頭で `return false`（ローレンツ当てはめは無し、クリップボードコピーのみ）。

### Q-15 物理定数（低）

- R: `UniversalConstants.R = 8.3144621`（MGD 9 引数版、Pt Matsui）、`8.31451`（`EOS.GetPressure`、Ar Jephcoat）、`8.3145`（Tange）。N_A: `6.0221367`（多く）、`6.0221`（Tange）。相対差 ≤ 6×10⁻⁶ で数値上は無視できる。

## 要確認事項

- R-Q1 Q-1 の正しい係数（原論文の確認）。
- R-Q2 Q-2 は resolver 側・サイト側のどちらで直す方針か。
- R-Q3 Q-3: Tange (09) に UI 上の温度下限を設けるか、数値積分に置き換えるか。
- R-Q5 Q-5 の 1〜4 は読解による推定。Windows 実機で、レジストリキーを消した状態で起動して確認が必要。
- R-Q12 Q-12 の 2（例外）は、360 点未満のスペクトルで実際に起こるか未確認。
- R-Q13 Q-13 の UI 停止が体感できる程度か未確認。
