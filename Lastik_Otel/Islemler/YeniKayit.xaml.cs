using Lastik_Otel.EkDosyalar;
using Lastik_Otel.Islemler;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lastik_Otel
{
    /// <summary>
    /// Interaction logic for YeniKayit.xaml
    /// </summary>
    public partial class YeniKayit : UserControl
    {
        #region Constructor
        private MUSTERI _musteri = null;
        MUSTERI_HAREKET musteriHareket { get; set; }
        sorgular sorgu { get; set; }
        MusteriArama ara { get; set; }
        MUSTERI musteri
        {
            get { return _musteri; }
            set
            {
                _musteri = value;
                if (_musteri == null)
                {
                    btnKaydet.IsEnabled = false;
                }
                else
                {
                    btnKaydet.IsEnabled = true;
                }
            }
        }
        #endregion


        void buttonAyarlaYeniKayit()
        {
            btnKaydet.Visibility = Visibility.Visible;
            btnYeniMusteri.Visibility = Visibility.Visible;
            btnMusteriGetir.Visibility = Visibility.Visible;

            btnIslemBitir.Visibility = Visibility.Hidden;
            btnYazdir.Visibility = Visibility.Hidden;
            btnGuncelle.Visibility = Visibility.Hidden;
            btnSil.Visibility = Visibility.Hidden;
        }

        void buttonAyarlaGuncelleme()
        {
            btnKaydet.Visibility = Visibility.Hidden;
            btnYeniMusteri.Visibility = Visibility.Hidden;
            btnMusteriGetir.Visibility = Visibility.Hidden;

            btnIslemBitir.Visibility = Visibility.Visible;
            btnYazdir.Visibility = Visibility.Visible;
            btnGuncelle.Visibility = Visibility.Visible;
            btnSil.Visibility = Visibility.Visible;
        }

        void buttonAyarlaIslemBittiSonrasi()
        {
            btnKaydet.Visibility = Visibility.Hidden;
            btnYeniMusteri.Visibility = Visibility.Hidden;
            btnMusteriGetir.Visibility = Visibility.Hidden;
            btnIslemBitir.Visibility = Visibility.Hidden;
            btnGuncelle.Visibility = Visibility.Hidden;
            btnSil.Visibility = Visibility.Hidden;


            btnYazdir.Visibility = Visibility.Visible;

            grdAracBilgileri.IsEnabled = false;
            grdKayitBilgileri.IsEnabled = false;
        }

        public YeniKayit()
        {
            InitializeComponent();

            sorgu = new sorgular();

            musteriHareket = new MUSTERI_HAREKET();

            grdAracBilgileri.DataContext = musteriHareket;
            grdKayitBilgileri.DataContext = musteriHareket;
            buttonAyarlaYeniKayit();
        }
        public YeniKayit(int MUSTERI_ID, int MUSTERI_HAREKET_ID)
        {
            InitializeComponent();

            sorgu = new sorgular();

            musteri = sorgu.MusteriGetirById(MUSTERI_ID);
            musteriHareket = sorgu.MusteriHareketGetirbyId(MUSTERI_HAREKET_ID);

            grdMusteri.DataContext = musteri;
            grdAracBilgileri.DataContext = musteriHareket;
            grdKayitBilgileri.DataContext = musteriHareket;

            if (musteriHareket.ISLEM_BITTI == 1)
            {
                buttonAyarlaIslemBittiSonrasi();
            }
            else
            {
                buttonAyarlaGuncelleme();
            }
        }

        public YeniKayit(int MUSTERI_ID, int MUSTERI_HAREKET_ID, int SADECE_GORUNTULE)
        {
            InitializeComponent();

            sorgu = new sorgular();

            musteri = sorgu.MusteriGetirById(MUSTERI_ID);
            musteriHareket = sorgu.MusteriHareketGetirbyId(MUSTERI_HAREKET_ID);

            grdMusteri.DataContext = musteri;
            grdAracBilgileri.DataContext = musteriHareket;
            grdKayitBilgileri.DataContext = musteriHareket;

            buttonAyarlaIslemBittiSonrasi();

        }


        private void btnYeniMusteri_Click(object sender, RoutedEventArgs e)
        {
            MusteriEkle ekle = new MusteriEkle(enumMusteriEkranModu.MusteriEklemeModu);
            ekle.yenikayit = this;
            ekle.ShowDialog();
        }

        public void musteriGetirKaydetten(int MUSTERI_ID)
        {
            try
            {
                musteri = sorgu.MusteriGetirById(MUSTERI_ID);
                grdMusteri.DataContext = musteri;

                musteriHareket = sorgu.MusteriSonKayitGetir(musteri.MUSTERI_ID);

                grdAracBilgileri.DataContext = musteriHareket;
                grdKayitBilgileri.DataContext = musteriHareket;

                musteriHareket.LASTIK_ADEDI = 4;
            }
            catch (Exception ex)
            {
                grdAracBilgileri.DataContext = null;
                grdKayitBilgileri.DataContext = null;

                musteri = null;

                Loglama.Write(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnMusteriGetir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ara = new MusteriArama();
                ara.yenikayit = this;
                ara.ShowDialog();
            }
            catch (Exception ex)
            {
                Loglama.Write(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (musteri == null)
                {
                    MessageBox.Show("Müşteri Seçilmedi");
                    return;
                }

                musteriHareket.MUSTERI_ID = musteri.MUSTERI_ID;
                if (musteriHareket.VERILEN_TARIH == DateTime.MinValue)
                {
                    musteriHareket.VERILEN_TARIH = DateTime.Today;
                }

                int sonuc = sorgu.MusteriHareketKaydet(musteriHareket);


                if (sonuc == (int)ProcessInfo.Hata_Var)
                {
                    MessageBox.Show("Hata Var");
                    return;
                }

                musteriHareket = sorgu.MusteriHareketGetirbyId(sonuc);
                sorgu.MusteriSonKayitGuncelle(musteriHareket);

                buttonAyarlaGuncelleme();

                grdAracBilgileri.DataContext = musteriHareket;
                grdKayitBilgileri.DataContext = musteriHareket;
            }
            catch (Exception ex)
            {
                grdAracBilgileri.DataContext = null;
                grdKayitBilgileri.DataContext = null;

                musteriHareket = null;

                Loglama.Write(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnYazdir_Click(object sender, RoutedEventArgs e)
        {
            ArrayList array = new ArrayList();

            array.Add(musteri.ADI + " " + musteri.SOYADI);
            array.Add("No: " + musteriHareket.MUSTERI_HAREKET_ID.ToString());
            array.Add(musteriHareket.PLAKA_NO);
            array.Add(musteriHareket.LASTIK_EBADI);
            array.Add(musteriHareket.ARAC_TIPI);

            Yazdir yazdir = new Yazdir();
            yazdir.CiktiAl(array);
        }

        private void btnOnayla_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sorgu.MusteriHareketBitir(musteriHareket.MUSTERI_HAREKET_ID);
                musteriHareket = sorgu.MusteriHareketGetirbyId(musteriHareket.MUSTERI_HAREKET_ID);

                buttonAyarlaIslemBittiSonrasi();

                grdAracBilgileri.DataContext = musteriHareket;
                grdKayitBilgileri.DataContext = musteriHareket;
            }
            catch (Exception ex)
            {
                grdAracBilgileri.DataContext = null;
                grdKayitBilgileri.DataContext = null;

                Loglama.Write(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (RegexKurallar.SadeceSayimi(e.Text))
            {
                e.Handled = true;
            }
        }

        private void btnGuncelle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (musteri != null && musteriHareket != null)
                {
                    if (musteriHareket.ISLEM_BITTI == 1)
                    {
                        MessageBox.Show("Bitilen İşlemin Kayıdı Güncellenemez.", "Hata", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    if (MessageBox.Show("Müşterinin Kayıdı Güncellenek.\n\n Devam Edilsin mi?", "", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
                    {
                        return;
                    }

                    musteriHareket.MUSTERI_ID = musteri.MUSTERI_ID;
                    if (musteriHareket.VERILEN_TARIH == DateTime.MinValue)
                    {
                        musteriHareket.VERILEN_TARIH = DateTime.Today;
                    }


                    sorgu.MusteriKayitGuncelle(musteriHareket);
                    sorgu.MusteriSonKayitGuncelle(musteriHareket);

                    musteriHareket = sorgu.MusteriHareketGetirbyId(musteriHareket.MUSTERI_HAREKET_ID);

                    grdAracBilgileri.DataContext = musteriHareket;
                    grdKayitBilgileri.DataContext = musteriHareket;

                }
                else
                {
                    MessageBox.Show("Güncellenek Kayıt Bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                Loglama.Write(ex.ToString());
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSil_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (musteri != null && musteriHareket!=null)
                {
                    if (musteriHareket.ISLEM_BITTI == 1)
                    {
                        MessageBox.Show("Bitilen İşlemin Kayıdı Silinemez.","Hata",MessageBoxButton.OK,MessageBoxImage.Warning);
                        return;
                    }

                    if (MessageBox.Show("Müşterinin Kayıdı Siliniyor.\n\n Devam Edilsin mi?","",MessageBoxButton.OKCancel,MessageBoxImage.Question) == MessageBoxResult.Cancel)
                    {
                        return;
                    }

                    int MUSTERI_ID = musteri.MUSTERI_ID;
                    int MUSTERI_HAREKET_ID = musteriHareket.MUSTERI_HAREKET_ID;

                    sorgu.MusteriKayitSil(MUSTERI_HAREKET_ID);

                    musteriHareket = new MUSTERI_HAREKET();

                    grdAracBilgileri.DataContext = musteriHareket;
                    grdKayitBilgileri.DataContext = musteriHareket;

                    buttonAyarlaYeniKayit();
                }
                else
                {
                    MessageBox.Show("Silinecek Kayıt Bulunamadı.");                        
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