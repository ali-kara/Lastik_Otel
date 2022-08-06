using Lastik_Otel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lastik_Otel.EkDosyalar
{
    /// <summary>
    /// Interaction logic for genelParametreler.xaml
    /// </summary>
    public partial class GenelParametreler : Window
    {
        sorgular webservis1 = new sorgular();
        public GenelParametreler()
        {
            InitializeComponent();

        }
        void tabKilitle()
        {
            for (int i = 0; i < this.myTabControl.Items.Count; i++)
            {
                TabItem ti = (TabItem)this.myTabControl.Items[i];
                ti.IsEnabled = false;
            }
        }
        void tabCoz()
        {
            for (int i = 0; i < this.myTabControl.Items.Count; i++)
            {
                TabItem ti = (TabItem)this.myTabControl.Items[i];
                ti.IsEnabled = true;
            }
        }

        #region GENEL PARAMETRELER TIP2 DEĞER
        private void tabTip2Deger_Loaded(object sender, RoutedEventArgs e)
        {
            getirParametreTip2Deger();
        }



        private void btnDuzenle_Click(object sender, RoutedEventArgs e)
        {
            if (btnDuzenle.Content.ToString() == "DÜZENLE")
            {
                lstParametre.IsEnabled = false;
                txtDeger.IsEnabled = true;
                tabKilitle();

                btnDuzenle.Content = "KAYDET";
            }
            else
            {
                if (guncelleParametreTip2Deger())
                {
                    getirParametreTip2Deger();

                    lstParametre.IsEnabled = true;
                    txtDeger.IsEnabled = false;
                    tabCoz();


                    btnDuzenle.Content = "DÜZENLE";

                    MessageBox.Show("Güncelleme İşlemi Başarıyla Tamamlandı.", "Bilgi", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
        private void getirParametreTip2Deger()
        {
            try
            {
                //    lstParametre.ItemsSource = webservis1.parametre2GetirMK(2).AsDataView();

                lstParametre.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool guncelleParametreTip2Deger()
        {
            try
            {
                if (lstParametre.SelectedIndex == -1)
                {
                    MessageBox.Show("Lütfen Listeden Bir Parametre Seçiniz", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return false;
                }

                object secilen = lstParametre.SelectedItem;
                DataRow row = ((DataRowView)secilen).Row;
                int id = Convert.ToInt32(row["PARAMETRE2_KODU"]);
                int parametreTipi = Convert.ToInt32(row["PARAMETRE_TIPI"]);
                string parametreAciklama = (row["PARAMETRE2_ACIKLAMA"]).ToString();


                //   webservis1.parametre2Guncelle(id, 0, txtDeger.Text, parametreAciklama, parametreTipi, MainWindow.userkodtut);
                return true;

            }
            catch
            {
                return false;
            }

        }
        private void listTakvimDetay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object secilen = lstParametre.SelectedItem;
                DataRow row = ((DataRowView)secilen).Row;

                txtAciklama.Text = row["PARAMETRE2_ACIKLAMA"].ToString();
                txtDeger.Text = row["PARAMETRE_DEGER"].ToString();

            }
            catch
            {
                txtAciklama.Text = "";
                txtDeger.Text = "";
            }
        }
        #endregion

        #region GENEL PARAMETRELER TIP1 CHECKBOX
        private void tabTip1Checkbox_Loaded(object sender, RoutedEventArgs e)
        {
            getirParametreTip1CheckBox();
        }

        private void getirParametreTip1CheckBox()
        {
            try
            {
                //   lstParametreTip1CheckBox.ItemsSource = webservis1.parametre2GetirMK(1).AsDataView();
                lstParametreTip1CheckBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void chPrmDurum_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chkGelen = sender as CheckBox;
            int index = lstParametreTip1CheckBox.Items.IndexOf(chkGelen.DataContext);

            object secilen = lstParametreTip1CheckBox.Items[index];
            DataRow row = ((DataRowView)secilen).Row;
            int id = Convert.ToInt32(row["PARAMETRE2_KODU"]);
            int parametreTipi = Convert.ToInt32(row["PARAMETRE_TIPI"]);
            string parametreAciklama = (row["PARAMETRE2_ACIKLAMA"]).ToString();

            //  webservis1.parametre2Guncelle(id, 1, null, parametreAciklama, parametreTipi, MainWindow.userkodtut);

            getirParametreTip1CheckBox();
        }

        private void chPrmDurum_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox chkGelen = sender as CheckBox;
            int index = lstParametreTip1CheckBox.Items.IndexOf(chkGelen.DataContext);

            object secilen = lstParametreTip1CheckBox.Items[index];
            DataRow row = ((DataRowView)secilen).Row;
            int id = Convert.ToInt32(row["PARAMETRE2_KODU"]);
            int parametreTipi = Convert.ToInt32(row["PARAMETRE_TIPI"]);
            string parametreAciklama = (row["PARAMETRE2_ACIKLAMA"]).ToString();
            //  webservis1.parametre2Guncelle(id, 0, null, parametreAciklama, parametreTipi, MainWindow.userkodtut);

            getirParametreTip1CheckBox();
        }
        #endregion

    }
}