namespace RandomForestExplorer
{
    partial class Explorer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;



        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.filePathLbl = new System.Windows.Forms.Label();
            this.openFileBtn = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.trainingGroupBox = new System.Windows.Forms.GroupBox();
            this.trainingFilePathLbl = new System.Windows.Forms.Label();
            this.openTrainingFileBtn = new System.Windows.Forms.Button();
            this.splitFileRb = new System.Windows.Forms.RadioButton();
            this.loadFileRb = new System.Windows.Forms.RadioButton();
            this.percentSplitLbl = new System.Windows.Forms.Label();
            this.percentSplit = new System.Windows.Forms.NumericUpDown();
            this.numOfFeatures = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.treeDepth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numOfTrees = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.attributesCountLbl = new System.Windows.Forms.Label();
            this.instancesCountLbl = new System.Windows.Forms.Label();
            this.relationNameLbl = new System.Windows.Forms.Label();
            this.attributeLbl = new System.Windows.Forms.Label();
            this.instanceLbl = new System.Windows.Forms.Label();
            this.relationLbl = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.stopBtn = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.openTrainingFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.trainingGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.percentSplit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOfFeatures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOfTrees)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.filePathLbl);
            this.groupBox1.Controls.Add(this.openFileBtn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(612, 45);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // filePathLbl
            // 
            this.filePathLbl.AutoSize = true;
            this.filePathLbl.Location = new System.Drawing.Point(93, 17);
            this.filePathLbl.Name = "filePathLbl";
            this.filePathLbl.Size = new System.Drawing.Size(0, 13);
            this.filePathLbl.TabIndex = 1;
            // 
            // openFileBtn
            // 
            this.openFileBtn.Location = new System.Drawing.Point(12, 12);
            this.openFileBtn.Name = "openFileBtn";
            this.openFileBtn.Size = new System.Drawing.Size(75, 23);
            this.openFileBtn.TabIndex = 0;
            this.openFileBtn.Text = "Open File ...";
            this.openFileBtn.UseVisualStyleBackColor = true;
            this.openFileBtn.Click += new System.EventHandler(this.OpenFileBtnClick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Weka files|*.arff";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(612, 259);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.trainingGroupBox);
            this.groupBox4.Controls.Add(this.numOfFeatures);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.treeDepth);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.numOfTrees);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(203, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(406, 240);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Configuration";
            // 
            // trainingGroupBox
            // 
            this.trainingGroupBox.Controls.Add(this.trainingFilePathLbl);
            this.trainingGroupBox.Controls.Add(this.openTrainingFileBtn);
            this.trainingGroupBox.Controls.Add(this.splitFileRb);
            this.trainingGroupBox.Controls.Add(this.loadFileRb);
            this.trainingGroupBox.Controls.Add(this.percentSplitLbl);
            this.trainingGroupBox.Controls.Add(this.percentSplit);
            this.trainingGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trainingGroupBox.Location = new System.Drawing.Point(3, 90);
            this.trainingGroupBox.Name = "trainingGroupBox";
            this.trainingGroupBox.Size = new System.Drawing.Size(400, 147);
            this.trainingGroupBox.TabIndex = 8;
            this.trainingGroupBox.TabStop = false;
            this.trainingGroupBox.Text = "Training";
            // 
            // trainingFilePathLbl
            // 
            this.trainingFilePathLbl.AutoSize = true;
            this.trainingFilePathLbl.Location = new System.Drawing.Point(8, 121);
            this.trainingFilePathLbl.Name = "trainingFilePathLbl";
            this.trainingFilePathLbl.Size = new System.Drawing.Size(0, 13);
            this.trainingFilePathLbl.TabIndex = 10;
            // 
            // openTrainingFileBtn
            // 
            this.openTrainingFileBtn.Location = new System.Drawing.Point(7, 90);
            this.openTrainingFileBtn.Name = "openTrainingFileBtn";
            this.openTrainingFileBtn.Size = new System.Drawing.Size(121, 23);
            this.openTrainingFileBtn.TabIndex = 9;
            this.openTrainingFileBtn.Text = "Open Training File ...";
            this.openTrainingFileBtn.UseVisualStyleBackColor = true;
            this.openTrainingFileBtn.Click += new System.EventHandler(this.OpenTrainingFileBtnClick);
            // 
            // splitFileRb
            // 
            this.splitFileRb.AutoSize = true;
            this.splitFileRb.Location = new System.Drawing.Point(7, 19);
            this.splitFileRb.Name = "splitFileRb";
            this.splitFileRb.Size = new System.Drawing.Size(64, 17);
            this.splitFileRb.TabIndex = 8;
            this.splitFileRb.TabStop = true;
            this.splitFileRb.Text = "Split File";
            this.splitFileRb.UseVisualStyleBackColor = true;
            // 
            // loadFileRb
            // 
            this.loadFileRb.AutoSize = true;
            this.loadFileRb.Location = new System.Drawing.Point(7, 67);
            this.loadFileRb.Name = "loadFileRb";
            this.loadFileRb.Size = new System.Drawing.Size(68, 17);
            this.loadFileRb.TabIndex = 7;
            this.loadFileRb.TabStop = true;
            this.loadFileRb.Text = "Load File";
            this.loadFileRb.UseVisualStyleBackColor = true;
            // 
            // percentSplitLbl
            // 
            this.percentSplitLbl.AutoSize = true;
            this.percentSplitLbl.Location = new System.Drawing.Point(6, 42);
            this.percentSplitLbl.Name = "percentSplitLbl";
            this.percentSplitLbl.Size = new System.Drawing.Size(103, 13);
            this.percentSplitLbl.TabIndex = 5;
            this.percentSplitLbl.Text = "Percentage split (%):";
            // 
            // percentSplit
            // 
            this.percentSplit.Location = new System.Drawing.Point(115, 40);
            this.percentSplit.Name = "percentSplit";
            this.percentSplit.Size = new System.Drawing.Size(53, 20);
            this.percentSplit.TabIndex = 6;
            this.percentSplit.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // numOfFeatures
            // 
            this.numOfFeatures.Location = new System.Drawing.Point(110, 64);
            this.numOfFeatures.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numOfFeatures.Name = "numOfFeatures";
            this.numOfFeatures.Size = new System.Drawing.Size(53, 20);
            this.numOfFeatures.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Number of features:";
            // 
            // treeDepth
            // 
            this.treeDepth.Location = new System.Drawing.Point(110, 40);
            this.treeDepth.Name = "treeDepth";
            this.treeDepth.Size = new System.Drawing.Size(53, 20);
            this.treeDepth.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tree max depth:";
            // 
            // numOfTrees
            // 
            this.numOfTrees.Location = new System.Drawing.Point(110, 14);
            this.numOfTrees.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numOfTrees.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numOfTrees.Name = "numOfTrees";
            this.numOfTrees.Size = new System.Drawing.Size(53, 20);
            this.numOfTrees.TabIndex = 1;
            this.numOfTrees.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number of trees:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.attributesCountLbl);
            this.groupBox3.Controls.Add(this.instancesCountLbl);
            this.groupBox3.Controls.Add(this.relationNameLbl);
            this.groupBox3.Controls.Add(this.attributeLbl);
            this.groupBox3.Controls.Add(this.instanceLbl);
            this.groupBox3.Controls.Add(this.relationLbl);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(3, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 240);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Current relation";
            // 
            // attributesCountLbl
            // 
            this.attributesCountLbl.AutoSize = true;
            this.attributesCountLbl.Location = new System.Drawing.Point(65, 67);
            this.attributesCountLbl.Name = "attributesCountLbl";
            this.attributesCountLbl.Size = new System.Drawing.Size(0, 13);
            this.attributesCountLbl.TabIndex = 5;
            this.attributesCountLbl.Tag = "";
            // 
            // instancesCountLbl
            // 
            this.instancesCountLbl.AutoSize = true;
            this.instancesCountLbl.Location = new System.Drawing.Point(65, 42);
            this.instancesCountLbl.Name = "instancesCountLbl";
            this.instancesCountLbl.Size = new System.Drawing.Size(0, 13);
            this.instancesCountLbl.TabIndex = 4;
            this.instancesCountLbl.Tag = "";
            // 
            // relationNameLbl
            // 
            this.relationNameLbl.AutoSize = true;
            this.relationNameLbl.Location = new System.Drawing.Point(65, 20);
            this.relationNameLbl.Name = "relationNameLbl";
            this.relationNameLbl.Size = new System.Drawing.Size(0, 13);
            this.relationNameLbl.TabIndex = 3;
            this.relationNameLbl.Tag = "";
            // 
            // attributeLbl
            // 
            this.attributeLbl.AutoSize = true;
            this.attributeLbl.Location = new System.Drawing.Point(7, 67);
            this.attributeLbl.Name = "attributeLbl";
            this.attributeLbl.Size = new System.Drawing.Size(54, 13);
            this.attributeLbl.TabIndex = 2;
            this.attributeLbl.Tag = "Features: ";
            this.attributeLbl.Text = "Features: ";
            // 
            // instanceLbl
            // 
            this.instanceLbl.AutoSize = true;
            this.instanceLbl.Location = new System.Drawing.Point(7, 42);
            this.instanceLbl.Name = "instanceLbl";
            this.instanceLbl.Size = new System.Drawing.Size(59, 13);
            this.instanceLbl.TabIndex = 1;
            this.instanceLbl.Tag = "Instances: ";
            this.instanceLbl.Text = "Instances: ";
            // 
            // relationLbl
            // 
            this.relationLbl.AutoSize = true;
            this.relationLbl.Location = new System.Drawing.Point(7, 20);
            this.relationLbl.Name = "relationLbl";
            this.relationLbl.Size = new System.Drawing.Size(52, 13);
            this.relationLbl.TabIndex = 0;
            this.relationLbl.Tag = "Relation: ";
            this.relationLbl.Text = "Relation: ";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.stopBtn);
            this.groupBox5.Controls.Add(this.startBtn);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 304);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(612, 46);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(96, 14);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(75, 23);
            this.stopBtn.TabIndex = 2;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.StopBtnClick);
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(12, 14);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 1;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.StartBtnClick);
            // 
            // openTrainingFileDialog
            // 
            this.openTrainingFileDialog.Filter = "Weka training files|*.txt";
            // 
            // Explorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(612, 521);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Explorer";
            this.Text = "Random Forest Explorer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.trainingGroupBox.ResumeLayout(false);
            this.trainingGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.percentSplit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOfFeatures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOfTrees)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button openFileBtn;
        private System.Windows.Forms.Label filePathLbl;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label attributeLbl;
        private System.Windows.Forms.Label instanceLbl;
        private System.Windows.Forms.Label relationLbl;
        private System.Windows.Forms.Label attributesCountLbl;
        private System.Windows.Forms.Label instancesCountLbl;
        private System.Windows.Forms.Label relationNameLbl;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown numOfTrees;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown treeDepth;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numOfFeatures;
        private System.Windows.Forms.NumericUpDown percentSplit;
        private System.Windows.Forms.Label percentSplitLbl;
        private System.Windows.Forms.GroupBox trainingGroupBox;
        private System.Windows.Forms.RadioButton splitFileRb;
        private System.Windows.Forms.RadioButton loadFileRb;
        private System.Windows.Forms.OpenFileDialog openTrainingFileDialog;
        private System.Windows.Forms.Button openTrainingFileBtn;
        private System.Windows.Forms.Label trainingFilePathLbl;
    }
}

