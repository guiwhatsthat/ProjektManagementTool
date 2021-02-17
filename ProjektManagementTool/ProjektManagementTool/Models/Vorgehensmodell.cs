using ProjektManagementTool.Helper;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;
using System.Windows;

namespace ProjektManagementTool.Models
{
    class Vorgehensmodell
    {
        //Eigentschaften vom Projekt
        public int Pkey { get; set; }
        public string Name { get; set; }
        public string Beschreibung { get; set; }

        public Vorgehensmodell(int t_Pkey, string t_Name, string t_Beschreibung)
        {
            Pkey = t_Pkey;
            Name = t_Name;
            Beschreibung = t_Beschreibung;
        }

        //SQL mapping
        [Table(Name = "Vorgehensmodell")]
        public class db_Vorgehensmodell
        {
            //Mapper auf Primary Key
            [Column(Name = "Pkey", IsDbGenerated = true, IsPrimaryKey = true)]
            public int Pkey { get; set; }
            //Mapper auf Feld Name der Gruppe
            [Column]
            public string Name;
            [Column]
            public string Beschreibung;
        }

        public int CreateInDB()
        {
            //Überprüfen ob es ein Objekt mit diesem Namen schon gibt
            var dbHelper = new DBHelper();
            var returnValue = dbHelper.RunQuery("Vorgehensmodell", "Select * from Vorgehensmodell");
            if (returnValue.Count > 0)
            {
                MessageBox.Show("Objekt mit diesem Namen existiert bereits", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return -1;
            }
            int pkey = dbHelper.Write("Vorgehensmodell", this);
            return pkey;
        }
    }
}
