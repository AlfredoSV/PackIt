using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PackIt
{
    public class FileViewModel : INotifyPropertyChanged
    {
        private FileZip[] _data;
        private string _outputPath;
        
        public string OutputPath {
            get { return _outputPath; }
            set
            {
                this._outputPath = value!;
                OnPropertyChanged();
            }
        }

        public FileZip[] Data {
            get { return _data; }
            set
            {
                this._data = value!;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public FileViewModel()
        {
        }

        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class FileZip
    {
        public string? Path { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public Stream? Stream { get; set; }
    }
}
