using Microsoft.Win32;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows;

namespace PackIt
{

    public class FileRelation
    {
        public Stream Stream { get; set; }

        public string Name { get; set; }
    }

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
            this.progress.Report(0);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "ZIP Folders (.ZIP)|*.zip";
            saveFileDialog.FileName = "data";

            if (!(bool)openFileDialog.ShowDialog()!)
            {
                return;
            }

            FileRelation[] files = new FileRelation[openFileDialog.SafeFileNames.Length];

            Stream[] streams = openFileDialog.OpenFiles();

            for (int i = 0; i < files.Length; i++)
            {
                files[i] = new FileRelation();
                files[i].Name = openFileDialog.SafeFileNames[i];
                files[i].Stream = streams[i];
            }

            if ((bool)saveFileDialog.ShowDialog()!)
            {
                await Task.Run(async () => {
                    await ZipOneFile(files, saveFileDialog.FileName);
                    this.fileViewModel.OutputPath = saveFileDialog.FileName;
                });
            }
        }

        private async Task ZipOneFile(FileRelation[] files, string pathSave)
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                int countFile = 70/ files.Length;
                int report = 0;
                using (var zip = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var entry = zip.CreateEntry(file.Name, CompressionLevel.Optimal);
                        this.progress.Report(report);
                        using (var streamEntru = entry.Open())
                        {
                            file.Stream.Position = 0;
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