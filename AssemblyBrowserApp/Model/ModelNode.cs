using System;
using System.Collections.ObjectModel;
using System.IO;

namespace AssemblyBrowserApp.Model
{
    public class ModelNode
    {
        public string NodeResult { get; protected set; }
        private string _imagePath;
        private string _imagesDirectory = Path.GetFullPath("Images");
        public string ImagePath
        {
            get { return _imagePath; }
            protected set
            {
                _imagePath = Path.Combine(_imagesDirectory, value);
            }
        }
        public ObservableCollection<ModelNode> Children { get; protected set; }
        public ModelNode()
        {
            Children = new ObservableCollection<ModelNode>();
            NodeResult = String.Empty;
            _imagePath = _imagesDirectory;
        }
    }
}
