﻿using ProjektManagementTool.Helper;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;

namespace ProjektManagementTool.Models
{
    class ZExterneResource
    {
        //Eigentschaften
        public int Pkey { get; set; }
        public int FKey_Aktiviteat { get; set; }
        public int FKey_ExterneResource { get; set; }
        public DateTime StartDatum { get; set; }
        public Nullable<DateTime> EndDatum { get; set; }
        public Nullable<decimal> Kosten { get; set; }
        public Nullable<decimal> Abweichung { get; set; }
        public string Kommentar { get; set; }

        public ZExterneResource(int t_Pkey, int t_FKey_Aktiviteat, int t_FKey_ExterneResource,DateTime t_StartDatum, Nullable<DateTime> t_EndDatum, Nullable<decimal> t_Kosten, Nullable<decimal> t_Abweichung, string t_Kommentar)
        {
            Pkey = t_Pkey;
            FKey_Aktiviteat = t_FKey_Aktiviteat;
            FKey_ExterneResource = t_FKey_ExterneResource;
            StartDatum = t_StartDatum;
            EndDatum = t_EndDatum;
            Kosten = t_Kosten;
            Abweichung = t_Abweichung;
            Kommentar = t_Kommentar;

        }

        //SQL mapping
        [Table(Name = "Z_ExterneResource")]
        public class db_ExterneResource
        {
            //Mapper auf Primary Key
            [Column(Name = "Pkey", IsDbGenerated = true, IsPrimaryKey = true)]
            public int Pkey { get; set; }
            //Mapper
            [Column]
            public int FKey_Aktiviteat;
            [Column]
            public int FKey_ExterneResource;
            [Column]
            public DateTime StartDatum;
            [Column]
            public Nullable<DateTime> EndDatum;
            [Column]
            public Nullable<decimal> Kosten;
            [Column]
            public Nullable<decimal> Abweichung;
            [Column]
            public string Kommentar;
        }

        public int CreateInDB()
        {
            var dbHelper = new DBHelper();
            int pkey = dbHelper.Write("ZExterneResource", this);
            return pkey;
        }
        public void Update()
        {
            var dbHelper = new DBHelper();
            dbHelper.Update("ZExterneResource", this);
        }
        public void Remove()
        {
            var dbHelper = new DBHelper();
            dbHelper.Remove("ZExterneResource", this);
        }
    }
}
