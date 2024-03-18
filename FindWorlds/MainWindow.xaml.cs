using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace FindWorlds
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
                Word = "Hello"

            };
            this.DataContext = model;
        }


        private void Open_Source_Button_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                model.Source = dialog.FileName;

                string[] FileNames = Directory.GetFiles(model.Source, "*.txt", SearchOption.AllDirectories);
                
            }
        }

        private async void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            model.Word = Search.Text;

            string[] fileEntries = Directory.GetFiles(model.Source, "*.txt", SearchOption.AllDirectories);
            foreach (string fileName in fileEntries)
            {
                string nameOfFile = System.IO.Path.GetFileName(fileName);

                SearchProccesInfo searchInfo = new SearchProccesInfo(nameOfFile, fileName, model.Word);
                //ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessFile), fileName);
                //MessageBox.Show($"{fileName}");
                model.AddProcess(searchInfo);
                await FileInfoAsunc(searchInfo);


            }

       



            //string[] files = Directory.GetFiles(model.Source);
            //foreach (string file in files)
            //{
            //    string text = File.ReadAllText(file);
            //    Thread thread = new Thread(()=>TextAnalyse(text, model.Word));
            //    thread.Start(text);
            //}
            //Thread.Sleep(2000);


        }

        private Task FileInfoAsunc(SearchProccesInfo searchInfo)
        {
            return Task.Run(() =>
            {

                string text = File.ReadAllText(searchInfo.FullPath);
                string[] Words = text.Split(new char[] { ' ', '.', '\n',',','!', '?' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in Words)
                {
                    if (model.Word == word)
                    {
                        searchInfo.CountWord++;
                    }
                }
                //searchInfo.CountWord = Words.Count(w => w == searchInfo.Word);

               // MessageBox.Show(searchInfo.CountWord.ToString());

            });
        }


    }



    [AddINotifyPropertyChangedInterface]
    public class ViewModel
    {
        public string Source { get; set; }
        public string Word { get; set; }
        //public double Percentage { get; set; }

        private ObservableCollection<SearchProccesInfo> files;

        public ViewModel()
        {
            files = new ObservableCollection<SearchProccesInfo>();
        }
        public IEnumerable<SearchProccesInfo> Files => files;   

        public void AddProcess(SearchProccesInfo info)
        {
            files.Add(info);
        }

    }
        [AddINotifyPropertyChangedInterface]

    public class SearchProccesInfo
    {
        
        public string FileName { get; set; }
        public string Word { get; set; }
        public string FullPath { get; set; }
        public int CountWord { get; set; }

        //public int PercentageInt => (int)Percentage;

       
        public SearchProccesInfo(string fileName, string FullPath, string Word)
        {
            this.FileName = fileName;
            this.FullPath = FullPath;
            this.Word = Word;

        }

        public override string ToString()
        {
            
            return $"{FileName} {FullPath} {CountWord}";
        }
    }
}
