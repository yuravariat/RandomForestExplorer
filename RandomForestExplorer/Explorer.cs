using System;
using System.Windows.Forms;
using RandomForestExplorer.Data;

namespace RandomForestExplorer
{
    public partial class Explorer : Form
    {
        private DataModel _model;
        public Action<string> OnFileLoad;
        public Action<string> OnTrainingFileLoad;
        public Action OnStart;
        public Action OnStop;

        public int NumberOfTrees
        {
            get { return (int) numOfTrees.Value; }
        }

        public int TreeDepth
        {
            get { return (int)treeDepth.Value; }
        }

        public int NumberOfFeatures
        {
            get { return (int) numOfFeatures.Value; }
        }

        public float PercentSplit
        {
            get { return (float)percentSplit.Value; }
        }

        public ProgressBar ProgressBar
        {
            get { return progressBar; }
        }

        public Explorer(DataModel p_model)
        {
            InitializeComponent();
            _model = p_model;
        }

        public void Write(string p_text)
        {
            this.UIThread(() =>
            {
                textArea.Text = string.Format("{0}\n[{1}] {2}",
                textArea.Text,
                DateTime.Now.ToString("HH:mm:ss:fff"),
                p_text);
            });
        }

        public void AddVisualization(Visualization visual)
        {
            this.visualizationContainer.Controls.Add(visual);
        }

        public void RemoveVisualization()
        {
            this.visualizationContainer.Controls.Clear();
        }

        #region Bindings
        private void Bind()
        {
            filePathLbl.DataBindings.Add("Text", _model, "FileName", false, DataSourceUpdateMode.OnPropertyChanged);
            trainingFilePathLbl.DataBindings.Add("Text", _model, "TrainingFileName", false, DataSourceUpdateMode.OnPropertyChanged);

            relationNameLbl.DataBindings.Add("Text", _model, "Relation", false, DataSourceUpdateMode.OnPropertyChanged);
            instancesCountLbl.DataBindings.Add("Text", _model, "TotalInstances", false, DataSourceUpdateMode.OnPropertyChanged);
            attributesCountLbl.DataBindings.Add("Text", _model, "TotalFeatures", false, DataSourceUpdateMode.OnPropertyChanged);

            trainingGroupBox.DataBindings.Add("Enabled", _model, "TrainingEnabled", false, DataSourceUpdateMode.OnPropertyChanged);

            splitFileRb.DataBindings.Add("Checked", _model, "TrainingFromData", false, DataSourceUpdateMode.OnPropertyChanged);
            percentSplit.DataBindings.Add("Enabled", splitFileRb, "Checked", false, DataSourceUpdateMode.OnPropertyChanged);
            percentSplitLbl.DataBindings.Add("Enabled", splitFileRb, "Checked", false, DataSourceUpdateMode.OnPropertyChanged);

            loadFileRb.DataBindings.Add("Checked", _model, "TrainingFromFile", false, DataSourceUpdateMode.OnPropertyChanged);
            openTrainingFileBtn.DataBindings.Add("Enabled", loadFileRb, "Checked", false, DataSourceUpdateMode.OnPropertyChanged);
            trainingFilePathLbl.DataBindings.Add("Enabled", loadFileRb, "Checked", false, DataSourceUpdateMode.OnPropertyChanged);

            startBtn.DataBindings.Add("Enabled", _model, "IsReady", false, DataSourceUpdateMode.OnPropertyChanged);
            stopBtn.DataBindings.Add("Enabled", _model, "InProcess", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void UnBind()
        {
            while (filePathLbl.DataBindings.Count > 0)
            {
                filePathLbl.DataBindings.RemoveAt(0);
            }

            while (relationNameLbl.DataBindings.Count > 0)
            {
                relationNameLbl.DataBindings.RemoveAt(0);
            }

            while (instancesCountLbl.DataBindings.Count > 0)
            {
                instancesCountLbl.DataBindings.RemoveAt(0);
            }

            while (attributesCountLbl.DataBindings.Count > 0)
            {
                attributesCountLbl.DataBindings.RemoveAt(0);
            }
        }
        #endregion

        private void StartBtnClick(object p_sender, EventArgs p_eventArgs)
        {
            if (OnStart != null)
                OnStart();
        }

        private void StopBtnClick(object p_sender, EventArgs p_eventArgs)
        {
            if (OnStop != null)
                OnStop();
        }

        private void OnClearBtnClick(object sender, EventArgs e)
        {
            textArea.Clear();
        }

        private void OpenFileBtnClick(object p_sender, EventArgs p_eventArgs)
        {
            if (openFileDialog.ShowDialog()==DialogResult.OK)
            {
                if (OnFileLoad != null)
                    OnFileLoad(openFileDialog.FileName);
            }
        }

        private void OpenTrainingFileBtnClick(object sender, EventArgs e)
        {
            if (openTrainingFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (OnTrainingFileLoad != null)
                    OnTrainingFileLoad(openTrainingFileDialog.FileName);
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="p_disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool p_disposing)
        {
            if (p_disposing && (components != null))
            {
                UnBind();
                components.Dispose();
                _model = null;
            }
            base.Dispose(p_disposing);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Bind();
        }
    }
}
