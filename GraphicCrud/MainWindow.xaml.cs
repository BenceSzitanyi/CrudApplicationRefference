using System;
using System.Collections.Generic;
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

namespace GraphicCrud
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gw = new GameWindow();
            gw.Show();
        }

        private void DeveloperButton_Click(object sender, RoutedEventArgs e)
        {
            DeveloperWindow dw = new DeveloperWindow();
            dw.Show();
        }

        private void PublisherButton_Click(object sender, RoutedEventArgs e)
        {
            PublisherWindow pw = new PublisherWindow();
            pw.Show();
        }
    }
}
