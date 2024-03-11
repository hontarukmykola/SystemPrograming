using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace AsyncAwait
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Random rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //int value = GenerateValue();
            //Task<int> task = Task.Run(GenerateValue);
            //list.Items.Add(task.Result);

            //await task;
            //MessageBox.Show("Complited!!!");
            //int value = await task;
            list.Items.Add(await GenerateValueAsync());
            list.Items.Add(await GenerateValueAsync());
            list.Items.Add(await GenerateValueAsync());
            list.Items.Add(await GenerateValueAsync());
            list.Items.Add(await GenerateValueAsync());

            await GenerateValueAsync();
        }
        int GenerateValue()
        {
            Thread.Sleep(rnd.Next(5000));
            MessageBox.Show("Generated!!!");
            return rnd.Next(1000);
        }

        Task<int> GenerateValueAsync()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(rnd.Next(10000));
                return rnd.Next(1000);

            });
        }
    }
}
