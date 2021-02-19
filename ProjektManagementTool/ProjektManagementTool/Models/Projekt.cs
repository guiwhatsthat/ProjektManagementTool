using ProjektManagementTool.Helper;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;

namespace ProjektManagementTool.Models
{
    public class Projekt
    {
        //Eigentschaften vom Projekt
        public int Pkey { get; set; }
        public string Name { get; set; }
        public string Beschreibung { get; set; }
        public Nullable<DateTime> FreigabeDatum { get; set; }
        public DateTime StartDatumG { get; set; }
        public DateTime EndDatumG { get; set; }
        public Nullable<DateTime> StartDatum { get; set; }
        public Nullable<DateTime> EndDatum { get; set; }
        public int FKey_ProjektleiterID { get; set; }
        public decimal KostenG { get; set; }
        public Nullable<decimal> Kosten { get; set; }
        public int FKey_VorgehensmodellID { get; set; }
        public string Dokumente { get; set; }
        public string Status { get; set; }

        public Projekt()
        {
            //leeres object für die tabellen abfrage
        }

        public Projekt(int t_Pkey, string t_Name, string t_Beschreibung, Nullable<DateTime> t_FreigabeDatum, DateTime t_StartDatumG, DateTime t_EndDatumG, Nullable<DateTime> t_StartDatum, Nullable<DateTime> t_EndDatum, int t_FKey_ProjektleiterID, decimal t_KostenG, Nullable<decimal> t_Kosten, int t_FKey_VorgehensmodellID, string t_Dokumente, string t_Status)
        {
            Pkey = t_Pkey;
            Name = t_Name;
            Beschreibung = t_Beschreibung;
            FreigabeDatum = t_FreigabeDatum;
            StartDatumG = t_StartDatumG;
            EndDatumG = t_EndDatumG;
            StartDatum = t_StartDatum;
            EndDatum = t_EndDatum;
            FKey_ProjektleiterID = t_FKey_ProjektleiterID;
            KostenG = t_KostenG;
            Kosten = t_Kosten;
            FKey_VorgehensmodellID = t_FKey_VorgehensmodellID;
            Dokumente = t_Dokumente;
            Status = t_Status;
        }


        //SQL mapping
        [Table(Name = "Projekt")]
        public class db_Projekt
        {
            //Mapper auf Primary Key
            [Column(Name = "Pkey", IsDbGenerated = true, IsPrimaryKey = true)]
            public int Pkey { get; set; }
            //Mapper auf Feld Name der Gruppe
            [Column]
            public string Name;
            [Column]
            public string Beschreibung;
            [Column(CanBeNull = true)]
            public Nullable<DateTime> FreigabeDatum;
            [Column]
            public DateTime StartDatumG;
            [Column]
            public DateTime EndDatumG;
            [Column(CanBeNull = true)]
            public Nullable<DateTime> StartDatum;
            [Column(CanBeNull = true)]
            public Nullable<DateTime> EndDatum;
            [Column]
            public int FKey_ProjektleiterID;
            [Column]
            public decimal KostenG;
            [Column(CanBeNull = true)]
            public Nullable<decimal> Kosten;
            [Column]
            public int FKey_VorgehensmodellID;
            [Column(CanBeNull = true)]
            public string Dokumente;
            [Column]
            public string Status;
        }

        public int CreateInDB()
        {
            var dbHelper = new DBHelper();
            int pkey = dbHelper.Write("Projekt", this);
            return pkey;
        }
    }
}
