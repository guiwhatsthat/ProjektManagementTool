using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;

namespace ProjektManagementTool.Models
{
    class VKostenAbweichungExterne
    {
        //Eigentschaften 
        public string Name { get; set; }
        public decimal KostenG { get; set; }
        public decimal Kosten { get; set; }
        public decimal Abweichung { get; set; }
        public string Art { get; set; }
        public string Kommentar { get; set; }
        public DateTime StartDatum { get; set; }
        public Nullable<DateTime> EndDatum { get; set; }
        public int Pkey { get; set; }
        public int Fkey_Aktivitaet { get; set; }

        public VKostenAbweichungExterne(string t_Name, decimal t_KostenG, decimal t_Kosten, decimal t_Abweichung, string t_Art, string t_Kommentar, DateTime t_StartDatum, Nullable<DateTime> t_EndDatum, int t_Pkey, int t_Fkey_Aktivitaet)
        {
            Name = t_Name;
            KostenG = t_KostenG;
            Kosten = t_Kosten;
            Abweichung = t_Abweichung;
            Art = t_Art;
            Kommentar = t_Kommentar;
            StartDatum = t_StartDatum;
            EndDatum = t_EndDatum;
            Pkey = t_Pkey;
            Fkey_Aktivitaet = t_Fkey_Aktivitaet;

    }

    //SQL mapping
    [Table(Name = "VKostenAbweichungExterne")]
        public class db_VKostenAbweichungExterne
        {
            //Mapper
            [Column]
            public string Name;
            [Column]
            public decimal KostenG;
            [Column]
            public decimal Kosten;
            [Column]
            public decimal Abweichung;
            [Column]
            public string Art;
            [Column]
            public string Kommentar;
            [Column]
            public DateTime StartDatum;
            [Column]
            public Nullable<DateTime> EndDatum;
            [Column]
            public int Pkey;
            [Column]
            public int Fkey_Aktivitaet;
        }
    }
}
