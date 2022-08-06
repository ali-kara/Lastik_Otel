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

namespace Lastik_Otel.GorunumF
{
    /// <summary>
    /// Interaction logic for MusteriHareketArama.xaml
    /// </summary>
    public partial class MusteriHareketArama : UserControl
    {
        sorgular sorgu { get; set; }
        public MusteriHareketArama()
        {
            sorgu = new sorgular();
            InitializeComponent();

            rdoBitmeyen.IsChecked = true;
        }

        private void rdo_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                int durum;

                if (rdoTumu.IsChecked == true)
                {
                    durum = 0;
                }
                else
                {
                    durum = 1;
                }

                DataTable dt = sorgu.MusteriHareketGetirDurumaGore(durum);

                if (dt != null)
                {
                    dg.ItemsSource = dt.AsDataView();
                }
                else
                {
                    MessageBox.Show("Hata Var. Muhtemelen Bağlantı Sağlanamadı. Hata Dosyalarına Bakınız.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Loglama.Write(ex.ToString());
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
                    wnd.Content = new YeniKayit(MUSTERI_ID, MUSTERI_HAREKET_ID,1);
                    wnd.SizeToContent = SizeToContent.WidthAndHeight;
                    wnd.ResizeMode = ResizeMode.NoResize;
                    wnd.ShowDialog();
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
