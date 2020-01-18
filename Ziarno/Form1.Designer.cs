namespace Ziarno
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbSeed = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gbProps = new System.Windows.Forms.GroupBox();
            this.btnBoundaries = new System.Windows.Forms.Button();
            this.btnSaveCsv = new System.Windows.Forms.Button();
            this.tbRadiusMax = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbRadiusMin = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.tbProgressionCount = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbProgression = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.lbRand = new System.Windows.Forms.Label();
            this.cbRandom = new System.Windows.Forms.ComboBox();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtRadius = new System.Windows.Forms.TextBox();
            this.lbRadius = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.lbCount = new System.Windows.Forms.Label();
            this.cbPbc = new System.Windows.Forms.CheckBox();
            this.lbNeigh = new System.Windows.Forms.Label();
            this.cbNeighbourType = new System.Windows.Forms.ComboBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.lbY = new System.Windows.Forms.Label();
            this.lbX = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.lbGBCThreshold = new System.Windows.Forms.Label();
            this.txtGBCTreshold = new System.Windows.Forms.TextBox();
            this.cbGBC = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.cbDualPhase = new System.Windows.Forms.CheckBox();
            this.btnDualPhase = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.txtMonteSeeds = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSeed)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gbProps.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pbSeed);
            this.panel1.Location = new System.Drawing.Point(218, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(620, 500);
            this.panel1.TabIndex = 2;
            // 
            // pbSeed
            // 
            this.pbSeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSeed.Location = new System.Drawing.Point(0, 0);
            this.pbSeed.Name = "pbSeed";
            this.pbSeed.Size = new System.Drawing.Size(500, 500);
            this.pbSeed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSeed.TabIndex = 0;
            this.pbSeed.TabStop = false;
            this.pbSeed.Click += new System.EventHandler(this.pbSeed_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(4, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(211, 506);
            this.tabControl1.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gbProps);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(203, 480);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ziarno";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gbProps
            // 
            this.gbProps.Controls.Add(this.btnBoundaries);
            this.gbProps.Controls.Add(this.btnSaveCsv);
            this.gbProps.Controls.Add(this.tbRadiusMax);
            this.gbProps.Controls.Add(this.label3);
            this.gbProps.Controls.Add(this.tbRadiusMin);
            this.gbProps.Controls.Add(this.button3);
            this.gbProps.Controls.Add(this.tbProgressionCount);
            this.gbProps.Controls.Add(this.label2);
            this.gbProps.Controls.Add(this.cbProgression);
            this.gbProps.Controls.Add(this.button2);
            this.gbProps.Controls.Add(this.lbRand);
            this.gbProps.Controls.Add(this.cbRandom);
            this.gbProps.Controls.Add(this.btnSaveImage);
            this.gbProps.Controls.Add(this.btnStart);
            this.gbProps.Controls.Add(this.txtRadius);
            this.gbProps.Controls.Add(this.lbRadius);
            this.gbProps.Controls.Add(this.txtCount);
            this.gbProps.Controls.Add(this.lbCount);
            this.gbProps.Controls.Add(this.cbPbc);
            this.gbProps.Controls.Add(this.lbNeigh);
            this.gbProps.Controls.Add(this.cbNeighbourType);
            this.gbProps.Controls.Add(this.txtY);
            this.gbProps.Controls.Add(this.txtX);
            this.gbProps.Controls.Add(this.lbY);
            this.gbProps.Controls.Add(this.lbX);
            this.gbProps.Location = new System.Drawing.Point(0, 0);
            this.gbProps.Name = "gbProps";
            this.gbProps.Size = new System.Drawing.Size(200, 484);
            this.gbProps.TabIndex = 1;
            this.gbProps.TabStop = false;
            this.gbProps.Text = "Properties";
            // 
            // btnBoundaries
            // 
            this.btnBoundaries.Location = new System.Drawing.Point(119, 446);
            this.btnBoundaries.Name = "btnBoundaries";
            this.btnBoundaries.Size = new System.Drawing.Size(75, 23);
            this.btnBoundaries.TabIndex = 25;
            this.btnBoundaries.Text = "Boundaries";
            this.btnBoundaries.UseVisualStyleBackColor = true;
            this.btnBoundaries.Click += new System.EventHandler(this.btnBoundaries_Click);
            // 
            // btnSaveCsv
            // 
            this.btnSaveCsv.Location = new System.Drawing.Point(119, 417);
            this.btnSaveCsv.Name = "btnSaveCsv";
            this.btnSaveCsv.Size = new System.Drawing.Size(75, 23);
            this.btnSaveCsv.TabIndex = 24;
            this.btnSaveCsv.Text = "Save CSV";
            this.btnSaveCsv.UseVisualStyleBackColor = true;
            this.btnSaveCsv.Click += new System.EventHandler(this.btnSaveCsv_Click);
            // 
            // tbRadiusMax
            // 
            this.tbRadiusMax.Location = new System.Drawing.Point(119, 324);
            this.tbRadiusMax.Name = "tbRadiusMax";
            this.tbRadiusMax.Size = new System.Drawing.Size(75, 20);
            this.tbRadiusMax.TabIndex = 23;
            this.tbRadiusMax.Text = "5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 327);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Radius";
            // 
            // tbRadiusMin
            // 
            this.tbRadiusMin.Location = new System.Drawing.Point(53, 324);
            this.tbRadiusMin.Name = "tbRadiusMin";
            this.tbRadiusMin.Size = new System.Drawing.Size(60, 20);
            this.tbRadiusMin.TabIndex = 21;
            this.tbRadiusMin.Text = "1";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(9, 446);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "Next";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tbProgressionCount
            // 
            this.tbProgressionCount.Location = new System.Drawing.Point(53, 298);
            this.tbProgressionCount.Name = "tbProgressionCount";
            this.tbProgressionCount.Size = new System.Drawing.Size(141, 20);
            this.tbProgressionCount.TabIndex = 19;
            this.tbProgressionCount.Text = "5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Count:";
            // 
            // cbProgression
            // 
            this.cbProgression.AutoSize = true;
            this.cbProgression.Location = new System.Drawing.Point(9, 277);
            this.cbProgression.Name = "cbProgression";
            this.cbProgression.Size = new System.Drawing.Size(73, 17);
            this.cbProgression.TabIndex = 17;
            this.cbProgression.Text = "Inclusions";
            this.cbProgression.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(9, 388);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Paint";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbRand
            // 
            this.lbRand.AutoSize = true;
            this.lbRand.Location = new System.Drawing.Point(6, 197);
            this.lbRand.Name = "lbRand";
            this.lbRand.Size = new System.Drawing.Size(77, 13);
            this.lbRand.TabIndex = 15;
            this.lbRand.Text = "Random Type:";
            // 
            // cbRandom
            // 
            this.cbRandom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRandom.FormattingEnabled = true;
            this.cbRandom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbRandom.Items.AddRange(new object[] {
            "Random",
            "Evenly",
            "Random with Radius",
            "By click"});
            this.cbRandom.Location = new System.Drawing.Point(9, 215);
            this.cbRandom.Name = "cbRandom";
            this.cbRandom.Size = new System.Drawing.Size(185, 21);
            this.cbRandom.TabIndex = 14;
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Location = new System.Drawing.Point(119, 388);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(75, 23);
            this.btnSaveImage.TabIndex = 13;
            this.btnSaveImage.Text = "Save BMP";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(9, 417);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 12;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtRadius
            // 
            this.txtRadius.Location = new System.Drawing.Point(75, 116);
            this.txtRadius.Name = "txtRadius";
            this.txtRadius.Size = new System.Drawing.Size(119, 20);
            this.txtRadius.TabIndex = 11;
            this.txtRadius.Text = "10";
            // 
            // lbRadius
            // 
            this.lbRadius.AutoSize = true;
            this.lbRadius.Location = new System.Drawing.Point(6, 119);
            this.lbRadius.Name = "lbRadius";
            this.lbRadius.Size = new System.Drawing.Size(43, 13);
            this.lbRadius.TabIndex = 10;
            this.lbRadius.Text = "Radius:";
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(75, 90);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(119, 20);
            this.txtCount.TabIndex = 8;
            this.txtCount.Text = "10";
            // 
            // lbCount
            // 
            this.lbCount.AutoSize = true;
            this.lbCount.Location = new System.Drawing.Point(6, 93);
            this.lbCount.Name = "lbCount";
            this.lbCount.Size = new System.Drawing.Size(65, 13);
            this.lbCount.TabIndex = 7;
            this.lbCount.Text = "Seed count:";
            // 
            // cbPbc
            // 
            this.cbPbc.AutoSize = true;
            this.cbPbc.Location = new System.Drawing.Point(9, 253);
            this.cbPbc.Name = "cbPbc";
            this.cbPbc.Size = new System.Drawing.Size(81, 17);
            this.cbPbc.TabIndex = 6;
            this.cbPbc.Text = "Periodic BC";
            this.cbPbc.UseVisualStyleBackColor = true;
            // 
            // lbNeigh
            // 
            this.lbNeigh.AutoSize = true;
            this.lbNeigh.Location = new System.Drawing.Point(6, 149);
            this.lbNeigh.Name = "lbNeigh";
            this.lbNeigh.Size = new System.Drawing.Size(86, 13);
            this.lbNeigh.TabIndex = 5;
            this.lbNeigh.Text = "Neighbour Type:";
            // 
            // cbNeighbourType
            // 
            this.cbNeighbourType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNeighbourType.FormattingEnabled = true;
            this.cbNeighbourType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbNeighbourType.Items.AddRange(new object[] {
            "Von Neumann",
            "Moore",
            "Hexagonal left",
            "Hexagonal right",
            "Hexagonal random",
            "Pentagonal random"});
            this.cbNeighbourType.Location = new System.Drawing.Point(9, 167);
            this.cbNeighbourType.Name = "cbNeighbourType";
            this.cbNeighbourType.Size = new System.Drawing.Size(185, 21);
            this.cbNeighbourType.TabIndex = 4;
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(29, 58);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(165, 20);
            this.txtY.TabIndex = 3;
            this.txtY.Text = "100";
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(29, 27);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(165, 20);
            this.txtX.TabIndex = 2;
            this.txtX.Text = "100";
            // 
            // lbY
            // 
            this.lbY.AutoSize = true;
            this.lbY.Location = new System.Drawing.Point(6, 61);
            this.lbY.Name = "lbY";
            this.lbY.Size = new System.Drawing.Size(17, 13);
            this.lbY.TabIndex = 1;
            this.lbY.Text = "Y:";
            // 
            // lbX
            // 
            this.lbX.AutoSize = true;
            this.lbX.Location = new System.Drawing.Point(6, 30);
            this.lbX.Name = "lbX";
            this.lbX.Size = new System.Drawing.Size(17, 13);
            this.lbX.TabIndex = 0;
            this.lbX.Text = "X:";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.lbGBCThreshold);
            this.tabPage3.Controls.Add(this.txtGBCTreshold);
            this.tabPage3.Controls.Add(this.cbGBC);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(203, 480);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "GBC";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // lbGBCThreshold
            // 
            this.lbGBCThreshold.AutoSize = true;
            this.lbGBCThreshold.Location = new System.Drawing.Point(3, 42);
            this.lbGBCThreshold.Name = "lbGBCThreshold";
            this.lbGBCThreshold.Size = new System.Drawing.Size(71, 13);
            this.lbGBCThreshold.TabIndex = 2;
            this.lbGBCThreshold.Text = "Threshold[%]:";
            // 
            // txtGBCTreshold
            // 
            this.txtGBCTreshold.Location = new System.Drawing.Point(80, 39);
            this.txtGBCTreshold.Name = "txtGBCTreshold";
            this.txtGBCTreshold.Size = new System.Drawing.Size(100, 20);
            this.txtGBCTreshold.TabIndex = 1;
            this.txtGBCTreshold.Text = "50";
            // 
            // cbGBC
            // 
            this.cbGBC.AutoSize = true;
            this.cbGBC.Checked = true;
            this.cbGBC.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGBC.Location = new System.Drawing.Point(6, 6);
            this.cbGBC.Name = "cbGBC";
            this.cbGBC.Size = new System.Drawing.Size(48, 17);
            this.cbGBC.TabIndex = 0;
            this.cbGBC.Text = "GBC";
            this.cbGBC.UseVisualStyleBackColor = true;
            this.cbGBC.CheckedChanged += new System.EventHandler(this.cbGBC_CheckedChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.cbDualPhase);
            this.tabPage4.Controls.Add(this.btnDualPhase);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(203, 480);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "DP";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // cbDualPhase
            // 
            this.cbDualPhase.AutoSize = true;
            this.cbDualPhase.Location = new System.Drawing.Point(6, 6);
            this.cbDualPhase.Name = "cbDualPhase";
            this.cbDualPhase.Size = new System.Drawing.Size(81, 17);
            this.cbDualPhase.TabIndex = 1;
            this.cbDualPhase.Text = "Dual Phase";
            this.cbDualPhase.UseVisualStyleBackColor = true;
            // 
            // btnDualPhase
            // 
            this.btnDualPhase.Location = new System.Drawing.Point(6, 60);
            this.btnDualPhase.Name = "btnDualPhase";
            this.btnDualPhase.Size = new System.Drawing.Size(75, 23);
            this.btnDualPhase.TabIndex = 0;
            this.btnDualPhase.Text = "Dual Phase";
            this.btnDualPhase.UseVisualStyleBackColor = true;
            this.btnDualPhase.Click += new System.EventHandler(this.btnDualPhase_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.txtMonteSeeds);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(203, 480);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tab2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 451);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtMonteSeeds
            // 
            this.txtMonteSeeds.Location = new System.Drawing.Point(81, 11);
            this.txtMonteSeeds.Name = "txtMonteSeeds";
            this.txtMonteSeeds.Size = new System.Drawing.Size(100, 20);
            this.txtMonteSeeds.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Liczba ziaren:";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(847, 524);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSeed)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.gbProps.ResumeLayout(false);
            this.gbProps.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbSeed;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtMonteSeeds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lbX;
        private System.Windows.Forms.Label lbY;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.ComboBox cbNeighbourType;
        private System.Windows.Forms.Label lbNeigh;
        private System.Windows.Forms.CheckBox cbPbc;
        private System.Windows.Forms.Label lbCount;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label lbRadius;
        private System.Windows.Forms.TextBox txtRadius;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.ComboBox cbRandom;
        private System.Windows.Forms.Label lbRand;
        private System.Windows.Forms.GroupBox gbProps;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox cbProgression;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbProgressionCount;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tbRadiusMax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbRadiusMin;
        private System.Windows.Forms.Button btnSaveCsv;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label lbGBCThreshold;
        private System.Windows.Forms.TextBox txtGBCTreshold;
        private System.Windows.Forms.CheckBox cbGBC;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox cbDualPhase;
        private System.Windows.Forms.Button btnDualPhase;
        private System.Windows.Forms.Button btnBoundaries;
    }
}

