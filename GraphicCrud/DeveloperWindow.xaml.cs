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
using System.Windows.Shapes;

namespace GraphicCrud
{
    /// <summary>
    /// Interaction logic for DeveloperWindow.xaml
    /// </summary>
    public partial class DeveloperWindow : Window
    {
        public DeveloperWindow()
        {
            InitializeComponent();
        }

        private void MostGamesButtonClick(object sender, RoutedEventArgs e)
        {
            GetDevelopersWithMostGames gdwmg = new GetDevelopersWithMostGames();
            gdwmg.Show();
        }

        private void DevelopersByPublisherButton(object sender, RoutedEventArgs e)
        {
            DevelopersByPublisher dbb = new DevelopersByPublisher();
            dbb.Show();
        }
    }
}
