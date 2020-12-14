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
using HomeLibrary.Model;

namespace HomeLibrary.View
{
    /// <summary>
    /// Interaction logic for PagesPerDayWindow.xaml
    /// </summary>
    public partial class PagesPerDayWindow : Window
    {
        public PagesPerDay PagesPerDay { get; private set; }
        public PagesPerDayWindow(PagesPerDay ppd)
        {
            InitializeComponent();
            PagesPerDay = ppd;
            this.DataContext = PagesPerDay;
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
