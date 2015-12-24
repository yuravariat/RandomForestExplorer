using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RandomForestExplorer.Annotations;

namespace RandomForestExplorer.Data
{
    public class DataModel : INotifyPropertyChanged
    {
        private string _fileName;
        private string _relation;

        public DataModel()
        {
            _fileName = string.Empty;
            _relation = string.Empty;
            Classes = new ObservableCollection<string>();
            Instances = new ObservableCollection<Instance>();
            Instances.CollectionChanged += (p_sender, p_args) => OnPropertyChanged("TotalInstances");
            Features = new ObservableCollection<Feature>();
            Features.CollectionChanged += (p_sender, p_args) => OnPropertyChanged("TotalFeatures");
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; OnPropertyChanged(); }
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

        public ObservableCollection<string> Classes { get; set; }
        public ObservableCollection<Instance> Instances { get; set; }
        public ObservableCollection<Feature> Features { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string p_propertyName = null)
        {
            if (PropertyChanged != null) 
                PropertyChanged(this, new PropertyChangedEventArgs(p_propertyName));
        }
    }
}
