using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Lastik_Otel.Gorunum
{
    /// <summary>
    /// Interaction logic for MusteriDuzenlemeListesi.xaml
    /// </summary>
    public partial class MusteriDuzenlemeListesi : UserControl
    {
        sorgular sorgu { get; set; }
        public MusteriDuzenlemeListesi()
        {
            InitializeComponent();
            sorgu = new sorgular();
            musteriGetir();
        }

        private void musteriGetir()
        {
            dg.ItemsSource = sorgu.MusteriGetir();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ac();
        }

        private void dg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ac();
        }

        void ac()
        {
            try
            {
                MUSTERI musteri = (MUSTERI)dg.SelectedItem;

                if (musteri != null)
                {
                    MusteriEkle ekle = new MusteriEkle(musteri.MUSTERI_ID, enumMusteriEkranModu.MusteriGuncellemeModu);
                    ekle._MusteriDuzenlemeListesi = this;
                    ekle.ShowDialog();

                    musteriGetir();
                }
            }
            catch (Exception ex)
            {
                Loglama.Write(ex.ToString());
                MessageBox.Show("Hata:/n" + ex.ToString());
            }
        }
    }
}