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
        string GetConnectionString()
        {
            string basepath = AppDomain.CurrentDomain.BaseDirectory;
            string mainpath = basepath.Split("bin")[0];
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + AppDomain.CurrentDomain.BaseDirectory + "ProjektDB.mdf" + ";Integrated Security = True;";//"Server=DESKTOP-35P8P5I\\SQLEXPRESS;Database=DB_Projekte;Connection timeout=30;Integrated Security=True";
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
                    foreach (var i in returnValue)
                    {
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
                case "Aktivitaet":
                    returnValue = connection.ExecuteQuery<Aktivitaet.db_Aktivitaet>(t_Query).ToList();
                    foreach (var i in returnValue)
                    {
                        var obj = new Aktivitaet(i.Pkey, i.Name, i.StartDatumG, i.EndDatumG, i.StartDatum, i.EndDatum, i.BudgetExterneKostenG, i.BudgetPersonenKostenG, i.BudgetExterneKosten, i.BudgetPersonenKosten, i.Fortschritt, i.FKey_VerantwortlichePersonID, i.FKey_PhaseID, i.Dokumente);
                        returnList.Add(obj);
                    }
                    break;
                case "ExterneResource":
                    returnValue = connection.ExecuteQuery<ExterneResource.db_ExterneResource>(t_Query).ToList();
                    foreach (var i in returnValue)
                    {
                        var obj = new ExterneResource(i.Pkey, i.Name, i.KostenG, i.Art, 0);
                        returnList.Add(obj);
                    }
                    break;
                case "ZExterneResource":
                    returnValue = connection.ExecuteQuery<ZExterneResource.db_ExterneResource>(t_Query).ToList();
                    foreach (var i in returnValue)
                    {
                        var obj = new ZExterneResource(i.Pkey, i.FKey_Aktiviteat, i.FKey_ExterneResource, i.StartDatum, i.EndDatum, i.Kosten, i.Abweichung, i.Kommentar);
                        returnList.Add(obj);
                    }
                    break;
                case "PerseonenResource":
                    returnValue = connection.ExecuteQuery<PerseonenResource.db_PerseonenResource>(t_Query).ToList();
                    foreach (var i in returnValue)
                    {
                        var obj = new PerseonenResource(i.Pkey, i.Name, i.KostenG, i.Funktion, 0);
                        returnList.Add(obj);
                    }
                    break;
                case "ZPerseonenResource":
                    returnValue = connection.ExecuteQuery<ZPerseonenResource.db_ZPerseonenResource>(t_Query).ToList();
                    foreach (var i in returnValue)
                    {
                        var obj = new ZPerseonenResource(i.Pkey, i.FKey_Aktiviteat, i.FKey_PerseonenResource, i.StartDatum, i.EndDatum, i.Kosten, i.Abweichung, i.Kommentar);
                        returnList.Add(obj);
                    }
                    break;
                case "VKostenAbweichungExterne":
                    returnValue = connection.ExecuteQuery<VKostenAbweichungExterne.db_VKostenAbweichungExterne>(t_Query).ToList();
                    foreach (var i in returnValue)
                    {
                        var obj = new VKostenAbweichungExterne(i.Name, i.KostenG, i.Kosten, i.Abweichung, i.Art, i.Kommentar, i.StartDatum, i.EndDatum, i.Pkey, i.Fkey_Aktivitaet);
                        returnList.Add(obj);
                    }
                    break;
                case "VKostenAbweichungPersonen":
                    returnValue = connection.ExecuteQuery<VKostenAbweichungPersonen.db_VKostenAbweichungPersonen>(t_Query).ToList();
                    foreach (var i in returnValue)
                    {
                        var obj = new VKostenAbweichungPersonen(i.Name, i.KostenG, i.Kosten, i.Abweichung, i.Funktion, i.Kommentar, i.StartDatum, i.EndDatum, i.Pkey, i.FKey_Aktiviteat);
                        returnList.Add(obj);
                    }
                    break;
                default:
                    break;
            }
            connection.Dispose();
            connection = null;
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
                        //Pkey = obj.Pkey,
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
                        FKey_PhaseID = obj.FKey_PhaseID,
                        Dokumente = obj.Dokumente
                    };
                    Table<Aktivitaet.db_Aktivitaet> tableAktivitaet = connection.GetTable<Aktivitaet.db_Aktivitaet>();
                    tableAktivitaet.InsertOnSubmit(dbObjAktivitaet);
                    connection.SubmitChanges();
                    pkey = dbObjAktivitaet.Pkey;
                    break;
                case "ExterneResource":
                    var dbObjExterneResource = new ExterneResource.db_ExterneResource
                    {
                        Name = obj.Name,
                        Art = obj.Art,
                        KostenG = obj.KostenG,
                        Pkey = obj.Pkey
                    };
                    Table<ExterneResource.db_ExterneResource> tableExterneResource = connection.GetTable<ExterneResource.db_ExterneResource>();
                    tableExterneResource.InsertOnSubmit(dbObjExterneResource);
                    connection.SubmitChanges();
                    pkey = dbObjExterneResource.Pkey;
                    break;
                case "ZExterneResource":
                    var dbObjZExterneResource = new ZExterneResource.db_ExterneResource
                    {
                        EndDatum = obj.EndDatum,
                        FKey_Aktiviteat = obj.FKey_Aktiviteat,
                        FKey_ExterneResource = obj.FKey_ExterneResource,
                        StartDatum = obj.StartDatum,
                        Pkey = obj.Pkey,
                        Abweichung = obj.Abweichung,
                        Kommentar = obj.Kommentar,
                        Kosten = obj.Kosten
                    };
                    Table<ZExterneResource.db_ExterneResource> tableZExterneResource = connection.GetTable<ZExterneResource.db_ExterneResource>();
                    tableZExterneResource.InsertOnSubmit(dbObjZExterneResource);
                    connection.SubmitChanges();
                    pkey = dbObjZExterneResource.Pkey;
                    break;
                case "PerseonenResource":
                    var dbObjPerseonenResource = new PerseonenResource.db_PerseonenResource
                    {
                        Name = obj.Name,
                        Funktion = obj.Funktion,
                        KostenG = obj.KostenG,
                        Pkey = obj.Pkey
                    };
                    Table<PerseonenResource.db_PerseonenResource> tablePerseonenResource = connection.GetTable<PerseonenResource.db_PerseonenResource>();
                    tablePerseonenResource.InsertOnSubmit(dbObjPerseonenResource);
                    connection.SubmitChanges();
                    pkey = dbObjPerseonenResource.Pkey;
                    break;
                case "ZPerseonenResource":
                    var dbObjZPerseonenResource = new ZPerseonenResource.db_ZPerseonenResource
                    {
                        EndDatum = obj.EndDatum,
                        FKey_Aktiviteat = obj.FKey_Aktiviteat,
                        FKey_PerseonenResource = obj.FKey_PerseonenResource,
                        StartDatum = obj.StartDatum,
                        Pkey = obj.Pkey,
                        Abweichung = obj.Abweichung,
                        Kommentar = obj.Kommentar,
                        Kosten = obj.Kosten
                    };
                    Table<ZPerseonenResource.db_ZPerseonenResource> tableZPerseonenResource = connection.GetTable<ZPerseonenResource.db_ZPerseonenResource>();
                    tableZPerseonenResource.InsertOnSubmit(dbObjZPerseonenResource);
                    connection.SubmitChanges();
                    pkey = dbObjZPerseonenResource.Pkey;
                    break;
                case "Mitarbeiter":
                    var dbObjMitarbeiter = new Mitarbeiter.db_Mitarbeiter
                    {
                        Vorname = obj.Vorname,
                        Nachname = obj.Nachname,
                        Funktion = obj.Funktion,
                    };
                    Table<Mitarbeiter.db_Mitarbeiter> tableZMitarbeiter = connection.GetTable<Mitarbeiter.db_Mitarbeiter>();
                    tableZMitarbeiter.InsertOnSubmit(dbObjMitarbeiter);
                    connection.SubmitChanges();
                    pkey = dbObjMitarbeiter.Pkey;
                    break;
                default:

                    break;
            }
            connection.Dispose();
            connection = null;
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
                    entry.Fortschritt = obj.Fortschritt;

                    connection.SubmitChanges();
                    break;
                case "Phase":
                    Table<Phase.db_Phase> tablePhase = connection.GetTable<Phase.db_Phase>();
                    var phaseObj = (Phase)obj;
                    var entryPhase = (from i in tablePhase
                                      where i.Pkey == phaseObj.Pkey
                                      select i).First();
                    entryPhase.Name = phaseObj.Name;
                    entryPhase.Status = phaseObj.Status;
                    entryPhase.Fortschritt = phaseObj.Fortschritt;
                    entryPhase.StartDatumG = phaseObj.StartDatumG;
                    entryPhase.EndDatumG = DateTime.Parse(phaseObj.EndDatumG.ToString("dd.MM.yyyy"));
                    entryPhase.StartDatum = phaseObj.StartDatum;
                    entryPhase.EndDatum = phaseObj.EndDatum;
                    entryPhase.FKey_PhaseTemplateID = phaseObj.FKey_PhaseTemplateID;
                    entryPhase.FKey_ProjektID = phaseObj.FKey_ProjektID;
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
                    entryAktivitaet.Dokumente = obj.Dokumente;
                    connection.SubmitChanges();
                    break;
                case "ExterneResource":
                    Table<ExterneResource.db_ExterneResource> tableExterneResource = connection.GetTable<ExterneResource.db_ExterneResource>();
                    var ExterneResourceObj = (ExterneResource)obj;
                    var entryExterneResource = (from i in tableExterneResource
                                                where i.Pkey == ExterneResourceObj.Pkey
                                                select i).First();
                    entryExterneResource.Name = obj.Name;
                    entryExterneResource.Art = obj.Art;
                    entryExterneResource.KostenG = obj.KostenG;
                    entryExterneResource.Pkey = obj.Pkey;
                    connection.SubmitChanges();
                    break;
                case "ZExterneResource":
                    Table<ZExterneResource.db_ExterneResource> tableZExterneResource = connection.GetTable<ZExterneResource.db_ExterneResource>();
                    var ZExterneResourceObj = (ZExterneResource)obj;
                    var entryZExterneResource = (from i in tableZExterneResource
                                                 where i.Pkey == ZExterneResourceObj.Pkey
                                                 select i).First();
                    entryZExterneResource.EndDatum = obj.EndDatum;
                    entryZExterneResource.FKey_Aktiviteat = obj.FKey_Aktiviteat;
                    entryZExterneResource.FKey_ExterneResource = obj.FKey_ExterneResource;
                    entryZExterneResource.StartDatum = obj.StartDatum;
                    entryZExterneResource.Pkey = obj.Pkey;
                    entryZExterneResource.Abweichung = obj.Abweichung;
                    entryZExterneResource.Kommentar = obj.Kommentar;
                    entryZExterneResource.Kosten = obj.Kosten;
                    connection.SubmitChanges();
                    break;
                case "PerseonenResource":
                    Table<PerseonenResource.db_PerseonenResource> tablePerseonenResource = connection.GetTable<PerseonenResource.db_PerseonenResource>();
                    var PersonenResourceObj = (PerseonenResource)obj;
                    var entryPerseonenResource = (from i in tablePerseonenResource
                                                  where i.Pkey == PersonenResourceObj.Pkey
                                                  select i).First();
                    entryPerseonenResource.Name = obj.Name;
                    entryPerseonenResource.Funktion = obj.Funktion;
                    entryPerseonenResource.KostenG = obj.KostenG;
                    entryPerseonenResource.Pkey = obj.Pkey;
                    connection.SubmitChanges();
                    break;
                case "ZPerseonenResource":
                    Table<ZPerseonenResource.db_ZPerseonenResource> tableZPerseonenResource = connection.GetTable<ZPerseonenResource.db_ZPerseonenResource>();
                    var ZPersonenResourceObj = (ZPerseonenResource)obj;
                    var entryZPerseonenResource = (from i in tableZPerseonenResource
                                                   where i.Pkey == ZPersonenResourceObj.Pkey
                                                   select i).First();
                    entryZPerseonenResource.EndDatum = obj.EndDatum;
                    entryZPerseonenResource.FKey_Aktiviteat = obj.FKey_Aktiviteat;
                    entryZPerseonenResource.FKey_PerseonenResource = obj.FKey_PerseonenResource;
                    entryZPerseonenResource.StartDatum = obj.StartDatum;
                    entryZPerseonenResource.Pkey = obj.Pkey;
                    entryZPerseonenResource.Abweichung = obj.Abweichung;
                    entryZPerseonenResource.Kommentar = obj.Kommentar;
                    entryZPerseonenResource.Kosten = obj.Kosten;
                    connection.SubmitChanges();
                    break;
                default:

                    break;
            }
            connection.Dispose();
            connection = null;
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
                case "ZExterneResource":
                    //aktuelles objekt aus der DB
                    Table<ZExterneResource.db_ExterneResource> tableZExterneResource = connection.GetTable<ZExterneResource.db_ExterneResource>();
                    var ZExterneResourceObj = (ZExterneResource)obj;
                    var entryZExterneResource = (from i in tableZExterneResource
                                                 where i.Pkey == ZExterneResourceObj.Pkey
                                                 select i).First();
                    //Remove
                    tableZExterneResource.DeleteOnSubmit(entryZExterneResource);
                    connection.SubmitChanges();
                    break;
                case "ZPerseonenResource":
                    //aktuelles objekt aus der DB
                    Table<ZPerseonenResource.db_ZPerseonenResource> tableZPerseonenResource = connection.GetTable<ZPerseonenResource.db_ZPerseonenResource>();
                    var ZPerseonenResourcetObj = (ZPerseonenResource)obj;
                    var entryZPerseonenResource = (from i in tableZPerseonenResource
                                                   where i.Pkey == ZPerseonenResourcetObj.Pkey
                                                   select i).First();
                    //Remove
                    tableZPerseonenResource.DeleteOnSubmit(entryZPerseonenResource);
                    connection.SubmitChanges();
                    break;
                default:

                    break;
            }
            connection.Dispose();
            connection = null;
        }
    }
}
