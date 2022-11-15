using AssemblyBrowserApp.Model;
using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AssemblyBrowserApp
{
    public class AssemblyInfoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private AssemblyModel? _assemblyModel;
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
        public AssemblyModel? AssemblyModel
        {
            get { return _assemblyModel; }
            set
            {
                _assemblyModel = value;
                OnPropertyChanged("AssemblyModel");
            }
        }

        public AssemblyInfoViewModel()
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
                AssemblyModel = new InformatorModel(_assemblyPath).AssemblyModel;
                if (AssemblyModel.ExceptionMessage != "")
                {
                    MessageBox.Show(AssemblyModel.ExceptionMessage, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }, canExecute => _assemblyPath != null);
        }
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

    }
}
