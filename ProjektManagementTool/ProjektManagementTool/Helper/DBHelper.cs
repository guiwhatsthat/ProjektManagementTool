using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using ProjektManagementTool.Models;

namespace ProjektManagementTool.Helper

{
    class DBHelper
    {
        static DataContext connection;
        //Fixer Wert sollte über die sttings gesetzt werden können, eventuell später implementieren (da kein Ziel)
        string GetConnectionString()
        {
            string connectionString = "Server=DESKTOP-35P8P5I\\SQLEXPRESS;Database=DB_Projekte;Connection timeout=30;Integrated Security=True";
            return connectionString;
        }

        DataContext Create_DBConnection()
        {
            DataContext dbConnection = new DataContext(GetConnectionString());
            return dbConnection;
        }

        public dynamic RunQuery(string t_Table, string t_Query)
        {
            if (null == connection)
            {
                connection = Create_DBConnection();
            }
            dynamic returnValue;
            List<dynamic> returnList = new List<dynamic>();
            //Switch case für die unterschiedlichen tabellen
            switch (t_Table)
            {
                case "Projekt":
                    returnValue = connection.ExecuteQuery<Projekt.db_Projekt>(t_Query).ToList();
                    foreach (var i in returnValue) {
                        var obj = new Projekt(i.Pkey, i.Name, i.Beschreibung, i.FreigabeDatum, i.StartDatumG, i.EndDatumG, i.StartDatum, i.EndDatum, i.FKey_ProjektleiterID, i.KostenG, i.Kosten, i.FKey_VorgehensmodellID, i.Dokumente, i.Status);
                        returnList.Add(obj);
                    }
                    break;
                case "Vorgehensmodell":
                    returnValue = connection.ExecuteQuery<Vorgehensmodell.db_Vorgehensmodell>(t_Query).ToList();
                    break;
                default:
                    returnValue = connection.ExecuteQuery<Projekt.db_Projekt>(t_Query).ToList();
                    break;
            }
            return returnList;
        }

        public int Write(string t_Table, dynamic obj)
        {
            if (null == connection)
            {
                connection = Create_DBConnection();
            }
            int pkey = -1;
            switch (t_Table)
            {
                case "Vorgehensmodell":
                    var dbObjVorgehensmodell = new Vorgehensmodell.db_Vorgehensmodell
                    {
                        Name = obj.Name,
                        Beschreibung = obj.Beschreibung
                    };
                    Table<Vorgehensmodell.db_Vorgehensmodell> tableVorgehensmodell = connection.GetTable<Vorgehensmodell.db_Vorgehensmodell>();
                    tableVorgehensmodell.InsertOnSubmit(dbObjVorgehensmodell);
                    connection.SubmitChanges();
                    pkey = dbObjVorgehensmodell.Pkey;
                    break;
                case "PhaseTemplate":
                    var dbObjPhaseTemplate = new PhaseTemplate.db_PhaseTemplate
                    {
                        Name = obj.Name,
                        FKey_VorgehensmodellID = obj.FKey_VorgehensmodellID
                    };
                    Table<PhaseTemplate.db_PhaseTemplate> tablePhaseTemplate = connection.GetTable<PhaseTemplate.db_PhaseTemplate>();
                    tablePhaseTemplate.InsertOnSubmit(dbObjPhaseTemplate);
                    connection.SubmitChanges();
                    pkey = dbObjPhaseTemplate.Pkey;
                    break;
                default:
                    
                    break;
            }
            return pkey;
        }

    }
}
