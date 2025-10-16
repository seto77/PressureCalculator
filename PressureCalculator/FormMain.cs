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
using System.Windows.Forms;

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

        PeakFunction[] PF;

        string StringFormat => $"f{numericBoxDecimalPlaces.ValueInteger}";


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
            //Sim
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
        ("Sim02",numericalTextBoxGoldSim.Value ),
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

                var dlg = new SaveFileDialog { Filter = "*.csv|*.csv" };
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
