﻿using System;
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
	public class FormMain : System.Windows.Forms.Form
    {

        #region コントロール
        public Profile Original,BottomProfile;
		public Profile OriginalSmooth,BottomProfileSmooth;
		public double UppestKayser,UpperKayser,LowestKayser,LowerKayser,UppestCount,UpperCount,LowestCount,LowerCount,UpperDiff,LowerDiff,UppestDiff,LowestDiff;
		public Bitmap BmpOriginal,BmpDifferentiation;
		public Graphics gOriginal,gDifferentiation;
		public bool MouseRange=true;
		
		public string FileName="";
		public Thread thread;
        public DateTime time, oldTime;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItemFileRead;
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
        private System.Windows.Forms.TextBox textBoxAkahama2004P;
		private System.Windows.Forms.MenuItem menuItemWatchFile;
		private System.Windows.Forms.MenuItem menuItem3;
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
        private TextBox textBoxAkahama2006Nu;
        private Label label30;
        private TextBox textBoxAkahama2006K0;
        private TextBox textBoxAkahama2006K0Prime;
        private Label label31;
        private Label label32;
        private TextBox textBoxAkahama2006P;
        private Label label35;
        private TextBox textBoxMaoQuasiP;
        private Label label42;
        private Label label44;
        private TextBox textBoxAkahama2006Nu0;
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
        private MenuItem menuItemExport;
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
        private IContainer components;
        #endregion
        public FormMain() {
			//
			// Windows フォーム デザイナ サポートに必要です。
			//
			InitializeComponent();

			//
			// TODO: InitializeComponent 呼び出しの後に、コンストラクタ コードを追加してください。
			//
		}

		/// <summary>
		/// 使用されているリソースに後処理を実行します。
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

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
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemFileRead = new System.Windows.Forms.MenuItem();
            this.menuItemExport = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemWatchFile = new System.Windows.Forms.MenuItem();
            this.textBoxAkahama2004A = new System.Windows.Forms.TextBox();
            this.textBoxAkahama2004B = new System.Windows.Forms.TextBox();
            this.textBoxAkahama2004C = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxAkahama2004P = new System.Windows.Forms.TextBox();
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
            this.groupBoxAkahama2006 = new System.Windows.Forms.GroupBox();
            this.textBoxAkahama2006P = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.textBoxAkahama2006Nu0 = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.textBoxAkahama2006Nu = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.textBoxAkahama2006K0 = new System.Windows.Forms.TextBox();
            this.textBoxAkahama2006K0Prime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
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
            this.SuspendLayout();
            // 
            // numericUpDownDifferentiationRunningAverage
            // 
            this.numericUpDownDifferentiationRunningAverage.AutoSize = true;
            this.numericUpDownDifferentiationRunningAverage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.numericUpDownOriginalRunningAverage.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(126, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Running Average";
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFileRead,
            this.menuItemExport,
            this.menuItem3,
            this.menuItemWatchFile});
            this.menuItem1.Text = "File";
            // 
            // menuItemFileRead
            // 
            this.menuItemFileRead.Index = 0;
            this.menuItemFileRead.Text = "Read";
            this.menuItemFileRead.Click += new System.EventHandler(this.menuItemFileRead_Click);
            // 
            // menuItemExport
            // 
            this.menuItemExport.Index = 1;
            this.menuItemExport.Text = "Export CSV file";
            this.menuItemExport.Click += new System.EventHandler(this.menuItemExport_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.Text = "-";
            // 
            // menuItemWatchFile
            // 
            this.menuItemWatchFile.Index = 3;
            this.menuItemWatchFile.Text = "Watch a new file";
            this.menuItemWatchFile.Click += new System.EventHandler(this.menuItemWatchFile_Click);
            // 
            // textBoxAkahama2004A
            // 
            this.textBoxAkahama2004A.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAkahama2004A.Location = new System.Drawing.Point(297, 21);
            this.textBoxAkahama2004A.Name = "textBoxAkahama2004A";
            this.textBoxAkahama2004A.Size = new System.Drawing.Size(44, 21);
            this.textBoxAkahama2004A.TabIndex = 0;
            this.textBoxAkahama2004A.Text = "66.9";
            this.textBoxAkahama2004A.TextChanged += new System.EventHandler(this.textBoxNu_TextChanged);
            // 
            // textBoxAkahama2004B
            // 
            this.textBoxAkahama2004B.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAkahama2004B.Location = new System.Drawing.Point(353, 21);
            this.textBoxAkahama2004B.Name = "textBoxAkahama2004B";
            this.textBoxAkahama2004B.Size = new System.Drawing.Size(44, 21);
            this.textBoxAkahama2004B.TabIndex = 0;
            this.textBoxAkahama2004B.Text = "-0.5281";
            this.textBoxAkahama2004B.TextChanged += new System.EventHandler(this.textBoxNu_TextChanged);
            // 
            // textBoxAkahama2004C
            // 
            this.textBoxAkahama2004C.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAkahama2004C.Location = new System.Drawing.Point(421, 21);
            this.textBoxAkahama2004C.Name = "textBoxAkahama2004C";
            this.textBoxAkahama2004C.Size = new System.Drawing.Size(44, 21);
            this.textBoxAkahama2004C.TabIndex = 0;
            this.textBoxAkahama2004C.Text = "3.585";
            this.textBoxAkahama2004C.TextChanged += new System.EventHandler(this.textBoxNu_TextChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(268, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 12);
            this.label5.TabIndex = 1;
            this.label5.Text = "P=";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(341, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "+";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(397, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "ν+";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(465, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "E-4 ν^2=";
            // 
            // textBoxAkahama2004P
            // 
            this.textBoxAkahama2004P.BackColor = System.Drawing.Color.Navy;
            this.textBoxAkahama2004P.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAkahama2004P.ForeColor = System.Drawing.Color.White;
            this.textBoxAkahama2004P.Location = new System.Drawing.Point(608, 15);
            this.textBoxAkahama2004P.Name = "textBoxAkahama2004P";
            this.textBoxAkahama2004P.ReadOnly = true;
            this.textBoxAkahama2004P.Size = new System.Drawing.Size(71, 26);
            this.textBoxAkahama2004P.TabIndex = 0;
            this.textBoxAkahama2004P.Text = "0";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(685, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "GPa";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.Control;
            this.label14.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.numericUpDownOriginalGaussian.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label12.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.labelBottomTitle.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label13.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.numericUpDownDifferentiationGaussian.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.splitContainer1.Location = new System.Drawing.Point(0, 106);
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
            this.splitContainer1.Size = new System.Drawing.Size(725, 407);
            this.splitContainer1.SplitterDistance = 189;
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
            this.graphControlTop.Font = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.graphControlTop.Size = new System.Drawing.Size(725, 161);
            this.graphControlTop.Smoothing = false;
            this.graphControlTop.TabIndex = 5;
            this.graphControlTop.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.graphControlBottom.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.graphControlBottom.Size = new System.Drawing.Size(725, 186);
            this.graphControlBottom.Smoothing = false;
            this.graphControlBottom.TabIndex = 6;
            this.graphControlBottom.TextFont = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label10.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.numericUpDownFitRange.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.labelDimension.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.radioButtonDiamondRaman.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonDiamondRaman.Location = new System.Drawing.Point(3, 3);
            this.radioButtonDiamondRaman.Name = "radioButtonDiamondRaman";
            this.radioButtonDiamondRaman.Size = new System.Drawing.Size(127, 18);
            this.radioButtonDiamondRaman.TabIndex = 7;
            this.radioButtonDiamondRaman.TabStop = true;
            this.radioButtonDiamondRaman.Text = "Diamond Raman";
            this.radioButtonDiamondRaman.UseVisualStyleBackColor = true;
            this.radioButtonDiamondRaman.CheckedChanged += new System.EventHandler(this.radioButtonDiamondRaman_CheckedChanged);
            // 
            // radioButtonRubyFluorescence
            // 
            this.radioButtonRubyFluorescence.AutoSize = true;
            this.radioButtonRubyFluorescence.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonRubyFluorescence.Location = new System.Drawing.Point(136, 3);
            this.radioButtonRubyFluorescence.Name = "radioButtonRubyFluorescence";
            this.radioButtonRubyFluorescence.Size = new System.Drawing.Size(142, 18);
            this.radioButtonRubyFluorescence.TabIndex = 7;
            this.radioButtonRubyFluorescence.Text = "Ruby Fluorescence";
            this.radioButtonRubyFluorescence.UseVisualStyleBackColor = true;
            this.radioButtonRubyFluorescence.CheckedChanged += new System.EventHandler(this.radioButtonDiamondRaman_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.radioButtonDiamondRaman);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonRubyFluorescence);
            this.flowLayoutPanel1.Controls.Add(this.radioButtonEOS);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(725, 24);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // radioButtonEOS
            // 
            this.radioButtonEOS.AutoSize = true;
            this.radioButtonEOS.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButtonEOS.Location = new System.Drawing.Point(284, 3);
            this.radioButtonEOS.Name = "radioButtonEOS";
            this.radioButtonEOS.Size = new System.Drawing.Size(51, 18);
            this.radioButtonEOS.TabIndex = 7;
            this.radioButtonEOS.Text = "EOS";
            this.radioButtonEOS.UseVisualStyleBackColor = true;
            this.radioButtonEOS.CheckedChanged += new System.EventHandler(this.radioButtonDiamondRaman_CheckedChanged);
            // 
            // textBoxMaoHydroP
            // 
            this.textBoxMaoHydroP.BackColor = System.Drawing.Color.Navy;
            this.textBoxMaoHydroP.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label28.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(683, 23);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(31, 17);
            this.label28.TabIndex = 1;
            this.label28.Text = "GPa";
            // 
            // groupBoxMao
            // 
            this.groupBoxMao.Controls.Add(this.groupBox4);
            this.groupBoxMao.Controls.Add(this.groupBox2);
            this.groupBoxMao.Controls.Add(this.groupBox3);
            this.groupBoxMao.Controls.Add(this.groupBox1);
            this.groupBoxMao.Controls.Add(this.numericBoxRubyR1);
            this.groupBoxMao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxMao.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxMao.Location = new System.Drawing.Point(0, 585);
            this.groupBoxMao.Name = "groupBoxMao";
            this.groupBoxMao.Size = new System.Drawing.Size(725, 251);
            this.groupBoxMao.TabIndex = 12;
            this.groupBoxMao.TabStop = false;
            this.groupBoxMao.Text = "Pressure calculation from the ruby fluorescence";
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
            this.groupBox4.Location = new System.Drawing.Point(3, 166);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(719, 82);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Pressure calculation, where x = R1/R1₀ ,  f(x, y) = (x^y-1)/y ,  Δ=R1-R1₀";
            // 
            // textBoxMaoP
            // 
            this.textBoxMaoP.BackColor = System.Drawing.Color.Navy;
            this.textBoxMaoP.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.numericBoxMaoA.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxMaoA.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxMaoA.FooterText = "f(x, 5)=";
            this.numericBoxMaoA.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxMaoA.HeaderText = "P=";
            this.numericBoxMaoA.Location = new System.Drawing.Point(118, 22);
            this.numericBoxMaoA.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxMaoA.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxMaoA.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxMaoA.Name = "numericBoxMaoA";
            this.numericBoxMaoA.RadianValue = 33.231068957972035D;
            this.numericBoxMaoA.Size = new System.Drawing.Size(103, 25);
            this.numericBoxMaoA.TabIndex = 3;
            this.numericBoxMaoA.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxMaoA.Value = 1904D;
            // 
            // numericBoxMaoQuasiA
            // 
            this.numericBoxMaoQuasiA.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxMaoQuasiA.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxMaoQuasiA.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxMaoQuasiA.FooterText = "f(x, 7.665)=";
            this.numericBoxMaoQuasiA.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxMaoQuasiA.HeaderText = "P=";
            this.numericBoxMaoQuasiA.Location = new System.Drawing.Point(117, 49);
            this.numericBoxMaoQuasiA.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxMaoQuasiA.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxMaoQuasiA.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxMaoQuasiA.Name = "numericBoxMaoQuasiA";
            this.numericBoxMaoQuasiA.RadianValue = 33.231068957972035D;
            this.numericBoxMaoQuasiA.Size = new System.Drawing.Size(123, 25);
            this.numericBoxMaoQuasiA.TabIndex = 3;
            this.numericBoxMaoQuasiA.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxMaoQuasiA.Value = 1904D;
            // 
            // numericBoxShenA
            // 
            this.numericBoxShenA.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxShenA.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxShenA.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxShenA.FooterText = "(Δ+5.63 Δ²)=";
            this.numericBoxShenA.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxShenA.HeaderText = "P=";
            this.numericBoxShenA.Location = new System.Drawing.Point(485, 49);
            this.numericBoxShenA.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxShenA.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxShenA.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxShenA.Name = "numericBoxShenA";
            this.numericBoxShenA.RadianValue = 32.637657012293964D;
            this.numericBoxShenA.Size = new System.Drawing.Size(131, 25);
            this.numericBoxShenA.TabIndex = 3;
            this.numericBoxShenA.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxShenA.Value = 1870D;
            // 
            // numericBoxMaoHydroA
            // 
            this.numericBoxMaoHydroA.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxMaoHydroA.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxMaoHydroA.FooterFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxMaoHydroA.FooterText = "f(x, 7.715)=";
            this.numericBoxMaoHydroA.HeaderFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxMaoHydroA.HeaderText = "P=";
            this.numericBoxMaoHydroA.Location = new System.Drawing.Point(485, 22);
            this.numericBoxMaoHydroA.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxMaoHydroA.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxMaoHydroA.MinimumSize = new System.Drawing.Size(1, 23);
            this.numericBoxMaoHydroA.Name = "numericBoxMaoHydroA";
            this.numericBoxMaoHydroA.RadianValue = 33.231068957972035D;
            this.numericBoxMaoHydroA.Size = new System.Drawing.Size(123, 25);
            this.numericBoxMaoHydroA.TabIndex = 3;
            this.numericBoxMaoHydroA.TextFont = new System.Drawing.Font("Segoe UI Symbol", 9F);
            this.numericBoxMaoHydroA.Value = 1904D;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(305, 50);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(31, 17);
            this.label42.TabIndex = 1;
            this.label42.Text = "GPa";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(305, 23);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 17);
            this.label18.TabIndex = 1;
            this.label18.Text = "GPa";
            // 
            // textBoxMaoQuasiP
            // 
            this.textBoxMaoQuasiP.BackColor = System.Drawing.Color.Navy;
            this.textBoxMaoQuasiP.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label107.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label107.Location = new System.Drawing.Point(367, 50);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(108, 17);
            this.label107.TabIndex = 1;
            this.label107.Text = "Shen et al. (2020)";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(367, 23);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(114, 17);
            this.label46.TabIndex = 1;
            this.label46.Text = "Mao-hydro (2000)";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label45.Location = new System.Drawing.Point(6, 50);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(111, 17);
            this.label45.TabIndex = 1;
            this.label45.Text = "Mao-quasi (1986)";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(6, 23);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(75, 17);
            this.label34.TabIndex = 1;
            this.label34.Text = "Mao (1978)";
            // 
            // textBoxShenP
            // 
            this.textBoxShenP.BackColor = System.Drawing.Color.Navy;
            this.textBoxShenP.Font = new System.Drawing.Font("Segoe UI Symbol", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label104.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.groupBox2.Text = "Temperature dependency (Ragan et al. 1992)";
            // 
            // numericBoxRubyRagan
            // 
            this.numericBoxRubyRagan.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxRubyRagan.DecimalPlaces = 3;
            this.numericBoxRubyRagan.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxRubyRagan.FooterText = "+ 4.49×10⁻²×T - 4.81×10⁻⁴×T² + 3.71×10⁻⁷×T³ )⁻¹ ×10⁷    [nm]";
            this.numericBoxRubyRagan.HeaderText = "R1(T, P=0) = (";
            this.numericBoxRubyRagan.Location = new System.Drawing.Point(9, 18);
            this.numericBoxRubyRagan.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxRubyRagan.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxRubyRagan.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxRubyRagan.Name = "numericBoxRubyRagan";
            this.numericBoxRubyRagan.RadianValue = 251.72883801514215D;
            this.numericBoxRubyRagan.Size = new System.Drawing.Size(542, 25);
            this.numericBoxRubyRagan.TabIndex = 3;
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
            this.checkBoxRubyR1_0CalculatedFromRagan.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
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
            this.numericBoxRubyT.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxRubyT.FooterText = "K";
            this.numericBoxRubyT.HeaderText = "T =";
            this.numericBoxRubyT.Location = new System.Drawing.Point(11, 18);
            this.numericBoxRubyT.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxRubyT.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxRubyT.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxRubyT.Name = "numericBoxRubyT";
            this.numericBoxRubyT.RadianValue = 5.1164327022213767D;
            this.numericBoxRubyT.Size = new System.Drawing.Size(103, 25);
            this.numericBoxRubyT.TabIndex = 3;
            this.numericBoxRubyT.Value = 293.15D;
            this.numericBoxRubyT.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxRubyT_ValueChanged);
            // 
            // checkBoxRubyTemeratureSameAsRef
            // 
            this.checkBoxRubyTemeratureSameAsRef.AutoSize = true;
            this.checkBoxRubyTemeratureSameAsRef.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.checkBoxRubyTemeratureSameAsRef.Location = new System.Drawing.Point(118, 20);
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
            this.numericBoxRubyR1_0.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxRubyR1_0.FooterText = "nm";
            this.numericBoxRubyR1_0.HeaderText = "R1₀=R1(T,P=0)";
            this.numericBoxRubyR1_0.Location = new System.Drawing.Point(11, 52);
            this.numericBoxRubyR1_0.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxRubyR1_0.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxRubyR1_0.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxRubyR1_0.Name = "numericBoxRubyR1_0";
            this.numericBoxRubyR1_0.RadianValue = 12.12392964897861D;
            this.numericBoxRubyR1_0.Size = new System.Drawing.Size(171, 25);
            this.numericBoxRubyR1_0.TabIndex = 3;
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
            this.buttonRubyRefR1Set.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
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
            this.numericBoxRubyRefT.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxRubyRefT.FooterText = "K";
            this.numericBoxRubyRefT.Location = new System.Drawing.Point(46, 21);
            this.numericBoxRubyRefT.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxRubyRefT.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxRubyRefT.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxRubyRefT.Name = "numericBoxRubyRefT";
            this.numericBoxRubyRefT.RadianValue = 5.1164327022213767D;
            this.numericBoxRubyRefT.Size = new System.Drawing.Size(63, 25);
            this.numericBoxRubyRefT.TabIndex = 3;
            this.numericBoxRubyRefT.Value = 293.15D;
            this.numericBoxRubyRefT.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxRubyRefT_ValueChanged);
            // 
            // numericBoxRubyRefR1
            // 
            this.numericBoxRubyRefR1.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxRubyRefR1.DecimalPlaces = 3;
            this.numericBoxRubyRefR1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxRubyRefR1.FooterText = "nm";
            this.numericBoxRubyRefR1.HeaderText = "R1(T=                , P=0) = ";
            this.numericBoxRubyRefR1.Location = new System.Drawing.Point(7, 21);
            this.numericBoxRubyRefR1.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxRubyRefR1.MaximumSize = new System.Drawing.Size(1000, 25);
            this.numericBoxRubyRefR1.MinimumSize = new System.Drawing.Size(1, 25);
            this.numericBoxRubyRefR1.Name = "numericBoxRubyRefR1";
            this.numericBoxRubyRefR1.RadianValue = 12.116948331970631D;
            this.numericBoxRubyRefR1.Size = new System.Drawing.Size(243, 25);
            this.numericBoxRubyRefR1.TabIndex = 3;
            this.numericBoxRubyRefR1.Value = 694.25D;
            this.numericBoxRubyRefR1.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxRubyRefR1_ValueChanged);
            // 
            // numericBoxRubyR1
            // 
            this.numericBoxRubyR1.BackColor = System.Drawing.Color.Transparent;
            this.numericBoxRubyR1.DecimalPlaces = 3;
            this.numericBoxRubyR1.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F);
            this.numericBoxRubyR1.FooterText = "nm";
            this.numericBoxRubyR1.HeaderText = "R1";
            this.numericBoxRubyR1.Location = new System.Drawing.Point(8, 28);
            this.numericBoxRubyR1.Margin = new System.Windows.Forms.Padding(0);
            this.numericBoxRubyR1.MaximumSize = new System.Drawing.Size(1000, 29);
            this.numericBoxRubyR1.MinimumSize = new System.Drawing.Size(1, 27);
            this.numericBoxRubyR1.Name = "numericBoxRubyR1";
            this.numericBoxRubyR1.RadianValue = 12.12392964897861D;
            this.numericBoxRubyR1.Size = new System.Drawing.Size(125, 29);
            this.numericBoxRubyR1.TabIndex = 3;
            this.numericBoxRubyR1.TextFont = new System.Drawing.Font("Segoe UI Symbol", 11F);
            this.numericBoxRubyR1.Value = 694.65D;
            this.numericBoxRubyR1.ValueChanged += new Crystallography.Controls.NumericBox.MyEventHandler(this.numericBoxRubyR1_ValueChanged);
            // 
            // groupBoxAkahama2006
            // 
            this.groupBoxAkahama2006.Controls.Add(this.textBoxAkahama2006P);
            this.groupBoxAkahama2006.Controls.Add(this.label44);
            this.groupBoxAkahama2006.Controls.Add(this.label29);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxAkahama2004A);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxAkahama2004B);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxAkahama2006Nu0);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxAkahama2004C);
            this.groupBoxAkahama2006.Controls.Add(this.label43);
            this.groupBoxAkahama2006.Controls.Add(this.label5);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxAkahama2006Nu);
            this.groupBoxAkahama2006.Controls.Add(this.label6);
            this.groupBoxAkahama2006.Controls.Add(this.label30);
            this.groupBoxAkahama2006.Controls.Add(this.label7);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxAkahama2006K0);
            this.groupBoxAkahama2006.Controls.Add(this.label8);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxAkahama2006K0Prime);
            this.groupBoxAkahama2006.Controls.Add(this.textBoxAkahama2004P);
            this.groupBoxAkahama2006.Controls.Add(this.label4);
            this.groupBoxAkahama2006.Controls.Add(this.label3);
            this.groupBoxAkahama2006.Controls.Add(this.label31);
            this.groupBoxAkahama2006.Controls.Add(this.label9);
            this.groupBoxAkahama2006.Controls.Add(this.label33);
            this.groupBoxAkahama2006.Controls.Add(this.label32);
            this.groupBoxAkahama2006.Controls.Add(this.label35);
            this.groupBoxAkahama2006.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxAkahama2006.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxAkahama2006.Location = new System.Drawing.Point(0, 513);
            this.groupBoxAkahama2006.Name = "groupBoxAkahama2006";
            this.groupBoxAkahama2006.Size = new System.Drawing.Size(725, 72);
            this.groupBoxAkahama2006.TabIndex = 13;
            this.groupBoxAkahama2006.TabStop = false;
            this.groupBoxAkahama2006.Text = "Pressure calculation from the Raman edge";
            // 
            // textBoxAkahama2006P
            // 
            this.textBoxAkahama2006P.BackColor = System.Drawing.Color.Navy;
            this.textBoxAkahama2006P.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAkahama2006P.ForeColor = System.Drawing.Color.White;
            this.textBoxAkahama2006P.Location = new System.Drawing.Point(608, 40);
            this.textBoxAkahama2006P.Name = "textBoxAkahama2006P";
            this.textBoxAkahama2006P.ReadOnly = true;
            this.textBoxAkahama2006P.Size = new System.Drawing.Size(71, 26);
            this.textBoxAkahama2006P.TabIndex = 0;
            this.textBoxAkahama2006P.Text = "0";
            // 
            // label44
            // 
            this.label44.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label44.Location = new System.Drawing.Point(6, 43);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(24, 12);
            this.label44.TabIndex = 1;
            this.label44.Text = "ν0";
            // 
            // label29
            // 
            this.label29.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(4, 20);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(16, 12);
            this.label29.TabIndex = 1;
            this.label29.Text = "ν";
            // 
            // textBoxAkahama2006Nu0
            // 
            this.textBoxAkahama2006Nu0.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAkahama2006Nu0.Location = new System.Drawing.Point(36, 39);
            this.textBoxAkahama2006Nu0.Name = "textBoxAkahama2006Nu0";
            this.textBoxAkahama2006Nu0.Size = new System.Drawing.Size(63, 22);
            this.textBoxAkahama2006Nu0.TabIndex = 0;
            this.textBoxAkahama2006Nu0.Text = "1334";
            this.textBoxAkahama2006Nu0.TextChanged += new System.EventHandler(this.textBoxNu_TextChanged);
            // 
            // label43
            // 
            this.label43.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label43.Location = new System.Drawing.Point(105, 46);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(52, 12);
            this.label43.TabIndex = 1;
            this.label43.Text = "cm^-1";
            // 
            // textBoxAkahama2006Nu
            // 
            this.textBoxAkahama2006Nu.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAkahama2006Nu.Location = new System.Drawing.Point(35, 16);
            this.textBoxAkahama2006Nu.Name = "textBoxAkahama2006Nu";
            this.textBoxAkahama2006Nu.Size = new System.Drawing.Size(64, 22);
            this.textBoxAkahama2006Nu.TabIndex = 0;
            this.textBoxAkahama2006Nu.Text = "0";
            this.textBoxAkahama2006Nu.TextChanged += new System.EventHandler(this.textBoxNu_TextChanged);
            // 
            // label30
            // 
            this.label30.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(105, 20);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(52, 12);
            this.label30.TabIndex = 1;
            this.label30.Text = "cm^-1";
            // 
            // textBoxAkahama2006K0
            // 
            this.textBoxAkahama2006K0.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAkahama2006K0.Location = new System.Drawing.Point(301, 43);
            this.textBoxAkahama2006K0.Name = "textBoxAkahama2006K0";
            this.textBoxAkahama2006K0.Size = new System.Drawing.Size(44, 21);
            this.textBoxAkahama2006K0.TabIndex = 0;
            this.textBoxAkahama2006K0.Text = "547";
            this.textBoxAkahama2006K0.TextChanged += new System.EventHandler(this.textBoxNu_TextChanged);
            // 
            // textBoxAkahama2006K0Prime
            // 
            this.textBoxAkahama2006K0Prime.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAkahama2006K0Prime.Location = new System.Drawing.Point(468, 44);
            this.textBoxAkahama2006K0Prime.Name = "textBoxAkahama2006K0Prime";
            this.textBoxAkahama2006K0Prime.Size = new System.Drawing.Size(44, 21);
            this.textBoxAkahama2006K0Prime.TabIndex = 0;
            this.textBoxAkahama2006K0Prime.Text = "3.75";
            this.textBoxAkahama2006K0Prime.TextChanged += new System.EventHandler(this.textBoxNu_TextChanged);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(157, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "Akahama (2004)";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(157, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "Akahama (2006)";
            // 
            // label31
            // 
            this.label31.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(268, 47);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(31, 12);
            this.label31.TabIndex = 1;
            this.label31.Text = "P=K0";
            // 
            // label33
            // 
            this.label33.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.Location = new System.Drawing.Point(518, 47);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(101, 12);
            this.label33.TabIndex = 1;
            this.label33.Text = "-1)*Δν/ν0]=";
            // 
            // label32
            // 
            this.label32.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(347, 47);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(198, 12);
            this.label32.TabIndex = 1;
            this.label32.Text = "*Δν/ν0 * [1+1/2 (K0\'";
            // 
            // label35
            // 
            this.label35.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(686, 46);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(34, 12);
            this.label35.TabIndex = 1;
            this.label35.Text = "GPa";
            // 
            // flowLayoutPanelEOS
            // 
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
            this.groupBoxGold.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.textBoxGoldJamieson.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.textBoxGoldAnderson.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.textBoxGoldTsuchiya.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label50.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(60, 78);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(38, 16);
            this.label50.TabIndex = 7;
            this.label50.Text = "GPa";
            // 
            // textBoxGoldSim
            // 
            this.textBoxGoldSim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxGoldSim.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label11.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(60, 115);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 16);
            this.label11.TabIndex = 7;
            this.label11.Text = "GPa";
            // 
            // label70
            // 
            this.label70.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label70.Location = new System.Drawing.Point(60, 187);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(38, 16);
            this.label70.TabIndex = 7;
            this.label70.Text = "GPa";
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(60, 151);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 16);
            this.label15.TabIndex = 7;
            this.label15.Text = "GPa";
            // 
            // textBoxGold_a
            // 
            this.textBoxGold_a.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxGold_a.Location = new System.Drawing.Point(20, 36);
            this.textBoxGold_a.Name = "textBoxGold_a";
            this.textBoxGold_a.Size = new System.Drawing.Size(48, 21);
            this.textBoxGold_a.TabIndex = 5;
            this.textBoxGold_a.Text = "4.0786";
            this.textBoxGold_a.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxGold_a0
            // 
            this.textBoxGold_a0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxGold_a0.Location = new System.Drawing.Point(20, 16);
            this.textBoxGold_a0.Name = "textBoxGold_a0";
            this.textBoxGold_a0.Size = new System.Drawing.Size(48, 21);
            this.textBoxGold_a0.TabIndex = 5;
            this.textBoxGold_a0.Text = "4.0786";
            this.textBoxGold_a0.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(4, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(20, 12);
            this.label16.TabIndex = 7;
            this.label16.Text = "a0";
            // 
            // label37
            // 
            this.label37.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(68, 20);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(12, 16);
            this.label37.TabIndex = 7;
            this.label37.Text = "Å";
            // 
            // label49
            // 
            this.label49.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label49.Location = new System.Drawing.Point(12, 59);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(88, 19);
            this.label49.TabIndex = 7;
            this.label49.Text = "Jamieson (82)";
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(68, 40);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(12, 16);
            this.label23.TabIndex = 7;
            this.label23.Text = "Å";
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(12, 96);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 19);
            this.label22.TabIndex = 7;
            this.label22.Text = "Anderson (89)";
            // 
            // label69
            // 
            this.label69.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label69.Location = new System.Drawing.Point(12, 171);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(86, 16);
            this.label69.TabIndex = 7;
            this.label69.Text = "Tsuchiya (03)";
            // 
            // label36
            // 
            this.label36.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(12, 135);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(86, 16);
            this.label36.TabIndex = 7;
            this.label36.Text = "Sim (02)";
            // 
            // label38
            // 
            this.label38.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.groupBoxPlatinum.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPlatinum.Location = new System.Drawing.Point(109, 3);
            this.groupBoxPlatinum.Name = "groupBoxPlatinum";
            this.groupBoxPlatinum.Size = new System.Drawing.Size(98, 136);
            this.groupBoxPlatinum.TabIndex = 13;
            this.groupBoxPlatinum.TabStop = false;
            this.groupBoxPlatinum.Text = "Platinum";
            // 
            // textBoxPtA0
            // 
            this.textBoxPtA0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.textBoxPtJamieson.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.textBoxPtHolems.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label39.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label39.Location = new System.Drawing.Point(60, 80);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(24, 16);
            this.label39.TabIndex = 7;
            this.label39.Text = "GPa";
            // 
            // label47
            // 
            this.label47.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label47.Location = new System.Drawing.Point(4, 20);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(24, 12);
            this.label47.TabIndex = 7;
            this.label47.Text = "a0";
            // 
            // label61
            // 
            this.label61.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label61.Location = new System.Drawing.Point(60, 116);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(24, 16);
            this.label61.TabIndex = 7;
            this.label61.Text = "GPa";
            // 
            // textBoxPtA
            // 
            this.textBoxPtA.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPtA.Location = new System.Drawing.Point(20, 40);
            this.textBoxPtA.Name = "textBoxPtA";
            this.textBoxPtA.Size = new System.Drawing.Size(48, 21);
            this.textBoxPtA.TabIndex = 5;
            this.textBoxPtA.Text = "3.9231";
            this.textBoxPtA.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label54
            // 
            this.label54.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(68, 20);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(12, 16);
            this.label54.TabIndex = 7;
            this.label54.Text = "Å";
            // 
            // label48
            // 
            this.label48.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label48.Location = new System.Drawing.Point(68, 44);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(12, 16);
            this.label48.TabIndex = 7;
            this.label48.Text = "Å";
            // 
            // label51
            // 
            this.label51.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.Location = new System.Drawing.Point(4, 64);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(96, 14);
            this.label51.TabIndex = 7;
            this.label51.Text = "Jamieson (82)";
            // 
            // label52
            // 
            this.label52.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label52.Location = new System.Drawing.Point(12, 44);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(36, 12);
            this.label52.TabIndex = 7;
            this.label52.Text = "a";
            // 
            // label60
            // 
            this.label60.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.groupBoxNaClB1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxNaClB1.Location = new System.Drawing.Point(213, 3);
            this.groupBoxNaClB1.Name = "groupBoxNaClB1";
            this.groupBoxNaClB1.Size = new System.Drawing.Size(98, 100);
            this.groupBoxNaClB1.TabIndex = 16;
            this.groupBoxNaClB1.TabStop = false;
            this.groupBoxNaClB1.Text = "NaCl B1";
            // 
            // textBoxNaClB1_a0
            // 
            this.textBoxNaClB1_a0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNaClB1_a0.Location = new System.Drawing.Point(20, 16);
            this.textBoxNaClB1_a0.Name = "textBoxNaClB1_a0";
            this.textBoxNaClB1_a0.Size = new System.Drawing.Size(48, 21);
            this.textBoxNaClB1_a0.TabIndex = 5;
            this.textBoxNaClB1_a0.Text = "5.63900";
            this.textBoxNaClB1_a0.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxNaClB1_a
            // 
            this.textBoxNaClB1_a.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNaClB1_a.Location = new System.Drawing.Point(20, 36);
            this.textBoxNaClB1_a.Name = "textBoxNaClB1_a";
            this.textBoxNaClB1_a.Size = new System.Drawing.Size(48, 21);
            this.textBoxNaClB1_a.TabIndex = 5;
            this.textBoxNaClB1_a.Text = "4.0786";
            this.textBoxNaClB1_a.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label53
            // 
            this.label53.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(4, 20);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(20, 12);
            this.label53.TabIndex = 7;
            this.label53.Text = "a0";
            // 
            // label55
            // 
            this.label55.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(12, 40);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(16, 12);
            this.label55.TabIndex = 7;
            this.label55.Text = "a";
            // 
            // label56
            // 
            this.label56.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(60, 76);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(32, 16);
            this.label56.TabIndex = 7;
            this.label56.Text = "GPa";
            // 
            // textBoxNaClB1Brown
            // 
            this.textBoxNaClB1Brown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxNaClB1Brown.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label62.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label62.Location = new System.Drawing.Point(68, 20);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(12, 16);
            this.label62.TabIndex = 7;
            this.label62.Text = "Å";
            // 
            // label57
            // 
            this.label57.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label57.Location = new System.Drawing.Point(68, 40);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(12, 16);
            this.label57.TabIndex = 7;
            this.label57.Text = "Å";
            // 
            // label58
            // 
            this.label58.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.groupBoxNaClB2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.textBoxNaClB2_a0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNaClB2_a0.Location = new System.Drawing.Point(20, 16);
            this.textBoxNaClB2_a0.Name = "textBoxNaClB2_a0";
            this.textBoxNaClB2_a0.Size = new System.Drawing.Size(48, 21);
            this.textBoxNaClB2_a0.TabIndex = 5;
            this.textBoxNaClB2_a0.Text = "0";
            this.textBoxNaClB2_a0.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxNaClB2_a
            // 
            this.textBoxNaClB2_a.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.textBoxNaClB2SataMgO.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.textBoxNaClB2SataPt.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label65.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label65.Location = new System.Drawing.Point(4, 20);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(32, 12);
            this.label65.TabIndex = 7;
            this.label65.Text = "a0";
            // 
            // label67
            // 
            this.label67.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.Location = new System.Drawing.Point(60, 108);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(40, 16);
            this.label67.TabIndex = 7;
            this.label67.Text = "GPa";
            // 
            // label59
            // 
            this.label59.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label59.Location = new System.Drawing.Point(60, 72);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(40, 16);
            this.label59.TabIndex = 7;
            this.label59.Text = "GPa";
            // 
            // label63
            // 
            this.label63.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label63.Location = new System.Drawing.Point(12, 40);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(32, 12);
            this.label63.TabIndex = 7;
            this.label63.Text = "a";
            // 
            // label64
            // 
            this.label64.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label64.Location = new System.Drawing.Point(68, 20);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(12, 16);
            this.label64.TabIndex = 7;
            this.label64.Text = "Å";
            // 
            // label66
            // 
            this.label66.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label66.Location = new System.Drawing.Point(68, 40);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(12, 16);
            this.label66.TabIndex = 7;
            this.label66.Text = "Å";
            // 
            // label68
            // 
            this.label68.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label68.Location = new System.Drawing.Point(4, 92);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(76, 16);
            this.label68.TabIndex = 7;
            this.label68.Text = "Sata (02) (MgO)";
            // 
            // label71
            // 
            this.label71.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.groupBoxPericlase.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxPericlase.Location = new System.Drawing.Point(423, 3);
            this.groupBoxPericlase.Name = "groupBoxPericlase";
            this.groupBoxPericlase.Size = new System.Drawing.Size(100, 172);
            this.groupBoxPericlase.TabIndex = 10;
            this.groupBoxPericlase.TabStop = false;
            this.groupBoxPericlase.Text = "Periclase";
            // 
            // label72
            // 
            this.label72.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label72.Location = new System.Drawing.Point(60, 148);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(40, 16);
            this.label72.TabIndex = 7;
            this.label72.Text = "GPa";
            // 
            // label73
            // 
            this.label73.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label73.Location = new System.Drawing.Point(60, 112);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(40, 16);
            this.label73.TabIndex = 7;
            this.label73.Text = "GPa";
            // 
            // textBoxMgOAizawa
            // 
            this.textBoxMgOAizawa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxMgOAizawa.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label74.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label74.Location = new System.Drawing.Point(60, 76);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(40, 16);
            this.label74.TabIndex = 7;
            this.label74.Text = "GPa";
            // 
            // textBoxMgODewaele
            // 
            this.textBoxMgODewaele.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxMgODewaele.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.textBoxMgOJacson.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.textBoxMgOA0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMgOA0.Location = new System.Drawing.Point(20, 16);
            this.textBoxMgOA0.Name = "textBoxMgOA0";
            this.textBoxMgOA0.Size = new System.Drawing.Size(48, 21);
            this.textBoxMgOA0.TabIndex = 5;
            this.textBoxMgOA0.Text = "4.2112";
            this.textBoxMgOA0.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label75
            // 
            this.label75.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label75.Location = new System.Drawing.Point(8, 132);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(84, 12);
            this.label75.TabIndex = 7;
            this.label75.Text = "Aizawa (06)";
            // 
            // label76
            // 
            this.label76.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label76.Location = new System.Drawing.Point(4, 96);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(88, 12);
            this.label76.TabIndex = 7;
            this.label76.Text = "Dewaele (00)";
            // 
            // label77
            // 
            this.label77.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label77.Location = new System.Drawing.Point(4, 60);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(92, 16);
            this.label77.TabIndex = 7;
            this.label77.Text = "Jackson (98)";
            // 
            // label78
            // 
            this.label78.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label78.Location = new System.Drawing.Point(4, 20);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(20, 12);
            this.label78.TabIndex = 7;
            this.label78.Text = "a0";
            // 
            // textBoxMgOA
            // 
            this.textBoxMgOA.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMgOA.Location = new System.Drawing.Point(20, 36);
            this.textBoxMgOA.Name = "textBoxMgOA";
            this.textBoxMgOA.Size = new System.Drawing.Size(48, 21);
            this.textBoxMgOA.TabIndex = 5;
            this.textBoxMgOA.Text = "4.2112";
            this.textBoxMgOA.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label79
            // 
            this.label79.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label79.Location = new System.Drawing.Point(68, 20);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(12, 16);
            this.label79.TabIndex = 7;
            this.label79.Text = "Å";
            // 
            // label80
            // 
            this.label80.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label80.Location = new System.Drawing.Point(4, 40);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(16, 12);
            this.label80.TabIndex = 7;
            this.label80.Text = "a";
            // 
            // label81
            // 
            this.label81.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.groupBoxCorundum.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxCorundum.Location = new System.Drawing.Point(529, 3);
            this.groupBoxCorundum.Name = "groupBoxCorundum";
            this.groupBoxCorundum.Size = new System.Drawing.Size(100, 101);
            this.groupBoxCorundum.TabIndex = 9;
            this.groupBoxCorundum.TabStop = false;
            this.groupBoxCorundum.Text = "Corundum";
            // 
            // label82
            // 
            this.label82.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label82.Location = new System.Drawing.Point(56, 76);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(40, 16);
            this.label82.TabIndex = 7;
            this.label82.Text = "GPa";
            // 
            // textBoxCorundumDubrovinsky
            // 
            this.textBoxCorundumDubrovinsky.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxCorundumDubrovinsky.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.textBoxColundumV0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxColundumV0.Location = new System.Drawing.Point(20, 16);
            this.textBoxColundumV0.Name = "textBoxColundumV0";
            this.textBoxColundumV0.Size = new System.Drawing.Size(36, 21);
            this.textBoxColundumV0.TabIndex = 5;
            this.textBoxColundumV0.Text = "254.6959";
            this.textBoxColundumV0.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label83
            // 
            this.label83.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label83.Location = new System.Drawing.Point(4, 56);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(105, 20);
            this.label83.TabIndex = 7;
            this.label83.Text = "Dubrovinsky (98)";
            // 
            // label84
            // 
            this.label84.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label84.Location = new System.Drawing.Point(4, 20);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(20, 12);
            this.label84.TabIndex = 7;
            this.label84.Text = "V0";
            // 
            // textBoxCorundumV
            // 
            this.textBoxCorundumV.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCorundumV.Location = new System.Drawing.Point(20, 36);
            this.textBoxCorundumV.Name = "textBoxCorundumV";
            this.textBoxCorundumV.Size = new System.Drawing.Size(36, 21);
            this.textBoxCorundumV.TabIndex = 5;
            this.textBoxCorundumV.Text = "254.6959";
            this.textBoxCorundumV.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label85
            // 
            this.label85.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label85.Location = new System.Drawing.Point(56, 20);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(38, 16);
            this.label85.TabIndex = 7;
            this.label85.Text = "Å^3";
            // 
            // label86
            // 
            this.label86.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label86.Location = new System.Drawing.Point(4, 40);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(16, 12);
            this.label86.TabIndex = 7;
            this.label86.Text = "V";
            // 
            // label87
            // 
            this.label87.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.groupBoxAr.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.textBoxArA0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxArA0.Location = new System.Drawing.Point(20, 15);
            this.textBoxArA0.Name = "textBoxArA0";
            this.textBoxArA0.Size = new System.Drawing.Size(48, 21);
            this.textBoxArA0.TabIndex = 5;
            this.textBoxArA0.Text = "0";
            this.textBoxArA0.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxArA
            // 
            this.textBoxArA.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.textBoxArJephcoat.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label88.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label88.Location = new System.Drawing.Point(60, 110);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(38, 16);
            this.label88.TabIndex = 7;
            this.label88.Text = "GPa";
            // 
            // textBoxArRoss
            // 
            this.textBoxArRoss.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxArRoss.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.label89.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label89.Location = new System.Drawing.Point(60, 74);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(38, 16);
            this.label89.TabIndex = 7;
            this.label89.Text = "GPa";
            // 
            // label90
            // 
            this.label90.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label90.Location = new System.Drawing.Point(4, 19);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(32, 12);
            this.label90.TabIndex = 7;
            this.label90.Text = "a0";
            // 
            // label91
            // 
            this.label91.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label91.Location = new System.Drawing.Point(12, 40);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(32, 12);
            this.label91.TabIndex = 7;
            this.label91.Text = "a";
            // 
            // label92
            // 
            this.label92.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label92.Location = new System.Drawing.Point(4, 94);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(94, 12);
            this.label92.TabIndex = 7;
            this.label92.Text = "Jephcoat (98)";
            // 
            // label93
            // 
            this.label93.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label93.Location = new System.Drawing.Point(68, 40);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(12, 16);
            this.label93.TabIndex = 7;
            this.label93.Text = "Å";
            // 
            // label94
            // 
            this.label94.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label94.Location = new System.Drawing.Point(4, 58);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(94, 12);
            this.label94.TabIndex = 7;
            this.label94.Text = "Ross et al. (86)";
            // 
            // label95
            // 
            this.label95.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.groupBoxRe.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxRe.Location = new System.Drawing.Point(113, 223);
            this.groupBoxRe.Name = "groupBoxRe";
            this.groupBoxRe.Size = new System.Drawing.Size(104, 101);
            this.groupBoxRe.TabIndex = 11;
            this.groupBoxRe.TabStop = false;
            this.groupBoxRe.Text = "Re";
            // 
            // label96
            // 
            this.label96.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label96.Location = new System.Drawing.Point(56, 76);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(24, 16);
            this.label96.TabIndex = 7;
            this.label96.Text = "GPa";
            // 
            // textBoxReZha
            // 
            this.textBoxReZha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBoxReZha.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.textBoxReV0.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxReV0.Location = new System.Drawing.Point(20, 16);
            this.textBoxReV0.Name = "textBoxReV0";
            this.textBoxReV0.Size = new System.Drawing.Size(36, 21);
            this.textBoxReV0.TabIndex = 5;
            this.textBoxReV0.Text = "29.42795";
            this.textBoxReV0.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label97
            // 
            this.label97.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label97.Location = new System.Drawing.Point(4, 56);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(100, 20);
            this.label97.TabIndex = 7;
            this.label97.Text = "Zha et al. (04)";
            // 
            // label98
            // 
            this.label98.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label98.Location = new System.Drawing.Point(4, 20);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(20, 12);
            this.label98.TabIndex = 7;
            this.label98.Text = "V0";
            // 
            // textBoxReV
            // 
            this.textBoxReV.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxReV.Location = new System.Drawing.Point(20, 36);
            this.textBoxReV.Name = "textBoxReV";
            this.textBoxReV.Size = new System.Drawing.Size(36, 21);
            this.textBoxReV.TabIndex = 5;
            this.textBoxReV.Text = "254.6959";
            this.textBoxReV.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label99
            // 
            this.label99.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label99.Location = new System.Drawing.Point(56, 20);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(42, 16);
            this.label99.TabIndex = 7;
            this.label99.Text = "Å^3";
            // 
            // label100
            // 
            this.label100.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label100.Location = new System.Drawing.Point(4, 40);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(16, 12);
            this.label100.TabIndex = 7;
            this.label100.Text = "V";
            // 
            // label101
            // 
            this.label101.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.panelEOS.Location = new System.Drawing.Point(0, 24);
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
            this.label103.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label103.Location = new System.Drawing.Point(7, 3);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(87, 14);
            this.label103.TabIndex = 18;
            this.label103.Text = "Temperature";
            // 
            // textBoxT
            // 
            this.textBoxT.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxT.Location = new System.Drawing.Point(94, -1);
            this.textBoxT.Name = "textBoxT";
            this.textBoxT.Size = new System.Drawing.Size(83, 22);
            this.textBoxT.TabIndex = 17;
            this.textBoxT.Text = "300";
            this.textBoxT.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label102
            // 
            this.label102.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label102.Location = new System.Drawing.Point(183, 5);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(16, 16);
            this.label102.TabIndex = 18;
            this.label102.Text = "K";
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 15);
            this.ClientSize = new System.Drawing.Size(725, 836);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBoxAkahama2006);
            this.Controls.Add(this.groupBoxMao);
            this.Controls.Add(this.panelEOS);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu = this.mainMenu;
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
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main() {
			Application.Run(new FormMain());
		}
		
        private double fittingRangeDiamond=6;
        private double fittingRangeRuby = 3;

        private void FormMain_Load(object sender, System.EventArgs e)
        {

            MouseRange = false;
            menuItemWatchFile.Checked = true;
            thread = new Thread(new ThreadStart(WatchFile));
            time = oldTime = DateTime.Now;
            thread.Start();        // スレッドの起動

            ReadInitialRegistry();

            radioButtonDiamondRaman_CheckedChanged(new object(), new EventArgs());

            numericBoxRubyRefR1_ValueChanged(sender, e);
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveInitialRegistry();
        }

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

                this.textBoxAkahama2006Nu0.Text = (string)regKey.GetValue("textBoxAkahama2006Nu0.Text", textBoxAkahama2006Nu0.Text);
                numericBoxRubyR1_0.Text = (string)regKey.GetValue("textBoxMaoRamda0.Text", numericBoxRubyR1_0.Text);

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
            regKey.SetValue("textBoxAkahama2006Nu0.Text", textBoxAkahama2006Nu0.Text);
            regKey.SetValue("textBoxMaoRamda0.Text", numericBoxRubyR1_0.Text);


            regKey.Close();
        }
        #endregion


		//メニューから呼び出されるFileRead
		private void menuItemFileRead_Click(object sender, System.EventArgs e) {
			OpenFileDialog Dlg = new OpenFileDialog();
			//Dlg.Filter= "WinSpec Converted Data(txt)|*.txt";
			if(Dlg.ShowDialog()==DialogResult.OK) {
				try {
					if (!File.Exists(Dlg.FileName)) return ;  // ファイルの有無をチェック
					FileName=Dlg.FileName;
					this.Text="Diamond Raman    "+ FileName;
					time =oldTime= File.GetLastWriteTime(FileName);
                    Original = readFile(Dlg.FileName);
					CalcSmoothingAndDifferentiation();
                    graphControlTop.Profile = OriginalSmooth;
                    graphControlBottom.Profile = BottomProfileSmooth;					//プロファイル描画
                    SearchBandEdge();
				}
				catch {
					MessageBox.Show("ファイルを開けません");
					return;
				}
			}
		}

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            //コントロール内にドロップされたとき実行される
            //ドロップされたすべてのファイル名を取得する
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            //ListBoxに追加する
            if (fileName.Length == 1)
            {
                skipFitting = true;
                Original = readFile(fileName[0]);
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


        #region ファイル読み込み関連
        //監視スレッドから呼び出されるFileRead
        private void FileRead() {
			try {
                Original = readFile(FileName);
				CalcSmoothingAndDifferentiation();

                graphControlTop.Profile = OriginalSmooth;
                graphControlBottom.Profile = BottomProfileSmooth;
                SearchBandEdge();

			}
			catch {
				MessageBox.Show("ファイルを開けません");
				return;
			}
		}


        private Profile readFile(string fileName)
        {

            var reader = new StreamReader(fileName, Encoding.GetEncoding("Shift_JIS"));
            var contents = new List<string>();
            string str;
            while ((str = reader.ReadLine()) != null)
                if(str!="")
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
            Profile profile  = new Profile();
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
                    BottomProfile=OriginalSmooth.Differential(1);
            else
                for (int i = 0; i < Original.Pt.Count; i++)
                    BottomProfile.Pt.Add(Original.Pt[i]);

            //微分スムージング
            if (numericUpDownDifferentiationRunningAverage.ReadOnly == false)
            {//移動平均
                n = (int)numericUpDownDifferentiationRunningAverage.Value;
                for (int i = 0; i < BottomProfile.Pt.Count- n + 1; i++)
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
                    for (int i = 0; i < BottomProfile.Pt.Count;i++ )
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

                FittingPeak.FitMultiPeaksThread(pt.ToArray(), true, 0, ref pf);

                var p = new Profile();
                for (double x = pf[0].X - range * 0.9; x < pf[0].X + range * 0.9; x += range / 200)
                    p.Pt.Add(new PointD(x, -pf[0].GetValue(x, false) - pf[0].B1 - pf[0].B2 * (x - pf[0].X)));
                p.Color = Color.Red;

                graphControlBottom.ReplaceProfile(1, p);
                graphControlBottom.ReplaceProfile(2, null);
                graphControlBottom.ReplaceProfile(3, null);

                textBoxAkahama2006Nu.Text = pf[0].X.ToString();
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

                FittingPeak.FitMultiPeaksThread(pt.ToArray(), true, 0, ref pf);

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

            startHk = FitRange*0.01; endHk =FitRange*20; stepHk = (endHk - startHk) /20;
            startEdge = edge - FitRange; endEdge = edge + FitRange; stepEdge = (endEdge - startEdge) /20;

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
                                    temp = pt1[i].Y - I * 2/Hk/Math.PI* (1 / (1 +4*   (edge - pt1[i].X)/Hk * (edge - pt1[i].X)/Hk ));
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
                    p.Pt.Add(new PointD(x, bestI*2/bestHk/Math.PI * (1 / (1 + 4* (bestEdge - x)/bestHk * (bestEdge - x)/bestHk))));

            if (radioButtonDiamondRaman.Checked)
            {
                p.Color = Color.Red;
                graphControlBottom.ReplaceProfile(1, p);

                graphControlBottom.ReplaceProfile(2, null);
                graphControlBottom.ReplaceProfile(3, null);

                textBoxAkahama2006Nu.Text = bestEdge.ToString();
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



        private void textBoxNu_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                //Akahama2004
                var nu = Convert.ToDouble(textBoxAkahama2006Nu.Text);
                textBoxAkahama2004P.Text = (Convert.ToDouble(textBoxAkahama2004A.Text) + Convert.ToDouble(textBoxAkahama2004B.Text) * nu + Convert.ToDouble(textBoxAkahama2004C.Text) * 0.0001 * nu * nu).ToString();
                //Akahama2006
                var nuovernu0 = (Convert.ToDouble(textBoxAkahama2006Nu.Text) - Convert.ToDouble(textBoxAkahama2006Nu0.Text)) / Convert.ToDouble(textBoxAkahama2006Nu0.Text);
                textBoxAkahama2006P.Text = (Convert.ToDouble(textBoxAkahama2006K0.Text) * nuovernu0 * (1 + 0.5 * (Convert.ToDouble(textBoxAkahama2006K0Prime.Text) - 1) * nuovernu0)).ToString();

              

                


            }
            catch
            {
                MessageBox.Show("適切な数値を入れてください");
            }
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
			if(numericUpDownOriginalRunningAverage.ReadOnly==true)
			{
				numericUpDownOriginalRunningAverage.ReadOnly=false;
				numericUpDownOriginalGaussian.ReadOnly=true;
			}
			CalcSmoothingAndDifferentiation();
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = true;
            graphControlTop.Profile = OriginalSmooth;
            graphControlBottom.Profile = BottomProfileSmooth;
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = false;	
		}

		private void numericUpDownOriginalGaussian_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(numericUpDownOriginalGaussian.ReadOnly==true)
			{
				numericUpDownOriginalRunningAverage.ReadOnly=true;
				numericUpDownOriginalGaussian.ReadOnly=false;
			}
			CalcSmoothingAndDifferentiation();
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = true;
            graphControlTop.Profile = OriginalSmooth;
            graphControlBottom.Profile = BottomProfileSmooth;
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal =false;

		}

		private void numericUpDownDifferentiationRunningAverage_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(numericUpDownDifferentiationRunningAverage.ReadOnly==true)
			{
				numericUpDownDifferentiationRunningAverage.ReadOnly=false;
				numericUpDownDifferentiationGaussian.ReadOnly=true;
			}
			CalcSmoothingAndDifferentiation();
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = true;
            graphControlTop.Profile = OriginalSmooth;
            graphControlBottom.Profile = BottomProfileSmooth;
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = false;
		}

		private void numericUpDownwnDifferentiationGaussian_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if(numericUpDownDifferentiationGaussian.ReadOnly==true)
			{
				numericUpDownDifferentiationRunningAverage.ReadOnly=true;
				numericUpDownDifferentiationGaussian.ReadOnly=false;
			}
			CalcSmoothingAndDifferentiation();
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = true;
            graphControlTop.Profile = OriginalSmooth;
            graphControlBottom.Profile = BottomProfileSmooth;
            graphControlBottom.FixRangeHorizontal = graphControlTop.FixRangeHorizontal = false;
        }
        #endregion

        #region クリップボード監視関連
        private void  WatchFile() 
		{  //クリップボード監視
			while(true) {
				if(File.Exists(FileName)){
					time= File.GetLastWriteTime(FileName);
					if(time>oldTime && time!=oldTime){
						Thread.Sleep(200);
						FileRead();
						oldTime=time;
						Thread.Sleep(2000);
					}
					Thread.Sleep(100) ;
				}
			}
		}

		private void menuItemWatchFile_Click(object sender, System.EventArgs e) {
			if(menuItemWatchFile.Checked){
				menuItemWatchFile.Checked=false;
				thread.Abort();			// スレッドの中止
			}
			else {
				menuItemWatchFile.Checked=true;
				thread = new Thread(new ThreadStart(WatchFile));
				thread.Start() ;        // スレッドの起動
			}
		}

		private void FormDiamondRaman_Closed(object sender, System.EventArgs e) {
			thread.Abort();			// スレッドの中止
        }
        #endregion

        private void radioButtonDiamondRaman_CheckedChanged(object sender, EventArgs e)
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

        /// <summary>
        /// Ruby 現在のR1をR1_0にコピー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRubyRefR1Set_Click(object sender, EventArgs e)
        {
            numericBoxRubyRefR1.Value = numericBoxRubyR1.Value;
        }

        private void numericBoxRubyRefT_ValueChanged(object sender, EventArgs e)
        {
            calcRaganParameter();
        }
        private void numericBoxRubyRefR1_ValueChanged(object sender, EventArgs e)
        {
            calcRaganParameter();
        }
        /// <summary>
        /// Referenceのデータから、Raganのパラメータを計算する
        /// </summary>
        private void calcRaganParameter() 
        {
            var r = numericBoxRubyRefR1.Value;
            var t = numericBoxRubyRefT.Value;
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
            if(checkBoxRubyR1_0CalculatedFromRagan.Checked)
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
            var t = numericBoxRubyT.Value;
            var prm = numericBoxRubyRagan.Value;
            numericBoxRubyR1_0.Enabled = true;

            numericBoxRubyR1_0.Value = 1E7 /(prm + 4.49E-2 * t - 4.81E-4 * t * t + 3.71E-7 * t * t * t);
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
        private void numericBoxRubyR1_ValueChanged(object sender, EventArgs e)
        {
            calcRubyPressure();

        }


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

   

 

        private void menuItemExport_Click(object sender, EventArgs e)
        {

            if (graphControlTop.Profile != null && graphControlTop.Profile.Pt != null && graphControlTop.Profile.Pt.Count != 0)
            {
                PointD[] pt = new PointD[0];
                pt = graphControlTop.Profile.Pt.ToArray();
            //クリップボードにcsvデータを保存
            
                
                var dlg = new SaveFileDialog();
                dlg.Filter = "*.csv|*.csv";
                if(dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    using(StreamWriter sw = new StreamWriter(dlg.FileName))
                    {
                        for (int i = 0; i < pt.Length; i++)
                            sw.WriteLine(pt[i].X.ToString() + "," + pt[i].Y.ToString());
                        sw.Close();
                    }
            }


        }








    }
	
    

}
