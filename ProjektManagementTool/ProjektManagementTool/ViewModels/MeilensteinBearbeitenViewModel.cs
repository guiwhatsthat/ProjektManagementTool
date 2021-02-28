using ProjektManagementTool.Helper;
using ProjektManagementTool.Models;
using ProjektManagementTool.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Linq;

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
        Nullable<DateTime> _Datum;
        public Nullable<DateTime> Datum
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
            PhaseName = Phasen[Index].Name;
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
            if (Datum != null)
            {
                return;
            }
            //Checken ob Projekt in Arbeit ist
            if (ParentContext.Status != "In Arbeit")
            {
                System.Windows.MessageBox.Show("Kann nicht abgeschlossen werden weil das Projekt nicht in Arbeit ist.", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Datum = DateTime.Now;
            Speichern();

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
            //check ob Daten leer sind
            if (string.IsNullOrEmpty(Name))
            {
                MessageBox.Show("Name darf nicht leer sein", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } else if (PhaseName == null)
            {
                MessageBox.Show("Es muss eine Phase gewählt werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!DateTime.TryParse(DatumG.ToString(), out DateTime a))
            {
                System.Windows.MessageBox.Show("Datum (Geplant) hat das falsche Format", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } else if (DatumG == DateTime.MinValue )
            {
                System.Windows.MessageBox.Show("Datum (Geplant) wurden nicht spezifiziert", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (Pkey == 0)
            {
                //dieser test muss nur beim eröffnen gemacht werden
                if (DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")) > DatumG)
                {
                    System.Windows.MessageBox.Show("Datum (Geplant) muss heute oder in der Zukunft sein", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var meilenstein = new Meilenstein(0,Name,DatumG,Datum,Phasen[Index].Pkey);
                Pkey = meilenstein.CreateInDB();
                meilenstein.Pkey = Pkey;
                MessageBox.Show($"Phase '{Name}' hinzugefügt", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                ParentContext.ListMeilensteine.Add(meilenstein);

            } else
            {
                var dbHelper = new DBHelper();
                string query = $"Select * from Phase where Name='{PhaseName}'";
                var dbPhase = dbHelper.RunQuery("Phase", query);
                var meilenstein = new Meilenstein(Pkey, Name, DatumG, Datum, dbPhase[0].Pkey);
                meilenstein.Update();
                MessageBox.Show($"Phase '{Name}' aktualisiert", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                int index = 0;
                for (int i = 0; i < ParentContext.ListMeilensteine.Count; i++)
                {
                    var obj = (Meilenstein)ParentContext.ListMeilensteine[i];
                    if (obj.Pkey == Pkey)
                    {
                        index = i;
                        i = ParentContext.ListMeilensteine.Count;
                    }
                }
                ParentContext.ListMeilensteine.RemoveAt(index);
                ParentContext.ListMeilensteine.Add(meilenstein);
            }
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
            if (Name.Contains("_End"))
            {
                MessageBox.Show($"Phase '{Name}' kann nicht entfernt werden. Jede Phase braucht ein Meilenstein am Ende", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var dbHelper = new DBHelper();
            string query = $"Select * from Phase where Name='{PhaseName}'";
            var dbPhase = dbHelper.RunQuery("Phase", query);
            var meilenstein = new Meilenstein(Pkey, Name, DatumG, Datum, dbPhase[0].Pkey);
            meilenstein.Remove();
            MessageBox.Show($"Phase '{Name}' enntfernt", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            int index = 0;
            for (int i = 0; i < ParentContext.ListMeilensteine.Count;i++)
            {
                var obj = (Meilenstein)ParentContext.ListMeilensteine[i];
                if (obj.Pkey == Pkey)
                {
                    index = i;
                    i = ParentContext.ListMeilensteine.Count;
                }
            }
            ParentContext.ListMeilensteine.RemoveAt(index);
        }
    }
}
