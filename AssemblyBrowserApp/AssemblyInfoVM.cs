using AssemblyBrowserApp.Model;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AssemblyBrowserApp
{
    public class AssemblyInfoVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ModelNode? _assemblyModel;
        public Command SearchAssemblyPath { get; }
        public Command ApplyAssemblyPath { get; }
        private string _assemblyPath = "";
        public string SearchPath
        {
            get { return _assemblyPath; }
            set
            {
                _assemblyPath = value;
                OnPropertyChanged("SearchPath");
            }
        }
        public string AssemblyPath
        {
            get { return _assemblyPath; }
            set
            {
                _assemblyPath = value;
                OnPropertyChanged("AssemblyPath");
            }
        }
        public ModelNode? ModelNode
        {
            get { return _assemblyModel; }
            set
            {
                _assemblyModel = value;
                OnPropertyChanged("ModelNode");
            }
        }
        public AssemblyModel? AssemblyModel
        {
            get { return _assemblyModel as AssemblyModel; }
        }
        public AssemblyInfoVM()
        {
            SearchAssemblyPath = new Command(openFile =>
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.DefaultExt = ".dll";
                dialog.Filter = "Assemblies (.dll)|*.dll";

                if (dialog.ShowDialog() == true)
                {
                    AssemblyPath = dialog.FileName;
                }
            }, canExecute => true);

            ApplyAssemblyPath = new Command(apply =>
            {
                ModelNode = new InformatorModel(_assemblyPath).AssemblyModel;
                AssemblyModel? model = ModelNode as AssemblyModel;
                if (model != null)
                {
                    if (model.ExceptionMessage != "")
                    {
                        MessageBox.Show(model.ExceptionMessage, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }, canExecute => _assemblyPath != null);
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
