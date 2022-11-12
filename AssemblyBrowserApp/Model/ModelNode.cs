using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.Pkcs;
using System.IO;

namespace AssemblyBrowserApp.Model
{
    public class ModelNode
    {
        public string NodeResult { get; protected set; }
        private string _imagePath;
        public string ImagePath { 
            get { return _imagePath;} 
            protected set 
            {
                _imagePath = Path.GetFullPath(value);
            } 
        }
        public ObservableCollection<ModelNode> Children { get; protected set; }
        public ModelNode()
        {
            Children = new ObservableCollection<ModelNode>();
            NodeResult = String.Empty;
            _imagePath = Path.GetFullPath("Images/Assembly.png");
        }
    }
}
