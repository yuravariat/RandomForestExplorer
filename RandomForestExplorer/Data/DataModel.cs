using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RandomForestExplorer.Annotations;
using System.Collections.Generic;

namespace RandomForestExplorer.Data
{
    public class DataModel : INotifyPropertyChanged
    {
        private bool _isReady;
        private bool _inProcess;
        private string _fileName;
        private string _trainingFileName;
        private string _relation;
        private bool _trainingEnabled;
        private bool _trainingFromFile;
        private DecisionTrees.TreeOutput dataType;

        public DataModel()
        {
            _fileName = string.Empty;
            _relation = string.Empty;
            Classes = new ObservableCollection<string>();
            Instances = new ObservableCollection<Instance>();
            Instances.CollectionChanged += (p_sender, p_args) => OnPropertyChanged("TotalInstances");
            Features = new ObservableCollection<Feature>();
            Features.CollectionChanged += (p_sender, p_args) => OnPropertyChanged("TotalFeatures");
            TrainingInstances = new List<Instance>();
        }

        public void Reset()
        {
            Classes.Clear();
            Instances.Clear();
            Features.Clear();
            TrainingInstances.Clear();
        }

        public bool IsReady
        {
            get { return _isReady; }
            set { _isReady = value; OnPropertyChanged(); }
        }

        public bool InProcess
        {
            get { return _inProcess; }
            set { _inProcess = value; OnPropertyChanged(); }
        }

        public bool TrainingFromFile
        {
            get { return _trainingFromFile; }
            set { _trainingFromFile = value; OnPropertyChanged(); }
        }

        public bool TrainingFromData
        {
            get { return !_trainingFromFile; }
            set { _trainingFromFile = !value; OnPropertyChanged(); }
        }

        public bool TrainingEnabled
        {
            get { return _trainingEnabled; }
            set { _trainingEnabled = value; OnPropertyChanged(); }
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; OnPropertyChanged(); }
        }

        public string TrainingFileName
        {
            get { return _trainingFileName; }
            set { _trainingFileName = value; OnPropertyChanged(); }
        }

        public string Relation
        {
            get { return _relation; }
            set { _relation = value; OnPropertyChanged(); }
        }

        public int TotalInstances
        {
            get { return Instances.Count; }
        }

        public int TotalFeatures
        {
            get { return Features.Count; }
        }
        public DecisionTrees.TreeOutput DataType
        {
            get { return dataType; }
            set { dataType = value;}
        }

        public ObservableCollection<string> Classes { get; set; }
        public ObservableCollection<Instance> Instances { get; set; }
        public ObservableCollection<Feature> Features { get; set; }
        public List<Instance> TrainingInstances { get; set; }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string p_propertyName = null)
        {
            if (PropertyChanged != null) 
                PropertyChanged(this, new PropertyChangedEventArgs(p_propertyName));
        }
        #endregion
    }
}
