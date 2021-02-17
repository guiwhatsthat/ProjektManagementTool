﻿using ProjektManagementTool.Helper;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;

namespace ProjektManagementTool.Models
{
    class Mitarbeiter
    {
        //Eigentschaften vom PhaseTemplate
        public int Pkey { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Funktion { get; set; }

        public Mitarbeiter(int t_Pkey, string t_Vorname, string t_Nachname, string t_Funktion)
        {
            Pkey = t_Pkey;
            Vorname = t_Vorname;
            Nachname = t_Nachname;
            Funktion = t_Funktion;
        }

        //SQL mapping
        [Table(Name = "Mitarbeiter")]
        public class db_Mitarbeiter
        {
            //Mapper auf Primary Key
            [Column(Name = "Pkey", IsDbGenerated = true, IsPrimaryKey = true)]
            public int Pkey { get; set; }
            //Mapper auf Feld Name der Gruppe
            [Column]
            public string Vorname;
            [Column]
            public string Nachname;
            [Column]
            public string Funktion;
        }

        public int CreateInDB()
        {
            var dbHelper = new DBHelper();
            int pkey = dbHelper.Write("Mitarbeiter", this);
            return pkey;
        }
    }
}
