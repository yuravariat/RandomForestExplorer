using System;
using System.Windows.Forms;
using RandomForestExplorer.Data;

namespace RandomForestExplorer
{
    public partial class Explorer : Form
    {
        private DataModel _model;
        public Action<string> OnFileLoad;
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

        public Explorer(DataModel p_model)
        {
            InitializeComponent();
            _model = p_model;
            Bind();
        }

        #region Bindings
        private void Bind()
        {
            filePathLbl.DataBindings.Add("Text", _model, "FileName", false, DataSourceUpdateMode.OnPropertyChanged);
            relationNameLbl.DataBindings.Add("Text", _model, "Relation", false, DataSourceUpdateMode.OnPropertyChanged);
            instancesCountLbl.DataBindings.Add("Text", _model, "TotalInstances", false, DataSourceUpdateMode.OnPropertyChanged);
            attributesCountLbl.DataBindings.Add("Text", _model, "TotalFeatures", false, DataSourceUpdateMode.OnPropertyChanged);
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

        private void OpenFileBtnClick(object p_sender, EventArgs p_eventArgs)
        {
            if (openFileDialog.ShowDialog()==DialogResult.OK)
            {
                if (OnFileLoad != null)
                    OnFileLoad(openFileDialog.FileName);
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
    }
}
