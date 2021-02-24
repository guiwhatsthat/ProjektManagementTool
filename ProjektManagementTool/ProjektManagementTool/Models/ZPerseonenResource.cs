using ProjektManagementTool.Helper;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;

namespace ProjektManagementTool.Models
{
    class ZPerseonenResource
    {
        //Eigentschaften
        public int Pkey { get; set; }
        public int FKey_Aktiviteat { get; set; }
        public int FKey_PerseonenResource { get; set; }
        public DateTime StartDatum { get; set; }
        public Nullable<DateTime> EndDatum { get; set; }

        public ZPerseonenResource(int t_Pkey, int t_FKey_Aktiviteat, int t_FKey_PerseonenResource, DateTime t_StartDatum, Nullable<DateTime> t_EndDatum)
        {
            Pkey = t_Pkey;
            FKey_Aktiviteat = t_FKey_Aktiviteat;
            FKey_PerseonenResource = t_FKey_PerseonenResource;
            StartDatum = t_StartDatum;
            EndDatum = t_EndDatum;
        }

        //SQL mapping
        [Table(Name = "Z_PerseonenResource")]
        public class db_ZPerseonenResource
        {
            //Mapper auf Primary Key
            [Column(Name = "Pkey", IsDbGenerated = true, IsPrimaryKey = true)]
            public int Pkey { get; set; }
            //Mapper
            [Column]
            public int FKey_Aktiviteat;
            [Column]
            public int FKey_PerseonenResource;
            [Column]
            public DateTime StartDatum;
            [Column]
            public Nullable<DateTime> EndDatum;
        }

        public int CreateInDB()
        {
            var dbHelper = new DBHelper();
            int pkey = dbHelper.Write("ZPerseonenResource", this);
            return pkey;
        }
        public void Update()
        {
            var dbHelper = new DBHelper();
            dbHelper.Update("ZPerseonenResource", this);
        }
        public void Remove()
        {
            var dbHelper = new DBHelper();
            dbHelper.Remove("ZPerseonenResource", this);
        }
    }
}
