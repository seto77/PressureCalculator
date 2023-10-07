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
        private GroupBox groupBoxGold;
        private TextBox textBoxGoldJamieson;
        private TextBox textBoxGoldAnderson;
        private TextBox textBoxGoldTsuchiya;
        private Label label50;
        private TextBox textBoxGoldSim;
        private Label label11;
        private Label label70;
        private Label label15;
        public TextBox textBoxGold_a;
        private TextBox textBoxGold_a0;
        private Label label16;
        private Label label37;
        private Label label49;
        private Label label23;
        private Label label22;
        private Label label69;
        private Label label36;
        private Label label38;
        private GroupBox groupBoxPlatinum;
        private TextBox textBoxPtA0;
        private TextBox textBoxPtJamieson;
        private TextBox textBoxPtHolems;
        private Label label39;
        private Label label47;
        private Label label61;
        public TextBox textBoxPtA;
        private Label label54;
        private Label label48;
        private Label label51;
        private Label label52;
        private Label label60;
        private GroupBox groupBoxNaClB1;
        public TextBox textBoxNaClB1_a0;
        public TextBox textBoxNaClB1_a;
        private Label label53;
        private Label label55;
        private Label label56;
        private TextBox textBoxNaClB1Brown;
        private Label label62;
        private Label label57;
        private Label label58;
        private GroupBox groupBoxNaClB2;
        public TextBox textBoxNaClB2_a0;
        public TextBox textBoxNaClB2_a;
        private TextBox textBoxNaClB2SataMgO;
        private TextBox textBoxNaClB2SataPt;
        private Label label65;
        private Label label67;
        private Label label59;
        private Label label63;
        private Label label64;
        private Label label66;
        private Label label68;
        private Label label71;
        private GroupBox groupBoxPericlase;
        private Label label72;
        private Label label73;
        private TextBox textBoxMgOAizawa;
        private Label label74;
        private TextBox textBoxMgODewaele;
        private TextBox textBoxMgOJacson;
        public TextBox textBoxMgOA0;
        private Label label75;
        private Label label76;
        private Label label77;
        private Label label78;
        public TextBox textBoxMgOA;
        private Label label79;
        private Label label80;
        private Label label81;
        private GroupBox groupBoxCorundum;
        private Label label82;
        private TextBox textBoxCorundumDubrovinsky;
        public TextBox textBoxColundumV0;
        private Label label83;
        private Label label84;
        public TextBox textBoxCorundumV;
        private Label label85;
        private Label label86;
        private Label label87;
        private GroupBox groupBoxAr;
        public TextBox textBoxArA0;
        public TextBox textBoxArA;
        private TextBox textBoxArJephcoat;
        private Label label88;
        private TextBox textBoxArRoss;
        private Label label89;
        private Label label90;
        private Label label91;
        private Label label92;
        private Label label93;
        private Label label94;
        private Label label95;
        private GroupBox groupBoxRe;
        private Label label96;
        private TextBox textBoxReZha;
        public TextBox textBoxReV0;
        private Label label97;
        private Label label98;
        public TextBox textBoxReV;
        private Label label99;
        private Label label100;
        private Label label101;
        private Panel panelEOS;
        private TextBox textBoxT;
        private Label label103;
        private Label label102;
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
            textBoxGoldJamieson = new TextBox();
            textBoxGoldAnderson = new TextBox();
            textBoxGoldTsuchiya = new TextBox();
            label50 = new Label();
            textBoxGoldSim = new TextBox();
            label11 = new Label();
            label70 = new Label();
            label15 = new Label();
            textBoxGold_a = new TextBox();
            textBoxGold_a0 = new TextBox();
            label16 = new Label();
            label37 = new Label();
            label49 = new Label();
            label23 = new Label();
            label22 = new Label();
            label69 = new Label();
            label36 = new Label();
            label38 = new Label();
            groupBoxPlatinum = new GroupBox();
            textBoxPtA0 = new TextBox();
            textBoxPtJamieson = new TextBox();
            textBoxPtHolems = new TextBox();
            label39 = new Label();
            label47 = new Label();
            label61 = new Label();
            textBoxPtA = new TextBox();
            label54 = new Label();
            label48 = new Label();
            label51 = new Label();
            label52 = new Label();
            label60 = new Label();
            groupBoxNaClB1 = new GroupBox();
            textBoxNaClB1_a0 = new TextBox();
            textBoxNaClB1_a = new TextBox();
            label53 = new Label();
            label55 = new Label();
            label56 = new Label();
            textBoxNaClB1Brown = new TextBox();
            label62 = new Label();
            label57 = new Label();
            label58 = new Label();
            groupBoxNaClB2 = new GroupBox();
            textBoxNaClB2_a0 = new TextBox();
            textBoxNaClB2_a = new TextBox();
            textBoxNaClB2SataMgO = new TextBox();
            textBoxNaClB2SataPt = new TextBox();
            label65 = new Label();
            label67 = new Label();
            label59 = new Label();
            label63 = new Label();
            label64 = new Label();
            label66 = new Label();
            label68 = new Label();
            label71 = new Label();
            groupBoxPericlase = new GroupBox();
            label72 = new Label();
            label73 = new Label();
            textBoxMgOAizawa = new TextBox();
            label74 = new Label();
            textBoxMgODewaele = new TextBox();
            textBoxMgOJacson = new TextBox();
            textBoxMgOA0 = new TextBox();
            label75 = new Label();
            label76 = new Label();
            label77 = new Label();
            label78 = new Label();
            textBoxMgOA = new TextBox();
            label79 = new Label();
            label80 = new Label();
            label81 = new Label();
            groupBoxCorundum = new GroupBox();
            label82 = new Label();
            textBoxCorundumDubrovinsky = new TextBox();
            textBoxColundumV0 = new TextBox();
            label83 = new Label();
            label84 = new Label();
            textBoxCorundumV = new TextBox();
            label85 = new Label();
            label86 = new Label();
            label87 = new Label();
            groupBoxAr = new GroupBox();
            textBoxArA0 = new TextBox();
            textBoxArA = new TextBox();
            textBoxArJephcoat = new TextBox();
            label88 = new Label();
            textBoxArRoss = new TextBox();
            label89 = new Label();
            label90 = new Label();
            label91 = new Label();
            label92 = new Label();
            label93 = new Label();
            label94 = new Label();
            label95 = new Label();
            groupBoxRe = new GroupBox();
            label96 = new Label();
            textBoxReZha = new TextBox();
            textBoxReV0 = new TextBox();
            label97 = new Label();
            label98 = new Label();
            textBoxReV = new TextBox();
            label99 = new Label();
            label100 = new Label();
            label101 = new Label();
            panelEOS = new Panel();
            panel2 = new Panel();
            label103 = new Label();
            textBoxT = new TextBox();
            label102 = new Label();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            readToolStripMenuItem = new ToolStripMenuItem();
            exportAsCSVToolStripMenuItem = new ToolStripMenuItem();
            watchNewFileToolStripMenuItem = new ToolStripMenuItem();
            timer = new System.Windows.Forms.Timer(components);
            label25 = new Label();
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
            panelEOS.SuspendLayout();
            panel2.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // numericUpDownDifferentiationRunningAverage
            // 
            numericUpDownDifferentiationRunningAverage.AutoSize = true;
            numericUpDownDifferentiationRunningAverage.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            numericUpDownOriginalRunningAverage.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            label2.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(126, 6);
            label2.Margin = new Padding(3, 6, 3, 0);
            label2.Name = "label2";
            label2.Size = new Size(114, 14);
            label2.TabIndex = 3;
            label2.Text = "Running Average";
            // 
            // textBoxAkahama2004A
            // 
            textBoxAkahama2004A.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxAkahama2004A.Location = new Point(293, 16);
            textBoxAkahama2004A.Name = "textBoxAkahama2004A";
            textBoxAkahama2004A.Size = new Size(44, 22);
            textBoxAkahama2004A.TabIndex = 0;
            textBoxAkahama2004A.Text = "66.9";
            textBoxAkahama2004A.TextChanged += textBoxNu_TextChanged;
            // 
            // textBoxAkahama2004B
            // 
            textBoxAkahama2004B.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxAkahama2004B.Location = new Point(355, 16);
            textBoxAkahama2004B.Name = "textBoxAkahama2004B";
            textBoxAkahama2004B.Size = new Size(44, 22);
            textBoxAkahama2004B.TabIndex = 0;
            textBoxAkahama2004B.Text = "-0.5281";
            textBoxAkahama2004B.TextChanged += textBoxNu_TextChanged;
            // 
            // textBoxAkahama2004C
            // 
            textBoxAkahama2004C.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxAkahama2004C.Location = new Point(427, 16);
            textBoxAkahama2004C.Name = "textBoxAkahama2004C";
            textBoxAkahama2004C.Size = new Size(44, 22);
            textBoxAkahama2004C.TabIndex = 0;
            textBoxAkahama2004C.Text = "3.585";
            textBoxAkahama2004C.TextChanged += textBoxNu_TextChanged;
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(264, 20);
            label5.Name = "label5";
            label5.Size = new Size(39, 12);
            label5.TabIndex = 1;
            label5.Text = "P =";
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(337, 19);
            label6.Name = "label6";
            label6.Size = new Size(12, 12);
            label6.TabIndex = 1;
            label6.Text = "+";
            // 
            // label7
            // 
            label7.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label7.Location = new Point(399, 19);
            label7.Name = "label7";
            label7.Size = new Size(24, 12);
            label7.TabIndex = 1;
            label7.Text = "ν+";
            // 
            // label8
            // 
            label8.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(475, 19);
            label8.Name = "label8";
            label8.Size = new Size(60, 12);
            label8.TabIndex = 1;
            label8.Text = "E-4 ν^2=";
            // 
            // textBoxDiamondAkahama2004P
            // 
            textBoxDiamondAkahama2004P.BackColor = Color.Navy;
            textBoxDiamondAkahama2004P.Font = new Font("Segoe UI Symbol", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
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
            label9.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            label14.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            numericUpDownOriginalGaussian.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            label12.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            labelBottomTitle.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            label13.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            label1.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            numericUpDownDifferentiationGaussian.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            splitContainer1.Location = new Point(0, 130);
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
            splitContainer1.Size = new Size(725, 296);
            splitContainer1.SplitterDistance = 136;
            splitContainer1.TabIndex = 6;
            // 
            // graphControlTop
            // 
            graphControlTop.AllowMouseOperation = true;
            graphControlTop.AxisLineColor = Color.Gray;
            graphControlTop.AxisTextColor = Color.Black;
            graphControlTop.AxisTextFont = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            graphControlTop.Font = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            graphControlTop.Size = new Size(725, 108);
            graphControlTop.Smoothing = false;
            graphControlTop.TabIndex = 5;
            graphControlTop.UnitX = "";
            graphControlTop.UnitY = "";
            graphControlTop.UpperPanelFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            graphControlBottom.AxisTextFont = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            graphControlBottom.Font = new Font("Cambria", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            graphControlBottom.Size = new Size(725, 96);
            graphControlBottom.Smoothing = false;
            graphControlBottom.TabIndex = 6;
            graphControlBottom.UnitX = "";
            graphControlBottom.UnitY = "";
            graphControlBottom.UpperPanelFont = new Font("Cambria", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            // textBoxFittingInformation
            // 
            textBoxFittingInformation.BackColor = SystemColors.Control;
            textBoxFittingInformation.Font = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            label10.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            numericUpDownFitRange.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            labelDimension.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            radioButtonDiamondRaman.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            radioButtonRubyFluorescence.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            radioButtonEOS.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            textBoxMaoHydroP.Font = new Font("Segoe UI Symbol", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
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
            label28.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            groupBoxMao.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxMao.Location = new Point(0, 550);
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
            radioButtonTempUnitK.Font = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            radioButtonTempUnitC.Font = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            textBoxMaoP.Font = new Font("Segoe UI Symbol", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
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
            numericBoxMaoA.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxMaoA.FooterFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxMaoA.FooterText = "f(x, 5)=";
            numericBoxMaoA.HeaderFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxMaoA.HeaderText = "P=";
            numericBoxMaoA.Location = new Point(118, 22);
            numericBoxMaoA.Margin = new Padding(0);
            numericBoxMaoA.MaximumSize = new Size(1000, 28);
            numericBoxMaoA.MinimumSize = new Size(1, 18);
            numericBoxMaoA.Name = "numericBoxMaoA";
            numericBoxMaoA.RadianValue = 33.231068957972035D;
            numericBoxMaoA.RoundErrorAccuracy = -1;
            numericBoxMaoA.Size = new Size(103, 25);
            numericBoxMaoA.TabIndex = 3;
            numericBoxMaoA.TextFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxMaoA.Value = 1904D;
            // 
            // numericBoxMaoQuasiA
            // 
            numericBoxMaoQuasiA.BackColor = Color.Transparent;
            numericBoxMaoQuasiA.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxMaoQuasiA.FooterFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxMaoQuasiA.FooterText = "f(x, 7.665)=";
            numericBoxMaoQuasiA.HeaderFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxMaoQuasiA.HeaderText = "P=";
            numericBoxMaoQuasiA.Location = new Point(117, 49);
            numericBoxMaoQuasiA.Margin = new Padding(0);
            numericBoxMaoQuasiA.MaximumSize = new Size(1000, 28);
            numericBoxMaoQuasiA.MinimumSize = new Size(1, 18);
            numericBoxMaoQuasiA.Name = "numericBoxMaoQuasiA";
            numericBoxMaoQuasiA.RadianValue = 33.231068957972035D;
            numericBoxMaoQuasiA.RoundErrorAccuracy = -1;
            numericBoxMaoQuasiA.Size = new Size(123, 25);
            numericBoxMaoQuasiA.TabIndex = 3;
            numericBoxMaoQuasiA.TextFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxMaoQuasiA.Value = 1904D;
            // 
            // numericBoxShenA
            // 
            numericBoxShenA.BackColor = Color.Transparent;
            numericBoxShenA.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxShenA.FooterFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxShenA.FooterText = "(Δ+5.63 Δ²)=";
            numericBoxShenA.HeaderFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxShenA.HeaderText = "P=";
            numericBoxShenA.Location = new Point(485, 49);
            numericBoxShenA.Margin = new Padding(0);
            numericBoxShenA.MaximumSize = new Size(1000, 28);
            numericBoxShenA.MinimumSize = new Size(1, 18);
            numericBoxShenA.Name = "numericBoxShenA";
            numericBoxShenA.RadianValue = 32.637657012293964D;
            numericBoxShenA.RoundErrorAccuracy = -1;
            numericBoxShenA.Size = new Size(131, 25);
            numericBoxShenA.TabIndex = 3;
            numericBoxShenA.TextFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxShenA.Value = 1870D;
            // 
            // numericBoxMaoHydroA
            // 
            numericBoxMaoHydroA.BackColor = Color.Transparent;
            numericBoxMaoHydroA.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxMaoHydroA.FooterFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxMaoHydroA.FooterText = "f(x, 7.715)=";
            numericBoxMaoHydroA.HeaderFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxMaoHydroA.HeaderText = "P=";
            numericBoxMaoHydroA.Location = new Point(485, 22);
            numericBoxMaoHydroA.Margin = new Padding(0);
            numericBoxMaoHydroA.MaximumSize = new Size(1000, 28);
            numericBoxMaoHydroA.MinimumSize = new Size(1, 18);
            numericBoxMaoHydroA.Name = "numericBoxMaoHydroA";
            numericBoxMaoHydroA.RadianValue = 33.231068957972035D;
            numericBoxMaoHydroA.RoundErrorAccuracy = -1;
            numericBoxMaoHydroA.Size = new Size(123, 25);
            numericBoxMaoHydroA.TabIndex = 3;
            numericBoxMaoHydroA.TextFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxMaoHydroA.Value = 1904D;
            // 
            // label42
            // 
            label42.AutoSize = true;
            label42.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label42.Location = new Point(305, 50);
            label42.Name = "label42";
            label42.Size = new Size(31, 17);
            label42.TabIndex = 1;
            label42.Text = "GPa";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label18.Location = new Point(305, 23);
            label18.Name = "label18";
            label18.Size = new Size(31, 17);
            label18.TabIndex = 1;
            label18.Text = "GPa";
            // 
            // textBoxMaoQuasiP
            // 
            textBoxMaoQuasiP.BackColor = Color.Navy;
            textBoxMaoQuasiP.Font = new Font("Segoe UI Symbol", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
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
            label107.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label107.Location = new Point(367, 50);
            label107.Name = "label107";
            label107.Size = new Size(108, 17);
            label107.TabIndex = 1;
            label107.Text = "Shen et al. (2020)";
            // 
            // label46
            // 
            label46.AutoSize = true;
            label46.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label46.Location = new Point(367, 23);
            label46.Name = "label46";
            label46.Size = new Size(114, 17);
            label46.TabIndex = 1;
            label46.Text = "Mao-hydro (2000)";
            // 
            // label45
            // 
            label45.AutoSize = true;
            label45.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label45.Location = new Point(6, 50);
            label45.Name = "label45";
            label45.Size = new Size(111, 17);
            label45.TabIndex = 1;
            label45.Text = "Mao-quasi (1986)";
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label34.Location = new Point(6, 23);
            label34.Name = "label34";
            label34.Size = new Size(75, 17);
            label34.TabIndex = 1;
            label34.Text = "Mao (1978)";
            // 
            // textBoxShenP
            // 
            textBoxShenP.BackColor = Color.Navy;
            textBoxShenP.Font = new Font("Segoe UI Symbol", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
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
            label104.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            numericBoxRubyRagan.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxRubyRagan.FooterText = "+ 4.49×10⁻²×T - 4.81×10⁻⁴×T² + 3.71×10⁻⁷×T³ )⁻¹ ×10⁷    [nm]";
            numericBoxRubyRagan.HeaderText = "R1(T, P=0) = (";
            numericBoxRubyRagan.Location = new Point(9, 18);
            numericBoxRubyRagan.Margin = new Padding(0);
            numericBoxRubyRagan.MaximumSize = new Size(1000, 28);
            numericBoxRubyRagan.MinimumSize = new Size(1, 18);
            numericBoxRubyRagan.Name = "numericBoxRubyRagan";
            numericBoxRubyRagan.RadianValue = 251.72883801514214D;
            numericBoxRubyRagan.RoundErrorAccuracy = -1;
            numericBoxRubyRagan.Size = new Size(542, 25);
            numericBoxRubyRagan.TabIndex = 3;
            numericBoxRubyRagan.TextFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            checkBoxRubyR1_0CalculatedFromRagan.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            numericBoxRubyT.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxRubyT.FooterText = "K";
            numericBoxRubyT.HeaderText = "T =";
            numericBoxRubyT.Location = new Point(11, 18);
            numericBoxRubyT.Margin = new Padding(0);
            numericBoxRubyT.MaximumSize = new Size(1000, 28);
            numericBoxRubyT.MinimumSize = new Size(1, 18);
            numericBoxRubyT.Name = "numericBoxRubyT";
            numericBoxRubyT.RadianValue = 5.18624587230115D;
            numericBoxRubyT.RoundErrorAccuracy = -1;
            numericBoxRubyT.Size = new Size(116, 23);
            numericBoxRubyT.TabIndex = 3;
            numericBoxRubyT.TextFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxRubyT.Value = 297.15D;
            numericBoxRubyT.ValueChanged += numericBoxRubyT_ValueChanged;
            // 
            // checkBoxRubyTemeratureSameAsRef
            // 
            checkBoxRubyTemeratureSameAsRef.AutoSize = true;
            checkBoxRubyTemeratureSameAsRef.Checked = true;
            checkBoxRubyTemeratureSameAsRef.CheckState = CheckState.Checked;
            checkBoxRubyTemeratureSameAsRef.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            numericBoxRubyR1_0.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxRubyR1_0.FooterText = "nm";
            numericBoxRubyR1_0.HeaderText = "R1₀=R1(T,P=0)";
            numericBoxRubyR1_0.Location = new Point(11, 52);
            numericBoxRubyR1_0.Margin = new Padding(0);
            numericBoxRubyR1_0.MaximumSize = new Size(1000, 28);
            numericBoxRubyR1_0.MinimumSize = new Size(1, 18);
            numericBoxRubyR1_0.Name = "numericBoxRubyR1_0";
            numericBoxRubyR1_0.RadianValue = 12.12392964897861D;
            numericBoxRubyR1_0.RoundErrorAccuracy = -1;
            numericBoxRubyR1_0.Size = new Size(171, 25);
            numericBoxRubyR1_0.TabIndex = 3;
            numericBoxRubyR1_0.TextFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            buttonRubyRefR1Set.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            numericBoxRubyRefT.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxRubyRefT.FooterText = "K";
            numericBoxRubyRefT.Location = new Point(46, 21);
            numericBoxRubyRefT.Margin = new Padding(0);
            numericBoxRubyRefT.MaximumSize = new Size(1000, 28);
            numericBoxRubyRefT.MinimumSize = new Size(1, 18);
            numericBoxRubyRefT.Name = "numericBoxRubyRefT";
            numericBoxRubyRefT.RadianValue = 5.18624587230115D;
            numericBoxRubyRefT.RoundErrorAccuracy = -1;
            numericBoxRubyRefT.Size = new Size(67, 23);
            numericBoxRubyRefT.TabIndex = 3;
            numericBoxRubyRefT.TextFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxRubyRefT.Value = 297.15D;
            numericBoxRubyRefT.ValueChanged += numericBoxRubyRefT_ValueChanged;
            // 
            // numericBoxRubyRefR1
            // 
            numericBoxRubyRefR1.BackColor = Color.Transparent;
            numericBoxRubyRefR1.DecimalPlaces = 3;
            numericBoxRubyRefR1.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxRubyRefR1.FooterText = "nm";
            numericBoxRubyRefR1.HeaderText = "R1(T=                , P=0) = ";
            numericBoxRubyRefR1.Location = new Point(7, 21);
            numericBoxRubyRefR1.Margin = new Padding(0);
            numericBoxRubyRefR1.MaximumSize = new Size(1000, 28);
            numericBoxRubyRefR1.MinimumSize = new Size(1, 18);
            numericBoxRubyRefR1.Name = "numericBoxRubyRefR1";
            numericBoxRubyRefR1.RadianValue = 12.116948331970631D;
            numericBoxRubyRefR1.RoundErrorAccuracy = -1;
            numericBoxRubyRefR1.Size = new Size(243, 25);
            numericBoxRubyRefR1.TabIndex = 3;
            numericBoxRubyRefR1.TextFont = new Font("Segoe UI Symbol", 9F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxRubyRefR1.Value = 694.25D;
            numericBoxRubyRefR1.ValueChanged += numericBoxRubyRefR1_ValueChanged;
            // 
            // numericBoxRubyR1
            // 
            numericBoxRubyR1.BackColor = Color.Transparent;
            numericBoxRubyR1.DecimalPlaces = 3;
            numericBoxRubyR1.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxRubyR1.FooterText = "nm";
            numericBoxRubyR1.HeaderText = "R1";
            numericBoxRubyR1.Location = new Point(8, 28);
            numericBoxRubyR1.Margin = new Padding(0);
            numericBoxRubyR1.MaximumSize = new Size(1000, 32);
            numericBoxRubyR1.MinimumSize = new Size(1, 22);
            numericBoxRubyR1.Name = "numericBoxRubyR1";
            numericBoxRubyR1.RadianValue = 12.12392964897861D;
            numericBoxRubyR1.RoundErrorAccuracy = -1;
            numericBoxRubyR1.Size = new Size(125, 29);
            numericBoxRubyR1.TabIndex = 3;
            numericBoxRubyR1.TextFont = new Font("Segoe UI Symbol", 11F, FontStyle.Regular, GraphicsUnit.Point);
            numericBoxRubyR1.Value = 694.65D;
            numericBoxRubyR1.ValueChanged += numericBoxRubyR1_ValueChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI Symbol", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
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
            groupBoxAkahama2006.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxAkahama2006.Location = new Point(0, 426);
            groupBoxAkahama2006.Name = "groupBoxAkahama2006";
            groupBoxAkahama2006.Size = new Size(725, 124);
            groupBoxAkahama2006.TabIndex = 13;
            groupBoxAkahama2006.TabStop = false;
            groupBoxAkahama2006.Text = "Pressure calculation from the Raman edge";
            // 
            // textBoxDiamondFratandunoHigh
            // 
            textBoxDiamondFratandunoHigh.BackColor = Color.Navy;
            textBoxDiamondFratandunoHigh.Font = new Font("Segoe UI Symbol", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
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
            textBoxDiamondAkahama2006P.Font = new Font("Segoe UI Symbol", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
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
            label44.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label44.Location = new Point(6, 43);
            label44.Name = "label44";
            label44.Size = new Size(24, 12);
            label44.TabIndex = 1;
            label44.Text = "ν0";
            // 
            // label29
            // 
            label29.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label29.Location = new Point(4, 20);
            label29.Name = "label29";
            label29.Size = new Size(16, 12);
            label29.TabIndex = 1;
            label29.Text = "ν";
            // 
            // textBoxDiamondRamanNu0
            // 
            textBoxDiamondRamanNu0.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxDiamondRamanNu0.Location = new Point(36, 39);
            textBoxDiamondRamanNu0.Name = "textBoxDiamondRamanNu0";
            textBoxDiamondRamanNu0.Size = new Size(63, 22);
            textBoxDiamondRamanNu0.TabIndex = 0;
            textBoxDiamondRamanNu0.Text = "1334";
            textBoxDiamondRamanNu0.TextChanged += textBoxNu_TextChanged;
            // 
            // label43
            // 
            label43.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label43.Location = new Point(105, 46);
            label43.Name = "label43";
            label43.Size = new Size(52, 12);
            label43.TabIndex = 1;
            label43.Text = "cm^-1";
            // 
            // textBoxDiamondRamanNu
            // 
            textBoxDiamondRamanNu.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxDiamondRamanNu.Location = new Point(35, 16);
            textBoxDiamondRamanNu.Name = "textBoxDiamondRamanNu";
            textBoxDiamondRamanNu.Size = new Size(64, 22);
            textBoxDiamondRamanNu.TabIndex = 0;
            textBoxDiamondRamanNu.Text = "0";
            textBoxDiamondRamanNu.TextChanged += textBoxNu_TextChanged;
            // 
            // label30
            // 
            label30.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label30.Location = new Point(105, 20);
            label30.Name = "label30";
            label30.Size = new Size(52, 12);
            label30.TabIndex = 1;
            label30.Text = "cm^-1";
            // 
            // textBoxAkahama2006K0
            // 
            textBoxAkahama2006K0.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxAkahama2006K0.Location = new Point(309, 43);
            textBoxAkahama2006K0.Name = "textBoxAkahama2006K0";
            textBoxAkahama2006K0.Size = new Size(44, 22);
            textBoxAkahama2006K0.TabIndex = 0;
            textBoxAkahama2006K0.Text = "547";
            textBoxAkahama2006K0.TextChanged += textBoxNu_TextChanged;
            // 
            // textBoxAkahama2006K0Prime
            // 
            textBoxAkahama2006K0Prime.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
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
            textBoxDiamondFratandunoLow.Font = new Font("Segoe UI Symbol", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
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
            label4.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(160, 19);
            label4.Name = "label4";
            label4.Size = new Size(90, 13);
            label4.TabIndex = 1;
            label4.Text = "Akahama (2004):";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label20.Location = new Point(160, 100);
            label20.Name = "label20";
            label20.Size = new Size(430, 13);
            label20.TabIndex = 1;
            label20.Text = "Fratanduono+ (2021, >200GPa):  P = 199.49 - 852.78 × Δν/ν0 + 3103.8 × (Δν/ν0)² = ";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label19.Location = new Point(160, 73);
            label19.Name = "label19";
            label19.Size = new Size(387, 13);
            label19.TabIndex = 1;
            label19.Text = "Fratanduono+ (2021, <300GPa):  P = 503.77 × Δν/ν0 + 753.83 × (Δν/ν0)² = ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(160, 46);
            label3.Name = "label3";
            label3.Size = new Size(90, 13);
            label3.TabIndex = 1;
            label3.Text = "Akahama (2006):";
            // 
            // label24
            // 
            label24.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label24.Location = new Point(688, 72);
            label24.Name = "label24";
            label24.Size = new Size(34, 12);
            label24.TabIndex = 1;
            label24.Text = "GPa";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label31.Location = new Point(264, 46);
            label31.Name = "label31";
            label31.Size = new Size(39, 13);
            label31.TabIndex = 1;
            label31.Text = "P = K0";
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label33.Location = new Point(520, 46);
            label33.Name = "label33";
            label33.Size = new Size(65, 13);
            label33.TabIndex = 1;
            label33.Text = "-1)*Δν/ν0]=";
            // 
            // label21
            // 
            label21.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label21.Location = new Point(691, 99);
            label21.Name = "label21";
            label21.Size = new Size(34, 12);
            label21.TabIndex = 1;
            label21.Text = "GPa";
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Font = new Font("Segoe UI Symbol", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label32.Location = new Point(357, 46);
            label32.Name = "label32";
            label32.Size = new Size(115, 13);
            label32.TabIndex = 1;
            label32.Text = "× Δν/ν0 × [1+1/2 (K0'";
            // 
            // label35
            // 
            label35.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            flowLayoutPanelEOS.Dock = DockStyle.Fill;
            flowLayoutPanelEOS.Location = new Point(0, 26);
            flowLayoutPanelEOS.Name = "flowLayoutPanelEOS";
            flowLayoutPanelEOS.Size = new Size(725, 56);
            flowLayoutPanelEOS.TabIndex = 14;
            // 
            // groupBoxGold
            // 
            groupBoxGold.Controls.Add(textBoxGoldJamieson);
            groupBoxGold.Controls.Add(textBoxGoldAnderson);
            groupBoxGold.Controls.Add(textBoxGoldTsuchiya);
            groupBoxGold.Controls.Add(label50);
            groupBoxGold.Controls.Add(textBoxGoldSim);
            groupBoxGold.Controls.Add(label11);
            groupBoxGold.Controls.Add(label70);
            groupBoxGold.Controls.Add(label15);
            groupBoxGold.Controls.Add(textBoxGold_a);
            groupBoxGold.Controls.Add(textBoxGold_a0);
            groupBoxGold.Controls.Add(label16);
            groupBoxGold.Controls.Add(label37);
            groupBoxGold.Controls.Add(label49);
            groupBoxGold.Controls.Add(label23);
            groupBoxGold.Controls.Add(label22);
            groupBoxGold.Controls.Add(label69);
            groupBoxGold.Controls.Add(label36);
            groupBoxGold.Controls.Add(label38);
            groupBoxGold.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxGold.Location = new Point(3, 3);
            groupBoxGold.Name = "groupBoxGold";
            groupBoxGold.Size = new Size(100, 214);
            groupBoxGold.TabIndex = 14;
            groupBoxGold.TabStop = false;
            groupBoxGold.Text = "Gold";
            // 
            // textBoxGoldJamieson
            // 
            textBoxGoldJamieson.BackColor = Color.FromArgb(64, 64, 64);
            textBoxGoldJamieson.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxGoldJamieson.ForeColor = Color.FromArgb(192, 192, 255);
            textBoxGoldJamieson.Location = new Point(12, 74);
            textBoxGoldJamieson.Name = "textBoxGoldJamieson";
            textBoxGoldJamieson.ReadOnly = true;
            textBoxGoldJamieson.Size = new Size(48, 22);
            textBoxGoldJamieson.TabIndex = 5;
            textBoxGoldJamieson.Text = "0";
            // 
            // textBoxGoldAnderson
            // 
            textBoxGoldAnderson.BackColor = Color.FromArgb(64, 64, 64);
            textBoxGoldAnderson.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxGoldAnderson.ForeColor = Color.FromArgb(192, 192, 255);
            textBoxGoldAnderson.Location = new Point(12, 111);
            textBoxGoldAnderson.Name = "textBoxGoldAnderson";
            textBoxGoldAnderson.ReadOnly = true;
            textBoxGoldAnderson.Size = new Size(48, 22);
            textBoxGoldAnderson.TabIndex = 5;
            textBoxGoldAnderson.Text = "0";
            // 
            // textBoxGoldTsuchiya
            // 
            textBoxGoldTsuchiya.BackColor = Color.FromArgb(64, 64, 64);
            textBoxGoldTsuchiya.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxGoldTsuchiya.ForeColor = Color.FromArgb(192, 192, 255);
            textBoxGoldTsuchiya.Location = new Point(12, 183);
            textBoxGoldTsuchiya.Name = "textBoxGoldTsuchiya";
            textBoxGoldTsuchiya.ReadOnly = true;
            textBoxGoldTsuchiya.Size = new Size(48, 22);
            textBoxGoldTsuchiya.TabIndex = 5;
            textBoxGoldTsuchiya.Text = "0";
            // 
            // label50
            // 
            label50.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label50.Location = new Point(60, 78);
            label50.Name = "label50";
            label50.Size = new Size(38, 16);
            label50.TabIndex = 7;
            label50.Text = "GPa";
            // 
            // textBoxGoldSim
            // 
            textBoxGoldSim.BackColor = Color.FromArgb(64, 64, 64);
            textBoxGoldSim.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxGoldSim.ForeColor = Color.FromArgb(192, 192, 255);
            textBoxGoldSim.Location = new Point(12, 147);
            textBoxGoldSim.Name = "textBoxGoldSim";
            textBoxGoldSim.ReadOnly = true;
            textBoxGoldSim.Size = new Size(48, 22);
            textBoxGoldSim.TabIndex = 5;
            textBoxGoldSim.Text = "0";
            // 
            // label11
            // 
            label11.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label11.Location = new Point(60, 115);
            label11.Name = "label11";
            label11.Size = new Size(38, 16);
            label11.TabIndex = 7;
            label11.Text = "GPa";
            // 
            // label70
            // 
            label70.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label70.Location = new Point(60, 187);
            label70.Name = "label70";
            label70.Size = new Size(38, 16);
            label70.TabIndex = 7;
            label70.Text = "GPa";
            // 
            // label15
            // 
            label15.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label15.Location = new Point(60, 151);
            label15.Name = "label15";
            label15.Size = new Size(38, 16);
            label15.TabIndex = 7;
            label15.Text = "GPa";
            // 
            // textBoxGold_a
            // 
            textBoxGold_a.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxGold_a.Location = new Point(20, 36);
            textBoxGold_a.Name = "textBoxGold_a";
            textBoxGold_a.Size = new Size(48, 21);
            textBoxGold_a.TabIndex = 5;
            textBoxGold_a.Text = "4.0786";
            textBoxGold_a.TextChanged += textBox_TextChanged;
            // 
            // textBoxGold_a0
            // 
            textBoxGold_a0.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxGold_a0.Location = new Point(20, 16);
            textBoxGold_a0.Name = "textBoxGold_a0";
            textBoxGold_a0.Size = new Size(48, 21);
            textBoxGold_a0.TabIndex = 5;
            textBoxGold_a0.Text = "4.0786";
            textBoxGold_a0.TextChanged += textBox_TextChanged;
            // 
            // label16
            // 
            label16.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label16.Location = new Point(4, 20);
            label16.Name = "label16";
            label16.Size = new Size(20, 12);
            label16.TabIndex = 7;
            label16.Text = "a0";
            // 
            // label37
            // 
            label37.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label37.Location = new Point(68, 20);
            label37.Name = "label37";
            label37.Size = new Size(12, 16);
            label37.TabIndex = 7;
            label37.Text = "Å";
            // 
            // label49
            // 
            label49.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label49.Location = new Point(12, 59);
            label49.Name = "label49";
            label49.Size = new Size(88, 19);
            label49.TabIndex = 7;
            label49.Text = "Jamieson (82)";
            // 
            // label23
            // 
            label23.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label23.Location = new Point(68, 40);
            label23.Name = "label23";
            label23.Size = new Size(12, 16);
            label23.TabIndex = 7;
            label23.Text = "Å";
            // 
            // label22
            // 
            label22.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label22.Location = new Point(12, 96);
            label22.Name = "label22";
            label22.Size = new Size(88, 19);
            label22.TabIndex = 7;
            label22.Text = "Anderson (89)";
            // 
            // label69
            // 
            label69.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label69.Location = new Point(12, 171);
            label69.Name = "label69";
            label69.Size = new Size(86, 16);
            label69.TabIndex = 7;
            label69.Text = "Tsuchiya (03)";
            // 
            // label36
            // 
            label36.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label36.Location = new Point(12, 135);
            label36.Name = "label36";
            label36.Size = new Size(86, 16);
            label36.TabIndex = 7;
            label36.Text = "Sim (02)";
            // 
            // label38
            // 
            label38.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label38.Location = new Point(12, 40);
            label38.Name = "label38";
            label38.Size = new Size(16, 12);
            label38.TabIndex = 7;
            label38.Text = "a";
            // 
            // groupBoxPlatinum
            // 
            groupBoxPlatinum.Controls.Add(textBoxPtA0);
            groupBoxPlatinum.Controls.Add(textBoxPtJamieson);
            groupBoxPlatinum.Controls.Add(textBoxPtHolems);
            groupBoxPlatinum.Controls.Add(label39);
            groupBoxPlatinum.Controls.Add(label47);
            groupBoxPlatinum.Controls.Add(label61);
            groupBoxPlatinum.Controls.Add(textBoxPtA);
            groupBoxPlatinum.Controls.Add(label54);
            groupBoxPlatinum.Controls.Add(label48);
            groupBoxPlatinum.Controls.Add(label51);
            groupBoxPlatinum.Controls.Add(label52);
            groupBoxPlatinum.Controls.Add(label60);
            groupBoxPlatinum.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxPlatinum.Location = new Point(109, 3);
            groupBoxPlatinum.Name = "groupBoxPlatinum";
            groupBoxPlatinum.Size = new Size(98, 136);
            groupBoxPlatinum.TabIndex = 13;
            groupBoxPlatinum.TabStop = false;
            groupBoxPlatinum.Text = "Platinum";
            // 
            // textBoxPtA0
            // 
            textBoxPtA0.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxPtA0.Location = new Point(20, 16);
            textBoxPtA0.Name = "textBoxPtA0";
            textBoxPtA0.Size = new Size(48, 21);
            textBoxPtA0.TabIndex = 5;
            textBoxPtA0.Text = "3.9231";
            textBoxPtA0.TextChanged += textBox_TextChanged;
            // 
            // textBoxPtJamieson
            // 
            textBoxPtJamieson.BackColor = Color.FromArgb(64, 64, 64);
            textBoxPtJamieson.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxPtJamieson.ForeColor = Color.FromArgb(192, 192, 255);
            textBoxPtJamieson.Location = new Point(12, 76);
            textBoxPtJamieson.Name = "textBoxPtJamieson";
            textBoxPtJamieson.ReadOnly = true;
            textBoxPtJamieson.Size = new Size(48, 22);
            textBoxPtJamieson.TabIndex = 5;
            textBoxPtJamieson.Text = "0";
            // 
            // textBoxPtHolems
            // 
            textBoxPtHolems.BackColor = Color.FromArgb(64, 64, 64);
            textBoxPtHolems.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxPtHolems.ForeColor = Color.FromArgb(192, 192, 255);
            textBoxPtHolems.Location = new Point(12, 112);
            textBoxPtHolems.Name = "textBoxPtHolems";
            textBoxPtHolems.ReadOnly = true;
            textBoxPtHolems.Size = new Size(48, 22);
            textBoxPtHolems.TabIndex = 5;
            textBoxPtHolems.Text = "0";
            // 
            // label39
            // 
            label39.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label39.Location = new Point(60, 80);
            label39.Name = "label39";
            label39.Size = new Size(24, 16);
            label39.TabIndex = 7;
            label39.Text = "GPa";
            // 
            // label47
            // 
            label47.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label47.Location = new Point(4, 20);
            label47.Name = "label47";
            label47.Size = new Size(24, 12);
            label47.TabIndex = 7;
            label47.Text = "a0";
            // 
            // label61
            // 
            label61.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label61.Location = new Point(60, 116);
            label61.Name = "label61";
            label61.Size = new Size(24, 16);
            label61.TabIndex = 7;
            label61.Text = "GPa";
            // 
            // textBoxPtA
            // 
            textBoxPtA.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxPtA.Location = new Point(20, 40);
            textBoxPtA.Name = "textBoxPtA";
            textBoxPtA.Size = new Size(48, 21);
            textBoxPtA.TabIndex = 5;
            textBoxPtA.Text = "3.9231";
            textBoxPtA.TextChanged += textBox_TextChanged;
            // 
            // label54
            // 
            label54.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label54.Location = new Point(68, 20);
            label54.Name = "label54";
            label54.Size = new Size(12, 16);
            label54.TabIndex = 7;
            label54.Text = "Å";
            // 
            // label48
            // 
            label48.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label48.Location = new Point(68, 44);
            label48.Name = "label48";
            label48.Size = new Size(12, 16);
            label48.TabIndex = 7;
            label48.Text = "Å";
            // 
            // label51
            // 
            label51.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label51.Location = new Point(4, 64);
            label51.Name = "label51";
            label51.Size = new Size(96, 14);
            label51.TabIndex = 7;
            label51.Text = "Jamieson (82)";
            // 
            // label52
            // 
            label52.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label52.Location = new Point(12, 44);
            label52.Name = "label52";
            label52.Size = new Size(36, 12);
            label52.TabIndex = 7;
            label52.Text = "a";
            // 
            // label60
            // 
            label60.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label60.Location = new Point(8, 101);
            label60.Name = "label60";
            label60.Size = new Size(92, 15);
            label60.TabIndex = 7;
            label60.Text = "Holmes (89)";
            // 
            // groupBoxNaClB1
            // 
            groupBoxNaClB1.Controls.Add(textBoxNaClB1_a0);
            groupBoxNaClB1.Controls.Add(textBoxNaClB1_a);
            groupBoxNaClB1.Controls.Add(label53);
            groupBoxNaClB1.Controls.Add(label55);
            groupBoxNaClB1.Controls.Add(label56);
            groupBoxNaClB1.Controls.Add(textBoxNaClB1Brown);
            groupBoxNaClB1.Controls.Add(label62);
            groupBoxNaClB1.Controls.Add(label57);
            groupBoxNaClB1.Controls.Add(label58);
            groupBoxNaClB1.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxNaClB1.Location = new Point(213, 3);
            groupBoxNaClB1.Name = "groupBoxNaClB1";
            groupBoxNaClB1.Size = new Size(98, 100);
            groupBoxNaClB1.TabIndex = 16;
            groupBoxNaClB1.TabStop = false;
            groupBoxNaClB1.Text = "NaCl B1";
            // 
            // textBoxNaClB1_a0
            // 
            textBoxNaClB1_a0.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxNaClB1_a0.Location = new Point(20, 16);
            textBoxNaClB1_a0.Name = "textBoxNaClB1_a0";
            textBoxNaClB1_a0.Size = new Size(48, 21);
            textBoxNaClB1_a0.TabIndex = 5;
            textBoxNaClB1_a0.Text = "5.63900";
            textBoxNaClB1_a0.TextChanged += textBox_TextChanged;
            // 
            // textBoxNaClB1_a
            // 
            textBoxNaClB1_a.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxNaClB1_a.Location = new Point(20, 36);
            textBoxNaClB1_a.Name = "textBoxNaClB1_a";
            textBoxNaClB1_a.Size = new Size(48, 21);
            textBoxNaClB1_a.TabIndex = 5;
            textBoxNaClB1_a.Text = "4.0786";
            textBoxNaClB1_a.TextChanged += textBox_TextChanged;
            // 
            // label53
            // 
            label53.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label53.Location = new Point(4, 20);
            label53.Name = "label53";
            label53.Size = new Size(20, 12);
            label53.TabIndex = 7;
            label53.Text = "a0";
            // 
            // label55
            // 
            label55.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label55.Location = new Point(12, 40);
            label55.Name = "label55";
            label55.Size = new Size(16, 12);
            label55.TabIndex = 7;
            label55.Text = "a";
            // 
            // label56
            // 
            label56.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label56.Location = new Point(60, 76);
            label56.Name = "label56";
            label56.Size = new Size(32, 16);
            label56.TabIndex = 7;
            label56.Text = "GPa";
            // 
            // textBoxNaClB1Brown
            // 
            textBoxNaClB1Brown.BackColor = Color.FromArgb(64, 64, 64);
            textBoxNaClB1Brown.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxNaClB1Brown.ForeColor = Color.FromArgb(192, 192, 255);
            textBoxNaClB1Brown.Location = new Point(12, 72);
            textBoxNaClB1Brown.Name = "textBoxNaClB1Brown";
            textBoxNaClB1Brown.ReadOnly = true;
            textBoxNaClB1Brown.Size = new Size(48, 22);
            textBoxNaClB1Brown.TabIndex = 5;
            textBoxNaClB1Brown.Text = "0";
            // 
            // label62
            // 
            label62.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label62.Location = new Point(68, 20);
            label62.Name = "label62";
            label62.Size = new Size(12, 16);
            label62.TabIndex = 7;
            label62.Text = "Å";
            // 
            // label57
            // 
            label57.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label57.Location = new Point(68, 40);
            label57.Name = "label57";
            label57.Size = new Size(12, 16);
            label57.TabIndex = 7;
            label57.Text = "Å";
            // 
            // label58
            // 
            label58.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label58.Location = new Point(8, 60);
            label58.Name = "label58";
            label58.Size = new Size(76, 16);
            label58.TabIndex = 7;
            label58.Text = "Brown (99)";
            // 
            // groupBoxNaClB2
            // 
            groupBoxNaClB2.Controls.Add(textBoxNaClB2_a0);
            groupBoxNaClB2.Controls.Add(textBoxNaClB2_a);
            groupBoxNaClB2.Controls.Add(textBoxNaClB2SataMgO);
            groupBoxNaClB2.Controls.Add(textBoxNaClB2SataPt);
            groupBoxNaClB2.Controls.Add(label65);
            groupBoxNaClB2.Controls.Add(label67);
            groupBoxNaClB2.Controls.Add(label59);
            groupBoxNaClB2.Controls.Add(label63);
            groupBoxNaClB2.Controls.Add(label64);
            groupBoxNaClB2.Controls.Add(label66);
            groupBoxNaClB2.Controls.Add(label68);
            groupBoxNaClB2.Controls.Add(label71);
            groupBoxNaClB2.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxNaClB2.Location = new Point(317, 3);
            groupBoxNaClB2.Name = "groupBoxNaClB2";
            groupBoxNaClB2.Size = new Size(100, 128);
            groupBoxNaClB2.TabIndex = 15;
            groupBoxNaClB2.TabStop = false;
            groupBoxNaClB2.Text = "NaCl B2";
            // 
            // textBoxNaClB2_a0
            // 
            textBoxNaClB2_a0.Enabled = false;
            textBoxNaClB2_a0.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxNaClB2_a0.Location = new Point(20, 16);
            textBoxNaClB2_a0.Name = "textBoxNaClB2_a0";
            textBoxNaClB2_a0.Size = new Size(48, 21);
            textBoxNaClB2_a0.TabIndex = 5;
            textBoxNaClB2_a0.Text = "0";
            textBoxNaClB2_a0.TextChanged += textBox_TextChanged;
            // 
            // textBoxNaClB2_a
            // 
            textBoxNaClB2_a.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxNaClB2_a.Location = new Point(20, 36);
            textBoxNaClB2_a.Name = "textBoxNaClB2_a";
            textBoxNaClB2_a.Size = new Size(48, 21);
            textBoxNaClB2_a.TabIndex = 5;
            textBoxNaClB2_a.Text = "4.0786";
            textBoxNaClB2_a.TextChanged += textBox_TextChanged;
            // 
            // textBoxNaClB2SataMgO
            // 
            textBoxNaClB2SataMgO.BackColor = Color.FromArgb(64, 64, 64);
            textBoxNaClB2SataMgO.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxNaClB2SataMgO.ForeColor = Color.FromArgb(192, 192, 255);
            textBoxNaClB2SataMgO.Location = new Point(12, 104);
            textBoxNaClB2SataMgO.Name = "textBoxNaClB2SataMgO";
            textBoxNaClB2SataMgO.ReadOnly = true;
            textBoxNaClB2SataMgO.Size = new Size(48, 22);
            textBoxNaClB2SataMgO.TabIndex = 5;
            textBoxNaClB2SataMgO.Text = "0";
            // 
            // textBoxNaClB2SataPt
            // 
            textBoxNaClB2SataPt.BackColor = Color.FromArgb(64, 64, 64);
            textBoxNaClB2SataPt.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxNaClB2SataPt.ForeColor = Color.FromArgb(192, 192, 255);
            textBoxNaClB2SataPt.Location = new Point(12, 68);
            textBoxNaClB2SataPt.Name = "textBoxNaClB2SataPt";
            textBoxNaClB2SataPt.ReadOnly = true;
            textBoxNaClB2SataPt.Size = new Size(48, 22);
            textBoxNaClB2SataPt.TabIndex = 5;
            textBoxNaClB2SataPt.Text = "0";
            // 
            // label65
            // 
            label65.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label65.Location = new Point(4, 20);
            label65.Name = "label65";
            label65.Size = new Size(32, 12);
            label65.TabIndex = 7;
            label65.Text = "a0";
            // 
            // label67
            // 
            label67.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label67.Location = new Point(60, 108);
            label67.Name = "label67";
            label67.Size = new Size(40, 16);
            label67.TabIndex = 7;
            label67.Text = "GPa";
            // 
            // label59
            // 
            label59.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label59.Location = new Point(60, 72);
            label59.Name = "label59";
            label59.Size = new Size(40, 16);
            label59.TabIndex = 7;
            label59.Text = "GPa";
            // 
            // label63
            // 
            label63.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label63.Location = new Point(12, 40);
            label63.Name = "label63";
            label63.Size = new Size(32, 12);
            label63.TabIndex = 7;
            label63.Text = "a";
            // 
            // label64
            // 
            label64.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label64.Location = new Point(68, 20);
            label64.Name = "label64";
            label64.Size = new Size(12, 16);
            label64.TabIndex = 7;
            label64.Text = "Å";
            // 
            // label66
            // 
            label66.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label66.Location = new Point(68, 40);
            label66.Name = "label66";
            label66.Size = new Size(12, 16);
            label66.TabIndex = 7;
            label66.Text = "Å";
            // 
            // label68
            // 
            label68.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label68.Location = new Point(4, 92);
            label68.Name = "label68";
            label68.Size = new Size(76, 16);
            label68.TabIndex = 7;
            label68.Text = "Sata (02) (MgO)";
            // 
            // label71
            // 
            label71.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label71.Location = new Point(4, 56);
            label71.Name = "label71";
            label71.Size = new Size(76, 12);
            label71.TabIndex = 7;
            label71.Text = "Sata (02) (Pt)";
            // 
            // groupBoxPericlase
            // 
            groupBoxPericlase.Controls.Add(label72);
            groupBoxPericlase.Controls.Add(label73);
            groupBoxPericlase.Controls.Add(textBoxMgOAizawa);
            groupBoxPericlase.Controls.Add(label74);
            groupBoxPericlase.Controls.Add(textBoxMgODewaele);
            groupBoxPericlase.Controls.Add(textBoxMgOJacson);
            groupBoxPericlase.Controls.Add(textBoxMgOA0);
            groupBoxPericlase.Controls.Add(label75);
            groupBoxPericlase.Controls.Add(label76);
            groupBoxPericlase.Controls.Add(label77);
            groupBoxPericlase.Controls.Add(label78);
            groupBoxPericlase.Controls.Add(textBoxMgOA);
            groupBoxPericlase.Controls.Add(label79);
            groupBoxPericlase.Controls.Add(label80);
            groupBoxPericlase.Controls.Add(label81);
            groupBoxPericlase.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxPericlase.Location = new Point(423, 3);
            groupBoxPericlase.Name = "groupBoxPericlase";
            groupBoxPericlase.Size = new Size(100, 172);
            groupBoxPericlase.TabIndex = 10;
            groupBoxPericlase.TabStop = false;
            groupBoxPericlase.Text = "Periclase";
            // 
            // label72
            // 
            label72.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label72.Location = new Point(60, 148);
            label72.Name = "label72";
            label72.Size = new Size(40, 16);
            label72.TabIndex = 7;
            label72.Text = "GPa";
            // 
            // label73
            // 
            label73.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label73.Location = new Point(60, 112);
            label73.Name = "label73";
            label73.Size = new Size(40, 16);
            label73.TabIndex = 7;
            label73.Text = "GPa";
            // 
            // textBoxMgOAizawa
            // 
            textBoxMgOAizawa.BackColor = Color.FromArgb(64, 64, 64);
            textBoxMgOAizawa.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxMgOAizawa.ForeColor = Color.FromArgb(192, 192, 255);
            textBoxMgOAizawa.Location = new Point(12, 144);
            textBoxMgOAizawa.Name = "textBoxMgOAizawa";
            textBoxMgOAizawa.ReadOnly = true;
            textBoxMgOAizawa.Size = new Size(48, 22);
            textBoxMgOAizawa.TabIndex = 5;
            textBoxMgOAizawa.Text = "0";
            // 
            // label74
            // 
            label74.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label74.Location = new Point(60, 76);
            label74.Name = "label74";
            label74.Size = new Size(40, 16);
            label74.TabIndex = 7;
            label74.Text = "GPa";
            // 
            // textBoxMgODewaele
            // 
            textBoxMgODewaele.BackColor = Color.FromArgb(64, 64, 64);
            textBoxMgODewaele.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxMgODewaele.ForeColor = Color.FromArgb(192, 192, 255);
            textBoxMgODewaele.Location = new Point(12, 108);
            textBoxMgODewaele.Name = "textBoxMgODewaele";
            textBoxMgODewaele.ReadOnly = true;
            textBoxMgODewaele.Size = new Size(48, 22);
            textBoxMgODewaele.TabIndex = 5;
            textBoxMgODewaele.Text = "0";
            // 
            // textBoxMgOJacson
            // 
            textBoxMgOJacson.BackColor = Color.FromArgb(64, 64, 64);
            textBoxMgOJacson.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxMgOJacson.ForeColor = Color.FromArgb(192, 192, 255);
            textBoxMgOJacson.Location = new Point(12, 72);
            textBoxMgOJacson.Name = "textBoxMgOJacson";
            textBoxMgOJacson.ReadOnly = true;
            textBoxMgOJacson.Size = new Size(48, 22);
            textBoxMgOJacson.TabIndex = 5;
            textBoxMgOJacson.Text = "0";
            // 
            // textBoxMgOA0
            // 
            textBoxMgOA0.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxMgOA0.Location = new Point(20, 16);
            textBoxMgOA0.Name = "textBoxMgOA0";
            textBoxMgOA0.Size = new Size(48, 21);
            textBoxMgOA0.TabIndex = 5;
            textBoxMgOA0.Text = "4.2112";
            textBoxMgOA0.TextChanged += textBox_TextChanged;
            // 
            // label75
            // 
            label75.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label75.Location = new Point(8, 132);
            label75.Name = "label75";
            label75.Size = new Size(84, 12);
            label75.TabIndex = 7;
            label75.Text = "Aizawa (06)";
            // 
            // label76
            // 
            label76.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label76.Location = new Point(4, 96);
            label76.Name = "label76";
            label76.Size = new Size(88, 12);
            label76.TabIndex = 7;
            label76.Text = "Dewaele (00)";
            // 
            // label77
            // 
            label77.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label77.Location = new Point(4, 60);
            label77.Name = "label77";
            label77.Size = new Size(92, 16);
            label77.TabIndex = 7;
            label77.Text = "Jackson (98)";
            // 
            // label78
            // 
            label78.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label78.Location = new Point(4, 20);
            label78.Name = "label78";
            label78.Size = new Size(20, 12);
            label78.TabIndex = 7;
            label78.Text = "a0";
            // 
            // textBoxMgOA
            // 
            textBoxMgOA.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxMgOA.Location = new Point(20, 36);
            textBoxMgOA.Name = "textBoxMgOA";
            textBoxMgOA.Size = new Size(48, 21);
            textBoxMgOA.TabIndex = 5;
            textBoxMgOA.Text = "4.2112";
            textBoxMgOA.TextChanged += textBox_TextChanged;
            // 
            // label79
            // 
            label79.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label79.Location = new Point(68, 20);
            label79.Name = "label79";
            label79.Size = new Size(12, 16);
            label79.TabIndex = 7;
            label79.Text = "Å";
            // 
            // label80
            // 
            label80.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label80.Location = new Point(4, 40);
            label80.Name = "label80";
            label80.Size = new Size(16, 12);
            label80.TabIndex = 7;
            label80.Text = "a";
            // 
            // label81
            // 
            label81.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label81.Location = new Point(68, 40);
            label81.Name = "label81";
            label81.Size = new Size(12, 16);
            label81.TabIndex = 7;
            label81.Text = "Å";
            // 
            // groupBoxCorundum
            // 
            groupBoxCorundum.Controls.Add(label82);
            groupBoxCorundum.Controls.Add(textBoxCorundumDubrovinsky);
            groupBoxCorundum.Controls.Add(textBoxColundumV0);
            groupBoxCorundum.Controls.Add(label83);
            groupBoxCorundum.Controls.Add(label84);
            groupBoxCorundum.Controls.Add(textBoxCorundumV);
            groupBoxCorundum.Controls.Add(label85);
            groupBoxCorundum.Controls.Add(label86);
            groupBoxCorundum.Controls.Add(label87);
            groupBoxCorundum.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxCorundum.Location = new Point(529, 3);
            groupBoxCorundum.Name = "groupBoxCorundum";
            groupBoxCorundum.Size = new Size(100, 101);
            groupBoxCorundum.TabIndex = 9;
            groupBoxCorundum.TabStop = false;
            groupBoxCorundum.Text = "Corundum";
            // 
            // label82
            // 
            label82.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label82.Location = new Point(56, 76);
            label82.Name = "label82";
            label82.Size = new Size(40, 16);
            label82.TabIndex = 7;
            label82.Text = "GPa";
            // 
            // textBoxCorundumDubrovinsky
            // 
            textBoxCorundumDubrovinsky.BackColor = Color.FromArgb(64, 64, 64);
            textBoxCorundumDubrovinsky.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxCorundumDubrovinsky.ForeColor = Color.FromArgb(192, 192, 255);
            textBoxCorundumDubrovinsky.Location = new Point(8, 72);
            textBoxCorundumDubrovinsky.Name = "textBoxCorundumDubrovinsky";
            textBoxCorundumDubrovinsky.ReadOnly = true;
            textBoxCorundumDubrovinsky.Size = new Size(48, 22);
            textBoxCorundumDubrovinsky.TabIndex = 5;
            textBoxCorundumDubrovinsky.Text = "0";
            // 
            // textBoxColundumV0
            // 
            textBoxColundumV0.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxColundumV0.Location = new Point(20, 16);
            textBoxColundumV0.Name = "textBoxColundumV0";
            textBoxColundumV0.Size = new Size(36, 21);
            textBoxColundumV0.TabIndex = 5;
            textBoxColundumV0.Text = "254.6959";
            textBoxColundumV0.TextChanged += textBox_TextChanged;
            // 
            // label83
            // 
            label83.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label83.Location = new Point(4, 56);
            label83.Name = "label83";
            label83.Size = new Size(105, 20);
            label83.TabIndex = 7;
            label83.Text = "Dubrovinsky (98)";
            // 
            // label84
            // 
            label84.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label84.Location = new Point(4, 20);
            label84.Name = "label84";
            label84.Size = new Size(20, 12);
            label84.TabIndex = 7;
            label84.Text = "V0";
            // 
            // textBoxCorundumV
            // 
            textBoxCorundumV.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxCorundumV.Location = new Point(20, 36);
            textBoxCorundumV.Name = "textBoxCorundumV";
            textBoxCorundumV.Size = new Size(36, 21);
            textBoxCorundumV.TabIndex = 5;
            textBoxCorundumV.Text = "254.6959";
            textBoxCorundumV.TextChanged += textBox_TextChanged;
            // 
            // label85
            // 
            label85.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label85.Location = new Point(56, 20);
            label85.Name = "label85";
            label85.Size = new Size(38, 16);
            label85.TabIndex = 7;
            label85.Text = "Å^3";
            // 
            // label86
            // 
            label86.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label86.Location = new Point(4, 40);
            label86.Name = "label86";
            label86.Size = new Size(16, 12);
            label86.TabIndex = 7;
            label86.Text = "V";
            // 
            // label87
            // 
            label87.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label87.Location = new Point(56, 40);
            label87.Name = "label87";
            label87.Size = new Size(40, 16);
            label87.TabIndex = 7;
            label87.Text = "Å^3";
            // 
            // groupBoxAr
            // 
            groupBoxAr.Controls.Add(textBoxArA0);
            groupBoxAr.Controls.Add(textBoxArA);
            groupBoxAr.Controls.Add(textBoxArJephcoat);
            groupBoxAr.Controls.Add(label88);
            groupBoxAr.Controls.Add(textBoxArRoss);
            groupBoxAr.Controls.Add(label89);
            groupBoxAr.Controls.Add(label90);
            groupBoxAr.Controls.Add(label91);
            groupBoxAr.Controls.Add(label92);
            groupBoxAr.Controls.Add(label93);
            groupBoxAr.Controls.Add(label94);
            groupBoxAr.Controls.Add(label95);
            groupBoxAr.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxAr.Location = new Point(3, 223);
            groupBoxAr.Name = "groupBoxAr";
            groupBoxAr.Size = new Size(104, 132);
            groupBoxAr.TabIndex = 12;
            groupBoxAr.TabStop = false;
            groupBoxAr.Text = "Ar";
            // 
            // textBoxArA0
            // 
            textBoxArA0.Enabled = false;
            textBoxArA0.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxArA0.Location = new Point(20, 15);
            textBoxArA0.Name = "textBoxArA0";
            textBoxArA0.Size = new Size(48, 21);
            textBoxArA0.TabIndex = 5;
            textBoxArA0.Text = "0";
            textBoxArA0.TextChanged += textBox_TextChanged;
            // 
            // textBoxArA
            // 
            textBoxArA.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxArA.Location = new Point(20, 36);
            textBoxArA.Name = "textBoxArA";
            textBoxArA.Size = new Size(48, 21);
            textBoxArA.TabIndex = 5;
            textBoxArA.Text = "4.0786";
            textBoxArA.TextChanged += textBox_TextChanged;
            // 
            // textBoxArJephcoat
            // 
            textBoxArJephcoat.BackColor = Color.FromArgb(64, 64, 64);
            textBoxArJephcoat.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxArJephcoat.ForeColor = Color.FromArgb(192, 192, 255);
            textBoxArJephcoat.Location = new Point(12, 106);
            textBoxArJephcoat.Name = "textBoxArJephcoat";
            textBoxArJephcoat.ReadOnly = true;
            textBoxArJephcoat.Size = new Size(48, 22);
            textBoxArJephcoat.TabIndex = 5;
            textBoxArJephcoat.Text = "0";
            // 
            // label88
            // 
            label88.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label88.Location = new Point(60, 110);
            label88.Name = "label88";
            label88.Size = new Size(38, 16);
            label88.TabIndex = 7;
            label88.Text = "GPa";
            // 
            // textBoxArRoss
            // 
            textBoxArRoss.BackColor = Color.FromArgb(64, 64, 64);
            textBoxArRoss.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxArRoss.ForeColor = Color.FromArgb(192, 192, 255);
            textBoxArRoss.Location = new Point(12, 70);
            textBoxArRoss.Name = "textBoxArRoss";
            textBoxArRoss.ReadOnly = true;
            textBoxArRoss.Size = new Size(48, 22);
            textBoxArRoss.TabIndex = 5;
            textBoxArRoss.Text = "0";
            // 
            // label89
            // 
            label89.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label89.Location = new Point(60, 74);
            label89.Name = "label89";
            label89.Size = new Size(38, 16);
            label89.TabIndex = 7;
            label89.Text = "GPa";
            // 
            // label90
            // 
            label90.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label90.Location = new Point(4, 19);
            label90.Name = "label90";
            label90.Size = new Size(32, 12);
            label90.TabIndex = 7;
            label90.Text = "a0";
            // 
            // label91
            // 
            label91.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label91.Location = new Point(12, 40);
            label91.Name = "label91";
            label91.Size = new Size(32, 12);
            label91.TabIndex = 7;
            label91.Text = "a";
            // 
            // label92
            // 
            label92.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label92.Location = new Point(4, 94);
            label92.Name = "label92";
            label92.Size = new Size(94, 12);
            label92.TabIndex = 7;
            label92.Text = "Jephcoat (98)";
            // 
            // label93
            // 
            label93.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label93.Location = new Point(68, 40);
            label93.Name = "label93";
            label93.Size = new Size(12, 16);
            label93.TabIndex = 7;
            label93.Text = "Å";
            // 
            // label94
            // 
            label94.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label94.Location = new Point(4, 58);
            label94.Name = "label94";
            label94.Size = new Size(94, 12);
            label94.TabIndex = 7;
            label94.Text = "Ross et al. (86)";
            // 
            // label95
            // 
            label95.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label95.Location = new Point(68, 19);
            label95.Name = "label95";
            label95.Size = new Size(12, 16);
            label95.TabIndex = 7;
            label95.Text = "Å";
            // 
            // groupBoxRe
            // 
            groupBoxRe.Controls.Add(label96);
            groupBoxRe.Controls.Add(textBoxReZha);
            groupBoxRe.Controls.Add(textBoxReV0);
            groupBoxRe.Controls.Add(label97);
            groupBoxRe.Controls.Add(label98);
            groupBoxRe.Controls.Add(textBoxReV);
            groupBoxRe.Controls.Add(label99);
            groupBoxRe.Controls.Add(label100);
            groupBoxRe.Controls.Add(label101);
            groupBoxRe.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            groupBoxRe.Location = new Point(113, 223);
            groupBoxRe.Name = "groupBoxRe";
            groupBoxRe.Size = new Size(104, 101);
            groupBoxRe.TabIndex = 11;
            groupBoxRe.TabStop = false;
            groupBoxRe.Text = "Re";
            // 
            // label96
            // 
            label96.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label96.Location = new Point(56, 76);
            label96.Name = "label96";
            label96.Size = new Size(24, 16);
            label96.TabIndex = 7;
            label96.Text = "GPa";
            // 
            // textBoxReZha
            // 
            textBoxReZha.BackColor = Color.FromArgb(64, 64, 64);
            textBoxReZha.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point);
            textBoxReZha.ForeColor = Color.FromArgb(192, 192, 255);
            textBoxReZha.Location = new Point(8, 72);
            textBoxReZha.Name = "textBoxReZha";
            textBoxReZha.ReadOnly = true;
            textBoxReZha.Size = new Size(48, 22);
            textBoxReZha.TabIndex = 5;
            textBoxReZha.Text = "0";
            // 
            // textBoxReV0
            // 
            textBoxReV0.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxReV0.Location = new Point(20, 16);
            textBoxReV0.Name = "textBoxReV0";
            textBoxReV0.Size = new Size(36, 21);
            textBoxReV0.TabIndex = 5;
            textBoxReV0.Text = "29.42795";
            textBoxReV0.TextChanged += textBox_TextChanged;
            // 
            // label97
            // 
            label97.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label97.Location = new Point(4, 56);
            label97.Name = "label97";
            label97.Size = new Size(100, 20);
            label97.TabIndex = 7;
            label97.Text = "Zha et al. (04)";
            // 
            // label98
            // 
            label98.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label98.Location = new Point(4, 20);
            label98.Name = "label98";
            label98.Size = new Size(20, 12);
            label98.TabIndex = 7;
            label98.Text = "V0";
            // 
            // textBoxReV
            // 
            textBoxReV.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxReV.Location = new Point(20, 36);
            textBoxReV.Name = "textBoxReV";
            textBoxReV.Size = new Size(36, 21);
            textBoxReV.TabIndex = 5;
            textBoxReV.Text = "254.6959";
            textBoxReV.TextChanged += textBox_TextChanged;
            // 
            // label99
            // 
            label99.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label99.Location = new Point(56, 20);
            label99.Name = "label99";
            label99.Size = new Size(42, 16);
            label99.TabIndex = 7;
            label99.Text = "Å^3";
            // 
            // label100
            // 
            label100.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label100.Location = new Point(4, 40);
            label100.Name = "label100";
            label100.Size = new Size(16, 12);
            label100.TabIndex = 7;
            label100.Text = "V";
            // 
            // label101
            // 
            label101.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            label101.Location = new Point(56, 40);
            label101.Name = "label101";
            label101.Size = new Size(42, 16);
            label101.TabIndex = 7;
            label101.Text = "Å^3";
            // 
            // panelEOS
            // 
            panelEOS.Controls.Add(flowLayoutPanelEOS);
            panelEOS.Controls.Add(panel2);
            panelEOS.Dock = DockStyle.Top;
            panelEOS.Location = new Point(0, 48);
            panelEOS.Name = "panelEOS";
            panelEOS.Size = new Size(725, 82);
            panelEOS.TabIndex = 15;
            // 
            // panel2
            // 
            panel2.Controls.Add(label103);
            panel2.Controls.Add(textBoxT);
            panel2.Controls.Add(label102);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(725, 26);
            panel2.TabIndex = 19;
            // 
            // label103
            // 
            label103.AutoSize = true;
            label103.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label103.Location = new Point(7, 5);
            label103.Name = "label103";
            label103.Size = new Size(87, 14);
            label103.TabIndex = 18;
            label103.Text = "Temperature";
            // 
            // textBoxT
            // 
            textBoxT.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            textBoxT.Location = new Point(94, 2);
            textBoxT.Name = "textBoxT";
            textBoxT.Size = new Size(83, 22);
            textBoxT.TabIndex = 17;
            textBoxT.Text = "300";
            textBoxT.TextChanged += textBox_TextChanged;
            // 
            // label102
            // 
            label102.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label102.Location = new Point(183, 5);
            label102.Name = "label102";
            label102.Size = new Size(16, 16);
            label102.TabIndex = 18;
            label102.Text = "K";
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
            // label25
            // 
            label25.AutoSize = true;
            label25.BackColor = SystemColors.Control;
            label25.Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label25.Location = new Point(3, 6);
            label25.Margin = new Padding(3, 6, 3, 0);
            label25.Name = "label25";
            label25.Size = new Size(123, 14);
            label25.TabIndex = 3;
            label25.Text = "Fitting Information";
            // 
            // FormMain
            // 
            AllowDrop = true;
            AutoScaleBaseSize = new Size(7, 15);
            ClientSize = new Size(725, 794);
            Controls.Add(splitContainer1);
            Controls.Add(groupBoxAkahama2006);
            Controls.Add(groupBoxMao);
            Controls.Add(panelEOS);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(menuStrip1);
            Font = new Font("Verdana", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            panelEOS.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
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
    }



}
