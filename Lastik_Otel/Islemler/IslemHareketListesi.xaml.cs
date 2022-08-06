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

namespace Lastik_Otel.Islemler
{
    /// <summary>
    /// Interaction logic for IslemHareketListesi.xaml
    /// </summary>
    public partial class IslemHareketListesi : UserControl
    {
        sorgular sorgu { get; set; }
        public IslemHareketListesi()
        {
            InitializeComponent();

            sorgu = new sorgular();
            Getir();
        }

        private void Getir()
        {
            dg.ItemsSource = sorgu.MusteriHareketGetirBitirmekIcin();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ekranAc();

        }

        private void dg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ekranAc();
        }
        void ekranAc()
        {
            try
            {
                object secilen = dg.SelectedItem;

                if (secilen != null)
                {
                    DataRow row = ((DataRowView)secilen).Row;
                    int MUSTERI_ID = (int)row["MUSTERI_ID"];
                    int MUSTERI_HAREKET_ID = (int)row["MUSTERI_HAREKET_ID"];

                    Window wnd = new Window();
                    wnd.Content = new YeniKayit(MUSTERI_ID, MUSTERI_HAREKET_ID);
                    wnd.SizeToContent = SizeToContent.WidthAndHeight;
                    wnd.ResizeMode = ResizeMode.NoResize;
                    wnd.ShowDialog();

                    Getir();
                }
            }
            catch (Exception ex)
            {
                Loglama.Write(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
