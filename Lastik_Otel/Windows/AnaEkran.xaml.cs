using Lastik_Otel.EkDosyalar;
using Lastik_Otel.Gorunum;
using Lastik_Otel.Islemler;
using Lastik_Otel.Yardim;
using System;
using System.Windows;
using System.Windows.Controls;
using Wpf.Controls;

namespace Lastik_Otel
{
    /// <summary>
    /// Interaction logic for AnaEkran.xaml
    /// </summary>
    public partial class AnaEkran : Window
    {
        public AnaEkran()
        {
            InitializeComponent();
        }

        private void mi_versiyon_Click(object sender, RoutedEventArgs e)
        {
            Versiyonlar ver = new Versiyonlar();
            ver.ShowDialog();
        }

        private void mi_hakkinda_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void miCikis_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Uygulamadan Çıkmak İstediğinize Emin misiniz?","Uyarı",MessageBoxButton.YesNo, MessageBoxImage.Question)== MessageBoxResult.No)
            {
                return;
            }

            Environment.Exit(0);
        }

        private void miYeniIslemKaydi_Click(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new YeniKayit());

        }

        private void miYeniMusteri_Click(object sender, RoutedEventArgs e)
        {
            MusteriEkle ekle = new MusteriEkle(enumMusteriEkranModu.MusteriEklemeModu);
            ekle.ShowDialog();

            if (DialogResult == true)
            {
                MessageBox.Show(ekle._MusteriId.ToString());
            }
        }

        private void splitButton_Click(object sender, RoutedEventArgs e)
        {
            var a = sender as SplitButton;
            a.IsContextMenuOpen = true;
        }

        private void miMusteriListesi_Click(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new MusteriDuzenlemeListesi());
        }

        private void miAyarlar_Click(object sender, RoutedEventArgs e)
        {
            mesaj();
        }

        void LoadUserControl(UserControl user)
        {
            gridUser.Children.Clear();
            gridUser.Children.Add(user);
        }

        private void mi_lisans_Click(object sender, RoutedEventArgs e)
        {
            mesaj();
        }

        void mesaj()
        {
            MessageBox.Show("Bu Özellik Sonraki Versiyonlarda Gelecektir.");
        }

        private void miIslemBitir_Click(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new IslemHareketListesi());
        }

        private void miBitmeyenIslemListesi_Click(object sender, RoutedEventArgs e)
        {
            LoadUserControl(new MusteriHareketArama());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GenelIslemler.killWordInstance();
            Environment.Exit(0);
        }
    }
}
