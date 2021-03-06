﻿namespace RandomForestExplorer
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
            this.trainingGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.attributesCountLbl = new System.Windows.Forms.Label();
            this.instancesCountLbl = new System.Windows.Forms.Label();
            this.relationNameLbl = new System.Windows.Forms.Label();
            this.attributeLbl = new System.Windows.Forms.Label();
            this.instanceLbl = new System.Windows.Forms.Label();
            this.relationLbl = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numOfFeatures = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.treeDepth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numOfTrees = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.splitFileRb = new System.Windows.Forms.RadioButton();
            this.percentSplit = new System.Windows.Forms.NumericUpDown();
            this.loadFileRb = new System.Windows.Forms.RadioButton();
            this.percentSplitLbl = new System.Windows.Forms.Label();
            this.trainingFilePathLbl = new System.Windows.Forms.Label();
            this.filePathLbl = new System.Windows.Forms.Label();
            this.openTrainingFileBtn = new System.Windows.Forms.Button();
            this.openFileBtn = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.visualizationContainer = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.clearBtn = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.openTrainingFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.textArea = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label4 = new System.Windows.Forms.Label();
            this.numOfTreesToTest = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            this.trainingGroupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOfFeatures)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOfTrees)).BeginInit();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.percentSplit)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOfTreesToTest)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.trainingGroupBox);
            this.groupBox1.Controls.Add(this.trainingFilePathLbl);
            this.groupBox1.Controls.Add(this.filePathLbl);
            this.groupBox1.Controls.Add(this.openTrainingFileBtn);
            this.groupBox1.Controls.Add(this.openFileBtn);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(929, 230);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // trainingGroupBox
            // 
            this.trainingGroupBox.Controls.Add(this.groupBox3);
            this.trainingGroupBox.Controls.Add(this.groupBox4);
            this.trainingGroupBox.Controls.Add(this.groupBox7);
            this.trainingGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trainingGroupBox.Location = new System.Drawing.Point(3, 80);
            this.trainingGroupBox.Name = "trainingGroupBox";
            this.trainingGroupBox.Size = new System.Drawing.Size(923, 147);
            this.trainingGroupBox.TabIndex = 8;
            this.trainingGroupBox.TabStop = false;
            this.trainingGroupBox.Text = "Training";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.attributesCountLbl);
            this.groupBox3.Controls.Add(this.instancesCountLbl);
            this.groupBox3.Controls.Add(this.relationNameLbl);
            this.groupBox3.Controls.Add(this.attributeLbl);
            this.groupBox3.Controls.Add(this.instanceLbl);
            this.groupBox3.Controls.Add(this.relationLbl);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(409, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(511, 128);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Current Relation";
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
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.numOfTreesToTest);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.numOfFeatures);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.treeDepth);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.numOfTrees);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox4.Location = new System.Drawing.Point(200, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(209, 128);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Trees Configuration";
            // 
            // numOfFeatures
            // 
            this.numOfFeatures.Location = new System.Drawing.Point(130, 62);
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
            this.treeDepth.Location = new System.Drawing.Point(130, 37);
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
            this.numOfTrees.Location = new System.Drawing.Point(130, 13);
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
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.splitFileRb);
            this.groupBox7.Controls.Add(this.percentSplit);
            this.groupBox7.Controls.Add(this.loadFileRb);
            this.groupBox7.Controls.Add(this.percentSplitLbl);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox7.Location = new System.Drawing.Point(3, 16);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(197, 128);
            this.groupBox7.TabIndex = 9;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Data Source Configuration";
            // 
            // splitFileRb
            // 
            this.splitFileRb.AutoSize = true;
            this.splitFileRb.Location = new System.Drawing.Point(6, 40);
            this.splitFileRb.Name = "splitFileRb";
            this.splitFileRb.Size = new System.Drawing.Size(64, 17);
            this.splitFileRb.TabIndex = 8;
            this.splitFileRb.TabStop = true;
            this.splitFileRb.Text = "Split File";
            this.splitFileRb.UseVisualStyleBackColor = true;
            // 
            // percentSplit
            // 
            this.percentSplit.Location = new System.Drawing.Point(112, 60);
            this.percentSplit.Name = "percentSplit";
            this.percentSplit.Size = new System.Drawing.Size(53, 20);
            this.percentSplit.TabIndex = 6;
            this.percentSplit.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // loadFileRb
            // 
            this.loadFileRb.AutoSize = true;
            this.loadFileRb.Location = new System.Drawing.Point(6, 17);
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
            this.percentSplitLbl.Location = new System.Drawing.Point(3, 64);
            this.percentSplitLbl.Name = "percentSplitLbl";
            this.percentSplitLbl.Size = new System.Drawing.Size(103, 13);
            this.percentSplitLbl.TabIndex = 5;
            this.percentSplitLbl.Text = "Percentage split (%):";
            // 
            // trainingFilePathLbl
            // 
            this.trainingFilePathLbl.AutoSize = true;
            this.trainingFilePathLbl.Location = new System.Drawing.Point(139, 56);
            this.trainingFilePathLbl.Name = "trainingFilePathLbl";
            this.trainingFilePathLbl.Size = new System.Drawing.Size(148, 13);
            this.trainingFilePathLbl.TabIndex = 10;
            this.trainingFilePathLbl.Text = "Please choose a training file...";
            // 
            // filePathLbl
            // 
            this.filePathLbl.AutoSize = true;
            this.filePathLbl.Location = new System.Drawing.Point(139, 17);
            this.filePathLbl.Name = "filePathLbl";
            this.filePathLbl.Size = new System.Drawing.Size(131, 13);
            this.filePathLbl.TabIndex = 1;
            this.filePathLbl.Text = "Please choose a test file...";
            // 
            // openTrainingFileBtn
            // 
            this.openTrainingFileBtn.Location = new System.Drawing.Point(12, 51);
            this.openTrainingFileBtn.Name = "openTrainingFileBtn";
            this.openTrainingFileBtn.Size = new System.Drawing.Size(121, 23);
            this.openTrainingFileBtn.TabIndex = 9;
            this.openTrainingFileBtn.Text = "Open Training File ...";
            this.openTrainingFileBtn.UseVisualStyleBackColor = true;
            this.openTrainingFileBtn.Click += new System.EventHandler(this.OpenTrainingFileBtnClick);
            // 
            // openFileBtn
            // 
            this.openFileBtn.Location = new System.Drawing.Point(12, 12);
            this.openFileBtn.Name = "openFileBtn";
            this.openFileBtn.Size = new System.Drawing.Size(121, 23);
            this.openFileBtn.TabIndex = 0;
            this.openFileBtn.Text = "Open Test File ...";
            this.openFileBtn.UseVisualStyleBackColor = true;
            this.openFileBtn.Click += new System.EventHandler(this.OpenFileBtnClick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Weka files|*.arff";
            // 
            // visualizationContainer
            // 
            this.visualizationContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visualizationContainer.Location = new System.Drawing.Point(0, 0);
            this.visualizationContainer.Name = "visualizationContainer";
            this.visualizationContainer.Size = new System.Drawing.Size(514, 345);
            this.visualizationContainer.TabIndex = 1;
            this.visualizationContainer.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.progressBar);
            this.groupBox5.Controls.Add(this.clearBtn);
            this.groupBox5.Controls.Add(this.stopBtn);
            this.groupBox5.Controls.Add(this.startBtn);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(0, 230);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(929, 46);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(228, 16);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(698, 27);
            this.progressBar.Step = 5;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 3;
            // 
            // clearBtn
            // 
            this.clearBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.clearBtn.Location = new System.Drawing.Point(153, 16);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(75, 27);
            this.clearBtn.TabIndex = 4;
            this.clearBtn.Text = "Clear";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.OnClearBtnClick);
            // 
            // stopBtn
            // 
            this.stopBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.stopBtn.Location = new System.Drawing.Point(78, 16);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(75, 27);
            this.stopBtn.TabIndex = 2;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.StopBtnClick);
            // 
            // startBtn
            // 
            this.startBtn.Dock = System.Windows.Forms.DockStyle.Left;
            this.startBtn.Location = new System.Drawing.Point(3, 16);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 27);
            this.startBtn.TabIndex = 1;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.StartBtnClick);
            // 
            // openTrainingFileDialog
            // 
            this.openTrainingFileDialog.Filter = "Weka training files|*.txt";
            // 
            // textArea
            // 
            this.textArea.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.textArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textArea.Location = new System.Drawing.Point(0, 0);
            this.textArea.Name = "textArea";
            this.textArea.ReadOnly = true;
            this.textArea.Size = new System.Drawing.Size(411, 345);
            this.textArea.TabIndex = 0;
            this.textArea.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 276);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textArea);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.visualizationContainer);
            this.splitContainer1.Size = new System.Drawing.Size(929, 345);
            this.splitContainer1.SplitterDistance = 411;
            this.splitContainer1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Number of trees to test:";
            // 
            // numOfTreesToTest
            // 
            this.numOfTreesToTest.Location = new System.Drawing.Point(130, 88);
            this.numOfTreesToTest.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numOfTreesToTest.Name = "numOfTreesToTest";
            this.numOfTreesToTest.Size = new System.Drawing.Size(53, 20);
            this.numOfTreesToTest.TabIndex = 9;
            this.numOfTreesToTest.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // Explorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 621);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Name = "Explorer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Random Forest Explorer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.trainingGroupBox.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numOfFeatures)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOfTrees)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.percentSplit)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numOfTreesToTest)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button openFileBtn;
        private System.Windows.Forms.Label filePathLbl;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox visualizationContainer;
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
        private System.Windows.Forms.RichTextBox textArea;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.NumericUpDown numOfTreesToTest;
        private System.Windows.Forms.Label label4;
    }
}

