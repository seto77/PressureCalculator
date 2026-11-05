本稿はコードから Claude が起こした下書き（2026-11-05）。著者未査読。

# 既存マニュアルと現行コードの突き合わせ

## 棚卸しした既存ドキュメント

| 文書 | 場所 | 状態 |
|---|---|---|
| オンラインマニュアル（MkDocs Material、11 言語 × 5 ページ） | `docs/src/{index.md, en/, ja/, de/, …}/`、設定 `docs/mkdocs.yml`。`pages.yml` で GitHub Pages に公開 | 本稿の比較対象（英語 `docs/src/en/*.md` と `docs/src/index.md`、日本語 `docs/src/ja/*.md` を読んだ。他 9 言語は同内容の翻訳と仮定して未読） |
| ポータブル版 README | `README-PORTABLE.txt` | インストール手順のみ。コードとの食い違いなし |
| 版履歴 | `PressureCalculator/Version.cs` の `History` | 最終行 ver1.009 (2025/10/16) |
| 参考文献 PDF | `references/`（Ragan et al 1992 JAP、Akahama 2006、Shen et al HPR 2020、Syassen 2008 HPR） | 式の照合に使用（[圧力スケール一覧](02-pressure-scales.md)） |
| GitHub Wiki | `https://github.com/seto77/PressureCalculator.wiki.git` | clone 不可（認証要求。Wiki 未作成と思われる） |
| リポジトリ直下 README.md | — | 存在しない |

対象コード: PressureCalculator `master` @ `1676a57`、Crystallography @ `bd3caf4`、Crystallography.Controls @ `eca409d`。

## A. 記述がコードと食い違う箇所

「マニュアル」欄は英語版（`docs/src/en/…`）の記述。日本語版も同じ内容。

| # | 重要度 | マニュアルの記述 | 現行コード | 根拠 |
|---|---|---|---|---|
| A-1 | 高 | 2-diamond-raman: Fratanduono et al. (2021, <300 GPa) **P = 503.77 x + 753.83 x²** | 計算は **530.77** x + 753.83 x²（UI ラベルは 503.77 でマニュアルと同じ） | `FormMain.cs:794`、Designer:1544。[疑問点 Q-1](03-implementation-questions.md) |
| A-2 | 高 | 3-equation-of-states: 物質一覧に **Mo**（Zhao+ (2000), Huang+ (2016) MGD）と **Pb**（Strässle+ (2014)）を掲載 | Mo・Pb のグループは **非表示**（計算はされるが画面に出ない） | `groupBoxMo.Visible = false` Designer:3251、`groupBoxPb.Visible = false` Designer:3386 |
| A-3 | 高 | index / オンラインヘルプ: 「F1（または Help → Online manual）で現在のモードのページが開く」 | 英語 UI では各モードのページ URL が 404（日本語など他言語は正常） | `FormMain.cs:47–52`。2026-11-05 実測。[Q-2](03-implementation-questions.md) |
| A-4 | 中 | 4-spectra-and-fitting / Graphs: 「**左**ボタンで矩形ドラッグするとズームイン、ごく小さなドラッグで段階的にズームアウト」 | ズームは **右**ボタンのドラッグ。右ボタンの小さな（3 px 未満）ドラッグで 1.5 倍に縮小。中ボタンドラッグで平行移動。左ボタン単クリックは縦線の選択 | `Crystallography.Controls/GraphControl.cs:1604–1651`（MouseDown）、`1755–1818`（MouseUp） |
| A-5 | 中 | 4-spectra-and-fitting / Graphs: 「ダブルクリックで表示中のプロファイルをクリップボードへコピー」 | ダイヤモンドモードはそのとおり。**ルビーモードでは、クリック位置を中心にローレンツ関数を当てはめて R1 欄を上書きしたうえで** コピーも行う | `FormMain.cs:617–702`、`GraphControl.cs:1636–1648`。[Q-14](03-implementation-questions.md) |
| A-6 | 中 | 4-spectra-and-fitting / Smoothing: 「2 種類の **独立な** フィルタ（Running Average と Gaussian blur σ）」 | 2 つは **排他的**。クリックした方が有効になり、他方は ReadOnly になる。既定はガウスぼかし（σ = 8）が有効。σ の単位はデータ点数で、重みは整数除算のため階段状 | `FormMain.cs:421–510`, `720–775`、Designer:384, 398, 523, 580。[Q-4](03-implementation-questions.md) |
| A-7 | 中 | 4-spectra-and-fitting / Fitting: 「Fitting Range: ピーク（または微分極小）の周囲でフィッティングに使う領域の **幅**」 | フィット対象は **現在のグラフ表示範囲（X）内の点** に限られ、初期中心は表示範囲内の最大点（ルビー）／微分の最小点（ダイヤ）。Fitting Range は初期中心からの **半幅**（ルビーは R1 中心 −1.5 nm の R2 初期位置も含めて [R1−1.5−range, R1+range]）で、初期 FWHM にも使われる（ルビー range/2、ダイヤ range）。整数（最小 1、刻み 1） | `FormMain.cs:516–614`（`SearchBandEdge`）、`Crystallography/PeakSearch.cs:53–59, 440–450` |
| A-8 | 中 | 3-equation-of-states / Workflow 1: 「測定温度と参照温度 T₀ を入力。熱的状態方程式ではこれらが使われる」 | **T₀ を使うのは Pt Matsui (09) と Pb（非表示）だけ**。他の熱的スケールは基準温度 300 K（Ar Jephcoat は 4 K）がコード内で固定 | [スケール一覧 §0.2](02-pressure-scales.md) |
| A-9 | 中 | 3-equation-of-states / Workflow 2: 「各物質について a₀ と a を入力」 | NaCl B2 の a₀ 欄は無効化され未使用。Ar の a₀ は未使用。Re の V₀ は Zha だけが使い、Anz/Sakai/Dub は固定 V₀ | Designer:2526、`EOS.cs:438–477, 1367–1396`、`FormMain.cs:1240–1244` |
| A-10 | 中 | 3-equation-of-states / Workflow 2: 「コランダムとレニウムは体積 V₀・V を入力」 | Mo（非表示）も体積入力。記述自体は正しいが不完全 | Designer:3318, 3354 |
| A-11 | 中 | 3-equation-of-states / 表: Re「Zha et al. (2004) **and others**」 | 実際は Zha et al. (04)、Anz (##)、Sakai. (##)、Dub. (##) の 4 欄。後 3 者は UI でも年が「##」 | Designer:3103, 3138, 3173, 3209 |
| A-12 | 中 | 3-equation-of-states / note: 「室温スケールでは差は無視される」（Workflow 1） | 温度を無視するのは Au/Pt Fratanduono、NaCl B2 の Sata/Sakai、Ar Ross、Re Anz/Sakai/Dub。表の物質ごとにどれが熱的かの記載が無い | [スケール一覧 §3](02-pressure-scales.md) |
| A-13 | 低 | 1-ruby-fluorescence: 式 $P=\frac{A}{y}[(R1/R1_0)^y-1]$ と表「Mao (1978) A=1904, y=5 / Mao-quasi y=7.665 / Mao-hydro y=7.715」「係数は編集可能」 | 編集可能なのは A だけで、y（指数）と Shen の 5.63 はコード固定。A の既定値は 3 式とも 1904、Shen は 1870 | `FormMain.cs:1006–1016`、Designer:1012, 1031, 1050, 1069 |
| A-14 | 低 | 1-ruby-fluorescence / Workflow 3: 「参照条件（ゼロ圧力での R1 波長 R1₀）を設定。Set the current R1 で現在の R1 を参照ボックスにコピー」 | 「参照条件」は **R1(T_ref, P=0) と T_ref の組** で、Ragan 式の定数 c を較正するためのもの。圧力計算に使う R1₀ は **「測定条件」グループ** の `R1₀=R1(T,P=0)` 欄（既定 694.65 nm）で、「Ragan の式から計算」を ON にしたときだけ参照条件から計算される。OFF のときは参照条件は圧力値に影響しない | `FormMain.cs:909–1017`、Designer:1205, 1259, 1269, 1282 |
| A-15 | 低 | 1-ruby-fluorescence / 温度補正: 「Same as reference: 測定温度を参照温度と同じとみなす」 | チェックを入れた瞬間に値をコピーするだけで、その後参照温度を変えても追従しない | [Q-6](03-implementation-questions.md) |
| A-16 | 低 | 1-ruby-fluorescence / 温度補正: 適用範囲 50-600 K | UI ラベルと一致。ただし同梱 Ragan PDF のフィット範囲は 15–600 K | Designer:1173、`references/Ragan et al 1992JAP.pdf` |
| A-17 | 低 | 2-diamond-raman / Workflow 2: 「高波数側エッジは微分の極小として現れ、Fitting Range 内でフィッティング」 | 微分の符号を反転したプロファイルに pseudo-Voigt＋1 次バックグラウンドを当てはめ、その中心をエッジ位置とする。微分は平滑化後のスペクトルの前進差分（隣接 2 点の中点に置く） | `FormMain.cs:530–565`、`Crystallography/Profile.cs:329–348` |
| A-18 | 低 | 2-diamond-raman / 表: Akahama (2004)「エッジ位置の多項式」 | $P = A + B\nu + C\times10^{-4}\nu^2$（ν は Δν ではなく絶対値、ν₀ 不使用）。既定 A=66.9, B=-0.5281, C=3.585 | `FormMain.cs:789`、Designer:423–443 |
| A-19 | 低 | 4-spectra-and-fitting / Loading: 「最初の数値列を横軸、次の列を強度」 | 区切り文字を `,`→空白→タブの順に試し、(1) 数値が 2 個以上・同じ個数で 100 行以上連続するブロックを選び、(2) 一定の正の刻みで 100 点以上続く列を X、(3) 残りの列で分散が最大の列を Y とする。どれも失敗したらカンマ区切りの簡易読み込み（3 列以上なら 1 列目の文字数が 3 以上なら (1列目, 3列目)、そうでなければ (2列目, 3列目)）。文字コードは UTF-8 固定 | `FormMain.cs:262–417`（`readFile`, `ConvertUnknownFileToProfileData`） |
| A-20 | 低 | 4-spectra-and-fitting / Export: 「表示中のスペクトル（元データ・平滑化・フィット曲線）」 | 出力列は「X, Y (original), Y (smoothed),, X(R1), Y(R1), Y(R1_Bg),, X (R2), Y(R2), Y(R2_Bg)」（ルビー）または「X, Y (original), Y (smoothed),, X(Edge), Y(Edge)」（ダイヤ）。移動平均時は列ずれあり | `FormMain.cs:1448–1489`。[Q-12](03-implementation-questions.md) |
| A-21 | 低 | index / Installation: MSI は .NET Desktop Runtime 10 が必要 | `TargetFramework` は `net10.0-windows` で一致 | `PressureCalculator.csproj:7` |
| A-22 | 低 | index: 設定は `HKEY_CURRENT_USER\Software\Crystallography\PressureCalculator` | 一致（`FormMain.cs:100, 134`、`Program.cs`） | — |

## B. コードにあってマニュアルに無い機能・約束事

| # | 内容 | 根拠 |
|---|---|---|
| B-1 | **小数点以下の桁数**（`numericBoxDecimalPlaces`、0〜10、既定 4）はルビー・ダイヤ・EOS の全出力欄に効く（マニュアルではスペクトル章にだけ記載） | `FormMain.cs:1563–1611`、Designer:874–887 |
| B-2 | 各ルビー式の **A 値**（既定 1904/1904/1904/1870）と Ragan 定数（既定 14423、参照条件から自動計算）が編集可能 | Designer:1000–1069, 1178–1191 |
| B-3 | **温度単位 K/℃** の切替はルビーモードだけ（EOS は K のみ）。℃ 選択時は内部で +273.15、下限も −273.15 ℃／0 K に切替 | `FormMain.cs:1019–1035` |
| B-4 | 初回起動時の既定（読解による推定）: ルビー蛍光モード・℃・参照 R1 = 297.15 nm になりうる | [Q-5](03-implementation-questions.md) |
| B-5 | 保存される設定の一覧（ウィンドウ位置・大きさ、モード、Fitting Range（ルビー/ダイヤ別）、ν₀、参照 R1・参照 T、温度単位、UI 言語）。測定条件の R1₀・各係数・EOS の入力値・平滑化設定・ファイル監視は保存されない | `FormMain.cs:132–157` |
| B-6 | Fitting Range はモードごとに別の値を保持（コード上の初期値 ルビー 3 nm、ダイヤ 6 cm⁻¹。Designer の 4 はモード切替時に上書きされる） | `FormMain.cs:32–33, 816, 842, 893–900` |
| B-7 | ファイルを 1 つドロップしたときだけ読み込む（複数ファイルは無視） | `FormMain.cs:1418–1435` |
| B-8 | ファイル監視は 1 秒ごと、更新時刻で判定。設定は保存されず起動時は常に ON | `FormMain.cs:1520–1558`、Designer:3575, 3606–3607 |
| B-9 | 非対話モード: `--smoke [出力ファイル]`（アーキテクチャと実行フォルダを書き出して終了）、`--capture <出力フォルダ> <カルチャ>`（3 モードのスクリーンショットを PNG 保存して終了。設定は保存しない） | `Program.cs:16–34`、`GuiCapture.cs` |
| B-10 | ウィンドウタイトルに版番号と日付（`Version.VersionAndDate`）、ファイル読み込み後はファイル名を表示 | `FormMain.cs:58`, `302` |
| B-11 | 逆算（P → a/V）や圧力単位換算の機能は **無い**（ライブラリに `EOS.InverseThirdBirchMurnaghan` はあるが UI から未使用） | `EOS.cs:287–309`、FormMain 全体 |
| B-12 | 各スケールの式・パラメータ値・基準温度・V₀ の扱い（マニュアルには式の一部とスケール名のみ） | [スケール一覧](02-pressure-scales.md) |

## C. 版履歴（Version.cs）とのずれ

- `Version.History` の最新は「ver1.009 (2025/10/16) Improved the EOS functions.」（ver1.008 も同日・同文）。`AssemblyVersion`/`FileVersion` も `2025.10.16.0829`（`PressureCalculator.csproj:9–10`）。
- 一方、コード中には 2026 年の変更コメント（`260703Cl`〜`260805Cl`: FormBase 継承・F1 ヘルプ・11 言語化・`--smoke`/`--capture`・DPI 設定・版表記の空白挿入など）が多数あり、履歴に記載が無い。マニュアル（index の 11 言語・F1 ヘルプ・arm64 版）はこれらを前提に書かれている。
- ver1.002 の「Added two diamond Raman equations (Fratanduono et al., 2021)」はマニュアルに反映済み（ただし A-1 の係数問題あり）。ver1.004 の「Fitting information is now displayed」も反映済み。

## 要確認事項

- R-0-1 A-1（Fratanduono 係数）はコード・ラベル・マニュアルのどれを正とするか。
- R-0-2 A-2: Mo・Pb を UI に出す予定か。出さないならマニュアルから外すか「非表示」と注記するか。
- R-0-3 A-4/A-5 のマウス操作は Crystallography.Controls の GraphControl 共通仕様なので、兄弟アプリのマニュアルと合わせて直すか。
- R-0-4 他 9 言語のマニュアルが英語・日本語と同内容か（本稿では未読）。
- R-0-5 Version.cs 履歴に 2026 年の変更を追記するか（版番号の更新はコード変更になるので本作業では触れていない）。
- R-0-6 Wiki の有無（clone は認証エラー）。
