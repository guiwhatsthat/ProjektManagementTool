using ProjektManagementTool.Helper;
using ProjektManagementTool.Models;
using ProjektManagementTool.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ProjektManagementTool.ViewModels
{
    class BearbeitenWaehlerViewModel : BaseViewModel
    {
        //Typ
        string _Header;
        public string Header
        {
            get { return _Header; }
            set
            {
                _Header = value;
                OnPropertyChanged("Header");
            }
        }
        //Index
        int _Index;
        public int Index
        {
            get { return _Index; }
            set
            {
                _Index = value;
                OnPropertyChanged("Index");
            }
        }

        //Liste mit den Objekten
        ObservableCollection<dynamic> _ListObj;
        public ObservableCollection<dynamic> ListObj
        {
            get { return _ListObj; }
            set
            {
                _ListObj = value;
                OnPropertyChanged("ListObj");
            }
        }

        //Button Zuweisen
        ICommand _Bearbeiten;
        public ICommand CMDBearbeiten
        {
            get
            {
                return _Bearbeiten ?? (_Bearbeiten =
                new RelayCommand(p => ShowBearbeiten()));
            }
        }
        //Funktion für den Command
        void ShowBearbeiten()
        {
            if (ListObj.Count == 0)
            {
                return;
            }
            else if (Index > ListObj.Count - 1)
            {
                return;
            }
            var dbhelper = new DBHelper();
            if (Header == "Projekt")
            {
                string query = $"Select * from Mitarbeiter where PKey='{ListObj[Index].FKey_ProjektleiterID}'";
                Mitarbeiter mitarbeiter = (Mitarbeiter)dbhelper.RunQuery("Mitarbeiter", query)[0];
                query = $"Select * from Vorgehensmodell where PKey='{ListObj[Index].FKey_VorgehensmodellID}'";
                Vorgehensmodell vorgehensmodell = (Vorgehensmodell)dbhelper.RunQuery("Vorgehensmodell", query)[0];

                var projektbearbeiten = new ProjektBearbeitenView();
                var context = (ProjektBearbeitenViewModel)projektbearbeiten.DataContext;
                context.Ablage = ListObj[Index].Dokumente;
                context.Beschreibung = ListObj[Index].Beschreibung;
                context.EndtDatumG = ListObj[Index].EndDatumG;
                context.Fortschritt = ListObj[Index].Fortschritt;
                context.Freigabedatum = Convert.ToString(ListObj[Index].FreigabeDatum);
                context.Kosten = ListObj[Index].Kosten;
                context.KostenG = ListObj[Index].KostenG;
                context.Mitarbeiter = mitarbeiter.Vorname + " " + mitarbeiter.Nachname;
                context.Modell = vorgehensmodell.Name;
                context.Name = ListObj[Index].Name;
                context.StartDatumG = ListObj[Index].StartDatumG;
                context.Status = ListObj[Index].Status;
                context.Pkey = ListObj[Index].Pkey;
                context.ProjektleiterID = mitarbeiter.Pkey;
                context.VorgehensmodellID = vorgehensmodell.Pkey;
                context.EnablePhase = true;
                context.Prio = ListObj[Index].Prioritaet.Trim();
                query = $"Select * from Phase where FKey_ProjektID='{ListObj[Index].Pkey}'";
                context.PhasenListe = new ObservableCollection<dynamic>(dbhelper.RunQuery("Phase", query));
                var aktivitaetListe = new ObservableCollection<Aktivitaet>();
                foreach (var obj in context.PhasenListe)
                {
                    var phase = (Phase)obj;
                    query = $"Select * from Aktivitaet where FKey_PhaseID='{phase.Pkey}'";
                    var listAktivitaeten = dbhelper.RunQuery("Aktivitaet", query);
                    foreach (Aktivitaet aktivitaet in listAktivitaeten)
                    {
                        aktivitaetListe.Add((Aktivitaet)aktivitaet);
                    }

                }
                //aktivitäten
                context.ListAktivitaet = aktivitaetListe;
                //List der angezeigten Elemente übergeben
                context.WaehlerContext = this;

                //Meilensteine 
                ObservableCollection<Meilenstein> listMeilensteine = new ObservableCollection<Meilenstein>();
                foreach (var phase in context.PhasenListe)
                {
                    query = $"Select * from Meilenstein where FKey_PhaseID='{phase.Pkey}'";
                    foreach (var i in dbhelper.RunQuery("Meilenstein", query))
                    {
                        listMeilensteine.Add((Meilenstein)i);
                    }
                }
                context.ListMeilensteine = listMeilensteine;

                //Kosten
                decimal alleKosten = 0;
                foreach (var a in aktivitaetListe)
                {
                    alleKosten += a.BudgetExterneKosten;
                    alleKosten += a.BudgetPersonenKosten;
                }
                context.Kosten = alleKosten;
                //UI elemente ausblenden
                string status = ListObj[Index].Status;
                context.StartenErlaubt = false;
                context.StartenErlaubt = false;
                context.BeendenErlaubt = false;
                context.FreigebenErlaubt = false;

                switch (status)
                {
                    case "Erfasst":
                        context.FreigebenErlaubt = true;
                        break;
                    case "Freigegeben":
                        context.PlanenErlaubt = true;
                        break;
                    case "In Planung":
                        context.StartenErlaubt = true;
                        break;
                    case "In Arbeit":
                        context.BeendenErlaubt = true;
                        break;
                    default:
                        break;
                }
                projektbearbeiten.Show();

            }
            else
            {
                var vorgehensmodellErfassen = new VorgehensmodellErfassenView();
                var context = (VorgehensmodellErfassenViewModel)vorgehensmodellErfassen.DataContext;

                context.ModellName = ListObj[Index].Name;
                context.ModellBeschreibung = ListObj[Index].Beschreibung;
                context.Pkey = ListObj[Index].Pkey;

                //Phasen holen
                string query = $"Select * from PhaseTemplate where FKey_VorgehensmodellID='{ListObj[Index].Pkey}'";
                var phaseTemplates = dbhelper.RunQuery("PhaseTemplate", query);
                var list = new ObservableCollection<string>();
                foreach (var i in phaseTemplates)
                {
                    list.Add(i.Name);
                }
                context.Phasen = list;

                //List der angezeigten Elemente übergeben
                context.WaehlerContext = this;

                vorgehensmodellErfassen.Show();
            }
        }
    }
}
