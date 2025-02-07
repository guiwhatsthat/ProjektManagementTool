﻿using ProjektManagementTool.Helper;
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
        public ProjektBearbeitenViewModel()
        {
            var Prios = new List<string>();
            Prios.Add("Tief");
            Prios.Add("Mittel");
            Prios.Add("Hoch");
            PrioListe = Prios;
        }


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

        //UI elmente aktivieren
        public bool _StartenErlaubt;
        public bool StartenErlaubt
        {
            get { return _StartenErlaubt; }
            set
            {
                _StartenErlaubt = value;
                OnPropertyChanged("StartenErlaubt");
            }
        }
        public bool _PlanenErlaubt;
        public bool PlanenErlaubt
        {
            get { return _PlanenErlaubt; }
            set
            {
                _PlanenErlaubt = value;
                OnPropertyChanged("PlanenErlaubt");
            }
        }
        public bool _BeendenErlaubt;
        public bool BeendenErlaubt
        {
            get { return _BeendenErlaubt; }
            set
            {
                _BeendenErlaubt = value;
                OnPropertyChanged("BeendenErlaubt");
            }
        }
        public bool _FreigebenErlaubt;
        public bool FreigebenErlaubt
        {
            get { return _FreigebenErlaubt; }
            set
            {
                _FreigebenErlaubt = value;
                OnPropertyChanged("FreigebenErlaubt");
            }
        }

        //PrioListe
        List<String> _PrioListe;
        public List<String> PrioListe
        {
            get { return _PrioListe; }
            set
            {
                _PrioListe = value;
                OnPropertyChanged("PrioListe");
            }
        }
        //Prio
        string _Prio;
        public string Prio
        {
            get { return _Prio; }
            set
            {
                _Prio = value;
                OnPropertyChanged("Prio");
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
                }
                else
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
        //Meilensteine
        ObservableCollection<Meilenstein> _ListMeilensteine;
        public ObservableCollection<Meilenstein> ListMeilensteine
        {
            get { return _ListMeilensteine; }
            set
            {
                _ListMeilensteine = value;
                OnPropertyChanged("ListMeilensteine");
            }
        }
        //Meilenstein Index
        int _MeilensteinIndex;
        public int MeilensteinIndex
        {
            get { return _MeilensteinIndex; }
            set
            {
                _MeilensteinIndex = value;
                OnPropertyChanged("MeilensteinIndex");
            }
        }

        //AktivitaetenListe
        ObservableCollection<Aktivitaet> _ListAktivitaet;
        public ObservableCollection<Aktivitaet> ListAktivitaet
        {
            get { return _ListAktivitaet; }
            set
            {
                _ListAktivitaet = value;
                OnPropertyChanged("ListAktivitaet");
            }
        }
        //Aktivität Index
        int _AktivitaeteIndex;
        public int AktivitaeteIndex
        {
            get { return _AktivitaeteIndex; }
            set
            {
                _AktivitaeteIndex = value;
                OnPropertyChanged("AktivitaeteIndex");
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
            if (Status == "Erfasst")
            {
                var hinzufuegenView = new HinzufuegenView();
                HinzufuegenViewModel context = (HinzufuegenViewModel)hinzufuegenView.DataContext;
                context.Header = "Vorgehensmodell";
                var dbHelper = new DBHelper();
                context.ListObj = dbHelper.RunQuery("Vorgehensmodell", "Select * from Vorgehensmodell");
                context.ParentDataContext = this;
                hinzufuegenView.Show();
            }
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
            if (Pkey == 0)
            {
                return;
            }
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
                    var phaseObj = new Phase(0, template.Name, "Eröffnet", 0, DateTime.Now, DateTime.Now, null, null, template.Pkey, Pkey, "", null, null, DateTime.Now);
                    int phasePKey = phaseObj.CreateInDB();
                    phaseObj.Pkey = phasePKey;
                    //Phasen dem bindig zuweisen
                    PhasenListe.Add(phaseObj);
                }
                EnablePhase = false;
            }
            else
            {
                System.Windows.MessageBox.Show("Projekt wurde bereits freigegeben", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            PlanenErlaubt = true;
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
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Beschreibung) || string.IsNullOrEmpty(Mitarbeiter) || string.IsNullOrEmpty(Modell) || string.IsNullOrEmpty(Prio))
            {
                System.Windows.MessageBox.Show("Nicht alle Pflichtfelder ausgefüllt", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Check Daten
            if (!DateTime.TryParse(StartDatumG.ToString(), out DateTime a))
            {
                System.Windows.MessageBox.Show("Startdatum hat das falsche Format", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (!DateTime.TryParse(EndtDatumG.ToString(), out DateTime b))
            {
                System.Windows.MessageBox.Show("Enddatum hat das falsche Format", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }



            if (StartDatumG == DateTime.MinValue || EndtDatumG == DateTime.MinValue)
            {
                System.Windows.MessageBox.Show("Daten wurden nicht spezifiziert", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (StartDatumG > EndtDatumG)
            {
                System.Windows.MessageBox.Show("Startdatum muss vor dem enddatum liegen", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Pkey == 0)
            {
                //dieser test muss nur beim eröffnen gemacht werden
                if (DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")) > StartDatumG)
                {
                    System.Windows.MessageBox.Show("Startdatum muss heute oder in der Zukunft sein", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                //Kostenabrufn
                KostenAbrufen(false);

                //Projekt speichern
                Projekt projekt = new Projekt(0, Name, Beschreibung, null, StartDatumG, EndtDatumG, null, null, ProjektleiterID, KostenG, Kosten, VorgehensmodellID, Ablage, Status, Fortschritt, Prio);
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
                FreigebenErlaubt = true;
            }
            else
            {
                //Kostenabrufn
                KostenAbrufen(false);
                //Update
                if (null == Freigabedatum)
                {
                    Projekt projekt = new Projekt(Pkey, Name, Beschreibung, null, StartDatumG, EndtDatumG, null, null, ProjektleiterID, KostenG, Kosten, VorgehensmodellID, Ablage, Status, Fortschritt, Prio);
                    projekt.Update();
                }
                else
                {
                    Projekt projekt = new Projekt(Pkey, Name, Beschreibung, DateTime.Parse(Freigabedatum), StartDatumG, EndtDatumG, null, null, ProjektleiterID, KostenG, Kosten, VorgehensmodellID, Ablage, Status, Fortschritt, Prio);
                    projekt.Update();
                }

                var dbHelper = new DBHelper();

                //Update anzeige view
                if (WaehlerContext != null)
                {
                    WaehlerContext.ListObj = new ObservableCollection<dynamic>(dbHelper.RunQuery("Projekt", "Select * from Projekt"));
                }

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

            if (IndexPhase == -1)
            {
                return;
            }

            var phaseDatenView = new PhasenDatenView();
            var context = (PhasenDatenViewModel)phaseDatenView.DataContext;
            context.Name = PhasenListe[IndexPhase].Name;
            context.StartDatum = PhasenListe[IndexPhase].StartDatum;
            context.StartDatumG = PhasenListe[IndexPhase].StartDatumG;
            context.Status = PhasenListe[IndexPhase].Status;
            context.EndDatum = PhasenListe[IndexPhase].EndDatum;
            context.EndDatumG = PhasenListe[IndexPhase].EndDatumG;
            context.Fortschritt = PhasenListe[IndexPhase].Fortschritt;
            context.Pkey = PhasenListe[IndexPhase].Pkey;
            context.ProjektStatus = Status;
            context.FKey_ProjektID = PhasenListe[IndexPhase].FKey_ProjektID;
            context.FKey_PhaseTemplateID = PhasenListe[IndexPhase].FKey_PhaseTemplateID;
            context.FreigabeDatum = PhasenListe[IndexPhase].FreigabeDatum;
            context.Visum = PhasenListe[IndexPhase].Visum;
            context.ReviewDatumG = PhasenListe[IndexPhase].ReviewDatumG;
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
            StartenErlaubt = true;
            PlanenErlaubt = false;
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
            BeendenErlaubt = false;
            FreigebenErlaubt = false;
            PlanenErlaubt = false;
            StartenErlaubt = false;
            ReadOnlyStatus = true;
            Status = "Archiviert";
            Projektspeichern();
        }

        //Button Projekt löschen
        ICommand _Projektbeenden;
        public ICommand CMDProjektbeenden
        {
            get
            {
                return _Projektbeenden ?? (_Projektbeenden =
                new RelayCommand(p => Projektbeenden()));
            }
        }
        //Funktion für den Command
        void Projektbeenden()
        {
            bool abschliessenPhasen = true;
            //checken ob alle Phasen abgeschlossen sind
            foreach (var phase in PhasenListe)
            {
                if (phase.Status != "Review durchgeführt")
                {
                    abschliessenPhasen = false;
                }
            }
            if (abschliessenPhasen == false)
            {
                System.Windows.MessageBox.Show("Es müssen zuerst alle Reviews der Phasen abgeschlossen werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //checken ob alle Meilensteine abgeschlossen sind
            bool abschliessenMeilenstein = true;
            foreach (var meilenstein in ListMeilensteine)
            {
                if (meilenstein.Datum == null)
                {
                    abschliessenMeilenstein = false;
                }
            }
            if (abschliessenMeilenstein == false)
            {
                System.Windows.MessageBox.Show("Es müssen zuerst alle Meilensteine abgeschlossen werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Check ob Abweichungen Kommentiert sind
            bool abschliessenKosten = true;
            var dbHelper = new DBHelper();
            if (ListAktivitaet != null)
            {
                foreach (var aktivitaet in ListAktivitaet)
                {
                    if (abschliessenKosten == true)
                    {
                        //alle exterene Resourcen
                        string query = $"Select * from VKostenAbweichungExterne where Fkey_Aktivitaet='{aktivitaet.Pkey}' and Kommentar=''";
                        var resultE = dbHelper.RunQuery("VKostenAbweichungExterne", query);
                        if (resultE.Count > 0)
                        {
                            abschliessenKosten = false;
                            continue;
                        }
                        //alle Personen resourcen
                        if (abschliessenKosten == true)
                        {
                            query = $"Select * from VKostenAbweichungPersonen where FKey_Aktiviteat='{aktivitaet.Pkey}' and Kommentar=''";
                            var resultP = dbHelper.RunQuery("VKostenAbweichungPersonen", query);
                            if (resultP.Count > 0)
                            {
                                abschliessenKosten = false;
                                continue;
                            }
                        }
                    }
                }
                if (abschliessenKosten == false)
                {
                    System.Windows.MessageBox.Show("Es müssen zuerst alle Abweichungen der Kosten (in den Aktivitäten) kommentiert werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            

            //Ui Elemente deaktivieren
            BeendenErlaubt = false;
            FreigebenErlaubt = false;
            PlanenErlaubt = false;
            StartenErlaubt = false;
            ReadOnlyStatus = true;

            //Abschliessen
            Fortschritt = 100;
            Status = "Abgeschlossen";
            Projektspeichern();
            System.Windows.MessageBox.Show("Projekt abgeschlossen", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

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
            foreach (var phase in phasen)
            {
                if (phase.Status == "Eröffnet")
                {
                    startErlaubt = false;
                }
            }

            if (startErlaubt)
            {
                Status = "In Arbeit";
                BeendenErlaubt = true;
            }
            else
            {
                System.Windows.MessageBox.Show("Projekt kann nicht gestartet werden. Zuerst müssen alle Phasen bearbeitet werden. Sie dürfen nicht mehr den Status Eröffnet haben. Einne Statusänderung kann durch das Speichern der Phase erreicht werden.", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Projektspeichern();
        }
        //Meilenstein bearbeiten
        ICommand _MeilensteinBearbeiten;
        public ICommand CMDMeilensteinBearbeiten
        {
            get
            {
                return _MeilensteinBearbeiten ?? (_MeilensteinBearbeiten =
                new RelayCommand(p => MeilensteinBearbeiten()));
            }
        }
        //Funktion für den Command
        void MeilensteinBearbeiten()
        {
            var dbHelper = new DBHelper();
            var meilensteinbearbeiten = new MeilensteinBearbeitenView();
            var context = (MeilensteinBearbeitenViewModel)meilensteinbearbeiten.DataContext;
            if (ListMeilensteine == null || ListMeilensteine.Count == 0)
            {
                return;
            }
            Meilenstein meilenstein = ListMeilensteine[MeilensteinIndex];
            context.Name = meilenstein.Name;
            context.Datum = meilenstein.Datum;
            context.DatumG = meilenstein.DatumG;
            context.Pkey = meilenstein.Pkey;
            context.ParentContext = this;
            context.ProjektName = Name;
            string query = $"select * from Phase where Pkey='{meilenstein.FKey_PhaseID}'";
            var phase = dbHelper.RunQuery("Phase", query);
            context.PhaseName = phase[0].Name;
            context.Phasen = PhasenListe;
            meilensteinbearbeiten.Show();
        }

        //Meilenstein erfassen
        ICommand _MeilensteinErfassen;
        public ICommand CMDMeilensteinErfassen
        {
            get
            {
                return _MeilensteinErfassen ?? (_MeilensteinErfassen =
                new RelayCommand(p => MeilensteinErfassen()));
            }
        }
        //Funktion für den Command
        void MeilensteinErfassen()
        {
            if (Status != "In Planung" && Status != "In Arbeit")
            {
                System.Windows.MessageBox.Show("Meilensteine könnent nur bearbeitet werden, wenn das Projekt in Planung ist", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var dbHelper = new DBHelper();
            var meilensteinbearbeiten = new MeilensteinBearbeitenView();
            var context = (MeilensteinBearbeitenViewModel)meilensteinbearbeiten.DataContext;
            context.ParentContext = this;
            context.ProjektName = Name;
            context.Phasen = PhasenListe;
            meilensteinbearbeiten.Show();
        }

        //Aktivitaeter fassen
        ICommand _Aktivitaeterfassen;
        public ICommand CMDAktivitaeterfassen
        {
            get
            {
                return _Aktivitaeterfassen ?? (_Aktivitaeterfassen =
                new RelayCommand(p => Aktivitaeterfassen()));
            }
        }
        //Funktion für den Command
        void Aktivitaeterfassen()
        {
            if (Status != "In Planung" && Status != "In Arbeit")
            {
                System.Windows.MessageBox.Show("Aktivitäten könnent nur bearbeitet werden, wenn das Projekt in Planung ist", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var aktiviteatBearbeitenView = new AktiviteatBearbeitenView();
            var context = (AktiviteatBearbeitenViewModel)aktiviteatBearbeitenView.DataContext;
            context.ParentDataContext = this;
            context.ListPhasen = PhasenListe;
            aktiviteatBearbeitenView.Show();
        }

        //Meilenstein erfassen
        ICommand _Aktivitaetbearbeiten;
        public ICommand CMDAktivitaetbearbeiten
        {
            get
            {
                return _Aktivitaetbearbeiten ?? (_Aktivitaetbearbeiten =
                new RelayCommand(p => Aktivitaetbearbeiten()));
            }
        }
        //Funktion für den Command
        void Aktivitaetbearbeiten()
        {
            if (ListAktivitaet == null || ListAktivitaet.Count == 0 || AktivitaeteIndex == -1)
            {
                return;
            }
            var dbHelper = new DBHelper();
            var aktiviteatBearbeitenView = new AktiviteatBearbeitenView();
            var context = (AktiviteatBearbeitenViewModel)aktiviteatBearbeitenView.DataContext;
            context.ParentDataContext = this;
            context.ListPhasen = PhasenListe;

            var aktivitaet = ListAktivitaet[AktivitaeteIndex];
            string query = $"Select * from Mitarbeiter where Pkey='{aktivitaet.FKey_VerantwortlichePersonID}'";
            var mitarbeiter = dbHelper.RunQuery("Mitarbeiter", query)[0];
            context.MitarbeiterName = mitarbeiter.Vorname + " " + mitarbeiter.Nachname;
            context.MitarbeiterPkey = aktivitaet.FKey_VerantwortlichePersonID;
            context.Name = aktivitaet.Name;
            context.PersonenKosten = aktivitaet.BudgetPersonenKosten;
            context.PersonenKostenG = aktivitaet.BudgetPersonenKostenG;
            context.ExterneKosten = aktivitaet.BudgetExterneKosten;
            context.ExterneKostenG = aktivitaet.BudgetExterneKostenG;
            query = $"Select * from Phase where Pkey='{aktivitaet.FKey_PhaseID}'";
            context.PhaseName = dbHelper.RunQuery("Phase", query)[0].Name;
            context.StartDatum = aktivitaet.StartDatum;
            context.StartDatumG = aktivitaet.StartDatumG;
            context.EndDatum = aktivitaet.EndDatum;
            context.EndDatumG = aktivitaet.EndDatumG;
            context.Pkey = aktivitaet.Pkey;
            context.PhasePkey = aktivitaet.FKey_PhaseID;
            context.MitarbeiterPkey = aktivitaet.FKey_VerantwortlichePersonID;
            context.Fortschritt = aktivitaet.Fortschritt;

            //Liste mit allen zugewiesenen Kosten
            var kosten = new ObservableCollection<GenericKosten>();

            //externeKosten
            query = $"Select * from Z_ExterneResource where FKey_Aktiviteat='{aktivitaet.Pkey}'";
            var listZTable = dbHelper.RunQuery("ZExterneResource", query);
            foreach (var result in listZTable)
            {
                query = $"Select * from ExterneResource where Pkey='{result.FKey_ExterneResource}'";
                var externeKosten = dbHelper.RunQuery("ExterneResource", query)[0];
                if (externeKosten != null)
                {
                    var genericKosten = new GenericKosten("ExterneKosten", externeKosten.Name, externeKosten.Pkey, aktivitaet.Pkey, result.Pkey);
                    kosten.Add(genericKosten);
                }
            }

            //personenkosten
            query = $"Select * from Z_PerseonenResource where FKey_Aktiviteat='{aktivitaet.Pkey}'";
            listZTable = dbHelper.RunQuery("ZPerseonenResource", query);
            foreach (var result in listZTable)
            {
                query = $"Select * from PerseonenResource where Pkey='{result.FKey_PerseonenResource}'";
                var externeKosten = dbHelper.RunQuery("PerseonenResource", query)[0];
                if (externeKosten != null)
                {
                    var genericKosten = new GenericKosten("PersonenKosten", externeKosten.Name, externeKosten.Pkey, aktivitaet.Pkey, result.Pkey);
                    kosten.Add(genericKosten);
                }
            }

            query = $"Select * from Z_PerseonenResource where FKey_Aktiviteat='{aktivitaet.Pkey}'";
            var listP = dbHelper.RunQuery("ZPerseonenResource", query);
            decimal pKosten = 0;
            foreach (var p in listP)
            {
                pKosten += p.Kosten;
            }

            query = $"Select * from Z_ExterneResource where FKey_Aktiviteat='{aktivitaet.Pkey}'";
            var listE = dbHelper.RunQuery("ZExterneResource", query);
            decimal eKosten = 0;
            foreach (var e in listE)
            {
                eKosten += e.Kosten;
            }

            context.PersonenKosten = pKosten;
            context.ExterneKosten = eKosten;
            context.ListKosten = kosten;
            aktiviteatBearbeitenView.Show();
        }
        //Meilenstein erfassen
        ICommand _KostenAbrufen;
        public ICommand CMDKostenAbrufen
        {
            get
            {
                return _KostenAbrufen ?? (_KostenAbrufen =
                new RelayCommand(p => KostenAbrufen()));
            }
        }
        //Funktion für den Command
        void KostenAbrufen(bool save = true)
        {
            decimal alleKosten = 0;
            if (ListAktivitaet == null || ListAktivitaet.Count == 0)
            {
                return;
            }
            foreach (var a in ListAktivitaet)
            {
                alleKosten += a.BudgetExterneKosten;
                alleKosten += a.BudgetPersonenKosten;
            }
            Kosten = alleKosten;
            if (save)
            {
                Projektspeichern();
            }   
        }
    }
}
