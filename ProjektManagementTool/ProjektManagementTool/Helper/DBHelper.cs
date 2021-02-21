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
                case "Phase":
                    returnValue = connection.ExecuteQuery<Phase.db_Phase>(t_Query).ToList();
                    foreach (var i in returnValue)
                    {
                        var obj = new Phase(i.Pkey, i.Name, i.Status, i.Fortschritt, i.StartDatumG, i.EndDatumG, i.StartDatum, i.EndDatum, i.FKey_PhaseTemplateID, i.FKey_ProjektID);
                        returnList.Add(obj);
                    }
                    break;
                case "Meilenstein":
                    returnValue = connection.ExecuteQuery<Meilenstein.db_Meilenstein>(t_Query).ToList();
                    foreach (var i in returnValue)
                    {
                        var obj = new Meilenstein(i.Pkey, i.Name, i.DatumG, i.Datum, i.FKey_PhaseID);
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
                case "Phase":
                    var dbObjPhase = new Phase.db_Phase
                    {
                        Name = obj.Name,
                        Status = obj.Status,
                        Fortschritt = obj.Fortschritt,
                        StartDatumG = obj.StartDatumG,
                        EndDatumG = obj.EndDatumG,
                        StartDatum = obj.StartDatum,
                        EndDatum = obj.EndDatum,
                        FKey_PhaseTemplateID = obj.FKey_PhaseTemplateID,
                        FKey_ProjektID = obj.FKey_ProjektID,
                    };
                    Table<Phase.db_Phase> tablePhase = connection.GetTable<Phase.db_Phase>();
                    tablePhase.InsertOnSubmit(dbObjPhase);
                    connection.SubmitChanges();
                    pkey = dbObjPhase.Pkey;
                    break;
                case "Meilenstein":
                    var dbObjMeilenstein = new Meilenstein.db_Meilenstein
                    {
                        Name = obj.Name,
                        DatumG = obj.DatumG,
                        Datum = obj.Datum,
                        FKey_PhaseID = obj.FKey_PhaseID
                    };
                    Table<Meilenstein.db_Meilenstein> tableMeilenstein = connection.GetTable<Meilenstein.db_Meilenstein>();
                    tableMeilenstein.InsertOnSubmit(dbObjMeilenstein);
                    connection.SubmitChanges();
                    pkey = dbObjMeilenstein.Pkey;
                    break;
                case "Aktivitaet":
                    var dbObjAktivitaet = new Aktivitaet.db_Aktivitaet
                    {
                        Pkey = obj.Pkey,
                        Name = obj.Name,
                        StartDatumG = obj.StartDatumG,
                        EndDatumG = obj.EndDatumG,
                        StartDatum = obj.StartDatum,
                        EndDatum = obj.EndDatum,
                        BudgetExterneKostenG = obj.BudgetExterneKostenG,
                        BudgetPersonenKostenG = obj.BudgetPersonenKostenG,
                        BudgetExterneKosten = obj.BudgetExterneKosten,
                        BudgetPersonenKosten = obj.BudgetPersonenKosten,
                        Fortschritt = obj.Fortschritt,
                        FKey_VerantwortlichePersonID = obj.FKey_VerantwortlichePersonID,
                        FKey_PhaseID = obj.FKey_PhaseID
                    };
                    Table<Aktivitaet.db_Aktivitaet> tableAktivitaet = connection.GetTable<Aktivitaet.db_Aktivitaet>();
                    tableAktivitaet.InsertOnSubmit(dbObjAktivitaet);
                    connection.SubmitChanges();
                    pkey = dbObjAktivitaet.Pkey;
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
                case "Phase":
                    Table<Phase.db_Phase> tablePhase = connection.GetTable<Phase.db_Phase>();
                    var phaseObj = (Phase)obj;
                    var entryPhase = (from i in tablePhase
                                 where i.Pkey == phaseObj.Pkey
                                 select i).First();
                    entryPhase.Name = obj.Name;
                    entryPhase.Status = obj.Status;
                    entryPhase.Fortschritt = obj.Fortschritt;
                    entryPhase.StartDatumG = obj.StartDatumG;
                    entryPhase.EndDatumG = obj.EndDatumG;
                    entryPhase.StartDatum = obj.StartDatum;
                    entryPhase.EndDatum = obj.EndDatum;
                    entryPhase.FKey_PhaseTemplateID = obj.FKey_PhaseTemplateID;
                    entryPhase.FKey_ProjektID = obj.FKey_ProjektID;

                    connection.SubmitChanges();
                    break;
                case "Meilenstein":
                    Table<Meilenstein.db_Meilenstein> tableMeilenstein = connection.GetTable<Meilenstein.db_Meilenstein>();
                    var meilensteinObj = (Meilenstein)obj;
                    var entryMeilenstein = (from i in tableMeilenstein
                                      where i.Pkey == meilensteinObj.Pkey
                                      select i).First();
                    entryMeilenstein.Name = obj.Name;
                    entryMeilenstein.DatumG = obj.DatumG;
                    entryMeilenstein.Datum = obj.Datum;
                    entryMeilenstein.FKey_PhaseID = obj.FKey_PhaseID;
                    connection.SubmitChanges();
                    break;
                case "Aktivitaet":
                    Table<Aktivitaet.db_Aktivitaet> tableAktivitaet = connection.GetTable<Aktivitaet.db_Aktivitaet>();
                    var AktivitaetObj = (Aktivitaet)obj;
                    var entryAktivitaet = (from i in tableAktivitaet
                                           where i.Pkey == AktivitaetObj.Pkey
                                            select i).First();
                    entryAktivitaet.Name = obj.Name;
                    entryAktivitaet.StartDatumG = obj.StartDatumG;
                    entryAktivitaet.EndDatumG = obj.EndDatumG;
                    entryAktivitaet.StartDatum = obj.StartDatum;
                    entryAktivitaet.EndDatum = obj.EndDatum;
                    entryAktivitaet.BudgetExterneKostenG = obj.BudgetExterneKostenG;
                    entryAktivitaet.BudgetPersonenKostenG = obj.BudgetPersonenKostenG;
                    entryAktivitaet.BudgetExterneKosten = obj.BudgetExterneKosten;
                    entryAktivitaet.BudgetPersonenKosten = obj.BudgetPersonenKosten;
                    entryAktivitaet.Fortschritt = obj.Fortschritt;
                    entryAktivitaet.FKey_VerantwortlichePersonID = obj.FKey_VerantwortlichePersonID;
                    entryAktivitaet.FKey_PhaseID = obj.FKey_PhaseID;
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
                                                where i.Name == PhaseTemplateObj.Name
                                                select i).First();
                    //Remove
                    tablePhaseTemplate.DeleteOnSubmit(entryPhasenTamplate);
                    connection.SubmitChanges();
                    break;
                case "Meilenstein":
                    //aktuelles objekt aus der DB
                    Table<Meilenstein.db_Meilenstein> tableMeilenstein = connection.GetTable<Meilenstein.db_Meilenstein>();
                    var meilensteinObj = (Meilenstein)obj;
                    var entryMeilenstein = (from i in tableMeilenstein
                                               where i.Name == meilensteinObj.Name
                                               select i).First();
                    //Remove
                    tableMeilenstein.DeleteOnSubmit(entryMeilenstein);
                    connection.SubmitChanges();
                    break;
                case "Aktivitaet":
                    //aktuelles objekt aus der DB
                    Table<Aktivitaet.db_Aktivitaet> tableAktivitaet = connection.GetTable<Aktivitaet.db_Aktivitaet>();
                    var aktivitaetObj = (Aktivitaet)obj;
                    var entryAktivitaet = (from i in tableAktivitaet
                                           where i.Name == aktivitaetObj.Name
                                            select i).First();
                    //Remove
                    tableAktivitaet.DeleteOnSubmit(entryAktivitaet);
                    connection.SubmitChanges();
                    break;
                default:

                    break;
            }
        }
    }
}
