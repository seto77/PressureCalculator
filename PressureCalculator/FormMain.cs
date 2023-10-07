using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using Crystallography;
using Microsoft.Win32;
using System.Linq;

namespace PressureCalculator
{





    /// <summary>
    /// Form1 の概要の説明です。
    /// </summary>
    public partial class FormMain : Form
    {

        private DateTime lastWriteTime;
        public string currentFileName = "";

        private double fittingRangeDiamond = 6;
        private double fittingRangeRuby = 3;

        #region ロード、クローズ
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, System.EventArgs e)
        {
            this.Text = Version.Software + "  " + Version.VersionAndDate;

            MouseRange = false;

            timer.Start();

            ReadInitialRegistry();

            radioButtonMode_CheckedChanged(new object(), new EventArgs());

            numericBoxRubyRefR1_ValueChanged(sender, e);
        }
        private void FormDiamondRaman_Closed(object sender, System.EventArgs e)
        {
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
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
                numericBoxRubyRefR1.Text = (string)regKey.GetValue("numericBoxRubyR1_0.Text", numericBoxRubyRefR1.Text);
                numericBoxRubyRefT.Text = (string)regKey.GetValue("numericBoxRubyRefT.Text", numericBoxRubyRefT.Text);
                numericBoxRubyRefR1.Text = (string)regKey.GetValue("numericBoxRubyRefR1.Text", numericBoxRubyRefT.Text);

                this.radioButtonTempUnitK.Checked = (string)regKey.GetValue("radioButtonTempUnitK.Checked", "True") == "True";
                this.radioButtonTempUnitC.Checked = (string)regKey.GetValue("radioButtonTempUnitC.Checked", "True") == "True";


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
            regKey.SetValue("textBoxMaoRamda0.Text", numericBoxRubyRefR1.Text);
            regKey.SetValue("numericBoxRubyRefT.Text", numericBoxRubyRefT.Text);
            regKey.SetValue("numericBoxRubyRefR1.Text", numericBoxRubyRefR1.Text);

            regKey.SetValue("radioButtonTempUnitK.Checked", radioButtonTempUnitK.Checked);
            regKey.SetValue("radioButtonTempUnitC.Checked", radioButtonTempUnitC.Checked);


            regKey.Close();
        }
        #endregion



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
                                count += Original.Pt[i + j].Y * Math.Exp(-j * j / n / n);
                                weight += Math.Exp(-j * j / n / n);
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
                                count += BottomProfile.Pt[i + j].Y * Math.Exp(-j * j / n / n);
                                weight += Math.Exp(-j * j / n / n);
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
                var pf = new PeakFunction[] { new PeakFunction(centerX, range, range, PeakFunctionForm.PseudoVoigt) };

                FittingPeak.FitMultiPeaksThread(pt, true, 0, ref pf);

                textBoxFittingInformation.Text = $"Edge:\t  X = {pf[0].X:f3}\tFWHM = {pf[0].Hk:f3}\r\n";

                var p = new Profile();
                for (double x = pf[0].X - range * 0.9; x < pf[0].X + range * 0.9; x += range / 200)
                    p.Pt.Add(new PointD(x, -pf[0].GetValue(x, false) - pf[0].B1 - pf[0].B2 * (x - pf[0].X)));
                p.Color = Color.Red;

                graphControlBottom.ReplaceProfile(1, p);
                graphControlBottom.ReplaceProfile(2, null);
                graphControlBottom.ReplaceProfile(3, null);

                textBoxDiamondRamanNu.Text = pf[0].X.ToString();
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
                PeakFunction[] pf = new PeakFunction[2];
                pf[0] = new PeakFunction(centerX, range / 2, range, PeakFunctionForm.PseudoVoigt);
                pf[1] = new PeakFunction(centerX - 1.5, range / 2, range, PeakFunctionForm.PseudoVoigt);

                FittingPeak.FitMultiPeaksThread(pt, true, 0, ref pf);

                textBoxFittingInformation.Text = $"R1:\t  X = {pf[0].X:f3}\tFWHM = {pf[0].Hk:f3}\r\n";
                textBoxFittingInformation.Text += $"R2:\t  X = {pf[1].X:f3}\tFWHM = {pf[1].Hk:f3}";

                Profile p1 = new Profile();
                for (double x = pf[0].X - range * 0.9; x < pf[0].X + range * 0.9; x += range / 200)
                    p1.Pt.Add(new PointD(x, pf[0].GetValue(x, false) + pf[0].B1 + pf[0].B2 * (x - pf[0].X)));
                p1.Color = Color.LightGreen;

                Profile p2 = new Profile();
                for (double x = pf[1].X - range * 0.9; x < pf[1].X + range * 0.9; x += range / 200)
                    p2.Pt.Add(new PointD(x, pf[1].GetValue(x, false) + pf[1].B1 + pf[1].B2 * (x - pf[1].X)));
                p2.Color = Color.LightCyan;

                Profile p3 = new Profile();
                for (double x = pf[1].X - range * 0.9; x < pf[0].X + range * 0.9; x += range / 200)
                    p3.Pt.Add(new PointD(x, pf[1].GetValue(x, false) + pf[0].GetValue(x, false) + pf[1].B1 + pf[1].B2 * (x - pf[1].X)));
                p3.Color = Color.LightPink;


                graphControlBottom.ReplaceProfile(1, p1);
                graphControlBottom.ReplaceProfile(2, p2);
                graphControlBottom.ReplaceProfile(3, p3);

                numericBoxRubyR1.Value = pf[0].X;
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
                    (Convert.ToDouble(textBoxAkahama2004A.Text) + Convert.ToDouble(textBoxAkahama2004B.Text) * nu + Convert.ToDouble(textBoxAkahama2004C.Text) * 0.0001 * nu * nu).ToString("f2");
                //Akahama2006
                textBoxDiamondAkahama2006P.Text =
                    (Convert.ToDouble(textBoxAkahama2006K0.Text) * nuPerNu0 * (1 + 0.5 * (Convert.ToDouble(textBoxAkahama2006K0Prime.Text) - 1) * nuPerNu0)).ToString("f2");
                //Fratanduno 2021 Low
                textBoxDiamondFratandunoLow.Text = (530.77 * nuPerNu0 + 753.83 * nuPerNu0 * nuPerNu0).ToString("f2");
                //Fratanduno 2021 High
                textBoxDiamondFratandunoHigh.Text = (199.49 - 852.78 * nuPerNu0 + 3103.8 * nuPerNu0 * nuPerNu0).ToString("f2");

            }
            catch
            {
                MessageBox.Show("適切な数値を入れてください");
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
                labelBottomTitle.Text = "First Differentiation";
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
                labelBottomTitle.Text = "Ruby Fluorescence";
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
            textBoxMaoP.Text = (numericBoxMaoA.Value * (Math.Pow(r, 5) - 1) / 5).ToString("f2");

            //MaoQuasi
            textBoxMaoQuasiP.Text = (numericBoxMaoQuasiA.Value * (Math.Pow(r, 7.665) - 1) / 7.665).ToString("f2");

            //MaoQuasi
            textBoxMaoHydroP.Text = (numericBoxMaoHydroA.Value * (Math.Pow(r, 7.715) - 1) / 7.715).ToString("f2");

            var delta = (numericBoxRubyR1.Value - numericBoxRubyR1_0.Value) / numericBoxRubyR1_0.Value;
            //Shen et al
            textBoxShenP.Text = (numericBoxShenA.Value * (delta + delta * delta * 5.63)).ToString("f2");
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

        public double temperature = 300;

        private void textBoxNumOnly_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if ((e.KeyChar < '.' || e.KeyChar > '9') && e.KeyChar != '\b')
                e.Handled = true;
        }
        public bool skipTextChangeEvent = false;
        public void textBox_TextChanged(object sender, System.EventArgs e)
        {
            if (skipTextChangeEvent) return;
            try
            {
                temperature = Convert.ToDouble(textBoxT.Text);
            }
            catch { };
            Gold_Jamieson();
            Gold_Sim();
            Gold_Anderson();
            Gold_Tsuchiya();
            NaClB1_Brown();
            NaClB2_SataPt();
            NaClB2_SataMgO();
            Pt_Holmes();
            Pt_Jamieson();
            MgO_Jackson();
            MgO_Dewaele();
            MgO_Aizawa();
            Corundum_Dubrovinsky();
            Ar_Ross();
            Ar_Jephcoat();
            Re_Zha();
        }

        private void Ar_Jephcoat()
        {

            double P = 0;
            try { P = EOS.Ar_Jephcoat(Convert.ToDouble(textBoxArA.Text) / 10, temperature); }
            catch { }
            textBoxArJephcoat.Text = P.ToString("f3");
        }

        private void Ar_Ross()
        {

            double P = 0;
            try { P = EOS.Ar_Ross(Convert.ToDouble(textBoxArA.Text) / 10); }
            catch { }
            textBoxArRoss.Text = P.ToString("f3");
        }



        private void MgO_Aizawa()
        {
            double P = 0;
            try
            {
                P = EOS.MieGruneisen(4, Convert.ToDouble(textBoxMgOA.Text) / 10, Convert.ToDouble(textBoxMgOA0.Text) / 10, temperature,
                    160, 4.15, 1.41, 0.7, 773);
            }
            catch { }
            textBoxMgOAizawa.Text = P.ToString("f3");
        }

        private void MgO_Dewaele()
        {
            double P = 0;
            try
            {
                P = EOS.MieGruneisen(4, Convert.ToDouble(textBoxMgOA.Text) / 10, Convert.ToDouble(textBoxMgOA0.Text) / 10, temperature,
                    161, 3.94, 1.45, 0.8, 800);
            }
            catch { }
            textBoxMgODewaele.Text = P.ToString("f3");
        }

        private void MgO_Jackson()
        {
            double P = 0;
            try
            {
                P = EOS.MieGruneisen(4, Convert.ToDouble(textBoxMgOA.Text) / 10, Convert.ToDouble(textBoxMgOA0.Text) / 10, temperature,
                     162.5, 4.13, 1.41, 1.3, 673);
            }
            catch { }
            textBoxMgOJacson.Text = P.ToString("f3");
        }

        private void Gold_Jamieson()
        {
            double P = 0;
            try
            {
                P = EOS.Au_Jamieson(
                    Convert.ToDouble(textBoxGold_a.Text) / 10,
                    Convert.ToDouble(textBoxGold_a0.Text) / 10,
                    Convert.ToDouble(textBoxT.Text));
            }
            catch { }
            textBoxGoldJamieson.Text = P.ToString("f3");
        }

        private void Gold_Sim()
        {
            double P = 0;
            try
            {
                P = EOS.MieGruneisen(4, Convert.ToDouble(textBoxGold_a.Text) / 10, Convert.ToDouble(textBoxGold_a0.Text) / 10, temperature,
                    167, 5.0, 2.97, 1.0, 170);
            }
            catch { }
            textBoxGoldSim.Text = P.ToString("f3");
        }

        private void Gold_Anderson()
        {
            double P = 0;
            try
            {
                P = EOS.Au_Anderson(Convert.ToDouble(textBoxT.Text),
                    Convert.ToDouble(textBoxGold_a.Text) / 10,
                    Convert.ToDouble(textBoxGold_a0.Text) / 10);
                textBoxGoldAnderson.Text = P.ToString("f3");
            }
            catch { }
        }






        private void Gold_Tsuchiya()
        {
            double P = 0;
            try
            {
                P = EOS.Au_Tsuchiya(
                    Convert.ToDouble(textBoxGold_a.Text) / 10,
                    Convert.ToDouble(textBoxGold_a0.Text) / 10
                    , Convert.ToDouble(textBoxT.Text));
                textBoxGoldTsuchiya.Text = P.ToString("f3");
            }
            catch { }
        }

        private void NaClB1_Brown()
        {
            double P = 0;
            try
            {
                //P=EOS.NaClB1_Brown(Convert.ToDouble(textBoxNaClB1_a.Text)/10,temperature);
                //P = EOS.NaClB1_Sata(Convert.ToDouble(textBoxNaClB1_a.Text) / 10);
                P = EOS.NaCl_Brown(Convert.ToDouble(textBoxNaClB1_a.Text) / 10,
                    Convert.ToDouble(textBoxNaClB1_a0.Text) / 10
                    , Convert.ToDouble(textBoxT.Text));
            }
            catch { }
            textBoxNaClB1Brown.Text = P.ToString("f3");
        }

        private void NaClB2_SataPt()
        {
            double P = 0;
            try
            {
                P = EOS.NaClB2_Sata(Convert.ToDouble(textBoxNaClB2_a.Text), 31.14, 143.5);
            }
            catch { }
            textBoxNaClB2SataPt.Text = P.ToString("f3");
        }
        private void NaClB2_SataMgO()
        {
            double P = 0;
            try
            {
                P = EOS.NaClB2_Sata(Convert.ToDouble(textBoxNaClB2_a.Text), 32.15, 141.0);
            }
            catch { }
            textBoxNaClB2SataMgO.Text = P.ToString("f3");
        }


        private void FormEOS_Load(object sender, System.EventArgs e)
        {

        }

        private void Pt_Holmes()
        {
            double P = 0;
            try
            {
                P = EOS.Pt_Holmez(Convert.ToDouble(textBoxT.Text),
                    Convert.ToDouble(textBoxPtA.Text) / 10,
                    Convert.ToDouble(textBoxPtA0.Text) / 10);
                textBoxPtHolems.Text = P.ToString("f3");
            }
            catch { }
        }
        private void Pt_Jamieson()
        {
            double P = 0;
            try
            {
                P = EOS.Pt_Jamieson(
                    Convert.ToDouble(textBoxPtA.Text) / 10,
                    Convert.ToDouble(textBoxPtA0.Text) / 10,
                    Convert.ToDouble(textBoxT.Text));
                textBoxPtJamieson.Text = P.ToString("f3");
            }
            catch { }
        }

        private void Corundum_Dubrovinsky()
        {
            double P = 0;
            try
            {
                P = EOS.Corundum_Dubrovinsky(
                    Convert.ToDouble(textBoxColundumV0.Text),
                    Convert.ToDouble(textBoxCorundumV.Text),
                    Convert.ToDouble(textBoxT.Text));
                textBoxCorundumDubrovinsky.Text = P.ToString("f3");
            }
            catch { }
        }

        private void Re_Zha()
        {
            double P = 0;
            try
            {
                P = EOS.Re_Zha(
                    Convert.ToDouble(textBoxReV0.Text),
                    Convert.ToDouble(textBoxReV.Text),
                    Convert.ToDouble(textBoxT.Text));
                textBoxReZha.Text = P.ToString("f3");
            }
            catch { }
        }

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

            if (graphControlTop.Profile != null && graphControlTop.Profile.Pt != null && graphControlTop.Profile.Pt.Count != 0)
            {
                var pt = graphControlTop.Profile.Pt.ToArray();
                //クリップボードにcsvデータを保存
                var dlg = new SaveFileDialog { Filter = "*.csv|*.csv" };
                if (dlg.ShowDialog() == DialogResult.OK)
                    using (var sw = new StreamWriter(dlg.FileName))
                    {
                        for (int i = 0; i < pt.Length; i++)
                            sw.WriteLine(pt[i].X.ToString() + "," + pt[i].Y.ToString());
                        sw.Close();
                    }
            }
        }


        //メニューから呼び出されるFileRead
        private void menuItemFileRead_Click(object sender, System.EventArgs e)
        {
            var Dlg = new OpenFileDialog();
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
                    MessageBox.Show("ファイルを開けません");
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

        private void timer_Tick(object sender, EventArgs e)
        {
            //ファイルの更新を監視
            if (File.Exists(currentFileName))
            {
                var time = File.GetLastWriteTime(currentFileName);
                Thread.Sleep(200);
                if (time > lastWriteTime)
                {
                    try
                    {
                        Original = readFile(currentFileName);
                        CalcSmoothingAndDifferentiation();
                        graphControlTop.Profile = OriginalSmooth;
                        graphControlBottom.Profile = BottomProfileSmooth;
                        SearchBandEdge();
                    }
                    catch
                    {
                        MessageBox.Show("ファイルを開けません");
                        return;
                    }
                }
                Thread.Sleep(200);
            }
        }

        #endregion

    }



}
