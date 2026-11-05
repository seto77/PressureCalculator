using Crystallography;
using MathNet.Numerics;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks; // 261105Cl 追加: timer_Tick の await Task.Delay 用
using System.Windows.Forms;

namespace PressureCalculator
{





    /// <summary>
    /// Form1 の概要の説明です。
    /// </summary>
    //260703Cl 変更: Form → FormBase 継承 (UiFont 一元適用 + F1 オンラインヘルプ + CodeLocalizer による多言語化。兄弟アプリ同型)
    //旧: public partial class FormMain : Form
    public partial class FormMain : Crystallography.Controls.FormBase
    {

        private DateTime lastWriteTime;
        public string currentFileName = "";

        private double fittingRangeDiamond = 6;
        private double fittingRangeRuby = 3;

        PeakFunction[] PF;

        string StringFormat => $"f{numericBoxDecimalPlaces.ValueInteger}";


        #region ロード、クローズ
        public FormMain()
        {
            InitializeComponent();

            //260703Cl 追加: F1 オンラインヘルプの URL 解決ロジックを登録 (起動時に 1 回、IPAnalyzer/FormMain と同方式)。
            //  マニュアルは mkdocs-static-i18n (folder mode): en はサイトルート直下、他言語は /{lang}/ 配下。
            //261105Cl 変更: 上の説明は誤り。docs/mkdocs.yml は static-i18n を使わず明示 nav で、英語の本文ページは en/ 配下 (en/1-ruby-fluorescence.md 等)。
            //  サイトルート直下にあるのは英語のホーム (index.md) だけで、/en/ 自体にはホームが無い。
            //  旧コードは英語で /{HelpPage}/ を開いて 404 だった (2026-11-05 に curl で確認: /1-ruby-fluorescence/ 404, /en/1-ruby-fluorescence/ 200, /en/ 404, / 200)。
            //  よって ページあり = 全言語 /{lang}/{HelpPage}/、ページ無し (ホーム) = 英語はルート・他言語は /{lang}/。
            Crystallography.Controls.FormBase.HelpUrlResolver = f =>
            {
                var lang = Crystallography.SupportedCultures.Current.HelpCulture;
                return string.IsNullOrEmpty(f.HelpPage)
                    ? (lang == "en" ? "https://seto77.github.io/PressureCalculator/" : $"https://seto77.github.io/PressureCalculator/{lang}/")
                    //: (lang == "en" ? $"https://seto77.github.io/PressureCalculator/{f.HelpPage}/" : $"https://seto77.github.io/PressureCalculator/{lang}/{f.HelpPage}/");
                    : $"https://seto77.github.io/PressureCalculator/{lang}/{f.HelpPage}/"; // 261105Cl 変更: en も /en/{HelpPage}/
            };
        }

        private void FormMain_Load(object sender, System.EventArgs e)
        {
            this.Text = Version.Software + "  " + Version.VersionAndDate;

            PopulateLanguageMenu();//260703Cl 追加: 言語メニューを SupportedCultures から動的生成
            UpdateLanguageMenuChecks(Crystallography.SupportedCultures.Current.Name);//260703Cl 追加

            //260704Cl 追加: NumericBox.HeaderText は CodeLocalizer (Control.Text/HeaderText=DataGridView限定) の対象外のため実行時 Loc で差し替える
            numericBoxDecimalPlaces.HeaderText = Crystallography.Localization.Loc(
                en: "Number of decimal places", ja: "小数点以下の桁数", de: "Anzahl der Dezimalstellen", fr: "Nombre de décimales",
                es: "Número de decimales", pt: "Número de casas decimais", it: "Numero di cifre decimali",
                ru: "Число десятичных знаков", zhHans: "小数位数", zhHant: "小數位數", ko: "소수점 이하 자릿수");

            //261105Cl 追加: EOS 適用温度範囲外の凡例のツールチップ (ToolTip は CodeLocalizer の対象外のため実行時 Loc で差し替える。凡例の文言は PressureCalculatorLocalizationData)
            toolTip.SetToolTip(labelEOSValidityWarning, Crystallography.Localization.Loc( // 261105Cl 追加
                en: "Pressures shown in red are computed at a temperature (Temperature, or also T₀ for Pb) outside the range where that scale is valid. The value is still shown (extrapolated) and is not changed; hover over it for the valid range.",
                ja: "赤で表示した圧力は、そのスケールの適用範囲外の温度 (Temperature、Pb では T₀ も) で計算した値です。値はそのまま (外挿で) 表示しており変更していません。値にマウスを置くと有効範囲を表示します。",
                de: "Rot angezeigte Drücke wurden bei einer Temperatur (Temperature, bei Pb auch T₀) außerhalb des Gültigkeitsbereichs der jeweiligen Skala berechnet. Der Wert wird weiterhin (extrapoliert) angezeigt und nicht verändert; zeigen Sie mit der Maus darauf, um den gültigen Bereich zu sehen.",
                fr: "Les pressions affichées en rouge sont calculées à une température (Temperature, ou aussi T₀ pour Pb) hors du domaine de validité de l'échelle. La valeur reste affichée (extrapolée) et n'est pas modifiée ; survolez-la pour voir le domaine valide.",
                es: "Las presiones mostradas en rojo se calculan a una temperatura (Temperature, o también T₀ para Pb) fuera del rango de validez de la escala. El valor se sigue mostrando (extrapolado) y no se modifica; pase el ratón sobre él para ver el rango válido.",
                pt: "As pressões mostradas em vermelho são calculadas a uma temperatura (Temperature, ou também T₀ para Pb) fora do intervalo de validade da escala. O valor continua sendo mostrado (extrapolado) e não é alterado; passe o mouse sobre ele para ver o intervalo válido.",
                it: "Le pressioni mostrate in rosso sono calcolate a una temperatura (Temperature, o anche T₀ per Pb) fuori dall'intervallo di validità della scala. Il valore resta visualizzato (estrapolato) e non viene modificato; passa il mouse sopra per vedere l'intervallo valido.",
                ru: "Давления, показанные красным, вычислены при температуре (Temperature, для Pb также T₀) вне области применимости шкалы. Значение по-прежнему показывается (экстраполяция) и не изменяется; наведите на него курсор, чтобы увидеть допустимый диапазон.",
                zhHans: "红色显示的压力是在该标尺适用范围之外的温度（Temperature，Pb 还包括 T₀）下计算的。数值仍照常（外推）显示，未作更改；将鼠标悬停在数值上可查看有效范围。",
                zhHant: "紅色顯示的壓力是在該標尺適用範圍之外的溫度（Temperature，Pb 還包括 T₀）下計算的。數值仍照常（外推）顯示，未作更改；將滑鼠停在數值上可查看有效範圍。",
                ko: "빨간색으로 표시된 압력은 해당 스케일의 유효 범위를 벗어난 온도(Temperature, Pb는 T₀도 포함)에서 계산된 값입니다. 값은 그대로(외삽하여) 표시되며 변경되지 않습니다. 값 위에 마우스를 올리면 유효 범위가 표시됩니다."));

            MouseRange = false;

            timer.Start();

            ReadInitialRegistry();

            radioButtonMode_CheckedChanged(new object(), new EventArgs());

            numericBoxRubyRefR1_ValueChanged(sender, e);
        }
        //261105Cl 変更: 旧 Form.Closed (WFDEV004: 非推奨) を FormClosed に置換。本体は元々空なので挙動は不変
        //private void FormDiamondRaman_Closed(object sender, System.EventArgs e)
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        //260704Cl 追加: GuiCapture (--capture) 実行時は設定を保存しない (キャプチャ用のカルチャ・ウィンドウ状態でユーザー設定を汚染しないため)
        internal bool SuppressRegistrySave = false;

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SuppressRegistrySave)//260704Cl 追加
                SaveInitialRegistry();
        }

        #endregion

        #region レジストリ操作
        //レジストリの読み込み
        private void ReadInitialRegistry()
        {
            try
            {
                RegistryKey regKey = Registry.CurrentUser.CreateSubKey("Software\\Crystallography\\PressureCalculator");
                if (regKey == null) return;

                if ((int)regKey.GetValue("formMainLocationX", this.Location.X) >= 0)
                {
                    this.Width = (int)regKey.GetValue("formMainWidth", this.Width);
                    this.Height = (int)regKey.GetValue("formMainHeight", this.Height);

                    this.Location = new Point((int)regKey.GetValue("formMainLocationX", this.Location.X), (int)regKey.GetValue("formMainLocationY", this.Location.Y));
                }

                this.radioButtonDiamondRaman.Checked = (string)regKey.GetValue("radioButtonDiamondRaman.Checked", "True") == "True";
                this.radioButtonEOS.Checked = (string)regKey.GetValue("radioButtonEOS.Checked", "True") == "True";
                this.radioButtonRubyFluorescence.Checked = (string)regKey.GetValue("radioButtonRubyFluorescence.Checked", "True") == "True";

                this.fittingRangeRuby = Convert.ToDouble(regKey.GetValue("fittingRangeRuby", fittingRangeRuby));
                this.fittingRangeDiamond = Convert.ToDouble(regKey.GetValue("fittingRangeDiamond", fittingRangeDiamond));

                this.textBoxDiamondRamanNu0.Text = (string)regKey.GetValue("textBoxAkahama2006Nu0.Text", textBoxDiamondRamanNu0.Text);
                //261105Cl 変更: 次行は Save 側で一度も書かれないキー ("numericBoxRubyR1_0.Text") を参照R1へ読む死んだ行 (直後の "numericBoxRubyRefR1.Text" で必ず上書き) なので廃止
                //numericBoxRubyRefR1.Text = (string)regKey.GetValue("numericBoxRubyR1_0.Text", numericBoxRubyRefR1.Text);
                numericBoxRubyRefT.Text = (string)regKey.GetValue("numericBoxRubyRefT.Text", numericBoxRubyRefT.Text);
                //261105Cl 変更: 既定値に温度欄 (numericBoxRubyRefT.Text = 297.15) を渡していたため、初回起動で参照R1が 297.15 nm になり保存されていた。既定値は参照R1欄自身 (Designer 既定 694.25 nm)
                //numericBoxRubyRefR1.Text = (string)regKey.GetValue("numericBoxRubyRefR1.Text", numericBoxRubyRefT.Text);
                var defaultRefR1 = numericBoxRubyRefR1.Value; // 261105Cl 追加: Designer 既定 (694.25 nm)
                numericBoxRubyRefR1.Text = (string)regKey.GetValue("numericBoxRubyRefR1.Text", numericBoxRubyRefR1.Text);
                // 261105Cl 追加: 上の不具合で 297.15 nm などが保存されたままの環境を救う。ルビー R1 として明らかにおかしい値 (600–800 nm の外、読めない値) は既定値へ戻す
                if (numericBoxRubyRefR1.Value is not (>= 600 and <= 800)) // 261105Cl
                    numericBoxRubyRefR1.Value = defaultRefR1; // 261105Cl

                this.radioButtonTempUnitK.Checked = (string)regKey.GetValue("radioButtonTempUnitK.Checked", "True") == "True";
                //261105Cl 変更: 既定 "True" だと初回起動で K の直後に ℃ が選ばれ、297.15 (K のつもりの既定値) が 297.15 ℃ と解釈されていた。既定は Designer と同じ K
                //this.radioButtonTempUnitC.Checked = (string)regKey.GetValue("radioButtonTempUnitC.Checked", "True") == "True";
                this.radioButtonTempUnitC.Checked = (string)regKey.GetValue("radioButtonTempUnitC.Checked", "False") == "True";

                // 261105Cl 追加: 測定 R1₀ (R1(T,P=0)) と「Ragan の式から計算」も保存・復元する (従来は起動のたびに Designer 既定 694.65 nm / オフへ戻っていた)。
                //  キー名は既存の流儀 (コントロール名.Text / .Checked)。どちらのキーも過去のどの版でも書かれていない (R1₀ は 2021-05 の初版だけ "textBoxMaoRamda0.Text" に保存し、
                //  その後このキーは参照R1 を入れていた) ので、古いレジストリから意味の違う値を読むことはない。キーが無ければ Designer 既定のまま。
                //  参照R1/参照T/温度単位を読んだ後に置く: チェックが復元されると CheckedChanged → calcR1_0() が確定済みの入力で R1₀ を計算し直す。
                var defaultR1_0 = numericBoxRubyR1_0.Value; // 261105Cl 追加: Designer 既定 (694.65 nm)
                numericBoxRubyR1_0.Text = (string)regKey.GetValue("numericBoxRubyR1_0.Text", numericBoxRubyR1_0.Text); // 261105Cl 追加
                if (numericBoxRubyR1_0.Value is not (>= 600 and <= 800)) // 261105Cl 追加: 参照R1 と同じく、ルビー R1 として明らかにおかしい値・読めない値は既定値へ
                    numericBoxRubyR1_0.Value = defaultR1_0; // 261105Cl 追加
                this.checkBoxRubyR1_0CalculatedFromRagan.Checked = (string)regKey.GetValue("checkBoxRubyR1_0CalculatedFromRagan.Checked", "False") == "True"; // 261105Cl 追加

                regKey.Close();
            }
            catch { }
        }
        //.iniファイルを書き込み
        private void SaveInitialRegistry()
        {
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey("Software\\Crystallography\\PressureCalculator");
            if (regKey == null) return;
            regKey.SetValue("formMainWidth", this.Width);
            regKey.SetValue("formMainHeight", this.Height);
            regKey.SetValue("formMainLocationX", Location.X);
            regKey.SetValue("formMainLocationY", Location.Y);

            regKey.SetValue("radioButtonDiamondRaman.Checked", radioButtonDiamondRaman.Checked);
            regKey.SetValue("radioButtonEOS.Checked", radioButtonEOS.Checked);
            regKey.SetValue("radioButtonRubyFluorescence.Checked", radioButtonRubyFluorescence.Checked);

            regKey.SetValue("fittingRangeRuby", fittingRangeRuby);
            regKey.SetValue("fittingRangeDiamond", fittingRangeDiamond);
            regKey.SetValue("textBoxAkahama2006Nu0.Text", textBoxDiamondRamanNu0.Text);
            //regKey.SetValue("textBoxMaoRamda0.Text", numericBoxRubyRefR1.Text);//261105Cl 変更: 読み込み側に対応するキーが無い (参照R1は下の "numericBoxRubyRefR1.Text" で保存・復元) ので廃止
            regKey.SetValue("numericBoxRubyRefT.Text", numericBoxRubyRefT.Text);
            regKey.SetValue("numericBoxRubyRefR1.Text", numericBoxRubyRefR1.Text);

            regKey.SetValue("radioButtonTempUnitK.Checked", radioButtonTempUnitK.Checked);
            regKey.SetValue("radioButtonTempUnitC.Checked", radioButtonTempUnitC.Checked);

            regKey.SetValue("numericBoxRubyR1_0.Text", numericBoxRubyR1_0.Text); // 261105Cl 追加: 測定 R1₀
            regKey.SetValue("checkBoxRubyR1_0CalculatedFromRagan.Checked", checkBoxRubyR1_0CalculatedFromRagan.Checked); // 261105Cl 追加: 「Ragan の式から計算」

            regKey.SetValue("Culture", Thread.CurrentThread.CurrentUICulture.Name);//260703Cl 追加: UI 言語の永続化 (Program.Main が起動時に読む)

            regKey.Close();
        }
        #endregion

        #region 言語メニュー (260703Cl 追加: CSManager/FormMain の SupportedCultures 駆動 + 自動再起動方式を移植)
        // 言語メニュー項目を中央 allow-list (SupportedCultures) から動的生成する。
        // Released=true の言語だけを各々の自言語表記 (NativeName) で並べ、Tag に CultureInfo 名を入れて Tag 駆動で切り替える。
        private void PopulateLanguageMenu()
        {
            languageToolStripMenuItem.DropDownItems.Clear();
            foreach (var c in Crystallography.SupportedCultures.All)
            {
                if (!c.Released)
                    continue;
                var item = new ToolStripMenuItem(c.NativeName) { Tag = c.Name, Name = c.Name + "ToolStripMenuItem" };
                item.Click += languageToolStripMenuItem_Click;
                languageToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        private void UpdateLanguageMenuChecks(string currentName)
        {
            foreach (ToolStripItem it in languageToolStripMenuItem.DropDownItems)
                if (it is ToolStripMenuItem mi)
                    mi.Checked = (mi.Tag as string) == currentName;
        }

        private void languageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var culture = Crystallography.SupportedCultures.Resolve(((ToolStripMenuItem)sender).Tag as string);

            // 既に現在の言語を選んだだけなら何もしない (再クリックでの無用な再起動を防ぐ)。
            if (culture.Name == Crystallography.SupportedCultures.Current.Name)
            {
                UpdateLanguageMenuChecks(culture.Name);
                return;
            }

            // 切替には再起動が要る (作業中の状態は失われる) ことを、まだ切り替わっていない現在の言語で確認する。
            var msg = Crystallography.Localization.Loc(
                en: $"Switching the display language to \"{culture.NativeName}\" requires restarting PressureCalculator.\nUnsaved work will be lost. Restart now?",
                ja: $"表示言語を「{culture.NativeName}」に切り替えるには PressureCalculator の再起動が必要です。\n保存していない作業は失われます。今すぐ再起動しますか？");
            if (MessageBox.Show(this, msg, Crystallography.Localization.Loc(en: "Change language", ja: "言語の変更"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                UpdateLanguageMenuChecks(Crystallography.SupportedCultures.Current.Name);
                return;
            }

            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture.Name);
            UpdateLanguageMenuChecks(culture.Name);
            RestartApplicationForLanguageChange();
        }

        // 言語切替の自動再起動。FormClosing を待たず先に言語値を保存し、新インスタンスを起動してから自身を閉じる。
        private void RestartApplicationForLanguageChange()
        {
            SaveInitialRegistry();// ここで CurrentUICulture は新言語へ切替済み → 新プロセスが起動直後に新言語を読める

            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = !string.IsNullOrEmpty(Application.ExecutablePath) ? Application.ExecutablePath : Environment.ProcessPath,
                WorkingDirectory = Environment.CurrentDirectory,
                UseShellExecute = true,
            };

            try
            {
                if (System.Diagnostics.Process.Start(startInfo) == null)
                    throw new InvalidOperationException("Process.Start returned null.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    Crystallography.Localization.Loc(
                        en: $"Failed to restart PressureCalculator.\n{ex.Message}",
                        ja: $"PressureCalculator の再起動に失敗しました。\n{ex.Message}"),
                    Crystallography.Localization.Loc(en: "Change language", ja: "言語の変更"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Close();
        }
        #endregion

        //260704Cl 追加: GuiCapture (--capture) 用のモード切替。radioButton は private のため internal メソッドで公開する。
        internal void SetCaptureMode(string mode)
        {
            if (mode == "diamond") radioButtonDiamondRaman.Checked = true;
            else if (mode == "ruby") radioButtonRubyFluorescence.Checked = true;
            else radioButtonEOS.Checked = true;
        }

        //260703Cl 追加: Help メニュー「Online manual」(F1 と同じ URL を開く)
        private void helpOnlineManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var url = Crystallography.Controls.FormBase.HelpUrlResolver?.Invoke(this);
            if (!string.IsNullOrEmpty(url))
                try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true }); } catch { }
        }



        #region ファイル読み込み関連
        private Profile readFile(string fileName)
        {
            var reader = new StreamReader(fileName, Encoding.GetEncoding("UTF-8"));
            var contents = new List<string>();
            string str;
            while ((str = reader.ReadLine()) != null)
                if (str != "")
                    contents.Add(str);
            reader.DiscardBufferedData();
            reader.Close();

            var temp = contents.ToArray();

            var profile = new Profile();
            if ((profile = ConvertUnknownFileToProfileData(temp, ',')) == null)//そのほかのファイル形式のとき
                if ((profile = ConvertUnknownFileToProfileData(temp, ' ')) == null)
                    if ((profile = ConvertUnknownFileToProfileData(temp, '\t')) == null)
                    {
                        string[] str1;
                        for (int i = 0; i < temp.Length; i++)
                        {
                            str1 = temp[i].Split(',');
                            if (str1.Length > 2)
                            {
                                if (str1[0].Length > 2)
                                    profile.Pt.Add(new PointD(Convert.ToDouble(str1[0]), Convert.ToDouble(str1[2])));
                                else
                                    profile.Pt.Add(new PointD(Convert.ToDouble(str1[1]), Convert.ToDouble(str1[2])));
                            }
                            else if (str1.Length == 2)
                            {
                                profile.Pt.Add(new PointD(Convert.ToDouble(str1[0]), Convert.ToDouble(str1[1])));
                            }
                        }
                    }

            if (profile.Pt != null && profile.Pt.Count > 0)
            {
                currentFileName = fileName;
                lastWriteTime = File.GetLastWriteTime(currentFileName);
                this.Text = "Pressure Calculator    " + currentFileName;
            }

            return profile;
        }


        //よく分からないファイルを読み込む
        private Profile ConvertUnknownFileToProfileData(string[] strArray, char separater)
        {
            var stringList = new List<string[]>();
            //まず指定されたセパレータで全てを区切る
            for (int i = 0; i < strArray.Count(); i++)
                stringList.Add(strArray[i].Split(new char[] { separater }, StringSplitOptions.RemoveEmptyEntries));
            //その全てを数値にへんかんする
            var doubleList = new List<double[]>();
            var doubleTemp = new List<double>();
            double result;
            for (int i = 0; i < stringList.Count; i++)
            {
                doubleTemp = new List<double>();
                for (int j = 0; j < stringList[i].Length; j++)
                    if (double.TryParse(stringList[i][j], out result))
                        doubleTemp.Add(result);
                doubleList.Add(doubleTemp.ToArray());
            }
            int count = 1;
            int beforeLength = 0;
            int countMax = int.MinValue;
            int startColumn = 1;
            int endColumn = 0;

            //doubleList中で2つ以上数値を含み、100個以上連続しているものばしょをえらぶ
            for (int i = 0; i < doubleList.Count; i++)
            {
                count = 0;
                beforeLength = doubleList[i].Length;
                if (beforeLength >= 2)
                {
                    for (int j = i; j < doubleList.Count && beforeLength == doubleList[j].Length; j++)
                        count++;
                    if (countMax < count)
                    {
                        countMax = count;
                        startColumn = i;
                        endColumn = i + count - 1;
                    }
                    i += count - 1;
                }
            }
            if (countMax < 100) return null;//100以下だったらNullをかえす


            if (endColumn + 1 < doubleList.Count)
                doubleList.RemoveRange(endColumn + 1, doubleList.Count - endColumn - 1);
            doubleList.RemoveRange(0, startColumn);

            //X軸をきめる 0.00000001以上のステップで100個以上連続しているものをさがす
            int xRow = -1;
            double tempStep;
            countMax = int.MinValue;
            startColumn = 0;
            endColumn = 0;
            for (int i = 0; i < doubleList[0].Length; i++)
            {
                for (int j = 0; j < doubleList.Count - 1; j++)
                {
                    count = 0;
                    tempStep = doubleList[j + 1][i] - doubleList[j][i];
                    for (int k = j + 1; k < doubleList.Count - 1 && Math.Abs(doubleList[k][i] + tempStep - doubleList[k + 1][i]) < 10 && tempStep > 0.00001; k++)
                        count++;
                    if (countMax < count)
                    {
                        countMax = count + 2;
                        startColumn = j;
                        endColumn = startColumn + count + 1;
                        xRow = i;
                    }
                    j += count;
                }
            }
            if (countMax < 100 || xRow == -1) return null;//100以下かXを見つけられなかったらNullをかえす

            if (endColumn + 1 < doubleList.Count)
                doubleList.RemoveRange(endColumn + 1, doubleList.Count - endColumn - 1);
            doubleList.RemoveRange(0, startColumn);

            //y軸を決める 標準偏差が一番大きい数が格納されている
            int yRow = -1;
            double Sum, SumSquare, Deviation, DeviationMax;
            DeviationMax = double.NegativeInfinity;
            for (int i = 0; i < doubleList[0].Length; i++)
                if (i != xRow)
                {
                    Sum = SumSquare = 0;
                    for (int j = 0; j < doubleList.Count; j++)
                    {
                        Sum += doubleList[j][i];
                        SumSquare += doubleList[j][i] * doubleList[j][i];
                    }
                    Deviation = (doubleList.Count * SumSquare - Sum * Sum) / doubleList.Count / (doubleList.Count - 1);
                    if (DeviationMax < Deviation)
                    {
                        DeviationMax = Deviation;
                        yRow = i;
                    }
                }
            if (yRow == -1) return null;

            //最後に値を代入
            Profile profile = new Profile();
            for (int i = 0; i < doubleList.Count; i++)
                profile.Pt.Add(new PointD(doubleList[i][xRow], doubleList[i][yRow]));

            return profile;
        }


        //移動平均と微分プロファイルを計算
        private void CalcSmoothingAndDifferentiation()
        {
            if (Original == null) return;
            int n; double kayser, count, weight;

            //上側のPictureBoxのプロファイル
            OriginalSmooth = new Profile();
            if (numericUpDownOriginalRunningAverage.ReadOnly == false)
            {
                n = (int)numericUpDownOriginalRunningAverage.Value;
                for (int i = 0; i < Original.Pt.Count - n + 1; i++)
                {
                    kayser = 0; count = 0;
                    for (int j = 0; j < n; j++)
                    {
                        kayser += Original.Pt[i + j].X;
                        count += Original.Pt[i + j].Y;
                    }
                    OriginalSmooth.Pt.Add(new PointD(kayser / n, count / n));
                }
            }
            else
            {
                n = (int)numericUpDownOriginalGaussian.Value;
                if (n == 0)
                    for (int i = 0; i < Original.Pt.Count; i++)
                        OriginalSmooth.Pt.Add(Original.Pt[i]);
                else
                    for (int i = 0; i < Original.Pt.Count; i++)
                    {
                        count = weight = 0;
                        for (int j = -50; j <= 50; j++)
                        {
                            if (i + j > -1 && i + j < Original.Pt.Count)
                            {
                                //261105Cl 変更: j, n が int のため -j * j / n / n が整数除算になり、重みが exp(-floor(j²/n²)) の階段関数になっていた。double で計算する
                                //count += Original.Pt[i + j].Y * Math.Exp(-j * j / n / n);
                                //weight += Math.Exp(-j * j / n / n);
                                count += Original.Pt[i + j].Y * Math.Exp(-(double)j * j / n / n);
                                weight += Math.Exp(-(double)j * j / n / n);
                            }
                        }
                        OriginalSmooth.Pt.Add(new PointD(Original.Pt[i].X, count / weight));
                    }
            }



            //微分計算
            BottomProfile = new Profile();
            BottomProfileSmooth = new Profile();

            if (radioButtonDiamondRaman.Checked)//DiamondRamanモードのとき
                BottomProfile = OriginalSmooth.Differential(1);
            else
                for (int i = 0; i < Original.Pt.Count; i++)
                    BottomProfile.Pt.Add(Original.Pt[i]);

            //微分スムージング
            if (numericUpDownDifferentiationRunningAverage.ReadOnly == false)
            {//移動平均
                n = (int)numericUpDownDifferentiationRunningAverage.Value;
                for (int i = 0; i < BottomProfile.Pt.Count - n + 1; i++)
                {
                    kayser = count = 0;
                    for (int j = 0; j < n; j++)
                    {
                        kayser += BottomProfile.Pt[i + j].X;
                        count += BottomProfile.Pt[i + j].Y;
                    }
                    BottomProfileSmooth.Pt.Add(new PointD(kayser / n, count / n));
                }
            }
            else
            {//ガウシアン
                n = (int)numericUpDownDifferentiationGaussian.Value;
                if (n == 0)
                    for (int i = 0; i < BottomProfile.Pt.Count; i++)
                        BottomProfileSmooth.Pt.Add(BottomProfile.Pt[i]);
                else
                    for (int i = 0; i < BottomProfile.Pt.Count; i++)
                    {
                        count = weight = 0;
                        for (int j = -50; j <= 50; j++)
                            if (i + j > -1 && i + j < BottomProfile.Pt.Count)
                            {
                                //261105Cl 変更: 整数除算の修正 (上の元プロファイル側と同じ)
                                //count += BottomProfile.Pt[i + j].Y * Math.Exp(-j * j / n / n);
                                //weight += Math.Exp(-j * j / n / n);
                                count += BottomProfile.Pt[i + j].Y * Math.Exp(-(double)j * j / n / n);
                                weight += Math.Exp(-(double)j * j / n / n);
                            }
                        BottomProfileSmooth.Pt.Add(new PointD(BottomProfile.Pt[i].X, count / weight));
                    }
            }
        }
        #endregion

        bool skipFitting = false;

        //プロファイルからバンドエッジを探す
        private void SearchBandEdge()

        {
            if (skipFitting)
                return;

            if (BottomProfile == null) return;

            skipFitting = true;
            while (graphControlBottom.ProfileList.Length < 4)
                graphControlBottom.AddProfile(null);
            skipFitting = false;


            if (radioButtonDiamondRaman.Checked)//DiamondRamanのとき
            {

                double maximumY = double.NegativeInfinity;
                double centerX = 0;
                List<PointD> pt = new List<PointD>();
                for (int i = 0; i < BottomProfileSmooth.Pt.Count; i++)
                    if (BottomProfileSmooth.Pt[i].X < graphControlBottom.UpperX && BottomProfileSmooth.Pt[i].X > graphControlBottom.LowerX)
                    {
                        pt.Add(new PointD(BottomProfileSmooth.Pt[i].X, -BottomProfileSmooth.Pt[i].Y));
                        if (maximumY < -BottomProfileSmooth.Pt[i].Y)
                        {
                            maximumY = -BottomProfileSmooth.Pt[i].Y;
                            centerX = BottomProfileSmooth.Pt[i].X;
                        }
                    }
                //double range = (BottomProfileSmooth.Pt[1].X - BottomProfileSmooth.Pt[0].X) * (int)numericUpDownFitRange.Value;
                double range = (double)numericUpDownFitRange.Value;
                PF = new PeakFunction[] { new PeakFunction(centerX, range, range, PeakFunctionForm.PseudoVoigt) };

                FittingPeak.FitMultiPeaksThread(pt, true, 0, ref PF);

                textBoxFittingInformation.Text = $"Edge:\t  X = {PF[0].X:f4}\tFWHM = {PF[0].Hk:f4}\tη = {PF[0].eta:f4}\tBackground Y = {PF[0].B1 - PF[0].B2 * PF[0].X:g8} + {PF[0].B2:g8} * X\r\n";

                var p = new Profile();
                for (double x = PF[0].X - range * 0.9; x < PF[0].X + range * 0.9; x += range / 200)
                    p.Pt.Add(new PointD(x, -PF[0].GetValue(x, false) - PF[0].B1 - PF[0].B2 * (x - PF[0].X)));
                p.Color = Color.Red;

                graphControlBottom.ReplaceProfile(1, p);
                graphControlBottom.ReplaceProfile(2, null);
                graphControlBottom.ReplaceProfile(3, null);

                textBoxDiamondRamanNu.Text = PF[0].X.ToString();
            }
            else if (radioButtonRubyFluorescence.Checked)//Ruby 蛍光のとき
            {

                var maximumY = double.NegativeInfinity;
                double centerX = 0;
                var pt = new List<PointD>();
                for (int i = 0; i < BottomProfileSmooth.Pt.Count; i++)
                    if (BottomProfileSmooth.Pt[i].X < graphControlBottom.UpperX && BottomProfileSmooth.Pt[i].X > graphControlBottom.LowerX)
                    {
                        pt.Add(new PointD(BottomProfileSmooth.Pt[i].X, BottomProfileSmooth.Pt[i].Y));
                        if (maximumY < BottomProfileSmooth.Pt[i].Y)
                        {
                            maximumY = BottomProfileSmooth.Pt[i].Y;
                            centerX = BottomProfileSmooth.Pt[i].X;
                        }
                    }
                // double range = (BottomProfileSmooth.Pt[1].X - BottomProfileSmooth.Pt[0].X) * (int)numericUpDownFitRange.Value;
                double range = (int)numericUpDownFitRange.Value;
                PF = new PeakFunction[2];
                PF[0] = new PeakFunction(centerX, range / 2, range, PeakFunctionForm.PseudoVoigt);
                PF[1] = new PeakFunction(centerX - 1.5, range / 2, range, PeakFunctionForm.PseudoVoigt);

                FittingPeak.FitMultiPeaksThread(pt, true, 0, ref PF);

                textBoxFittingInformation.Text = $"R1:\t  X = {PF[0].X:f4}\tFWHM = {PF[0].Hk:f4}\tη = {PF[0].eta:f4}\tBackground Y = {PF[0].B1 - PF[0].B2 * PF[0].X:g8} + {PF[0].B2:g8} * X\r\n";
                textBoxFittingInformation.Text += $"R2:\t  X = {PF[1].X:f4}\tFWHM = {PF[1].Hk:f4}\tη = {PF[1].eta:f4}\tBackground Y = {PF[1].B1 - PF[1].B2 * PF[1].X:g8} + {PF[1].B2:g8} * X";

                Profile p1 = new Profile();
                for (double x = PF[0].X - range * 0.9; x < PF[0].X + range * 0.9; x += range / 200)
                    p1.Pt.Add(new PointD(x, PF[0].GetValue(x, false) + PF[0].B1 + PF[0].B2 * (x - PF[0].X)));
                p1.Color = Color.LightGreen;

                Profile p2 = new Profile();
                for (double x = PF[1].X - range * 0.9; x < PF[1].X + range * 0.9; x += range / 200)
                    p2.Pt.Add(new PointD(x, PF[1].GetValue(x, false) + PF[1].B1 + PF[1].B2 * (x - PF[1].X)));
                p2.Color = Color.LightCyan;

                Profile p3 = new Profile();
                for (double x = PF[1].X - range * 0.9; x < PF[0].X + range * 0.9; x += range / 200)
                    p3.Pt.Add(new PointD(x, PF[1].GetValue(x, false) + PF[0].GetValue(x, false) + PF[1].B1 + PF[1].B2 * (x - PF[1].X)));
                p3.Color = Color.LightPink;


                graphControlBottom.ReplaceProfile(1, p1);
                graphControlBottom.ReplaceProfile(2, p2);
                graphControlBottom.ReplaceProfile(3, p3);

                numericBoxRubyR1.Value = PF[0].X;
            }
        }

        //ダブルクリックした点を中心にプロファイルをフィッティングする
        private bool graphControlBottom_MouseDoubleClick2(PointD pt)
        {
            if (BottomProfile == null) return false;
            if (radioButtonDiamondRaman.Checked) return false;
            while (graphControlBottom.ProfileList.Length < 4)
                graphControlBottom.AddProfile(null);

            double edge = pt.X;
            double I = 0, startI, endI, stepI, Hk, startHk, endHk, stepHk, startEdge, endEdge, stepEdge, residual, residualBest;
            double bestI = 0, bestHk = 0, bestEdge = 0;
            List<PointD> pt1 = new List<PointD>();
            double FitRange = (double)this.numericUpDownFitRange.Value;

            for (int i = 0; i < BottomProfileSmooth.Pt.Count; i++)
                if (BottomProfileSmooth.Pt[i].X > pt.X - FitRange && BottomProfileSmooth.Pt[i].X < pt.X + FitRange)
                {
                    pt1.Add(new PointD(BottomProfileSmooth.Pt[i].X, BottomProfileSmooth.Pt[i].Y));
                    I += BottomProfile.Pt[i].Y;
                }
            if (pt1.Count < 4) return false;
            I *= pt1[1].X - pt1[0].X;


            { startI = I * 0.5; endI = I * 5; stepI = (endI - startI) / 20; }
            if (I < 0)
            { double t = startI; startI = endI; endI = t; stepI = -stepI; }

            startHk = FitRange * 0.01; endHk = FitRange * 20; stepHk = (endHk - startHk) / 20;
            startEdge = edge - FitRange; endEdge = edge + FitRange; stepEdge = (endEdge - startEdge) / 20;

            residualBest = double.PositiveInfinity;
            double temp;
            for (int n = 0; n < 10; n++)
            {	//全体の繰り返し回数
                for (edge = startEdge; edge <= endEdge; edge += stepEdge)
                    for (I = startI; I <= endI; I += stepI)
                        for (Hk = startHk; Hk <= endHk; Hk += stepHk)
                        {
                            residual = 0;
                            for (int i = 0; i < pt1.Count; i++)
                            {
                                temp = pt1[i].Y - I * 2 / Hk / Math.PI * (1 / (1 + 4 * (edge - pt1[i].X) / Hk * (edge - pt1[i].X) / Hk));
                                residual += temp * temp;
                            }

                            if (residual < residualBest)
                            {
                                residualBest = residual;
                                bestEdge = edge;
                                bestHk = Hk;
                                bestI = I;
                            }
                        }
                startI = bestI - 1.5 * stepI; endI = bestI + 1.5 * stepI; stepI *= 0.66;
                startHk = bestHk - 2.5 * stepHk; endHk = bestHk + 2.5 * stepHk; stepHk *= 0.36;
                startEdge = bestEdge - 1.5 * stepEdge; endEdge = bestEdge + 1.5 * stepEdge; stepEdge *= 0.66;
            }

            Profile p = new Profile();
            for (double x = bestEdge - FitRange * 0.9; x < bestEdge + FitRange * 0.9; x += FitRange / 200)
                p.Pt.Add(new PointD(x, bestI * 2 / bestHk / Math.PI * (1 / (1 + 4 * (bestEdge - x) / bestHk * (bestEdge - x) / bestHk))));

            if (radioButtonDiamondRaman.Checked)
            {
                p.Color = Color.Red;
                graphControlBottom.ReplaceProfile(1, p);

                graphControlBottom.ReplaceProfile(2, null);
                graphControlBottom.ReplaceProfile(3, null);

                textBoxDiamondRamanNu.Text = bestEdge.ToString();
            }
            else
            {
                p.Color = Color.LightGreen;
                graphControlBottom.ReplaceProfile(1, p);

                graphControlBottom.ReplaceProfile(2, null);
                graphControlBottom.ReplaceProfile(3, null);

                numericBoxRubyR1.Value = bestEdge;
            }
            return false;


        }





        #region 平滑化コントロール関連

        private void numericUpDown_ValueChanged(object sender, System.EventArgs e)
        {
            CalcSmoothingAndDifferentiation();
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = true;
            graphControlTop.Profile = OriginalSmooth;
            graphControlBottom.Profile = BottomProfileSmooth;
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = false;
        }


        private void numericUpDownOriginalRunningAverage_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (numericUpDownOriginalRunningAverage.ReadOnly == true)
            {
                numericUpDownOriginalRunningAverage.ReadOnly = false;
                numericUpDownOriginalGaussian.ReadOnly = true;
            }
            CalcSmoothingAndDifferentiation();
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = true;
            graphControlTop.Profile = OriginalSmooth;
            graphControlBottom.Profile = BottomProfileSmooth;
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = false;
        }

        private void numericUpDownOriginalGaussian_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (numericUpDownOriginalGaussian.ReadOnly == true)
            {
                numericUpDownOriginalRunningAverage.ReadOnly = true;
                numericUpDownOriginalGaussian.ReadOnly = false;
            }
            CalcSmoothingAndDifferentiation();
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = true;
            graphControlTop.Profile = OriginalSmooth;
            graphControlBottom.Profile = BottomProfileSmooth;
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = false;

        }

        private void numericUpDownDifferentiationRunningAverage_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (numericUpDownDifferentiationRunningAverage.ReadOnly == true)
            {
                numericUpDownDifferentiationRunningAverage.ReadOnly = false;
                numericUpDownDifferentiationGaussian.ReadOnly = true;
            }
            CalcSmoothingAndDifferentiation();
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = true;
            graphControlTop.Profile = OriginalSmooth;
            graphControlBottom.Profile = BottomProfileSmooth;
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = false;
        }

        private void numericUpDownwnDifferentiationGaussian_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (numericUpDownDifferentiationGaussian.ReadOnly == true)
            {
                numericUpDownDifferentiationRunningAverage.ReadOnly = true;
                numericUpDownDifferentiationGaussian.ReadOnly = false;
            }
            CalcSmoothingAndDifferentiation();
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = true;
            graphControlTop.Profile = OriginalSmooth;
            graphControlBottom.Profile = BottomProfileSmooth;
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = false;
        }
        #endregion


   
        private void textBoxNu_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                var nu = Convert.ToDouble(textBoxDiamondRamanNu.Text);
                var nuPerNu0 = (nu - Convert.ToDouble(textBoxDiamondRamanNu0.Text)) / Convert.ToDouble(textBoxDiamondRamanNu0.Text);

                //Akahama2004
                textBoxDiamondAkahama2004P.Text =
                    (Convert.ToDouble(textBoxAkahama2004A.Text) + Convert.ToDouble(textBoxAkahama2004B.Text) * nu + Convert.ToDouble(textBoxAkahama2004C.Text) * 0.0001 * nu * nu).ToString(StringFormat);
                //Akahama2006
                textBoxDiamondAkahama2006P.Text =
                    (Convert.ToDouble(textBoxAkahama2006K0.Text) * nuPerNu0 * (1 + 0.5 * (Convert.ToDouble(textBoxAkahama2006K0Prime.Text) - 1) * nuPerNu0)).ToString(StringFormat);
                //Fratanduno 2021 Low
                textBoxDiamondFratandunoLow.Text = (530.77 * nuPerNu0 + 753.83 * nuPerNu0 * nuPerNu0).ToString(StringFormat);
                //Fratanduno 2021 High
                textBoxDiamondFratandunoHigh.Text = (199.49 - 852.78 * nuPerNu0 + 3103.8 * nuPerNu0 * nuPerNu0).ToString(StringFormat);

            }
            catch
            {
                //260704Cl 変更: 日本語直書き → 11言語 Loc (旧: MessageBox.Show("適切な数値を入れてください");)
                MessageBox.Show(Crystallography.Localization.Loc(
                    en: "Please enter a valid number.", ja: "適切な数値を入れてください", de: "Bitte geben Sie eine gültige Zahl ein.",
                    fr: "Veuillez saisir un nombre valide.", es: "Introduzca un número válido.", pt: "Insira um número válido.",
                    it: "Inserire un numero valido.", ru: "Введите корректное число.", zhHans: "请输入有效的数值。",
                    zhHant: "請輸入有效的數值。", ko: "올바른 숫자를 입력하세요."));
            }
        }

        private void radioButtonMode_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDiamondRaman.Checked)
            {
                panelEOS.Visible = false;
                splitContainer1.Visible = true;
                numericUpDownFitRange.Value = (decimal)fittingRangeDiamond;

                splitContainer1.SplitterDistance = splitContainer1.Height / 2;
                //260704Cl 変更: 11言語 Loc (旧: labelBottomTitle.Text = "First Differentiation";)
                labelBottomTitle.Text = Crystallography.Localization.Loc(
                    en: "First Differentiation", ja: "一次微分", de: "Erste Ableitung", fr: "Dérivée première",
                    es: "Primera derivada", pt: "Primeira derivada", it: "Derivata prima", ru: "Первая производная",
                    zhHans: "一阶微分", zhHant: "一階微分", ko: "1차 미분");
                groupBoxAkahama2006.Visible = true;
                groupBoxMao.Visible = false;
                labelDimension.Text = "cm^-1";

                textBoxFittingInformation.Height = 24;

            }

            else if (radioButtonRubyFluorescence.Checked)
            {
                panelEOS.Visible = false;
                splitContainer1.Visible = true;
                splitContainer1.SplitterDistance = 0;
                //260704Cl 変更: 11言語 Loc (旧: labelBottomTitle.Text = "Ruby Fluorescence";)
                labelBottomTitle.Text = Crystallography.Localization.Loc(
                    en: "Ruby Fluorescence", ja: "ルビー蛍光", de: "Rubin-Fluoreszenz", fr: "Fluorescence du rubis",
                    es: "Fluorescencia del rubí", pt: "Fluorescência do rubi", it: "Fluorescenza del rubino",
                    ru: "Флуоресценция рубина", zhHans: "红宝石荧光", zhHant: "紅寶石螢光", ko: "루비 형광");
                numericUpDownFitRange.Value = (decimal)fittingRangeRuby;


                groupBoxAkahama2006.Visible = false;
                groupBoxMao.Visible = true;
                labelDimension.Text = "nm";

                textBoxFittingInformation.Height = 36;

            }

            else
            {
                panelEOS.Visible = true;
                panelEOS.Dock = DockStyle.Fill;
                splitContainer1.Visible = false;
                groupBoxAkahama2006.Visible = false;
                groupBoxMao.Visible = false;
            }

            //260704Cl 追加: F1 オンラインヘルプの飛び先を現在のモードのページに合わせる (slug は docs/mkdocs.yml の実ページ名と一致必須)
            HelpPage = radioButtonDiamondRaman.Checked ? "2-diamond-raman"
                     : radioButtonRubyFluorescence.Checked ? "1-ruby-fluorescence" : "3-equation-of-states";

            CalcSmoothingAndDifferentiation();
            graphControlTop.Profile = OriginalSmooth;
            graphControlBottom.Profile = BottomProfileSmooth;

        }

        bool skipCycling = false;
        private void graphControlTop_DrawingRangeChanged(RectangleD rectangle)
        {
            if (!radioButtonDiamondRaman.Checked) return;
            if (skipCycling) return;
            skipCycling = true;
            graphControlBottom.SetDrawingRangeXandExpandY(graphControlTop.DrawingRange);
            skipCycling = false;
            SearchBandEdge();

        }

        private void graphControlBottom_DrawingRangeChanged(RectangleD rectangle)
        {
            if (skipCycling) return;
            skipCycling = true;
            graphControlTop.SetDrawingRangeXandExpandY(graphControlBottom.DrawingRange);
            skipCycling = false;
            SearchBandEdge();
        }

        private void numericUpDownFitRange_ValueChanged(object sender, EventArgs e)
        {
            if (radioButtonDiamondRaman.Checked)
                fittingRangeDiamond = (double)numericUpDownFitRange.Value;
            else if (radioButtonRubyFluorescence.Checked)
                fittingRangeRuby = (double)numericUpDownFitRange.Value;
            SearchBandEdge();
        }

        #region ルビー蛍光

        /// <summary>
        /// Ruby 現在のR1をR1_0にコピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRubyRefR1Set_Click(object sender, EventArgs e) => numericBoxRubyRefR1.Value = numericBoxRubyR1.Value;

        private void numericBoxRubyRefT_ValueChanged(object sender, EventArgs e) => calcRaganParameter();
        private void numericBoxRubyRefR1_ValueChanged(object sender, EventArgs e) => calcRaganParameter();

        /// <summary>
        /// Referenceのデータから、Raganのパラメータを計算する
        /// </summary>
        private void calcRaganParameter()
        {
            var r = numericBoxRubyRefR1.Value;
            var t = radioButtonTempUnitK.Checked ? numericBoxRubyRefT.Value : numericBoxRubyRefT.Value + 273.15;
            numericBoxRubyRagan.Value = 1E7 / r - 4.49E-2 * t + 4.81E-4 * t * t - 3.71E-7 * t * t * t;
        }


        private void checkBoxRubyTemeratureSameAsRef_CheckedChanged(object sender, EventArgs e)
        {
            numericBoxRubyT.Enabled = !checkBoxRubyTemeratureSameAsRef.Checked;
            if (checkBoxRubyTemeratureSameAsRef.Checked)
                numericBoxRubyT.Value = numericBoxRubyRefT.Value;
        }

        /// <summary>
        ///  Raganの式から計算するというチェックボックスのイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxRubyR1CalculatedFromRagan_CheckedChanged(object sender, EventArgs e)
        {
            numericBoxRubyR1_0.Enabled = !checkBoxRubyR1_0CalculatedFromRagan.Checked;
            if (checkBoxRubyR1_0CalculatedFromRagan.Checked)
                calcR1_0();
        }

        /// <summary>
        /// Raganの式のパラメータが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericBoxRubyRagan_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxRubyR1_0CalculatedFromRagan.Checked)
                calcR1_0();
        }

        /// <summary>
        /// 測定時の温度の値が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericBoxRubyT_ValueChanged(object sender, EventArgs e)
        {
            if (checkBoxRubyR1_0CalculatedFromRagan.Checked)
                calcR1_0();
        }

        /// <summary>
        /// Raganの式からR1_0の値を計算
        /// </summary>
        private void calcR1_0()
        {
            var t = radioButtonTempUnitK.Checked ? numericBoxRubyT.Value : numericBoxRubyT.Value + 273.15;
            var prm = numericBoxRubyRagan.Value;
            numericBoxRubyR1_0.Enabled = true;

            numericBoxRubyR1_0.Value = 1E7 / (prm + 4.49E-2 * t - 4.81E-4 * t * t + 3.71E-7 * t * t * t);
            numericBoxRubyR1_0.Enabled = false;


        }

        /// <summary>
        /// R1_0の値が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericBoxR1_0_ValueChanged(object sender, EventArgs e)
        {
            calcRubyPressure();
        }

        /// <summary>
        /// R1の値が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericBoxRubyR1_ValueChanged(object sender, EventArgs e) => calcRubyPressure();


        /// <summary>
        /// R1とR1_0からルビースケール圧力を計算
        /// </summary>
        private void calcRubyPressure()
        {
            //Mao
            var r = numericBoxRubyR1.Value / numericBoxRubyR1_0.Value;
            textBoxMaoP.Text = (numericBoxMaoA.Value * (Math.Pow(r, 5) - 1) / 5).ToString(StringFormat);

            //MaoQuasi
            textBoxMaoQuasiP.Text = (numericBoxMaoQuasiA.Value * (Math.Pow(r, 7.665) - 1) / 7.665).ToString(StringFormat);

            //MaoQuasi
            textBoxMaoHydroP.Text = (numericBoxMaoHydroA.Value * (Math.Pow(r, 7.715) - 1) / 7.715).ToString(StringFormat);

            var delta = (numericBoxRubyR1.Value - numericBoxRubyR1_0.Value) / numericBoxRubyR1_0.Value;
            //Shen et al
            textBoxShenP.Text = (numericBoxShenA.Value * (delta + delta * delta * 5.63)).ToString(StringFormat);
        }

        private void radioButtonTempUnit_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTempUnitC.Checked)
            {
                numericBoxRubyRefT.FooterText = numericBoxRubyT.FooterText = "℃";
                numericBoxRubyRefT.Minimum = numericBoxRubyT.Minimum = -273.15;
            }
            else
            {
                numericBoxRubyRefT.FooterText = numericBoxRubyT.FooterText = "K";
                numericBoxRubyRefT.Minimum = numericBoxRubyT.Minimum = 0;

            }

            numericBoxRubyRefT_ValueChanged(sender, e);
            numericBoxRubyT_ValueChanged(sender, e);
        }

        #endregion

        #region EOS関連

     

        public void textBox_TextChanged(object sender, System.EventArgs e)
        {
            Gold();
            Pt();
            NaClB1();
            NaClB2();
            MgO();
            Corundum();
            Ar();
            Re();
            Mo();
            Pb();
            UpdateEosValidityWarnings(); // 261105Cl 追加
        }

        // 261105Cl 追加: EOS ごとの適用温度範囲。範囲外ならその圧力を赤字にし、ツールチップで範囲を示す (表示だけ。計算値は一切変えない)。
        //  Kind 0 = 公表された表の温度範囲 (外側は表の外挿)。Pb は T₀ でも a₀ の表 (0–310 K) を引くので T₀ も調べる。
        //  Kind 1 = 熱的な項が高温向けの形 (熱圧力が (T − 300 K) に比例: Anderson/Holmes/Ueda、高温の熱膨張式 α = a + bT + c/T² と K の (T − 300 K) 依存: Dubrovinsky (98)/Zhao (00))。室温未満は非物理的。
        //  Kind 2 = 室温 (300 K) の等温式で温度を使わない (Fratanduono/Sata/Sakai/Ross/Re の 3 種)。厳密には 300 K だけだが、室温の入力 (295 K 等) で警告が出続けないよう 290–310 K の外で警告。
        //  Kind 3 = 著者が示す適用範囲 (Matsui (09): 3000 K・300 GPa まで)。
        //  Debye 模型の熱圧力 (MGD) を持ち範囲の記載を確かめられないもの (Shim, Matsui (12), MgO 4 種, Jephcoat, Huang) は対象外。
        private (Crystallography.Controls.NumericBox Box, double Tmin, double Tmax, int Kind, Color ForeColor)[] eosValidity;

        // 261105Cl 追加
        private void UpdateEosValidityWarnings()
        {
            const double inf = double.PositiveInfinity;
            eosValidity ??= new (Crystallography.Controls.NumericBox Box, double Tmin, double Tmax, int Kind)[]
            {
                (numericalTextBoxGoldJamieson, 200, 1500, 0), (numericalTextBoxGoldAnderson, 300, inf, 1), (numericalTextBoxGoldTsuchiya, 300, 2500, 0),
                (numericBoxAuYokoo, 0, 3000, 0), (numericBoxAuFratanduono, 290, 310, 2),
                (numericalTextBoxPtJamieson, 200, 1500, 0), (numericalTextBoxPtHolmes, 300, inf, 1), (numericalTextBoxPtMatsui, 0, 3000, 3),
                (numericBoxPtYokoo, 0, 3000, 0), (numericBoxPtFratanduono, 290, 310, 2),
                (numericalTextBoxNaClB1Brown, 300, 1200, 0),
                (numericalTextBoxNaClB2SataPt, 290, 310, 2), (numericalTextBoxNaClB2SataMgO, 290, 310, 2), (numericalTextBoxNaClB2Ueda, 300, inf, 1),
                (numericalTextBoxNaClB2SakaiBM, 290, 310, 2), (numericalTextBoxNaClB2SakaiVinet, 290, 310, 2),
                (numericBoxCorundumDubrovinsky, 300, inf, 1),
                (numericalTextBoxArRoss, 290, 310, 2),
                (numericalTextBoxReZha, 300, 3000, 0), (numericBoxReAnz, 290, 310, 2), (numericBoxReSakai, 290, 310, 2), (numericBoxReDub, 290, 310, 2),
                (numericBoxMoZhao, 300, inf, 1),
                (numericBoxPbStrassle, 0, 300, 0),
            }.Select(e => (e.Box, e.Tmin, e.Tmax, e.Kind, e.Box.ValueForeColor)).ToArray(); // 元の文字色を覚えておき、範囲内に戻ったら戻す

            double t = numericBoxTemperature.Value, t0 = numericBoxTemperature0.Value;
            bool any = false;
            foreach (var (box, tmin, tmax, kind, foreColor) in eosValidity)
            {
                bool isPb = box == numericBoxPbStrassle;
                bool outside = t < tmin || t > tmax || (isPb && (t0 < 0 || t0 > 310));
                box.ValueForeColor = outside ? Color.Red : foreColor;
                if (!outside) { box.ToolTip = ""; continue; }
                any = true;
                var range = isPb ? "T: 0–300 K, T₀: 0–310 K" : $"{tmin}–{tmax} K";
                box.ToolTip = Crystallography.Localization.Loc(
                    en: "Outside the validity range of this scale (extrapolated).", ja: "このスケールの適用範囲外です (外挿値)。",
                    de: "Außerhalb des Gültigkeitsbereichs dieser Skala (extrapoliert).", fr: "Hors du domaine de validité de cette échelle (extrapolé).",
                    es: "Fuera del rango de validez de esta escala (extrapolado).", pt: "Fora do intervalo de validade desta escala (extrapolado).",
                    it: "Fuori dall'intervallo di validità di questa scala (estrapolato).", ru: "Вне области применимости этой шкалы (экстраполяция).",
                    zhHans: "超出该标尺的适用范围（外推值）。", zhHant: "超出該標尺的適用範圍（外推值）。", ko: "이 스케일의 적용 범위를 벗어났습니다 (외삽값).")
                    + "\r\n" + kind switch
                    {
                        0 => Crystallography.Localization.Loc(
                            en: $"Valid temperature range: {range} (range of the published table).", ja: $"有効な温度範囲: {range} (公表された表の範囲)。",
                            de: $"Gültiger Temperaturbereich: {range} (Bereich der veröffentlichten Tabelle).", fr: $"Plage de température valide : {range} (domaine de la table publiée).",
                            es: $"Rango de temperatura válido: {range} (rango de la tabla publicada).", pt: $"Faixa de temperatura válida: {range} (faixa da tabela publicada).",
                            it: $"Intervallo di temperatura valido: {range} (intervallo della tabella pubblicata).", ru: $"Допустимый диапазон температур: {range} (диапазон опубликованной таблицы).",
                            zhHans: $"有效温度范围：{range}（已发表数据表的范围）。", zhHant: $"有效溫度範圍：{range}（已發表數據表的範圍）。", ko: $"유효 온도 범위: {range} (발표된 표의 범위)."),
                        1 => Crystallography.Localization.Loc(
                            en: "Valid for T ≥ 300 K: the thermal term is a high-temperature form (linear in T − 300 K, or a high-temperature thermal-expansion fit) and is not physical below room temperature.",
                            ja: "T ≥ 300 K で有効: 熱的な項が高温向けの形 ((T − 300 K) に比例、または高温の熱膨張の式) で、室温より低温では物理的に正しくありません。",
                            de: "Gültig für T ≥ 300 K: Der thermische Term ist eine Hochtemperaturform (linear in T − 300 K oder eine Hochtemperatur-Anpassung der Wärmeausdehnung) und unterhalb der Raumtemperatur nicht physikalisch.",
                            fr: "Valable pour T ≥ 300 K : le terme thermique est une forme de haute température (linéaire en T − 300 K, ou un ajustement de la dilatation thermique à haute température) et n'est pas physique en dessous de la température ambiante.",
                            es: "Válido para T ≥ 300 K: el término térmico es una forma de alta temperatura (lineal en T − 300 K, o un ajuste de la dilatación térmica a alta temperatura) y no es físico por debajo de la temperatura ambiente.",
                            pt: "Válido para T ≥ 300 K: o termo térmico é uma forma de alta temperatura (linear em T − 300 K, ou um ajuste da expansão térmica em alta temperatura) e não é físico abaixo da temperatura ambiente.",
                            it: "Valido per T ≥ 300 K: il termine termico è una forma di alta temperatura (lineare in T − 300 K, o un fit dell'espansione termica ad alta temperatura) e non è fisico al di sotto della temperatura ambiente.",
                            ru: "Применимо при T ≥ 300 K: тепловой член записан в высокотемпературной форме (линейно по T − 300 K или как высокотемпературная аппроксимация теплового расширения) и не имеет физического смысла ниже комнатной температуры.",
                            zhHans: "适用于 T ≥ 300 K：热项为高温形式（与 T − 300 K 成线性，或高温热膨胀拟合式），在室温以下没有物理意义。",
                            zhHant: "適用於 T ≥ 300 K：熱項為高溫形式（與 T − 300 K 成線性，或高溫熱膨脹擬合式），在室溫以下沒有物理意義。",
                            ko: "T ≥ 300 K에서 유효: 열적 항이 고온용 형태(T − 300 K에 선형, 또는 고온 열팽창 피팅식)이므로 실온 이하에서는 물리적으로 맞지 않습니다."),
                        2 => Crystallography.Localization.Loc(
                            en: "This scale is a room-temperature (300 K) isotherm and does not use the temperature; it is shown in red outside 290–310 K.",
                            ja: "このスケールは室温 (300 K) の等温式で、温度を使いません。290–310 K の外では赤で表示します。",
                            de: "Diese Skala ist eine Raumtemperatur-Isotherme (300 K) und verwendet die Temperatur nicht; außerhalb von 290–310 K wird sie rot angezeigt.",
                            fr: "Cette échelle est une isotherme à température ambiante (300 K) et n'utilise pas la température ; elle est affichée en rouge hors de 290–310 K.",
                            es: "Esta escala es una isoterma a temperatura ambiente (300 K) y no usa la temperatura; se muestra en rojo fuera de 290–310 K.",
                            pt: "Esta escala é uma isoterma à temperatura ambiente (300 K) e não usa a temperatura; é mostrada em vermelho fora de 290–310 K.",
                            it: "Questa scala è un'isoterma a temperatura ambiente (300 K) e non usa la temperatura; viene mostrata in rosso fuori da 290–310 K.",
                            ru: "Эта шкала — изотерма при комнатной температуре (300 K) и не использует температуру; вне 290–310 K значение показывается красным.",
                            zhHans: "该标尺为室温（300 K）等温线，不使用温度；在 290–310 K 之外以红色显示。",
                            zhHant: "該標尺為室溫（300 K）等溫線，不使用溫度；在 290–310 K 之外以紅色顯示。",
                            ko: "이 스케일은 실온(300 K) 등온식이며 온도를 사용하지 않습니다. 290–310 K 밖에서는 빨간색으로 표시합니다."),
                        _ => Crystallography.Localization.Loc(
                            en: $"Valid range stated by the authors: up to {tmax} K.", ja: $"著者が示す適用範囲: {tmax} K まで。",
                            de: $"Von den Autoren angegebener Gültigkeitsbereich: bis {tmax} K.", fr: $"Domaine de validité indiqué par les auteurs : jusqu'à {tmax} K.",
                            es: $"Rango de validez indicado por los autores: hasta {tmax} K.", pt: $"Intervalo de validade indicado pelos autores: até {tmax} K.",
                            it: $"Intervallo di validità indicato dagli autori: fino a {tmax} K.", ru: $"Область применимости, указанная авторами: до {tmax} K.",
                            zhHans: $"作者给出的适用范围：至 {tmax} K。", zhHant: $"作者給出的適用範圍：至 {tmax} K。", ko: $"저자가 제시한 적용 범위: {tmax} K까지."),
                    };
            }
            labelEOSValidityWarning.Visible = any;
        }

        #region Gold
        public (string Reseacher, double Pressure)[] Gold()
        {
            double a = numericalTextBoxGoldA.Value, v = a * a * a;
            double a0 = numericBoxGoldA0.Value, v0 = a0 * a0 * a0;
            double t = numericBoxTemperature.Value;
            //Jamieson
            numericalTextBoxGoldJamieson.Value = EOS.Au_Jamieson(a / 10, a0 / 10, t);
            //Anderson
            numericalTextBoxGoldAnderson.Value = EOS.Au_Anderson(t, a / 10, a0 / 10);
            //Shim (Shim, Duffy & Takemura 2002) 261105Cl 変更: 旧コメント "//Sim" は誤記
            numericalTextBoxGoldSim.Value = EOS.BirchMurnaghan3rd(167, 5.0, v0, v) + EOS.MieGruneisen(4, 1, 170, 2.97, 1.0, 300, v0, t, v);
            //Tsuchiya
            numericalTextBoxGoldTsuchiya.Value = EOS.Au_Tsuchiya(a / 10, a0 / 10, t);
            //Yokoo
            numericBoxAuYokoo.Value = EOS.AuYokoo(v0, v, t);
            //Fratanduono
            numericBoxAuFratanduono.Value = EOS.Au_Fratanduono_Vinet(v0, v);

            return
            [
            ("Jamieson82",numericalTextBoxGoldJamieson.Value ),
        ("Anderson89",numericalTextBoxGoldAnderson.Value ),
        //("Sim02",numericalTextBoxGoldSim.Value ),
        ("Shim02",numericalTextBoxGoldSim.Value ),//261105Cl 変更: Sim は Shim (Shim, Duffy & Takemura 2002) の誤記
        ("Tsuchiya03",numericalTextBoxGoldTsuchiya.Value ),
        ("Yokoo09",numericBoxAuYokoo.Value ),
        ("Fratanduono21",numericBoxAuFratanduono.Value ),
        ];
        }
        #endregion

        #region Pt
        public (string Reseacher, double Pressure)[] Pt()
        {
            double a = numericalTextBoxPtA.Value;
            var v = a * a * a;
            double a0 = numericBoxPtA0.Value;
            var v0 = a0 * a0 * a0;
            double t0 = numericBoxTemperature0.Value;
            double t = numericBoxTemperature.Value;

            //Jamieson
            numericalTextBoxPtJamieson.Value = EOS.Pt_Jamieson(a / 10, a0 / 10, t);
            //Holmes
            numericalTextBoxPtHolmes.Value = EOS.Pt_Holmez(t, a / 10, a0 / 10);
            //Matsui
            numericalTextBoxPtMatsui.Value = EOS.Pt_Matsui(t, t0, a / 10, a0 / 10);
            //Yokoo
            numericBoxPtYokoo.Value = EOS.PtYokoo(v0, v, t);
            //Fratanduono
            numericBoxPtFratanduono.Value = EOS.Pt_Fratanduono_Vinet(v0, v);

            return
            [
            ("Jamieson82",numericalTextBoxPtJamieson.Value),
        ("Holmes89",numericalTextBoxPtHolmes.Value ),
        ("Matsui09",numericalTextBoxPtMatsui.Value ),
        ("Yokoo09",numericBoxPtYokoo.Value),
        ("Fratanduono21",numericBoxPtFratanduono.Value ),
        ];
        }
        #endregion

        #region Ar
        public (string Reseacher, double Pressure)[] Ar()
        {
            double a = numericalTextBoxArA.Value, v = a * a * a;
            double a0 = numericBoxArA0.Value, v0 = a0 * a0 * a0;
            double t = numericBoxTemperature.Value;
            //Jephcoat
            numericalTextBoxArJephcoat.Value = EOS.Ar_Jephcoat(a / 10, t);
            //Ross
            numericalTextBoxArRoss.Value = EOS.Ar_Ross(a / 10);

            return new (string Reseacher, double Pressure)[]
                {
        ("Ross86",numericalTextBoxArRoss.Value),
        ("Jephcoat98",numericalTextBoxArJephcoat.Value),
                };
        }
        #endregion

        #region MgO
        public (string Reseacher, double Pressure)[] MgO()
        {
            double a = numericalTextBoxMgOA.Value, v = a * a * a;
            double a0 = numericBoxMgOA0.Value, v0 = a0 * a0 * a0;
            double t = numericBoxTemperature.Value;
            //Jacson
            numericalTextBoxMgOJacson.Value = EOS.BirchMurnaghan3rd(162.5, 4.13, v0, v) + EOS.MieGruneisen(4, 2, 673, 1.41, 1.3, 300, v0, t, v);
            //Dewaele
            numericalTextBoxMgODewaele.Value = EOS.BirchMurnaghan3rd(161, 3.94, v0, v) + EOS.MieGruneisen(4, 2, 800, 1.45, 0.8, 300, v0, t, v);
            //Aizawa
            numericalTextBoxMgOAizawa.Value = EOS.BirchMurnaghan3rd(160, 4.15, v0, v) + EOS.MieGruneisen(4, 2, 773, 1.41, 0.7, 300, v0, t, v);
            //Tange Vinet
            numericBoxMgOTangeVinet.Value = EOS.MgO_Tange_Vinet(t, v0, v);
            //Tange BM
            numericBoxMgOTangeBM.Value = EOS.MgO_Tange_BM(t, v0, v);

            return
           [
            ("Jackson98",numericalTextBoxMgOJacson.Value),
        ("Dewaele00",numericalTextBoxMgODewaele.Value ),
        ("Aizawa06",numericalTextBoxMgOAizawa.Value ),
        ("Tange09Vinet",numericBoxMgOTangeVinet.Value ),
        ("Tange09BM",numericBoxMgOTangeBM.Value ),
       ];
        }
        #endregion

        #region NaClB2
        public (string Reseacher, double Pressure)[] NaClB2()
        {
            double a = numericalTextBoxNaClB2A.Value, v = a * a * a;
            double a0 = numericalTextBoxNaClB2A0.Value, v0 = a0 * a0 * a0;
            double t = numericBoxTemperature.Value;
            //Sata Pt
            numericalTextBoxNaClB2SataPt.Value = EOS.NaClB2_Sata(a, 31.14, 143.5);
            //Sata MgO
            numericalTextBoxNaClB2SataMgO.Value = EOS.NaClB2_Sata(a, 32.15, 141.0);
            //Ueda
            numericalTextBoxNaClB2Ueda.Value = EOS.NaClB2_Ueda(a, t);
            //Sakai BM
            numericalTextBoxNaClB2SakaiBM.Value = EOS.NaClB2_SakaiBM(a);
            //Sakai Vinet
            numericalTextBoxNaClB2SakaiVinet.Value = EOS.NaClB2_SakaiVinet(a);

            return
          [
            ("Sata02Pt",numericalTextBoxNaClB2SataPt.Value),
        ("Sata02MgO",numericalTextBoxNaClB2SataMgO.Value ),
        ("Ueda08",numericalTextBoxNaClB2Ueda.Value),
        ("Sakai11BM",numericalTextBoxNaClB2SakaiBM.Value ),
        ("Sakai11Vinet",numericalTextBoxNaClB2SakaiVinet.Value ),
      ];
        }
        #endregion

        #region NaClB1
        public (string Reseacher, double Pressure)[] NaClB1()
        {
            double a = numericalTextBoxNaClB1A.Value;
            double a0 = numericBoxNaClB1A0.Value;
            double t = numericBoxTemperature.Value;
            //Brown
            numericalTextBoxNaClB1Brown.Value = EOS.NaCl_Brown(a / 10, a0 / 10, t);
            //Matsui
            numericalTextBoxNaClB1Matsui.Value = EOS.NaClB1_Matsui(a0, a, t);

            return new (string Reseacher, double Pressure)[]
                {
                 ("Brown99",numericalTextBoxNaClB1Brown.Value),
             ("Matsui12",numericalTextBoxNaClB1Matsui.Value ),
                };
        }
        #endregion

        #region Al2O3
        public (string Reseacher, double Pressure)[] Corundum()
        {
            double v = numericalTextBoxColundumV.Value;
            double v0 = numericBoxColundumV0.Value;
            double t = numericBoxTemperature.Value;

            //Dubrovinsky
            numericBoxCorundumDubrovinsky.Value = EOS.Corundum_Dubrovinsky(v0, v, t);

            return new (string Reseacher, double Pressure)[]
                {
                 ("Dubrovinsky98",numericBoxCorundumDubrovinsky.Value),
                };
        }
        #endregion

        #region Re
        public (string Reseacher, double Pressure)[] Re()
        {
            double v = numericBoxReV.Value;
            double v0 = numerictBoxReV0.Value;
            double t = numericBoxTemperature.Value;
            //Zha04
            numericalTextBoxReZha.Value = EOS.Re_Zha(v0, v, t);
            //Anz
            numericBoxReAnz.Value = EOS.Vinet(352.6, 4.56, 29.467, v);
            //Sakai
            numericBoxReSakai.Value = EOS.Vinet(358, 4.8, 29.47, v);
            //Dub
            numericBoxReDub.Value = EOS.BirchMurnaghan4th(342, 6.15, -0.029, 29.46 / v);

            return new (string Reseacher, double Pressure)[]
             {
                 ("Zha04",numericalTextBoxReZha.Value),
                 ("Anz",numericBoxReAnz.Value),
                 ("Sakai",numericBoxReSakai.Value),
                 ("Dub",numericBoxReDub.Value),
             };
        }
        #endregion

        #region Mo
        public (string Reseacher, double Pressure)[] Mo()
        {
            double v = numericBoxMoV.Value;
            double v0 = numericBoxMoV0.Value;
            double t = numericBoxTemperature.Value;

            //Huang+16
            numericBoxMoHuang.Value = EOS.BirchMurnaghan3rd(255, 4.25, v0 / v) + EOS.MieGruneisen(2, 1, 470, 2.01, 0.6, 300, v0, t, v);

            //Zhao+00
            numericBoxMoZhao.Value = new EOS()
            {
                Z = 2,
                K0 = 268,
                Kp0 = 3.81,
                Kpp0 = -1.41E-2,
                KperT = -2.13E-2,
                A = 1.31,
                B = 11.2,
                T0 = 300,
                Temperature = t,
                CellVolume0 = v0,
                ThermalPressureApproach = ThermalPressure.T_dependence_BM,
                IsothermalPressureApproach = IsothermalPressure.BM4
            }.GetPressure(v);

            return
               [
                     ("Huang16",numericBoxMoHuang.Value),
                 ("Zhao00",numericBoxMoZhao.Value),
           ];
        }
        #endregion

        #region Pb
        (double T, double B, double Bp)[] PbVinet = new (double T, double B, double Bprime)[] {
        #region
        (0,     48.3298,    5.4511),
        (20,    48.2387,    5.4542),
        (40,    47.9462,    5.4644),
        (60,    47.5019,    5.4801),
        (80,    47.0000,    5.4979),
        (100,   46.4875 ,   5.5165),
        (120,   45.9743 ,   5.5353),
        (140,   45.4578 ,   5.5545),
        (160,   44.9356 ,   5.5742),
        (180,   44.4073 ,   5.5945),
        (200,   43.8743 ,   5.6152),
        (220,   43.3386 ,   5.6364),
        (240,   42.8019 ,   5.6580),
        (260,   42.2659 ,   5.6799),
        (280,   41.7317 ,   5.7021),
        (300,   41.2000 ,   5.7245),
        #endregion
    };

        (double T, double A0)[] PbA0 = new (double T, double A0)[]
        {
        #region
        (   0   ,   4.91366 ),
(   5   ,   4.91370 ),
(   10  ,   4.91378 ),
(   15  ,   4.91391 ),
(   20  ,   4.91410 ),
(   25  ,   4.91436 ),
(   30  ,   4.91469 ),
(   35  ,   4.91508 ),
(   40  ,   4.91552 ),
(   45  ,   4.91601 ),
(   50  ,   4.91654 ),
(   55  ,   4.91710 ),
(   60  ,   4.91768 ),
(   65  ,   4.91828 ),
(   70  ,   4.91890 ),
(   75  ,   4.91952 ),
(   80  ,   4.92014 ),
(   85  ,   4.92077 ),
(   90  ,   4.92140 ),
(   95  ,   4.92203 ),
(   100 ,   4.92267 ),
(   105 ,   4.92330 ),
(   110 ,   4.92394 ),
(   115 ,   4.92457 ),
(   120 ,   4.92521 ),
(   125 ,   4.92585 ),
(   130 ,   4.92650 ),
(   135 ,   4.92714 ),
(   140 ,   4.92779 ),
(   145 ,   4.92844 ),
(   150 ,   4.92909 ),
(   155 ,   4.92975 ),
(   160 ,   4.93041 ),
(   165 ,   4.93108 ),
(   170 ,   4.93174 ),
(   175 ,   4.93241 ),
(   180 ,   4.93308 ),
(   185 ,   4.93376 ),
(   190 ,   4.93444 ),
(   195 ,   4.93511 ),
(   200 ,   4.93580 ),
(   205 ,   4.93648 ),
(   210 ,   4.93717 ),
(   215 ,   4.93785 ),
(   220 ,   4.93854 ),
(   225 ,   4.93923 ),
(   230 ,   4.93993 ),
(   235 ,   4.94062 ),
(   240 ,   4.94131 ),
(   245 ,   4.94201 ),
(   250 ,   4.94270 ),
(   255 ,   4.94340 ),
(   260 ,   4.94410 ),
(   265 ,   4.94480 ),
(   270 ,   4.94550 ),
(   275 ,   4.94619 ),
(   280 ,   4.94689 ),
(   285 ,   4.94760 ),
(   290 ,   4.94830 ),
(   295 ,   4.94900 ),
(   300 ,   4.94970 ),
(   305 ,   4.95040 ),
(   310 ,   4.95110 ),
            #endregion
        };

        MathNet.Numerics.Interpolation.IInterpolation PbInterA0, PbInterB, PbInterBp;
        public (string Reseacher, double Pressure)[] Pb()
        {
            if (PbInterA0 == null)
            {
                PbInterA0 = Interpolate.Linear(PbA0.Select(e => e.T).ToArray(), PbA0.Select(e => e.A0));
                PbInterB = Interpolate.Linear(PbVinet.Select(e => e.T).ToArray(), PbVinet.Select(e => e.B));
                PbInterBp = Interpolate.Linear(PbVinet.Select(e => e.T).ToArray(), PbVinet.Select(e => e.Bp));
            }
            var a = numericBoxPbA.Value;
            var a0 = numericBoxPbA0.Value;
            var t = numericBoxTemperature.Value;
            var t0 = numericBoxTemperature0.Value;

            var scale = a0 / PbInterA0.Interpolate(t0);
            var a0atT = scale * PbInterA0.Interpolate(t);

            var x = a / a0atT;

            var B = PbInterB.Interpolate(t);
            var Bp = PbInterBp.Interpolate(t);

            numericBoxPbStrassle.Value = 3 * B * (1 - x) / x / x * Math.Exp(1.5 * (Bp - 1) * (1 - x));

            return new (string Reseacher, double Pressure)[]
               {
                 ("Strassle14",numericBoxPbStrassle.Value),
               };
        }
        #endregion

        #endregion



        #region Drag&Drop
        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            //コントロール内にドロップされたとき実行される
            //ドロップされたすべてのファイル名を取得する
            string[] fileNames = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            //ListBoxに追加する
            if (fileNames.Length == 1)
            {
                skipFitting = true;
                Original = readFile(fileNames[0]);
                CalcSmoothingAndDifferentiation();
                graphControlTop.Profile = OriginalSmooth;
                graphControlBottom.Profile = BottomProfileSmooth;
                skipFitting = false;
                SearchBandEdge();

            }
        }

        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; //ドラッグされたデータ形式を調べ、ファイルのときはコピーとする
            else
                e.Effect = DragDropEffects.None;//ファイル以外は受け付けない
        }
        #endregion

        #region メニューアイテム

        private void menuItemExport_Click(object sender, EventArgs e)
        {

            if (graphControlBottom.Profile != null && graphControlBottom.Profile.Pt != null && graphControlBottom.Profile.Pt.Count != 0)
            {
                var originalPt = Original.Pt.ToArray();
                var smoothPt = OriginalSmooth.Pt.ToArray();
                PointD[] r1Pt = graphControlBottom.ProfileList.Count() > 1 ? graphControlBottom.ProfileList[1].Pt.ToArray() : null;
                PointD[] r2Pt = graphControlBottom.ProfileList.Count() > 2 ? graphControlBottom.ProfileList[2].Pt.ToArray() : null;

                var textList = new List<string>();

                if (radioButtonRubyFluorescence.Checked)
                    textList.Add("X, Y (original), Y (smoothed),,X(R1),Y(R1),Y(R1_Bg),,X (R2),Y(R2), Y(R2_Bg)");
                else
                    textList.Add("X, Y (original), Y (smoothed),, X(Edge),Y(Edge)");

                var dlg = new System.Windows.Forms.SaveFileDialog { Filter = "*.csv|*.csv" };//260703Cl 変更: Microsoft.Win32 (Registry用using) との名前衝突回避 (旧: new SaveFileDialog)
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < smoothPt.Length; i++)
                        textList.Add($"{smoothPt[i].X},{originalPt[i].Y},{smoothPt[i].Y}");

                    if (radioButtonRubyFluorescence.Checked && r1Pt != null && r2Pt != null)
                        for (int i = 0; i < textList.Count && (i < r1Pt.Length || i < r2Pt.Length); i++)
                        {
                            textList[i + 1] += i < r1Pt.Length ? $",,{r1Pt[i].X},{r1Pt[i].Y},{PF[0].B1 + PF[0].B2 * (r1Pt[i].X - PF[0].X)}" : ",,,,";
                            textList[i + 1] += i < r2Pt.Length ? $",,{r2Pt[i].X},{r2Pt[i].Y},{PF[1].B1 + PF[1].B2 * (r2Pt[i].X - PF[1].X)}" : ",,,,";
                        }
                    else if (radioButtonDiamondRaman.Checked && r1Pt != null)
                        for (int i = 0; i < textList.Count && i < r1Pt.Length; i++)
                            textList[i + 1] += i < r1Pt.Length ? $",,{r1Pt[i].X},{r1Pt[i].Y}" : ",,,";


                    using StreamWriter sw = new(dlg.FileName);
                    for (int i = 0; i < textList.Count; i++)
                        sw.WriteLine(textList[i]);

                    sw.Close();
                }
            }
        }


        //メニューから呼び出されるFileRead
        private void menuItemFileRead_Click(object sender, System.EventArgs e)
        {
            var Dlg = new System.Windows.Forms.OpenFileDialog();//260703Cl 変更: Microsoft.Win32 との名前衝突回避 (旧: new OpenFileDialog)
            if (Dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (!File.Exists(Dlg.FileName)) return;  // ファイルの有無をチェック
                    Original = readFile(Dlg.FileName);
                    CalcSmoothingAndDifferentiation();
                    graphControlTop.Profile = OriginalSmooth;
                    graphControlBottom.Profile = BottomProfileSmooth;					//プロファイル描画
                    SearchBandEdge();
                }
                catch
                {
                    //260704Cl 変更: 日本語直書き → 11言語 Loc (旧: MessageBox.Show("ファイルを開けません");)
                    MessageBox.Show(Crystallography.Localization.Loc(
                        en: "Cannot open the file.", ja: "ファイルを開けません", de: "Die Datei kann nicht geöffnet werden.",
                        fr: "Impossible d'ouvrir le fichier.", es: "No se puede abrir el archivo.", pt: "Não é possível abrir o arquivo.",
                        it: "Impossibile aprire il file.", ru: "Не удаётся открыть файл.", zhHans: "无法打开文件。",
                        zhHant: "無法開啟檔案。", ko: "파일을 열 수 없습니다."));
                    return;
                }
            }
        }

        private void menuItemWatchFile_Click(object sender, System.EventArgs e)
        {
            if (watchNewFileToolStripMenuItem.Checked)
                timer.Start();
            else
                timer.Stop();
        }

        private bool fileWatchBusy = false; // 261105Cl 追加: timer_Tick の再入防止 (下の await 中や MessageBox 表示中に次の Tick が来ても、同じ更新を重ねて読まない・ダイアログを積まない)

        //261105Cl 変更: UI スレッドで Thread.Sleep(200) を毎秒 2 回 (ファイル未更新でも) 呼び、読み込み後は UI が毎秒約 0.4 s 固まっていた (Q-13)。
        //  1 回目の Sleep の役目は「書き込み側 (分光器ソフト) が書き終えるのを待ってから読む」なので、更新を検出したときだけ await Task.Delay(200) で待つ (UI は塞がない)。
        //  2 回目の Sleep は読み込み後の何もしない待ちで、UI を止める以外の効果が無いので廃止。検出 → 200 ms 後に再読込、という挙動は従来どおり。
        //private void timer_Tick(object sender, EventArgs e)
        private async void timer_Tick(object sender, EventArgs e)
        {
            if (fileWatchBusy) return; // 261105Cl 追加
            //ファイルの更新を監視
            if (File.Exists(currentFileName))
            {
                var time = File.GetLastWriteTime(currentFileName);
                //Thread.Sleep(200);//261105Cl 変更: 下の await Task.Delay(200) へ (更新があったときだけ待つ)
                if (time > lastWriteTime)
                {
                    fileWatchBusy = true; // 261105Cl 追加
                    await Task.Delay(200); // 261105Cl 追加: 書き込み側が書き終えるのを待つ (旧 Thread.Sleep(200) の役目)。UI スレッドは塞がない
                    try
                    {
                        if (IsDisposed) return; // 261105Cl 追加: 待っている間にウィンドウが閉じられた場合は何もしない (finally で解除)
                        Original = readFile(currentFileName);
                        CalcSmoothingAndDifferentiation();
                        graphControlTop.Profile = OriginalSmooth;
                        graphControlBottom.Profile = BottomProfileSmooth;
                        SearchBandEdge();
                    }
                    catch
                    {
                        //260704Cl 変更: 日本語直書き → 11言語 Loc (旧: MessageBox.Show("ファイルを開けません");)
                    MessageBox.Show(Crystallography.Localization.Loc(
                        en: "Cannot open the file.", ja: "ファイルを開けません", de: "Die Datei kann nicht geöffnet werden.",
                        fr: "Impossible d'ouvrir le fichier.", es: "No se puede abrir el archivo.", pt: "Não é possível abrir o arquivo.",
                        it: "Impossibile aprire il file.", ru: "Не удаётся открыть файл.", zhHans: "无法打开文件。",
                        zhHant: "無法開啟檔案。", ko: "파일을 열 수 없습니다."));
                        return;
                    }
                    finally { fileWatchBusy = false; } // 261105Cl 追加: MessageBox を閉じた後に解除 (次の Tick で再試行するのは従来どおり)
                }
                //Thread.Sleep(200);//261105Cl 変更: 廃止 (UI を止めるだけの待ち)
            }
        }

        #endregion


        private void numericBoxDecimalPlaces_ValueChanged(object sender, EventArgs e)
        {
            textBoxNu_TextChanged(sender, e);
            calcRubyPressure();

            numericalTextBoxGoldJamieson.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericalTextBoxGoldAnderson.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericalTextBoxGoldSim.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericalTextBoxGoldTsuchiya.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericBoxAuYokoo.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericBoxAuFratanduono.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;

            numericalTextBoxPtJamieson.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericalTextBoxPtHolmes.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericalTextBoxPtMatsui.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericBoxPtYokoo.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericBoxPtFratanduono.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;

            numericalTextBoxArJephcoat.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericalTextBoxArRoss.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;

            numericalTextBoxMgOJacson.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericalTextBoxMgODewaele.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericalTextBoxMgOAizawa.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericBoxMgOTangeVinet.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericBoxMgOTangeBM.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;

            numericalTextBoxNaClB2SataPt.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericalTextBoxNaClB2SataMgO.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericalTextBoxNaClB2Ueda.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericalTextBoxNaClB2SakaiBM.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericalTextBoxNaClB2SakaiVinet.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;


            numericalTextBoxNaClB1Brown.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericalTextBoxNaClB1Matsui.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;

            numericBoxCorundumDubrovinsky.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;

            numericalTextBoxReZha.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericBoxReAnz.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericBoxReSakai.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericBoxReDub.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;

            numericBoxMoHuang.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
            numericBoxMoZhao.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;

            numericBoxPbStrassle.DecimalPlaces = numericBoxDecimalPlaces.ValueInteger;
        }

    }



}
