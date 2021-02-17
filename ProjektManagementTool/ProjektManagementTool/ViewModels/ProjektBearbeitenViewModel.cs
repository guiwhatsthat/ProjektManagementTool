using ProjektManagementTool.Helper;
using ProjektManagementTool.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ProjektManagementTool.ViewModels
{
    class ProjektBearbeitenViewModel : BaseViewModel
    {
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
        string _Fortschritt;
        public string Fortschritt
        {
            get { return _Fortschritt; }
            set
            {
                _Fortschritt = value;
                OnPropertyChanged("Fortschritt");
            }
        }
        //Staus
        string _Staus;
        public string Staus
        {
            get { return _Staus; }
            set
            {
                _Staus = value;
                OnPropertyChanged("Staus");
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

        }
    }
}
