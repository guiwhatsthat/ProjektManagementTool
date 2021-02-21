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
    class MeilensteinBearbeitenViewModel : BaseViewModel
    {
        //Eigenschaften
        //Pkey Meilenstein
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
        //Name Meilenstein
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
        //Datum Geplant
        DateTime _DatumG;
        public DateTime DatumG
        {
            get { return _DatumG; }
            set
            {
                _DatumG = value;
                OnPropertyChanged("DatumG");
            }
        }
        //Phasen
        ObservableCollection<dynamic> _Phasen;
        public ObservableCollection<dynamic> Phasen
        {
            get { return _Phasen; }
            set
            {
                _Phasen = value;
                OnPropertyChanged("Phasen");
            }
        }
        //Index Phasen
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
        //Datum
        DateTime _Datum;
        public DateTime Datum
        {
            get { return _Datum; }
            set
            {
                _Datum = value;
                OnPropertyChanged("Datum");
            }
        }
        //PhaseName
        string _PhaseName;
        public string PhaseName
        {
            get { return _PhaseName; }
            set
            {
                _PhaseName = value;
                OnPropertyChanged("PhaseName");
            }
        }
        //Helper variable für update
        string _ProjektName;
        public string ProjektName
        {
            get { return _ProjektName; }
            set
            {
                _ProjektName = value;
                OnPropertyChanged("ProjektName");
            }
        }
        //Helper variable für update
        ProjektBearbeitenViewModel _ParentContext;
        public ProjektBearbeitenViewModel ParentContext
        {
            get { return _ParentContext; }
            set
            {
                _ParentContext = value;
                OnPropertyChanged("ParentContext ");
            }
        }

        //Buttons
        //Phase zuweisen
        ICommand _Waehlen;
        public ICommand CMDWaehlen
        {
            get
            {
                return _Waehlen ?? (_Waehlen =
                new RelayCommand(p => Waehlen()));
            }
        }
        //Funktion für den Command
        void Waehlen()
        {
            //Muss noch gemacht werden
        }
        //Abschliessen
        ICommand _Abschliessen;
        public ICommand CMDAbschliessen
        {
            get
            {
                return _Abschliessen ?? (_Abschliessen =
                new RelayCommand(p => Abschliessen()));
            }
        }
        //Funktion für den Command
        void Abschliessen()
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
                new RelayCommand(p => Speichern()));
            }
        }
        //Funktion für den Command
        void Speichern()
        {
            //Muss noch gemacht werden
        }
        //Löschen
        ICommand _Loeschen;
        public ICommand CMDLoeschen
        {
            get
            {
                return _Loeschen ?? (_Loeschen =
                new RelayCommand(p => Loeschen()));
            }
        }
        //Funktion für den Command
        void Loeschen()
        {
            //Muss noch gemacht werden
        }
    }
}
