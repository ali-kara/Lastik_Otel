using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;


namespace Lastik_Otel
{
    public enum ProcessInfo
    {
        Basarili = 1,
        Hata_Var = -1
    }
    public class sorgular
    {
        #region Constructor
        OleDbConnection baglan { get; set; }
        OleDbDataAdapter adaptor { get; set; }
        OleDbCommand kos { get; set; }
        DataTable dt { get; set; }
        OleDbDataReader reader { get; set; }
        string baglanString { get; set; }

        MUSTERI_HAREKET musteriHareket { get; set; }

        MUSTERI musteri { get; set; }

        List<MUSTERI> listMusteri { get; set; } 
        #endregion
        public sorgular()
        {
            baglanString = "Provider=Microsoft.Ace.OleDb.12.0;Data Source = Data/data.accdb;Jet OLEDB:Database Password=ALIKARA1905";
            baglan = new OleDbConnection(baglanString);
        }

        public static T MapToClass<T>(OleDbDataReader reader) where T : class
        {
            T returnedObject = Activator.CreateInstance<T>();
            PropertyInfo[] modelProperties = returnedObject.GetType().GetProperties();

            for (int i = 0; i < modelProperties.Length; i++)
            {
                MappingAttribute[] attributes = modelProperties[i].GetCustomAttributes<MappingAttribute>(true).ToArray();

                if (attributes.Length > 0 && attributes[0].ColumnName != null)
                {
                    //  modelProperties[i].SetValue(returnedObject, Convert.ChangeType(reader[attributes[0].ColumnName], modelProperties[i].PropertyType), null);
                    try
                    {
                        if (modelProperties[i].PropertyType == typeof(DateTime))
                        {
                            if (reader[attributes[0].ColumnName] == DBNull.Value)
                            {
                                modelProperties[i].SetValue(returnedObject, DateTime.MinValue, null);
                            }
                            else
                            {
                                modelProperties[i].SetValue(returnedObject, Convert.ChangeType(reader[attributes[0].ColumnName], modelProperties[i].PropertyType), null);
                            }
                        }
                        else
                        {
                            modelProperties[i].SetValue(returnedObject, Convert.ChangeType(reader[attributes[0].ColumnName], modelProperties[i].PropertyType), null);
                        }
                    }
                    catch
                    {


                    }
                }
            }
            return returnedObject;
        }
        private object GetDataValueTime(object value)
        {
            if (value == null || (DateTime)value == DateTime.MinValue || value == null)
            {
                return DBNull.Value;
            }

            return value;
        }
        private object GetDataValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }
            return value;
        }

        public ProcessInfo BaglantiAc()
        {
            try
            {
                baglan.Open();
                baglan.Close();

                return ProcessInfo.Basarili;
            }
            catch (Exception ex)
            {
                baglan.Close();
                Loglama.Write(ex.ToString());
                return ProcessInfo.Hata_Var;
            }
        }
        public DataTable MusteriHareketGetirBitirmekIcin()
        {
            try
            {
                kos = new OleDbCommand("SELECT M.MUSTERI_ID, MUSTERI_HAREKET_ID, VERILEN_TARIH, ALINAN_TARIH, LASTIK_EBADI, LASTIK_TIPI, ARAC_TIPI, ADI,SOYADI, CEP_TEL, CEP_TEL2 FROM MUSTERI_HAREKET MH INNER JOIN MUSTERI M ON MH.MUSTERI_ID = M.MUSTERI_ID WHERE ISLEM_BITTI <> 1 ORDER BY MUSTERI_HAREKET_ID", baglan);

                baglan.Open();

                adaptor = new OleDbDataAdapter(kos);
                dt = new DataTable("a");
                adaptor.Fill(dt);

                baglan.Close();
                return dt;
            }
            catch (Exception ex)
            {
                baglan.Close();
                Loglama.Write(ex.ToString());

                return null;
            }
        }
        public DataTable MusteriHareketGetirDurumaGore(int durum)
        {
            try
            {
                string query = "SELECT M.MUSTERI_ID, MUSTERI_HAREKET_ID, VERILEN_TARIH, ALINAN_TARIH, ISLEM_BITTI, iif(ISLEM_BITTI = 1, 'İŞLEM BİTTİ','DEVAM EDİYOR') AS ISLEM_DURUMU  , LASTIK_EBADI, LASTIK_TIPI, ARAC_TIPI, ADI,SOYADI, CEP_TEL, CEP_TEL2, MH.PLAKA_NO FROM MUSTERI_HAREKET MH INNER JOIN MUSTERI M ON MH.MUSTERI_ID = M.MUSTERI_ID ";


                if (durum == 1) // sadece bitmeyen
                {
                    query += "WHERE MH.ISLEM_BITTI = 0";
                }
                query += " ORDER BY MUSTERI_HAREKET_ID";


                kos = new OleDbCommand(query, baglan);

                baglan.Open();

                adaptor = new OleDbDataAdapter(kos);
                dt = new DataTable("a");
                adaptor.Fill(dt);

                baglan.Close();

                return dt;

            }
            catch (Exception ex)
            {
                baglan.Close();
                Loglama.Write(ex.ToString());

                return null;
            }
        }
        public MUSTERI_HAREKET MusteriHareketGetirbyId(int MUSTERI_HAREKET_ID)
        {
            try
            {
                kos = new OleDbCommand("SELECT * FROM MUSTERI_HAREKET WHERE MUSTERI_HAREKET_ID = @MUSTERI_HAREKET_ID", baglan);
                kos.Parameters.AddWithValue("@MUSTERI_HAREKET_ID", MUSTERI_HAREKET_ID);
                baglan.Open();

                musteriHareket = new MUSTERI_HAREKET();

                using (OleDbDataReader reader = kos.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        musteriHareket = MapToClass<MUSTERI_HAREKET>(reader);
                    }
                }

                baglan.Close();
                return musteriHareket;
            }
            catch (Exception ex)
            {
                baglan.Close();
                Loglama.Write(ex.ToString());

                return null;
            }
        }
        private bool MusteriKayitVarmi(int MUSTERI_ID)
        {
            OleDbConnection baglan2 = new OleDbConnection(baglanString);
            try
            {                
                OleDbCommand kosKayitVarmi = new OleDbCommand("SELECT * FROM MUSTERI_SON_KAYIT WHERE MUSTERI_ID = @MUSTERI_ID", baglan2);

                kosKayitVarmi.Parameters.AddWithValue("@MUSTERI_ID", MUSTERI_ID);
                baglan2.Open();

                using (reader = kosKayitVarmi.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        baglan2.Close();
                        return true;
                    }
                    else
                    {
                        baglan2.Close();
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                baglan2.Close();
                Loglama.Write(ex.ToString());

                return false;
            }
        }
        public ProcessInfo MusteriHareketBitir(int MUSTERI_HAREKET_ID)
        {
            try
            {
                kos = new OleDbCommand("UPDATE MUSTERI_HAREKET SET  ALINAN_TARIH=@ALINAN_TARIH, ISLEM_BITTI = @ISLEM_BITTI WHERE MUSTERI_HAREKET_ID = @MUSTERI_HAREKET_ID", baglan);

                kos.Parameters.AddWithValue("@ALINAN_TARIH", DateTime.Today);
                kos.Parameters.AddWithValue("@ISLEM_BITTI", 1);
                kos.Parameters.AddWithValue("@MUSTERI_HAREKET_ID", MUSTERI_HAREKET_ID);

                baglan.Open();
                kos.ExecuteNonQuery();
                baglan.Close();

                return ProcessInfo.Basarili;
            }
            catch (Exception ex)
            {
                baglan.Close();
                Loglama.Write(ex.ToString());

                return ProcessInfo.Hata_Var;
            }
        }
        public ProcessInfo MusteriSonKayitGuncelle(MUSTERI_HAREKET musteriHareket)
        {
            try
            {
                string query;

                if (MusteriKayitVarmi(musteriHareket.MUSTERI_ID))
                {
                    query = "UPDATE MUSTERI_SON_KAYIT SET  MUSTERI_ID = @MUSTERI_ID, MUSTERI_HAREKET_ID=@MUSTERI_HAREKET_ID, ARAC_TIPI=@ARAC_TIPI, PLAKA_NO=@PLAKA_NO , LASTIK_ADEDI=@LASTIK_ADEDI, LASTIK_MARKASI=@LASTIK_MARKASI, LASTIK_MODELI=@LASTIK_MODELI, LASTIK_EBADI=@LASTIK_EBADI, LASTIK_TIPI=@LASTIK_TIPI, ACIKLAMA=@ACIKLAMA, EKLE_LOG_TARIH=@EKLE_LOG_TARIH  WHERE MUSTERI_ID = @MUSTERI_ID";
                }
                else
                {
                    query = "INSERT INTO MUSTERI_SON_KAYIT  (MUSTERI_ID, MUSTERI_HAREKET_ID, ARAC_TIPI, PLAKA_NO , LASTIK_ADEDI, LASTIK_MARKASI, LASTIK_MODELI, LASTIK_EBADI, LASTIK_TIPI, ACIKLAMA, EKLE_LOG_TARIH)  VALUES (@MUSTERI_ID, @MUSTERI_HAREKET_ID, @ARAC_TIPI, @PLAKA_NO , @LASTIK_ADEDI, @LASTIK_MARKASI, @LASTIK_MODELI, @LASTIK_EBADI, @LASTIK_TIPI, @ACIKLAMA, @EKLE_LOG_TARIH)";
                }

                kos = new OleDbCommand(query, baglan);

                kos.Parameters.AddWithValue("@MUSTERI_ID", GetDataValue(musteriHareket.MUSTERI_ID));
                kos.Parameters.AddWithValue("@MUSTERI_HAREKET_ID", GetDataValue(musteriHareket.MUSTERI_HAREKET_ID));
                kos.Parameters.AddWithValue("@ARAC_TIPI", GetDataValue(musteriHareket.ARAC_TIPI));
                kos.Parameters.AddWithValue("@PLAKA_NO", GetDataValue(musteriHareket.PLAKA_NO));
                kos.Parameters.AddWithValue("@LASTIK_ADEDI", GetDataValue(musteriHareket.LASTIK_ADEDI));
                kos.Parameters.AddWithValue("@LASTIK_MARKASI", GetDataValue(musteriHareket.LASTIK_MARKASI));
                kos.Parameters.AddWithValue("@LASTIK_MODELI", GetDataValue(musteriHareket.LASTIK_MODELI));
                kos.Parameters.AddWithValue("@LASTIK_EBADI", GetDataValue(musteriHareket.LASTIK_EBADI));
                kos.Parameters.AddWithValue("@LASTIK_TIPI", GetDataValue(musteriHareket.LASTIK_TIPI));
                kos.Parameters.AddWithValue("@ACIKLAMA", GetDataValue(musteriHareket.ACIKLAMA));
                kos.Parameters.AddWithValue("@EKLE_LOG_TARIH", DateTime.Today);


                baglan.Open();
                kos.ExecuteNonQuery();
                baglan.Close();

                return ProcessInfo.Basarili;
            }
            catch (Exception ex)
            {
                baglan.Close();
                Loglama.Write(ex.ToString());

                return ProcessInfo.Hata_Var;
            }
        }

        public ProcessInfo MusteriKayitSil(int MUSTERI_HAREKET_ID)
        {
            try
            {
                kos = new OleDbCommand("DELETE FROM MUSTERI_HAREKET WHERE MUSTERI_HAREKET_ID = @MUSTERI_HAREKET_ID ", baglan);


                kos.Parameters.AddWithValue("@MUSTERI_HAREKET_ID", MUSTERI_HAREKET_ID);

                baglan.Open();
                kos.ExecuteNonQuery();
                baglan.Close();

                return ProcessInfo.Basarili;
            }
            catch (Exception ex)
            {
                baglan.Close();
                Loglama.Write(ex.ToString());

                return ProcessInfo.Hata_Var;
            }
        }

        public ProcessInfo MusteriKayitGuncelle(MUSTERI_HAREKET musteriHareket)
        {
            try
            {

                kos = new OleDbCommand("UPDATE MUSTERI_HAREKET SET ARAC_TIPI = @ARAC_TIPI, PLAKA_NO = @PLAKA_NO, LASTIK_ADEDI = @LASTIK_ADEDI, LASTIK_MARKASI = @LASTIK_MARKASI, LASTIK_MODELI = @LASTIK_MODELI, LASTIK_EBADI = @LASTIK_EBADI, LASTIK_TIPI = @LASTIK_TIPI, ACIKLAMA = @ACIKLAMA, EKLE_LOG_TARIH = @EKLE_LOG_TARIH  WHERE MUSTERI_HAREKET_ID = @MUSTERI_HAREKET_ID", baglan);

                kos.Parameters.AddWithValue("@ARAC_TIPI", GetDataValue(musteriHareket.ARAC_TIPI));
                kos.Parameters.AddWithValue("@PLAKA_NO", GetDataValue(musteriHareket.PLAKA_NO));
                kos.Parameters.AddWithValue("@LASTIK_ADEDI", GetDataValue(musteriHareket.LASTIK_ADEDI));
                kos.Parameters.AddWithValue("@LASTIK_MARKASI", GetDataValue(musteriHareket.LASTIK_MARKASI));
                kos.Parameters.AddWithValue("@LASTIK_MODELI", GetDataValue(musteriHareket.LASTIK_MODELI));
                kos.Parameters.AddWithValue("@LASTIK_EBADI", GetDataValue(musteriHareket.LASTIK_EBADI));
                kos.Parameters.AddWithValue("@LASTIK_TIPI", GetDataValue(musteriHareket.LASTIK_TIPI));
                kos.Parameters.AddWithValue("@ACIKLAMA", GetDataValue(musteriHareket.ACIKLAMA));
                kos.Parameters.AddWithValue("@EKLE_LOG_TARIH", DateTime.Today);

                kos.Parameters.AddWithValue("@MUSTERI_HAREKET_ID", GetDataValue(musteriHareket.MUSTERI_HAREKET_ID));

                baglan.Open();
                kos.ExecuteNonQuery();
                baglan.Close();

                return ProcessInfo.Basarili;
            }
            catch (Exception ex)
            {
                baglan.Close();
                Loglama.Write(ex.ToString());

                return ProcessInfo.Hata_Var;
            }
        }

        public MUSTERI_HAREKET MusteriSonKayitGetir(int MUSTERI_ID)
        {
            try
            {
                musteriHareket = new MUSTERI_HAREKET();

                kos = new OleDbCommand("SELECT * FROM MUSTERI_SON_KAYIT WHERE MUSTERI_ID = @MUSTERI_ID", baglan);
                kos.Parameters.AddWithValue("@MUSTERI_ID", MUSTERI_ID);
                baglan.Open();


                using (OleDbDataReader reader = kos.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        musteriHareket = MapToClass<MUSTERI_HAREKET>(reader);
                    }
                }

                baglan.Close();
                musteriHareket.MUSTERI_HAREKET_ID = 0;

                return musteriHareket;
                
            }
            catch (Exception ex)
            {
                baglan.Close();
                Loglama.Write(ex.ToString());
                return musteriHareket;
            }
        }
        public int MusteriHareketKaydet(MUSTERI_HAREKET musteriHareket)
        {
            try
            {
                int donen = 0;
                string query = "Select @@Identity";
                kos = new OleDbCommand("INSERT INTO MUSTERI_HAREKET  (MUSTERI_ID, ARAC_TIPI, PLAKA_NO , LASTIK_ADEDI, LASTIK_MARKASI, LASTIK_MODELI, LASTIK_EBADI, LASTIK_TIPI, ACIKLAMA, VERILEN_TARIH,ALINAN_TARIH, CIKTI_ALINDI)  VALUES (@MUSTERI_ID, @ARAC_TIPI, @PLAKA_NO , @LASTIK_ADEDI, @LASTIK_MARKASI, @LASTIK_MODELI, @LASTIK_EBADI, @LASTIK_TIPI, @ACIKLAMA, @VERILEN_TARIH, @ALINAN_TARIH, @CIKTI_ALINDI)", baglan);

                kos.Parameters.AddWithValue("@MUSTERI_ID", GetDataValue(musteriHareket.MUSTERI_ID));
                kos.Parameters.AddWithValue("@ARAC_TIPI", GetDataValue(musteriHareket.ARAC_TIPI));
                kos.Parameters.AddWithValue("@PLAKA_NO", GetDataValue(musteriHareket.PLAKA_NO));
                kos.Parameters.AddWithValue("@LASTIK_ADEDI", GetDataValue(musteriHareket.LASTIK_ADEDI));
                kos.Parameters.AddWithValue("@LASTIK_MARKASI", GetDataValue(musteriHareket.LASTIK_MARKASI));
                kos.Parameters.AddWithValue("@LASTIK_MODELI", GetDataValue(musteriHareket.LASTIK_MODELI));
                kos.Parameters.AddWithValue("@LASTIK_EBADI", GetDataValue(musteriHareket.LASTIK_EBADI));
                kos.Parameters.AddWithValue("@LASTIK_TIPI", GetDataValue(musteriHareket.LASTIK_TIPI));
                kos.Parameters.AddWithValue("@ACIKLAMA", GetDataValue(musteriHareket.ACIKLAMA));
                kos.Parameters.AddWithValue("@VERILEN_TARIH", GetDataValueTime(musteriHareket.VERILEN_TARIH));
                kos.Parameters.AddWithValue("@ALINAN_TARIH", GetDataValueTime(musteriHareket.ALINAN_TARIH));
                kos.Parameters.AddWithValue("@CIKTI_ALINDI", GetDataValue(musteriHareket.CIKTI_ALINDI));




                baglan.Open();
                kos.ExecuteNonQuery();
                kos.CommandText = query;
                donen = (int)kos.ExecuteScalar();
                baglan.Close();

                if (donen != 0)
                {
                    return donen;
                }
                else
                {
                    return (int)ProcessInfo.Hata_Var;
                }
            }
            catch (Exception ex)
            {
                baglan.Close();
                Loglama.Write(ex.ToString());

                return (int)ProcessInfo.Hata_Var;
            }
        }
        public List<MUSTERI> MusteriGetir()
        {
            listMusteri = new List<MUSTERI>();

            try
            {
                kos = new OleDbCommand("SELECT * FROM MUSTERI ORDER BY MUSTERI_ID", baglan);

                baglan.Open();

                musteri = new MUSTERI();

                using (OleDbDataReader reader = kos.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        musteri = MapToClass<MUSTERI>(reader);
                        listMusteri.Add(musteri);
                    }
                }

                baglan.Close();

                return listMusteri;
            }
            catch (Exception ex)
            {
                baglan.Close();
                Loglama.Write(ex.ToString());

                return listMusteri;
            }
        }
        public ProcessInfo MusteriGuncelle(MUSTERI musteri)
        {
            try
            {
                kos = new OleDbCommand("UPDATE MUSTERI SET ADI=@ADI, SOYADI=@SOYADI, CEP_TEL=@CEP_TEL, CEP_TEL2=@CEP_TEL2, IS_TEL=@IS_TEL, ADRES=@ADRES  WHERE MUSTERI_ID = @MUSTERI_ID", baglan);

                kos.Parameters.AddWithValue("@ADI", GetDataValue(musteri.ADI));
                kos.Parameters.AddWithValue("@SOYADI", GetDataValue(musteri.SOYADI));
                kos.Parameters.AddWithValue("@CEP_TEL", GetDataValue(musteri.CEP_TEL));
                kos.Parameters.AddWithValue("@CEP_TEL2", GetDataValue(musteri.CEP_TEL2));
                kos.Parameters.AddWithValue("@IS_TEL", GetDataValue(musteri.IS_TEL));
                kos.Parameters.AddWithValue("@ADRES", GetDataValue(musteri.ADRES));
                kos.Parameters.AddWithValue("@MUSTERI_ID", GetDataValue(musteri.MUSTERI_ID));

                baglan.Open();

                kos.ExecuteNonQuery();

                baglan.Close();

                return ProcessInfo.Basarili;

            }
            catch (Exception ex)
            {
                baglan.Close();
                Loglama.Write(ex.ToString());

                return ProcessInfo.Hata_Var;
            }
        }
        public MUSTERI MusteriGetirById(int MUSTERI_ID)
        {
            musteri = new MUSTERI();
            try
            {
                kos = new OleDbCommand("SELECT * FROM MUSTERI WHERE MUSTERI_ID = @MUSTERI_ID", baglan);

                kos.Parameters.AddWithValue("@MUSTERI_ID", MUSTERI_ID);
                baglan.Open();


                using (OleDbDataReader reader = kos.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        musteri = MapToClass<MUSTERI>(reader);
                    }
                }

                baglan.Close();

                return musteri;
            }
            catch (Exception ex)
            {
                baglan.Close();
                Loglama.Write(ex.ToString());

                return musteri;
            }
        }
        public int MusteriKaydet(MUSTERI musteri)
        {
            try
            {
                int donen = 0;
                string query = "Select @@Identity";
                kos = new OleDbCommand("INSERT INTO MUSTERI  (ADI, SOYADI,CEP_TEL , CEP_TEL2, IS_TEL, ADRES)  VALUES (@ADI, @SOYADI,@CEP_TEL , @CEP_TEL2, @IS_TEL, @ADRES)", baglan);

                kos.Parameters.AddWithValue("@ADI", GetDataValue(musteri.ADI));
                kos.Parameters.AddWithValue("@SOYADI", GetDataValue(musteri.SOYADI));
                kos.Parameters.AddWithValue("@CEP_TEL", GetDataValue(musteri.CEP_TEL));
                kos.Parameters.AddWithValue("@CEP_TEL2", GetDataValue(musteri.CEP_TEL2));
                kos.Parameters.AddWithValue("@IS_TEL", GetDataValue(musteri.IS_TEL));
                kos.Parameters.AddWithValue("@ADRES", GetDataValue(musteri.ADRES));

                baglan.Open();
                kos.ExecuteNonQuery();
                kos.CommandText = query;
                donen = (int)kos.ExecuteScalar();
                baglan.Close();

                if (donen != 0)
                {
                    return donen;
                }
                else
                {
                    return (int)ProcessInfo.Hata_Var;
                }
            }
            catch (Exception ex)
            {
                baglan.Close();
                Loglama.Write(ex.ToString());

                return (int)ProcessInfo.Hata_Var;
            }
        }
    }
}