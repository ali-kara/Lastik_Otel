using System;
using System.Collections.Generic;
using System.Data;
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

namespace Lastik_Otel
{
    /// <summary>
    /// Interaction logic for MusteriListesi.xaml
    /// </summary>
    public partial class MusteriListesi : UserControl
    {
        sorgular sorgu { get; set; }
        public MusteriListesi()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            sorgu = new sorgular();
            dgHastaListesi.ItemsSource = sorgu.MusteriGetir();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int index = dgHastaListesi.Items.IndexOf(button.DataContext);
            object secilenTest = dgHastaListesi.Items[index];

            DataRow row = ((DataRowView)secilenTest).Row;
            int MUSTERI_ID = Convert.ToInt32(row["MUSTERI_ID"]);


            Window musteriEkle = new Window
            {
                Content = new MusteriEkle(MUSTERI_ID, enumMusteriEkranModu.MusteriGuncellemeModu),
                SizeToContent = SizeToContent.WidthAndHeight,                
            };

            musteriEkle.ShowDialog();

        }
    }
}
