using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Lastik_Otel
{

    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    [Serializable]
    public class MappingAttribute : Attribute
    {
        public string ColumnName = null;
    }

    public class MUSTERI
    {
        [Mapping(ColumnName = "MUSTERI_ID")]
        public int MUSTERI_ID { get; set; }

        [Mapping(ColumnName = "ADI")]
        public string ADI { get; set; }

        [Mapping(ColumnName = "SOYADI")]
        public string SOYADI { get; set; }

        [Mapping(ColumnName = "CEP_TEL")]
        public string CEP_TEL { get; set; }

        [Mapping(ColumnName = "CEP_TEL2")]
        public string CEP_TEL2 { get; set; }

        [Mapping(ColumnName = "IS_TEL")]
        public string IS_TEL { get; set; }

        [Mapping(ColumnName = "ADRES")]
        public string ADRES { get; set; }

        [Mapping(ColumnName = "KAYIT_TARIHI")]
        public DateTime KAYIT_TARIHI { get; set; }

        [Mapping(ColumnName = "EKLE_LOG_TARIH")]
        public DateTime EKLE_LOG_TARIH { get; set; }
    }
    public class MUSTERI_HAREKET : ARAC_BILGILERI
    {
        [Mapping(ColumnName = "MUSTERI_HAREKET_ID")]
        public int MUSTERI_HAREKET_ID { get; set; }

        [Mapping(ColumnName = "MUSTERI_ID")]
        public int MUSTERI_ID { get; set; }

        [Mapping(ColumnName = "VERILEN_TARIH")]
        public DateTime VERILEN_TARIH { get; set; }

        [Mapping(ColumnName = "ALINAN_TARIH")]
        public DateTime ALINAN_TARIH { get; set; }

        [Mapping(ColumnName = "CIKTI_ALINDI")]
        public int CIKTI_ALINDI { get; set; }

        [Mapping(ColumnName = "ISLEM_BITTI")]
        public int ISLEM_BITTI { get; set; }

        [Mapping(ColumnName = "EKLE_LOG_TARIH")]
        public DateTime EKLE_LOG_TARIH { get; set; }
    }
    public class ARAC_BILGILERI
    {
        [Mapping(ColumnName = "ARAC_TIPI")]
        public string ARAC_TIPI { get; set; }

        [Mapping(ColumnName = "PLAKA_NO")]
        public string PLAKA_NO { get; set; }

        [Mapping(ColumnName = "LASTIK_ADEDI")]
        public int LASTIK_ADEDI { get; set; }

        [Mapping(ColumnName = "LASTIK_MARKASI")]
        public string LASTIK_MARKASI { get; set; }

        [Mapping(ColumnName = "LASTIK_MODELI")]
        public string LASTIK_MODELI { get; set; }

        [Mapping(ColumnName = "LASTIK_EBADI")]
        public string LASTIK_EBADI { get; set; }

        [Mapping(ColumnName = "LASTIK_TIPI")]
        public string LASTIK_TIPI { get; set; }

        [Mapping(ColumnName = "ACIKLAMA")]
        public string ACIKLAMA { get; set; }
    }

    public class MUSTERI_HAREKET_LISTE : MUSTERI
    {

        [Mapping(ColumnName = "ARAC_TIPI")]
        public string ARAC_TIPI { get; set; }

        [Mapping(ColumnName = "PLAKA_NO")]
        public string PLAKA_NO { get; set; }

        [Mapping(ColumnName = "LASTIK_ADEDI")]
        public int LASTIK_ADEDI { get; set; }

        [Mapping(ColumnName = "LASTIK_MARKASI")]
        public string LASTIK_MARKASI { get; set; }

        [Mapping(ColumnName = "LASTIK_TIPI")]
        public string LASTIK_TIPI { get; set; }
    }

}