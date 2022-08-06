using System;
using System.Windows;
using System.Windows.Input;

namespace Lastik_Otel.Islemler
{
    /// <summary>
    /// Interaction logic for MusteriArama.xaml
    /// </summary>
    public partial class MusteriArama : Window
    {
        sorgular sorgu { get; set; }
        MUSTERI musteri { get; set; }
        public YeniKayit yenikayit { get; set; }

        public MusteriArama()
        {
            InitializeComponent();
            sorgu = new sorgular();
            dg.ItemsSource = sorgu.MusteriGetir();
        }

        private void dg_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                musteri = (MUSTERI)dg.SelectedItem;

                if (musteri != null)
                {
                    yenikayit.musteriGetirKaydetten(musteri.MUSTERI_ID);
                    Close();
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