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
    partial class FormMain : System.Windows.Forms.Form
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コントロール
        public Profile Original, BottomProfile;
        public Profile OriginalSmooth, BottomProfileSmooth;
        public double UppestKayser, UpperKayser, LowestKayser, LowerKayser, UppestCount, UpperCount, LowestCount, LowerCount, UpperDiff, LowerDiff, UppestDiff, LowestDiff;
        public Bitmap BmpOriginal, BmpDifferentiation;
        public Graphics gOriginal, gDifferentiation;
        public bool MouseRange = true;


        private Label label2;
        private NumericUpDown numericUpDownDifferentiationRunningAverage;
        private NumericUpDown numericUpDownOriginalRunningAverage;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox textBoxAkahama2004A;
        private TextBox textBoxAkahama2004B;
        private TextBox textBoxAkahama2004C;
        private TextBox textBoxDiamondAkahama2004P;
        private NumericUpDown numericUpDownOriginalGaussian;
        private Label label12;
        private NumericUpDown numericUpDownDifferentiationGaussian;
        private Label label14;
        private Label labelBottomTitle;
        private Label label13;
        private Label label1;
        private SplitContainer splitContainer1;
        private Label labelDimension;
        private NumericUpDown numericUpDownFitRange;
        private Label label10;
        private RadioButton radioButtonDiamondRaman;
        private RadioButton radioButtonRubyFluorescence;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel2;
        private Crystallography.Controls.GraphControl graphControlTop;
        private Crystallography.Controls.GraphControl graphControlBottom;
        private TextBox textBoxMaoHydroP;
        private Label label28;
        private GroupBox groupBoxMao;
        private TextBox textBoxMaoP;
        private GroupBox groupBoxAkahama2006;
        private Label label29;
        private TextBox textBoxDiamondRamanNu;
        private Label label30;
        private TextBox textBoxAkahama2006K0;
        private TextBox textBoxAkahama2006K0Prime;
        private Label label31;
        private Label label32;
        private TextBox textBoxDiamondAkahama2006P;
        private Label label35;
        private TextBox textBoxMaoQuasiP;
        private Label label42;
        private Label label44;
        private TextBox textBoxDiamondRamanNu0;
        private Label label43;
        private Label label33;
        private Label label46;
        private Label label45;
        private Label label34;
        private Label label4;
        private Label label3;
        private RadioButton radioButtonEOS;
        private FlowLayoutPanel flowLayoutPanelEOS;
        private Panel panelEOS;
        private Panel panel2;
        private Label label107;
        private TextBox textBoxShenP;
        private Label label104;
        private GroupBox groupBox4;
        private Label label18;
        private GroupBox groupBox2;
        private Crystallography.Controls.NumericBox numericBoxRubyRagan;
        private GroupBox groupBox3;
        private CheckBox checkBoxRubyR1_0CalculatedFromRagan;
        private Crystallography.Controls.NumericBox numericBoxRubyT;
        private CheckBox checkBoxRubyTemeratureSameAsRef;
        private Crystallography.Controls.NumericBox numericBoxRubyR1_0;
        private GroupBox groupBox1;
        private Button buttonRubyRefR1Set;
        private Crystallography.Controls.NumericBox numericBoxRubyRefT;
        private Crystallography.Controls.NumericBox numericBoxRubyRefR1;
        private Crystallography.Controls.NumericBox numericBoxRubyR1;
        private Crystallography.Controls.NumericBox numericBoxMaoA;
        private Crystallography.Controls.NumericBox numericBoxMaoQuasiA;
        private Crystallography.Controls.NumericBox numericBoxShenA;
        private Crystallography.Controls.NumericBox numericBoxMaoHydroA;
        #endregion

        #region Windows フォーム デザイナで生成されたコード 
        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(FormMain));
            numericUpDownDifferentiationRunningAverage = new NumericUpDown();
            numericUpDownOriginalRunningAverage = new NumericUpDown();
            label2 = new Label();
            textBoxAkahama2004A = new TextBox();
            textBoxAkahama2004B = new TextBox();
            textBoxAkahama2004C = new TextBox();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            textBoxDiamondAkahama2004P = new TextBox();
            label9 = new Label();
            label14 = new Label();
            numericUpDownOriginalGaussian = new NumericUpDown();
            label12 = new Label();
            labelBottomTitle = new Label();
            label13 = new Label();
            label1 = new Label();
            numericUpDownDifferentiationGaussian = new NumericUpDown();
            splitContainer1 = new SplitContainer();
            graphControlTop = new Crystallography.Controls.GraphControl();
            flowLayoutPanel3 = new FlowLayoutPanel();
            graphControlBottom = new Crystallography.Controls.GraphControl();
            flowLayoutPanel4 = new FlowLayoutPanel();
            label25 = new Label();
            textBoxFittingInformation = new TextBox();
            flowLayoutPanel2 = new FlowLayoutPanel();
            label10 = new Label();
            numericUpDownFitRange = new NumericUpDown();
            labelDimension = new Label();
            radioButtonDiamondRaman = new RadioButton();
            radioButtonRubyFluorescence = new RadioButton();
            flowLayoutPanel1 = new FlowLayoutPanel();
            radioButtonEOS = new RadioButton();
            textBoxMaoHydroP = new TextBox();
            label28 = new Label();
            groupBoxMao = new GroupBox();
            radioButtonTempUnitK = new RadioButton();
            radioButtonTempUnitC = new RadioButton();
            groupBox4 = new GroupBox();
            textBoxMaoP = new TextBox();
            numericBoxMaoA = new Crystallography.Controls.NumericBox();
            numericBoxMaoQuasiA = new Crystallography.Controls.NumericBox();
            numericBoxShenA = new Crystallography.Controls.NumericBox();
            numericBoxMaoHydroA = new Crystallography.Controls.NumericBox();
            label42 = new Label();
            label18 = new Label();
            textBoxMaoQuasiP = new TextBox();
            label107 = new Label();
            label46 = new Label();
            label45 = new Label();
            label34 = new Label();
            textBoxShenP = new TextBox();
            label104 = new Label();
            groupBox2 = new GroupBox();
            numericBoxRubyRagan = new Crystallography.Controls.NumericBox();
            groupBox3 = new GroupBox();
            checkBoxRubyR1_0CalculatedFromRagan = new CheckBox();
            numericBoxRubyT = new Crystallography.Controls.NumericBox();
            checkBoxRubyTemeratureSameAsRef = new CheckBox();
            numericBoxRubyR1_0 = new Crystallography.Controls.NumericBox();
            groupBox1 = new GroupBox();
            buttonRubyRefR1Set = new Button();
            numericBoxRubyRefT = new Crystallography.Controls.NumericBox();
            numericBoxRubyRefR1 = new Crystallography.Controls.NumericBox();
            numericBoxRubyR1 = new Crystallography.Controls.NumericBox();
            label17 = new Label();
            groupBoxAkahama2006 = new GroupBox();
            textBoxDiamondFratandunoHigh = new TextBox();
            textBoxDiamondAkahama2006P = new TextBox();
            label44 = new Label();
            label29 = new Label();
            textBoxDiamondRamanNu0 = new TextBox();
            label43 = new Label();
            textBoxDiamondRamanNu = new TextBox();
            label30 = new Label();
            textBoxAkahama2006K0 = new TextBox();
            textBoxAkahama2006K0Prime = new TextBox();
            textBoxDiamondFratandunoLow = new TextBox();
            label4 = new Label();
            label20 = new Label();
            label19 = new Label();
            label3 = new Label();
            label24 = new Label();
            label31 = new Label();
            label33 = new Label();
            label21 = new Label();
            label32 = new Label();
            label35 = new Label();
            flowLayoutPanelEOS = new FlowLayoutPanel();
            groupBoxGold = new GroupBox();
            numericBoxAuFratanduono = new Crystallography.Controls.NumericBox();
            numericBoxAuYokoo = new Crystallography.Controls.NumericBox();
            numericalTextBoxGoldJamieson = new Crystallography.Controls.NumericBox();
            numericalTextBoxGoldTsuchiya = new Crystallography.Controls.NumericBox();
            numericalTextBoxGoldSim = new Crystallography.Controls.NumericBox();
            label11 = new Label();
            numericalTextBoxGoldAnderson = new Crystallography.Controls.NumericBox();
            label49 = new Label();
            numericalTextBoxGoldA = new Crystallography.Controls.NumericBox();
            numericBoxGoldA0 = new Crystallography.Controls.NumericBox();
            label22 = new Label();
            label69 = new Label();
            label15 = new Label();
            label27 = new Label();
            groupBoxPlatinum = new GroupBox();
            numericBoxPtFratanduono = new Crystallography.Controls.NumericBox();
            numericBoxPtYokoo = new Crystallography.Controls.NumericBox();
            numericalTextBoxPtMatsui = new Crystallography.Controls.NumericBox();
            numericalTextBoxPtHolmes = new Crystallography.Controls.NumericBox();
            label16 = new Label();
            numericalTextBoxPtJamieson = new Crystallography.Controls.NumericBox();
            label23 = new Label();
            label26 = new Label();
            label36 = new Label();
            numericalTextBoxPtA = new Crystallography.Controls.NumericBox();
            numericBoxPtA0 = new Crystallography.Controls.NumericBox();
            label37 = new Label();
            groupBoxNaClB1 = new GroupBox();
            numericalTextBoxNaClB1Matsui = new Crystallography.Controls.NumericBox();
            numericalTextBoxNaClB1Brown = new Crystallography.Controls.NumericBox();
            label38 = new Label();
            label39 = new Label();
            numericalTextBoxNaClB1A = new Crystallography.Controls.NumericBox();
            numericBoxNaClB1A0 = new Crystallography.Controls.NumericBox();
            groupBoxNaClB2 = new GroupBox();
            numericalTextBoxNaClB2SakaiVinet = new Crystallography.Controls.NumericBox();
            numericalTextBoxNaClB2SakaiBM = new Crystallography.Controls.NumericBox();
            numericalTextBoxNaClB2Ueda = new Crystallography.Controls.NumericBox();
            label40 = new Label();
            numericalTextBoxNaClB2SataMgO = new Crystallography.Controls.NumericBox();
            label41 = new Label();
            numericalTextBoxNaClB2SataPt = new Crystallography.Controls.NumericBox();
            label47 = new Label();
            label48 = new Label();
            numericalTextBoxNaClB2A = new Crystallography.Controls.NumericBox();
            label50 = new Label();
            numericalTextBoxNaClB2A0 = new Crystallography.Controls.NumericBox();
            groupBoxPericlase = new GroupBox();
            numericBoxMgOTangeBM = new Crystallography.Controls.NumericBox();
            numericBoxMgOTangeVinet = new Crystallography.Controls.NumericBox();
            numericalTextBoxMgOAizawa = new Crystallography.Controls.NumericBox();
            label51 = new Label();
            numericalTextBoxMgODewaele = new Crystallography.Controls.NumericBox();
            label52 = new Label();
            numericalTextBoxMgOJacson = new Crystallography.Controls.NumericBox();
            label53 = new Label();
            label54 = new Label();
            numericalTextBoxMgOA = new Crystallography.Controls.NumericBox();
            label55 = new Label();
            numericBoxMgOA0 = new Crystallography.Controls.NumericBox();
            groupBoxCorundum = new GroupBox();
            numericBoxCorundumDubrovinsky = new Crystallography.Controls.NumericBox();
            numericalTextBoxColundumV = new Crystallography.Controls.NumericBox();
            label56 = new Label();
            numericBoxColundumV0 = new Crystallography.Controls.NumericBox();
            groupBoxAr = new GroupBox();
            numericalTextBoxArRoss = new Crystallography.Controls.NumericBox();
            numericalTextBoxArJephcoat = new Crystallography.Controls.NumericBox();
            numericalTextBoxArA = new Crystallography.Controls.NumericBox();
            label57 = new Label();
            label58 = new Label();
            numericBoxArA0 = new Crystallography.Controls.NumericBox();
            groupBoxRe = new GroupBox();
            numericBoxReDub = new Crystallography.Controls.NumericBox();
            numericBoxReSakai = new Crystallography.Controls.NumericBox();
            label59 = new Label();
            numericBoxReAnz = new Crystallography.Controls.NumericBox();
            label60 = new Label();
            numericalTextBoxReZha = new Crystallography.Controls.NumericBox();
            label61 = new Label();
            numericBoxReV = new Crystallography.Controls.NumericBox();
            label62 = new Label();
            numerictBoxReV0 = new Crystallography.Controls.NumericBox();
            groupBoxMo = new GroupBox();
            numericBoxMoZhao = new Crystallography.Controls.NumericBox();
            numericBoxMoHuang = new Crystallography.Controls.NumericBox();
            label63 = new Label();
            numericBoxMoV = new Crystallography.Controls.NumericBox();
            label64 = new Label();
            numericBoxMoV0 = new Crystallography.Controls.NumericBox();
            groupBoxPb = new GroupBox();
            numericBoxPbStrassle = new Crystallography.Controls.NumericBox();
            numericBoxPbA = new Crystallography.Controls.NumericBox();
            label65 = new Label();
            numericBoxPbA0 = new Crystallography.Controls.NumericBox();
            panelEOS = new Panel();
            panel2 = new Panel();
            numericBoxTemperature = new Crystallography.Controls.NumericBox();
            numericBoxTemperature0 = new Crystallography.Controls.NumericBox();
            numericBoxDecimalPlaces = new Crystallography.Controls.NumericBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            readToolStripMenuItem = new ToolStripMenuItem();
            exportAsCSVToolStripMenuItem = new ToolStripMenuItem();
            watchNewFileToolStripMenuItem = new ToolStripMenuItem();
            timer = new System.Windows.Forms.Timer(components);
            ((ISupportInitialize)numericUpDownDifferentiationRunningAverage).BeginInit();
            ((ISupportInitialize)numericUpDownOriginalRunningAverage).BeginInit();
            ((ISupportInitialize)numericUpDownOriginalGaussian).BeginInit();
            ((ISupportInitialize)numericUpDownDifferentiationGaussian).BeginInit();
            ((ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            ((ISupportInitialize)numericUpDownFitRange).BeginInit();
            flowLayoutPanel1.SuspendLayout();
            groupBoxMao.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBoxAkahama2006.SuspendLayout();
            flowLayoutPanelEOS.SuspendLayout();
            groupBoxGold.SuspendLayout();
            groupBoxPlatinum.SuspendLayout();
            groupBoxNaClB1.SuspendLayout();
            groupBoxNaClB2.SuspendLayout();
            groupBoxPericlase.SuspendLayout();
            groupBoxCorundum.SuspendLayout();
            groupBoxAr.SuspendLayout();
            groupBoxRe.SuspendLayout();
            groupBoxMo.SuspendLayout();
            groupBoxPb.SuspendLayout();
            panelEOS.SuspendLayout();
            panel2.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // numericUpDownDifferentiationRunningAverage
            // 
            numericUpDownDifferentiationRunningAverage.AutoSize = true;
            numericUpDownDifferentiationRunningAverage.Font = new Font("Verdana", 9F);
            numericUpDownDifferentiationRunningAverage.Location = new Point(256, 3);
            numericUpDownDifferentiationRunningAverage.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownDifferentiationRunningAverage.Name = "numericUpDownDifferentiationRunningAverage";
            numericUpDownDifferentiationRunningAverage.ReadOnly = true;
            numericUpDownDifferentiationRunningAverage.Size = new Size(47, 22);
            numericUpDownDifferentiationRunningAverage.TabIndex = 2;
            numericUpDownDifferentiationRunningAverage.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownDifferentiationRunningAverage.ValueChanged += numericUpDown_ValueChanged;
            numericUpDownDifferentiationRunningAverage.MouseDown += numericUpDownDifferentiationRunningAverage_MouseDown;
            // 
            // numericUpDownOriginalRunningAverage
            // 
            numericUpDownOriginalRunningAverage.AutoSize = true;
            numericUpDownOriginalRunningAverage.Font = new Font("Verdana", 9F);
            numericUpDownOriginalRunningAverage.Location = new Point(246, 3);
            numericUpDownOriginalRunningAverage.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownOriginalRunningAverage.Name = "numericUpDownOriginalRunningAverage";
            numericUpDownOriginalRunningAverage.ReadOnly = true;
            numericUpDownOriginalRunningAverage.Size = new Size(47, 22);
            numericUpDownOriginalRunningAverage.TabIndex = 3;
            numericUpDownOriginalRunningAverage.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numericUpDownOriginalRunningAverage.ValueChanged += numericUpDown_ValueChanged;
            numericUpDownOriginalRunningAverage.MouseDown += numericUpDownOriginalRunningAverage_MouseDown;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Verdana", 9F);
            label2.Location = new Point(126, 6);
            label2.Margin = new Padding(3, 6, 3, 0);
            label2.Name = "label2";
            label2.Size = new Size(114, 14);
            label2.TabIndex = 3;
            label2.Text = "Running Average";
            // 
            // textBoxAkahama2004A
            // 
            textBoxAkahama2004A.Font = new Font("Segoe UI Symbol", 8.25F);
            textBoxAkahama2004A.Location = new Point(293, 16);
            textBoxAkahama2004A.Name = "textBoxAkahama2004A";
            textBoxAkahama2004A.Size = new Size(44, 22);
            textBoxAkahama2004A.TabIndex = 0;
            textBoxAkahama2004A.Text = "66.9";
            textBoxAkahama2004A.TextChanged += textBoxNu_TextChanged;
            // 
            // textBoxAkahama2004B
            // 
            textBoxAkahama2004B.Font = new Font("Segoe UI Symbol", 8.25F);
            textBoxAkahama2004B.Location = new Point(355, 16);
            textBoxAkahama2004B.Name = "textBoxAkahama2004B";
            textBoxAkahama2004B.Size = new Size(44, 22);
            textBoxAkahama2004B.TabIndex = 0;
            textBoxAkahama2004B.Text = "-0.5281";
            textBoxAkahama2004B.TextChanged += textBoxNu_TextChanged;
            // 
            // textBoxAkahama2004C
            // 
            textBoxAkahama2004C.Font = new Font("Segoe UI Symbol", 8.25F);
            textBoxAkahama2004C.Location = new Point(427, 16);
            textBoxAkahama2004C.Name = "textBoxAkahama2004C";
            textBoxAkahama2004C.Size = new Size(44, 22);
            textBoxAkahama2004C.TabIndex = 0;
            textBoxAkahama2004C.Text = "3.585";
            textBoxAkahama2004C.TextChanged += textBoxNu_TextChanged;
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI Symbol", 8.25F);
            label5.Location = new Point(264, 20);
            label5.Name = "label5";
            label5.Size = new Size(39, 12);
            label5.TabIndex = 1;
            label5.Text = "P =";
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI Symbol", 8.25F);
            label6.Location = new Point(337, 19);
            label6.Name = "label6";
            label6.Size = new Size(12, 12);
            label6.TabIndex = 1;
            label6.Text = "+";
            // 
            // label7
            // 
            label7.Font = new Font("Segoe UI Symbol", 8.25F);
            label7.Location = new Point(399, 19);
            label7.Name = "label7";
            label7.Size = new Size(24, 12);
            label7.TabIndex = 1;
            label7.Text = "ν+";
            // 
            // label8
            // 
            label8.Font = new Font("Segoe UI Symbol", 8.25F);
            label8.Location = new Point(475, 19);
            label8.Name = "label8";
            label8.Size = new Size(60, 12);
            label8.TabIndex = 1;
            label8.Text = "E-4 ν^2=";
            // 
            // textBoxDiamondAkahama2004P
            // 
            textBoxDiamondAkahama2004P.BackColor = Color.Navy;
            textBoxDiamondAkahama2004P.Font = new Font("Segoe UI Symbol", 11.25F);
            textBoxDiamondAkahama2004P.ForeColor = Color.White;
            textBoxDiamondAkahama2004P.Location = new Point(615, 11);
            textBoxDiamondAkahama2004P.Name = "textBoxDiamondAkahama2004P";
            textBoxDiamondAkahama2004P.ReadOnly = true;
            textBoxDiamondAkahama2004P.Size = new Size(71, 27);
            textBoxDiamondAkahama2004P.TabIndex = 0;
            textBoxDiamondAkahama2004P.Text = "0";
            // 
            // label9
            // 
            label9.Font = new Font("Verdana", 9F);
            label9.Location = new Point(688, 18);
            label9.Name = "label9";
            label9.Size = new Size(34, 12);
            label9.TabIndex = 1;
            label9.Text = "GPa";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.BackColor = SystemColors.Control;
            label14.Font = new Font("Verdana", 9F);
            label14.Location = new Point(3, 6);
            label14.Margin = new Padding(3, 6, 3, 0);
            label14.Name = "label14";
            label14.Size = new Size(117, 14);
            label14.TabIndex = 3;
            label14.Text = "Original spectrum";
            // 
            // numericUpDownOriginalGaussian
            // 
            numericUpDownOriginalGaussian.AutoSize = true;
            numericUpDownOriginalGaussian.Font = new Font("Verdana", 9F);
            numericUpDownOriginalGaussian.Location = new Point(410, 3);
            numericUpDownOriginalGaussian.Name = "numericUpDownOriginalGaussian";
            numericUpDownOriginalGaussian.Size = new Size(47, 22);
            numericUpDownOriginalGaussian.TabIndex = 3;
            numericUpDownOriginalGaussian.Value = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDownOriginalGaussian.ValueChanged += numericUpDown_ValueChanged;
            numericUpDownOriginalGaussian.MouseDown += numericUpDownOriginalGaussian_MouseDown;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Verdana", 9F);
            label12.Location = new Point(299, 6);
            label12.Margin = new Padding(3, 6, 3, 0);
            label12.Name = "label12";
            label12.Size = new Size(105, 14);
            label12.TabIndex = 3;
            label12.Text = "Gaussian blur σ";
            // 
            // labelBottomTitle
            // 
            labelBottomTitle.AutoSize = true;
            labelBottomTitle.BackColor = SystemColors.Control;
            labelBottomTitle.Font = new Font("Verdana", 9F);
            labelBottomTitle.Location = new Point(3, 6);
            labelBottomTitle.Margin = new Padding(3, 6, 3, 0);
            labelBottomTitle.Name = "labelBottomTitle";
            labelBottomTitle.Size = new Size(127, 14);
            labelBottomTitle.TabIndex = 3;
            labelBottomTitle.Text = "First Differentiation";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Verdana", 9F);
            label13.Location = new Point(136, 6);
            label13.Margin = new Padding(3, 6, 3, 0);
            label13.Name = "label13";
            label13.Size = new Size(114, 14);
            label13.TabIndex = 3;
            label13.Text = "Running Average";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 9F);
            label1.Location = new Point(309, 6);
            label1.Margin = new Padding(3, 6, 3, 0);
            label1.Name = "label1";
            label1.Size = new Size(105, 14);
            label1.TabIndex = 3;
            label1.Text = "Gaussian blur σ";
            // 
            // numericUpDownDifferentiationGaussian
            // 
            numericUpDownDifferentiationGaussian.AutoSize = true;
            numericUpDownDifferentiationGaussian.Font = new Font("Verdana", 9F);
            numericUpDownDifferentiationGaussian.Location = new Point(420, 3);
            numericUpDownDifferentiationGaussian.Name = "numericUpDownDifferentiationGaussian";
            numericUpDownDifferentiationGaussian.Size = new Size(47, 22);
            numericUpDownDifferentiationGaussian.TabIndex = 3;
            numericUpDownDifferentiationGaussian.Value = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDownDifferentiationGaussian.ValueChanged += numericUpDown_ValueChanged;
            numericUpDownDifferentiationGaussian.MouseDown += numericUpDownwnDifferentiationGaussian_MouseDown;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 255);
            splitContainer1.Margin = new Padding(0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(graphControlTop);
            splitContainer1.Panel1.Controls.Add(flowLayoutPanel3);
            splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(graphControlBottom);
            splitContainer1.Panel2.Controls.Add(flowLayoutPanel4);
            splitContainer1.Panel2.Controls.Add(flowLayoutPanel2);
            splitContainer1.Panel2MinSize = 0;
            splitContainer1.Size = new Size(725, 293);
            splitContainer1.SplitterDistance = 132;
            splitContainer1.TabIndex = 6;
            // 
            // graphControlTop
            // 
            graphControlTop.AllowMouseOperation = true;
            graphControlTop.AxisLineColor = Color.Gray;
            graphControlTop.AxisTextColor = Color.Black;
            graphControlTop.AxisTextFont = new Font("Segoe UI", 9F);
            graphControlTop.AxisXTextVisible = true;
            graphControlTop.AxisYTextVisible = true;
            graphControlTop.BackgroundColor = Color.White;
            graphControlTop.BottomMargin = 0D;
            graphControlTop.DivisionLineColor = Color.Gray;
            graphControlTop.DivisionLineXVisible = true;
            graphControlTop.DivisionLineYVisible = true;
            graphControlTop.Dock = DockStyle.Fill;
            graphControlTop.DrawingRange = (RectangleD)resources.GetObject("graphControlTop.DrawingRange");
            graphControlTop.FixRangeHorizontal = false;
            graphControlTop.FixRangeVertical = false;
            graphControlTop.Font = new Font("Segoe UI Symbol", 9F);
            graphControlTop.GraphTitle = "";
            graphControlTop.Interpolation = false;
            graphControlTop.IsIntegerX = false;
            graphControlTop.IsIntegerY = false;
            graphControlTop.LabelX = "X:";
            graphControlTop.LabelY = "Y:";
            graphControlTop.LeftMargin = 0F;
            graphControlTop.LineWidth = 2F;
            graphControlTop.Location = new Point(0, 28);
            graphControlTop.LowerX = 0D;
            graphControlTop.LowerY = 0D;
            graphControlTop.MaximalX = 1D;
            graphControlTop.MaximalY = 1D;
            graphControlTop.MinimalX = 0D;
            graphControlTop.MinimalY = 0D;
            graphControlTop.Mode = Crystallography.Controls.GraphControl.DrawingMode.Line;
            graphControlTop.MousePositionVisible = true;
            graphControlTop.MousePositionXDigit = -1;
            graphControlTop.MousePositionYDigit = -1;
            graphControlTop.Name = "graphControlTop";
            graphControlTop.OriginPosition = new Point(40, 20);
            graphControlTop.Profile = null;
            graphControlTop.Size = new Size(725, 104);
            graphControlTop.Smoothing = false;
            graphControlTop.TabIndex = 5;
            graphControlTop.UnitX = "";
            graphControlTop.UnitY = "";
            graphControlTop.UpperPanelFont = new Font("Segoe UI Symbol", 9F);
            graphControlTop.UpperPanelVisible = true;
            graphControlTop.UpperX = 1D;
            graphControlTop.UpperY = 1D;
            graphControlTop.UseLineWidth = true;
            graphControlTop.VerticalLineColor = Color.Red;
            graphControlTop.XLog = false;
            graphControlTop.YLog = false;
            graphControlTop.DrawingRangeChanged += graphControlTop_DrawingRangeChanged;
            graphControlTop.MouseDoubleClick2 += graphControlBottom_MouseDoubleClick2;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.Controls.Add(label14);
            flowLayoutPanel3.Controls.Add(label2);
            flowLayoutPanel3.Controls.Add(numericUpDownOriginalRunningAverage);
            flowLayoutPanel3.Controls.Add(label12);
            flowLayoutPanel3.Controls.Add(numericUpDownOriginalGaussian);
            flowLayoutPanel3.Dock = DockStyle.Top;
            flowLayoutPanel3.Location = new Point(0, 0);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(725, 28);
            flowLayoutPanel3.TabIndex = 4;
            // 
            // graphControlBottom
            // 
            graphControlBottom.AllowMouseOperation = true;
            graphControlBottom.AxisLineColor = Color.Gray;
            graphControlBottom.AxisTextColor = Color.Black;
            graphControlBottom.AxisTextFont = new Font("Segoe UI", 9F);
            graphControlBottom.AxisXTextVisible = true;
            graphControlBottom.AxisYTextVisible = true;
            graphControlBottom.BackgroundColor = Color.White;
            graphControlBottom.BottomMargin = 0D;
            graphControlBottom.DivisionLineColor = Color.Gray;
            graphControlBottom.DivisionLineXVisible = true;
            graphControlBottom.DivisionLineYVisible = true;
            graphControlBottom.Dock = DockStyle.Fill;
            graphControlBottom.DrawingRange = (RectangleD)resources.GetObject("graphControlBottom.DrawingRange");
            graphControlBottom.FixRangeHorizontal = false;
            graphControlBottom.FixRangeVertical = false;
            graphControlBottom.Font = new Font("Cambria", 9F);
            graphControlBottom.GraphTitle = "";
            graphControlBottom.Interpolation = false;
            graphControlBottom.IsIntegerX = false;
            graphControlBottom.IsIntegerY = false;
            graphControlBottom.LabelX = "X:";
            graphControlBottom.LabelY = "Y:";
            graphControlBottom.LeftMargin = 0F;
            graphControlBottom.LineWidth = 2F;
            graphControlBottom.Location = new Point(0, 60);
            graphControlBottom.LowerX = 0D;
            graphControlBottom.LowerY = 0D;
            graphControlBottom.MaximalX = 1D;
            graphControlBottom.MaximalY = 1D;
            graphControlBottom.MinimalX = 0D;
            graphControlBottom.MinimalY = 0D;
            graphControlBottom.Mode = Crystallography.Controls.GraphControl.DrawingMode.Line;
            graphControlBottom.MousePositionVisible = true;
            graphControlBottom.MousePositionXDigit = -1;
            graphControlBottom.MousePositionYDigit = -1;
            graphControlBottom.Name = "graphControlBottom";
            graphControlBottom.OriginPosition = new Point(40, 20);
            graphControlBottom.Profile = null;
            graphControlBottom.Size = new Size(725, 97);
            graphControlBottom.Smoothing = false;
            graphControlBottom.TabIndex = 6;
            graphControlBottom.UnitX = "";
            graphControlBottom.UnitY = "";
            graphControlBottom.UpperPanelFont = new Font("Cambria", 9F);
            graphControlBottom.UpperPanelVisible = true;
            graphControlBottom.UpperX = 1D;
            graphControlBottom.UpperY = 1D;
            graphControlBottom.UseLineWidth = true;
            graphControlBottom.VerticalLineColor = Color.Red;
            graphControlBottom.XLog = false;
            graphControlBottom.YLog = false;
            graphControlBottom.DrawingRangeChanged += graphControlBottom_DrawingRangeChanged;
            graphControlBottom.MouseDoubleClick2 += graphControlBottom_MouseDoubleClick2;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.AutoSize = true;
            flowLayoutPanel4.Controls.Add(label25);
            flowLayoutPanel4.Controls.Add(textBoxFittingInformation);
            flowLayoutPanel4.Dock = DockStyle.Top;
            flowLayoutPanel4.Location = new Point(0, 28);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(725, 32);
            flowLayoutPanel4.TabIndex = 7;
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.BackColor = SystemColors.Control;
            label25.Font = new Font("Verdana", 9F);
            label25.Location = new Point(3, 6);
            label25.Margin = new Padding(3, 6, 3, 0);
            label25.Name = "label25";
            label25.Size = new Size(123, 14);
            label25.TabIndex = 3;
            label25.Text = "Fitting Information";
            // 
            // textBoxFittingInformation
            // 
            textBoxFittingInformation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBoxFittingInformation.BackColor = SystemColors.Control;
            textBoxFittingInformation.Font = new Font("Segoe UI Symbol", 8F);
            textBoxFittingInformation.ForeColor = SystemColors.ControlText;
            textBoxFittingInformation.Location = new Point(132, 3);
            textBoxFittingInformation.Multiline = true;
            textBoxFittingInformation.Name = "textBoxFittingInformation";
            textBoxFittingInformation.ReadOnly = true;
            textBoxFittingInformation.Size = new Size(585, 26);
            textBoxFittingInformation.TabIndex = 0;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.Controls.Add(labelBottomTitle);
            flowLayoutPanel2.Controls.Add(label13);
            flowLayoutPanel2.Controls.Add(numericUpDownDifferentiationRunningAverage);
            flowLayoutPanel2.Controls.Add(label1);
            flowLayoutPanel2.Controls.Add(numericUpDownDifferentiationGaussian);
            flowLayoutPanel2.Controls.Add(label10);
            flowLayoutPanel2.Controls.Add(numericUpDownFitRange);
            flowLayoutPanel2.Controls.Add(labelDimension);
            flowLayoutPanel2.Dock = DockStyle.Top;
            flowLayoutPanel2.Location = new Point(0, 0);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(725, 28);
            flowLayoutPanel2.TabIndex = 4;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Verdana", 9F);
            label10.Location = new Point(473, 6);
            label10.Margin = new Padding(3, 6, 3, 0);
            label10.Name = "label10";
            label10.Size = new Size(90, 14);
            label10.TabIndex = 3;
            label10.Text = "Fitting Range";
            // 
            // numericUpDownFitRange
            // 
            numericUpDownFitRange.AutoSize = true;
            numericUpDownFitRange.Font = new Font("Verdana", 9F);
            numericUpDownFitRange.Location = new Point(569, 3);
            numericUpDownFitRange.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownFitRange.Name = "numericUpDownFitRange";
            numericUpDownFitRange.Size = new Size(47, 22);
            numericUpDownFitRange.TabIndex = 2;
            numericUpDownFitRange.Value = new decimal(new int[] { 4, 0, 0, 0 });
            numericUpDownFitRange.ValueChanged += numericUpDownFitRange_ValueChanged;
            // 
            // labelDimension
            // 
            labelDimension.AutoSize = true;
            labelDimension.Font = new Font("Verdana", 9F);
            labelDimension.Location = new Point(622, 6);
            labelDimension.Margin = new Padding(3, 6, 3, 0);
            labelDimension.Name = "labelDimension";
            labelDimension.Size = new Size(37, 14);
            labelDimension.TabIndex = 3;
            labelDimension.Text = "cm-1";
            // 
            // radioButtonDiamondRaman
            // 
            radioButtonDiamondRaman.AutoSize = true;
            radioButtonDiamondRaman.Checked = true;
            radioButtonDiamondRaman.Font = new Font("Verdana", 9F);
            radioButtonDiamondRaman.Location = new Point(3, 3);
            radioButtonDiamondRaman.Name = "radioButtonDiamondRaman";
            radioButtonDiamondRaman.Size = new Size(127, 18);
            radioButtonDiamondRaman.TabIndex = 7;
            radioButtonDiamondRaman.TabStop = true;
            radioButtonDiamondRaman.Text = "Diamond Raman";
            radioButtonDiamondRaman.UseVisualStyleBackColor = true;
            radioButtonDiamondRaman.CheckedChanged += radioButtonMode_CheckedChanged;
            // 
            // radioButtonRubyFluorescence
            // 
            radioButtonRubyFluorescence.AutoSize = true;
            radioButtonRubyFluorescence.Font = new Font("Verdana", 9F);
            radioButtonRubyFluorescence.Location = new Point(136, 3);
            radioButtonRubyFluorescence.Name = "radioButtonRubyFluorescence";
            radioButtonRubyFluorescence.Size = new Size(142, 18);
            radioButtonRubyFluorescence.TabIndex = 7;
            radioButtonRubyFluorescence.Text = "Ruby Fluorescence";
            radioButtonRubyFluorescence.UseVisualStyleBackColor = true;
            radioButtonRubyFluorescence.CheckedChanged += radioButtonMode_CheckedChanged;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(radioButtonDiamondRaman);
            flowLayoutPanel1.Controls.Add(radioButtonRubyFluorescence);
            flowLayoutPanel1.Controls.Add(radioButtonEOS);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(0, 24);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(725, 24);
            flowLayoutPanel1.TabIndex = 9;
            // 
            // radioButtonEOS
            // 
            radioButtonEOS.AutoSize = true;
            radioButtonEOS.Font = new Font("Verdana", 9F);
            radioButtonEOS.Location = new Point(284, 3);
            radioButtonEOS.Name = "radioButtonEOS";
            radioButtonEOS.Size = new Size(51, 18);
            radioButtonEOS.TabIndex = 7;
            radioButtonEOS.Text = "EOS";
            radioButtonEOS.UseVisualStyleBackColor = true;
            radioButtonEOS.CheckedChanged += radioButtonMode_CheckedChanged;
            // 
            // textBoxMaoHydroP
            // 
            textBoxMaoHydroP.BackColor = Color.Navy;
            textBoxMaoHydroP.Font = new Font("Segoe UI Symbol", 11.25F);
            textBoxMaoHydroP.ForeColor = Color.White;
            textBoxMaoHydroP.Location = new Point(620, 20);
            textBoxMaoHydroP.Name = "textBoxMaoHydroP";
            textBoxMaoHydroP.ReadOnly = true;
            textBoxMaoHydroP.Size = new Size(64, 27);
            textBoxMaoHydroP.TabIndex = 0;
            textBoxMaoHydroP.Text = "0";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("Segoe UI Symbol", 9.75F);
            label28.Location = new Point(683, 23);
            label28.Name = "label28";
            label28.Size = new Size(31, 17);
            label28.TabIndex = 1;
            label28.Text = "GPa";
            // 
            // groupBoxMao
            // 
            groupBoxMao.Controls.Add(radioButtonTempUnitK);
            groupBoxMao.Controls.Add(radioButtonTempUnitC);
            groupBoxMao.Controls.Add(groupBox4);
            groupBoxMao.Controls.Add(groupBox2);
            groupBoxMao.Controls.Add(groupBox3);
            groupBoxMao.Controls.Add(groupBox1);
            groupBoxMao.Controls.Add(numericBoxRubyR1);
            groupBoxMao.Controls.Add(label17);
            groupBoxMao.Dock = DockStyle.Bottom;
            groupBoxMao.Font = new Font("Verdana", 9F);
            groupBoxMao.Location = new Point(0, 672);
            groupBoxMao.Name = "groupBoxMao";
            groupBoxMao.Size = new Size(725, 244);
            groupBoxMao.TabIndex = 12;
            groupBoxMao.TabStop = false;
            groupBoxMao.Text = "Pressure calculation from the ruby fluorescence";
            // 
            // radioButtonTempUnitK
            // 
            radioButtonTempUnitK.AutoSize = true;
            radioButtonTempUnitK.Checked = true;
            radioButtonTempUnitK.Font = new Font("Segoe UI Symbol", 9F);
            radioButtonTempUnitK.Location = new Point(53, 88);
            radioButtonTempUnitK.Name = "radioButtonTempUnitK";
            radioButtonTempUnitK.Size = new Size(32, 19);
            radioButtonTempUnitK.TabIndex = 7;
            radioButtonTempUnitK.TabStop = true;
            radioButtonTempUnitK.Text = "K";
            radioButtonTempUnitK.UseVisualStyleBackColor = true;
            radioButtonTempUnitK.CheckedChanged += radioButtonTempUnit_CheckedChanged;
            // 
            // radioButtonTempUnitC
            // 
            radioButtonTempUnitC.AutoSize = true;
            radioButtonTempUnitC.Font = new Font("Segoe UI Symbol", 9F);
            radioButtonTempUnitC.Location = new Point(93, 88);
            radioButtonTempUnitC.Name = "radioButtonTempUnitC";
            radioButtonTempUnitC.Size = new Size(37, 19);
            radioButtonTempUnitC.TabIndex = 7;
            radioButtonTempUnitC.Text = "℃";
            radioButtonTempUnitC.UseVisualStyleBackColor = true;
            radioButtonTempUnitC.CheckedChanged += radioButtonTempUnit_CheckedChanged;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(textBoxMaoP);
            groupBox4.Controls.Add(numericBoxMaoA);
            groupBox4.Controls.Add(numericBoxMaoQuasiA);
            groupBox4.Controls.Add(numericBoxShenA);
            groupBox4.Controls.Add(numericBoxMaoHydroA);
            groupBox4.Controls.Add(label42);
            groupBox4.Controls.Add(label18);
            groupBox4.Controls.Add(textBoxMaoQuasiP);
            groupBox4.Controls.Add(label107);
            groupBox4.Controls.Add(label46);
            groupBox4.Controls.Add(label45);
            groupBox4.Controls.Add(label34);
            groupBox4.Controls.Add(textBoxMaoHydroP);
            groupBox4.Controls.Add(textBoxShenP);
            groupBox4.Controls.Add(label28);
            groupBox4.Controls.Add(label104);
            groupBox4.Dock = DockStyle.Bottom;
            groupBox4.Location = new Point(3, 164);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(719, 77);
            groupBox4.TabIndex = 6;
            groupBox4.TabStop = false;
            groupBox4.Text = "Pressure calculation, where x = R1/R1₀ ,  f(x, y) = (x^y-1)/y ,  Δ=R1-R1₀";
            // 
            // textBoxMaoP
            // 
            textBoxMaoP.BackColor = Color.Navy;
            textBoxMaoP.Font = new Font("Segoe UI Symbol", 11.25F);
            textBoxMaoP.ForeColor = Color.White;
            textBoxMaoP.Location = new Point(241, 20);
            textBoxMaoP.Name = "textBoxMaoP";
            textBoxMaoP.ReadOnly = true;
            textBoxMaoP.Size = new Size(64, 27);
            textBoxMaoP.TabIndex = 0;
            textBoxMaoP.Text = "0";
            // 
            // numericBoxMaoA
            // 
            numericBoxMaoA.BackColor = Color.Transparent;
            numericBoxMaoA.Font = new Font("Segoe UI Symbol", 9.75F);
            numericBoxMaoA.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMaoA.FooterText = "f(x, 5)=";
            numericBoxMaoA.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMaoA.HeaderText = "P=";
            numericBoxMaoA.Location = new Point(118, 22);
            numericBoxMaoA.Margin = new Padding(0);
            numericBoxMaoA.MaximumSize = new Size(1000, 28);
            numericBoxMaoA.MinimumSize = new Size(1, 18);
            numericBoxMaoA.Name = "numericBoxMaoA";
            numericBoxMaoA.RadianValue = 33.231068957972035D;
            numericBoxMaoA.Size = new Size(103, 25);
            numericBoxMaoA.TabIndex = 3;
            numericBoxMaoA.TextFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMaoA.Value = 1904D;
            // 
            // numericBoxMaoQuasiA
            // 
            numericBoxMaoQuasiA.BackColor = Color.Transparent;
            numericBoxMaoQuasiA.Font = new Font("Segoe UI Symbol", 9.75F);
            numericBoxMaoQuasiA.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMaoQuasiA.FooterText = "f(x, 7.665)=";
            numericBoxMaoQuasiA.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMaoQuasiA.HeaderText = "P=";
            numericBoxMaoQuasiA.Location = new Point(117, 49);
            numericBoxMaoQuasiA.Margin = new Padding(0);
            numericBoxMaoQuasiA.MaximumSize = new Size(1000, 28);
            numericBoxMaoQuasiA.MinimumSize = new Size(1, 18);
            numericBoxMaoQuasiA.Name = "numericBoxMaoQuasiA";
            numericBoxMaoQuasiA.RadianValue = 33.231068957972035D;
            numericBoxMaoQuasiA.Size = new Size(123, 25);
            numericBoxMaoQuasiA.TabIndex = 3;
            numericBoxMaoQuasiA.TextFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMaoQuasiA.Value = 1904D;
            // 
            // numericBoxShenA
            // 
            numericBoxShenA.BackColor = Color.Transparent;
            numericBoxShenA.Font = new Font("Segoe UI Symbol", 9.75F);
            numericBoxShenA.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxShenA.FooterText = "(Δ+5.63 Δ²)=";
            numericBoxShenA.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxShenA.HeaderText = "P=";
            numericBoxShenA.Location = new Point(485, 49);
            numericBoxShenA.Margin = new Padding(0);
            numericBoxShenA.MaximumSize = new Size(1000, 28);
            numericBoxShenA.MinimumSize = new Size(1, 18);
            numericBoxShenA.Name = "numericBoxShenA";
            numericBoxShenA.RadianValue = 32.637657012293964D;
            numericBoxShenA.Size = new Size(131, 25);
            numericBoxShenA.TabIndex = 3;
            numericBoxShenA.TextFont = new Font("Segoe UI Symbol", 9F);
            numericBoxShenA.Value = 1870D;
            // 
            // numericBoxMaoHydroA
            // 
            numericBoxMaoHydroA.BackColor = Color.Transparent;
            numericBoxMaoHydroA.Font = new Font("Segoe UI Symbol", 9.75F);
            numericBoxMaoHydroA.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMaoHydroA.FooterText = "f(x, 7.715)=";
            numericBoxMaoHydroA.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMaoHydroA.HeaderText = "P=";
            numericBoxMaoHydroA.Location = new Point(485, 22);
            numericBoxMaoHydroA.Margin = new Padding(0);
            numericBoxMaoHydroA.MaximumSize = new Size(1000, 28);
            numericBoxMaoHydroA.MinimumSize = new Size(1, 18);
            numericBoxMaoHydroA.Name = "numericBoxMaoHydroA";
            numericBoxMaoHydroA.RadianValue = 33.231068957972035D;
            numericBoxMaoHydroA.Size = new Size(123, 25);
            numericBoxMaoHydroA.TabIndex = 3;
            numericBoxMaoHydroA.TextFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMaoHydroA.Value = 1904D;
            // 
            // label42
            // 
            label42.AutoSize = true;
            label42.Font = new Font("Segoe UI Symbol", 9.75F);
            label42.Location = new Point(305, 50);
            label42.Name = "label42";
            label42.Size = new Size(31, 17);
            label42.TabIndex = 1;
            label42.Text = "GPa";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI Symbol", 9.75F);
            label18.Location = new Point(305, 23);
            label18.Name = "label18";
            label18.Size = new Size(31, 17);
            label18.TabIndex = 1;
            label18.Text = "GPa";
            // 
            // textBoxMaoQuasiP
            // 
            textBoxMaoQuasiP.BackColor = Color.Navy;
            textBoxMaoQuasiP.Font = new Font("Segoe UI Symbol", 11.25F);
            textBoxMaoQuasiP.ForeColor = Color.White;
            textBoxMaoQuasiP.Location = new Point(241, 47);
            textBoxMaoQuasiP.Name = "textBoxMaoQuasiP";
            textBoxMaoQuasiP.ReadOnly = true;
            textBoxMaoQuasiP.Size = new Size(64, 27);
            textBoxMaoQuasiP.TabIndex = 0;
            textBoxMaoQuasiP.Text = "0";
            // 
            // label107
            // 
            label107.AutoSize = true;
            label107.Font = new Font("Segoe UI Symbol", 9.75F);
            label107.Location = new Point(367, 50);
            label107.Name = "label107";
            label107.Size = new Size(108, 17);
            label107.TabIndex = 1;
            label107.Text = "Shen et al. (2020)";
            // 
            // label46
            // 
            label46.AutoSize = true;
            label46.Font = new Font("Segoe UI Symbol", 9.75F);
            label46.Location = new Point(367, 23);
            label46.Name = "label46";
            label46.Size = new Size(114, 17);
            label46.TabIndex = 1;
            label46.Text = "Mao-hydro (2000)";
            // 
            // label45
            // 
            label45.AutoSize = true;
            label45.Font = new Font("Segoe UI Symbol", 9.75F);
            label45.Location = new Point(6, 50);
            label45.Name = "label45";
            label45.Size = new Size(111, 17);
            label45.TabIndex = 1;
            label45.Text = "Mao-quasi (1986)";
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.Font = new Font("Segoe UI Symbol", 9.75F);
            label34.Location = new Point(6, 23);
            label34.Name = "label34";
            label34.Size = new Size(75, 17);
            label34.TabIndex = 1;
            label34.Text = "Mao (1978)";
            // 
            // textBoxShenP
            // 
            textBoxShenP.BackColor = Color.Navy;
            textBoxShenP.Font = new Font("Segoe UI Symbol", 11.25F);
            textBoxShenP.ForeColor = Color.White;
            textBoxShenP.Location = new Point(620, 47);
            textBoxShenP.Name = "textBoxShenP";
            textBoxShenP.ReadOnly = true;
            textBoxShenP.Size = new Size(64, 27);
            textBoxShenP.TabIndex = 0;
            textBoxShenP.Text = "0";
            // 
            // label104
            // 
            label104.AutoSize = true;
            label104.Font = new Font("Segoe UI Symbol", 9.75F);
            label104.Location = new Point(683, 50);
            label104.Name = "label104";
            label104.Size = new Size(31, 17);
            label104.TabIndex = 1;
            label104.Text = "GPa";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(numericBoxRubyRagan);
            groupBox2.Location = new Point(136, 113);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(583, 48);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            groupBox2.Text = "Temperature dependency (Ragan et al., 1992) (Applicable in the range of 50-600K)";
            // 
            // numericBoxRubyRagan
            // 
            numericBoxRubyRagan.BackColor = Color.Transparent;
            numericBoxRubyRagan.DecimalPlaces = 3;
            numericBoxRubyRagan.Font = new Font("Segoe UI Symbol", 9.75F);
            numericBoxRubyRagan.FooterText = "+ 4.49×10⁻²×T - 4.81×10⁻⁴×T² + 3.71×10⁻⁷×T³ )⁻¹ ×10⁷    [nm]";
            numericBoxRubyRagan.HeaderText = "R1(T, P=0) = (";
            numericBoxRubyRagan.Location = new Point(9, 18);
            numericBoxRubyRagan.Margin = new Padding(0);
            numericBoxRubyRagan.MaximumSize = new Size(1000, 28);
            numericBoxRubyRagan.MinimumSize = new Size(1, 18);
            numericBoxRubyRagan.Name = "numericBoxRubyRagan";
            numericBoxRubyRagan.RadianValue = 251.72883801514214D;
            numericBoxRubyRagan.Size = new Size(542, 25);
            numericBoxRubyRagan.TabIndex = 3;
            numericBoxRubyRagan.TextFont = new Font("Segoe UI Symbol", 9F);
            numericBoxRubyRagan.Value = 14423D;
            numericBoxRubyRagan.ValueChanged += numericBoxRubyRagan_ValueChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(checkBoxRubyR1_0CalculatedFromRagan);
            groupBox3.Controls.Add(numericBoxRubyT);
            groupBox3.Controls.Add(checkBoxRubyTemeratureSameAsRef);
            groupBox3.Controls.Add(numericBoxRubyR1_0);
            groupBox3.Location = new Point(138, 19);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(319, 88);
            groupBox3.TabIndex = 5;
            groupBox3.TabStop = false;
            groupBox3.Text = "Measument condition";
            // 
            // checkBoxRubyR1_0CalculatedFromRagan
            // 
            checkBoxRubyR1_0CalculatedFromRagan.AutoSize = true;
            checkBoxRubyR1_0CalculatedFromRagan.Font = new Font("Segoe UI Symbol", 9.75F);
            checkBoxRubyR1_0CalculatedFromRagan.Location = new Point(185, 43);
            checkBoxRubyR1_0CalculatedFromRagan.Name = "checkBoxRubyR1_0CalculatedFromRagan";
            checkBoxRubyR1_0CalculatedFromRagan.Size = new Size(132, 38);
            checkBoxRubyR1_0CalculatedFromRagan.TabIndex = 7;
            checkBoxRubyR1_0CalculatedFromRagan.Text = "Caluculate from\r\n Ragan's equation";
            checkBoxRubyR1_0CalculatedFromRagan.UseVisualStyleBackColor = true;
            checkBoxRubyR1_0CalculatedFromRagan.CheckedChanged += checkBoxRubyR1CalculatedFromRagan_CheckedChanged;
            // 
            // numericBoxRubyT
            // 
            numericBoxRubyT.BackColor = Color.Transparent;
            numericBoxRubyT.DecimalPlaces = 2;
            numericBoxRubyT.Enabled = false;
            numericBoxRubyT.Font = new Font("Segoe UI Symbol", 9.75F);
            numericBoxRubyT.FooterText = "K";
            numericBoxRubyT.HeaderText = "T =";
            numericBoxRubyT.Location = new Point(11, 18);
            numericBoxRubyT.Margin = new Padding(0);
            numericBoxRubyT.MaximumSize = new Size(1000, 28);
            numericBoxRubyT.MinimumSize = new Size(1, 18);
            numericBoxRubyT.Name = "numericBoxRubyT";
            numericBoxRubyT.RadianValue = 5.18624587230115D;
            numericBoxRubyT.Size = new Size(116, 23);
            numericBoxRubyT.TabIndex = 3;
            numericBoxRubyT.TextFont = new Font("Segoe UI Symbol", 9F);
            numericBoxRubyT.Value = 297.15D;
            numericBoxRubyT.ValueChanged += numericBoxRubyT_ValueChanged;
            // 
            // checkBoxRubyTemeratureSameAsRef
            // 
            checkBoxRubyTemeratureSameAsRef.AutoSize = true;
            checkBoxRubyTemeratureSameAsRef.Checked = true;
            checkBoxRubyTemeratureSameAsRef.CheckState = CheckState.Checked;
            checkBoxRubyTemeratureSameAsRef.Font = new Font("Segoe UI Symbol", 9.75F);
            checkBoxRubyTemeratureSameAsRef.Location = new Point(135, 20);
            checkBoxRubyTemeratureSameAsRef.Name = "checkBoxRubyTemeratureSameAsRef";
            checkBoxRubyTemeratureSameAsRef.Size = new Size(135, 21);
            checkBoxRubyTemeratureSameAsRef.TabIndex = 7;
            checkBoxRubyTemeratureSameAsRef.Text = "Same as reference";
            checkBoxRubyTemeratureSameAsRef.UseVisualStyleBackColor = true;
            checkBoxRubyTemeratureSameAsRef.CheckedChanged += checkBoxRubyTemeratureSameAsRef_CheckedChanged;
            // 
            // numericBoxRubyR1_0
            // 
            numericBoxRubyR1_0.BackColor = Color.Transparent;
            numericBoxRubyR1_0.DecimalPlaces = 3;
            numericBoxRubyR1_0.Font = new Font("Segoe UI Symbol", 9.75F);
            numericBoxRubyR1_0.FooterText = "nm";
            numericBoxRubyR1_0.HeaderText = "R1₀=R1(T,P=0)";
            numericBoxRubyR1_0.Location = new Point(11, 52);
            numericBoxRubyR1_0.Margin = new Padding(0);
            numericBoxRubyR1_0.MaximumSize = new Size(1000, 28);
            numericBoxRubyR1_0.MinimumSize = new Size(1, 18);
            numericBoxRubyR1_0.Name = "numericBoxRubyR1_0";
            numericBoxRubyR1_0.RadianValue = 12.12392964897861D;
            numericBoxRubyR1_0.Size = new Size(171, 25);
            numericBoxRubyR1_0.TabIndex = 3;
            numericBoxRubyR1_0.TextFont = new Font("Segoe UI Symbol", 9F);
            numericBoxRubyR1_0.Value = 694.65D;
            numericBoxRubyR1_0.ValueChanged += numericBoxR1_0_ValueChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(buttonRubyRefR1Set);
            groupBox1.Controls.Add(numericBoxRubyRefT);
            groupBox1.Controls.Add(numericBoxRubyRefR1);
            groupBox1.Location = new Point(463, 19);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(256, 88);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Reference condition";
            // 
            // buttonRubyRefR1Set
            // 
            buttonRubyRefR1Set.AutoSize = true;
            buttonRubyRefR1Set.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            buttonRubyRefR1Set.Font = new Font("Segoe UI Symbol", 9.75F);
            buttonRubyRefR1Set.Location = new Point(128, 49);
            buttonRubyRefR1Set.Name = "buttonRubyRefR1Set";
            buttonRubyRefR1Set.Size = new Size(122, 27);
            buttonRubyRefR1Set.TabIndex = 4;
            buttonRubyRefR1Set.Text = "Set the current R1";
            buttonRubyRefR1Set.UseVisualStyleBackColor = true;
            buttonRubyRefR1Set.Click += buttonRubyRefR1Set_Click;
            // 
            // numericBoxRubyRefT
            // 
            numericBoxRubyRefT.BackColor = Color.Transparent;
            numericBoxRubyRefT.DecimalPlaces = 2;
            numericBoxRubyRefT.Font = new Font("Segoe UI Symbol", 9.75F);
            numericBoxRubyRefT.FooterText = "K";
            numericBoxRubyRefT.Location = new Point(46, 21);
            numericBoxRubyRefT.Margin = new Padding(0);
            numericBoxRubyRefT.MaximumSize = new Size(1000, 28);
            numericBoxRubyRefT.MinimumSize = new Size(1, 18);
            numericBoxRubyRefT.Name = "numericBoxRubyRefT";
            numericBoxRubyRefT.RadianValue = 5.18624587230115D;
            numericBoxRubyRefT.Size = new Size(67, 23);
            numericBoxRubyRefT.TabIndex = 3;
            numericBoxRubyRefT.TextFont = new Font("Segoe UI Symbol", 9F);
            numericBoxRubyRefT.Value = 297.15D;
            numericBoxRubyRefT.ValueChanged += numericBoxRubyRefT_ValueChanged;
            // 
            // numericBoxRubyRefR1
            // 
            numericBoxRubyRefR1.BackColor = Color.Transparent;
            numericBoxRubyRefR1.DecimalPlaces = 3;
            numericBoxRubyRefR1.Font = new Font("Segoe UI Symbol", 9.75F);
            numericBoxRubyRefR1.FooterText = "nm";
            numericBoxRubyRefR1.HeaderText = "R1(T=                , P=0) = ";
            numericBoxRubyRefR1.Location = new Point(7, 21);
            numericBoxRubyRefR1.Margin = new Padding(0);
            numericBoxRubyRefR1.MaximumSize = new Size(1000, 28);
            numericBoxRubyRefR1.MinimumSize = new Size(1, 18);
            numericBoxRubyRefR1.Name = "numericBoxRubyRefR1";
            numericBoxRubyRefR1.RadianValue = 12.116948331970631D;
            numericBoxRubyRefR1.Size = new Size(243, 25);
            numericBoxRubyRefR1.TabIndex = 3;
            numericBoxRubyRefR1.TextFont = new Font("Segoe UI Symbol", 9F);
            numericBoxRubyRefR1.Value = 694.25D;
            numericBoxRubyRefR1.ValueChanged += numericBoxRubyRefR1_ValueChanged;
            // 
            // numericBoxRubyR1
            // 
            numericBoxRubyR1.BackColor = Color.Transparent;
            numericBoxRubyR1.DecimalPlaces = 3;
            numericBoxRubyR1.Font = new Font("Segoe UI Symbol", 9.75F);
            numericBoxRubyR1.FooterText = "nm";
            numericBoxRubyR1.HeaderText = "R1";
            numericBoxRubyR1.Location = new Point(8, 28);
            numericBoxRubyR1.Margin = new Padding(0);
            numericBoxRubyR1.MaximumSize = new Size(1000, 32);
            numericBoxRubyR1.MinimumSize = new Size(1, 22);
            numericBoxRubyR1.Name = "numericBoxRubyR1";
            numericBoxRubyR1.RadianValue = 12.12392964897861D;
            numericBoxRubyR1.Size = new Size(125, 29);
            numericBoxRubyR1.TabIndex = 3;
            numericBoxRubyR1.TextFont = new Font("Segoe UI Symbol", 11F);
            numericBoxRubyR1.Value = 694.65D;
            numericBoxRubyR1.ValueChanged += numericBoxRubyR1_ValueChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI Symbol", 9.75F);
            label17.Location = new Point(6, 68);
            label17.Name = "label17";
            label17.Size = new Size(108, 17);
            label17.TabIndex = 1;
            label17.Text = "Temperature unit";
            // 
            // groupBoxAkahama2006
            // 
            groupBoxAkahama2006.Controls.Add(textBoxDiamondFratandunoHigh);
            groupBoxAkahama2006.Controls.Add(textBoxDiamondAkahama2006P);
            groupBoxAkahama2006.Controls.Add(label44);
            groupBoxAkahama2006.Controls.Add(label29);
            groupBoxAkahama2006.Controls.Add(textBoxAkahama2004A);
            groupBoxAkahama2006.Controls.Add(textBoxAkahama2004B);
            groupBoxAkahama2006.Controls.Add(textBoxDiamondRamanNu0);
            groupBoxAkahama2006.Controls.Add(textBoxAkahama2004C);
            groupBoxAkahama2006.Controls.Add(label43);
            groupBoxAkahama2006.Controls.Add(label5);
            groupBoxAkahama2006.Controls.Add(textBoxDiamondRamanNu);
            groupBoxAkahama2006.Controls.Add(label6);
            groupBoxAkahama2006.Controls.Add(label30);
            groupBoxAkahama2006.Controls.Add(label7);
            groupBoxAkahama2006.Controls.Add(textBoxAkahama2006K0);
            groupBoxAkahama2006.Controls.Add(label8);
            groupBoxAkahama2006.Controls.Add(textBoxAkahama2006K0Prime);
            groupBoxAkahama2006.Controls.Add(textBoxDiamondFratandunoLow);
            groupBoxAkahama2006.Controls.Add(textBoxDiamondAkahama2004P);
            groupBoxAkahama2006.Controls.Add(label4);
            groupBoxAkahama2006.Controls.Add(label20);
            groupBoxAkahama2006.Controls.Add(label19);
            groupBoxAkahama2006.Controls.Add(label3);
            groupBoxAkahama2006.Controls.Add(label24);
            groupBoxAkahama2006.Controls.Add(label31);
            groupBoxAkahama2006.Controls.Add(label9);
            groupBoxAkahama2006.Controls.Add(label33);
            groupBoxAkahama2006.Controls.Add(label21);
            groupBoxAkahama2006.Controls.Add(label32);
            groupBoxAkahama2006.Controls.Add(label35);
            groupBoxAkahama2006.Dock = DockStyle.Bottom;
            groupBoxAkahama2006.Font = new Font("Verdana", 9F);
            groupBoxAkahama2006.Location = new Point(0, 548);
            groupBoxAkahama2006.Name = "groupBoxAkahama2006";
            groupBoxAkahama2006.Size = new Size(725, 124);
            groupBoxAkahama2006.TabIndex = 13;
            groupBoxAkahama2006.TabStop = false;
            groupBoxAkahama2006.Text = "Pressure calculation from the Raman edge";
            // 
            // textBoxDiamondFratandunoHigh
            // 
            textBoxDiamondFratandunoHigh.BackColor = Color.Navy;
            textBoxDiamondFratandunoHigh.Font = new Font("Segoe UI Symbol", 11.25F);
            textBoxDiamondFratandunoHigh.ForeColor = Color.White;
            textBoxDiamondFratandunoHigh.Location = new Point(615, 92);
            textBoxDiamondFratandunoHigh.Name = "textBoxDiamondFratandunoHigh";
            textBoxDiamondFratandunoHigh.ReadOnly = true;
            textBoxDiamondFratandunoHigh.Size = new Size(71, 27);
            textBoxDiamondFratandunoHigh.TabIndex = 0;
            textBoxDiamondFratandunoHigh.Text = "0";
            // 
            // textBoxDiamondAkahama2006P
            // 
            textBoxDiamondAkahama2006P.BackColor = Color.Navy;
            textBoxDiamondAkahama2006P.Font = new Font("Segoe UI Symbol", 11.25F);
            textBoxDiamondAkahama2006P.ForeColor = Color.White;
            textBoxDiamondAkahama2006P.Location = new Point(615, 38);
            textBoxDiamondAkahama2006P.Name = "textBoxDiamondAkahama2006P";
            textBoxDiamondAkahama2006P.ReadOnly = true;
            textBoxDiamondAkahama2006P.Size = new Size(71, 27);
            textBoxDiamondAkahama2006P.TabIndex = 0;
            textBoxDiamondAkahama2006P.Text = "0";
            // 
            // label44
            // 
            label44.Font = new Font("Verdana", 9F);
            label44.Location = new Point(6, 43);
            label44.Name = "label44";
            label44.Size = new Size(24, 12);
            label44.TabIndex = 1;
            label44.Text = "ν0";
            // 
            // label29
            // 
            label29.Font = new Font("Verdana", 9F);
            label29.Location = new Point(4, 20);
            label29.Name = "label29";
            label29.Size = new Size(16, 12);
            label29.TabIndex = 1;
            label29.Text = "ν";
            // 
            // textBoxDiamondRamanNu0
            // 
            textBoxDiamondRamanNu0.Font = new Font("Verdana", 9F);
            textBoxDiamondRamanNu0.Location = new Point(36, 39);
            textBoxDiamondRamanNu0.Name = "textBoxDiamondRamanNu0";
            textBoxDiamondRamanNu0.Size = new Size(63, 22);
            textBoxDiamondRamanNu0.TabIndex = 0;
            textBoxDiamondRamanNu0.Text = "1334";
            textBoxDiamondRamanNu0.TextChanged += textBoxNu_TextChanged;
            // 
            // label43
            // 
            label43.Font = new Font("Verdana", 9F);
            label43.Location = new Point(105, 46);
            label43.Name = "label43";
            label43.Size = new Size(52, 12);
            label43.TabIndex = 1;
            label43.Text = "cm^-1";
            // 
            // textBoxDiamondRamanNu
            // 
            textBoxDiamondRamanNu.Font = new Font("Verdana", 9F);
            textBoxDiamondRamanNu.Location = new Point(35, 16);
            textBoxDiamondRamanNu.Name = "textBoxDiamondRamanNu";
            textBoxDiamondRamanNu.Size = new Size(64, 22);
            textBoxDiamondRamanNu.TabIndex = 0;
            textBoxDiamondRamanNu.Text = "0";
            textBoxDiamondRamanNu.TextChanged += textBoxNu_TextChanged;
            // 
            // label30
            // 
            label30.Font = new Font("Verdana", 9F);
            label30.Location = new Point(105, 20);
            label30.Name = "label30";
            label30.Size = new Size(52, 12);
            label30.TabIndex = 1;
            label30.Text = "cm^-1";
            // 
            // textBoxAkahama2006K0
            // 
            textBoxAkahama2006K0.Font = new Font("Segoe UI Symbol", 8.25F);
            textBoxAkahama2006K0.Location = new Point(309, 43);
            textBoxAkahama2006K0.Name = "textBoxAkahama2006K0";
            textBoxAkahama2006K0.Size = new Size(44, 22);
            textBoxAkahama2006K0.TabIndex = 0;
            textBoxAkahama2006K0.Text = "547";
            textBoxAkahama2006K0.TextChanged += textBoxNu_TextChanged;
            // 
            // textBoxAkahama2006K0Prime
            // 
            textBoxAkahama2006K0Prime.Font = new Font("Segoe UI Symbol", 8.25F);
            textBoxAkahama2006K0Prime.Location = new Point(474, 43);
            textBoxAkahama2006K0Prime.Name = "textBoxAkahama2006K0Prime";
            textBoxAkahama2006K0Prime.Size = new Size(44, 22);
            textBoxAkahama2006K0Prime.TabIndex = 0;
            textBoxAkahama2006K0Prime.Text = "3.75";
            textBoxAkahama2006K0Prime.TextChanged += textBoxNu_TextChanged;
            // 
            // textBoxDiamondFratandunoLow
            // 
            textBoxDiamondFratandunoLow.BackColor = Color.Navy;
            textBoxDiamondFratandunoLow.Font = new Font("Segoe UI Symbol", 11.25F);
            textBoxDiamondFratandunoLow.ForeColor = Color.White;
            textBoxDiamondFratandunoLow.Location = new Point(615, 65);
            textBoxDiamondFratandunoLow.Name = "textBoxDiamondFratandunoLow";
            textBoxDiamondFratandunoLow.ReadOnly = true;
            textBoxDiamondFratandunoLow.Size = new Size(71, 27);
            textBoxDiamondFratandunoLow.TabIndex = 0;
            textBoxDiamondFratandunoLow.Text = "0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI Symbol", 8.25F);
            label4.Location = new Point(160, 19);
            label4.Name = "label4";
            label4.Size = new Size(90, 13);
            label4.TabIndex = 1;
            label4.Text = "Akahama (2004):";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI Symbol", 8.25F);
            label20.Location = new Point(160, 100);
            label20.Name = "label20";
            label20.Size = new Size(430, 13);
            label20.TabIndex = 1;
            label20.Text = "Fratanduono+ (2021, >200GPa):  P = 199.49 - 852.78 × Δν/ν0 + 3103.8 × (Δν/ν0)² = ";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI Symbol", 8.25F);
            label19.Location = new Point(160, 73);
            label19.Name = "label19";
            label19.Size = new Size(387, 13);
            label19.TabIndex = 1;
            label19.Text = "Fratanduono+ (2021, <300GPa):  P = 503.77 × Δν/ν0 + 753.83 × (Δν/ν0)² = ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Symbol", 8.25F);
            label3.Location = new Point(160, 46);
            label3.Name = "label3";
            label3.Size = new Size(90, 13);
            label3.TabIndex = 1;
            label3.Text = "Akahama (2006):";
            // 
            // label24
            // 
            label24.Font = new Font("Verdana", 9F);
            label24.Location = new Point(688, 72);
            label24.Name = "label24";
            label24.Size = new Size(34, 12);
            label24.TabIndex = 1;
            label24.Text = "GPa";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Font = new Font("Segoe UI Symbol", 8.25F);
            label31.Location = new Point(264, 46);
            label31.Name = "label31";
            label31.Size = new Size(39, 13);
            label31.TabIndex = 1;
            label31.Text = "P = K0";
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Font = new Font("Segoe UI Symbol", 8.25F);
            label33.Location = new Point(520, 46);
            label33.Name = "label33";
            label33.Size = new Size(65, 13);
            label33.TabIndex = 1;
            label33.Text = "-1)*Δν/ν0]=";
            // 
            // label21
            // 
            label21.Font = new Font("Verdana", 9F);
            label21.Location = new Point(691, 99);
            label21.Name = "label21";
            label21.Size = new Size(34, 12);
            label21.TabIndex = 1;
            label21.Text = "GPa";
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Font = new Font("Segoe UI Symbol", 8.25F);
            label32.Location = new Point(357, 46);
            label32.Name = "label32";
            label32.Size = new Size(115, 13);
            label32.TabIndex = 1;
            label32.Text = "× Δν/ν0 × [1+1/2 (K0'";
            // 
            // label35
            // 
            label35.Font = new Font("Verdana", 9F);
            label35.Location = new Point(691, 45);
            label35.Name = "label35";
            label35.Size = new Size(34, 12);
            label35.TabIndex = 1;
            label35.Text = "GPa";
            // 
            // flowLayoutPanelEOS
            // 
            flowLayoutPanelEOS.AutoScroll = true;
            flowLayoutPanelEOS.Controls.Add(groupBoxGold);
            flowLayoutPanelEOS.Controls.Add(groupBoxPlatinum);
            flowLayoutPanelEOS.Controls.Add(groupBoxNaClB1);
            flowLayoutPanelEOS.Controls.Add(groupBoxNaClB2);
            flowLayoutPanelEOS.Controls.Add(groupBoxPericlase);
            flowLayoutPanelEOS.Controls.Add(groupBoxCorundum);
            flowLayoutPanelEOS.Controls.Add(groupBoxAr);
            flowLayoutPanelEOS.Controls.Add(groupBoxRe);
            flowLayoutPanelEOS.Controls.Add(groupBoxMo);
            flowLayoutPanelEOS.Controls.Add(groupBoxPb);
            flowLayoutPanelEOS.Dock = DockStyle.Fill;
            flowLayoutPanelEOS.Font = new Font("Tahoma", 8.25F);
            flowLayoutPanelEOS.Location = new Point(0, 29);
            flowLayoutPanelEOS.Name = "flowLayoutPanelEOS";
            flowLayoutPanelEOS.Size = new Size(725, 178);
            flowLayoutPanelEOS.TabIndex = 14;
            // 
            // groupBoxGold
            // 
            groupBoxGold.Controls.Add(numericBoxAuFratanduono);
            groupBoxGold.Controls.Add(numericBoxAuYokoo);
            groupBoxGold.Controls.Add(numericalTextBoxGoldJamieson);
            groupBoxGold.Controls.Add(numericalTextBoxGoldTsuchiya);
            groupBoxGold.Controls.Add(numericalTextBoxGoldSim);
            groupBoxGold.Controls.Add(label11);
            groupBoxGold.Controls.Add(numericalTextBoxGoldAnderson);
            groupBoxGold.Controls.Add(label49);
            groupBoxGold.Controls.Add(numericalTextBoxGoldA);
            groupBoxGold.Controls.Add(numericBoxGoldA0);
            groupBoxGold.Controls.Add(label22);
            groupBoxGold.Controls.Add(label69);
            groupBoxGold.Controls.Add(label15);
            groupBoxGold.Controls.Add(label27);
            groupBoxGold.Font = new Font("Segoe UI Symbol", 9.75F);
            groupBoxGold.Location = new Point(3, 3);
            groupBoxGold.Name = "groupBoxGold";
            groupBoxGold.Size = new Size(218, 208);
            groupBoxGold.TabIndex = 9;
            groupBoxGold.TabStop = false;
            groupBoxGold.Text = "Gold";
            // 
            // numericBoxAuFratanduono
            // 
            numericBoxAuFratanduono.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxAuFratanduono.BackColor = SystemColors.Control;
            numericBoxAuFratanduono.DecimalPlaces = 3;
            numericBoxAuFratanduono.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxAuFratanduono.FooterBackColor = SystemColors.Control;
            numericBoxAuFratanduono.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxAuFratanduono.FooterText = "GPa";
            numericBoxAuFratanduono.HeaderBackColor = SystemColors.Control;
            numericBoxAuFratanduono.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxAuFratanduono.Location = new Point(84, 179);
            numericBoxAuFratanduono.Margin = new Padding(0);
            numericBoxAuFratanduono.MaximumSize = new Size(1000, 30);
            numericBoxAuFratanduono.MinimumSize = new Size(1, 20);
            numericBoxAuFratanduono.Name = "numericBoxAuFratanduono";
            numericBoxAuFratanduono.Padding = new Padding(0, 0, 1, 0);
            numericBoxAuFratanduono.Size = new Size(123, 27);
            numericBoxAuFratanduono.SkipEventDuringInput = false;
            numericBoxAuFratanduono.SmartIncrement = true;
            numericBoxAuFratanduono.TabIndex = 12;
            numericBoxAuFratanduono.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericBoxAuFratanduono.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericBoxAuFratanduono.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericBoxAuFratanduono.ThonsandsSeparator = true;
            // 
            // numericBoxAuYokoo
            // 
            numericBoxAuYokoo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxAuYokoo.BackColor = SystemColors.Control;
            numericBoxAuYokoo.DecimalPlaces = 3;
            numericBoxAuYokoo.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxAuYokoo.FooterBackColor = SystemColors.Control;
            numericBoxAuYokoo.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxAuYokoo.FooterText = "GPa";
            numericBoxAuYokoo.HeaderBackColor = SystemColors.Control;
            numericBoxAuYokoo.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxAuYokoo.Location = new Point(84, 153);
            numericBoxAuYokoo.Margin = new Padding(0);
            numericBoxAuYokoo.MaximumSize = new Size(1000, 30);
            numericBoxAuYokoo.MinimumSize = new Size(1, 20);
            numericBoxAuYokoo.Name = "numericBoxAuYokoo";
            numericBoxAuYokoo.Padding = new Padding(0, 0, 1, 0);
            numericBoxAuYokoo.Size = new Size(123, 27);
            numericBoxAuYokoo.SkipEventDuringInput = false;
            numericBoxAuYokoo.SmartIncrement = true;
            numericBoxAuYokoo.TabIndex = 12;
            numericBoxAuYokoo.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericBoxAuYokoo.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericBoxAuYokoo.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericBoxAuYokoo.ThonsandsSeparator = true;
            // 
            // numericalTextBoxGoldJamieson
            // 
            numericalTextBoxGoldJamieson.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxGoldJamieson.BackColor = SystemColors.Control;
            numericalTextBoxGoldJamieson.DecimalPlaces = 3;
            numericalTextBoxGoldJamieson.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxGoldJamieson.FooterBackColor = SystemColors.Control;
            numericalTextBoxGoldJamieson.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxGoldJamieson.FooterText = "GPa";
            numericalTextBoxGoldJamieson.HeaderBackColor = SystemColors.Control;
            numericalTextBoxGoldJamieson.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxGoldJamieson.Location = new Point(84, 51);
            numericalTextBoxGoldJamieson.Margin = new Padding(0);
            numericalTextBoxGoldJamieson.MaximumSize = new Size(1000, 30);
            numericalTextBoxGoldJamieson.MinimumSize = new Size(1, 20);
            numericalTextBoxGoldJamieson.Name = "numericalTextBoxGoldJamieson";
            numericalTextBoxGoldJamieson.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxGoldJamieson.Size = new Size(123, 27);
            numericalTextBoxGoldJamieson.SkipEventDuringInput = false;
            numericalTextBoxGoldJamieson.SmartIncrement = true;
            numericalTextBoxGoldJamieson.TabIndex = 12;
            numericalTextBoxGoldJamieson.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxGoldJamieson.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxGoldJamieson.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxGoldJamieson.ThonsandsSeparator = true;
            // 
            // numericalTextBoxGoldTsuchiya
            // 
            numericalTextBoxGoldTsuchiya.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxGoldTsuchiya.BackColor = SystemColors.Control;
            numericalTextBoxGoldTsuchiya.DecimalPlaces = 3;
            numericalTextBoxGoldTsuchiya.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxGoldTsuchiya.FooterBackColor = SystemColors.Control;
            numericalTextBoxGoldTsuchiya.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxGoldTsuchiya.FooterText = "GPa";
            numericalTextBoxGoldTsuchiya.HeaderBackColor = SystemColors.Control;
            numericalTextBoxGoldTsuchiya.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxGoldTsuchiya.Location = new Point(84, 127);
            numericalTextBoxGoldTsuchiya.Margin = new Padding(0);
            numericalTextBoxGoldTsuchiya.MaximumSize = new Size(1000, 30);
            numericalTextBoxGoldTsuchiya.MinimumSize = new Size(1, 20);
            numericalTextBoxGoldTsuchiya.Name = "numericalTextBoxGoldTsuchiya";
            numericalTextBoxGoldTsuchiya.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxGoldTsuchiya.Size = new Size(123, 27);
            numericalTextBoxGoldTsuchiya.SkipEventDuringInput = false;
            numericalTextBoxGoldTsuchiya.SmartIncrement = true;
            numericalTextBoxGoldTsuchiya.TabIndex = 12;
            numericalTextBoxGoldTsuchiya.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxGoldTsuchiya.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxGoldTsuchiya.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxGoldTsuchiya.ThonsandsSeparator = true;
            // 
            // numericalTextBoxGoldSim
            // 
            numericalTextBoxGoldSim.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxGoldSim.BackColor = SystemColors.Control;
            numericalTextBoxGoldSim.DecimalPlaces = 3;
            numericalTextBoxGoldSim.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxGoldSim.FooterBackColor = SystemColors.Control;
            numericalTextBoxGoldSim.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxGoldSim.FooterText = "GPa";
            numericalTextBoxGoldSim.HeaderBackColor = SystemColors.Control;
            numericalTextBoxGoldSim.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxGoldSim.Location = new Point(84, 102);
            numericalTextBoxGoldSim.Margin = new Padding(0);
            numericalTextBoxGoldSim.MaximumSize = new Size(1000, 30);
            numericalTextBoxGoldSim.MinimumSize = new Size(1, 20);
            numericalTextBoxGoldSim.Name = "numericalTextBoxGoldSim";
            numericalTextBoxGoldSim.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxGoldSim.Size = new Size(123, 27);
            numericalTextBoxGoldSim.SkipEventDuringInput = false;
            numericalTextBoxGoldSim.SmartIncrement = true;
            numericalTextBoxGoldSim.TabIndex = 12;
            numericalTextBoxGoldSim.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxGoldSim.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxGoldSim.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxGoldSim.ThonsandsSeparator = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Symbol", 9F);
            label11.ImeMode = ImeMode.NoControl;
            label11.Location = new Point(20, 157);
            label11.Name = "label11";
            label11.Size = new Size(64, 15);
            label11.TabIndex = 7;
            label11.Text = "Yokoo (09)";
            // 
            // numericalTextBoxGoldAnderson
            // 
            numericalTextBoxGoldAnderson.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxGoldAnderson.BackColor = SystemColors.Control;
            numericalTextBoxGoldAnderson.DecimalPlaces = 3;
            numericalTextBoxGoldAnderson.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxGoldAnderson.FooterBackColor = SystemColors.Control;
            numericalTextBoxGoldAnderson.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxGoldAnderson.FooterText = "GPa";
            numericalTextBoxGoldAnderson.HeaderBackColor = SystemColors.Control;
            numericalTextBoxGoldAnderson.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxGoldAnderson.Location = new Point(84, 77);
            numericalTextBoxGoldAnderson.Margin = new Padding(0);
            numericalTextBoxGoldAnderson.MaximumSize = new Size(1000, 30);
            numericalTextBoxGoldAnderson.MinimumSize = new Size(1, 20);
            numericalTextBoxGoldAnderson.Name = "numericalTextBoxGoldAnderson";
            numericalTextBoxGoldAnderson.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxGoldAnderson.Size = new Size(123, 27);
            numericalTextBoxGoldAnderson.SkipEventDuringInput = false;
            numericalTextBoxGoldAnderson.SmartIncrement = true;
            numericalTextBoxGoldAnderson.TabIndex = 12;
            numericalTextBoxGoldAnderson.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxGoldAnderson.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxGoldAnderson.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxGoldAnderson.ThonsandsSeparator = true;
            // 
            // label49
            // 
            label49.AutoSize = true;
            label49.Font = new Font("Segoe UI Symbol", 9F);
            label49.ImeMode = ImeMode.NoControl;
            label49.Location = new Point(6, 56);
            label49.Name = "label49";
            label49.Size = new Size(79, 15);
            label49.TabIndex = 7;
            label49.Text = "Jamieson (82)";
            // 
            // numericalTextBoxGoldA
            // 
            numericalTextBoxGoldA.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxGoldA.BackColor = SystemColors.Control;
            numericalTextBoxGoldA.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxGoldA.FooterBackColor = SystemColors.Control;
            numericalTextBoxGoldA.FooterText = "Å";
            numericalTextBoxGoldA.HeaderBackColor = SystemColors.Control;
            numericalTextBoxGoldA.HeaderText = "a ";
            numericalTextBoxGoldA.Location = new Point(111, 20);
            numericalTextBoxGoldA.Margin = new Padding(0);
            numericalTextBoxGoldA.MaximumSize = new Size(1000, 30);
            numericalTextBoxGoldA.MinimumSize = new Size(1, 20);
            numericalTextBoxGoldA.Name = "numericalTextBoxGoldA";
            numericalTextBoxGoldA.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxGoldA.RadianValue = 0.071178890219458738D;
            numericalTextBoxGoldA.Size = new Size(99, 27);
            numericalTextBoxGoldA.SkipEventDuringInput = false;
            numericalTextBoxGoldA.SmartIncrement = true;
            numericalTextBoxGoldA.TabIndex = 10;
            numericalTextBoxGoldA.TextFont = new Font("メイリオ", 9F);
            numericalTextBoxGoldA.ThonsandsSeparator = true;
            numericalTextBoxGoldA.Value = 4.07825D;
            numericalTextBoxGoldA.ValueChanged += textBox_TextChanged;
            // 
            // numericBoxGoldA0
            // 
            numericBoxGoldA0.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxGoldA0.BackColor = SystemColors.Control;
            numericBoxGoldA0.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxGoldA0.FooterBackColor = SystemColors.Control;
            numericBoxGoldA0.FooterText = "Å";
            numericBoxGoldA0.HeaderBackColor = SystemColors.Control;
            numericBoxGoldA0.HeaderText = "a₀";
            numericBoxGoldA0.Location = new Point(3, 20);
            numericBoxGoldA0.Margin = new Padding(0);
            numericBoxGoldA0.MaximumSize = new Size(1000, 30);
            numericBoxGoldA0.MinimumSize = new Size(1, 20);
            numericBoxGoldA0.Name = "numericBoxGoldA0";
            numericBoxGoldA0.Padding = new Padding(0, 0, 1, 0);
            numericBoxGoldA0.RadianValue = 0.071178890219458738D;
            numericBoxGoldA0.Size = new Size(108, 27);
            numericBoxGoldA0.SkipEventDuringInput = false;
            numericBoxGoldA0.SmartIncrement = true;
            numericBoxGoldA0.TabIndex = 10;
            numericBoxGoldA0.TextFont = new Font("メイリオ", 9F);
            numericBoxGoldA0.ThonsandsSeparator = true;
            numericBoxGoldA0.Value = 4.07825D;
            numericBoxGoldA0.ValueChanged += textBox_TextChanged;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("Segoe UI Symbol", 9F);
            label22.ImeMode = ImeMode.NoControl;
            label22.Location = new Point(4, 81);
            label22.Name = "label22";
            label22.Size = new Size(81, 15);
            label22.TabIndex = 7;
            label22.Text = "Anderson (89)";
            // 
            // label69
            // 
            label69.AutoSize = true;
            label69.Font = new Font("Segoe UI Symbol", 9F);
            label69.ImeMode = ImeMode.NoControl;
            label69.Location = new Point(7, 131);
            label69.Name = "label69";
            label69.Size = new Size(77, 15);
            label69.TabIndex = 7;
            label69.Text = "Tsuchiya (03)";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI Symbol", 9F);
            label15.ImeMode = ImeMode.NoControl;
            label15.Location = new Point(34, 107);
            label15.Name = "label15";
            label15.Size = new Size(50, 15);
            label15.TabIndex = 7;
            label15.Text = "Sim (02)";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("Segoe UI Symbol", 7F);
            label27.ImeMode = ImeMode.NoControl;
            label27.Location = new Point(3, 184);
            label27.Name = "label27";
            label27.Size = new Size(81, 12);
            label27.TabIndex = 7;
            label27.Text = "Fratanduono (21)";
            // 
            // groupBoxPlatinum
            // 
            groupBoxPlatinum.Controls.Add(numericBoxPtFratanduono);
            groupBoxPlatinum.Controls.Add(numericBoxPtYokoo);
            groupBoxPlatinum.Controls.Add(numericalTextBoxPtMatsui);
            groupBoxPlatinum.Controls.Add(numericalTextBoxPtHolmes);
            groupBoxPlatinum.Controls.Add(label16);
            groupBoxPlatinum.Controls.Add(numericalTextBoxPtJamieson);
            groupBoxPlatinum.Controls.Add(label23);
            groupBoxPlatinum.Controls.Add(label26);
            groupBoxPlatinum.Controls.Add(label36);
            groupBoxPlatinum.Controls.Add(numericalTextBoxPtA);
            groupBoxPlatinum.Controls.Add(numericBoxPtA0);
            groupBoxPlatinum.Controls.Add(label37);
            groupBoxPlatinum.Font = new Font("Segoe UI Symbol", 9.75F);
            groupBoxPlatinum.Location = new Point(227, 3);
            groupBoxPlatinum.Name = "groupBoxPlatinum";
            groupBoxPlatinum.Size = new Size(218, 178);
            groupBoxPlatinum.TabIndex = 10;
            groupBoxPlatinum.TabStop = false;
            groupBoxPlatinum.Text = "Platinum";
            // 
            // numericBoxPtFratanduono
            // 
            numericBoxPtFratanduono.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxPtFratanduono.BackColor = SystemColors.Control;
            numericBoxPtFratanduono.DecimalPlaces = 3;
            numericBoxPtFratanduono.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxPtFratanduono.FooterBackColor = SystemColors.Control;
            numericBoxPtFratanduono.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxPtFratanduono.FooterText = "GPa";
            numericBoxPtFratanduono.HeaderBackColor = SystemColors.Control;
            numericBoxPtFratanduono.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxPtFratanduono.Location = new Point(84, 149);
            numericBoxPtFratanduono.Margin = new Padding(0);
            numericBoxPtFratanduono.MaximumSize = new Size(1000, 30);
            numericBoxPtFratanduono.MinimumSize = new Size(1, 20);
            numericBoxPtFratanduono.Name = "numericBoxPtFratanduono";
            numericBoxPtFratanduono.Padding = new Padding(0, 0, 1, 0);
            numericBoxPtFratanduono.Size = new Size(123, 27);
            numericBoxPtFratanduono.SkipEventDuringInput = false;
            numericBoxPtFratanduono.SmartIncrement = true;
            numericBoxPtFratanduono.TabIndex = 12;
            numericBoxPtFratanduono.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericBoxPtFratanduono.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericBoxPtFratanduono.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericBoxPtFratanduono.ThonsandsSeparator = true;
            // 
            // numericBoxPtYokoo
            // 
            numericBoxPtYokoo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxPtYokoo.BackColor = SystemColors.Control;
            numericBoxPtYokoo.DecimalPlaces = 3;
            numericBoxPtYokoo.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxPtYokoo.FooterBackColor = SystemColors.Control;
            numericBoxPtYokoo.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxPtYokoo.FooterText = "GPa";
            numericBoxPtYokoo.HeaderBackColor = SystemColors.Control;
            numericBoxPtYokoo.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxPtYokoo.Location = new Point(84, 123);
            numericBoxPtYokoo.Margin = new Padding(0);
            numericBoxPtYokoo.MaximumSize = new Size(1000, 30);
            numericBoxPtYokoo.MinimumSize = new Size(1, 20);
            numericBoxPtYokoo.Name = "numericBoxPtYokoo";
            numericBoxPtYokoo.Padding = new Padding(0, 0, 1, 0);
            numericBoxPtYokoo.Size = new Size(123, 27);
            numericBoxPtYokoo.SkipEventDuringInput = false;
            numericBoxPtYokoo.SmartIncrement = true;
            numericBoxPtYokoo.TabIndex = 12;
            numericBoxPtYokoo.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericBoxPtYokoo.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericBoxPtYokoo.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericBoxPtYokoo.ThonsandsSeparator = true;
            // 
            // numericalTextBoxPtMatsui
            // 
            numericalTextBoxPtMatsui.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxPtMatsui.BackColor = SystemColors.Control;
            numericalTextBoxPtMatsui.DecimalPlaces = 3;
            numericalTextBoxPtMatsui.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxPtMatsui.FooterBackColor = SystemColors.Control;
            numericalTextBoxPtMatsui.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxPtMatsui.FooterText = "GPa";
            numericalTextBoxPtMatsui.HeaderBackColor = SystemColors.Control;
            numericalTextBoxPtMatsui.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxPtMatsui.Location = new Point(84, 97);
            numericalTextBoxPtMatsui.Margin = new Padding(0);
            numericalTextBoxPtMatsui.MaximumSize = new Size(1000, 30);
            numericalTextBoxPtMatsui.MinimumSize = new Size(1, 20);
            numericalTextBoxPtMatsui.Name = "numericalTextBoxPtMatsui";
            numericalTextBoxPtMatsui.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxPtMatsui.Size = new Size(123, 27);
            numericalTextBoxPtMatsui.SkipEventDuringInput = false;
            numericalTextBoxPtMatsui.SmartIncrement = true;
            numericalTextBoxPtMatsui.TabIndex = 12;
            numericalTextBoxPtMatsui.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxPtMatsui.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxPtMatsui.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxPtMatsui.ThonsandsSeparator = true;
            // 
            // numericalTextBoxPtHolmes
            // 
            numericalTextBoxPtHolmes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxPtHolmes.BackColor = SystemColors.Control;
            numericalTextBoxPtHolmes.DecimalPlaces = 3;
            numericalTextBoxPtHolmes.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxPtHolmes.FooterBackColor = SystemColors.Control;
            numericalTextBoxPtHolmes.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxPtHolmes.FooterText = "GPa";
            numericalTextBoxPtHolmes.HeaderBackColor = SystemColors.Control;
            numericalTextBoxPtHolmes.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxPtHolmes.Location = new Point(84, 72);
            numericalTextBoxPtHolmes.Margin = new Padding(0);
            numericalTextBoxPtHolmes.MaximumSize = new Size(1000, 30);
            numericalTextBoxPtHolmes.MinimumSize = new Size(1, 20);
            numericalTextBoxPtHolmes.Name = "numericalTextBoxPtHolmes";
            numericalTextBoxPtHolmes.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxPtHolmes.Size = new Size(123, 27);
            numericalTextBoxPtHolmes.SkipEventDuringInput = false;
            numericalTextBoxPtHolmes.SmartIncrement = true;
            numericalTextBoxPtHolmes.TabIndex = 12;
            numericalTextBoxPtHolmes.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxPtHolmes.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxPtHolmes.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxPtHolmes.ThonsandsSeparator = true;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI Symbol", 9F);
            label16.ImeMode = ImeMode.NoControl;
            label16.Location = new Point(16, 128);
            label16.Name = "label16";
            label16.Size = new Size(64, 15);
            label16.TabIndex = 7;
            label16.Text = "Yokoo (09)";
            // 
            // numericalTextBoxPtJamieson
            // 
            numericalTextBoxPtJamieson.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxPtJamieson.BackColor = SystemColors.Control;
            numericalTextBoxPtJamieson.DecimalPlaces = 3;
            numericalTextBoxPtJamieson.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxPtJamieson.FooterBackColor = SystemColors.Control;
            numericalTextBoxPtJamieson.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxPtJamieson.FooterText = "GPa";
            numericalTextBoxPtJamieson.HeaderBackColor = SystemColors.Control;
            numericalTextBoxPtJamieson.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxPtJamieson.Location = new Point(84, 47);
            numericalTextBoxPtJamieson.Margin = new Padding(0);
            numericalTextBoxPtJamieson.MaximumSize = new Size(1000, 30);
            numericalTextBoxPtJamieson.MinimumSize = new Size(1, 20);
            numericalTextBoxPtJamieson.Name = "numericalTextBoxPtJamieson";
            numericalTextBoxPtJamieson.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxPtJamieson.Size = new Size(123, 27);
            numericalTextBoxPtJamieson.SkipEventDuringInput = false;
            numericalTextBoxPtJamieson.SmartIncrement = true;
            numericalTextBoxPtJamieson.TabIndex = 12;
            numericalTextBoxPtJamieson.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxPtJamieson.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxPtJamieson.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxPtJamieson.ThonsandsSeparator = true;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("Segoe UI Symbol", 9F);
            label23.ImeMode = ImeMode.NoControl;
            label23.Location = new Point(15, 102);
            label23.Name = "label23";
            label23.Size = new Size(66, 15);
            label23.TabIndex = 7;
            label23.Text = "Matsui (09)";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("Segoe UI Symbol", 9F);
            label26.ImeMode = ImeMode.NoControl;
            label26.Location = new Point(11, 77);
            label26.Name = "label26";
            label26.Size = new Size(71, 15);
            label26.TabIndex = 7;
            label26.Text = "Holmes (89)";
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Font = new Font("Segoe UI Symbol", 9F);
            label36.ImeMode = ImeMode.NoControl;
            label36.Location = new Point(4, 52);
            label36.Name = "label36";
            label36.Size = new Size(79, 15);
            label36.TabIndex = 7;
            label36.Text = "Jamieson (82)";
            // 
            // numericalTextBoxPtA
            // 
            numericalTextBoxPtA.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxPtA.BackColor = SystemColors.Control;
            numericalTextBoxPtA.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxPtA.FooterBackColor = SystemColors.Control;
            numericalTextBoxPtA.FooterText = "Å";
            numericalTextBoxPtA.HeaderBackColor = SystemColors.Control;
            numericalTextBoxPtA.HeaderText = "a";
            numericalTextBoxPtA.Location = new Point(111, 19);
            numericalTextBoxPtA.Margin = new Padding(0);
            numericalTextBoxPtA.MaximumSize = new Size(1000, 30);
            numericalTextBoxPtA.MinimumSize = new Size(1, 20);
            numericalTextBoxPtA.Name = "numericalTextBoxPtA";
            numericalTextBoxPtA.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxPtA.RadianValue = 0.068471011884989538D;
            numericalTextBoxPtA.Size = new Size(104, 27);
            numericalTextBoxPtA.SkipEventDuringInput = false;
            numericalTextBoxPtA.SmartIncrement = true;
            numericalTextBoxPtA.TabIndex = 10;
            numericalTextBoxPtA.TextFont = new Font("メイリオ", 9F);
            numericalTextBoxPtA.ThonsandsSeparator = true;
            numericalTextBoxPtA.Value = 3.9231D;
            numericalTextBoxPtA.ValueChanged += textBox_TextChanged;
            // 
            // numericBoxPtA0
            // 
            numericBoxPtA0.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxPtA0.BackColor = SystemColors.Control;
            numericBoxPtA0.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxPtA0.FooterBackColor = SystemColors.Control;
            numericBoxPtA0.FooterText = "Å";
            numericBoxPtA0.HeaderBackColor = SystemColors.Control;
            numericBoxPtA0.HeaderText = "a₀";
            numericBoxPtA0.Location = new Point(1, 20);
            numericBoxPtA0.Margin = new Padding(0);
            numericBoxPtA0.MaximumSize = new Size(1000, 30);
            numericBoxPtA0.MinimumSize = new Size(1, 20);
            numericBoxPtA0.Name = "numericBoxPtA0";
            numericBoxPtA0.Padding = new Padding(0, 0, 1, 0);
            numericBoxPtA0.RadianValue = 0.068471011884989538D;
            numericBoxPtA0.Size = new Size(110, 27);
            numericBoxPtA0.SkipEventDuringInput = false;
            numericBoxPtA0.SmartIncrement = true;
            numericBoxPtA0.TabIndex = 10;
            numericBoxPtA0.TextFont = new Font("メイリオ", 9F);
            numericBoxPtA0.ThonsandsSeparator = true;
            numericBoxPtA0.Value = 3.9231D;
            numericBoxPtA0.ValueChanged += textBox_TextChanged;
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.Font = new Font("Segoe UI Symbol", 7F);
            label37.ImeMode = ImeMode.NoControl;
            label37.Location = new Point(3, 155);
            label37.Name = "label37";
            label37.Size = new Size(81, 12);
            label37.TabIndex = 7;
            label37.Text = "Fratanduono (21)";
            // 
            // groupBoxNaClB1
            // 
            groupBoxNaClB1.Controls.Add(numericalTextBoxNaClB1Matsui);
            groupBoxNaClB1.Controls.Add(numericalTextBoxNaClB1Brown);
            groupBoxNaClB1.Controls.Add(label38);
            groupBoxNaClB1.Controls.Add(label39);
            groupBoxNaClB1.Controls.Add(numericalTextBoxNaClB1A);
            groupBoxNaClB1.Controls.Add(numericBoxNaClB1A0);
            groupBoxNaClB1.Font = new Font("Segoe UI Symbol", 9.75F);
            groupBoxNaClB1.Location = new Point(451, 3);
            groupBoxNaClB1.Name = "groupBoxNaClB1";
            groupBoxNaClB1.Size = new Size(218, 104);
            groupBoxNaClB1.TabIndex = 11;
            groupBoxNaClB1.TabStop = false;
            groupBoxNaClB1.Text = "NaCl B1";
            // 
            // numericalTextBoxNaClB1Matsui
            // 
            numericalTextBoxNaClB1Matsui.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxNaClB1Matsui.BackColor = SystemColors.Control;
            numericalTextBoxNaClB1Matsui.DecimalPlaces = 3;
            numericalTextBoxNaClB1Matsui.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB1Matsui.FooterBackColor = SystemColors.Control;
            numericalTextBoxNaClB1Matsui.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB1Matsui.FooterText = "GPa";
            numericalTextBoxNaClB1Matsui.HeaderBackColor = SystemColors.Control;
            numericalTextBoxNaClB1Matsui.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB1Matsui.Location = new Point(84, 75);
            numericalTextBoxNaClB1Matsui.Margin = new Padding(0);
            numericalTextBoxNaClB1Matsui.MaximumSize = new Size(1000, 30);
            numericalTextBoxNaClB1Matsui.MinimumSize = new Size(1, 20);
            numericalTextBoxNaClB1Matsui.Name = "numericalTextBoxNaClB1Matsui";
            numericalTextBoxNaClB1Matsui.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxNaClB1Matsui.Size = new Size(123, 27);
            numericalTextBoxNaClB1Matsui.SkipEventDuringInput = false;
            numericalTextBoxNaClB1Matsui.SmartIncrement = true;
            numericalTextBoxNaClB1Matsui.TabIndex = 12;
            numericalTextBoxNaClB1Matsui.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxNaClB1Matsui.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxNaClB1Matsui.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxNaClB1Matsui.ThonsandsSeparator = true;
            // 
            // numericalTextBoxNaClB1Brown
            // 
            numericalTextBoxNaClB1Brown.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxNaClB1Brown.BackColor = SystemColors.Control;
            numericalTextBoxNaClB1Brown.DecimalPlaces = 3;
            numericalTextBoxNaClB1Brown.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB1Brown.FooterBackColor = SystemColors.Control;
            numericalTextBoxNaClB1Brown.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB1Brown.FooterText = "GPa";
            numericalTextBoxNaClB1Brown.HeaderBackColor = SystemColors.Control;
            numericalTextBoxNaClB1Brown.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB1Brown.Location = new Point(84, 50);
            numericalTextBoxNaClB1Brown.Margin = new Padding(0);
            numericalTextBoxNaClB1Brown.MaximumSize = new Size(1000, 30);
            numericalTextBoxNaClB1Brown.MinimumSize = new Size(1, 20);
            numericalTextBoxNaClB1Brown.Name = "numericalTextBoxNaClB1Brown";
            numericalTextBoxNaClB1Brown.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxNaClB1Brown.Size = new Size(123, 27);
            numericalTextBoxNaClB1Brown.SkipEventDuringInput = false;
            numericalTextBoxNaClB1Brown.SmartIncrement = true;
            numericalTextBoxNaClB1Brown.TabIndex = 12;
            numericalTextBoxNaClB1Brown.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxNaClB1Brown.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxNaClB1Brown.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxNaClB1Brown.ThonsandsSeparator = true;
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.Font = new Font("Segoe UI Symbol", 9F);
            label38.ImeMode = ImeMode.NoControl;
            label38.Location = new Point(10, 80);
            label38.Name = "label38";
            label38.Size = new Size(66, 15);
            label38.TabIndex = 7;
            label38.Text = "Matsui (12)";
            // 
            // label39
            // 
            label39.AutoSize = true;
            label39.Font = new Font("Segoe UI Symbol", 9F);
            label39.ImeMode = ImeMode.NoControl;
            label39.Location = new Point(12, 55);
            label39.Name = "label39";
            label39.Size = new Size(64, 15);
            label39.TabIndex = 7;
            label39.Text = "Brown (99)";
            // 
            // numericalTextBoxNaClB1A
            // 
            numericalTextBoxNaClB1A.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxNaClB1A.BackColor = SystemColors.Control;
            numericalTextBoxNaClB1A.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB1A.FooterBackColor = SystemColors.Control;
            numericalTextBoxNaClB1A.FooterText = "Å";
            numericalTextBoxNaClB1A.HeaderBackColor = SystemColors.Control;
            numericalTextBoxNaClB1A.HeaderText = "a";
            numericalTextBoxNaClB1A.Location = new Point(111, 19);
            numericalTextBoxNaClB1A.Margin = new Padding(0);
            numericalTextBoxNaClB1A.MaximumSize = new Size(1000, 30);
            numericalTextBoxNaClB1A.MinimumSize = new Size(1, 20);
            numericalTextBoxNaClB1A.Name = "numericalTextBoxNaClB1A";
            numericalTextBoxNaClB1A.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxNaClB1A.RadianValue = 0.098419116519960256D;
            numericalTextBoxNaClB1A.Size = new Size(99, 27);
            numericalTextBoxNaClB1A.SkipEventDuringInput = false;
            numericalTextBoxNaClB1A.SmartIncrement = true;
            numericalTextBoxNaClB1A.TabIndex = 10;
            numericalTextBoxNaClB1A.TextFont = new Font("メイリオ", 9F);
            numericalTextBoxNaClB1A.ThonsandsSeparator = true;
            numericalTextBoxNaClB1A.Value = 5.639D;
            numericalTextBoxNaClB1A.ValueChanged += textBox_TextChanged;
            // 
            // numericBoxNaClB1A0
            // 
            numericBoxNaClB1A0.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxNaClB1A0.BackColor = SystemColors.Control;
            numericBoxNaClB1A0.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxNaClB1A0.FooterBackColor = SystemColors.Control;
            numericBoxNaClB1A0.FooterText = "Å";
            numericBoxNaClB1A0.HeaderBackColor = SystemColors.Control;
            numericBoxNaClB1A0.HeaderText = "a₀";
            numericBoxNaClB1A0.Location = new Point(3, 19);
            numericBoxNaClB1A0.Margin = new Padding(0);
            numericBoxNaClB1A0.MaximumSize = new Size(1000, 30);
            numericBoxNaClB1A0.MinimumSize = new Size(1, 20);
            numericBoxNaClB1A0.Name = "numericBoxNaClB1A0";
            numericBoxNaClB1A0.Padding = new Padding(0, 0, 1, 0);
            numericBoxNaClB1A0.RadianValue = 0.098419116519960256D;
            numericBoxNaClB1A0.Size = new Size(108, 27);
            numericBoxNaClB1A0.SkipEventDuringInput = false;
            numericBoxNaClB1A0.SmartIncrement = true;
            numericBoxNaClB1A0.TabIndex = 10;
            numericBoxNaClB1A0.TextFont = new Font("メイリオ", 9F);
            numericBoxNaClB1A0.ThonsandsSeparator = true;
            numericBoxNaClB1A0.Value = 5.639D;
            numericBoxNaClB1A0.ValueChanged += textBox_TextChanged;
            // 
            // groupBoxNaClB2
            // 
            groupBoxNaClB2.Controls.Add(numericalTextBoxNaClB2SakaiVinet);
            groupBoxNaClB2.Controls.Add(numericalTextBoxNaClB2SakaiBM);
            groupBoxNaClB2.Controls.Add(numericalTextBoxNaClB2Ueda);
            groupBoxNaClB2.Controls.Add(label40);
            groupBoxNaClB2.Controls.Add(numericalTextBoxNaClB2SataMgO);
            groupBoxNaClB2.Controls.Add(label41);
            groupBoxNaClB2.Controls.Add(numericalTextBoxNaClB2SataPt);
            groupBoxNaClB2.Controls.Add(label47);
            groupBoxNaClB2.Controls.Add(label48);
            groupBoxNaClB2.Controls.Add(numericalTextBoxNaClB2A);
            groupBoxNaClB2.Controls.Add(label50);
            groupBoxNaClB2.Controls.Add(numericalTextBoxNaClB2A0);
            groupBoxNaClB2.Font = new Font("Segoe UI Symbol", 9.75F);
            groupBoxNaClB2.Location = new Point(3, 217);
            groupBoxNaClB2.Name = "groupBoxNaClB2";
            groupBoxNaClB2.Size = new Size(218, 181);
            groupBoxNaClB2.TabIndex = 12;
            groupBoxNaClB2.TabStop = false;
            groupBoxNaClB2.Text = "NaCl B2";
            // 
            // numericalTextBoxNaClB2SakaiVinet
            // 
            numericalTextBoxNaClB2SakaiVinet.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxNaClB2SakaiVinet.BackColor = SystemColors.Control;
            numericalTextBoxNaClB2SakaiVinet.DecimalPlaces = 3;
            numericalTextBoxNaClB2SakaiVinet.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2SakaiVinet.FooterBackColor = SystemColors.Control;
            numericalTextBoxNaClB2SakaiVinet.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2SakaiVinet.FooterText = "GPa";
            numericalTextBoxNaClB2SakaiVinet.HeaderBackColor = SystemColors.Control;
            numericalTextBoxNaClB2SakaiVinet.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2SakaiVinet.Location = new Point(95, 148);
            numericalTextBoxNaClB2SakaiVinet.Margin = new Padding(0);
            numericalTextBoxNaClB2SakaiVinet.MaximumSize = new Size(1000, 30);
            numericalTextBoxNaClB2SakaiVinet.MinimumSize = new Size(1, 20);
            numericalTextBoxNaClB2SakaiVinet.Name = "numericalTextBoxNaClB2SakaiVinet";
            numericalTextBoxNaClB2SakaiVinet.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxNaClB2SakaiVinet.Size = new Size(115, 27);
            numericalTextBoxNaClB2SakaiVinet.SkipEventDuringInput = false;
            numericalTextBoxNaClB2SakaiVinet.SmartIncrement = true;
            numericalTextBoxNaClB2SakaiVinet.TabIndex = 12;
            numericalTextBoxNaClB2SakaiVinet.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxNaClB2SakaiVinet.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxNaClB2SakaiVinet.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxNaClB2SakaiVinet.ThonsandsSeparator = true;
            // 
            // numericalTextBoxNaClB2SakaiBM
            // 
            numericalTextBoxNaClB2SakaiBM.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxNaClB2SakaiBM.BackColor = SystemColors.Control;
            numericalTextBoxNaClB2SakaiBM.DecimalPlaces = 3;
            numericalTextBoxNaClB2SakaiBM.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2SakaiBM.FooterBackColor = SystemColors.Control;
            numericalTextBoxNaClB2SakaiBM.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2SakaiBM.FooterText = "GPa";
            numericalTextBoxNaClB2SakaiBM.HeaderBackColor = SystemColors.Control;
            numericalTextBoxNaClB2SakaiBM.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2SakaiBM.Location = new Point(95, 121);
            numericalTextBoxNaClB2SakaiBM.Margin = new Padding(0);
            numericalTextBoxNaClB2SakaiBM.MaximumSize = new Size(1000, 30);
            numericalTextBoxNaClB2SakaiBM.MinimumSize = new Size(1, 20);
            numericalTextBoxNaClB2SakaiBM.Name = "numericalTextBoxNaClB2SakaiBM";
            numericalTextBoxNaClB2SakaiBM.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxNaClB2SakaiBM.Size = new Size(115, 27);
            numericalTextBoxNaClB2SakaiBM.SkipEventDuringInput = false;
            numericalTextBoxNaClB2SakaiBM.SmartIncrement = true;
            numericalTextBoxNaClB2SakaiBM.TabIndex = 12;
            numericalTextBoxNaClB2SakaiBM.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxNaClB2SakaiBM.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxNaClB2SakaiBM.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxNaClB2SakaiBM.ThonsandsSeparator = true;
            // 
            // numericalTextBoxNaClB2Ueda
            // 
            numericalTextBoxNaClB2Ueda.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxNaClB2Ueda.BackColor = SystemColors.Control;
            numericalTextBoxNaClB2Ueda.DecimalPlaces = 3;
            numericalTextBoxNaClB2Ueda.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2Ueda.FooterBackColor = SystemColors.Control;
            numericalTextBoxNaClB2Ueda.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2Ueda.FooterText = "GPa";
            numericalTextBoxNaClB2Ueda.HeaderBackColor = SystemColors.Control;
            numericalTextBoxNaClB2Ueda.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2Ueda.Location = new Point(95, 96);
            numericalTextBoxNaClB2Ueda.Margin = new Padding(0);
            numericalTextBoxNaClB2Ueda.MaximumSize = new Size(1000, 30);
            numericalTextBoxNaClB2Ueda.MinimumSize = new Size(1, 20);
            numericalTextBoxNaClB2Ueda.Name = "numericalTextBoxNaClB2Ueda";
            numericalTextBoxNaClB2Ueda.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxNaClB2Ueda.Size = new Size(115, 27);
            numericalTextBoxNaClB2Ueda.SkipEventDuringInput = false;
            numericalTextBoxNaClB2Ueda.SmartIncrement = true;
            numericalTextBoxNaClB2Ueda.TabIndex = 12;
            numericalTextBoxNaClB2Ueda.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxNaClB2Ueda.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxNaClB2Ueda.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxNaClB2Ueda.ThonsandsSeparator = true;
            // 
            // label40
            // 
            label40.AutoSize = true;
            label40.Font = new Font("Segoe UI Symbol", 9F);
            label40.ImeMode = ImeMode.NoControl;
            label40.Location = new Point(2, 153);
            label40.Name = "label40";
            label40.Size = new Size(92, 15);
            label40.TabIndex = 7;
            label40.Text = "Sakai+(11) Vinet";
            // 
            // numericalTextBoxNaClB2SataMgO
            // 
            numericalTextBoxNaClB2SataMgO.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxNaClB2SataMgO.BackColor = SystemColors.Control;
            numericalTextBoxNaClB2SataMgO.DecimalPlaces = 3;
            numericalTextBoxNaClB2SataMgO.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2SataMgO.FooterBackColor = SystemColors.Control;
            numericalTextBoxNaClB2SataMgO.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2SataMgO.FooterText = "GPa";
            numericalTextBoxNaClB2SataMgO.HeaderBackColor = SystemColors.Control;
            numericalTextBoxNaClB2SataMgO.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2SataMgO.Location = new Point(95, 70);
            numericalTextBoxNaClB2SataMgO.Margin = new Padding(0);
            numericalTextBoxNaClB2SataMgO.MaximumSize = new Size(1000, 30);
            numericalTextBoxNaClB2SataMgO.MinimumSize = new Size(1, 20);
            numericalTextBoxNaClB2SataMgO.Name = "numericalTextBoxNaClB2SataMgO";
            numericalTextBoxNaClB2SataMgO.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxNaClB2SataMgO.Size = new Size(115, 27);
            numericalTextBoxNaClB2SataMgO.SkipEventDuringInput = false;
            numericalTextBoxNaClB2SataMgO.SmartIncrement = true;
            numericalTextBoxNaClB2SataMgO.TabIndex = 12;
            numericalTextBoxNaClB2SataMgO.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxNaClB2SataMgO.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxNaClB2SataMgO.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxNaClB2SataMgO.ThonsandsSeparator = true;
            // 
            // label41
            // 
            label41.AutoSize = true;
            label41.Font = new Font("Segoe UI Symbol", 9F);
            label41.ImeMode = ImeMode.NoControl;
            label41.Location = new Point(7, 126);
            label41.Name = "label41";
            label41.Size = new Size(83, 15);
            label41.TabIndex = 7;
            label41.Text = "Sakai+(11) BM";
            // 
            // numericalTextBoxNaClB2SataPt
            // 
            numericalTextBoxNaClB2SataPt.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxNaClB2SataPt.BackColor = SystemColors.Control;
            numericalTextBoxNaClB2SataPt.DecimalPlaces = 3;
            numericalTextBoxNaClB2SataPt.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2SataPt.FooterBackColor = SystemColors.Control;
            numericalTextBoxNaClB2SataPt.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2SataPt.FooterText = "GPa";
            numericalTextBoxNaClB2SataPt.HeaderBackColor = SystemColors.Control;
            numericalTextBoxNaClB2SataPt.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2SataPt.Location = new Point(95, 44);
            numericalTextBoxNaClB2SataPt.Margin = new Padding(0);
            numericalTextBoxNaClB2SataPt.MaximumSize = new Size(1000, 30);
            numericalTextBoxNaClB2SataPt.MinimumSize = new Size(1, 20);
            numericalTextBoxNaClB2SataPt.Name = "numericalTextBoxNaClB2SataPt";
            numericalTextBoxNaClB2SataPt.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxNaClB2SataPt.Size = new Size(115, 27);
            numericalTextBoxNaClB2SataPt.SkipEventDuringInput = false;
            numericalTextBoxNaClB2SataPt.SmartIncrement = true;
            numericalTextBoxNaClB2SataPt.TabIndex = 12;
            numericalTextBoxNaClB2SataPt.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxNaClB2SataPt.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxNaClB2SataPt.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxNaClB2SataPt.ThonsandsSeparator = true;
            // 
            // label47
            // 
            label47.AutoSize = true;
            label47.Font = new Font("Segoe UI Symbol", 9F);
            label47.ImeMode = ImeMode.NoControl;
            label47.Location = new Point(27, 102);
            label47.Name = "label47";
            label47.Size = new Size(62, 15);
            label47.TabIndex = 7;
            label47.Text = "Ueda+(08)";
            // 
            // label48
            // 
            label48.AutoSize = true;
            label48.Font = new Font("Segoe UI Symbol", 9F);
            label48.ImeMode = ImeMode.NoControl;
            label48.Location = new Point(10, 75);
            label48.Name = "label48";
            label48.Size = new Size(81, 15);
            label48.TabIndex = 7;
            label48.Text = "Sata (02) (Mg)";
            // 
            // numericalTextBoxNaClB2A
            // 
            numericalTextBoxNaClB2A.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxNaClB2A.BackColor = SystemColors.Control;
            numericalTextBoxNaClB2A.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2A.FooterBackColor = SystemColors.Control;
            numericalTextBoxNaClB2A.FooterText = "Å";
            numericalTextBoxNaClB2A.HeaderBackColor = SystemColors.Control;
            numericalTextBoxNaClB2A.HeaderText = "a ";
            numericalTextBoxNaClB2A.Location = new Point(110, 15);
            numericalTextBoxNaClB2A.Margin = new Padding(0);
            numericalTextBoxNaClB2A.MaximumSize = new Size(1000, 30);
            numericalTextBoxNaClB2A.MinimumSize = new Size(1, 20);
            numericalTextBoxNaClB2A.Name = "numericalTextBoxNaClB2A";
            numericalTextBoxNaClB2A.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxNaClB2A.RadianValue = 0.051138147083433859D;
            numericalTextBoxNaClB2A.Size = new Size(98, 27);
            numericalTextBoxNaClB2A.SkipEventDuringInput = false;
            numericalTextBoxNaClB2A.SmartIncrement = true;
            numericalTextBoxNaClB2A.TabIndex = 10;
            numericalTextBoxNaClB2A.TextFont = new Font("メイリオ", 9F);
            numericalTextBoxNaClB2A.ThonsandsSeparator = true;
            numericalTextBoxNaClB2A.Value = 2.93D;
            numericalTextBoxNaClB2A.ValueChanged += textBox_TextChanged;
            // 
            // label50
            // 
            label50.AutoSize = true;
            label50.Font = new Font("Segoe UI Symbol", 9F);
            label50.ImeMode = ImeMode.NoControl;
            label50.Location = new Point(15, 49);
            label50.Name = "label50";
            label50.Size = new Size(74, 15);
            label50.TabIndex = 7;
            label50.Text = "Sata (02) (Pt)";
            // 
            // numericalTextBoxNaClB2A0
            // 
            numericalTextBoxNaClB2A0.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxNaClB2A0.BackColor = SystemColors.Control;
            numericalTextBoxNaClB2A0.Enabled = false;
            numericalTextBoxNaClB2A0.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxNaClB2A0.FooterBackColor = SystemColors.Control;
            numericalTextBoxNaClB2A0.FooterText = "Å";
            numericalTextBoxNaClB2A0.HeaderBackColor = SystemColors.Control;
            numericalTextBoxNaClB2A0.HeaderText = "a₀";
            numericalTextBoxNaClB2A0.Location = new Point(2, 15);
            numericalTextBoxNaClB2A0.Margin = new Padding(0);
            numericalTextBoxNaClB2A0.MaximumSize = new Size(1000, 30);
            numericalTextBoxNaClB2A0.MinimumSize = new Size(1, 20);
            numericalTextBoxNaClB2A0.Name = "numericalTextBoxNaClB2A0";
            numericalTextBoxNaClB2A0.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxNaClB2A0.ReadOnly = true;
            numericalTextBoxNaClB2A0.Size = new Size(98, 27);
            numericalTextBoxNaClB2A0.SkipEventDuringInput = false;
            numericalTextBoxNaClB2A0.SmartIncrement = true;
            numericalTextBoxNaClB2A0.TabIndex = 10;
            numericalTextBoxNaClB2A0.TextBoxBackColor = SystemColors.Control;
            numericalTextBoxNaClB2A0.TextFont = new Font("メイリオ", 9F);
            numericalTextBoxNaClB2A0.ThonsandsSeparator = true;
            // 
            // groupBoxPericlase
            // 
            groupBoxPericlase.Controls.Add(numericBoxMgOTangeBM);
            groupBoxPericlase.Controls.Add(numericBoxMgOTangeVinet);
            groupBoxPericlase.Controls.Add(numericalTextBoxMgOAizawa);
            groupBoxPericlase.Controls.Add(label51);
            groupBoxPericlase.Controls.Add(numericalTextBoxMgODewaele);
            groupBoxPericlase.Controls.Add(label52);
            groupBoxPericlase.Controls.Add(numericalTextBoxMgOJacson);
            groupBoxPericlase.Controls.Add(label53);
            groupBoxPericlase.Controls.Add(label54);
            groupBoxPericlase.Controls.Add(numericalTextBoxMgOA);
            groupBoxPericlase.Controls.Add(label55);
            groupBoxPericlase.Controls.Add(numericBoxMgOA0);
            groupBoxPericlase.Font = new Font("Segoe UI Symbol", 9.75F);
            groupBoxPericlase.Location = new Point(227, 217);
            groupBoxPericlase.Name = "groupBoxPericlase";
            groupBoxPericlase.Size = new Size(218, 181);
            groupBoxPericlase.TabIndex = 13;
            groupBoxPericlase.TabStop = false;
            groupBoxPericlase.Text = "Periclase";
            // 
            // numericBoxMgOTangeBM
            // 
            numericBoxMgOTangeBM.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxMgOTangeBM.BackColor = SystemColors.Control;
            numericBoxMgOTangeBM.DecimalPlaces = 3;
            numericBoxMgOTangeBM.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxMgOTangeBM.FooterBackColor = SystemColors.Control;
            numericBoxMgOTangeBM.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMgOTangeBM.FooterText = "GPa";
            numericBoxMgOTangeBM.HeaderBackColor = SystemColors.Control;
            numericBoxMgOTangeBM.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMgOTangeBM.Location = new Point(95, 153);
            numericBoxMgOTangeBM.Margin = new Padding(0);
            numericBoxMgOTangeBM.MaximumSize = new Size(1000, 30);
            numericBoxMgOTangeBM.MinimumSize = new Size(1, 20);
            numericBoxMgOTangeBM.Name = "numericBoxMgOTangeBM";
            numericBoxMgOTangeBM.Padding = new Padding(0, 0, 1, 0);
            numericBoxMgOTangeBM.Size = new Size(114, 27);
            numericBoxMgOTangeBM.SkipEventDuringInput = false;
            numericBoxMgOTangeBM.SmartIncrement = true;
            numericBoxMgOTangeBM.TabIndex = 12;
            numericBoxMgOTangeBM.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericBoxMgOTangeBM.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericBoxMgOTangeBM.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericBoxMgOTangeBM.ThonsandsSeparator = true;
            // 
            // numericBoxMgOTangeVinet
            // 
            numericBoxMgOTangeVinet.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxMgOTangeVinet.BackColor = SystemColors.Control;
            numericBoxMgOTangeVinet.DecimalPlaces = 3;
            numericBoxMgOTangeVinet.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxMgOTangeVinet.FooterBackColor = SystemColors.Control;
            numericBoxMgOTangeVinet.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMgOTangeVinet.FooterText = "GPa";
            numericBoxMgOTangeVinet.HeaderBackColor = SystemColors.Control;
            numericBoxMgOTangeVinet.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMgOTangeVinet.Location = new Point(94, 128);
            numericBoxMgOTangeVinet.Margin = new Padding(0);
            numericBoxMgOTangeVinet.MaximumSize = new Size(1000, 30);
            numericBoxMgOTangeVinet.MinimumSize = new Size(1, 20);
            numericBoxMgOTangeVinet.Name = "numericBoxMgOTangeVinet";
            numericBoxMgOTangeVinet.Padding = new Padding(0, 0, 1, 0);
            numericBoxMgOTangeVinet.Size = new Size(115, 27);
            numericBoxMgOTangeVinet.SkipEventDuringInput = false;
            numericBoxMgOTangeVinet.SmartIncrement = true;
            numericBoxMgOTangeVinet.TabIndex = 12;
            numericBoxMgOTangeVinet.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericBoxMgOTangeVinet.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericBoxMgOTangeVinet.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericBoxMgOTangeVinet.ThonsandsSeparator = true;
            // 
            // numericalTextBoxMgOAizawa
            // 
            numericalTextBoxMgOAizawa.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxMgOAizawa.BackColor = SystemColors.Control;
            numericalTextBoxMgOAizawa.DecimalPlaces = 3;
            numericalTextBoxMgOAizawa.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxMgOAizawa.FooterBackColor = SystemColors.Control;
            numericalTextBoxMgOAizawa.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxMgOAizawa.FooterText = "GPa";
            numericalTextBoxMgOAizawa.HeaderBackColor = SystemColors.Control;
            numericalTextBoxMgOAizawa.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxMgOAizawa.Location = new Point(95, 101);
            numericalTextBoxMgOAizawa.Margin = new Padding(0);
            numericalTextBoxMgOAizawa.MaximumSize = new Size(1000, 30);
            numericalTextBoxMgOAizawa.MinimumSize = new Size(1, 20);
            numericalTextBoxMgOAizawa.Name = "numericalTextBoxMgOAizawa";
            numericalTextBoxMgOAizawa.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxMgOAizawa.Size = new Size(115, 27);
            numericalTextBoxMgOAizawa.SkipEventDuringInput = false;
            numericalTextBoxMgOAizawa.SmartIncrement = true;
            numericalTextBoxMgOAizawa.TabIndex = 12;
            numericalTextBoxMgOAizawa.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxMgOAizawa.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxMgOAizawa.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxMgOAizawa.ThonsandsSeparator = true;
            // 
            // label51
            // 
            label51.AutoSize = true;
            label51.Font = new Font("Segoe UI Symbol", 9F);
            label51.ImeMode = ImeMode.NoControl;
            label51.Location = new Point(2, 157);
            label51.Name = "label51";
            label51.Size = new Size(84, 15);
            label51.TabIndex = 7;
            label51.Text = "Tange (09) BM";
            // 
            // numericalTextBoxMgODewaele
            // 
            numericalTextBoxMgODewaele.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxMgODewaele.BackColor = SystemColors.Control;
            numericalTextBoxMgODewaele.DecimalPlaces = 3;
            numericalTextBoxMgODewaele.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxMgODewaele.FooterBackColor = SystemColors.Control;
            numericalTextBoxMgODewaele.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxMgODewaele.FooterText = "GPa";
            numericalTextBoxMgODewaele.HeaderBackColor = SystemColors.Control;
            numericalTextBoxMgODewaele.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxMgODewaele.Location = new Point(95, 75);
            numericalTextBoxMgODewaele.Margin = new Padding(0);
            numericalTextBoxMgODewaele.MaximumSize = new Size(1000, 30);
            numericalTextBoxMgODewaele.MinimumSize = new Size(1, 20);
            numericalTextBoxMgODewaele.Name = "numericalTextBoxMgODewaele";
            numericalTextBoxMgODewaele.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxMgODewaele.Size = new Size(115, 27);
            numericalTextBoxMgODewaele.SkipEventDuringInput = false;
            numericalTextBoxMgODewaele.SmartIncrement = true;
            numericalTextBoxMgODewaele.TabIndex = 12;
            numericalTextBoxMgODewaele.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxMgODewaele.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxMgODewaele.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxMgODewaele.ThonsandsSeparator = true;
            // 
            // label52
            // 
            label52.AutoSize = true;
            label52.Font = new Font("Segoe UI Symbol", 9F);
            label52.ImeMode = ImeMode.NoControl;
            label52.Location = new Point(1, 132);
            label52.Name = "label52";
            label52.Size = new Size(93, 15);
            label52.TabIndex = 7;
            label52.Text = "Tange (09) Vinet";
            // 
            // numericalTextBoxMgOJacson
            // 
            numericalTextBoxMgOJacson.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxMgOJacson.BackColor = SystemColors.Control;
            numericalTextBoxMgOJacson.DecimalPlaces = 3;
            numericalTextBoxMgOJacson.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxMgOJacson.FooterBackColor = SystemColors.Control;
            numericalTextBoxMgOJacson.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxMgOJacson.FooterText = "GPa";
            numericalTextBoxMgOJacson.HeaderBackColor = SystemColors.Control;
            numericalTextBoxMgOJacson.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxMgOJacson.Location = new Point(95, 49);
            numericalTextBoxMgOJacson.Margin = new Padding(0);
            numericalTextBoxMgOJacson.MaximumSize = new Size(1000, 30);
            numericalTextBoxMgOJacson.MinimumSize = new Size(1, 20);
            numericalTextBoxMgOJacson.Name = "numericalTextBoxMgOJacson";
            numericalTextBoxMgOJacson.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxMgOJacson.Size = new Size(115, 27);
            numericalTextBoxMgOJacson.SkipEventDuringInput = false;
            numericalTextBoxMgOJacson.SmartIncrement = true;
            numericalTextBoxMgOJacson.TabIndex = 12;
            numericalTextBoxMgOJacson.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxMgOJacson.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxMgOJacson.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxMgOJacson.ThonsandsSeparator = true;
            // 
            // label53
            // 
            label53.AutoSize = true;
            label53.Font = new Font("Segoe UI Symbol", 9F);
            label53.ImeMode = ImeMode.NoControl;
            label53.Location = new Point(19, 106);
            label53.Name = "label53";
            label53.Size = new Size(67, 15);
            label53.TabIndex = 7;
            label53.Text = "Aizawa (06)";
            // 
            // label54
            // 
            label54.AutoSize = true;
            label54.Font = new Font("Segoe UI Symbol", 9F);
            label54.ImeMode = ImeMode.NoControl;
            label54.Location = new Point(12, 80);
            label54.Name = "label54";
            label54.Size = new Size(74, 15);
            label54.TabIndex = 7;
            label54.Text = "Dewaele (00)";
            // 
            // numericalTextBoxMgOA
            // 
            numericalTextBoxMgOA.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxMgOA.BackColor = SystemColors.Control;
            numericalTextBoxMgOA.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxMgOA.FooterBackColor = SystemColors.Control;
            numericalTextBoxMgOA.FooterText = "Å";
            numericalTextBoxMgOA.HeaderBackColor = SystemColors.Control;
            numericalTextBoxMgOA.HeaderText = "a";
            numericalTextBoxMgOA.Location = new Point(110, 20);
            numericalTextBoxMgOA.Margin = new Padding(0);
            numericalTextBoxMgOA.MaximumSize = new Size(1000, 30);
            numericalTextBoxMgOA.MinimumSize = new Size(1, 20);
            numericalTextBoxMgOA.Name = "numericalTextBoxMgOA";
            numericalTextBoxMgOA.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxMgOA.RadianValue = 0.0734993054599852D;
            numericalTextBoxMgOA.Size = new Size(98, 27);
            numericalTextBoxMgOA.SkipEventDuringInput = false;
            numericalTextBoxMgOA.SmartIncrement = true;
            numericalTextBoxMgOA.TabIndex = 10;
            numericalTextBoxMgOA.TextFont = new Font("メイリオ", 9F);
            numericalTextBoxMgOA.ThonsandsSeparator = true;
            numericalTextBoxMgOA.Value = 4.2112D;
            numericalTextBoxMgOA.ValueChanged += textBox_TextChanged;
            // 
            // label55
            // 
            label55.AutoSize = true;
            label55.Font = new Font("Segoe UI Symbol", 9F);
            label55.ImeMode = ImeMode.NoControl;
            label55.Location = new Point(15, 55);
            label55.Name = "label55";
            label55.Size = new Size(71, 15);
            label55.TabIndex = 7;
            label55.Text = "Jackson (98)";
            // 
            // numericBoxMgOA0
            // 
            numericBoxMgOA0.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxMgOA0.BackColor = SystemColors.Control;
            numericBoxMgOA0.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxMgOA0.FooterBackColor = SystemColors.Control;
            numericBoxMgOA0.FooterText = "Å";
            numericBoxMgOA0.HeaderBackColor = SystemColors.Control;
            numericBoxMgOA0.HeaderText = "a₀";
            numericBoxMgOA0.Location = new Point(2, 20);
            numericBoxMgOA0.Margin = new Padding(0);
            numericBoxMgOA0.MaximumSize = new Size(1000, 30);
            numericBoxMgOA0.MinimumSize = new Size(1, 20);
            numericBoxMgOA0.Name = "numericBoxMgOA0";
            numericBoxMgOA0.Padding = new Padding(0, 0, 1, 0);
            numericBoxMgOA0.RadianValue = 0.0734993054599852D;
            numericBoxMgOA0.Size = new Size(98, 27);
            numericBoxMgOA0.SkipEventDuringInput = false;
            numericBoxMgOA0.SmartIncrement = true;
            numericBoxMgOA0.TabIndex = 10;
            numericBoxMgOA0.TextFont = new Font("メイリオ", 9F);
            numericBoxMgOA0.ThonsandsSeparator = true;
            numericBoxMgOA0.Value = 4.2112D;
            numericBoxMgOA0.ValueChanged += textBox_TextChanged;
            // 
            // groupBoxCorundum
            // 
            groupBoxCorundum.Controls.Add(numericBoxCorundumDubrovinsky);
            groupBoxCorundum.Controls.Add(numericalTextBoxColundumV);
            groupBoxCorundum.Controls.Add(label56);
            groupBoxCorundum.Controls.Add(numericBoxColundumV0);
            groupBoxCorundum.Font = new Font("Segoe UI Symbol", 9.75F);
            groupBoxCorundum.Location = new Point(451, 217);
            groupBoxCorundum.Name = "groupBoxCorundum";
            groupBoxCorundum.Size = new Size(218, 80);
            groupBoxCorundum.TabIndex = 14;
            groupBoxCorundum.TabStop = false;
            groupBoxCorundum.Text = "Corundum";
            // 
            // numericBoxCorundumDubrovinsky
            // 
            numericBoxCorundumDubrovinsky.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxCorundumDubrovinsky.BackColor = SystemColors.Control;
            numericBoxCorundumDubrovinsky.DecimalPlaces = 3;
            numericBoxCorundumDubrovinsky.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxCorundumDubrovinsky.FooterBackColor = SystemColors.Control;
            numericBoxCorundumDubrovinsky.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxCorundumDubrovinsky.FooterText = "GPa";
            numericBoxCorundumDubrovinsky.HeaderBackColor = SystemColors.Control;
            numericBoxCorundumDubrovinsky.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxCorundumDubrovinsky.Location = new Point(103, 50);
            numericBoxCorundumDubrovinsky.Margin = new Padding(0);
            numericBoxCorundumDubrovinsky.MaximumSize = new Size(1000, 30);
            numericBoxCorundumDubrovinsky.MinimumSize = new Size(1, 20);
            numericBoxCorundumDubrovinsky.Name = "numericBoxCorundumDubrovinsky";
            numericBoxCorundumDubrovinsky.Padding = new Padding(0, 0, 1, 0);
            numericBoxCorundumDubrovinsky.Size = new Size(104, 27);
            numericBoxCorundumDubrovinsky.SkipEventDuringInput = false;
            numericBoxCorundumDubrovinsky.SmartIncrement = true;
            numericBoxCorundumDubrovinsky.TabIndex = 12;
            numericBoxCorundumDubrovinsky.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericBoxCorundumDubrovinsky.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericBoxCorundumDubrovinsky.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericBoxCorundumDubrovinsky.ThonsandsSeparator = true;
            // 
            // numericalTextBoxColundumV
            // 
            numericalTextBoxColundumV.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxColundumV.BackColor = SystemColors.Control;
            numericalTextBoxColundumV.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxColundumV.FooterBackColor = SystemColors.Control;
            numericalTextBoxColundumV.FooterText = "Å³";
            numericalTextBoxColundumV.HeaderBackColor = SystemColors.Control;
            numericalTextBoxColundumV.HeaderText = "V";
            numericalTextBoxColundumV.Location = new Point(108, 21);
            numericalTextBoxColundumV.Margin = new Padding(0);
            numericalTextBoxColundumV.MaximumSize = new Size(1000, 30);
            numericalTextBoxColundumV.MinimumSize = new Size(1, 20);
            numericalTextBoxColundumV.Name = "numericalTextBoxColundumV";
            numericalTextBoxColundumV.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxColundumV.RadianValue = 4.4662054024689839D;
            numericalTextBoxColundumV.Size = new Size(98, 27);
            numericalTextBoxColundumV.SkipEventDuringInput = false;
            numericalTextBoxColundumV.SmartIncrement = true;
            numericalTextBoxColundumV.TabIndex = 10;
            numericalTextBoxColundumV.TextFont = new Font("メイリオ", 9F);
            numericalTextBoxColundumV.ThonsandsSeparator = true;
            numericalTextBoxColundumV.Value = 255.89472D;
            numericalTextBoxColundumV.ValueChanged += textBox_TextChanged;
            // 
            // label56
            // 
            label56.AutoSize = true;
            label56.Font = new Font("Segoe UI Symbol", 9F);
            label56.ImeMode = ImeMode.NoControl;
            label56.Location = new Point(4, 55);
            label56.Name = "label56";
            label56.Size = new Size(96, 15);
            label56.TabIndex = 7;
            label56.Text = "Dubrovinsky (98)";
            // 
            // numericBoxColundumV0
            // 
            numericBoxColundumV0.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxColundumV0.BackColor = SystemColors.Control;
            numericBoxColundumV0.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxColundumV0.FooterBackColor = SystemColors.Control;
            numericBoxColundumV0.FooterText = "Å³";
            numericBoxColundumV0.HeaderBackColor = SystemColors.Control;
            numericBoxColundumV0.HeaderText = "V₀";
            numericBoxColundumV0.Location = new Point(2, 21);
            numericBoxColundumV0.Margin = new Padding(0);
            numericBoxColundumV0.MaximumSize = new Size(1000, 30);
            numericBoxColundumV0.MinimumSize = new Size(1, 20);
            numericBoxColundumV0.Name = "numericBoxColundumV0";
            numericBoxColundumV0.Padding = new Padding(0, 0, 1, 0);
            numericBoxColundumV0.RadianValue = 4.4662054024689839D;
            numericBoxColundumV0.Size = new Size(106, 27);
            numericBoxColundumV0.SkipEventDuringInput = false;
            numericBoxColundumV0.SmartIncrement = true;
            numericBoxColundumV0.TabIndex = 10;
            numericBoxColundumV0.TextFont = new Font("メイリオ", 9F);
            numericBoxColundumV0.ThonsandsSeparator = true;
            numericBoxColundumV0.Value = 255.89472D;
            numericBoxColundumV0.ValueChanged += textBox_TextChanged;
            // 
            // groupBoxAr
            // 
            groupBoxAr.Controls.Add(numericalTextBoxArRoss);
            groupBoxAr.Controls.Add(numericalTextBoxArJephcoat);
            groupBoxAr.Controls.Add(numericalTextBoxArA);
            groupBoxAr.Controls.Add(label57);
            groupBoxAr.Controls.Add(label58);
            groupBoxAr.Controls.Add(numericBoxArA0);
            groupBoxAr.Font = new Font("Segoe UI Symbol", 9.75F);
            groupBoxAr.Location = new Point(3, 404);
            groupBoxAr.Name = "groupBoxAr";
            groupBoxAr.Size = new Size(218, 100);
            groupBoxAr.TabIndex = 15;
            groupBoxAr.TabStop = false;
            groupBoxAr.Text = "Ar";
            // 
            // numericalTextBoxArRoss
            // 
            numericalTextBoxArRoss.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxArRoss.BackColor = SystemColors.Control;
            numericalTextBoxArRoss.DecimalPlaces = 3;
            numericalTextBoxArRoss.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxArRoss.FooterBackColor = SystemColors.Control;
            numericalTextBoxArRoss.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxArRoss.FooterText = "GPa";
            numericalTextBoxArRoss.HeaderBackColor = SystemColors.Control;
            numericalTextBoxArRoss.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxArRoss.Location = new Point(93, 44);
            numericalTextBoxArRoss.Margin = new Padding(0);
            numericalTextBoxArRoss.MaximumSize = new Size(1000, 30);
            numericalTextBoxArRoss.MinimumSize = new Size(1, 20);
            numericalTextBoxArRoss.Name = "numericalTextBoxArRoss";
            numericalTextBoxArRoss.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxArRoss.Size = new Size(111, 27);
            numericalTextBoxArRoss.SkipEventDuringInput = false;
            numericalTextBoxArRoss.SmartIncrement = true;
            numericalTextBoxArRoss.TabIndex = 12;
            numericalTextBoxArRoss.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxArRoss.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxArRoss.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxArRoss.ThonsandsSeparator = true;
            // 
            // numericalTextBoxArJephcoat
            // 
            numericalTextBoxArJephcoat.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxArJephcoat.BackColor = SystemColors.Control;
            numericalTextBoxArJephcoat.DecimalPlaces = 3;
            numericalTextBoxArJephcoat.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxArJephcoat.FooterBackColor = SystemColors.Control;
            numericalTextBoxArJephcoat.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxArJephcoat.FooterText = "GPa";
            numericalTextBoxArJephcoat.HeaderBackColor = SystemColors.Control;
            numericalTextBoxArJephcoat.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxArJephcoat.Location = new Point(93, 70);
            numericalTextBoxArJephcoat.Margin = new Padding(0);
            numericalTextBoxArJephcoat.MaximumSize = new Size(1000, 30);
            numericalTextBoxArJephcoat.MinimumSize = new Size(1, 20);
            numericalTextBoxArJephcoat.Name = "numericalTextBoxArJephcoat";
            numericalTextBoxArJephcoat.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxArJephcoat.Size = new Size(111, 27);
            numericalTextBoxArJephcoat.SkipEventDuringInput = false;
            numericalTextBoxArJephcoat.SmartIncrement = true;
            numericalTextBoxArJephcoat.TabIndex = 12;
            numericalTextBoxArJephcoat.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxArJephcoat.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxArJephcoat.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxArJephcoat.ThonsandsSeparator = true;
            // 
            // numericalTextBoxArA
            // 
            numericalTextBoxArA.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxArA.BackColor = SystemColors.Control;
            numericalTextBoxArA.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxArA.FooterBackColor = SystemColors.Control;
            numericalTextBoxArA.FooterText = "Å";
            numericalTextBoxArA.HeaderBackColor = SystemColors.Control;
            numericalTextBoxArA.HeaderText = "a";
            numericalTextBoxArA.Location = new Point(111, 15);
            numericalTextBoxArA.Margin = new Padding(0);
            numericalTextBoxArA.MaximumSize = new Size(1000, 30);
            numericalTextBoxArA.MinimumSize = new Size(1, 20);
            numericalTextBoxArA.Name = "numericalTextBoxArA";
            numericalTextBoxArA.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxArA.RadianValue = 0.071184998871840724D;
            numericalTextBoxArA.Size = new Size(93, 27);
            numericalTextBoxArA.SkipEventDuringInput = false;
            numericalTextBoxArA.SmartIncrement = true;
            numericalTextBoxArA.TabIndex = 10;
            numericalTextBoxArA.TextFont = new Font("メイリオ", 9F);
            numericalTextBoxArA.ThonsandsSeparator = true;
            numericalTextBoxArA.Value = 4.0786D;
            numericalTextBoxArA.ValueChanged += textBox_TextChanged;
            // 
            // label57
            // 
            label57.AutoSize = true;
            label57.Font = new Font("Segoe UI Symbol", 9F);
            label57.ImeMode = ImeMode.NoControl;
            label57.Location = new Point(13, 75);
            label57.Name = "label57";
            label57.Size = new Size(77, 15);
            label57.TabIndex = 7;
            label57.Text = "Jephcoat (98)";
            // 
            // label58
            // 
            label58.AutoSize = true;
            label58.Font = new Font("Segoe UI Symbol", 9F);
            label58.ImeMode = ImeMode.NoControl;
            label58.Location = new Point(8, 49);
            label58.Name = "label58";
            label58.Size = new Size(82, 15);
            label58.TabIndex = 7;
            label58.Text = "Ross et al. (86)";
            // 
            // numericBoxArA0
            // 
            numericBoxArA0.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxArA0.BackColor = SystemColors.Control;
            numericBoxArA0.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxArA0.FooterBackColor = SystemColors.Control;
            numericBoxArA0.FooterText = "Å";
            numericBoxArA0.HeaderBackColor = SystemColors.Control;
            numericBoxArA0.HeaderText = "a₀";
            numericBoxArA0.Location = new Point(3, 15);
            numericBoxArA0.Margin = new Padding(0);
            numericBoxArA0.MaximumSize = new Size(1000, 30);
            numericBoxArA0.MinimumSize = new Size(1, 20);
            numericBoxArA0.Name = "numericBoxArA0";
            numericBoxArA0.Padding = new Padding(0, 0, 1, 0);
            numericBoxArA0.Size = new Size(98, 27);
            numericBoxArA0.SkipEventDuringInput = false;
            numericBoxArA0.SmartIncrement = true;
            numericBoxArA0.TabIndex = 10;
            numericBoxArA0.TextFont = new Font("メイリオ", 9F);
            numericBoxArA0.ThonsandsSeparator = true;
            numericBoxArA0.ValueChanged += textBox_TextChanged;
            // 
            // groupBoxRe
            // 
            groupBoxRe.Controls.Add(numericBoxReDub);
            groupBoxRe.Controls.Add(numericBoxReSakai);
            groupBoxRe.Controls.Add(label59);
            groupBoxRe.Controls.Add(numericBoxReAnz);
            groupBoxRe.Controls.Add(label60);
            groupBoxRe.Controls.Add(numericalTextBoxReZha);
            groupBoxRe.Controls.Add(label61);
            groupBoxRe.Controls.Add(numericBoxReV);
            groupBoxRe.Controls.Add(label62);
            groupBoxRe.Controls.Add(numerictBoxReV0);
            groupBoxRe.Font = new Font("Segoe UI Symbol", 9.75F);
            groupBoxRe.Location = new Point(227, 404);
            groupBoxRe.Name = "groupBoxRe";
            groupBoxRe.Size = new Size(218, 157);
            groupBoxRe.TabIndex = 16;
            groupBoxRe.TabStop = false;
            groupBoxRe.Text = "Re";
            // 
            // numericBoxReDub
            // 
            numericBoxReDub.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxReDub.BackColor = SystemColors.Control;
            numericBoxReDub.DecimalPlaces = 3;
            numericBoxReDub.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxReDub.FooterBackColor = SystemColors.Control;
            numericBoxReDub.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxReDub.FooterText = "GPa";
            numericBoxReDub.HeaderBackColor = SystemColors.Control;
            numericBoxReDub.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxReDub.Location = new Point(96, 126);
            numericBoxReDub.Margin = new Padding(0);
            numericBoxReDub.MaximumSize = new Size(1000, 30);
            numericBoxReDub.MinimumSize = new Size(1, 20);
            numericBoxReDub.Name = "numericBoxReDub";
            numericBoxReDub.Padding = new Padding(0, 0, 1, 0);
            numericBoxReDub.Size = new Size(119, 27);
            numericBoxReDub.SkipEventDuringInput = false;
            numericBoxReDub.SmartIncrement = true;
            numericBoxReDub.TabIndex = 12;
            numericBoxReDub.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericBoxReDub.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericBoxReDub.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericBoxReDub.ThonsandsSeparator = true;
            // 
            // numericBoxReSakai
            // 
            numericBoxReSakai.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxReSakai.BackColor = SystemColors.Control;
            numericBoxReSakai.DecimalPlaces = 3;
            numericBoxReSakai.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxReSakai.FooterBackColor = SystemColors.Control;
            numericBoxReSakai.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxReSakai.FooterText = "GPa";
            numericBoxReSakai.HeaderBackColor = SystemColors.Control;
            numericBoxReSakai.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxReSakai.Location = new Point(96, 100);
            numericBoxReSakai.Margin = new Padding(0);
            numericBoxReSakai.MaximumSize = new Size(1000, 30);
            numericBoxReSakai.MinimumSize = new Size(1, 20);
            numericBoxReSakai.Name = "numericBoxReSakai";
            numericBoxReSakai.Padding = new Padding(0, 0, 1, 0);
            numericBoxReSakai.Size = new Size(119, 27);
            numericBoxReSakai.SkipEventDuringInput = false;
            numericBoxReSakai.SmartIncrement = true;
            numericBoxReSakai.TabIndex = 12;
            numericBoxReSakai.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericBoxReSakai.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericBoxReSakai.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericBoxReSakai.ThonsandsSeparator = true;
            // 
            // label59
            // 
            label59.AutoSize = true;
            label59.Font = new Font("Segoe UI Symbol", 9F);
            label59.ImeMode = ImeMode.NoControl;
            label59.Location = new Point(4, 131);
            label59.Name = "label59";
            label59.Size = new Size(57, 15);
            label59.TabIndex = 7;
            label59.Text = "Dub. (##)";
            // 
            // numericBoxReAnz
            // 
            numericBoxReAnz.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxReAnz.BackColor = SystemColors.Control;
            numericBoxReAnz.DecimalPlaces = 3;
            numericBoxReAnz.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxReAnz.FooterBackColor = SystemColors.Control;
            numericBoxReAnz.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxReAnz.FooterText = "GPa";
            numericBoxReAnz.HeaderBackColor = SystemColors.Control;
            numericBoxReAnz.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxReAnz.Location = new Point(96, 76);
            numericBoxReAnz.Margin = new Padding(0);
            numericBoxReAnz.MaximumSize = new Size(1000, 30);
            numericBoxReAnz.MinimumSize = new Size(1, 20);
            numericBoxReAnz.Name = "numericBoxReAnz";
            numericBoxReAnz.Padding = new Padding(0, 0, 1, 0);
            numericBoxReAnz.Size = new Size(119, 27);
            numericBoxReAnz.SkipEventDuringInput = false;
            numericBoxReAnz.SmartIncrement = true;
            numericBoxReAnz.TabIndex = 12;
            numericBoxReAnz.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericBoxReAnz.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericBoxReAnz.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericBoxReAnz.ThonsandsSeparator = true;
            // 
            // label60
            // 
            label60.AutoSize = true;
            label60.Font = new Font("Segoe UI Symbol", 9F);
            label60.ImeMode = ImeMode.NoControl;
            label60.Location = new Point(4, 105);
            label60.Name = "label60";
            label60.Size = new Size(62, 15);
            label60.TabIndex = 7;
            label60.Text = "Sakai. (##)";
            // 
            // numericalTextBoxReZha
            // 
            numericalTextBoxReZha.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericalTextBoxReZha.BackColor = SystemColors.Control;
            numericalTextBoxReZha.DecimalPlaces = 3;
            numericalTextBoxReZha.Font = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxReZha.FooterBackColor = SystemColors.Control;
            numericalTextBoxReZha.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxReZha.FooterText = "GPa";
            numericalTextBoxReZha.HeaderBackColor = SystemColors.Control;
            numericalTextBoxReZha.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericalTextBoxReZha.Location = new Point(96, 49);
            numericalTextBoxReZha.Margin = new Padding(0);
            numericalTextBoxReZha.MaximumSize = new Size(1000, 30);
            numericalTextBoxReZha.MinimumSize = new Size(1, 20);
            numericalTextBoxReZha.Name = "numericalTextBoxReZha";
            numericalTextBoxReZha.Padding = new Padding(0, 0, 1, 0);
            numericalTextBoxReZha.Size = new Size(119, 27);
            numericalTextBoxReZha.SkipEventDuringInput = false;
            numericalTextBoxReZha.SmartIncrement = true;
            numericalTextBoxReZha.TabIndex = 12;
            numericalTextBoxReZha.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericalTextBoxReZha.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericalTextBoxReZha.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericalTextBoxReZha.ThonsandsSeparator = true;
            // 
            // label61
            // 
            label61.AutoSize = true;
            label61.Font = new Font("Segoe UI Symbol", 9F);
            label61.ImeMode = ImeMode.NoControl;
            label61.Location = new Point(4, 81);
            label61.Name = "label61";
            label61.Size = new Size(52, 15);
            label61.TabIndex = 7;
            label61.Text = "Anz (##)";
            // 
            // numericBoxReV
            // 
            numericBoxReV.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxReV.BackColor = SystemColors.Control;
            numericBoxReV.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxReV.FooterBackColor = SystemColors.Control;
            numericBoxReV.FooterText = "Å³";
            numericBoxReV.HeaderBackColor = SystemColors.Control;
            numericBoxReV.HeaderText = "V";
            numericBoxReV.Location = new Point(110, 20);
            numericBoxReV.Margin = new Padding(0);
            numericBoxReV.MaximumSize = new Size(1000, 30);
            numericBoxReV.MinimumSize = new Size(1, 20);
            numericBoxReV.Name = "numericBoxReV";
            numericBoxReV.Padding = new Padding(0, 0, 1, 0);
            numericBoxReV.RadianValue = 0.51361461961226529D;
            numericBoxReV.Size = new Size(105, 27);
            numericBoxReV.SkipEventDuringInput = false;
            numericBoxReV.SmartIncrement = true;
            numericBoxReV.TabIndex = 10;
            numericBoxReV.TextFont = new Font("メイリオ", 9F);
            numericBoxReV.ThonsandsSeparator = true;
            numericBoxReV.Value = 29.42795D;
            numericBoxReV.ValueChanged += textBox_TextChanged;
            // 
            // label62
            // 
            label62.AutoSize = true;
            label62.Font = new Font("Segoe UI Symbol", 9F);
            label62.ImeMode = ImeMode.NoControl;
            label62.Location = new Point(4, 54);
            label62.Name = "label62";
            label62.Size = new Size(78, 15);
            label62.TabIndex = 7;
            label62.Text = "Zha et al. (04)";
            // 
            // numerictBoxReV0
            // 
            numerictBoxReV0.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numerictBoxReV0.BackColor = SystemColors.Control;
            numerictBoxReV0.Font = new Font("Segoe UI Symbol", 9F);
            numerictBoxReV0.FooterBackColor = SystemColors.Control;
            numerictBoxReV0.FooterText = "Å³";
            numerictBoxReV0.HeaderBackColor = SystemColors.Control;
            numerictBoxReV0.HeaderText = "V₀";
            numerictBoxReV0.Location = new Point(2, 20);
            numerictBoxReV0.Margin = new Padding(0);
            numerictBoxReV0.MaximumSize = new Size(1000, 30);
            numerictBoxReV0.MinimumSize = new Size(1, 20);
            numerictBoxReV0.Name = "numerictBoxReV0";
            numerictBoxReV0.Padding = new Padding(0, 0, 1, 0);
            numerictBoxReV0.RadianValue = 0.51361461961226529D;
            numerictBoxReV0.Size = new Size(108, 27);
            numerictBoxReV0.SkipEventDuringInput = false;
            numerictBoxReV0.SmartIncrement = true;
            numerictBoxReV0.TabIndex = 10;
            numerictBoxReV0.TextFont = new Font("メイリオ", 9F);
            numerictBoxReV0.ThonsandsSeparator = true;
            numerictBoxReV0.Value = 29.42795D;
            numerictBoxReV0.ValueChanged += textBox_TextChanged;
            // 
            // groupBoxMo
            // 
            groupBoxMo.Controls.Add(numericBoxMoZhao);
            groupBoxMo.Controls.Add(numericBoxMoHuang);
            groupBoxMo.Controls.Add(label63);
            groupBoxMo.Controls.Add(numericBoxMoV);
            groupBoxMo.Controls.Add(label64);
            groupBoxMo.Controls.Add(numericBoxMoV0);
            groupBoxMo.Font = new Font("Segoe UI Symbol", 9.75F);
            groupBoxMo.Location = new Point(451, 404);
            groupBoxMo.Name = "groupBoxMo";
            groupBoxMo.Size = new Size(218, 103);
            groupBoxMo.TabIndex = 17;
            groupBoxMo.TabStop = false;
            groupBoxMo.Text = "Mo";
            groupBoxMo.Visible = false;
            // 
            // numericBoxMoZhao
            // 
            numericBoxMoZhao.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxMoZhao.BackColor = SystemColors.Control;
            numericBoxMoZhao.DecimalPlaces = 3;
            numericBoxMoZhao.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxMoZhao.FooterBackColor = SystemColors.Control;
            numericBoxMoZhao.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMoZhao.FooterText = "GPa";
            numericBoxMoZhao.HeaderBackColor = SystemColors.Control;
            numericBoxMoZhao.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMoZhao.Location = new Point(105, 78);
            numericBoxMoZhao.Margin = new Padding(0);
            numericBoxMoZhao.MaximumSize = new Size(1000, 30);
            numericBoxMoZhao.MinimumSize = new Size(1, 20);
            numericBoxMoZhao.Name = "numericBoxMoZhao";
            numericBoxMoZhao.Padding = new Padding(0, 0, 1, 0);
            numericBoxMoZhao.Size = new Size(110, 27);
            numericBoxMoZhao.SkipEventDuringInput = false;
            numericBoxMoZhao.SmartIncrement = true;
            numericBoxMoZhao.TabIndex = 12;
            numericBoxMoZhao.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericBoxMoZhao.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericBoxMoZhao.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericBoxMoZhao.ThonsandsSeparator = true;
            // 
            // numericBoxMoHuang
            // 
            numericBoxMoHuang.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxMoHuang.BackColor = SystemColors.Control;
            numericBoxMoHuang.DecimalPlaces = 3;
            numericBoxMoHuang.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxMoHuang.FooterBackColor = SystemColors.Control;
            numericBoxMoHuang.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMoHuang.FooterText = "GPa";
            numericBoxMoHuang.HeaderBackColor = SystemColors.Control;
            numericBoxMoHuang.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxMoHuang.Location = new Point(105, 50);
            numericBoxMoHuang.Margin = new Padding(0);
            numericBoxMoHuang.MaximumSize = new Size(1000, 30);
            numericBoxMoHuang.MinimumSize = new Size(1, 20);
            numericBoxMoHuang.Name = "numericBoxMoHuang";
            numericBoxMoHuang.Padding = new Padding(0, 0, 1, 0);
            numericBoxMoHuang.Size = new Size(110, 27);
            numericBoxMoHuang.SkipEventDuringInput = false;
            numericBoxMoHuang.SmartIncrement = true;
            numericBoxMoHuang.TabIndex = 12;
            numericBoxMoHuang.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericBoxMoHuang.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericBoxMoHuang.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericBoxMoHuang.ThonsandsSeparator = true;
            // 
            // label63
            // 
            label63.AutoSize = true;
            label63.Font = new Font("Segoe UI Symbol", 9F);
            label63.ImeMode = ImeMode.NoControl;
            label63.Location = new Point(4, 82);
            label63.Name = "label63";
            label63.Size = new Size(62, 15);
            label63.TabIndex = 7;
            label63.Text = "Zhao+(00)";
            // 
            // numericBoxMoV
            // 
            numericBoxMoV.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxMoV.BackColor = SystemColors.Control;
            numericBoxMoV.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxMoV.FooterBackColor = SystemColors.Control;
            numericBoxMoV.FooterText = "Å³";
            numericBoxMoV.HeaderBackColor = SystemColors.Control;
            numericBoxMoV.HeaderText = "V";
            numericBoxMoV.Location = new Point(110, 20);
            numericBoxMoV.Margin = new Padding(0);
            numericBoxMoV.MaximumSize = new Size(1000, 30);
            numericBoxMoV.MinimumSize = new Size(1, 20);
            numericBoxMoV.Name = "numericBoxMoV";
            numericBoxMoV.Padding = new Padding(0, 0, 1, 0);
            numericBoxMoV.RadianValue = 0.54349552907103427D;
            numericBoxMoV.Size = new Size(105, 27);
            numericBoxMoV.SkipEventDuringInput = false;
            numericBoxMoV.SmartIncrement = true;
            numericBoxMoV.TabIndex = 10;
            numericBoxMoV.TextFont = new Font("メイリオ", 9F);
            numericBoxMoV.ThonsandsSeparator = true;
            numericBoxMoV.Value = 31.14D;
            numericBoxMoV.ValueChanged += textBox_TextChanged;
            // 
            // label64
            // 
            label64.AutoSize = true;
            label64.Font = new Font("Segoe UI Symbol", 9F);
            label64.ImeMode = ImeMode.NoControl;
            label64.Location = new Point(4, 54);
            label64.Name = "label64";
            label64.Size = new Size(98, 15);
            label64.TabIndex = 7;
            label64.Text = "Huang+(16)MGD";
            // 
            // numericBoxMoV0
            // 
            numericBoxMoV0.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxMoV0.BackColor = SystemColors.Control;
            numericBoxMoV0.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxMoV0.FooterBackColor = SystemColors.Control;
            numericBoxMoV0.FooterText = "Å³";
            numericBoxMoV0.HeaderBackColor = SystemColors.Control;
            numericBoxMoV0.HeaderText = "V₀";
            numericBoxMoV0.Location = new Point(2, 20);
            numericBoxMoV0.Margin = new Padding(0);
            numericBoxMoV0.MaximumSize = new Size(1000, 30);
            numericBoxMoV0.MinimumSize = new Size(1, 20);
            numericBoxMoV0.Name = "numericBoxMoV0";
            numericBoxMoV0.Padding = new Padding(0, 0, 1, 0);
            numericBoxMoV0.RadianValue = 0.54349552907103427D;
            numericBoxMoV0.Size = new Size(108, 27);
            numericBoxMoV0.SkipEventDuringInput = false;
            numericBoxMoV0.SmartIncrement = true;
            numericBoxMoV0.TabIndex = 10;
            numericBoxMoV0.TextFont = new Font("メイリオ", 9F);
            numericBoxMoV0.ThonsandsSeparator = true;
            numericBoxMoV0.Value = 31.14D;
            numericBoxMoV0.ValueChanged += textBox_TextChanged;
            // 
            // groupBoxPb
            // 
            groupBoxPb.Controls.Add(numericBoxPbStrassle);
            groupBoxPb.Controls.Add(numericBoxPbA);
            groupBoxPb.Controls.Add(label65);
            groupBoxPb.Controls.Add(numericBoxPbA0);
            groupBoxPb.Font = new Font("Segoe UI Symbol", 9.75F);
            groupBoxPb.Location = new Point(3, 567);
            groupBoxPb.Name = "groupBoxPb";
            groupBoxPb.Size = new Size(218, 76);
            groupBoxPb.TabIndex = 18;
            groupBoxPb.TabStop = false;
            groupBoxPb.Text = "Pb";
            groupBoxPb.Visible = false;
            // 
            // numericBoxPbStrassle
            // 
            numericBoxPbStrassle.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxPbStrassle.BackColor = SystemColors.Control;
            numericBoxPbStrassle.DecimalPlaces = 3;
            numericBoxPbStrassle.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxPbStrassle.FooterBackColor = SystemColors.Control;
            numericBoxPbStrassle.FooterFont = new Font("Segoe UI Symbol", 9F);
            numericBoxPbStrassle.FooterText = "GPa";
            numericBoxPbStrassle.HeaderBackColor = SystemColors.Control;
            numericBoxPbStrassle.HeaderFont = new Font("Segoe UI Symbol", 9F);
            numericBoxPbStrassle.Location = new Point(105, 50);
            numericBoxPbStrassle.Margin = new Padding(0);
            numericBoxPbStrassle.MaximumSize = new Size(1000, 30);
            numericBoxPbStrassle.MinimumSize = new Size(1, 20);
            numericBoxPbStrassle.Name = "numericBoxPbStrassle";
            numericBoxPbStrassle.Padding = new Padding(0, 0, 1, 0);
            numericBoxPbStrassle.Size = new Size(110, 27);
            numericBoxPbStrassle.SkipEventDuringInput = false;
            numericBoxPbStrassle.SmartIncrement = true;
            numericBoxPbStrassle.TabIndex = 12;
            numericBoxPbStrassle.TextBoxBackColor = Color.FromArgb(64, 64, 64);
            numericBoxPbStrassle.TextBoxForeColor = Color.FromArgb(192, 192, 255);
            numericBoxPbStrassle.TextFont = new Font("Segoe UI Symbol", 9.75F, FontStyle.Bold);
            numericBoxPbStrassle.ThonsandsSeparator = true;
            // 
            // numericBoxPbA
            // 
            numericBoxPbA.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxPbA.BackColor = SystemColors.Control;
            numericBoxPbA.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxPbA.FooterBackColor = SystemColors.Control;
            numericBoxPbA.FooterText = "Å";
            numericBoxPbA.HeaderBackColor = SystemColors.Control;
            numericBoxPbA.HeaderText = "a";
            numericBoxPbA.Location = new Point(110, 20);
            numericBoxPbA.Margin = new Padding(0);
            numericBoxPbA.MaximumSize = new Size(1000, 30);
            numericBoxPbA.MinimumSize = new Size(1, 20);
            numericBoxPbA.Name = "numericBoxPbA";
            numericBoxPbA.Padding = new Padding(0, 0, 1, 0);
            numericBoxPbA.RadianValue = 0.086404095416306087D;
            numericBoxPbA.Size = new Size(105, 27);
            numericBoxPbA.SkipEventDuringInput = false;
            numericBoxPbA.SmartIncrement = true;
            numericBoxPbA.TabIndex = 10;
            numericBoxPbA.TextFont = new Font("メイリオ", 9F);
            numericBoxPbA.ThonsandsSeparator = true;
            numericBoxPbA.Value = 4.95059D;
            numericBoxPbA.ValueChanged += textBox_TextChanged;
            // 
            // label65
            // 
            label65.AutoSize = true;
            label65.Font = new Font("Segoe UI Symbol", 9F);
            label65.ImeMode = ImeMode.NoControl;
            label65.Location = new Point(4, 54);
            label65.Name = "label65";
            label65.Size = new Size(74, 15);
            label65.TabIndex = 7;
            label65.Text = "Strässle+(14)";
            // 
            // numericBoxPbA0
            // 
            numericBoxPbA0.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxPbA0.BackColor = SystemColors.Control;
            numericBoxPbA0.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxPbA0.FooterBackColor = SystemColors.Control;
            numericBoxPbA0.FooterText = "Å";
            numericBoxPbA0.HeaderBackColor = SystemColors.Control;
            numericBoxPbA0.HeaderText = "a₀";
            numericBoxPbA0.Location = new Point(2, 20);
            numericBoxPbA0.Margin = new Padding(0);
            numericBoxPbA0.MaximumSize = new Size(1000, 30);
            numericBoxPbA0.MinimumSize = new Size(1, 20);
            numericBoxPbA0.Name = "numericBoxPbA0";
            numericBoxPbA0.Padding = new Padding(0, 0, 1, 0);
            numericBoxPbA0.RadianValue = 0.086404095416306087D;
            numericBoxPbA0.Size = new Size(108, 27);
            numericBoxPbA0.SkipEventDuringInput = false;
            numericBoxPbA0.SmartIncrement = true;
            numericBoxPbA0.TabIndex = 10;
            numericBoxPbA0.TextFont = new Font("メイリオ", 9F);
            numericBoxPbA0.ThonsandsSeparator = true;
            numericBoxPbA0.Value = 4.95059D;
            numericBoxPbA0.ValueChanged += textBox_TextChanged;
            // 
            // panelEOS
            // 
            panelEOS.Controls.Add(flowLayoutPanelEOS);
            panelEOS.Controls.Add(panel2);
            panelEOS.Dock = DockStyle.Top;
            panelEOS.Location = new Point(0, 48);
            panelEOS.Name = "panelEOS";
            panelEOS.Size = new Size(725, 207);
            panelEOS.TabIndex = 15;
            // 
            // panel2
            // 
            panel2.Controls.Add(numericBoxTemperature);
            panel2.Controls.Add(numericBoxTemperature0);
            panel2.Controls.Add(numericBoxDecimalPlaces);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(725, 29);
            panel2.TabIndex = 19;
            // 
            // numericBoxTemperature
            // 
            numericBoxTemperature.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxTemperature.BackColor = SystemColors.Control;
            numericBoxTemperature.Font = new Font("Segoe UI Symbol", 9.75F);
            numericBoxTemperature.FooterBackColor = SystemColors.Control;
            numericBoxTemperature.FooterText = "K";
            numericBoxTemperature.HeaderBackColor = SystemColors.Control;
            numericBoxTemperature.HeaderText = "Temperature";
            numericBoxTemperature.Location = new Point(3, 0);
            numericBoxTemperature.Margin = new Padding(0);
            numericBoxTemperature.MaximumSize = new Size(1000, 30);
            numericBoxTemperature.MinimumSize = new Size(1, 20);
            numericBoxTemperature.Name = "numericBoxTemperature";
            numericBoxTemperature.Padding = new Padding(0, 0, 1, 0);
            numericBoxTemperature.RadianValue = 5.2359877559829888D;
            numericBoxTemperature.Size = new Size(133, 27);
            numericBoxTemperature.SkipEventDuringInput = false;
            numericBoxTemperature.SmartIncrement = true;
            numericBoxTemperature.TabIndex = 21;
            numericBoxTemperature.TextFont = new Font("メイリオ", 9F);
            numericBoxTemperature.ThonsandsSeparator = true;
            numericBoxTemperature.Value = 300D;
            numericBoxTemperature.ValueChanged += textBox_TextChanged;
            // 
            // numericBoxTemperature0
            // 
            numericBoxTemperature0.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            numericBoxTemperature0.BackColor = SystemColors.Control;
            numericBoxTemperature0.Font = new Font("Segoe UI Symbol", 9F);
            numericBoxTemperature0.FooterBackColor = SystemColors.Control;
            numericBoxTemperature0.FooterText = "K";
            numericBoxTemperature0.HeaderBackColor = SystemColors.Control;
            numericBoxTemperature0.HeaderText = "T₀";
            numericBoxTemperature0.Location = new Point(160, 0);
            numericBoxTemperature0.Margin = new Padding(0);
            numericBoxTemperature0.MaximumSize = new Size(1000, 30);
            numericBoxTemperature0.MinimumSize = new Size(1, 20);
            numericBoxTemperature0.Name = "numericBoxTemperature0";
            numericBoxTemperature0.Padding = new Padding(0, 0, 1, 0);
            numericBoxTemperature0.RadianValue = 5.2359877559829888D;
            numericBoxTemperature0.Size = new Size(79, 27);
            numericBoxTemperature0.SkipEventDuringInput = false;
            numericBoxTemperature0.SmartIncrement = true;
            numericBoxTemperature0.TabIndex = 20;
            numericBoxTemperature0.TextFont = new Font("メイリオ", 9F);
            numericBoxTemperature0.ThonsandsSeparator = true;
            numericBoxTemperature0.Value = 300D;
            numericBoxTemperature0.ValueChanged += textBox_TextChanged;
            // 
            // numericBoxDecimalPlaces
            // 
            numericBoxDecimalPlaces.BackColor = Color.Transparent;
            numericBoxDecimalPlaces.Font = new Font("Verdana", 9F);
            numericBoxDecimalPlaces.FooterFont = new Font("Verdana", 9F);
            numericBoxDecimalPlaces.HeaderFont = new Font("Verdana", 9F);
            numericBoxDecimalPlaces.HeaderText = "Number of decimal places";
            numericBoxDecimalPlaces.Location = new Point(263, 2);
            numericBoxDecimalPlaces.Margin = new Padding(0);
            numericBoxDecimalPlaces.Maximum = 10D;
            numericBoxDecimalPlaces.MaximumSize = new Size(1000, 27);
            numericBoxDecimalPlaces.Minimum = 0D;
            numericBoxDecimalPlaces.MinimumSize = new Size(1, 17);
            numericBoxDecimalPlaces.Name = "numericBoxDecimalPlaces";
            numericBoxDecimalPlaces.RadianValue = 0.069813170079773182D;
            numericBoxDecimalPlaces.ShowUpDown = true;
            numericBoxDecimalPlaces.Size = new Size(208, 24);
            numericBoxDecimalPlaces.TabIndex = 19;
            numericBoxDecimalPlaces.TextFont = new Font("Verdana", 9F);
            numericBoxDecimalPlaces.Value = 4D;
            numericBoxDecimalPlaces.ValueChanged += numericBoxDecimalPlaces_ValueChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(725, 24);
            menuStrip1.TabIndex = 16;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { readToolStripMenuItem, exportAsCSVToolStripMenuItem, watchNewFileToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // readToolStripMenuItem
            // 
            readToolStripMenuItem.Name = "readToolStripMenuItem";
            readToolStripMenuItem.Size = new Size(226, 22);
            readToolStripMenuItem.Text = "Load";
            readToolStripMenuItem.Click += menuItemFileRead_Click;
            // 
            // exportAsCSVToolStripMenuItem
            // 
            exportAsCSVToolStripMenuItem.Name = "exportAsCSVToolStripMenuItem";
            exportAsCSVToolStripMenuItem.Size = new Size(226, 22);
            exportAsCSVToolStripMenuItem.Text = "Export as CSV";
            exportAsCSVToolStripMenuItem.Click += menuItemExport_Click;
            // 
            // watchNewFileToolStripMenuItem
            // 
            watchNewFileToolStripMenuItem.Checked = true;
            watchNewFileToolStripMenuItem.CheckOnClick = true;
            watchNewFileToolStripMenuItem.CheckState = CheckState.Checked;
            watchNewFileToolStripMenuItem.Name = "watchNewFileToolStripMenuItem";
            watchNewFileToolStripMenuItem.Size = new Size(226, 22);
            watchNewFileToolStripMenuItem.Text = "Reload the file if it is updated";
            watchNewFileToolStripMenuItem.CheckedChanged += menuItemWatchFile_Click;
            // 
            // timer
            // 
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Tick += timer_Tick;
            // 
            // FormMain
            // 
            AllowDrop = true;
            AutoScaleBaseSize = new Size(7, 15);
            ClientSize = new Size(725, 916);
            Controls.Add(splitContainer1);
            Controls.Add(groupBoxAkahama2006);
            Controls.Add(groupBoxMao);
            Controls.Add(panelEOS);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(menuStrip1);
            Font = new Font("Verdana", 9F);
            MainMenuStrip = menuStrip1;
            Name = "FormMain";
            Text = "Pressure Calculator";
            Closed += FormDiamondRaman_Closed;
            FormClosing += FormMain_FormClosing;
            Load += FormMain_Load;
            DragDrop += FormMain_DragDrop;
            DragEnter += FormMain_DragEnter;
            ((ISupportInitialize)numericUpDownDifferentiationRunningAverage).EndInit();
            ((ISupportInitialize)numericUpDownOriginalRunningAverage).EndInit();
            ((ISupportInitialize)numericUpDownOriginalGaussian).EndInit();
            ((ISupportInitialize)numericUpDownDifferentiationGaussian).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel4.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            ((ISupportInitialize)numericUpDownFitRange).EndInit();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            groupBoxMao.ResumeLayout(false);
            groupBoxMao.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBoxAkahama2006.ResumeLayout(false);
            groupBoxAkahama2006.PerformLayout();
            flowLayoutPanelEOS.ResumeLayout(false);
            groupBoxGold.ResumeLayout(false);
            groupBoxGold.PerformLayout();
            groupBoxPlatinum.ResumeLayout(false);
            groupBoxPlatinum.PerformLayout();
            groupBoxNaClB1.ResumeLayout(false);
            groupBoxNaClB1.PerformLayout();
            groupBoxNaClB2.ResumeLayout(false);
            groupBoxNaClB2.PerformLayout();
            groupBoxPericlase.ResumeLayout(false);
            groupBoxPericlase.PerformLayout();
            groupBoxCorundum.ResumeLayout(false);
            groupBoxCorundum.PerformLayout();
            groupBoxAr.ResumeLayout(false);
            groupBoxAr.PerformLayout();
            groupBoxRe.ResumeLayout(false);
            groupBoxRe.PerformLayout();
            groupBoxMo.ResumeLayout(false);
            groupBoxMo.PerformLayout();
            groupBoxPb.ResumeLayout(false);
            groupBoxPb.PerformLayout();
            panelEOS.ResumeLayout(false);
            panel2.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private RadioButton radioButtonTempUnitK;
        private RadioButton radioButtonTempUnitC;
        private Label label17;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem readToolStripMenuItem;
        private ToolStripMenuItem exportAsCSVToolStripMenuItem;
        private ToolStripMenuItem watchNewFileToolStripMenuItem;
        private System.Windows.Forms.Timer timer;
        private TextBox textBoxDiamondFratandunoHigh;
        private TextBox textBoxDiamondFratandunoLow;
        private Label label20;
        private Label label19;
        private Label label24;
        private Label label21;
        private FlowLayoutPanel flowLayoutPanel4;
        private TextBox textBoxFittingInformation;
        private Label label25;
        private Crystallography.Controls.NumericBox numericBoxDecimalPlaces;
        private GroupBox groupBoxGold;
        private Crystallography.Controls.NumericBox numericBoxAuFratanduono;
        private Crystallography.Controls.NumericBox numericBoxAuYokoo;
        private Crystallography.Controls.NumericBox numericalTextBoxGoldJamieson;
        private Crystallography.Controls.NumericBox numericalTextBoxGoldTsuchiya;
        private Crystallography.Controls.NumericBox numericalTextBoxGoldSim;
        private Label label11;
        private Crystallography.Controls.NumericBox numericalTextBoxGoldAnderson;
        private Label label49;
        public Crystallography.Controls.NumericBox numericalTextBoxGoldA;
        public Crystallography.Controls.NumericBox numericBoxGoldA0;
        private Label label22;
        private Label label69;
        private Label label15;
        private Label label27;
        private GroupBox groupBoxPlatinum;
        private Crystallography.Controls.NumericBox numericBoxPtFratanduono;
        private Crystallography.Controls.NumericBox numericBoxPtYokoo;
        private Crystallography.Controls.NumericBox numericalTextBoxPtMatsui;
        private Crystallography.Controls.NumericBox numericalTextBoxPtHolmes;
        private Label label16;
        private Crystallography.Controls.NumericBox numericalTextBoxPtJamieson;
        private Label label23;
        private Label label26;
        private Label label36;
        public Crystallography.Controls.NumericBox numericalTextBoxPtA;
        public Crystallography.Controls.NumericBox numericBoxPtA0;
        private Label label37;
        private GroupBox groupBoxNaClB1;
        private Crystallography.Controls.NumericBox numericalTextBoxNaClB1Matsui;
        private Crystallography.Controls.NumericBox numericalTextBoxNaClB1Brown;
        private Label label38;
        private Label label39;
        public Crystallography.Controls.NumericBox numericalTextBoxNaClB1A;
        public Crystallography.Controls.NumericBox numericBoxNaClB1A0;
        private GroupBox groupBoxNaClB2;
        private Crystallography.Controls.NumericBox numericalTextBoxNaClB2SakaiVinet;
        private Crystallography.Controls.NumericBox numericalTextBoxNaClB2SakaiBM;
        private Crystallography.Controls.NumericBox numericalTextBoxNaClB2Ueda;
        private Label label40;
        private Crystallography.Controls.NumericBox numericalTextBoxNaClB2SataMgO;
        private Label label41;
        private Crystallography.Controls.NumericBox numericalTextBoxNaClB2SataPt;
        private Label label47;
        private Label label48;
        public Crystallography.Controls.NumericBox numericalTextBoxNaClB2A;
        private Label label50;
        private Crystallography.Controls.NumericBox numericalTextBoxNaClB2A0;
        private GroupBox groupBoxPericlase;
        private Crystallography.Controls.NumericBox numericBoxMgOTangeBM;
        private Crystallography.Controls.NumericBox numericBoxMgOTangeVinet;
        private Crystallography.Controls.NumericBox numericalTextBoxMgOAizawa;
        private Label label51;
        private Crystallography.Controls.NumericBox numericalTextBoxMgODewaele;
        private Label label52;
        private Crystallography.Controls.NumericBox numericalTextBoxMgOJacson;
        private Label label53;
        private Label label54;
        public Crystallography.Controls.NumericBox numericalTextBoxMgOA;
        private Label label55;
        public Crystallography.Controls.NumericBox numericBoxMgOA0;
        private GroupBox groupBoxCorundum;
        private Crystallography.Controls.NumericBox numericBoxCorundumDubrovinsky;
        public Crystallography.Controls.NumericBox numericalTextBoxColundumV;
        private Label label56;
        public Crystallography.Controls.NumericBox numericBoxColundumV0;
        private GroupBox groupBoxAr;
        private Crystallography.Controls.NumericBox numericalTextBoxArRoss;
        private Crystallography.Controls.NumericBox numericalTextBoxArJephcoat;
        public Crystallography.Controls.NumericBox numericalTextBoxArA;
        private Label label57;
        private Label label58;
        public Crystallography.Controls.NumericBox numericBoxArA0;
        private GroupBox groupBoxRe;
        private Crystallography.Controls.NumericBox numericBoxReDub;
        private Crystallography.Controls.NumericBox numericBoxReSakai;
        private Label label59;
        private Crystallography.Controls.NumericBox numericBoxReAnz;
        private Label label60;
        private Crystallography.Controls.NumericBox numericalTextBoxReZha;
        private Label label61;
        public Crystallography.Controls.NumericBox numericBoxReV;
        private Label label62;
        public Crystallography.Controls.NumericBox numerictBoxReV0;
        private GroupBox groupBoxMo;
        private Crystallography.Controls.NumericBox numericBoxMoZhao;
        private Crystallography.Controls.NumericBox numericBoxMoHuang;
        private Label label63;
        public Crystallography.Controls.NumericBox numericBoxMoV;
        private Label label64;
        public Crystallography.Controls.NumericBox numericBoxMoV0;
        private GroupBox groupBoxPb;
        private Crystallography.Controls.NumericBox numericBoxPbStrassle;
        public Crystallography.Controls.NumericBox numericBoxPbA;
        private Label label65;
        public Crystallography.Controls.NumericBox numericBoxPbA0;
        public Crystallography.Controls.NumericBox numericBoxTemperature;
        public Crystallography.Controls.NumericBox numericBoxTemperature0;
    }



}
