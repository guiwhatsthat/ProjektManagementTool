using ProjektManagementTool.Helper;
using ProjektManagementTool.Models;
using ProjektManagementTool.Views;
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
        //Status EndDatumG
        DateTime _EndDatumG;
        public DateTime EndDatumG
        {
            get { return _EndDatumG; }
            set
            {
                _EndDatumG = value;
                OnPropertyChanged("EndDatumG");
            }
        }
        //Status FreigabeDatum
        Nullable<DateTime> _FreigabeDatum;
        public Nullable<DateTime> FreigabeDatum
        {
            get { return _FreigabeDatum; }
            set
            {
                _FreigabeDatum = value;
                OnPropertyChanged("FreigabeDatum");
            }
        }
        //Status ReviewDatum
        Nullable<DateTime> _ReviewDatum;
        public Nullable<DateTime> ReviewDatum
        {
            get { return _ReviewDatum; }
            set
            {
                _ReviewDatum = value;
                OnPropertyChanged("ReviewDatum");
            }
        }
        //Status ReviewDatumG
        DateTime _ReviewDatumG;
        public DateTime ReviewDatumG
        {
            get { return _ReviewDatumG; }
            set
            {
                _ReviewDatumG = value;
                OnPropertyChanged("ReviewDatumG");
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
        //Status EndDatum
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
        //Visum
        string _Visum;
        public string Visum
        {
            get { return _Visum; }
            set
            {
                _Visum = value;
                OnPropertyChanged("Visum");
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
            //Checken ob das Projekt gestartet ist
            if (FreigabeDatum == null)
            {
                System.Windows.MessageBox.Show("Phase kann erst gestartet werden, sie muss zuerst freigegeben werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (ProjektStatus != "In Arbeit") 
            {
                System.Windows.MessageBox.Show("Phase kann erst gestartet werden wenn das Projekt gestartet wurde", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (Status == "Eröffnet")
            {
                System.Windows.MessageBox.Show("Bitte zuerst die Phasen daten abfüllen und speichern. Aktuell sind noch die generierten Daten eingetragen", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            } else if (Status != "Freigegeben")
            {
                System.Windows.MessageBox.Show("Phase kann erst gestartet werden, wenn sie friegegeben wurde", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Checken ob das Enddatum geplant wurde
            if (EndDatumG == null || EndDatum == DateTime.MinValue)
            {
                System.Windows.MessageBox.Show("Phase kann nicht gestartet werden. Es muss noch das geplante Enddatum eingetragen werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //Startdatumsetzen
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
            if (Status == "Eröffnet")
            {
                System.Windows.MessageBox.Show("Bitte zuerst die Phaen daten abfüllen und speichern. Aktuell sind noch die generierten Daten eingetragen", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else if (ProjektStatus != "In Arbeit")
            {
                System.Windows.MessageBox.Show("Phase kann erst gestartet werden, wenn das Projekt gestartet wurde", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //Checken ob alle Aktivitäten abgeschlossen sind
            var dbHelper = new DBHelper();
            string query = $"Select * from Aktivitaet where FKey_PhaseID='{Pkey}' and EndDatum is null";
            List<dynamic> list = dbHelper.RunQuery("Aktivitaet", query);
            if (list.Count > 0)
            {
                System.Windows.MessageBox.Show("Es sind nicht alle Aktivitäten dieser Phase abgeschlossen, kann nicht beendet werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //Checken ob alle Meilensteine abgeschlossen sind
            query = $"Select * from Meilenstein where FKey_PhaseID='{Pkey}' and Datum is null and Name!='{Name}_End'";
            List<dynamic> listM = dbHelper.RunQuery("Meilenstein", query);
            if (list.Count > 0)
            {
                System.Windows.MessageBox.Show("Es sind nicht alle Meilensteine dieser Phase abgeschlossen, kann nicht beendet werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //End Meilenstein abschlissen
            query = $"Select * from Meilenstein where FKey_PhaseID='{Pkey}' and Name='{Name}_Ende'";
            List<dynamic> endMeilenstein = dbHelper.RunQuery("Meilenstein", query);
            if (endMeilenstein.Count > 0)
            {
                var meilenstein = (Meilenstein)endMeilenstein[0];
                meilenstein.Datum = DateTime.Now;
                meilenstein.Update();

                //Meilensteine liste aktualisieren
                int listIndex = 0;
                for (int i = 0; i < ParentContext.ListMeilensteine.Count; i++)
                {
                    if (ParentContext.ListMeilensteine[i].Pkey == meilenstein.Pkey)
                    {
                        listIndex = i;
                        i = ParentContext.ListMeilensteine.Count;
                    }
                }
                var temp = ParentContext.ListMeilensteine;
                temp.RemoveAt(listIndex);
                temp.Add(meilenstein);
                ParentContext.ListMeilensteine = temp;
            }

            //Enddatum setzen
            EndDatum = DateTime.Now;
            Status = "Abgeschlossen";
            Fortschritt = 100;
            Phasespeichern();
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
        public void Phasespeichern()
        {
            //RevieDatumG checken
            if (ReviewDatumG == null || ReviewDatumG == DateTime.MinValue)
            {
                System.Windows.MessageBox.Show("Geplantes Reviewdatum hat ein falsches Format", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } else if (DateTime.Parse(EndDatumG.ToString("dd.MM.yyyy")) > DateTime.Parse(ReviewDatumG.ToString("dd.MM.yyyy")))
            {
                System.Windows.MessageBox.Show("Geplantes Reviewdatum muss nach abschluss der Phase sein", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dbHelper = new DBHelper();
            if (Status == "Eröffnet")
            {
                Status = "Erfasst";
                //Meilenstein hinzufügen
                string meilensteinName = Name + "_Ende";
                var objMeilenstein = new Meilenstein(0,meilensteinName,EndDatumG,null,Pkey);
                int meilensteinPkey =objMeilenstein.CreateInDB();
                objMeilenstein.Pkey = meilensteinPkey;

                if (ParentContext.ListMeilensteine == null)
                {
                    ParentContext.ListMeilensteine = new ObservableCollection<Meilenstein>();
                }
                ParentContext.ListMeilensteine.Add(objMeilenstein);
            }
            var objPhase = new Phase(Pkey, Name, Status, Fortschritt, StartDatumG, EndDatumG, StartDatum, EndDatum, FKey_PhaseTemplateID, FKey_ProjektID, Visum, FreigabeDatum, ReviewDatum, ReviewDatumG);
            objPhase.Update();

            string query = $"Select * from Phase where FKey_ProjektID='{ParentContext.Pkey}'";
            ParentContext.PhasenListe = new ObservableCollection<dynamic>(dbHelper.RunQuery("Phase", query));
        }

        //Freigeben
        ICommand _Freigeben;
        public ICommand CMDFreigeben
        {
            get
            {
                return _Freigeben ?? (_Freigeben =
                new RelayCommand(p => Freigeben()));
            }
        }
        //Funktion für den Command
        void Freigeben()
        {
            if (FreigabeDatum != null)
            {
                return;
            }
            //Inputbox
            Phasespeichern();
            var visumView = new VisumView();
            var context = (VisumViewModel)visumView.DataContext;
            context.ParentConext = this;
            visumView.Show();

        }

        //Freigeben
        ICommand _Review;
        public ICommand CMDReview
        {
            get
            {
                return _Review ?? (_Review =
                new RelayCommand(p => Review()));
            }
        }
        //Funktion für den Command
        void Review()
        {
            if (Status != "Abgeschlossen")
            {
                System.Windows.MessageBox.Show("Review kann erst gemacht werden die Phase abgeschlossen wurde", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Status = "Review durchgeführt";
            ReviewDatum = DateTime.Now;
            Phasespeichern();
            System.Windows.MessageBox.Show("Review abgeschlossen", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
