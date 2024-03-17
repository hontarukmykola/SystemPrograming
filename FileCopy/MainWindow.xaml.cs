using IOExtensions;
using Microsoft.VisualBasic.Logging;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileCopy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel model;
       
        public MainWindow()
        {
            InitializeComponent();
            model = new ViewModel()
            {
                 
                Source = "C:\\Users\\Mykola\\Desktop\\test1\\1.jpg",
                Destination = "C:\\Users\\Mykola\\Desktop\\test",
                Progress = 0

            };


            this.DataContext = model;

        }

        private async void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = System.IO.Path.GetFileName(model.Source);
            //
            string destFilePath = System.IO.Path.Combine(model.Destination, fileName);
            CopyProccesInfo info = new CopyProccesInfo(fileName);
            model.AddProcess(info);
            info.Percentage = 100;
            
            await CopyFileAsyng(model.Source, destFilePath, info);
            MessageBox.Show("Complete");
        }

        private Task CopyFileAsyng(string src, string dest, CopyProccesInfo info)
        {
            //type 3 = using FileTransferManager
            return FileTransferManager.CopyWithProgressAsync(src, dest, (progress) =>
            {
                model.Progress = progress.Percentage;
                info.Percentage = progress.Percentage;
                info.ButesPerSecond = progress.BytesPerSecond;
            }, false);
            //return Task.Run(() =>
            //{
            //1 - using file Class
            //File.Copy(Source, destFilePath, true);
            //2 - FileStream

            //using FileStream srcSream = new FileStream(src, FileMode.Open, FileAccess.Read);
            //using FileStream desStream = new FileStream(dest, FileMode.Create, FileAccess.Write);
            //byte[] buffer = new byte[1024 * 8];
            //int bytes = 0;
            //do
            //{
            //    bytes = srcSream.Read(buffer, 0, buffer.Length);
            //    desStream.Write(buffer, 0, bytes);

            //    float percentage = desStream.Length / ( srcSream.Length / 100);
            //    model.Progress = percentage;  
            //} while (srcSream.Length > 0);


            //srcSream.Close();
            //desStream.Close();
            //});
        }

        private void Open_Source_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                model.Source = dialog.FileName;
                //srcTextBox.Text = Source;
            }
        }

        private void Open_Dest_Button_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if(dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                model.Destination = dialog.FileName;
                //destTextBox.Text = Destination;
            }
        }
    }

    [AddINotifyPropertyChangedInterface]
    public class ViewModel
    {
        private ObservableCollection<CopyProccesInfo> processes;
        public string Source { get; set; }
        public string Destination { get; set; }
        public double Progress { get; set; }
        public bool IsWaiting { get; set; }

        public ViewModel()
        {
            processes = new ObservableCollection<CopyProccesInfo>();
        }
        public IEnumerable<CopyProccesInfo> Processes => processes;

        public void AddProcess(CopyProccesInfo info)
        {
            processes.Add(info);
        }
    }


    [AddINotifyPropertyChangedInterface]
    public class CopyProccesInfo
    {
        public string FileName { get; set; }
        public double Percentage { get; set; }
        public int PercentageInt => (int)Percentage;
        public double ButesPerSecond { get; set; }
        public double MegaButesPerSecond =>Math.Round(ButesPerSecond / 1024/1024, 1);

        public CopyProccesInfo(string fileName)
        {
            this.FileName = fileName;
        }
    }

}
