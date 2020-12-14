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
    /// Interaction logic for ReadBookWindow.xaml
    /// </summary>
    public partial class ReadBookWindow : Window
    {
        public ReadBook ReadBook { get; private set; }
        public ReadBookWindow(ReadBook rb)
        {
            InitializeComponent();
            ReadBook = rb;
            this.DataContext = ReadBook;
        }
        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
