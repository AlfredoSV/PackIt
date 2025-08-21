using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PackIt
{
    public class FileViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _type;
        private string _outputPath;

        public string? Name { 
            get { return _name; } 
            set { 
                this._name = value!;
                OnPropertyChanged();
            } 
        }
        public string? Type
        {
            get { return _type; }
            set
            {
                this._type = value!;
                OnPropertyChanged();
            }
        }

        public string OutputPath {
            get { return _outputPath; }
            set
            {
                this._outputPath = value!;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        //private FileType _file;

        public FileViewModel()
        {
            //_file = new FileType();
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class FileType
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
    }
}
