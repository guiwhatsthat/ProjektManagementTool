using ProjektManagementTool.Helper;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Text;

namespace ProjektManagementTool.Models
{
    class Aktivitaet
    {
        //Eigentschaften
        public int Pkey { get; set; }
        public string Name { get; set; }
        public DateTime StartDatumG { get; set; }
        public DateTime EndDatumG { get; set; }
        public Nullable<DateTime> StartDatum { get; set; }
        public Nullable<DateTime> EndDatum { get; set; }
        public decimal BudgetExterneKostenG { get; set; }
        public decimal BudgetPersonenKostenG { get; set; }
        public decimal BudgetExterneKosten { get; set; }
        public decimal BudgetPersonenKosten { get; set; }
        public int Fortschritt { get; set; }
        public int FKey_VerantwortlichePersonID { get; set; }
        public int FKey_PhaseID { get; set; }


        public Aktivitaet(int t_Pkey, string t_Name, DateTime t_StartDatumG, DateTime t_EndDatumG, Nullable<DateTime> t_StartDatum, Nullable<DateTime> t_EndDatum, decimal t_BudgetExterneKostenG, decimal t_BudgetPersonenKostenG, decimal t_BudgetExterneKosten, decimal t_BudgetPersonenKosten, int t_Fortschritt, int t_FKey_VerantwortlichePersonID, int t_FKey_PhaseID)
        {
            Pkey = t_Pkey;
            Name = t_Name;
            StartDatumG = t_StartDatumG;
            EndDatumG = t_EndDatumG;
            StartDatum = t_StartDatum;
            EndDatum = t_EndDatum;
            BudgetExterneKostenG = t_BudgetExterneKostenG;
            BudgetPersonenKostenG = t_BudgetPersonenKostenG;
            BudgetExterneKosten = t_BudgetExterneKosten;
            BudgetPersonenKosten = t_BudgetPersonenKosten;
            Fortschritt = t_Fortschritt;
            FKey_VerantwortlichePersonID = t_FKey_VerantwortlichePersonID;
            FKey_PhaseID = t_FKey_PhaseID;
        }

        //SQL mapping
        [Table(Name = "Aktivitaet")]
        public class db_Aktivitaet
        {
            //Mapper auf Primary Key
            [Column(Name = "Pkey", IsDbGenerated = true, IsPrimaryKey = true)]
            public int Pkey { get; set; }
            //Mapper
            [Column]
            public string Name;
            [Column]
            public DateTime StartDatumG;
            [Column]
            public DateTime EndDatumG;
            [Column]
            public Nullable<DateTime> StartDatum;
            [Column]
            public Nullable<DateTime> EndDatum;
            [Column]
            public decimal BudgetExterneKostenG;
            [Column]
            public decimal BudgetPersonenKostenG;
            [Column]
            public decimal BudgetExterneKosten;
            [Column]
            public decimal BudgetPersonenKosten;
            [Column]
            public int Fortschritt;
            [Column]
            public int FKey_VerantwortlichePersonID;
            [Column]
            public int FKey_PhaseID;
        }

        public int CreateInDB()
        {
            var dbHelper = new DBHelper();
            int pkey = dbHelper.Write("Aktivitaet", this);
            return pkey;
        }
        public void Update()
        {
            var dbHelper = new DBHelper();
            dbHelper.Update("Aktivitaet", this);
        }
        public void Remove()
        {
            var dbHelper = new DBHelper();
            dbHelper.Remove("Aktivitaet", this);
        }
    }
}
