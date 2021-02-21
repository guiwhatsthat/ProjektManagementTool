using ProjektManagementTool.Helper;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;

namespace ProjektManagementTool.Models
{
    class Meilenstein
    {
        //Eigentschaften
        public int Pkey { get; set; }
        public string Name { get; set; }
        public DateTime DatumG { get; set; }
        public Nullable<DateTime> Datum { get; set; }
        public int FKey_PhaseID { get; set; }

        public Meilenstein(int t_Pkey, string t_Name, DateTime t_DatumG, Nullable<DateTime> t_Datum, int t_FKey_PhaseID)
        {
            Pkey = t_Pkey;
            Name = t_Name;
            DatumG = t_DatumG;
            Datum = t_Datum;
            FKey_PhaseID = t_FKey_PhaseID;
        }

        //SQL mapping
        [Table(Name = "Meilenstein")]
        public class db_Meilenstein
        {
            //Mapper auf Primary Key
            [Column(Name = "Pkey", IsDbGenerated = true, IsPrimaryKey = true)]
            public int Pkey { get; set; }
            //Mapper
            [Column]
            public string Name;
            [Column]
            public DateTime DatumG;
            [Column]
            public Nullable<DateTime> Datum;
            [Column]
            public int FKey_PhaseID;
        }

        public int CreateInDB()
        {
            var dbHelper = new DBHelper();
            int pkey = dbHelper.Write("Meilenstein", this);
            return pkey;
        }
        public void Update()
        {
            var dbHelper = new DBHelper();
            dbHelper.Update("Meilenstein", this);
        }
        public void Remove()
        {
            var dbHelper = new DBHelper();
            dbHelper.Remove("Meilenstein", this);
        }

    }
}
