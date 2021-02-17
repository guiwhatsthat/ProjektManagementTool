using ProjektManagementTool.Helper;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;

namespace ProjektManagementTool.Models
{
    class PhaseTemplate
    {
        //Eigentschaften vom Projekt
        public int Pkey { get; set; }
        public string Name { get; set; }
        public int FKey_VorgehensmodellID { get; set; }

        public PhaseTemplate(int t_Pkey, string t_Name, int t_FKey_VorgehensmodellID)
        {
            Pkey = t_Pkey;
            Name = t_Name;
            FKey_VorgehensmodellID = t_FKey_VorgehensmodellID;
        }

        //SQL mapping
        [Table(Name = "PhaseTemplate")]
        public class db_PhaseTemplate
        {
            //Mapper auf Primary Key
            [Column(Name = "Pkey", IsDbGenerated = true, IsPrimaryKey = true)]
            public int Pkey { get; set; }
            //Mapper auf Feld Name der Gruppe
            [Column]
            public string Name;
            [Column]
            public int FKey_VorgehensmodellID;
        }

        public int CreateInDB()
        {
            var dbHelper = new DBHelper();
            int pkey = dbHelper.Write("PhaseTemplate", this);
            return pkey;
        }
    }
}
