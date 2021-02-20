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
                        var obj = new Projekt(i.Pkey, i.Name, i.Beschreibung, i.FreigabeDatum, i.StartDatumG, i.EndDatumG, i.StartDatum, i.EndDatum, i.FKey_ProjektleiterID, i.KostenG, i.Kosten, i.FKey_VorgehensmodellID, i.Dokumente, i.Status, i.Fortschritt);
                        returnList.Add(obj);
                    }
                    break;
                case "Vorgehensmodell":
                    returnValue = connection.ExecuteQuery<Vorgehensmodell.db_Vorgehensmodell>(t_Query).ToList();
                    foreach (var i in returnValue)
                    {
                        var obj = new Vorgehensmodell(i.Pkey, i.Name, i.Beschreibung);
                        returnList.Add(obj);
                    }
                    break;
                case "Mitarbeiter":
                    returnValue = connection.ExecuteQuery<Mitarbeiter.db_Mitarbeiter>(t_Query).ToList();
                    foreach (var i in returnValue)
                    {
                        var obj = new Mitarbeiter(i.Pkey, i.Vorname, i.Nachname, i.Funktion);
                        returnList.Add(obj);
                    }
                    break;
                case "PhaseTemplate":
                    returnValue = connection.ExecuteQuery<PhaseTemplate.db_PhaseTemplate>(t_Query).ToList();
                    foreach (var i in returnValue)
                    {
                        var obj = new PhaseTemplate(i.Pkey, i.Name, i.FKey_VorgehensmodellID);
                        returnList.Add(obj);
                    }
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
                case "Projekt":
                    var dbObjProjekt = new Projekt.db_Projekt
                    {
                        Name = obj.Name,
                        Beschreibung = obj.Beschreibung,
                        FreigabeDatum = obj.FreigabeDatum,
                        StartDatumG = obj.StartDatumG,
                        EndDatumG = obj.EndDatumG,
                        StartDatum = obj.StartDatum,
                        EndDatum = obj.EndDatum,
                        FKey_ProjektleiterID = obj.FKey_ProjektleiterID,
                        KostenG = obj.KostenG,
                        Kosten = obj.Kosten,
                        FKey_VorgehensmodellID = obj.FKey_VorgehensmodellID,
                        Dokumente = obj.Dokumente,
                        Status = obj.Status,
                    };
                    Table<Projekt.db_Projekt> tableProjekt = connection.GetTable<Projekt.db_Projekt>();
                    tableProjekt.InsertOnSubmit(dbObjProjekt);
                    connection.SubmitChanges();
                    pkey = dbObjProjekt.Pkey;
                    break;
                default:
                    
                    break;
            }
            return pkey;
        }

        public void Update(string t_Table, dynamic obj)
        {
            if (null == connection)
            {
                connection = Create_DBConnection();
            }

            switch (t_Table)
            {
                case "Vorgehensmodell":
                    //aktuelles objekt aus der DB
                    Table<Vorgehensmodell.db_Vorgehensmodell> tableVorgehensmodell = connection.GetTable<Vorgehensmodell.db_Vorgehensmodell>();
                    var vorgehensmodellObj = (Vorgehensmodell)obj;
                    var entryVorgehensmodell = (from i in tableVorgehensmodell
                                 where i.Pkey == vorgehensmodellObj.Pkey
                                 select i).First();
                    //Daten Updaten
                    entryVorgehensmodell.Name = vorgehensmodellObj.Name;
                    entryVorgehensmodell.Beschreibung = vorgehensmodellObj.Beschreibung;
                    connection.SubmitChanges();
                    break;
                case "PhaseTemplate":
                    
                    break;
                case "Projekt":
                    Table<Projekt.db_Projekt> tableProjekt = connection.GetTable<Projekt.db_Projekt>();
                    var projektObj = (Projekt)obj;
                    var entry = (from i in tableProjekt
                                 where i.Pkey == projektObj.Pkey
                                 select i).First();
                    entry.Name = obj.Name;
                    entry.Beschreibung = obj.Beschreibung;
                    entry.FreigabeDatum = obj.FreigabeDatum;
                    entry.StartDatumG = obj.StartDatumG;
                    entry.EndDatumG = obj.EndDatumG;
                    entry.StartDatum = obj.StartDatum;
                    entry.EndDatum = obj.EndDatum;
                    entry.FKey_ProjektleiterID = obj.FKey_ProjektleiterID;
                    entry.KostenG = obj.KostenG;
                    entry.Kosten = obj.Kosten;
                    entry.FKey_VorgehensmodellID = obj.FKey_VorgehensmodellID;
                    entry.Dokumente = obj.Dokumente;
                    entry.Status = obj.Status;

                    connection.SubmitChanges();
                    break;
                default:

                    break;
            }
        }

        public void Remove(string t_Table, dynamic obj)
        {
            if (null == connection)
            {
                connection = Create_DBConnection();
            }

            switch (t_Table)
            {
                case "PhaseTemplate":
                    //aktuelles objekt aus der DB
                    Table<PhaseTemplate.db_PhaseTemplate> tablePhaseTemplate = connection.GetTable<PhaseTemplate.db_PhaseTemplate>();
                    var PhaseTemplateObj = (PhaseTemplate)obj;
                    var entryPhasenTamplate = (from i in tablePhaseTemplate
                                                where i.Pkey == PhaseTemplateObj.Pkey
                                                select i).First();
                    //Remove
                    tablePhaseTemplate.DeleteOnSubmit(entryPhasenTamplate);
                    connection.SubmitChanges();
                    break;
                default:

                    break;
            }
        }
    }
}
