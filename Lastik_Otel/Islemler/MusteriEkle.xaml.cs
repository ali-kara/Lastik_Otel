using Lastik_Otel.Gorunum;
using System.Windows;

namespace Lastik_Otel
{
    public enum enumMusteriEkranModu
    {
        MusteriEklemeModu,
        MusteriGuncellemeModu
    };
    /// <summary>
    /// Interaction logic for MusteriEkle.xaml
    /// </summary>
    public partial class MusteriEkle : Window
    {
        sorgular sorgu { get; set; }
        MUSTERI musteri { get; set; }
        private enumMusteriEkranModu mod { get; set; }
        public YeniKayit yenikayit { get; set; }

        public MusteriDuzenlemeListesi _MusteriDuzenlemeListesi { get; set; }
        
            private int _musteriId;
        public int _MusteriId
    {
        get { return _musteriId; }
        set { _musteriId = value; }
    }

        public MusteriEkle()
        {
            InitializeComponent();
        }

        public MusteriEkle(enumMusteriEkranModu _mod = enumMusteriEkranModu.MusteriEklemeModu)
        {
            InitializeComponent();

            sorgu = new sorgular();            
            musteri = new MUSTERI();
            DataContext = musteri;
            mod = _mod;
        }

        public MusteriEkle(int MUSTERI_ID, enumMusteriEkranModu _mod)
        {
            InitializeComponent();

            sorgu = new sorgular();
            musteri = sorgu.MusteriGetirById(MUSTERI_ID);
            if (!string.IsNullOrEmpty(musteri.MUSTERI_ID.ToString().Trim()) || musteri.MUSTERI_ID == 0)
            {
                btnKaydet.Content = "Güncelle";
            }
            DataContext = musteri;
            mod = _mod;
        }


        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (mod == enumMusteriEkranModu.MusteriGuncellemeModu)
            {
                ProcessInfo pi = sorgu.MusteriGuncelle(musteri);
                if (pi == ProcessInfo.Hata_Var)
                {
                    MessageBox.Show("Hata Var");
                }
                else
                {
                    MessageBox.Show("Müşteri Güncelleme Başarılı");
                    DialogResult = true;
                    Close();
                }
            }
            else
            {
                int MUSTERI_ID = sorgu.MusteriKaydet(musteri);
                if (MUSTERI_ID == (int)ProcessInfo.Hata_Var)
                {
                    MessageBox.Show("Hata Var");
                }
                else
                {
                    MessageBox.Show("Müşteri Ekleme Başarılı");
                    DialogResult = true;

                    // Yeni hareketten gelen kayıt işlemi için
                    if (yenikayit != null)
                    {
                        yenikayit.musteriGetirKaydetten(MUSTERI_ID);
                    }

                    Close();
                }
            }
        }
    }
}