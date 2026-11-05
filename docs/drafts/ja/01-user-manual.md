本稿はコードから Claude が起こした下書き（2026-11-05）。著者未査読。

# PressureCalculator 利用マニュアル（コード準拠の下書き）

- 対象: PressureCalculator `master` @ `1676a57`（表示上の版: ver1.009 (2025/10/16)）、Crystallography @ `bd3caf4`、Crystallography.Controls @ `eca409d`。
- UI 文字列は日本語 UI の実際の文字列（`PressureCalculatorLocalizationData.cs` の ja 列）を使う。訳が登録されていない文字列（「EOS」、数値欄の見出し「Temperature」「T₀」、文献ラベル、元素記号など）は英語のまま表示される。
- 圧力スケールの式とパラメータは [圧力スケール一覧](02-pressure-scales.md) を参照。本稿では操作と設定項目の意味だけを書く。

## 1. 画面（フォーム）一覧

| 画面 | 種類 | 実装 | 内容 |
|---|---|---|---|
| メインウィンドウ | `FormMain`（`Crystallography.Controls.FormBase` 派生） | `FormMain.cs`, `FormMain.designer.cs` | アプリの唯一のウィンドウ。3 つのモード（ダイヤモンドラマン／ルビー蛍光／EOS）を切り替えて使う |
| 言語変更の確認 | MessageBox | `FormMain.cs:196–204` | 「表示言語を「…」に切り替えるには PressureCalculator の再起動が必要です。…今すぐ再起動しますか？」 |
| 再起動失敗 | MessageBox | `FormMain.cs:228–235` | 「PressureCalculator の再起動に失敗しました。」 |
| 数値エラー | MessageBox | `FormMain.cs:802–806` | 「適切な数値を入れてください」（ダイヤモンド欄の数値が解釈できないとき） |
| ファイルを開けない | MessageBox | `FormMain.cs:1510–1514`, `1548–1552` | 「ファイルを開けません」 |
| （非対話）スモークテスト | コマンドライン `--smoke [出力ファイル]` | `Program.cs:20–28` | `arch=…` と `baseDir=…` をファイル（既定 `smoke-result.txt`）に書いて終了。CI 用 |
| （非対話）画面キャプチャ | コマンドライン `--capture <出力フォルダ> <カルチャ>` | `Program.cs:30–35`, `GuiCapture.cs` | 指定言語で起動し、ルビー・ダイヤ・EOS の 3 モードを `FormMain-ruby.png` 等として保存して終了。設定はレジストリに保存しない |

ウィンドウタイトルは「Pressure Calculator  ver1.009 (2025/10/16)」形式（`FormMain.cs:58`）、ファイル読み込み後は「Pressure Calculator    <ファイル名>」に変わる（`FormMain.cs:302`）。`FormBase` によりタイトル右端に「(F1: ヘルプ)」が付く。

## 2. メインウィンドウの構成

<!-- screenshot: メインウィンドウ全体（ルビー蛍光モード、スペクトル読み込み・フィット済み、日本語 UI） -->

### 2.1 メニュー

| メニュー | 項目 | 動作 | 実装 |
|---|---|---|---|
| ファイル | 読み込み | ファイルを開くダイアログでスペクトルを読み込み、平滑化・フィットまで実行 | `menuItemFileRead_Click` `FormMain.cs:1493–1517` |
| | CSV形式でエクスポート | 表示中のスペクトルとフィット曲線を CSV に保存（§7） | `menuItemExport_Click` `FormMain.cs:1448–1489` |
| | ファイルが更新されたら再読み込み | チェック時、1 秒ごとに読み込み済みファイルの更新時刻を調べ、新しければ再読み込み。既定 ON・設定は保存されない | `FormMain.cs:1520–1558`、Designer:3575–3577, 3606–3607 |
| Language | 各言語名（自言語表記） | 選ぶと確認のうえ再起動して切り替え。言語はレジストリ `Culture` に保存 | `FormMain.cs:164–240` |
| ヘルプ | オンラインマニュアル | F1 と同じ URL（現在のモードのページ）を既定ブラウザで開く | `FormMain.cs:252–257`, `47–52`, `863–864` |

- F1／オンラインマニュアルの URL は、英語 UI だと各モードのページで 404 になる（[疑問点 Q-2](03-implementation-questions.md)）。

### 2.2 モード選択と共通欄

- 画面上部のラジオボタン: **ダイヤモンドラマン**／**ルビー蛍光**／**EOS**（`radioButtonMode_CheckedChanged` `FormMain.cs:810–870`）。
  - ダイヤモンドラマン: 上下 2 段のグラフ（上: 元のスペクトル、下: 一次微分）と「ラマンエッジからの圧力計算」グループ。軸単位表示 `cm^-1`。
  - ルビー蛍光: 下段のグラフのみ（上段は高さ 0）と「ルビー蛍光からの圧力計算」グループ。軸単位表示 `nm`。
  - EOS: グラフを隠し、物質ごとの入力欄を並べたパネルを表示。
- **小数点以下の桁数**（0〜10、既定 4）: 全モードの圧力出力欄の表示桁数（`FormMain.cs:1563–1611`、Designer:874–887）。
- 既定のモード: Designer ではダイヤモンドラマンだが、起動時にレジストリ値で上書きされる。初回起動時はルビー蛍光になる可能性がある（[Q-5](03-implementation-questions.md)）。

## 3. スペクトルの読み込み

- 方法: ファイル → 読み込み、またはウィンドウへ **1 ファイル** をドラッグ＆ドロップ（複数ファイルは無視、`FormMain.cs:1418–1435`）。
- 形式（`readFile` / `ConvertUnknownFileToProfileData`, `FormMain.cs:262–417`）:
  1. UTF-8 で全行を読み、空行を除く。
  2. 区切り文字を「`,`」「空白」「タブ」の順に試す。各試行で
     - 数値が 2 個以上あり、かつ同じ個数の行が **100 行以上連続** するブロックを選ぶ（最長のもの）。
     - その中で、正の一定刻み（刻み > 1×10⁻⁵）で **100 点以上** 続く列を X とする。
     - 残りの列のうち分散が最大の列を Y とする。
  3. 3 つとも失敗したら、カンマ区切りの簡易読み込み: 3 列以上の行は 1 列目の文字数が 3 以上なら (1 列目, 3 列目)、そうでなければ (2 列目, 3 列目)。2 列の行は (1 列目, 2 列目)。
  - 拡張子は問わない。X の単位はルビーモードでは nm、ダイヤモンドモードでは cm⁻¹ として扱われる（変換はしない）。
- 読み込み後、平滑化 → （ダイヤのみ）微分 → ピークフィット → 圧力計算が自動で走る。

## 4. グラフ操作（Crystallography.Controls の GraphControl 共通）

実装: `GraphControl.pictureBox_MouseDown`（`GraphControl.cs:1604–1651`）、`pictureBox_MouseUp`（`1755–1818`）。

| 操作 | 動作 |
|---|---|
| **右**ボタンでドラッグ | 囲んだ範囲に拡大（X・Y とも） |
| 右ボタンで小さく（3 px 未満）ドラッグ／右クリック | 1.5 倍に縮小 |
| 中ボタンでドラッグ | 平行移動 |
| 左ボタンでダブルクリック | 表示中プロファイルを「X\tY」のテキストでクリップボードへコピー。**ルビーモードでは、その前にクリック位置 ±Fitting Range の点にローレンツ関数を当てはめ、R1 欄を上書きする**（`FormMain.cs:617–702`） |

- 表示範囲が変わるたびにピークフィットがやり直される（`FormMain.cs:873–891`）。**フィットの対象は表示範囲内の点だけ** なので、目的のピークが表示範囲内の最大（ルビー）／微分の最小（ダイヤ）になるように拡大しておく。
- ダイヤモンドモードでは上下のグラフの X 範囲が連動する。

## 5. 平滑化

<!-- screenshot: ダイヤモンドラマンモードの平滑化コントロール（元のスペクトル側と一次微分側） -->

| 欄 | 既定 | 意味 | 実装 |
|---|---|---|---|
| 移動平均 | 10（最小 1） | n 点の単純移動平均。X も平均するので出力は N−n+1 点 | `FormMain.cs:427–441`, `476–490` |
| ガウスぼかし σ | 8 | 重み $\exp(-\lfloor j^2/\sigma^2\rfloor)$、j = −50…50（データ点単位）。0 なら平滑化なし。整数除算のため重みは階段状（[Q-4](03-implementation-questions.md)） | `FormMain.cs:442–462`, `491–509` |

- 2 つは **排他的**。数値欄をクリック（MouseDown）した方が有効になり、他方は読み取り専用（灰色）になる（`FormMain.cs:720–775`）。既定は両モードともガウスぼかしが有効（Designer:384, 398）。
- ダイヤモンドモードでは「元のスペクトル」用と「一次微分」用に別々の設定がある。ルビーモードでは下段（ルビー蛍光）用の設定（Designer 上の「一次微分」側の欄）が効く。

## 6. ピークフィットと「フィッティング情報」

実装: `SearchBandEdge`（`FormMain.cs:516–614`）、`FittingPeak.FitMultiPeaksThread`（`Crystallography/PeakSearch.cs:407–`）。

| | ルビー蛍光 | ダイヤモンドラマン |
|---|---|---|
| 対象プロファイル | 平滑化後のスペクトル | 平滑化後スペクトルの一次微分（前進差分）をさらに平滑化し、**符号反転** したもの |
| 初期中心 | 表示範囲内の最大点 = R1、R2 は R1 − 1.5 nm | 表示範囲内の微分最小点 |
| モデル | pseudo-Voigt 2 本（R1, R2）＋ 1 次バックグラウンド | pseudo-Voigt 1 本 ＋ 1 次バックグラウンド |
| 当てはめに使う点 | [R1 − 1.5 − range, R1 + range] | [中心 − range, 中心 + range] |
| 初期 FWHM | range / 2 | range |
| 結果の行き先 | R1 欄（`numericBoxRubyR1`） | ν 欄（`textBoxDiamondRamanNu`） |

- **フィッティング範囲**（range）: 整数、最小 1、刻み 1（Designer:798–802）。値はモードごとに保持され（コード上の初期値: ルビー 3、ダイヤ 6。`FormMain.cs:32–33`）、レジストリに保存される。単位は軸の単位（nm / cm⁻¹）。
- **フィッティング情報** 欄: 各ピークの `X`（中心）、`FWHM`、`η`（pseudo-Voigt の混合比）、`Background Y = 切片 + 傾き * X` を表示（`FormMain.cs:552`, `589–590`）。
- グラフ上の曲線: ダイヤ = 赤（フィット）、ルビー = 薄緑（R1）、水色（R2）、桃色（R1+R2+R2 のバックグラウンド）。

## 7. CSV エクスポート

ファイル → CSV形式でエクスポート（`FormMain.cs:1448–1489`）。下段グラフにデータがあるときだけ動作する。

- ルビー: 見出し `X, Y (original), Y (smoothed),,X(R1),Y(R1),Y(R1_Bg),,X (R2),Y(R2), Y(R2_Bg)`
- ダイヤ: 見出し `X, Y (original), Y (smoothed),, X(Edge),Y(Edge)`
- データ行の X は平滑化後の X。移動平均を使っていると「Y (original)」が (n−1)/2 点ずれる（[Q-12](03-implementation-questions.md)）。
- Y(Edge) は符号反転した微分に対するフィット曲線（バックグラウンドを差し引かずに表示用に変換したもの）。

## 8. ルビー蛍光モード

<!-- screenshot: ルビー蛍光モードの右側パネル（参照条件・測定条件・温度依存性・圧力計算の各グループ） -->

### 8.1 操作手順

1. スペクトルを読み込む（または **R1** 欄に R1 波長 [nm] を直接入力）。読み込んだ場合は §6 のフィットで R1 欄が更新される。
2. **測定条件** グループの **R1₀=R1(T,P=0)** に、測定温度でのゼロ圧 R1 を入れる（既定 694.65 nm）。
   - 測定温度が参照温度と違う場合は、**温度依存性 (Ragan et al., 1992)** を使う: 参照条件を入れ（3.）、測定条件の **T =** を入れ、**Ragan の式から計算** をチェックすると R1₀ が自動計算される（欄は読み取り専用になる）。
3. **参照条件** グループ: ゼロ圧で測った R1 とその温度 **R1(T=  , P=0)** を入れる（既定 694.25 nm, 297.15 K）。**現在の R1 を設定** で、いま R1 欄にある値を参照 R1 にコピーできる（ゼロ圧のスペクトルを読み込んだ状態で押す）。参照条件が変わるたびに Ragan の定数（温度依存性グループの数値欄）が再計算される。
4. **圧力計算** グループに 4 式の圧力（GPa）が表示される。各式の係数 A は編集可能（指数は固定）。

### 8.2 設定項目

| UI 表示（日本語 UI） | 内部名 | 既定 | 意味 | 実装 |
|---|---|---|---|---|
| R1 | `numericBoxRubyR1` | 694.65 nm | 測定 R1。フィットで上書きされる | Designer:1337–1350 |
| 参照条件: R1(T=  , P=0) = | `numericBoxRubyRefR1` | 694.25 nm | 参照温度でのゼロ圧 R1 | Designer:1318–1331 |
| 参照条件: 温度（同じ行の右側の欄） | `numericBoxRubyRefT` | 297.15 K | 参照温度 | Designer:1300–1312 |
| 現在の R1 を設定 | `buttonRubyRefR1Set` | — | R1 → 参照 R1 にコピー | `FormMain.cs:909` |
| 測定条件: T = | `numericBoxRubyT` | 297.15 K（既定で無効） | 測定温度 | Designer:1222–1236 |
| 参照条件と同じ | `checkBoxRubyTemeratureSameAsRef` | ON | ON にした瞬間に参照温度を測定温度へコピーし、測定温度欄を無効化。以後は追従しない（[Q-6](03-implementation-questions.md)） | `FormMain.cs:925–930` |
| R1₀=R1(T,P=0) | `numericBoxRubyR1_0` | 694.65 nm | 圧力計算に使うゼロ圧 R1 | Designer:1256–1269 |
| Ragan の式から計算 | `checkBoxRubyR1_0CalculatedFromRagan` | OFF | ON で R1₀ を Ragan 式から計算 | `FormMain.cs:937–979` |
| R1(T, P=0) = ( [c] + 4.49×10⁻²×T − … )⁻¹ ×10⁷ [nm] | `numericBoxRubyRagan` | 14423 | Ragan 式の定数 c。参照条件から自動計算されるが手入力も可 | `FormMain.cs:917–922` |
| 温度単位 K / ℃ | `radioButtonTempUnitK/C` | K（Designer）。初回起動時は ℃ になる可能性（[Q-5](03-implementation-questions.md)） | ℃ のとき内部で +273.15。欄の下限も 0 K / −273.15 ℃ に切替 | `FormMain.cs:1019–1035` |
| P= [A] f(x, 5)= など | `numericBoxMaoA` ほか | 1904 / 1904 / 1904 / 1870 | 各式の係数 A（GPa） | Designer:1000–1069 |

## 9. ダイヤモンドラマンモード

<!-- screenshot: ダイヤモンドラマンモード（上: 元のスペクトル、下: 一次微分と赤いフィット曲線、右: ラマンエッジからの圧力計算） -->

### 9.1 操作手順

1. ダイヤモンドアンビルのラマンスペクトルを読み込む。
2. 下段（一次微分）で、高波数側エッジに対応する微分の極小が表示範囲内の最小になるように右ドラッグで拡大する。フィットされたエッジ位置が **ν** 欄に入る（直接入力も可）。
3. **ν0**（ゼロ圧のエッジ波数、既定 1334 cm⁻¹）を確認する。
4. 4 式の圧力（GPa）が表示される。

### 9.2 設定項目

| UI 表示 | 内部名 | 既定 | 意味 | 実装 |
|---|---|---|---|---|
| ν | `textBoxDiamondRamanNu` | 0 | 測定エッジ波数 [cm⁻¹] | Designer:1472 |
| ν0 | `textBoxDiamondRamanNu0` | 1334 | ゼロ圧エッジ波数。Akahama (2006) と Fratanduono の 2 式で使用。レジストリに保存 | Designer:1453、`FormMain.cs:118, 147` |
| Akahama (2004): P = [A] + [B] ν+ [C] E-4 ν^2= | `textBoxAkahama2004A/B/C` | 66.9 / -0.5281 / 3.585 | ν の 2 次式（ν は絶対値） | Designer:423–443 |
| Akahama (2006): P = K0 [547] × Δν/ν0 × [1+1/2 (K0' [3.75] -1)*Δν/ν0]= | `textBoxAkahama2006K0/K0Prime` | 547 / 3.75 | | Designer:1491, 1501 |
| Fratanduono+ (2021, <300GPa) | — | 固定 | ラベルの係数 503.77 と計算の係数 530.77 が不一致（[Q-1](03-implementation-questions.md)） | `FormMain.cs:794` |
| Fratanduono+ (2021, >200GPa) | — | 固定 | 200 GPa 未満では無意味な値になる 2 次式 | `FormMain.cs:796` |

- 係数欄・ν 欄の入力が数値として解釈できないと、そのたびに「適切な数値を入れてください」が出る（[Q-8](03-implementation-questions.md)）。

## 10. EOS モード

<!-- screenshot: EOS モード（金・白金・NaCl B1・NaCl B2・ペリクレース・コランダム・Ar・Re の各グループ） -->

### 10.1 操作手順

1. 画面上部の **Temperature**（測定温度、K、既定 300）と **T₀**（基準温度、K、既定 300）を入れる。
2. 使う物質のグループで、基準格子定数 **a₀**（Å）と測定格子定数 **a**（Å）を入れる。コランダムと Re は **V₀**・**V**（Å³）。
3. 入力するたびに全物質・全スケールの圧力（GPa）が再計算される（`textBox_TextChanged` `FormMain.cs:1043–1055`）。

### 10.2 注意（実装上の約束事）

- **T₀ を使うのは Pt Matsui (09) だけ**（表示中のスケールでは）。他の熱的スケールは基準温度 300 K（Ar Jephcoat は 4 K）がコードに固定されている。a₀/V₀ は 300 K の値を入れる前提。
- 温度を無視するスケール: Au/Pt Fratanduono (21)、NaCl B2 の Sata (02)・Sakai+(11)、Ar Ross et al. (86)、Re Anz・Sakai.・Dub.。
- UI の a₀/V₀ を使わないスケール: NaCl B2 の全スケール（a₀ 欄は無効）、Ar の両スケール（a₀ 欄は未使用で既定値も未設定）、Re の Anz・Sakai.・Dub.（固定 V₀）。
- 数表スケール（Jamieson, Tsuchiya, Yokoo, Brown, Zha, Ross）は表の範囲外を 1 次外挿する。
- Mo（Huang+(16)MGD, Zhao+(00)）と Pb（Strässle+(14)）のグループは非表示（`Visible = false`）。計算は内部で行われている。
- 詳細は [圧力スケール一覧](02-pressure-scales.md)。

### 10.3 物質ごとの入力欄と既定値

| グループ（日本語 UI） | 入力 | 既定値 | 表示されるスケール |
|---|---|---|---|
| 金 | a₀, a [Å] | 4.07825 / 4.07825 | Jamieson (82), Anderson (89), Sim (02), Tsuchiya (03), Yokoo (09), Fratanduono (21) |
| 白金 | a₀, a [Å] | 3.9231 / 3.9231 | Jamieson (82), Holmes (89), Matsui (09), Yokoo (09), Fratanduono (21) |
| NaCl B1 | a₀, a [Å] | 5.639 / 5.639 | Brown (99), Matsui (12) |
| NaCl B2 | a [Å]（a₀ は無効） | a = 2.93 | Sata (02) (Pt), Sata (02) (Mg), Ueda+(08), Sakai+(11) BM, Sakai+(11) Vinet |
| ペリクレース | a₀, a [Å] | 4.2112 / 4.2112 | Jackson (98), Dewaele (00), Aizawa (06), Tange (09) Vinet, Tange (09) BM |
| コランダム | V₀, V [Å³] | 255.89472 / 255.89472 | Dubrovinsky (98) |
| Ar | a [Å]（a₀ は未使用） | a = 4.0786 | Ross et al. (86), Jephcoat (98) |
| Re | V₀, V [Å³] | 29.42795 / 29.42795 | Zha et al. (04), Anz (##), Sakai. (##), Dub. (##) |
| Mo（非表示） | V₀, V [Å³] | 31.14 / 31.14 | Huang+(16)MGD, Zhao+(00) |
| Pb（非表示） | a₀, a [Å] | 4.95059 / 4.95059 | Strässle+(14) |

既定値の出典: Designer:1845, 1870, 2125, 2150, 2272, 2297, 2508, 2754, 2790, 2853, 2889, 2978, 3197, 3233, 3334, 3370, 3434, 3470。

## 11. 設定の保存（レジストリ）

キー: `HKEY_CURRENT_USER\Software\Crystallography\PressureCalculator`（`FormMain.cs:96–157`, `Program.cs:40–47`）。終了時に保存（`--capture` 実行時は保存しない）。

| 値の名前 | 内容 |
|---|---|
| `formMainWidth`, `formMainHeight`, `formMainLocationX`, `formMainLocationY` | ウィンドウの大きさと位置 |
| `radioButtonDiamondRaman.Checked`, `radioButtonEOS.Checked`, `radioButtonRubyFluorescence.Checked` | モード |
| `fittingRangeRuby`, `fittingRangeDiamond` | モード別のフィッティング範囲 |
| `textBoxAkahama2006Nu0.Text` | ν0 |
| `numericBoxRubyRefR1.Text`, `numericBoxRubyRefT.Text` | 参照 R1・参照温度 |
| `textBoxMaoRamda0.Text` | 参照 R1（同じ値を別名でも保存。読み込みは `numericBoxRubyR1_0.Text` という別名を見る: [Q-5](03-implementation-questions.md)） |
| `radioButtonTempUnitK.Checked`, `radioButtonTempUnitC.Checked` | 温度単位 |
| `Culture` | UI 言語（`Program.Main` が起動時に読む） |

保存されないもの: 測定条件の R1₀・測定温度、各式の係数（A, Akahama の係数）、EOS の全入力、平滑化設定、小数点以下の桁数、ファイル監視の ON/OFF。

## 12. このアプリに無い機能

- 圧力から格子定数・体積を求める逆算、圧力単位の換算、複数スケールの一括出力・保存は UI に無い（`EOS.InverseThirdBirchMurnaghan` はライブラリにあるが未使用）。
- EOS の温度単位切替（℃）は無い（K のみ）。

## 要確認事項

- R-1-1 §3 のファイル形式判定（100 行以上の連続ブロック、X 列の判定条件 `Math.Abs(... ) < 10`）を利用者向けにどこまで書くか。
- R-1-2 §5 「ルビーモードでは下段用（一次微分側）の平滑化欄が効く」は `CalcSmoothingAndDifferentiation` の読解による（ルビーモードの下段 = 元データを下段側の設定で平滑化）。上段の平滑化結果がルビーモードで使われるのはエクスポートの「Y (smoothed)」列だけ。画面上でどの欄が見えているかは実機で要確認。
- R-1-3 初回起動時の既定（モード・温度単位・参照 R1）は実機で要確認（[Q-5](03-implementation-questions.md)）。
- R-1-4 Ar の a₀ 欄に既定値が設定されていない（0 表示と思われる）。実機で要確認。
- R-1-5 スクリーンショットは Windows 上で `--capture` を使って撮り直す必要がある（既存の `docs/src/assets/cap-*-auto/` の 3 枚は流用可能か）。
- R-1-6 「参照条件」の温度欄は HeaderText が「R1(T=                , P=0) = 」で、温度欄が重ねて配置されていると思われる。レイアウトの実際の見え方は要確認。
