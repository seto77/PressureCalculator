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

namespace PressureCalculator{
	
	
	/// <summary>
	/// Form1 の概要の説明です。
	/// </summary>
	partial class FormMain : System.Windows.Forms.Form
    {

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
        public Profile Original,BottomProfile;
		public Profile OriginalSmooth,BottomProfileSmooth;
		public double UppestKayser,UpperKayser,LowestKayser,LowerKayser,UppestCount,UpperCount,LowestCount,LowerCount,UpperDiff,LowerDiff,UppestDiff,LowestDiff;
		public Bitmap BmpOriginal,BmpDifferentiation;
		public Graphics gOriginal,gDifferentiation;
		public bool MouseRange=true;
		
     
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numericUpDownDifferentiationRunningAverage;
        private System.Windows.Forms.NumericUpDown numericUpDownOriginalRunningAverage;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox textBoxAkahama2004A;
		private System.Windows.Forms.TextBox textBoxAkahama2004B;
		private System.Windows.Forms.TextBox textBoxAkahama2004C;
        private System.Windows.Forms.TextBox textBoxDiamondAkahama2004P;
		private System.Windows.Forms.NumericUpDown numericUpDownOriginalGaussian;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown numericUpDownDifferentiationGaussian;
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
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.numericUpDownDifferentiationRunningAverage = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownOriginalRunningAverage = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAkahama2004A = new System.Windows.Forms.TextBox();
            this.textBoxAkahama2004B = new System.Windows.Forms.TextBox();
            this.textBoxAkahama2004C = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxDiamondAkahama2004P = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.numericUpDownOriginalGaussian = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.labelBottomTitle = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownDifferentiationGaussian = new System.Windows.Forms.NumericUpDown();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.graphControlTop = new Crystallography.Controls.GraphControl();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.graphControlBottom = new Crystallography.Controls.GraphControl();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDownFitRange = new System.Windows.Forms.NumericUpDown();
            this.labelDimension = new System.Windows.Forms.Label();
            this.radioButtonDiamondRaman = new System.Windows.Forms.RadioButton();
            this.radioButtonRubyFluorescence = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.radioButtonEOS = new System.Windows.Forms.RadioButton();
            this.textBoxMaoHydroP = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.groupBoxMao = new System.Windows.Forms.GroupBox();
            this.radioButtonTempUnitK = new System.Windows.Forms.RadioButton();
            this.radioButtonTempUnitC = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxMaoP = new System.Windows.Forms.TextBox();
            this.numericBoxMaoA = new Crystallography.Controls.NumericBox();
            this.numericBoxMaoQuasiA = new Crystallography.Controls.NumericBox();
            this.numericBoxShenA = new Crystallography.Controls.NumericBox();
            this.numericBoxMaoHydroA = new Crystallography.Controls.NumericBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxMaoQuasiP = new System.Windows.Forms.TextBox();
            this.label107 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.textBoxShenP = new System.Windows.Forms.TextBox();
            this.label104 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericBoxRubyRagan = new Crystallography.Controls.NumericBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxRubyR1_0CalculatedFromRagan = new System.Windows.Forms.CheckBox();
            this.numericBoxRubyT = new Crystallography.Controls.NumericBox();
            this.checkBoxRubyTemeratureSameAsRef = new System.Windows.Forms.CheckBox();
            this.numericBoxRubyR1_0 = new Crystallography.Controls.NumericBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonRubyRefR1Set = new System.Windows.Forms.Button();
            this.numericBoxRubyRefT = new Crystallography.Controls.NumericBox();
            this.numericBoxRubyRefR1 = new Crystallography.Controls.NumericBox();
            this.numericBoxRubyR1 = new Crystallography.Controls.NumericBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBoxAkahama2006 = new System.Windows.Forms.GroupBox();
            this.textBoxDiamondFratandunoHigh = new System.Windows.Forms.TextBox();
            this.textBoxDiamondAkahama2006P = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.textBoxDiamondRamanNu0 = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.textBoxDiamondRamanNu = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.textBoxAkahama2006K0 = new System.Windows.Forms.TextBox();
            this.textBoxAkahama2006K0Prime = new System.Windows.Forms.TextBox();
            this.textBoxDiamondFratandunoLow = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.flowLayoutPanelEOS = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBoxGold = new System.Windows.Forms.GroupBox();
            this.textBoxGoldJamieson = new System.Windows.Forms.TextBox();
            this.textBoxGoldAnderson = new System.Windows.Forms.TextBox();
            this.textBoxGoldTsuchiya = new System.Windows.Forms.TextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.textBoxGoldSim = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label70 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxGold_a = new System.Windows.Forms.TextBox();
            this.textBoxGold_a0 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.groupBoxPlatinum = new System.Windows.Forms.GroupBox();
            this.textBoxPtA0 = new System.Windows.Forms.TextBox();
            this.textBoxPtJamieson = new System.Windows.Forms.TextBox();
            this.textBoxPtHolems = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label61 = new System.Windows.Forms.Label();
            this.textBoxPtA = new System.Windows.Forms.TextBox();
            this.label54 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.groupBoxNaClB1 = new System.Windows.Forms.GroupBox();
            this.textBoxNaClB1_a0 = new System.Windows.Forms.TextBox();
            this.textBoxNaClB1_a = new System.Windows.Forms.TextBox();
            this.label53 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.textBoxNaClB1Brown = new System.Windows.Forms.TextBox();
            this.label62 = new System.Windows.Forms.Label();
            this.label57 = new System.Windows.Forms.Label();
            this.label58 = new System.Windows.Forms.Label();
            this.groupBoxNaClB2 = new System.Windows.Forms.GroupBox();
            this.textBoxNaClB2_a0 = new System.Windows.Forms.TextBox();
            this.textBoxNaClB2_a = new System.Windows.Forms.TextBox();
            this.textBoxNaClB2SataMgO = new System.Windows.Forms.TextBox();
            this.textBoxNaClB2SataPt = new System.Windows.Forms.TextBox();
            this.label65 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label59 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label64 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.groupBoxPericlase = new System.Windows.Forms.GroupBox();
            this.label72 = new System.Windows.Forms.Label();
            this.label73 = new System.Windows.Forms.Label();
            this.textBoxMgOAizawa = new System.Windows.Forms.TextBox();
            this.label74 = new System.Windows.Forms.Label();
            this.textBoxMgODewaele = new System.Windows.Forms.TextBox();
            this.textBoxMgOJacson = new System.Windows.Forms.TextBox();
            this.textBoxMgOA0 = new System.Windows.Forms.TextBox();
            this.label75 = new System.Windows.Forms.Label();
            this.label76 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.textBoxMgOA = new System.Windows.Forms.TextBox();
            this.label79 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.groupBoxCorundum = new System.Windows.Forms.GroupBox();
            this.label82 = new System.Windows.Forms.Label();
            this.textBoxCorundumDubrovinsky = new System.Windows.Forms.TextBox();
            this.textBoxColundumV0 = new System.Windows.Forms.TextBox();
            this.label83 = new System.Windows.Forms.Label();
            this.label84 = new System.Windows.Forms.Label();
            this.textBoxCorundumV = new System.Windows.Forms.TextBox();
            this.label85 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label87 = new System.Windows.Forms.Label();
            this.groupBoxAr = new System.Windows.Forms.GroupBox();
            this.textBoxArA0 = new System.Windows.Forms.TextBox();
            this.textBoxArA = new System.Windows.Forms.TextBox();
            this.textBoxArJephcoat = new System.Windows.Forms.TextBox();
            this.label88 = new System.Windows.Forms.Label();
            this.textBoxArRoss = new System.Windows.Forms.TextBox();
            this.label89 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.label92 = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.groupBoxRe = new System.Windows.Forms.GroupBox();
            this.label96 = new System.Windows.Forms.Label();
            this.textBoxReZha = new System.Windows.Forms.TextBox();
            this.textBoxReV0 = new System.Windows.Forms.TextBox();
            this.label97 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.textBoxReV = new System.Windows.Forms.TextBox();
            this.label99 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.panelEOS = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label103 = new System.Windows.Forms.Label();
            this.textBoxT = new System.Windows.Forms.TextBox();
            this.label102 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAsCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.watchNewFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDifferentiationRunningAverage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOriginalRunningAverage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOriginalGaussian)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDifferentiationGaussian)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFitRange)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBoxMao.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxAkahama2006.SuspendLayout();
            this.flowLayoutPanelEOS.SuspendLayout();
            this.groupBoxGold.SuspendLayout();
            this.groupBoxPlatinum.SuspendLayout();
            this.groupBoxNaClB1.SuspendLayout();
            this.groupBoxNaClB2.SuspendLayout();
            this.groupBoxPericlase.SuspendLayout();
            this.groupBoxCorundum.SuspendLayout();
            this.groupBoxAr.SuspendLayout();
            this.groupBoxRe.SuspendLayout();
            this.panelEOS.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // numericUpDownDifferentiationRunningAverage
            // 
            this.numericUpDownDifferentiationRunningAverage.AutoSize = true;
            this.numericUpDownDifferentiationRunningAverage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDownDifferentiationRunningAverage.Location = new System.Drawing.Point(256, 3);
            this.numericUpDownDifferentiationRunningAverage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDifferentiationRunningAverage.Name = "numericUpDownDifferentiationRunningAverage";
            this.numericUpDownDifferentiationRunningAverage.ReadOnly = true;
            this.numericUpDownDifferentiationRunningAverage.Size = new System.Drawing.Size(47, 22);
            this.numericUpDownDifferentiationRunningAverage.TabIndex = 2;
            this.numericUpDownDifferentiationRunningAverage.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownDifferentiationRunningAverage.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDownDifferentiationRunningAverage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.numericUpDownDifferentiationRunningAverage_MouseDown);
            // 
            // numericUpDownOriginalRunningAverage
            // 
            this.numericUpDownOriginalRunningAverage.AutoSize = true;
            this.numericUpDownOriginalRunningAverage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDownOriginalRunningAverage.Location = new System.Drawing.Point(246, 3);
            this.numericUpDownOriginalRunningAverage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownOriginalRunningAverage.Name = "numericUpDownOriginalRunningAverage";
            this.numericUpDownOriginalRunningAverage.ReadOnly = true;
            this.numericUpDownOriginalRunningAverage.Size = new System.Drawing.Size(47, 22);
            this.numericUpDownOriginalRunningAverage.TabIndex = 3;
            this.numericUpDownOriginalRunningAverage.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownOriginalRunningAverage.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDownOriginalRunningAverage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.numericUpDownOriginalRunningAverage_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(126, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Running Average";
            // 
            // textBoxAkahama2004A
            // 
            this.textBoxAkahama2004A.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxAkahama2004A.Location = new System.Drawing.Point(293, 16);
            this.textBoxAkahama2004A.Name = "textBoxAkahama2004A";
            this.textBoxAkahama2004A.Size = new System.Drawing.Size(44, 22);
            this.textBoxAkahama2004A.TabIndex = 0;
            this.textBoxAkahama2004A.Text = "66.9";
            this.textBoxAkahama2004A.TextChanged += new System.EventHandler(this.textBoxNu_TextChanged);
            // 
            // textBoxAkahama2004B
            // 
            this.textBoxAkahama2004B.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxAkahama2004B.Location = new System.Drawing.Point(355, 16);
            this.textBoxAkahama2004B.Name = "textBoxAkahama2004B";
            this.textBoxAkahama2004B.Size = new System.Drawing.Size(44, 22);
            this.textBoxAkahama2004B.TabIndex = 0;
            this.textBoxAkahama2004B.Text = "-0.5281";
            this.textBoxAkahama2004B.TextChanged += new System.EventHandler(this.textBoxNu_TextChanged);
            // 
            // textBoxAkahama2004C
            // 
            this.textBoxAkahama2004C.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxAkahama2004C.Location = new System.Drawing.Point(427, 16);
            this.textBoxAkahama2004C.Name = "textBoxAkahama2004C";
            this.textBoxAkahama2004C.Size = new System.Drawing.Size(44, 22);
            this.textBoxAkahama2004C.TabIndex = 0;
            this.textBoxAkahama2004C.Text = "3.585";
            this.textBoxAkahama2004C.TextChanged += new System.EventHandler(this.textBoxNu_TextChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(264, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "P =";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(337, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "+";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(399, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "ν+";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(475, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "E-4 ν^2=";
            // 
            // textBoxDiamondAkahama2004P
            // 
            this.textBoxDiamondAkahama2004P.BackColor = System.Drawing.Color.Navy;
            this.textBoxDiamondAkahama2004P.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxDiamondAkahama2004P.ForeColor = System.Drawing.Color.White;
            this.textBoxDiamondAkahama2004P.Location = new System.Drawing.Point(615, 11);
            this.textBoxDiamondAkahama2004P.Name = "textBoxDiamondAkahama2004P";
            this.textBoxDiamondAkahama2004P.ReadOnly = true;
            this.textBoxDiamondAkahama2004P.Size = new System.Drawing.Size(71, 27);
            this.textBoxDiamondAkahama2004P.TabIndex = 0;
            this.textBoxDiamondAkahama2004P.Text = "0";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label9.Location = new System.Drawing.Point(688, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "GPa";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.Control;
            this.label14.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label14.Location = new System.Drawing.Point(3, 6);
            this.label14.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(117, 14);
            this.label14.TabIndex = 3;
            this.label14.Text = "Original spectrum";
            // 
            // numericUpDownOriginalGaussian
            // 
            this.numericUpDownOriginalGaussian.AutoSize = true;
            this.numericUpDownOriginalGaussian.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDownOriginalGaussian.Location = new System.Drawing.Point(382, 3);
            this.numericUpDownOriginalGaussian.Name = "numericUpDownOriginalGaussian";
            this.numericUpDownOriginalGaussian.Size = new System.Drawing.Size(47, 22);
            this.numericUpDownOriginalGaussian.TabIndex = 3;
            this.numericUpDownOriginalGaussian.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownOriginalGaussian.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDownOriginalGaussian.MouseDown += new System.Windows.Forms.MouseEventHandler(this.numericUpDownOriginalGaussian_MouseDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label12.Location = new System.Drawing.Point(299, 6);
            this.label12.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 14);
            this.label12.TabIndex = 3;
            this.label12.Text = "Gaussian σ";
            // 
            // labelBottomTitle
            // 
            this.labelBottomTitle.AutoSize = true;
            this.labelBottomTitle.BackColor = System.Drawing.SystemColors.Control;
            this.labelBottomTitle.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelBottomTitle.Location = new System.Drawing.Point(3, 6);
            this.labelBottomTitle.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.labelBottomTitle.Name = "labelBottomTitle";
            this.labelBottomTitle.Size = new System.Drawing.Size(127, 14);
            this.labelBottomTitle.TabIndex = 3;
            this.labelBottomTitle.Text = "First Differentiation";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label13.Location = new System.Drawing.Point(136, 6);
            this.label13.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 14);
            this.label13.TabIndex = 3;
            this.label13.Text = "Running Average";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(309, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "Gaussian σ";
            // 
            // numericUpDownDifferentiationGaussian
            // 
            this.numericUpDownDifferentiationGaussian.AutoSize = true;
            this.numericUpDownDifferentiationGaussian.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDownDifferentiationGaussian.Location = new System.Drawing.Point(392, 3);
            this.numericUpDownDifferentiationGaussian.Name = "numericUpDownDifferentiationGaussian";
            this.numericUpDownDifferentiationGaussian.Size = new System.Drawing.Size(47, 22);
            this.numericUpDownDifferentiationGaussian.TabIndex = 3;
            this.numericUpDownDifferentiationGaussian.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownDifferentiationGaussian.ValueChanged += new System.EventHandler(this.numericUpDown_ValueChanged);
            this.numericUpDownDifferentiationGaussian.MouseDown += new System.Windows.Forms.MouseEventHandler(this.numericUpDownwnDifferentiationGaussian_MouseDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 130);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.graphControlTop);
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel3);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.graphControlBottom);
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel2);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(725, 296);
            this.splitContainer1.SplitterDistance = 136;
            this.splitContainer1.TabIndex = 6;
            // 
            // graphControlTop
            // 
            this.graphControlTop.AllowMouseOperation = true;
            this.graphControlTop.BackgroundColor = System.Drawing.Color.White;
            this.graphControlTop.BottomMargin = 0D;
            this.graphControlTop.DivisionLineColor = System.Drawing.Color.Gray;
            this.graphControlTop.DivisionSubLineColor = System.Drawing.Color.LightGray;
            this.graphControlTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphControlTop.FixRangeHorizontal = false;
            this.graphControlTop.FixRangeVertical = false;
            this.graphControlTop.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.graphControlTop.GraphName = "";
            this.graphControlTop.HorizontalGradiationTextVisivle = true;
            this.graphControlTop.Interpolation = false;
            this.graphControlTop.IsIntegerX = false;
            this.graphControlTop.IsIntegerY = false;
            this.graphControlTop.LabelX = "X:";
            this.graphControlTop.LabelY = "Y:";
            this.graphControlTop.LeftMargin = 0F;
            this.graphControlTop.LineColor = System.Drawing.Color.Red;
            this.graphControlTop.LineWidth = 2F;
            this.graphControlTop.Location = new System.Drawing.Point(0, 28);
            this.graphControlTop.LowerX = 0D;
            this.graphControlTop.LowerY = 0D;
            this.graphControlTop.MaximalX = 1D;
            this.graphControlTop.MaximalY = 1D;
            this.graphControlTop.MinimalX = 0D;
            this.graphControlTop.MinimalY = 0D;
            this.graphControlTop.Mode = Crystallography.Controls.GraphControl.DrawingMode.Line;
            this.graphControlTop.MousePositionVisible = true;
            this.graphControlTop.Name = "graphControlTop";
            this.graphControlTop.OriginPosition = new System.Drawing.Point(40, 20);
            this.graphControlTop.Size = new System.Drawing.Size(725, 108);
            this.graphControlTop.Smoothing = false;
            this.graphControlTop.TabIndex = 5;
            this.graphControlTop.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.graphControlTop.UnitX = "";
            this.graphControlTop.UnitY = "";
            this.graphControlTop.UpperText = "";
            this.graphControlTop.UpperTextVisible = true;
            this.graphControlTop.UpperX = 1D;
            this.graphControlTop.UpperY = 1D;
            this.graphControlTop.UseLineWidth = true;
            this.graphControlTop.VerticalGradiationTextVisivle = true;
            this.graphControlTop.XLog = false;
            this.graphControlTop.XScaleLineVisible = true;
            this.graphControlTop.YLog = false;
            this.graphControlTop.YScaleLineVisible = true;
            this.graphControlTop.DrawingRangeChanged += new Crystallography.Controls.GraphControl.DrawingRangeChangedEventHandler(this.graphControlTop_DrawingRangeChanged);
            this.graphControlTop.MouseDoubleClick2 += new Crystallography.Controls.GraphControl.MouseEventHandler2(this.graphControlBottom_MouseDoubleClick2);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.Controls.Add(this.label14);
            this.flowLayoutPanel3.Controls.Add(this.label2);
            this.flowLayoutPanel3.Controls.Add(this.numericUpDownOriginalRunningAverage);
            this.flowLayoutPanel3.Controls.Add(this.label12);
            this.flowLayoutPanel3.Controls.Add(this.numericUpDownOriginalGaussian);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(725, 28);
            this.flowLayoutPanel3.TabIndex = 4;
            // 
            // graphControlBottom
            // 
            this.graphControlBottom.AllowMouseOperation = true;
            this.graphControlBottom.BackgroundColor = System.Drawing.Color.White;
            this.graphControlBottom.BottomMargin = 0D;
            this.graphControlBottom.DivisionLineColor = System.Drawing.Color.Gray;
            this.graphControlBottom.DivisionSubLineColor = System.Drawing.Color.LightGray;
            this.graphControlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphControlBottom.FixRangeHorizontal = false;
            this.graphControlBottom.FixRangeVertical = false;
            this.graphControlBottom.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.graphControlBottom.GraphName = "";
            this.graphControlBottom.HorizontalGradiationTextVisivle = true;
            this.graphControlBottom.Interpolation = false;
            this.graphControlBottom.IsIntegerX = false;
            this.graphControlBottom.IsIntegerY = false;
            this.graphControlBottom.LabelX = "X:";
            this.graphControlBottom.LabelY = "Y:";
            this.graphControlBottom.LeftMargin = 0F;
            this.graphControlBottom.LineColor = System.Drawing.Color.Red;
            this.graphControlBottom.LineWidth = 2F;
            this.graphControlBottom.Location = new System.Drawing.Point(0, 28);
            this.graphControlBottom.LowerX = 0D;
            this.graphControlBottom.LowerY = 0D;
            this.graphControlBottom.MaximalX = 1D;
            this.graphControlBottom.MaximalY = 1D;
            this.graphControlBottom.MinimalX = 0D;
            this.graphControlBottom.MinimalY = 0D;
            this.graphControlBottom.Mode = Crystallography.Controls.GraphControl.DrawingMode.Line;
            this.graphControlBottom.MousePositionVisible = true;
            this.graphControlBottom.Name = "graphControlBottom";
            this.graphControlBottom.OriginPosition = new System.Drawing.Point(40, 20);
            this.graphControlBottom.Size = new System.Drawing.Size(725, 128);
            this.graphControlBottom.Smoothing = false;
            this.graphControlBottom.TabIndex = 6;
            this.graphControlBottom.TextFont = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.graphControlBottom.UnitX = "";
            this.graphControlBottom.UnitY = "";
            this.graphControlBottom.UpperText = "";
            this.graphControlBottom.UpperTextVisible = true;
            this.graphControlBottom.UpperX = 1D;
            this.graphControlBottom.UpperY = 1D;
            this.graphControlBottom.UseLineWidth = true;
            this.graphControlBottom.VerticalGradiationTextVisivle = true;
            this.graphControlBottom.XLog = false;
            this.graphControlBottom.XScaleLineVisible = true;
            this.graphControlBottom.YLog = false;
            this.graphControlBottom.YScaleLineVisible = true;
            this.graphControlBottom.DrawingRangeChanged += new Crystallography.Controls.GraphControl.DrawingRangeChangedEventHandler(this.graphControlBottom_DrawingRangeChanged);
            this.graphControlBottom.MouseDoubleClick2 += new Crystallography.Controls.GraphControl.MouseEventHandler2(this.graphControlBottom_MouseDoubleClick2);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.labelBottomTitle);
            this.flowLayoutPanel2.Controls.Add(this.label13);
            this.flowLayoutPanel2.Controls.Add(this.numericUpDownDifferentiationRunningAverage);
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Controls.Add(this.numericUpDownDifferentiationGaussian);
            this.flowLayoutPanel2.Controls.Add(this.label10);
            this.flowLayoutPanel2.Controls.Add(this.numericUpDownFitRange);
            this.flowLayoutPanel2.Controls.Add(this.labelDimension);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(725, 28);
            this.flowLayoutPanel2.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label10.Location = new System.Drawing.Point(445, 6);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 14);
            this.label10.TabIndex = 3;
            this.label10.Text = "Fitting Range";
            // 
            // numericUpDownFitRange
            // 
            this.numericUpDownFitRange.AutoSize = true;
            this.numericUpDownFitRange.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericUpDownFitRange.Location = new System.Drawing.Point(541, 3);
            this.numericUpDownFitRange.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFitRange.Name = "numericUpDownFitRange";
            this.numericUpDownFitRange.Size = new System.Drawing.Size(47, 22);
            this.numericUpDownFitRange.TabIndex = 2;
            this.numericUpDownFitRange.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownFitRange.ValueChanged += new System.EventHandler(this.numericUpDownFitRange_ValueChanged);
            // 
            // labelDimension
            // 
            this.labelDimension.AutoSize = true;
            this.labelDimension.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelDimension.Location = new System.Drawing.Point(594, 6);
            this.labelDimension.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.labelDimension.Name = "labelDimension";
            this.labelDimension.Size = new System.Drawing.Size(37, 14);
            this.labelDimension.TabIndex = 3;
            this.labelDimension.Text = "cm-1";
            // 
            // radioButtonDiamondRaman
            // 
            this.radioButtonDiamondRaman.AutoSize = true;
            this.radioButtonDiamondRaman.Checked = true;
            this.radioButtonDiamondRaman.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButtonDiamondRaman.Location = new System.Drawing.Point(3, 3);
            this.radioButtonDiamondRaman.Name = "radioButtonDiamondRaman";
            this.radioButtonDiamondRaman.Size = new System.Drawing.Size(127, 18);
            this.radioButtonDiamondRaman.TabIndex = 7;
            this.radioButtonDiamondRaman.TabStop = true;
            this.radioButtonDiamondRaman.Text = "Diamond Raman";
            this.radioButtonDiamondRaman.UseVisualStyleBackColor = true;
            this.radioButtonDiamondRaman.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // radioButtonRubyFluorescence
            // 
            this.radioButtonRubyFluorescence.AutoSize = true;
            this.radioButtonRubyFluorescence.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButtonRubyFluorescence.Location = new System.Drawing.Point(136, 3);
            this.radioButtonRubyFluorescence.Name = "radioButtonRubyFluorescence";
            this.radioButtonRubyFluorescence.Size = new System.Drawing.Size(142, 18);
            this.radioButtonRubyFluorescence.TabIndex = 7;
            this.radioButtonRubyFluorescence.Text = "Ruby Fluorescence";
            this.radioButtonRubyFluorescence.UseVisualStyleBackColor = true;
            this.radioButtonRubyFluorescence.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.radioButtonDiamondRaman);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonRubyFluorescence);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonEOS);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(725, 24);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // radioButtonEOS
            // 
            this.radioButtonEOS.AutoSize = true;
            this.radioButtonEOS.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButtonEOS.Location = new System.Drawing.Point(284, 3);
            this.radioButtonEOS.Name = "radioButtonEOS";
            this.radioButtonEOS.Size = new System.Drawing.Size(51, 18);
            this.radioButtonEOS.TabIndex = 7;
            this.radioButtonEOS.Text = "EOS";
            this.radioButtonEOS.UseVisualStyleBackColor = true;
            this.radioButtonEOS.CheckedChanged += new System.EventHandler(this.radioButtonMode_CheckedChanged);
            // 
            // textBoxMaoHydroP
            // 
            this.textBoxMaoHydroP.BackColor = System.Drawing.Color.Navy;
            this.textBoxMaoHydroP.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxMaoHydroP.ForeColor = System.Drawing.Color.White;
            this.textBoxMaoHydroP.Location = new System.Drawing.Point(620, 20);
            this.textBoxMaoHydroP.Name = "textBoxMaoHydroP";
            this.textBoxMaoHydroP.ReadOnly = true;
            this.textBoxMaoHydroP.Size = new System.Drawing.Size(64, 27);
            this.textBoxMaoHydroP.TabIndex = 0;
            this.textBoxMaoHydroP.Text = "0";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label28.Location = new System.Drawing.Point(683, 23);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(31, 17);
            this.label28.TabIndex = 1;
            this.label28.Text = "GPa";
            // 
            // groupBoxMao
            // 
            this.groupBoxMao.Controls.Add(this.radioButtonTempUnitK);
            this.groupBoxMao.Controls.Add(this.radioButtonTempUnitC);
            this.groupBoxMao.Controls.Add(this.groupBox4);
            this.groupBoxMao.Controls.Add(this.groupBox2);
            this.groupBoxMao.Controls.Add(this.groupBox3);
            this.groupBoxMao.Controls.Add(this.groupBox1);
            this.groupBoxMao.Controls.Add(this.numericBoxRubyR1);
            this.groupBoxMao.Controls.Add(this.label17);
            this.groupBoxMao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxMao.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxMao.Location = new System.Drawing.Point(0, 550);
            this.groupBoxMao.Name = "groupBoxMao";
            this.groupBoxMao.Size = new System.Drawing.Size(725, 244);
            this.groupBoxMao.TabIndex = 12;
            this.groupBoxMao.TabStop = false;
            this.groupBoxMao.Text = "Pressure calculation from the ruby fluorescence";
            // 
            // radioButtonTempUnitK
            // 
            this.radioButtonTempUnitK.AutoSize = true;
            this.radioButtonTempUnitK.Checked = true;
            this.radioButtonTempUnitK.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButtonTempUnitK.Location = new System.Drawing.Point(53, 88);
            this.radioButtonTempUnitK.Name = "radioButtonTempUnitK";
            this.radioButtonTempUnitK.Size = new System.Drawing.Size(32, 19);
            this.radioButtonTempUnitK.TabIndex = 7;
            this.radioButtonTempUnitK.TabStop = true;
            this.radioButtonTempUnitK.Text = "K";
            this.radioButtonTempUnitK.UseVisualStyleBackColor = true;
            this.radioButtonTempUnitK.CheckedChanged += new System.EventHandler(this.radioButtonTempUnit_CheckedChanged);
            // 
            // radioButtonTempUnitC
            // 
            this.radioButtonTempUnitC.AutoSize = true;
            this.radioButtonTempUnitC.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButtonTempUnitC.Location = new System.Drawing.Point(93, 88);
            this.radioButtonTempUnitC.Name = "radioButtonTempUnitC";
            this.radioButtonTempUnitC.Size = new System.Drawing.Size(37, 19);
            this.radioButtonTempUnitC.TabIndex = 7;
            this.radioButtonTempUnitC.Text = "℃";
            this.radioButtonTempUnitC.UseVisualStyleBackColor = true;
            this.radioButtonTempUnitC.CheckedChanged += new System.EventHandler(this.radioButtonTempUnit_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxMaoP);
            this.groupBox4.Controls.Add(this.numericBoxMaoA);
            this.groupBox4.Controls.Add(this.numericBoxMaoQuasiA);
            this.groupBox4.Controls.Add(this.numericBoxShenA);
            this.groupBox4.Controls.Add(this.numericBoxMaoHydroA);
            this.groupBox4.Controls.Add(this.label42);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.textBoxMaoQuasiP);
            this.groupBox4.Controls.Add(this.label107);
            this.groupBox4.Controls.Add(this.label46);
            this.groupBox4.Controls.Add(this.label45);
            this.groupBox4.Controls.Add(this.label34);
            this.groupBox4.Controls.Add(this.textBoxMaoHydroP);
            this.groupBox4.Controls.Add(this.textBoxShenP);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.label104);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox4.Location = new System.Drawing.Point(3, 164);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(719, 77);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Pressure calculation, where x = R1/R1₀ ,  f(x, y) = (x^y-1)/y ,  Δ=R1-R1₀";
            // 
            // textBoxMaoP
            // 
            this.textBoxMaoP.BackColor = System.Drawing.Color.Navy;
            this.textBoxMaoP.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxMaoP.ForeColor = System.Drawing.Color.White;
            this.textBoxMaoP.Location = new System.Drawing.Point(241, 20);
            this.textBoxMaoP.Name = "textBoxMaoP";
            this.textBoxMaoP.ReadOnly = true;
            this.textBoxMaoP.Size = new System.Drawing.Size(64, 27);
            this.textBoxMaoP.TabIndex = 0;
            this.textBoxMaoP.Text = "0";
            // 
            // numericBoxMaoA
            // 
            this.numericBoxMaoA.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxMaoA.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxMaoA.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxMaoA.FooterText = "f(x, 5)=";
            this.numericBoxMaoA.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxMaoA.HeaderText = "P=";
            this.numericBoxMaoA.Location = new System.Drawing.Point(118, 22);
            this.numericBoxMaoA.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxMaoA.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxMaoA.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxMaoA.Name = "numericBoxMaoA";
            this.numericBoxMaoA.RadianValue = 33.231068957972035D;
            this.numericBoxMaoA.RoundErrorAccuracy = -1;
            this.numericBoxMaoA.Size = new System.Drawing.Size(103, 25);
            this.numericBoxMaoA.TabIndex = 3;
            this.numericBoxMaoA.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxMaoA.Value = 1904D;
            // 
            // numericBoxMaoQuasiA
            // 
            this.numericBoxMaoQuasiA.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxMaoQuasiA.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxMaoQuasiA.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxMaoQuasiA.FooterText = "f(x, 7.665)=";
            this.numericBoxMaoQuasiA.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxMaoQuasiA.HeaderText = "P=";
            this.numericBoxMaoQuasiA.Location = new System.Drawing.Point(117, 49);
            this.numericBoxMaoQuasiA.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxMaoQuasiA.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxMaoQuasiA.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxMaoQuasiA.Name = "numericBoxMaoQuasiA";
            this.numericBoxMaoQuasiA.RadianValue = 33.231068957972035D;
            this.numericBoxMaoQuasiA.RoundErrorAccuracy = -1;
            this.numericBoxMaoQuasiA.Size = new System.Drawing.Size(123, 25);
            this.numericBoxMaoQuasiA.TabIndex = 3;
            this.numericBoxMaoQuasiA.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxMaoQuasiA.Value = 1904D;
            // 
            // numericBoxShenA
            // 
            this.numericBoxShenA.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxShenA.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxShenA.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxShenA.FooterText = "(Δ+5.63 Δ²)=";
            this.numericBoxShenA.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxShenA.HeaderText = "P=";
            this.numericBoxShenA.Location = new System.Drawing.Point(485, 49);
            this.numericBoxShenA.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxShenA.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxShenA.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxShenA.Name = "numericBoxShenA";
            this.numericBoxShenA.RadianValue = 32.637657012293964D;
            this.numericBoxShenA.RoundErrorAccuracy = -1;
            this.numericBoxShenA.Size = new System.Drawing.Size(131, 25);
            this.numericBoxShenA.TabIndex = 3;
            this.numericBoxShenA.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxShenA.Value = 1870D;
            // 
            // numericBoxMaoHydroA
            // 
            this.numericBoxMaoHydroA.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxMaoHydroA.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxMaoHydroA.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxMaoHydroA.FooterText = "f(x, 7.715)=";
            this.numericBoxMaoHydroA.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxMaoHydroA.HeaderText = "P=";
            this.numericBoxMaoHydroA.Location = new System.Drawing.Point(485, 22);
            this.numericBoxMaoHydroA.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxMaoHydroA.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxMaoHydroA.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxMaoHydroA.Name = "numericBoxMaoHydroA";
            this.numericBoxMaoHydroA.RadianValue = 33.231068957972035D;
            this.numericBoxMaoHydroA.RoundErrorAccuracy = -1;
            this.numericBoxMaoHydroA.Size = new System.Drawing.Size(123, 25);
            this.numericBoxMaoHydroA.TabIndex = 3;
            this.numericBoxMaoHydroA.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxMaoHydroA.Value = 1904D;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label42.Location = new System.Drawing.Point(305, 50);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(31, 17);
            this.label42.TabIndex = 1;
            this.label42.Text = "GPa";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label18.Location = new System.Drawing.Point(305, 23);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 17);
            this.label18.TabIndex = 1;
            this.label18.Text = "GPa";
            // 
            // textBoxMaoQuasiP
            // 
            this.textBoxMaoQuasiP.BackColor = System.Drawing.Color.Navy;
            this.textBoxMaoQuasiP.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxMaoQuasiP.ForeColor = System.Drawing.Color.White;
            this.textBoxMaoQuasiP.Location = new System.Drawing.Point(241, 47);
            this.textBoxMaoQuasiP.Name = "textBoxMaoQuasiP";
            this.textBoxMaoQuasiP.ReadOnly = true;
            this.textBoxMaoQuasiP.Size = new System.Drawing.Size(64, 27);
            this.textBoxMaoQuasiP.TabIndex = 0;
            this.textBoxMaoQuasiP.Text = "0";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label107.Location = new System.Drawing.Point(367, 50);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(108, 17);
            this.label107.TabIndex = 1;
            this.label107.Text = "Shen et al. (2020)";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label46.Location = new System.Drawing.Point(367, 23);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(114, 17);
            this.label46.TabIndex = 1;
            this.label46.Text = "Mao-hydro (2000)";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label45.Location = new System.Drawing.Point(6, 50);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(111, 17);
            this.label45.TabIndex = 1;
            this.label45.Text = "Mao-quasi (1986)";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label34.Location = new System.Drawing.Point(6, 23);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(75, 17);
            this.label34.TabIndex = 1;
            this.label34.Text = "Mao (1978)";
            // 
            // textBoxShenP
            // 
            this.textBoxShenP.BackColor = System.Drawing.Color.Navy;
            this.textBoxShenP.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxShenP.ForeColor = System.Drawing.Color.White;
            this.textBoxShenP.Location = new System.Drawing.Point(620, 47);
            this.textBoxShenP.Name = "textBoxShenP";
            this.textBoxShenP.ReadOnly = true;
            this.textBoxShenP.Size = new System.Drawing.Size(64, 27);
            this.textBoxShenP.TabIndex = 0;
            this.textBoxShenP.Text = "0";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label104.Location = new System.Drawing.Point(683, 50);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(31, 17);
            this.label104.TabIndex = 1;
            this.label104.Text = "GPa";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericBoxRubyRagan);
            this.groupBox2.Location = new System.Drawing.Point(136, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(583, 48);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Temperature dependency (Ragan et al., 1992) (Applicable in the range of 50-600K)";
            // 
            // numericBoxRubyRagan
            // 
            this.numericBoxRubyRagan.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxRubyRagan.DecimalPlaces = 3;
            this.numericBoxRubyRagan.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxRubyRagan.FooterText = "+ 4.49×10⁻²×T - 4.81×10⁻⁴×T² + 3.71×10⁻⁷×T³ )⁻¹ ×10⁷    [nm]";
            this.numericBoxRubyRagan.HeaderText = "R1(T, P=0) = (";
            this.numericBoxRubyRagan.Location = new System.Drawing.Point(9, 18);
            this.numericBoxRubyRagan.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxRubyRagan.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxRubyRagan.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxRubyRagan.Name = "numericBoxRubyRagan";
            this.numericBoxRubyRagan.RadianValue = 251.72883801514215D;
            this.numericBoxRubyRagan.RoundErrorAccuracy = -1;
            this.numericBoxRubyRagan.Size = new System.Drawing.Size(542, 25);
            this.numericBoxRubyRagan.TabIndex = 3;
            this.numericBoxRubyRagan.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxRubyRagan.Value = 14423D;
            this.numericBoxRubyRagan.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxRubyRagan_ValueChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxRubyR1_0CalculatedFromRagan);
            this.groupBox3.Controls.Add(this.numericBoxRubyT);
            this.groupBox3.Controls.Add(this.checkBoxRubyTemeratureSameAsRef);
            this.groupBox3.Controls.Add(this.numericBoxRubyR1_0);
            this.groupBox3.Location = new System.Drawing.Point(138, 19);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(319, 88);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Measument condition";
            // 
            // checkBoxRubyR1_0CalculatedFromRagan
            // 
            this.checkBoxRubyR1_0CalculatedFromRagan.AutoSize = true;
            this.checkBoxRubyR1_0CalculatedFromRagan.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxRubyR1_0CalculatedFromRagan.Location = new System.Drawing.Point(185, 43);
            this.checkBoxRubyR1_0CalculatedFromRagan.Name = "checkBoxRubyR1_0CalculatedFromRagan";
            this.checkBoxRubyR1_0CalculatedFromRagan.Size = new System.Drawing.Size(132, 38);
            this.checkBoxRubyR1_0CalculatedFromRagan.TabIndex = 7;
            this.checkBoxRubyR1_0CalculatedFromRagan.Text = "Caluculate from\r\n Ragan\'s equation";
            this.checkBoxRubyR1_0CalculatedFromRagan.UseVisualStyleBackColor = true;
            this.checkBoxRubyR1_0CalculatedFromRagan.CheckedChanged += new System.EventHandler(this.checkBoxRubyR1CalculatedFromRagan_CheckedChanged);
            // 
            // numericBoxRubyT
            // 
            this.numericBoxRubyT.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxRubyT.DecimalPlaces = 2;
            this.numericBoxRubyT.Enabled = false;
            this.numericBoxRubyT.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxRubyT.FooterText = "K";
            this.numericBoxRubyT.HeaderText = "T =";
            this.numericBoxRubyT.Location = new System.Drawing.Point(11, 18);
            this.numericBoxRubyT.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxRubyT.MaximumSize = new System.Drawing.Size(1000, 23);
            this.numericBoxRubyT.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxRubyT.Name = "numericBoxRubyT";
            this.numericBoxRubyT.RadianValue = 5.18624587230115D;
            this.numericBoxRubyT.RoundErrorAccuracy = -1;
            this.numericBoxRubyT.Size = new System.Drawing.Size(116, 23);
            this.numericBoxRubyT.TabIndex = 3;
            this.numericBoxRubyT.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxRubyT.Value = 297.15D;
            this.numericBoxRubyT.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxRubyT_ValueChanged);
            // 
            // checkBoxRubyTemeratureSameAsRef
            // 
            this.checkBoxRubyTemeratureSameAsRef.AutoSize = true;
            this.checkBoxRubyTemeratureSameAsRef.Checked = true;
            this.checkBoxRubyTemeratureSameAsRef.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRubyTemeratureSameAsRef.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBoxRubyTemeratureSameAsRef.Location = new System.Drawing.Point(135, 20);
            this.checkBoxRubyTemeratureSameAsRef.Name = "checkBoxRubyTemeratureSameAsRef";
            this.checkBoxRubyTemeratureSameAsRef.Size = new System.Drawing.Size(135, 21);
            this.checkBoxRubyTemeratureSameAsRef.TabIndex = 7;
            this.checkBoxRubyTemeratureSameAsRef.Text = "Same as reference";
            this.checkBoxRubyTemeratureSameAsRef.UseVisualStyleBackColor = true;
            this.checkBoxRubyTemeratureSameAsRef.CheckedChanged += new System.EventHandler(this.checkBoxRubyTemeratureSameAsRef_CheckedChanged);
            // 
            // numericBoxRubyR1_0
            // 
            this.numericBoxRubyR1_0.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxRubyR1_0.DecimalPlaces = 3;
            this.numericBoxRubyR1_0.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxRubyR1_0.FooterText = "nm";
            this.numericBoxRubyR1_0.HeaderText = "R1₀=R1(T,P=0)";
            this.numericBoxRubyR1_0.Location = new System.Drawing.Point(11, 52);
            this.numericBoxRubyR1_0.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxRubyR1_0.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxRubyR1_0.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxRubyR1_0.Name = "numericBoxRubyR1_0";
            this.numericBoxRubyR1_0.RadianValue = 12.12392964897861D;
            this.numericBoxRubyR1_0.RoundErrorAccuracy = -1;
            this.numericBoxRubyR1_0.Size = new System.Drawing.Size(171, 25);
            this.numericBoxRubyR1_0.TabIndex = 3;
            this.numericBoxRubyR1_0.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxRubyR1_0.Value = 694.65D;
            this.numericBoxRubyR1_0.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxR1_0_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonRubyRefR1Set);
            this.groupBox1.Controls.Add(this.numericBoxRubyRefT);
            this.groupBox1.Controls.Add(this.numericBoxRubyRefR1);
            this.groupBox1.Location = new System.Drawing.Point(463, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 88);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reference condition";
            // 
            // buttonRubyRefR1Set
            // 
            this.buttonRubyRefR1Set.AutoSize = true;
            this.buttonRubyRefR1Set.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonRubyRefR1Set.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonRubyRefR1Set.Location = new System.Drawing.Point(128, 49);
            this.buttonRubyRefR1Set.Name = "buttonRubyRefR1Set";
            this.buttonRubyRefR1Set.Size = new System.Drawing.Size(122, 27);
            this.buttonRubyRefR1Set.TabIndex = 4;
            this.buttonRubyRefR1Set.Text = "Set the current R1";
            this.buttonRubyRefR1Set.UseVisualStyleBackColor = true;
            this.buttonRubyRefR1Set.Click += new System.EventHandler(this.buttonRubyRefR1Set_Click);
            // 
            // numericBoxRubyRefT
            // 
            this.numericBoxRubyRefT.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxRubyRefT.DecimalPlaces = 2;
            this.numericBoxRubyRefT.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxRubyRefT.FooterText = "K";
            this.numericBoxRubyRefT.Location = new System.Drawing.Point(46, 21);
            this.numericBoxRubyRefT.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxRubyRefT.MaximumSize = new System.Drawing.Size(1000, 23);
            this.numericBoxRubyRefT.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxRubyRefT.Name = "numericBoxRubyRefT";
            this.numericBoxRubyRefT.RadianValue = 5.18624587230115D;
            this.numericBoxRubyRefT.RoundErrorAccuracy = -1;
            this.numericBoxRubyRefT.Size = new System.Drawing.Size(67, 23);
            this.numericBoxRubyRefT.TabIndex = 3;
            this.numericBoxRubyRefT.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxRubyRefT.Value = 297.15D;
            this.numericBoxRubyRefT.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxRubyRefT_ValueChanged);
            // 
            // numericBoxRubyRefR1
            // 
            this.numericBoxRubyRefR1.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxRubyRefR1.DecimalPlaces = 3;
            this.numericBoxRubyRefR1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxRubyRefR1.FooterText = "nm";
            this.numericBoxRubyRefR1.HeaderText = "R1(T=                , P=0) = ";
            this.numericBoxRubyRefR1.Location = new System.Drawing.Point(7, 21);
            this.numericBoxRubyRefR1.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxRubyRefR1.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxRubyRefR1.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxRubyRefR1.Name = "numericBoxRubyRefR1";
            this.numericBoxRubyRefR1.RadianValue = 12.116948331970631D;
            this.numericBoxRubyRefR1.RoundErrorAccuracy = -1;
            this.numericBoxRubyRefR1.Size = new System.Drawing.Size(243, 25);
            this.numericBoxRubyRefR1.TabIndex = 3;
            this.numericBoxRubyRefR1.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxRubyRefR1.Value = 694.25D;
            this.numericBoxRubyRefR1.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxRubyRefR1_ValueChanged);
            // 
            // numericBoxRubyR1
            // 
            this.numericBoxRubyR1.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxRubyR1.DecimalPlaces = 3;
            this.numericBoxRubyR1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxRubyR1.FooterText = "nm";
            this.numericBoxRubyR1.HeaderText = "R1";
            this.numericBoxRubyR1.Location = new System.Drawing.Point(8, 28);
            this.numericBoxRubyR1.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxRubyR1.MaximumSize = new System.Drawing.Size(1000, 29);
            this.numericBoxRubyR1.MinimumSize = new System.Drawing.Size(1, 27);
            this.numericBoxRubyR1.Name = "numericBoxRubyR1";
            this.numericBoxRubyR1.RadianValue = 12.12392964897861D;
            this.numericBoxRubyR1.RoundErrorAccuracy = -1;
            this.numericBoxRubyR1.Size = new System.Drawing.Size(125, 29);
            this.numericBoxRubyR1.TabIndex = 3;
            this.numericBoxRubyR1.TextFont = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.numericBoxRubyR1.Value = 694.65D;
            this.numericBoxRubyR1.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxRubyR1_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label17.Location = new System.Drawing.Point(6, 68);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(108, 17);
            this.label17.TabIndex = 1;
            this.label17.Text = "Temperature unit";
            // 
            // groupBoxAkahama2006
            // 
            this.groupBoxAkahama2006.Controls.Add(this.textBoxDiamondFratandunoHigh);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxDiamondAkahama2006P);
            this.groupBoxAkahama2006.Controls.Add(this.label44);
            this.groupBoxAkahama2006.Controls.Add(this.label29);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxAkahama2004A);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxAkahama2004B);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxDiamondRamanNu0);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxAkahama2004C);
            this.groupBoxAkahama2006.Controls.Add(this.label43);
            this.groupBoxAkahama2006.Controls.Add(this.label5);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxDiamondRamanNu);
            this.groupBoxAkahama2006.Controls.Add(this.label6);
            this.groupBoxAkahama2006.Controls.Add(this.label30);
            this.groupBoxAkahama2006.Controls.Add(this.label7);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxAkahama2006K0);
            this.groupBoxAkahama2006.Controls.Add(this.label8);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxAkahama2006K0Prime);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxDiamondFratandunoLow);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxDiamondAkahama2004P);
            this.groupBoxAkahama2006.Controls.Add(this.label4);
            this.groupBoxAkahama2006.Controls.Add(this.label20);
            this.groupBoxAkahama2006.Controls.Add(this.label19);
            this.groupBoxAkahama2006.Controls.Add(this.label3);
            this.groupBoxAkahama2006.Controls.Add(this.label24);
            this.groupBoxAkahama2006.Controls.Add(this.label31);
            this.groupBoxAkahama2006.Controls.Add(this.label9);
            this.groupBoxAkahama2006.Controls.Add(this.label33);
            this.groupBoxAkahama2006.Controls.Add(this.label21);
            this.groupBoxAkahama2006.Controls.Add(this.label32);
            this.groupBoxAkahama2006.Controls.Add(this.label35);
            this.groupBoxAkahama2006.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxAkahama2006.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxAkahama2006.Location = new System.Drawing.Point(0, 426);
            this.groupBoxAkahama2006.Name = "groupBoxAkahama2006";
            this.groupBoxAkahama2006.Size = new System.Drawing.Size(725, 124);
            this.groupBoxAkahama2006.TabIndex = 13;
            this.groupBoxAkahama2006.TabStop = false;
            this.groupBoxAkahama2006.Text = "Pressure calculation from the Raman edge";
            // 
            // textBoxDiamondFratandunoHigh
            // 
            this.textBoxDiamondFratandunoHigh.BackColor = System.Drawing.Color.Navy;
            this.textBoxDiamondFratandunoHigh.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxDiamondFratandunoHigh.ForeColor = System.Drawing.Color.White;
            this.textBoxDiamondFratandunoHigh.Location = new System.Drawing.Point(615, 92);
            this.textBoxDiamondFratandunoHigh.Name = "textBoxDiamondFratandunoHigh";
            this.textBoxDiamondFratandunoHigh.ReadOnly = true;
            this.textBoxDiamondFratandunoHigh.Size = new System.Drawing.Size(71, 27);
            this.textBoxDiamondFratandunoHigh.TabIndex = 0;
            this.textBoxDiamondFratandunoHigh.Text = "0";
            // 
            // textBoxDiamondAkahama2006P
            // 
            this.textBoxDiamondAkahama2006P.BackColor = System.Drawing.Color.Navy;
            this.textBoxDiamondAkahama2006P.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxDiamondAkahama2006P.ForeColor = System.Drawing.Color.White;
            this.textBoxDiamondAkahama2006P.Location = new System.Drawing.Point(615, 38);
            this.textBoxDiamondAkahama2006P.Name = "textBoxDiamondAkahama2006P";
            this.textBoxDiamondAkahama2006P.ReadOnly = true;
            this.textBoxDiamondAkahama2006P.Size = new System.Drawing.Size(71, 27);
            this.textBoxDiamondAkahama2006P.TabIndex = 0;
            this.textBoxDiamondAkahama2006P.Text = "0";
            // 
            // label44
            // 
            this.label44.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label44.Location = new System.Drawing.Point(6, 43);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(24, 12);
            this.label44.TabIndex = 1;
            this.label44.Text = "ν0";
            // 
            // label29
            // 
            this.label29.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label29.Location = new System.Drawing.Point(4, 20);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(16, 12);
            this.label29.TabIndex = 1;
            this.label29.Text = "ν";
            // 
            // textBoxDiamondRamanNu0
            // 
            this.textBoxDiamondRamanNu0.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxDiamondRamanNu0.Location = new System.Drawing.Point(36, 39);
            this.textBoxDiamondRamanNu0.Name = "textBoxDiamondRamanNu0";
            this.textBoxDiamondRamanNu0.Size = new System.Drawing.Size(63, 22);
            this.textBoxDiamondRamanNu0.TabIndex = 0;
            this.textBoxDiamondRamanNu0.Text = "1334";
            this.textBoxDiamondRamanNu0.TextChanged += new System.EventHandler(this.textBoxNu_TextChanged);
            // 
            // label43
            // 
            this.label43.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label43.Location = new System.Drawing.Point(105, 46);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(52, 12);
            this.label43.TabIndex = 1;
            this.label43.Text = "cm^-1";
            // 
            // textBoxDiamondRamanNu
            // 
            this.textBoxDiamondRamanNu.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxDiamondRamanNu.Location = new System.Drawing.Point(35, 16);
            this.textBoxDiamondRamanNu.Name = "textBoxDiamondRamanNu";
            this.textBoxDiamondRamanNu.Size = new System.Drawing.Size(64, 22);
            this.textBoxDiamondRamanNu.TabIndex = 0;
            this.textBoxDiamondRamanNu.Text = "0";
            this.textBoxDiamondRamanNu.TextChanged += new System.EventHandler(this.textBoxNu_TextChanged);
            // 
            // label30
            // 
            this.label30.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label30.Location = new System.Drawing.Point(105, 20);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(52, 12);
            this.label30.TabIndex = 1;
            this.label30.Text = "cm^-1";
            // 
            // textBoxAkahama2006K0
            // 
            this.textBoxAkahama2006K0.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxAkahama2006K0.Location = new System.Drawing.Point(309, 43);
            this.textBoxAkahama2006K0.Name = "textBoxAkahama2006K0";
            this.textBoxAkahama2006K0.Size = new System.Drawing.Size(44, 22);
            this.textBoxAkahama2006K0.TabIndex = 0;
            this.textBoxAkahama2006K0.Text = "547";
            this.textBoxAkahama2006K0.TextChanged += new System.EventHandler(this.textBoxNu_TextChanged);
            // 
            // textBoxAkahama2006K0Prime
            // 
            this.textBoxAkahama2006K0Prime.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxAkahama2006K0Prime.Location = new System.Drawing.Point(474, 43);
            this.textBoxAkahama2006K0Prime.Name = "textBoxAkahama2006K0Prime";
            this.textBoxAkahama2006K0Prime.Size = new System.Drawing.Size(44, 22);
            this.textBoxAkahama2006K0Prime.TabIndex = 0;
            this.textBoxAkahama2006K0Prime.Text = "3.75";
            this.textBoxAkahama2006K0Prime.TextChanged += new System.EventHandler(this.textBoxNu_TextChanged);
            // 
            // textBoxDiamondFratandunoLow
            // 
            this.textBoxDiamondFratandunoLow.BackColor = System.Drawing.Color.Navy;
            this.textBoxDiamondFratandunoLow.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxDiamondFratandunoLow.ForeColor = System.Drawing.Color.White;
            this.textBoxDiamondFratandunoLow.Location = new System.Drawing.Point(615, 65);
            this.textBoxDiamondFratandunoLow.Name = "textBoxDiamondFratandunoLow";
            this.textBoxDiamondFratandunoLow.ReadOnly = true;
            this.textBoxDiamondFratandunoLow.Size = new System.Drawing.Size(71, 27);
            this.textBoxDiamondFratandunoLow.TabIndex = 0;
            this.textBoxDiamondFratandunoLow.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(160, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Akahama (2004):";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label20.Location = new System.Drawing.Point(160, 100);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(430, 13);
            this.label20.TabIndex = 1;
            this.label20.Text = "Fratanduono+ (2021, >200GPa):  P = 199.49 - 852.78 × Δν/ν0 + 3103.8 × (Δν/ν0)² = " +
    "";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label19.Location = new System.Drawing.Point(160, 73);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(387, 13);
            this.label19.TabIndex = 1;
            this.label19.Text = "Fratanduono+ (2021, <300GPa):  P = 503.77 × Δν/ν0 + 753.83 × (Δν/ν0)² = ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(160, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Akahama (2006):";
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label24.Location = new System.Drawing.Point(688, 72);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(34, 12);
            this.label24.TabIndex = 1;
            this.label24.Text = "GPa";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label31.Location = new System.Drawing.Point(264, 46);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(39, 13);
            this.label31.TabIndex = 1;
            this.label31.Text = "P = K0";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label33.Location = new System.Drawing.Point(520, 46);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(65, 13);
            this.label33.TabIndex = 1;
            this.label33.Text = "-1)*Δν/ν0]=";
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label21.Location = new System.Drawing.Point(691, 99);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(34, 12);
            this.label21.TabIndex = 1;
            this.label21.Text = "GPa";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Segoe UI Symbol", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label32.Location = new System.Drawing.Point(357, 46);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(115, 13);
            this.label32.TabIndex = 1;
            this.label32.Text = "× Δν/ν0 × [1+1/2 (K0\'";
            // 
            // label35
            // 
            this.label35.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label35.Location = new System.Drawing.Point(691, 45);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(34, 12);
            this.label35.TabIndex = 1;
            this.label35.Text = "GPa";
            // 
            // flowLayoutPanelEOS
            // 
            this.flowLayoutPanelEOS.AutoScroll = true;
            this.flowLayoutPanelEOS.Controls.Add(this.groupBoxGold);
            this.flowLayoutPanelEOS.Controls.Add(this.groupBoxPlatinum);
            this.flowLayoutPanelEOS.Controls.Add(this.groupBoxNaClB1);
            this.flowLayoutPanelEOS.Controls.Add(this.groupBoxNaClB2);
            this.flowLayoutPanelEOS.Controls.Add(this.groupBoxPericlase);
            this.flowLayoutPanelEOS.Controls.Add(this.groupBoxCorundum);
            this.flowLayoutPanelEOS.Controls.Add(this.groupBoxAr);
            this.flowLayoutPanelEOS.Controls.Add(this.groupBoxRe);
            this.flowLayoutPanelEOS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelEOS.Location = new System.Drawing.Point(0, 26);
            this.flowLayoutPanelEOS.Name = "flowLayoutPanelEOS";
            this.flowLayoutPanelEOS.Size = new System.Drawing.Size(725, 56);
            this.flowLayoutPanelEOS.TabIndex = 14;
            // 
            // groupBoxGold
            // 
            this.groupBoxGold.Controls.Add(this.textBoxGoldJamieson);
            this.groupBoxGold.Controls.Add(this.textBoxGoldAnderson);
            this.groupBoxGold.Controls.Add(this.textBoxGoldTsuchiya);
            this.groupBoxGold.Controls.Add(this.label50);
            this.groupBoxGold.Controls.Add(this.textBoxGoldSim);
            this.groupBoxGold.Controls.Add(this.label11);
            this.groupBoxGold.Controls.Add(this.label70);
            this.groupBoxGold.Controls.Add(this.label15);
            this.groupBoxGold.Controls.Add(this.textBoxGold_a);
            this.groupBoxGold.Controls.Add(this.textBoxGold_a0);
            this.groupBoxGold.Controls.Add(this.label16);
            this.groupBoxGold.Controls.Add(this.label37);
            this.groupBoxGold.Controls.Add(this.label49);
            this.groupBoxGold.Controls.Add(this.label23);
            this.groupBoxGold.Controls.Add(this.label22);
            this.groupBoxGold.Controls.Add(this.label69);
            this.groupBoxGold.Controls.Add(this.label36);
            this.groupBoxGold.Controls.Add(this.label38);
            this.groupBoxGold.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxGold.Location = new System.Drawing.Point(3, 3);
            this.groupBoxGold.Name = "groupBoxGold";
            this.groupBoxGold.Size = new System.Drawing.Size(100, 214);
            this.groupBoxGold.TabIndex = 14;
            this.groupBoxGold.TabStop = false;
            this.groupBoxGold.Text = "Gold";
            // 
            // textBoxGoldJamieson
            // 
            this.textBoxGoldJamieson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxGoldJamieson.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxGoldJamieson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxGoldJamieson.Location = new System.Drawing.Point(12, 74);
            this.textBoxGoldJamieson.Name = "textBoxGoldJamieson";
            this.textBoxGoldJamieson.ReadOnly = true;
            this.textBoxGoldJamieson.Size = new System.Drawing.Size(48, 22);
            this.textBoxGoldJamieson.TabIndex = 5;
            this.textBoxGoldJamieson.Text = "0";
            // 
            // textBoxGoldAnderson
            // 
            this.textBoxGoldAnderson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxGoldAnderson.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxGoldAnderson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxGoldAnderson.Location = new System.Drawing.Point(12, 111);
            this.textBoxGoldAnderson.Name = "textBoxGoldAnderson";
            this.textBoxGoldAnderson.ReadOnly = true;
            this.textBoxGoldAnderson.Size = new System.Drawing.Size(48, 22);
            this.textBoxGoldAnderson.TabIndex = 5;
            this.textBoxGoldAnderson.Text = "0";
            // 
            // textBoxGoldTsuchiya
            // 
            this.textBoxGoldTsuchiya.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxGoldTsuchiya.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxGoldTsuchiya.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxGoldTsuchiya.Location = new System.Drawing.Point(12, 183);
            this.textBoxGoldTsuchiya.Name = "textBoxGoldTsuchiya";
            this.textBoxGoldTsuchiya.ReadOnly = true;
            this.textBoxGoldTsuchiya.Size = new System.Drawing.Size(48, 22);
            this.textBoxGoldTsuchiya.TabIndex = 5;
            this.textBoxGoldTsuchiya.Text = "0";
            // 
            // label50
            // 
            this.label50.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label50.Location = new System.Drawing.Point(60, 78);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(38, 16);
            this.label50.TabIndex = 7;
            this.label50.Text = "GPa";
            // 
            // textBoxGoldSim
            // 
            this.textBoxGoldSim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxGoldSim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxGoldSim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxGoldSim.Location = new System.Drawing.Point(12, 147);
            this.textBoxGoldSim.Name = "textBoxGoldSim";
            this.textBoxGoldSim.ReadOnly = true;
            this.textBoxGoldSim.Size = new System.Drawing.Size(48, 22);
            this.textBoxGoldSim.TabIndex = 5;
            this.textBoxGoldSim.Text = "0";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label11.Location = new System.Drawing.Point(60, 115);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 16);
            this.label11.TabIndex = 7;
            this.label11.Text = "GPa";
            // 
            // label70
            // 
            this.label70.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label70.Location = new System.Drawing.Point(60, 187);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(38, 16);
            this.label70.TabIndex = 7;
            this.label70.Text = "GPa";
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label15.Location = new System.Drawing.Point(60, 151);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 16);
            this.label15.TabIndex = 7;
            this.label15.Text = "GPa";
            // 
            // textBoxGold_a
            // 
            this.textBoxGold_a.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxGold_a.Location = new System.Drawing.Point(20, 36);
            this.textBoxGold_a.Name = "textBoxGold_a";
            this.textBoxGold_a.Size = new System.Drawing.Size(48, 21);
            this.textBoxGold_a.TabIndex = 5;
            this.textBoxGold_a.Text = "4.0786";
            this.textBoxGold_a.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxGold_a0
            // 
            this.textBoxGold_a0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxGold_a0.Location = new System.Drawing.Point(20, 16);
            this.textBoxGold_a0.Name = "textBoxGold_a0";
            this.textBoxGold_a0.Size = new System.Drawing.Size(48, 21);
            this.textBoxGold_a0.TabIndex = 5;
            this.textBoxGold_a0.Text = "4.0786";
            this.textBoxGold_a0.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label16.Location = new System.Drawing.Point(4, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(20, 12);
            this.label16.TabIndex = 7;
            this.label16.Text = "a0";
            // 
            // label37
            // 
            this.label37.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label37.Location = new System.Drawing.Point(68, 20);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(12, 16);
            this.label37.TabIndex = 7;
            this.label37.Text = "Å";
            // 
            // label49
            // 
            this.label49.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label49.Location = new System.Drawing.Point(12, 59);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(88, 19);
            this.label49.TabIndex = 7;
            this.label49.Text = "Jamieson (82)";
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label23.Location = new System.Drawing.Point(68, 40);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(12, 16);
            this.label23.TabIndex = 7;
            this.label23.Text = "Å";
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label22.Location = new System.Drawing.Point(12, 96);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 19);
            this.label22.TabIndex = 7;
            this.label22.Text = "Anderson (89)";
            // 
            // label69
            // 
            this.label69.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label69.Location = new System.Drawing.Point(12, 171);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(86, 16);
            this.label69.TabIndex = 7;
            this.label69.Text = "Tsuchiya (03)";
            // 
            // label36
            // 
            this.label36.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label36.Location = new System.Drawing.Point(12, 135);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(86, 16);
            this.label36.TabIndex = 7;
            this.label36.Text = "Sim (02)";
            // 
            // label38
            // 
            this.label38.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label38.Location = new System.Drawing.Point(12, 40);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(16, 12);
            this.label38.TabIndex = 7;
            this.label38.Text = "a";
            // 
            // groupBoxPlatinum
            // 
            this.groupBoxPlatinum.Controls.Add(this.textBoxPtA0);
            this.groupBoxPlatinum.Controls.Add(this.textBoxPtJamieson);
            this.groupBoxPlatinum.Controls.Add(this.textBoxPtHolems);
            this.groupBoxPlatinum.Controls.Add(this.label39);
            this.groupBoxPlatinum.Controls.Add(this.label47);
            this.groupBoxPlatinum.Controls.Add(this.label61);
            this.groupBoxPlatinum.Controls.Add(this.textBoxPtA);
            this.groupBoxPlatinum.Controls.Add(this.label54);
            this.groupBoxPlatinum.Controls.Add(this.label48);
            this.groupBoxPlatinum.Controls.Add(this.label51);
            this.groupBoxPlatinum.Controls.Add(this.label52);
            this.groupBoxPlatinum.Controls.Add(this.label60);
            this.groupBoxPlatinum.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxPlatinum.Location = new System.Drawing.Point(109, 3);
            this.groupBoxPlatinum.Name = "groupBoxPlatinum";
            this.groupBoxPlatinum.Size = new System.Drawing.Size(98, 136);
            this.groupBoxPlatinum.TabIndex = 13;
            this.groupBoxPlatinum.TabStop = false;
            this.groupBoxPlatinum.Text = "Platinum";
            // 
            // textBoxPtA0
            // 
            this.textBoxPtA0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxPtA0.Location = new System.Drawing.Point(20, 16);
            this.textBoxPtA0.Name = "textBoxPtA0";
            this.textBoxPtA0.Size = new System.Drawing.Size(48, 21);
            this.textBoxPtA0.TabIndex = 5;
            this.textBoxPtA0.Text = "3.9231";
            this.textBoxPtA0.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxPtJamieson
            // 
            this.textBoxPtJamieson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxPtJamieson.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxPtJamieson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxPtJamieson.Location = new System.Drawing.Point(12, 76);
            this.textBoxPtJamieson.Name = "textBoxPtJamieson";
            this.textBoxPtJamieson.ReadOnly = true;
            this.textBoxPtJamieson.Size = new System.Drawing.Size(48, 22);
            this.textBoxPtJamieson.TabIndex = 5;
            this.textBoxPtJamieson.Text = "0";
            // 
            // textBoxPtHolems
            // 
            this.textBoxPtHolems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxPtHolems.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxPtHolems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxPtHolems.Location = new System.Drawing.Point(12, 112);
            this.textBoxPtHolems.Name = "textBoxPtHolems";
            this.textBoxPtHolems.ReadOnly = true;
            this.textBoxPtHolems.Size = new System.Drawing.Size(48, 22);
            this.textBoxPtHolems.TabIndex = 5;
            this.textBoxPtHolems.Text = "0";
            // 
            // label39
            // 
            this.label39.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label39.Location = new System.Drawing.Point(60, 80);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(24, 16);
            this.label39.TabIndex = 7;
            this.label39.Text = "GPa";
            // 
            // label47
            // 
            this.label47.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label47.Location = new System.Drawing.Point(4, 20);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(24, 12);
            this.label47.TabIndex = 7;
            this.label47.Text = "a0";
            // 
            // label61
            // 
            this.label61.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label61.Location = new System.Drawing.Point(60, 116);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(24, 16);
            this.label61.TabIndex = 7;
            this.label61.Text = "GPa";
            // 
            // textBoxPtA
            // 
            this.textBoxPtA.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxPtA.Location = new System.Drawing.Point(20, 40);
            this.textBoxPtA.Name = "textBoxPtA";
            this.textBoxPtA.Size = new System.Drawing.Size(48, 21);
            this.textBoxPtA.TabIndex = 5;
            this.textBoxPtA.Text = "3.9231";
            this.textBoxPtA.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label54
            // 
            this.label54.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label54.Location = new System.Drawing.Point(68, 20);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(12, 16);
            this.label54.TabIndex = 7;
            this.label54.Text = "Å";
            // 
            // label48
            // 
            this.label48.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label48.Location = new System.Drawing.Point(68, 44);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(12, 16);
            this.label48.TabIndex = 7;
            this.label48.Text = "Å";
            // 
            // label51
            // 
            this.label51.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label51.Location = new System.Drawing.Point(4, 64);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(96, 14);
            this.label51.TabIndex = 7;
            this.label51.Text = "Jamieson (82)";
            // 
            // label52
            // 
            this.label52.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label52.Location = new System.Drawing.Point(12, 44);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(36, 12);
            this.label52.TabIndex = 7;
            this.label52.Text = "a";
            // 
            // label60
            // 
            this.label60.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label60.Location = new System.Drawing.Point(8, 101);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(92, 15);
            this.label60.TabIndex = 7;
            this.label60.Text = "Holmes (89)";
            // 
            // groupBoxNaClB1
            // 
            this.groupBoxNaClB1.Controls.Add(this.textBoxNaClB1_a0);
            this.groupBoxNaClB1.Controls.Add(this.textBoxNaClB1_a);
            this.groupBoxNaClB1.Controls.Add(this.label53);
            this.groupBoxNaClB1.Controls.Add(this.label55);
            this.groupBoxNaClB1.Controls.Add(this.label56);
            this.groupBoxNaClB1.Controls.Add(this.textBoxNaClB1Brown);
            this.groupBoxNaClB1.Controls.Add(this.label62);
            this.groupBoxNaClB1.Controls.Add(this.label57);
            this.groupBoxNaClB1.Controls.Add(this.label58);
            this.groupBoxNaClB1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxNaClB1.Location = new System.Drawing.Point(213, 3);
            this.groupBoxNaClB1.Name = "groupBoxNaClB1";
            this.groupBoxNaClB1.Size = new System.Drawing.Size(98, 100);
            this.groupBoxNaClB1.TabIndex = 16;
            this.groupBoxNaClB1.TabStop = false;
            this.groupBoxNaClB1.Text = "NaCl B1";
            // 
            // textBoxNaClB1_a0
            // 
            this.textBoxNaClB1_a0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxNaClB1_a0.Location = new System.Drawing.Point(20, 16);
            this.textBoxNaClB1_a0.Name = "textBoxNaClB1_a0";
            this.textBoxNaClB1_a0.Size = new System.Drawing.Size(48, 21);
            this.textBoxNaClB1_a0.TabIndex = 5;
            this.textBoxNaClB1_a0.Text = "5.63900";
            this.textBoxNaClB1_a0.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxNaClB1_a
            // 
            this.textBoxNaClB1_a.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxNaClB1_a.Location = new System.Drawing.Point(20, 36);
            this.textBoxNaClB1_a.Name = "textBoxNaClB1_a";
            this.textBoxNaClB1_a.Size = new System.Drawing.Size(48, 21);
            this.textBoxNaClB1_a.TabIndex = 5;
            this.textBoxNaClB1_a.Text = "4.0786";
            this.textBoxNaClB1_a.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label53
            // 
            this.label53.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label53.Location = new System.Drawing.Point(4, 20);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(20, 12);
            this.label53.TabIndex = 7;
            this.label53.Text = "a0";
            // 
            // label55
            // 
            this.label55.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label55.Location = new System.Drawing.Point(12, 40);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(16, 12);
            this.label55.TabIndex = 7;
            this.label55.Text = "a";
            // 
            // label56
            // 
            this.label56.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label56.Location = new System.Drawing.Point(60, 76);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(32, 16);
            this.label56.TabIndex = 7;
            this.label56.Text = "GPa";
            // 
            // textBoxNaClB1Brown
            // 
            this.textBoxNaClB1Brown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxNaClB1Brown.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxNaClB1Brown.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxNaClB1Brown.Location = new System.Drawing.Point(12, 72);
            this.textBoxNaClB1Brown.Name = "textBoxNaClB1Brown";
            this.textBoxNaClB1Brown.ReadOnly = true;
            this.textBoxNaClB1Brown.Size = new System.Drawing.Size(48, 22);
            this.textBoxNaClB1Brown.TabIndex = 5;
            this.textBoxNaClB1Brown.Text = "0";
            // 
            // label62
            // 
            this.label62.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label62.Location = new System.Drawing.Point(68, 20);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(12, 16);
            this.label62.TabIndex = 7;
            this.label62.Text = "Å";
            // 
            // label57
            // 
            this.label57.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label57.Location = new System.Drawing.Point(68, 40);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(12, 16);
            this.label57.TabIndex = 7;
            this.label57.Text = "Å";
            // 
            // label58
            // 
            this.label58.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label58.Location = new System.Drawing.Point(8, 60);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(76, 16);
            this.label58.TabIndex = 7;
            this.label58.Text = "Brown (99)";
            // 
            // groupBoxNaClB2
            // 
            this.groupBoxNaClB2.Controls.Add(this.textBoxNaClB2_a0);
            this.groupBoxNaClB2.Controls.Add(this.textBoxNaClB2_a);
            this.groupBoxNaClB2.Controls.Add(this.textBoxNaClB2SataMgO);
            this.groupBoxNaClB2.Controls.Add(this.textBoxNaClB2SataPt);
            this.groupBoxNaClB2.Controls.Add(this.label65);
            this.groupBoxNaClB2.Controls.Add(this.label67);
            this.groupBoxNaClB2.Controls.Add(this.label59);
            this.groupBoxNaClB2.Controls.Add(this.label63);
            this.groupBoxNaClB2.Controls.Add(this.label64);
            this.groupBoxNaClB2.Controls.Add(this.label66);
            this.groupBoxNaClB2.Controls.Add(this.label68);
            this.groupBoxNaClB2.Controls.Add(this.label71);
            this.groupBoxNaClB2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxNaClB2.Location = new System.Drawing.Point(317, 3);
            this.groupBoxNaClB2.Name = "groupBoxNaClB2";
            this.groupBoxNaClB2.Size = new System.Drawing.Size(100, 128);
            this.groupBoxNaClB2.TabIndex = 15;
            this.groupBoxNaClB2.TabStop = false;
            this.groupBoxNaClB2.Text = "NaCl B2";
            // 
            // textBoxNaClB2_a0
            // 
            this.textBoxNaClB2_a0.Enabled = false;
            this.textBoxNaClB2_a0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxNaClB2_a0.Location = new System.Drawing.Point(20, 16);
            this.textBoxNaClB2_a0.Name = "textBoxNaClB2_a0";
            this.textBoxNaClB2_a0.Size = new System.Drawing.Size(48, 21);
            this.textBoxNaClB2_a0.TabIndex = 5;
            this.textBoxNaClB2_a0.Text = "0";
            this.textBoxNaClB2_a0.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxNaClB2_a
            // 
            this.textBoxNaClB2_a.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxNaClB2_a.Location = new System.Drawing.Point(20, 36);
            this.textBoxNaClB2_a.Name = "textBoxNaClB2_a";
            this.textBoxNaClB2_a.Size = new System.Drawing.Size(48, 21);
            this.textBoxNaClB2_a.TabIndex = 5;
            this.textBoxNaClB2_a.Text = "4.0786";
            this.textBoxNaClB2_a.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxNaClB2SataMgO
            // 
            this.textBoxNaClB2SataMgO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxNaClB2SataMgO.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxNaClB2SataMgO.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxNaClB2SataMgO.Location = new System.Drawing.Point(12, 104);
            this.textBoxNaClB2SataMgO.Name = "textBoxNaClB2SataMgO";
            this.textBoxNaClB2SataMgO.ReadOnly = true;
            this.textBoxNaClB2SataMgO.Size = new System.Drawing.Size(48, 22);
            this.textBoxNaClB2SataMgO.TabIndex = 5;
            this.textBoxNaClB2SataMgO.Text = "0";
            // 
            // textBoxNaClB2SataPt
            // 
            this.textBoxNaClB2SataPt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxNaClB2SataPt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxNaClB2SataPt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxNaClB2SataPt.Location = new System.Drawing.Point(12, 68);
            this.textBoxNaClB2SataPt.Name = "textBoxNaClB2SataPt";
            this.textBoxNaClB2SataPt.ReadOnly = true;
            this.textBoxNaClB2SataPt.Size = new System.Drawing.Size(48, 22);
            this.textBoxNaClB2SataPt.TabIndex = 5;
            this.textBoxNaClB2SataPt.Text = "0";
            // 
            // label65
            // 
            this.label65.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label65.Location = new System.Drawing.Point(4, 20);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(32, 12);
            this.label65.TabIndex = 7;
            this.label65.Text = "a0";
            // 
            // label67
            // 
            this.label67.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label67.Location = new System.Drawing.Point(60, 108);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(40, 16);
            this.label67.TabIndex = 7;
            this.label67.Text = "GPa";
            // 
            // label59
            // 
            this.label59.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label59.Location = new System.Drawing.Point(60, 72);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(40, 16);
            this.label59.TabIndex = 7;
            this.label59.Text = "GPa";
            // 
            // label63
            // 
            this.label63.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label63.Location = new System.Drawing.Point(12, 40);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(32, 12);
            this.label63.TabIndex = 7;
            this.label63.Text = "a";
            // 
            // label64
            // 
            this.label64.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label64.Location = new System.Drawing.Point(68, 20);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(12, 16);
            this.label64.TabIndex = 7;
            this.label64.Text = "Å";
            // 
            // label66
            // 
            this.label66.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label66.Location = new System.Drawing.Point(68, 40);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(12, 16);
            this.label66.TabIndex = 7;
            this.label66.Text = "Å";
            // 
            // label68
            // 
            this.label68.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label68.Location = new System.Drawing.Point(4, 92);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(76, 16);
            this.label68.TabIndex = 7;
            this.label68.Text = "Sata (02) (MgO)";
            // 
            // label71
            // 
            this.label71.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label71.Location = new System.Drawing.Point(4, 56);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(76, 12);
            this.label71.TabIndex = 7;
            this.label71.Text = "Sata (02) (Pt)";
            // 
            // groupBoxPericlase
            // 
            this.groupBoxPericlase.Controls.Add(this.label72);
            this.groupBoxPericlase.Controls.Add(this.label73);
            this.groupBoxPericlase.Controls.Add(this.textBoxMgOAizawa);
            this.groupBoxPericlase.Controls.Add(this.label74);
            this.groupBoxPericlase.Controls.Add(this.textBoxMgODewaele);
            this.groupBoxPericlase.Controls.Add(this.textBoxMgOJacson);
            this.groupBoxPericlase.Controls.Add(this.textBoxMgOA0);
            this.groupBoxPericlase.Controls.Add(this.label75);
            this.groupBoxPericlase.Controls.Add(this.label76);
            this.groupBoxPericlase.Controls.Add(this.label77);
            this.groupBoxPericlase.Controls.Add(this.label78);
            this.groupBoxPericlase.Controls.Add(this.textBoxMgOA);
            this.groupBoxPericlase.Controls.Add(this.label79);
            this.groupBoxPericlase.Controls.Add(this.label80);
            this.groupBoxPericlase.Controls.Add(this.label81);
            this.groupBoxPericlase.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxPericlase.Location = new System.Drawing.Point(423, 3);
            this.groupBoxPericlase.Name = "groupBoxPericlase";
            this.groupBoxPericlase.Size = new System.Drawing.Size(100, 172);
            this.groupBoxPericlase.TabIndex = 10;
            this.groupBoxPericlase.TabStop = false;
            this.groupBoxPericlase.Text = "Periclase";
            // 
            // label72
            // 
            this.label72.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label72.Location = new System.Drawing.Point(60, 148);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(40, 16);
            this.label72.TabIndex = 7;
            this.label72.Text = "GPa";
            // 
            // label73
            // 
            this.label73.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label73.Location = new System.Drawing.Point(60, 112);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(40, 16);
            this.label73.TabIndex = 7;
            this.label73.Text = "GPa";
            // 
            // textBoxMgOAizawa
            // 
            this.textBoxMgOAizawa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxMgOAizawa.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxMgOAizawa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxMgOAizawa.Location = new System.Drawing.Point(12, 144);
            this.textBoxMgOAizawa.Name = "textBoxMgOAizawa";
            this.textBoxMgOAizawa.ReadOnly = true;
            this.textBoxMgOAizawa.Size = new System.Drawing.Size(48, 22);
            this.textBoxMgOAizawa.TabIndex = 5;
            this.textBoxMgOAizawa.Text = "0";
            // 
            // label74
            // 
            this.label74.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label74.Location = new System.Drawing.Point(60, 76);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(40, 16);
            this.label74.TabIndex = 7;
            this.label74.Text = "GPa";
            // 
            // textBoxMgODewaele
            // 
            this.textBoxMgODewaele.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxMgODewaele.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxMgODewaele.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxMgODewaele.Location = new System.Drawing.Point(12, 108);
            this.textBoxMgODewaele.Name = "textBoxMgODewaele";
            this.textBoxMgODewaele.ReadOnly = true;
            this.textBoxMgODewaele.Size = new System.Drawing.Size(48, 22);
            this.textBoxMgODewaele.TabIndex = 5;
            this.textBoxMgODewaele.Text = "0";
            // 
            // textBoxMgOJacson
            // 
            this.textBoxMgOJacson.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxMgOJacson.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxMgOJacson.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxMgOJacson.Location = new System.Drawing.Point(12, 72);
            this.textBoxMgOJacson.Name = "textBoxMgOJacson";
            this.textBoxMgOJacson.ReadOnly = true;
            this.textBoxMgOJacson.Size = new System.Drawing.Size(48, 22);
            this.textBoxMgOJacson.TabIndex = 5;
            this.textBoxMgOJacson.Text = "0";
            // 
            // textBoxMgOA0
            // 
            this.textBoxMgOA0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxMgOA0.Location = new System.Drawing.Point(20, 16);
            this.textBoxMgOA0.Name = "textBoxMgOA0";
            this.textBoxMgOA0.Size = new System.Drawing.Size(48, 21);
            this.textBoxMgOA0.TabIndex = 5;
            this.textBoxMgOA0.Text = "4.2112";
            this.textBoxMgOA0.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label75
            // 
            this.label75.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label75.Location = new System.Drawing.Point(8, 132);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(84, 12);
            this.label75.TabIndex = 7;
            this.label75.Text = "Aizawa (06)";
            // 
            // label76
            // 
            this.label76.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label76.Location = new System.Drawing.Point(4, 96);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(88, 12);
            this.label76.TabIndex = 7;
            this.label76.Text = "Dewaele (00)";
            // 
            // label77
            // 
            this.label77.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label77.Location = new System.Drawing.Point(4, 60);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(92, 16);
            this.label77.TabIndex = 7;
            this.label77.Text = "Jackson (98)";
            // 
            // label78
            // 
            this.label78.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label78.Location = new System.Drawing.Point(4, 20);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(20, 12);
            this.label78.TabIndex = 7;
            this.label78.Text = "a0";
            // 
            // textBoxMgOA
            // 
            this.textBoxMgOA.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxMgOA.Location = new System.Drawing.Point(20, 36);
            this.textBoxMgOA.Name = "textBoxMgOA";
            this.textBoxMgOA.Size = new System.Drawing.Size(48, 21);
            this.textBoxMgOA.TabIndex = 5;
            this.textBoxMgOA.Text = "4.2112";
            this.textBoxMgOA.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label79
            // 
            this.label79.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label79.Location = new System.Drawing.Point(68, 20);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(12, 16);
            this.label79.TabIndex = 7;
            this.label79.Text = "Å";
            // 
            // label80
            // 
            this.label80.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label80.Location = new System.Drawing.Point(4, 40);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(16, 12);
            this.label80.TabIndex = 7;
            this.label80.Text = "a";
            // 
            // label81
            // 
            this.label81.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label81.Location = new System.Drawing.Point(68, 40);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(12, 16);
            this.label81.TabIndex = 7;
            this.label81.Text = "Å";
            // 
            // groupBoxCorundum
            // 
            this.groupBoxCorundum.Controls.Add(this.label82);
            this.groupBoxCorundum.Controls.Add(this.textBoxCorundumDubrovinsky);
            this.groupBoxCorundum.Controls.Add(this.textBoxColundumV0);
            this.groupBoxCorundum.Controls.Add(this.label83);
            this.groupBoxCorundum.Controls.Add(this.label84);
            this.groupBoxCorundum.Controls.Add(this.textBoxCorundumV);
            this.groupBoxCorundum.Controls.Add(this.label85);
            this.groupBoxCorundum.Controls.Add(this.label86);
            this.groupBoxCorundum.Controls.Add(this.label87);
            this.groupBoxCorundum.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxCorundum.Location = new System.Drawing.Point(529, 3);
            this.groupBoxCorundum.Name = "groupBoxCorundum";
            this.groupBoxCorundum.Size = new System.Drawing.Size(100, 101);
            this.groupBoxCorundum.TabIndex = 9;
            this.groupBoxCorundum.TabStop = false;
            this.groupBoxCorundum.Text = "Corundum";
            // 
            // label82
            // 
            this.label82.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label82.Location = new System.Drawing.Point(56, 76);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(40, 16);
            this.label82.TabIndex = 7;
            this.label82.Text = "GPa";
            // 
            // textBoxCorundumDubrovinsky
            // 
            this.textBoxCorundumDubrovinsky.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxCorundumDubrovinsky.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxCorundumDubrovinsky.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxCorundumDubrovinsky.Location = new System.Drawing.Point(8, 72);
            this.textBoxCorundumDubrovinsky.Name = "textBoxCorundumDubrovinsky";
            this.textBoxCorundumDubrovinsky.ReadOnly = true;
            this.textBoxCorundumDubrovinsky.Size = new System.Drawing.Size(48, 22);
            this.textBoxCorundumDubrovinsky.TabIndex = 5;
            this.textBoxCorundumDubrovinsky.Text = "0";
            // 
            // textBoxColundumV0
            // 
            this.textBoxColundumV0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxColundumV0.Location = new System.Drawing.Point(20, 16);
            this.textBoxColundumV0.Name = "textBoxColundumV0";
            this.textBoxColundumV0.Size = new System.Drawing.Size(36, 21);
            this.textBoxColundumV0.TabIndex = 5;
            this.textBoxColundumV0.Text = "254.6959";
            this.textBoxColundumV0.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label83
            // 
            this.label83.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label83.Location = new System.Drawing.Point(4, 56);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(105, 20);
            this.label83.TabIndex = 7;
            this.label83.Text = "Dubrovinsky (98)";
            // 
            // label84
            // 
            this.label84.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label84.Location = new System.Drawing.Point(4, 20);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(20, 12);
            this.label84.TabIndex = 7;
            this.label84.Text = "V0";
            // 
            // textBoxCorundumV
            // 
            this.textBoxCorundumV.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxCorundumV.Location = new System.Drawing.Point(20, 36);
            this.textBoxCorundumV.Name = "textBoxCorundumV";
            this.textBoxCorundumV.Size = new System.Drawing.Size(36, 21);
            this.textBoxCorundumV.TabIndex = 5;
            this.textBoxCorundumV.Text = "254.6959";
            this.textBoxCorundumV.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label85
            // 
            this.label85.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label85.Location = new System.Drawing.Point(56, 20);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(38, 16);
            this.label85.TabIndex = 7;
            this.label85.Text = "Å^3";
            // 
            // label86
            // 
            this.label86.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label86.Location = new System.Drawing.Point(4, 40);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(16, 12);
            this.label86.TabIndex = 7;
            this.label86.Text = "V";
            // 
            // label87
            // 
            this.label87.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label87.Location = new System.Drawing.Point(56, 40);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(40, 16);
            this.label87.TabIndex = 7;
            this.label87.Text = "Å^3";
            // 
            // groupBoxAr
            // 
            this.groupBoxAr.Controls.Add(this.textBoxArA0);
            this.groupBoxAr.Controls.Add(this.textBoxArA);
            this.groupBoxAr.Controls.Add(this.textBoxArJephcoat);
            this.groupBoxAr.Controls.Add(this.label88);
            this.groupBoxAr.Controls.Add(this.textBoxArRoss);
            this.groupBoxAr.Controls.Add(this.label89);
            this.groupBoxAr.Controls.Add(this.label90);
            this.groupBoxAr.Controls.Add(this.label91);
            this.groupBoxAr.Controls.Add(this.label92);
            this.groupBoxAr.Controls.Add(this.label93);
            this.groupBoxAr.Controls.Add(this.label94);
            this.groupBoxAr.Controls.Add(this.label95);
            this.groupBoxAr.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxAr.Location = new System.Drawing.Point(3, 223);
            this.groupBoxAr.Name = "groupBoxAr";
            this.groupBoxAr.Size = new System.Drawing.Size(104, 132);
            this.groupBoxAr.TabIndex = 12;
            this.groupBoxAr.TabStop = false;
            this.groupBoxAr.Text = "Ar";
            // 
            // textBoxArA0
            // 
            this.textBoxArA0.Enabled = false;
            this.textBoxArA0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxArA0.Location = new System.Drawing.Point(20, 15);
            this.textBoxArA0.Name = "textBoxArA0";
            this.textBoxArA0.Size = new System.Drawing.Size(48, 21);
            this.textBoxArA0.TabIndex = 5;
            this.textBoxArA0.Text = "0";
            this.textBoxArA0.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxArA
            // 
            this.textBoxArA.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxArA.Location = new System.Drawing.Point(20, 36);
            this.textBoxArA.Name = "textBoxArA";
            this.textBoxArA.Size = new System.Drawing.Size(48, 21);
            this.textBoxArA.TabIndex = 5;
            this.textBoxArA.Text = "4.0786";
            this.textBoxArA.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxArJephcoat
            // 
            this.textBoxArJephcoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxArJephcoat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxArJephcoat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxArJephcoat.Location = new System.Drawing.Point(12, 106);
            this.textBoxArJephcoat.Name = "textBoxArJephcoat";
            this.textBoxArJephcoat.ReadOnly = true;
            this.textBoxArJephcoat.Size = new System.Drawing.Size(48, 22);
            this.textBoxArJephcoat.TabIndex = 5;
            this.textBoxArJephcoat.Text = "0";
            // 
            // label88
            // 
            this.label88.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label88.Location = new System.Drawing.Point(60, 110);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(38, 16);
            this.label88.TabIndex = 7;
            this.label88.Text = "GPa";
            // 
            // textBoxArRoss
            // 
            this.textBoxArRoss.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxArRoss.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxArRoss.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxArRoss.Location = new System.Drawing.Point(12, 70);
            this.textBoxArRoss.Name = "textBoxArRoss";
            this.textBoxArRoss.ReadOnly = true;
            this.textBoxArRoss.Size = new System.Drawing.Size(48, 22);
            this.textBoxArRoss.TabIndex = 5;
            this.textBoxArRoss.Text = "0";
            // 
            // label89
            // 
            this.label89.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label89.Location = new System.Drawing.Point(60, 74);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(38, 16);
            this.label89.TabIndex = 7;
            this.label89.Text = "GPa";
            // 
            // label90
            // 
            this.label90.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label90.Location = new System.Drawing.Point(4, 19);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(32, 12);
            this.label90.TabIndex = 7;
            this.label90.Text = "a0";
            // 
            // label91
            // 
            this.label91.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label91.Location = new System.Drawing.Point(12, 40);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(32, 12);
            this.label91.TabIndex = 7;
            this.label91.Text = "a";
            // 
            // label92
            // 
            this.label92.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label92.Location = new System.Drawing.Point(4, 94);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(94, 12);
            this.label92.TabIndex = 7;
            this.label92.Text = "Jephcoat (98)";
            // 
            // label93
            // 
            this.label93.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label93.Location = new System.Drawing.Point(68, 40);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(12, 16);
            this.label93.TabIndex = 7;
            this.label93.Text = "Å";
            // 
            // label94
            // 
            this.label94.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label94.Location = new System.Drawing.Point(4, 58);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(94, 12);
            this.label94.TabIndex = 7;
            this.label94.Text = "Ross et al. (86)";
            // 
            // label95
            // 
            this.label95.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label95.Location = new System.Drawing.Point(68, 19);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(12, 16);
            this.label95.TabIndex = 7;
            this.label95.Text = "Å";
            // 
            // groupBoxRe
            // 
            this.groupBoxRe.Controls.Add(this.label96);
            this.groupBoxRe.Controls.Add(this.textBoxReZha);
            this.groupBoxRe.Controls.Add(this.textBoxReV0);
            this.groupBoxRe.Controls.Add(this.label97);
            this.groupBoxRe.Controls.Add(this.label98);
            this.groupBoxRe.Controls.Add(this.textBoxReV);
            this.groupBoxRe.Controls.Add(this.label99);
            this.groupBoxRe.Controls.Add(this.label100);
            this.groupBoxRe.Controls.Add(this.label101);
            this.groupBoxRe.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxRe.Location = new System.Drawing.Point(113, 223);
            this.groupBoxRe.Name = "groupBoxRe";
            this.groupBoxRe.Size = new System.Drawing.Size(104, 101);
            this.groupBoxRe.TabIndex = 11;
            this.groupBoxRe.TabStop = false;
            this.groupBoxRe.Text = "Re";
            // 
            // label96
            // 
            this.label96.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label96.Location = new System.Drawing.Point(56, 76);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(24, 16);
            this.label96.TabIndex = 7;
            this.label96.Text = "GPa";
            // 
            // textBoxReZha
            // 
            this.textBoxReZha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxReZha.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.textBoxReZha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.textBoxReZha.Location = new System.Drawing.Point(8, 72);
            this.textBoxReZha.Name = "textBoxReZha";
            this.textBoxReZha.ReadOnly = true;
            this.textBoxReZha.Size = new System.Drawing.Size(48, 22);
            this.textBoxReZha.TabIndex = 5;
            this.textBoxReZha.Text = "0";
            // 
            // textBoxReV0
            // 
            this.textBoxReV0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxReV0.Location = new System.Drawing.Point(20, 16);
            this.textBoxReV0.Name = "textBoxReV0";
            this.textBoxReV0.Size = new System.Drawing.Size(36, 21);
            this.textBoxReV0.TabIndex = 5;
            this.textBoxReV0.Text = "29.42795";
            this.textBoxReV0.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label97
            // 
            this.label97.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label97.Location = new System.Drawing.Point(4, 56);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(100, 20);
            this.label97.TabIndex = 7;
            this.label97.Text = "Zha et al. (04)";
            // 
            // label98
            // 
            this.label98.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label98.Location = new System.Drawing.Point(4, 20);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(20, 12);
            this.label98.TabIndex = 7;
            this.label98.Text = "V0";
            // 
            // textBoxReV
            // 
            this.textBoxReV.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxReV.Location = new System.Drawing.Point(20, 36);
            this.textBoxReV.Name = "textBoxReV";
            this.textBoxReV.Size = new System.Drawing.Size(36, 21);
            this.textBoxReV.TabIndex = 5;
            this.textBoxReV.Text = "254.6959";
            this.textBoxReV.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label99
            // 
            this.label99.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label99.Location = new System.Drawing.Point(56, 20);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(42, 16);
            this.label99.TabIndex = 7;
            this.label99.Text = "Å^3";
            // 
            // label100
            // 
            this.label100.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label100.Location = new System.Drawing.Point(4, 40);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(16, 12);
            this.label100.TabIndex = 7;
            this.label100.Text = "V";
            // 
            // label101
            // 
            this.label101.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label101.Location = new System.Drawing.Point(56, 40);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(42, 16);
            this.label101.TabIndex = 7;
            this.label101.Text = "Å^3";
            // 
            // panelEOS
            // 
            this.panelEOS.Controls.Add(this.flowLayoutPanelEOS);
            this.panelEOS.Controls.Add(this.panel2);
            this.panelEOS.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEOS.Location = new System.Drawing.Point(0, 48);
            this.panelEOS.Name = "panelEOS";
            this.panelEOS.Size = new System.Drawing.Size(725, 82);
            this.panelEOS.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label103);
            this.panel2.Controls.Add(this.textBoxT);
            this.panel2.Controls.Add(this.label102);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(725, 26);
            this.panel2.TabIndex = 19;
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label103.Location = new System.Drawing.Point(7, 5);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(87, 14);
            this.label103.TabIndex = 18;
            this.label103.Text = "Temperature";
            // 
            // textBoxT
            // 
            this.textBoxT.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxT.Location = new System.Drawing.Point(94, 2);
            this.textBoxT.Name = "textBoxT";
            this.textBoxT.Size = new System.Drawing.Size(83, 22);
            this.textBoxT.TabIndex = 17;
            this.textBoxT.Text = "300";
            this.textBoxT.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label102
            // 
            this.label102.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label102.Location = new System.Drawing.Point(183, 5);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(16, 16);
            this.label102.TabIndex = 18;
            this.label102.Text = "K";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(725, 24);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readToolStripMenuItem,
            this.exportAsCSVToolStripMenuItem,
            this.watchNewFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // readToolStripMenuItem
            // 
            this.readToolStripMenuItem.Name = "readToolStripMenuItem";
            this.readToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.readToolStripMenuItem.Text = "Load";
            this.readToolStripMenuItem.Click += new System.EventHandler(this.menuItemFileRead_Click);
            // 
            // exportAsCSVToolStripMenuItem
            // 
            this.exportAsCSVToolStripMenuItem.Name = "exportAsCSVToolStripMenuItem";
            this.exportAsCSVToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.exportAsCSVToolStripMenuItem.Text = "Export as CSV";
            this.exportAsCSVToolStripMenuItem.Click += new System.EventHandler(this.menuItemExport_Click);
            // 
            // watchNewFileToolStripMenuItem
            // 
            this.watchNewFileToolStripMenuItem.Checked = true;
            this.watchNewFileToolStripMenuItem.CheckOnClick = true;
            this.watchNewFileToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.watchNewFileToolStripMenuItem.Name = "watchNewFileToolStripMenuItem";
            this.watchNewFileToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
            this.watchNewFileToolStripMenuItem.Text = "Reload the file if it is updated";
            this.watchNewFileToolStripMenuItem.CheckedChanged += new System.EventHandler(this.menuItemWatchFile_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
            this.ClientSize = new System.Drawing.Size(725, 794);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBoxAkahama2006);
            this.Controls.Add(this.groupBoxMao);
            this.Controls.Add(this.panelEOS);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Pressure Calculator";
            this.Closed += new System.EventHandler(this.FormDiamondRaman_Closed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDifferentiationRunningAverage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOriginalRunningAverage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOriginalGaussian)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDifferentiationGaussian)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFitRange)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBoxMao.ResumeLayout(false);
            this.groupBoxMao.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxAkahama2006.ResumeLayout(false);
            this.groupBoxAkahama2006.PerformLayout();
            this.flowLayoutPanelEOS.ResumeLayout(false);
            this.groupBoxGold.ResumeLayout(false);
            this.groupBoxGold.PerformLayout();
            this.groupBoxPlatinum.ResumeLayout(false);
            this.groupBoxPlatinum.PerformLayout();
            this.groupBoxNaClB1.ResumeLayout(false);
            this.groupBoxNaClB1.PerformLayout();
            this.groupBoxNaClB2.ResumeLayout(false);
            this.groupBoxNaClB2.PerformLayout();
            this.groupBoxPericlase.ResumeLayout(false);
            this.groupBoxPericlase.PerformLayout();
            this.groupBoxCorundum.ResumeLayout(false);
            this.groupBoxCorundum.PerformLayout();
            this.groupBoxAr.ResumeLayout(false);
            this.groupBoxAr.PerformLayout();
            this.groupBoxRe.ResumeLayout(false);
            this.groupBoxRe.PerformLayout();
            this.panelEOS.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
	
    

}
