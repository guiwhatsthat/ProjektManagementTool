using ProjektManagementTool.Helper;
using ProjektManagementTool.Models;
using ProjektManagementTool.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace ProjektManagementTool.ViewModels
{
    class ProjektBearbeitenViewModel : BaseViewModel
    {
        //Aktion die ausgeführt werden muss (Erfassen/Bearbeiten)
        string _Aktion;
        public string Aktion
        {
            get { return _Aktion; }
            set
            {
                _Aktion = value;
                OnPropertyChanged("Aktion");
            }
        }
        //Helper Value für die FKey
        int _ProjektleiterID;
        public int ProjektleiterID
        {
            get { return _ProjektleiterID; }
            set
            {
                _ProjektleiterID = value;
                OnPropertyChanged("ProjektleiterID");
            }
        }
        int _VorgehensmodellID;
        public int VorgehensmodellID
        {
            get { return _VorgehensmodellID; }
            set
            {
                _VorgehensmodellID = value;
                OnPropertyChanged("VorgehensmodellID");
            }
        }
        int _Pkey;
        public int Pkey
        {
            get { return _Pkey; }
            set
            {
                _Pkey = value;
                OnPropertyChanged("Pkey");
            }
        }
        //wählerListe um die Objekte zu aktualisieren nach dem ein Update ausgeführt wurde
        //Liste mit den Objekten
        BearbeitenWaehlerViewModel _WaehlerContext;
        public BearbeitenWaehlerViewModel WaehlerContext
        {
            get { return _WaehlerContext; }
            set
            {
                _WaehlerContext = value;
                OnPropertyChanged("WaehlerContext");
            }
        }

        //Objekte welche in diesem Status nicht ertfasst werden können blocken
        bool _EnablePhase;
        public bool EnablePhase
        {
            get { return _EnablePhase; }
            set
            {
                _EnablePhase = value;
                OnPropertyChanged("EnablePhase");
            }
        }
        bool _ReadOnlyFortschritt;
        public bool ReadOnlyFortschritt
        {
            get { return _ReadOnlyFortschritt; }
            set
            {
                _ReadOnlyFortschritt = value;
                OnPropertyChanged("ReadOnlyFortschritt");
            }
        }

        bool _ReadOnlyStatus;
        public bool ReadOnlyStatus
        {
            get { return _ReadOnlyStatus; }
            set
            {
                _ReadOnlyStatus = value;
                OnPropertyChanged("ReadOnlyStatus");
            }
        }
        //Fortschritt
        int _IndexPhase;
        public int IndexPhase
        {
            get { return _IndexPhase; }
            set
            {
                _IndexPhase = value;
                OnPropertyChanged("IndexPhase");
            }
        }


        //Alle TextBox Values
        //Name
        string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        //Beschreibung
        string _Beschreibung;
        public string Beschreibung
        {
            get { return _Beschreibung; }
            set
            {
                _Beschreibung = value;
                OnPropertyChanged("Beschreibung");
            }
        }
        //KostenG
        decimal _KostenG;
        public decimal KostenG
        {
            get { return _KostenG; }
            set
            {
                _KostenG = value;
                OnPropertyChanged("KostenG");
            }
        }
        //Fortschritt
        int _Fortschritt;
        public int Fortschritt
        {
            get { return _Fortschritt; }
            set
            {
                _Fortschritt = value;
                OnPropertyChanged("Fortschritt");
            }
        }
        //Staus
        string _Status;
        public string Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                OnPropertyChanged("Status");
                if (Status == "Freigegeben" || Status == "Erfasst" || Status == "Archiviert")
                {
                    EnablePhase = false;
                } else
                {
                    EnablePhase = true;
                }
            }
        }
        //Kosten
        decimal _Kosten;
        public decimal Kosten
        {
            get { return _Kosten; }
            set
            {
                _Kosten = value;
                OnPropertyChanged("Kosten");
            }
        }
        //Mitarbeiter
        string _Mitarbeiter;
        public string Mitarbeiter
        {
            get { return _Mitarbeiter; }
            set
            {
                _Mitarbeiter = value;
                OnPropertyChanged("Mitarbeiter");
            }
        }
        //Modell
        string _Modell;
        public string Modell
        {
            get { return _Modell; }
            set
            {
                _Modell = value;
                OnPropertyChanged("Modell");
            }
        }
        //Ablage
        string _Ablage;
        public string Ablage
        {
            get { return _Ablage; }
            set
            {
                _Ablage = value;
                OnPropertyChanged("Ablage");
            }
        }
        //Phasen
        ObservableCollection<dynamic> _PhasenListe;
        public ObservableCollection<dynamic> PhasenListe
        {
            get { return _PhasenListe; }
            set
            {
                _PhasenListe = value;
                OnPropertyChanged("PhasenListe");
            }
        }
        //Datum Eigenschaften
        //StartDatumG
        DateTime _StartDatumG;
        public DateTime StartDatumG
        {
            get { return _StartDatumG; }
            set
            {
                _StartDatumG = value;
                OnPropertyChanged("StartDatumG");
            }
        }
        //StartDatumG
        DateTime _EndtDatumG;
        public DateTime EndtDatumG
        {
            get { return _EndtDatumG; }
            set
            {
                _EndtDatumG = value;
                OnPropertyChanged("EndtDatumG");
            }
        }
        //Freigabedatum
        string _Freigabedatum;
        public string Freigabedatum
        {
            get { return _Freigabedatum; }
            set
            {
                _Freigabedatum = value;
                OnPropertyChanged("Freigabedatum");
            }
        }

        //Buttons

        //Button Mitarbeiter hinzufügen
        ICommand _MitarbeiterHinzufuegen;
        public ICommand CMDShowMitarbeiterHinzufuegen
        {
            get
            {
                return _MitarbeiterHinzufuegen ?? (_MitarbeiterHinzufuegen =
                new RelayCommand(p => ShowMitarbeiterHinzufuegen()));
            }
        }
        //Funktion für den Command
        void ShowMitarbeiterHinzufuegen()
        {
            var hinzufuegenView = new HinzufuegenView();
            HinzufuegenViewModel context = (HinzufuegenViewModel)hinzufuegenView.DataContext;
            context.Header = "Mitarbeiter";
            var dbHelper = new DBHelper();
            context.ListObj = dbHelper.RunQuery("Mitarbeiter", "Select * from Mitarbeiter");
            context.ParentDataContext = this;
            hinzufuegenView.Show();
        }
        //Button Vorgehensmodell hinzufügen
        ICommand _Vorgehensmodellhinzufügen;
        public ICommand CMDShowVorgehensmodellhinzufügen
        {
            get
            {
                return _Vorgehensmodellhinzufügen ?? (_Vorgehensmodellhinzufügen =
                new RelayCommand(p => ShowVorgehensmodellhinzufügen()));
            }
        }
        //Funktion für den Command
        void ShowVorgehensmodellhinzufügen()
        {
            var hinzufuegenView = new HinzufuegenView();
            HinzufuegenViewModel context = (HinzufuegenViewModel)hinzufuegenView.DataContext;
            context.Header = "Vorgehensmodell";
            var dbHelper = new DBHelper();
            context.ListObj = dbHelper.RunQuery("Vorgehensmodell", "Select * from Vorgehensmodell");
            context.ParentDataContext = this;
            hinzufuegenView.Show();
        }
        //Button Ablage hinzufügen
        ICommand _Ablagehinzufügen;
        public ICommand CMDShowAblagehinzufügen
        {
            get
            {
                return _Ablagehinzufügen ?? (_Ablagehinzufügen =
                new RelayCommand(p => ShowAblagehinzufügen()));
            }
        }
        //Funktion für den Command
        void ShowAblagehinzufügen()
        {
            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                Ablage = fbd.SelectedPath;

            }
        }
        //Button Aktivität erfassen
        ICommand _Aaktivitaeterfassen;
        public ICommand CMDShowAaktivitaeterfassen
        {
            get
            {
                return _Aaktivitaeterfassen ?? (_Aaktivitaeterfassen =
                new RelayCommand(p => ShowAaktivitaeterfassen()));
            }
        }
        //Funktion für den Command
        void ShowAaktivitaeterfassen()
        {

        }
        //Button Aktivität bearbeiten
        ICommand _Aktivitaetbearbeiten;
        public ICommand CMDShowAktivitaetbearbeiten
        {
            get
            {
                return _Aktivitaetbearbeiten ?? (_Aktivitaetbearbeiten =
                new RelayCommand(p => ShowAktivitaetbearbeiten()));
            }
        }
        //Funktion für den Command
        void ShowAktivitaetbearbeiten()
        {

        }
        //Button Freigeabe erteilen
        ICommand _Freigeabeerteilen;
        public ICommand CMDFreigeabeerteilen
        {
            get
            {
                return _Freigeabeerteilen ?? (_Freigeabeerteilen =
                new RelayCommand(p => Freigeabeerteilen()));
            }
        }
        //Funktion für den Command
        void Freigeabeerteilen()
        {
            if (string.IsNullOrEmpty(Freigabedatum))
            {
                Freigabedatum = DateTime.Now.ToString("dd.MM.yyyy");
                Status = "Freigegeben";
                Projektspeichern();

                //Phasen generieren
                var dbHelper = new DBHelper();

                //PhasenTemplates holen
                string query = $"Select * from PhaseTemplate where FKey_VorgehensmodellID='{VorgehensmodellID}'";
                var phasenTemplates = dbHelper.RunQuery("PhaseTemplate", query);
                //Duch phasentemplates loopen und phasen mit Projekt pkey als fkey erstellen
                PhasenListe = new ObservableCollection<dynamic>();
                foreach (var template in phasenTemplates)
                {
                    //Create Phasen
                    var phaseObj = new Phase(0,template.Name,"Eröffnet",0,DateTime.Now, DateTime.Now,null,null,template.Pkey, Pkey);
                    int phasePKey = phaseObj.CreateInDB();
                    //Phasen dem bindig zuweisen
                    PhasenListe.Add(phaseObj);
                }
                EnablePhase = false;
            } else
            {
                System.Windows.MessageBox.Show("Projekt wurde bereits freigegeben", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        //Button Projekt speicher
        ICommand _Projektspeichern;
        public ICommand CMDProjektspeichern
        {
            get
            {
                return _Projektspeichern ?? (_Projektspeichern =
                new RelayCommand(p => Projektspeichern()));
            }
        }
        //Funktion für den Command
        void Projektspeichern()
        {
            //Check Daten leer
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Beschreibung) || string.IsNullOrEmpty(Mitarbeiter) || string.IsNullOrEmpty(Modell))
            {
                System.Windows.MessageBox.Show("Nicht alle Pflichtfelder ausgefüllt", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Check Daten
            if (!DateTime.TryParse(StartDatumG.ToString(), out DateTime a))
            {
                System.Windows.MessageBox.Show("Startdatum hat das falsche Format", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } else if (!DateTime.TryParse(EndtDatumG.ToString(), out DateTime b))
            {
                System.Windows.MessageBox.Show("Enddatum hat das falsche Format", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            

            if (StartDatumG == DateTime.MinValue || EndtDatumG == DateTime.MinValue)
            {
                System.Windows.MessageBox.Show("Daten wurden nicht spezifiziert", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } else if (StartDatumG > EndtDatumG)
            {
                System.Windows.MessageBox.Show("Startdatum muss vor dem enddatum liegen", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Pkey == 0)
            {
                //dieser test muss nur beim eröffnen gemacht werden
                if (DateTime.Now > StartDatumG)
                {
                    System.Windows.MessageBox.Show("Startdatum muss heute oder in der Zukunft sein", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                //Projekt speichern
                Projekt projekt = new Projekt(0, Name, Beschreibung, null, StartDatumG, EndtDatumG, null, null, ProjektleiterID, KostenG, Kosten, VorgehensmodellID, Ablage, Status, Fortschritt);
                int projektPkey = projekt.CreateInDB();
                if (projektPkey == -1)
                {
                    System.Windows.MessageBox.Show("Konnte nicht in DB geschrieben werden", "fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    System.Windows.MessageBox.Show("Projekt erfasst", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                Pkey = projektPkey;
            } else
            {
                //Update
                if (null == Freigabedatum)
                {
                    Projekt projekt = new Projekt(Pkey, Name, Beschreibung,null, StartDatumG, EndtDatumG, null, null, ProjektleiterID, KostenG, Kosten, VorgehensmodellID, Ablage, Status, Fortschritt);
                    projekt.Update();
                } else
                {
                    Projekt projekt = new Projekt(Pkey, Name, Beschreibung, DateTime.Parse(Freigabedatum), StartDatumG, EndtDatumG, null, null, ProjektleiterID, KostenG, Kosten, VorgehensmodellID, Ablage, Status, Fortschritt);
                    projekt.Update();
                }
                
                var dbHelper = new DBHelper();

                //Update anzeige view
                WaehlerContext.ListObj = new ObservableCollection<dynamic>(dbHelper.RunQuery("Projekt", "Select * from Projekt"));
            }
            

        }
        //Button Projekt planen
        ICommand _ShowPhaseDaten;
        public ICommand CMDPhaseDatenBearbeiten
        {
            get
            {
                return _ShowPhaseDaten ?? (_ShowPhaseDaten =
                new RelayCommand(p => ShowPhaseDaten()));
            }
        }
        //Funktion für den Command
        void ShowPhaseDaten()
        {
            if (Status != "In Planung" && Status != "In Arbeit")
            {
                System.Windows.MessageBox.Show("Phasen könnent nur bearbeitet werden, wenn das Projekt in Planung ist", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var phaseDatenView = new PhasenDatenView();
            var context = (PhasenDatenViewModel)phaseDatenView.DataContext;
            context.Name = PhasenListe[IndexPhase].Name;
            context.StartDatum = PhasenListe[IndexPhase].StartDatum;
            context.StartDatumG = PhasenListe[IndexPhase].StartDatumG;
            context.Status = PhasenListe[IndexPhase].Status;
            context.EndtDatum = PhasenListe[IndexPhase].EndDatum;
            context.EndtDatumG = PhasenListe[IndexPhase].EndDatumG;
            context.Fortschritt = PhasenListe[IndexPhase].Fortschritt;
            context.Pkey = PhasenListe[IndexPhase].Pkey;
            context.ProjektStatus = Status;
            context.FKey_ProjektID = PhasenListe[IndexPhase].FKey_ProjektID;
            context.FKey_PhaseTemplateID = PhasenListe[IndexPhase].FKey_PhaseTemplateID;
            context.ParentContext = this;
            phaseDatenView.Show();
        }

        //Button Projekt planen
        ICommand _Projektplanen;
        public ICommand CMDPlanen
        {
            get
            {
                return _Projektplanen ?? (_Projektplanen =
                new RelayCommand(p => Projektplanen()));
            }
        }
        //Funktion für den Command
        void Projektplanen()
        {
            Status = "In Planung";
            Projektspeichern();
        }

        //Button Projekt löschen
        ICommand _Projektloeschen;
        public ICommand CMDProjektloeschen
        {
            get
            {
                return _Projektloeschen ?? (_Projektloeschen =
                new RelayCommand(p => Projektloeschen()));
            }
        }
        //Funktion für den Command
        void Projektloeschen()
        {
            Status = "Archiviert";
            Projektspeichern();
        }

        //Button Projekt Starten
        ICommand _Projektstarten;
        public ICommand CMDStarten
        {
            get
            {
                return _Projektstarten ?? (_Projektstarten =
                new RelayCommand(p => Projektstarten()));
            }
        }
        //Funktion für den Command
        void Projektstarten()
        {
            //Checken ob alle Phasen den status erfasst haben
            bool startErlaubt = true;
            var dbHelper = new DBHelper();
            string query = $"Select * from Phase where FKey_ProjektID='{Pkey}'";
            List<dynamic> phasen = dbHelper.RunQuery("Phase", query);
            foreach(var phase in phasen)
            {
                if (phase.Status == "Eröffnet")
                {
                    startErlaubt = false;
                }
            }

            if (startErlaubt)
            {
                Status = "In Arbeit";
            } else
            {
                System.Windows.MessageBox.Show("Projekt kann nicht gestartet werden. Zuerst müssen alle Phasen bearbeitet werden.", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

        }
    }
}
