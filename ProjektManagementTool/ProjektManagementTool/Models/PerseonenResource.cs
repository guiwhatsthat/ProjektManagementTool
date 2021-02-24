using ProjektManagementTool.Helper;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;

namespace ProjektManagementTool.Models
{
    class PerseonenResource
    {
        //Eigentschaften
        public int Pkey { get; set; }
        public string Name { get; set; }
        public decimal KostenG { get; set; }
        public string Funktion { get; set; }
        public int Fkey_Aktivitaet { get; set; }

        public PerseonenResource(int t_Pkey, string t_Name, decimal t_KostenG, string t_Art, int t_Fkey_Aktivitaet)
        {
            Pkey = t_Pkey;
            Name = t_Name;
            KostenG = t_KostenG;
            Funktion = t_Art;
            Fkey_Aktivitaet = t_Fkey_Aktivitaet;
        }

        //SQL mapping
        [Table(Name = "PerseonenResource")]
        public class db_PerseonenResource
        {
            //Mapper auf Primary Key
            [Column(Name = "Pkey", IsDbGenerated = true, IsPrimaryKey = true)]
            public int Pkey { get; set; }
            //Mapper
            [Column]
            public string Name;
            [Column]
            public decimal KostenG;
            [Column]
            public string Funktion;
        }

        public int CreateInDB()
        {
            var dbHelper = new DBHelper();
            int pkey = dbHelper.Write("PerseonenResource", this);
            return pkey;
        }
        public void Update()
        {
            var dbHelper = new DBHelper();
            dbHelper.Update("PerseonenResource", this);
        }
        public void Remove()
        {
            var dbHelper = new DBHelper();
            dbHelper.Remove("PerseonenResource", this);
        }
    }
}
