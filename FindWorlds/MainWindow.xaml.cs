using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
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
                Word = "Hello",
                Progress = 0

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

        private void Button_Search_Click(object sender, RoutedEventArgs e)
        {
            model.Word = Search.Text;
        }

        private void FindsWordsAsunc()
        {

        }

    }



    public class ViewModel
    {
        public string Source { get; set; }
        public string Word { get; set; }
        public double Progress { get; set; }
        public bool IsWaiting { get; set; }
    }

    public class SearchProccesInfo
    {
        public string FileName { get; set; }

        public string Word { get; set; }
        public double Percentage { get; set; }
        public int PercentageInt => (int)Percentage;
        public double ButesPerSecond { get; set; }
        public double MegaButesPerSecond => Math.Round(ButesPerSecond / 1024 / 1024, 1);

        public SearchProccesInfo(string fileName)
        {
            this.FileName = fileName;
        }
    }
}
