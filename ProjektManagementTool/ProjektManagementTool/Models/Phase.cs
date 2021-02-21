using ProjektManagementTool.Helper;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;

namespace ProjektManagementTool.Models
{
    class Phase
    {
        //Eigentschaften vom PhaseTemplate
        public int Pkey { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public int Fortschritt { get; set; }
        public DateTime StartDatumG { get; set; }
        public DateTime EndDatumG { get; set; }
        public Nullable<DateTime> StartDatum { get; set; }
        public Nullable<DateTime> EndDatum { get; set; }
        public int FKey_PhaseTemplateID { get; set; }
        public int FKey_ProjektID { get; set; }

        public Phase(int t_Pkey, string t_Name, string t_Status, int t_Fortschritt, DateTime t_StartDatumG, DateTime t_EndDatumG ,Nullable<DateTime> t_StartDatum, Nullable<DateTime> t_EndDatum, int t_FKey_PhaseTemplateID, int t_FKey_ProjektID)
        {
            Pkey = t_Pkey;
            Name = t_Name;
            Status = t_Status;
            Fortschritt = t_Fortschritt;
            StartDatumG = t_StartDatumG;
            EndDatumG = t_EndDatumG;
            StartDatum = t_StartDatum;
            EndDatum = t_EndDatum;
            FKey_PhaseTemplateID = t_FKey_PhaseTemplateID;
            FKey_ProjektID = t_FKey_ProjektID;
        }

        //SQL mapping
        [Table(Name = "Phase")]
        public class db_Phase
        {
            //Mapper auf Primary Key
            [Column(Name = "Pkey", IsDbGenerated = true, IsPrimaryKey = true)]
            public int Pkey { get; set; }
            //Mapper
            [Column]
            public string Name;
            [Column]
            public string Status;
            [Column]
            public int Fortschritt;
            [Column]
            public DateTime StartDatumG;
            [Column]
            public DateTime EndDatumG;
            [Column]
            public Nullable<DateTime> StartDatum;
            [Column]
            public Nullable<DateTime> EndDatum;
            [Column]
            public int FKey_PhaseTemplateID;
            [Column]
            public int FKey_ProjektID;
        }

        public int CreateInDB()
        {
            var dbHelper = new DBHelper();
            int pkey = dbHelper.Write("Phase", this);
            return pkey;
        }

        public void Update()
        {
            var dbHelper = new DBHelper();
            dbHelper.Update("Phase", this);
        }
    }
}
