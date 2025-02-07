﻿using ProjektManagementTool.Helper;
using ProjektManagementTool.Models;
using ProjektManagementTool.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ProjektManagementTool.ViewModels
{
    class VorgehensmodellErfassenViewModel : BaseViewModel
    {
        //Helper variable für update
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

        //wählerListe um die Objekte zu aktualisieren nach dem ein Update ausgeführt wurde (Context)
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

        //Button: Phase erfassen
        ICommand _PhaseErfassen;
        public ICommand CMDPhaseErfassen
        {
            get
            {
                return _PhaseErfassen ?? (_PhaseErfassen =
                new RelayCommand(p => ShowPhaseErfassen()));
            }
        }
        //Funktion für den Command
        void ShowPhaseErfassen()
        {
            //Hier die neue View anzeigen
            var phaseBearbeitenView = new PhaseBearbeitenView();
            PhaseBearbeitenViewModel childContext = (PhaseBearbeitenViewModel)phaseBearbeitenView.DataContext;
            childContext.ParentDataContext = this;
            phaseBearbeitenView.Show();
        }

        //Button: Phase bearbeiten
        ICommand _PhaseBearbeiten;
        public ICommand CMDPhaseBearbeiten
        {
            get
            {
                return _PhaseBearbeiten ?? (_PhaseBearbeiten =
                new RelayCommand(p => ShowPhasBearbeiten()));
            }
        }
        //Funktion für den Command
        void ShowPhasBearbeiten()
        {
            //Hier die neue View anzeigen, muss noch als Parameter die aktuell ausgewählte phase übergeben
            var phaseBearbeitenView = new PhaseBearbeitenView();
            PhaseBearbeitenViewModel childContext = (PhaseBearbeitenViewModel)phaseBearbeitenView.DataContext;
            childContext.ParentDataContext = this;

            //Listbox abfüllen in der neuen View
            childContext.PhaseName = Phasen[Index];

            phaseBearbeitenView.Show();
        }

        //Liste mit den Phasen
        ObservableCollection<string> _Phasen;
        public ObservableCollection<string> Phasen
        {
            get { return _Phasen; }
            set
            {
                _Phasen = value;
                OnPropertyChanged("Phasen");
            }
        }

        //ListViewIndex
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

        //ModellName
        string _ModellName;
        public string ModellName
        {
            get { return _ModellName; }
            set
            {
                _ModellName = value;
                OnPropertyChanged("ModellName");
            }
        }

        //ModellBeschreibung
        string _ModellBeschreibung;
        public string ModellBeschreibung
        {
            get { return _ModellBeschreibung; }
            set
            {
                _ModellBeschreibung = value;
                OnPropertyChanged("ModellBeschreibung");
            }
        }

        //Button: Modell speichern
        ICommand _ModellSpeichern;
        public ICommand CMDModellSpeichern
        {
            get
            {
                return _ModellSpeichern ?? (_ModellSpeichern =
                new RelayCommand(p => ModellSpeichern()));
            }
        }
        //Funktion für den Command
        void ModellSpeichern()
        {
            //Checken ob alle Daten fürs Speichern vorhanden sind.
            if (string.IsNullOrEmpty(ModellName))
            {
                MessageBox.Show("Name kann nicht leer sein", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } else if (string.IsNullOrEmpty(ModellBeschreibung))
            {
                MessageBox.Show("Beschreibung kann nicht leer sein", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } else if (Phasen == null || Phasen.Count == 0)
            {
                MessageBox.Show("Es muss mindestens eine Phase erfasst sein", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Pkey == 0)
            {
                //Vorgensmodell Objekt erstellen
                var objModell = new Vorgehensmodell(0, ModellName, ModellBeschreibung);
                Pkey = objModell.CreateInDB();
                if (Pkey == -1)
                {
                    return;
                }
                //Phasen erstellen
                foreach (var phase in Phasen)
                {
                    var objPhaseTemplate = new PhaseTemplate(0, phase, Pkey);
                    objPhaseTemplate.CreateInDB();
                    MessageBox.Show("Modell erfasst", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } else
            {
                //Update
                var objModell = new Vorgehensmodell(Pkey, ModellName, ModellBeschreibung);
                objModell.Update();


                //Update anzeige view
                if (WaehlerContext.ListObj != null)
                {
                    var dbHelper = new DBHelper();
                    WaehlerContext.ListObj = new ObservableCollection<dynamic>(dbHelper.RunQuery("Vorgehensmodell", "Select * from Vorgehensmodell"));
                }
            }
            
        }
    }
}
