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
        List<dynamic> _ListObj;
        public List<dynamic> ListObj
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
                context.Freigabedatum = ListObj[Index].FreigabeDatum;
                context.Kosten = ListObj[Index].Kosten;
                context.KostenG = ListObj[Index].KostenG;
                context.Mitarbeiter = mitarbeiter.Vorname + " " + mitarbeiter.Nachname;
                context.Modell = vorgehensmodell.Name;
                context.Name = ListObj[Index].Name;
                context.StartDatumG = ListObj[Index].StartDatumG;
                context.Status = ListObj[Index].Status;
                context.Pkey = ListObj[Index].Pkey;
                context.EnablePhase = true;

                //Meilensteine 

                //aktivitäten

                projektbearbeiten.Show();
            } else
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
                foreach(var i in phaseTemplates)
                {
                    list.Add(i.Name);
                }
                context.Phasen = list;
                vorgehensmodellErfassen.Show();
            }
        }
    }
}
