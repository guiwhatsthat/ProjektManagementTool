using ProjektManagementTool.Helper;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;

namespace ProjektManagementTool.Models
{
    class ExterneResource
    {
        //Eigentschaften
        public int Pkey { get; set; }
        public string Name { get; set; }
        public decimal KostenG { get; set; }
        public string Art { get; set; }
        public int Fkey_Aktivitaet{ get; set; }

        public ExterneResource(int t_Pkey, string t_Name, decimal t_KostenG, string t_Art, int t_Fkey_Aktivitaet)
        {
            Pkey = t_Pkey;
            Name = t_Name;
            KostenG = t_KostenG;
            Art = t_Art;
            Fkey_Aktivitaet = t_Fkey_Aktivitaet;
        }

        //SQL mapping
        [Table(Name = "ExterneResource")]
        public class db_ExterneResource
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
            public string Art;
        }

        public int CreateInDB()
        {
            var dbHelper = new DBHelper();
            int pkey = dbHelper.Write("ExterneResource", this);
            return pkey;
        }
        public void Update()
        {
            var dbHelper = new DBHelper();
            dbHelper.Update("ExterneResource", this);
        }
        public void Remove()
        {
            var dbHelper = new DBHelper();
            dbHelper.Remove("ExterneResource", this);
        }
    }
}
