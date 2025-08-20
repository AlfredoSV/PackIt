using Microsoft.Win32;
using System.IO;
using System.IO.Compression;
using System.Windows;

namespace PackIt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FileViewModel fileViewModel;
        public MainWindow()
        {
            InitializeComponent();
            fileViewModel = (FileViewModel)this.DataContext;
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Multiselect = true;

            if (!(bool)openFileDialog.ShowDialog()!)
            {
                return;
            }

            string[] values = openFileDialog.SafeFileName.Split('.');

            if (!(values.Length >= 2))
            {
                return;
            }

            fileViewModel.Name = $"Name: {values[0]}";
            fileViewModel.Type = $"Type: {values[values.Length - 1]}";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "ZIP Folders (.ZIP)|*.zip";
            saveFileDialog.FileName = "data";

            if ((bool)saveFileDialog.ShowDialog()!)
            {
                ZipOneFile(stream: openFileDialog.OpenFile(), pathSave: saveFileDialog.FileName, fileName: openFileDialog.SafeFileName);
            }

        }

        private void ZipOneFile(Stream stream, string pathSave, string fileName)
        {
            MemoryStream memoryStream = new MemoryStream();

            using (var zip = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                var entry = zip.CreateEntry(fileName, CompressionLevel.Optimal);
                using (var streamEntru = entry.Open())
                {
                    stream.Position = 0;
                    stream.CopyTo(streamEntru);
                }
            }

            File.WriteAllBytes(pathSave, memoryStream.ToArray());

            memoryStream.Dispose();
            memoryStream.Close();
            //return memoryStream.ToArray();
        }
    }
}