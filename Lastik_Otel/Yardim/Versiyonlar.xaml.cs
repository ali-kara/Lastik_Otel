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

namespace Lastik_Otel.Yardim
{
    /// <summary>
    /// Interaction logic for Versiyonlar.xaml
    /// </summary>
    public partial class Versiyonlar : Window
    {
        public Versiyonlar()
        {
            InitializeComponent();
            txtVersiyon.Text = Properties.Resources.Versiyon;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void btnCikis_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}