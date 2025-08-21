using Microsoft.Win32;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows;

namespace PackIt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FileViewModel fileViewModel;
        public IProgress<int> progress;

        public MainWindow()
        {
            InitializeComponent();
            this.progress = new Progress<int>((value) =>
            {
                progressUploadFile.Value = value;
            });

            fileViewModel = (FileViewModel)this.DataContext;
        }

        private async void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            this.fileViewModel.OutputPath = "";
            this.fileViewModel.Data = null;
            this.progress.Report(0);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "ZIP Folders (.ZIP)|*.zip";
            saveFileDialog.FileName = "data";

            if (!(bool)openFileDialog.ShowDialog()!)
            {
                return;
            }

            fileViewModel.Data = new FileZip[openFileDialog.SafeFileNames.Length];
            Stream[] streams = openFileDialog.OpenFiles();

            for (int i = 0; i < fileViewModel.Data.Length; i++)
            {
                fileViewModel.Data[i] = new FileZip();
                fileViewModel.Data[i].Name = openFileDialog.SafeFileNames[i];
                fileViewModel.Data[i].Stream = streams[i];
                fileViewModel.Data[i].Type = openFileDialog.SafeFileNames[i].Split(".")[openFileDialog.SafeFileNames[i].Split(".").Length - 1];
                fileViewModel.Data[i].Path = openFileDialog.FileName.Replace(fileViewModel.Data[i].Name ?? string.Empty, "");
            }

            if ((bool)saveFileDialog.ShowDialog()!)
            {
                await ZipOneFile(fileViewModel.Data, saveFileDialog.FileName);
                this.fileViewModel.OutputPath = saveFileDialog.FileName;
            }
        }

        private async Task ZipOneFile(FileZip[] files, string pathSave)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                int countFile = 70 / files.Length;
                int report = 0;
                using (var zip = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var entry = zip.CreateEntry(file.Name ?? string.Empty, CompressionLevel.Optimal);
                        this.progress.Report(report);
                        using (var streamEntru = entry.Open())
                        {
                            file.Stream!.Position = 0;
                            await file.Stream.CopyToAsync(streamEntru);
                        }
                        report += countFile;
                    }
                }

                await File.WriteAllBytesAsync(pathSave, memoryStream.ToArray());
                await memoryStream.DisposeAsync();
                memoryStream.Close();
                this.progress.Report(100);
                //return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}