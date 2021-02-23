using ProjektManagementTool.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace ProjektManagementTool.ViewModels
{
    class RessourceBearbeitenViewModel : BaseViewModel
    {

        public RessourceBearbeitenViewModel() {
            var ArtenListe = new List<string>();
            ArtenListe.Add("ExterneKosten");
            ArtenListe.Add("PersonenKosten");
            Arten = ArtenListe;
        }
        #region Eigenschaften
        //Parent DataContext
        AktiviteatBearbeitenViewModel _AktiviteatBearbeitenViewModel;
        public AktiviteatBearbeitenViewModel ParentDataContext
        {
            get { return _AktiviteatBearbeitenViewModel; }
            set
            {
                _AktiviteatBearbeitenViewModel = value;
                OnPropertyChanged("ParentDataContext");
            }
        }
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
        //Arten
        List<String> _Arten;
        public List<String> Arten
        {
            get { return _Arten; }
            set
            {
                _Arten = value;
                OnPropertyChanged("Arten");
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
        //StartDaum
        DateTime _StartDaum;
        public DateTime StartDaum
        {
            get { return _StartDaum; }
            set
            {
                _StartDaum = value;
                OnPropertyChanged("StartDaum");
            }
        }
        //EndDatum
        Nullable<DateTime> _EndDatum;
        public Nullable<DateTime> EndDatum
        {
            get { return _EndDatum; }
            set
            {
                _EndDatum = value;
                OnPropertyChanged("EndDatum");
            }
        }
        //Resourcen
        ObservableCollection<GenericKosten> _Resourcen;
        public ObservableCollection<GenericKosten> Resourcen
        {
            get { return _Resourcen; }
            set
            {
                _Resourcen = value;
                OnPropertyChanged("Resourcen");
            }
        }
        //ResourcenIndex
        int _ResourcenIndex;
        public int ResourcenIndex
        {
            get { return _ResourcenIndex; }
            set
            {
                _ResourcenIndex = value;
                OnPropertyChanged("ResourcenIndex");
            }
        }
        #endregion

        #region Buttons
        //CMDSpeichern
        //CMDLöschen
        //CMDWaehlen
        #endregion
    }
}
