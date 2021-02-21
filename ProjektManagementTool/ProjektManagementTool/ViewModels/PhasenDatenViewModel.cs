using ProjektManagementTool.Helper;
using ProjektManagementTool.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ProjektManagementTool.ViewModels
{
    class PhasenDatenViewModel : BaseViewModel
    {
        //Helper
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
        int _FKey_ProjektID;
        public int FKey_ProjektID
        {
            get { return _FKey_ProjektID; }
            set
            {
                _FKey_ProjektID = value;
                OnPropertyChanged("FKey_ProjektID");
            }
        }
        int _FKey_PhaseTemplateID;
        public int FKey_PhaseTemplateID
        {
            get { return _FKey_PhaseTemplateID; }
            set
            {
                _FKey_PhaseTemplateID = value;
                OnPropertyChanged("FKey_PhaseTemplateID");
            }
        }
        ProjektBearbeitenViewModel _ParentContext;
        public ProjektBearbeitenViewModel ParentContext
        {
            get { return _ParentContext; }
            set
            {
                _ParentContext = value;
                OnPropertyChanged("ParentContext");
            }
        }

        //Textbox Values
        //Name Binding
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
        //Status Binding
        string _Status;
        public string Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                OnPropertyChanged("Status");
            }
        }
        //ProjektStatus Binding
        string _ProjektStatus;
        public string ProjektStatus
        {
            get { return _ProjektStatus; }
            set
            {
                _ProjektStatus = value;
                OnPropertyChanged("ProjektStatus");
            }
        }
        //Fortschritt Binding
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
        //Datenpicker values
        //Status StartDatumG
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
        //Status EndtDatumG
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
        //Status StartDatum
        Nullable<DateTime> _StartDatum;
        public Nullable<DateTime> StartDatum
        {
            get { return _StartDatum; }
            set
            {
                _StartDatum = value;
                OnPropertyChanged("StartDatum");
            }
        }
        //Status EndtDatumG
        Nullable<DateTime> _EndtDatum;
        public Nullable<DateTime> EndtDatum
        {
            get { return _EndtDatum; }
            set
            {
                _EndtDatum = value;
                OnPropertyChanged("EndtDatum");
            }
        }
        

        //Buttons
        //Starten
        ICommand _Starten;
        public ICommand CMDStarten
        {
            get
            {
                return _Starten ?? (_Starten =
                new RelayCommand(p => Phasestarten()));
            }
        }
        //Funktion für den Command
        void Phasestarten()
        {
            if (Status == "Eröffnet")
            {
                System.Windows.MessageBox.Show("Bitte zuerst die Phaen daten abfüllen und speichern. Aktuell sind noch die generierten Daten eingetragen", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            } else if (ProjektStatus != "In Arbeit")
            {
                System.Windows.MessageBox.Show("Phase kann erst gestartet werden, wenn das Projekt gestartet wurde", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            StartDatum = DateTime.Now;
            Status = "In Arbeit";
            Phasespeichern();
        }
        //Beenden
        ICommand _Beenden;
        public ICommand CMDBeenden
        {
            get
            {
                return _Beenden ?? (_Beenden =
                new RelayCommand(p => Phasebeenden()));
            }
        }
        //Funktion für den Command
        void Phasebeenden()
        {
            //Muss noch gemacht werden
        }
        //Speichern
        ICommand _Speichern;
        public ICommand CMDSpeichern
        {
            get
            {
                return _Speichern ?? (_Speichern =
                new RelayCommand(p => Phasespeichern()));
            }
        }
        //Funktion für den Command
        void Phasespeichern()
        {
            if (Status == "Eröffnet")
            {
                Status = "Erfasst";
            }
            var objPhase = new Phase(Pkey, Name, Status, Fortschritt, StartDatumG, EndtDatumG, StartDatum, EndtDatum, FKey_PhaseTemplateID, FKey_ProjektID);
            objPhase.Update();

            var dbHelper = new DBHelper();
            string query = $"Select * from Phase where FKey_ProjektID='{ParentContext.Pkey}'";
            ParentContext.PhasenListe = new ObservableCollection<dynamic>(dbHelper.RunQuery("Phase", query));
        }
    }
}
