using ProjektManagementTool.Helper;
using ProjektManagementTool.Models;
using ProjektManagementTool.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

namespace ProjektManagementTool.ViewModels
{
    class AktiviteatBearbeitenViewModel : BaseViewModel
    {
        #region Eigentschaften Bindings
        //Parent DataContext
        ProjektBearbeitenViewModel _ProjektBearbeitenViewModel;
        public ProjektBearbeitenViewModel ParentDataContext
        {
            get { return _ProjektBearbeitenViewModel; }
            set
            {
                _ProjektBearbeitenViewModel = value;
                OnPropertyChanged();
            }
        }
        //Liste mit allen Phasen (ist dynamisch damit ich die Daten vom Projekt einfach übergegebn kann)
        ObservableCollection<dynamic> _ListPhasen;
        public ObservableCollection<dynamic> ListPhasen
        {
            get { return _ListPhasen; }
            set
            {
                _ListPhasen = value;
                OnPropertyChanged("ListPhasen");
            }
        }
        //Index wert für Phasenliste
        int _PhasenIndex;
        public int PhasenIndex
        {
            get { return _PhasenIndex; }
            set
            {
                _PhasenIndex = value;
                OnPropertyChanged("PhasenIndex");
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
        int _MitarbeiterPkey;
        public int MitarbeiterPkey
        {
            get { return _MitarbeiterPkey; }
            set
            {
                _MitarbeiterPkey = value;
                OnPropertyChanged("MitarbeiterPkey");
            }
        }
        int _PhasePkey;
        public int PhasePkey
        {
            get { return _PhasePkey; }
            set
            {
                _PhasePkey = value;
                OnPropertyChanged("PhasePkey");
            }
        }
        //Startdatum
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
        //Startdatum Geplant
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
        //EndDatum Geplant
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
        //Name der Aktivität
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
        //ExterneKosten Geplant
        decimal _ExterneKostenG;
        public decimal ExterneKostenG
        {
            get { return _ExterneKostenG; }
            set
            {
                _ExterneKostenG = value;
                OnPropertyChanged("ExterneKostenG");
            }
        }
        //PersonenKosten Geplant
        decimal _PersonenKostenG;
        public decimal PersonenKostenG
        {
            get { return _PersonenKostenG; }
            set
            {
                _PersonenKostenG = value;
                OnPropertyChanged("PersonenKostenG");
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

        //Kosten liste ist dynamisch da zwei Datentypen abegfüllt werden müssen
        ObservableCollection<GenericKosten> _ListKosten;
        public ObservableCollection<GenericKosten> ListKosten
        {
            get { return _ListKosten; }
            set
            {
                _ListKosten = value;
                OnPropertyChanged("ListKosten");
            }
        }
        //Index der Liste Kosten
        int _KostenIndex;
        public int KostenIndex
        {
            get { return _KostenIndex; }
            set
            {
                _KostenIndex = value;
                OnPropertyChanged("KostenIndex");
            }
        }
        //ExterneKosten
        decimal _ExterneKosten;
        public decimal ExterneKosten
        {
            get { return _ExterneKosten; }
            set
            {
                _ExterneKosten = value;
                OnPropertyChanged("ExterneKosten");
            }
        }
        //PersonenKosten
        decimal _PersonenKosten;
        public decimal PersonenKosten
        {
            get { return _PersonenKosten; }
            set
            {
                _PersonenKosten = value;
                OnPropertyChanged("PersonenKosten");
            }
        }
        //Name der Phase
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

        //Mitarbeitername
        string _MitarbeiterName;
        public string MitarbeiterName
        {
            get { return _MitarbeiterName; }
            set
            {
                _MitarbeiterName = value;
                OnPropertyChanged("MitarbeiterName");
            }
        }
        #endregion

        #region Buttons
        //Button Aktivität starten
        ICommand _Starten;
        public ICommand CMDStarten
        {
            get
            {
                return _Starten ?? (_Starten =
                new RelayCommand(p => Starten()));
            }
        }
        //Funktion für den Command
        void Starten()
        {
            //Checken on Enddatum Geplant gsetzte ist
            if (EndDatumG == null || EndDatum == DateTime.MinValue)
            {
                System.Windows.MessageBox.Show("Aktivität kann nicht gestartet werden. Es muss noch das geplante Enddatum eingetragen werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            StartDatum = DateTime.Now;
            Speichern();
        }
        //Button Aktivität beenden
        ICommand _Beenden;
        public ICommand CMDBeenden
        {
            get
            {
                return _Beenden ?? (_Beenden =
                new RelayCommand(p => Beenden()));
            }
        }
        //Funktion für den Command
        void Beenden()
        {
            if (StartDatum == null || EndDatum != null)
            {
                return;
            }
            //Checken ob Kosten abgeschlossen sind
            var dbHelper = new DBHelper();
            string query = $"Select * from Z_ExterneResource where FKey_Aktiviteat='{Pkey}' and EndDatum is null";
            var listeE = dbHelper.RunQuery("ZExterneResource", query);
            query = $"Select * from Z_PerseonenResource where FKey_Aktiviteat='{Pkey}' and EndDatum is null";
            var listeP = dbHelper.RunQuery("ZPerseonenResource", query);
            if (listeP.Count > 0 || listeE.Count > 0)
            {
                System.Windows.MessageBox.Show("Aktivität kann nicht abgeschlossen werden. Es sind noch nicht alle Kosten abgeschlossen", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

        }
        //Button Aktivität speichern
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
            }
            else if (PhaseName == null)
            {
                MessageBox.Show("Es muss eine Phase gewählt werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (MitarbeiterName == null)
            {
                MessageBox.Show("Es muss ein Mitarbeiter zugewiesen sein", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (EndDatumG < StartDatumG)
            {
                System.Windows.MessageBox.Show("Startdatum muss vor dem Enddatum liegen", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!DateTime.TryParse(StartDatumG.ToString(), out DateTime a))
            {
                System.Windows.MessageBox.Show("Startdatum (Geplant) hat das falsche Format", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }else if (!DateTime.TryParse(EndDatumG.ToString(), out DateTime b))
            {
                System.Windows.MessageBox.Show("Enddatum (Geplant) hat das falsche Format", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else if (StartDatumG == DateTime.MinValue)
            {
                System.Windows.MessageBox.Show("Startdatum (Geplant) wurden nicht spezifiziert", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } else if (EndDatumG == DateTime.MinValue)
            {
                System.Windows.MessageBox.Show("Enddatum (Geplant) wurden nicht spezifiziert", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (Pkey == 0)
            {
                //Checken ob Phase abgeschlossen ist, wenn ja kann die Aktiität nicht zugewiesen werden
                foreach (var phase in ListPhasen)
                {
                    if (phase.Pkey == PhasePkey)
                    {
                        if (phase.EndDatum != null)
                        {
                            System.Windows.MessageBox.Show("Aktivität kann nicht einer Abgeschlossenen Phase zugewiesen werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                }
                
                //dieser test muss nur beim eröffnen gemacht werden
                if (DateTime.Parse(DateTime.Now.ToString("dd.MM.yyyy")) > StartDatumG)
                {
                    System.Windows.MessageBox.Show("Datum (Geplant) muss heute oder in der Zukunft sein", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var aktivitaet = new Aktivitaet(0, Name, StartDatumG, EndDatumG, StartDatum, EndDatum, ExterneKostenG, PersonenKostenG, ExterneKosten, PersonenKosten, Fortschritt, MitarbeiterPkey, PhasePkey, Ablage);
                Pkey = aktivitaet.CreateInDB();
                aktivitaet.Pkey = Pkey;
                if (Pkey == -1)
                {
                    System.Windows.MessageBox.Show("Fehler! beim schreiben in die DB", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                } else
                {
                    System.Windows.MessageBox.Show("Aktivität erfasst", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                if (ParentDataContext.ListAktivitaet == null)
                {
                    ParentDataContext.ListAktivitaet = new ObservableCollection<Aktivitaet>();
                }
                ParentDataContext.ListAktivitaet.Add(aktivitaet);
            }
            else
            {
                var aktivitaet = new Aktivitaet(Pkey, Name, StartDatumG, EndDatumG, StartDatum, EndDatum, ExterneKostenG, PersonenKostenG, ExterneKosten, PersonenKosten, Fortschritt, MitarbeiterPkey, PhasePkey, Ablage);
                aktivitaet.Update();
                System.Windows.MessageBox.Show("Aktivität aktualisiert", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                
                int aindex = 0;
                for (int i = 0; i < ParentDataContext.ListAktivitaet.Count; i++)
                {
                    if (Pkey == ParentDataContext.ListAktivitaet[i].Pkey)
                    {
                        aindex = i;
                        i = ParentDataContext.ListAktivitaet.Count;
                    }
                }
                ParentDataContext.ListAktivitaet.RemoveAt(aindex);
                ParentDataContext.ListAktivitaet.Add(aktivitaet);
            }
        }
        //Button Aktivität löschen
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
            if (Pkey != 0 || Pkey != -1 )
            {
                var aktivitaet = new Aktivitaet(Pkey, Name, StartDatumG, EndDatumG, StartDatum, EndDatum, ExterneKostenG, PersonenKostenG, ExterneKosten, PersonenKosten, Fortschritt, MitarbeiterPkey, PhasePkey, Ablage);
                aktivitaet.Remove();
                System.Windows.MessageBox.Show("Aktivität gelöscht", "Warnung", MessageBoxButton.OK, MessageBoxImage.Information);
                int aindex = 0;
                for (int i = 0; i < ParentDataContext.ListAktivitaet.Count; i++)
                {
                    if (Pkey == ParentDataContext.ListAktivitaet[i].Pkey)
                    {
                        aindex = i;
                        i = ParentDataContext.ListAktivitaet.Count;
                    }
                }
                ParentDataContext.ListAktivitaet.RemoveAt(aindex);
            }
        }
        //Button Mitarbeiter zuweisen
        ICommand _Mitarbeiterzuweisen;
        public ICommand CMDMitarbeiterzuweisen
        {
            get
            {
                return _Mitarbeiterzuweisen ?? (_Mitarbeiterzuweisen =
                new RelayCommand(p => Mitarbeiterzuweisen()));
            }
        }
        //Funktion für den Command
        void Mitarbeiterzuweisen()
        {
            var hinzufuegenView = new HinzufuegenView();
            HinzufuegenViewModel context = (HinzufuegenViewModel)hinzufuegenView.DataContext;
            context.Header = "Mitarbeiter";
            var dbHelper = new DBHelper();
            context.ListObj = dbHelper.RunQuery("Mitarbeiter", "Select * from Mitarbeiter");
            context.ParentDataContextAktiviteat = this;
            hinzufuegenView.Show();
        }
        //Button Ablage zuweisen
        ICommand _Ablagezuweisen;
        public ICommand CMDAblagezuweisen
        {
            get
            {
                return _Ablagezuweisen ?? (_Ablagezuweisen =
                new RelayCommand(p => Ablagezuweisen()));
            }
        }
        //Funktion für den Command
        void Ablagezuweisen()
        {
            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                Ablage = fbd.SelectedPath;

            }
        }
        //Button Kosten erfassen
        ICommand _Kostenerfassen;
        public ICommand CMDKostenerfassen
        {
            get
            {
                return _Kostenerfassen ?? (_Kostenerfassen =
                new RelayCommand(p => Kostenerfassen()));
            }
        }
        //Funktion für den Command
        void Kostenerfassen()
        {
            var ressourceBearbeitenView = new RessourceBearbeiten();
            var context = (RessourceBearbeitenViewModel)ressourceBearbeitenView.DataContext;
            context.Fkey_Aktivitaet = Pkey;
            context.ParentDataContext = this;

            //Liste mit allen verfübaren kosten
            var dbHelper = new DBHelper();
            string query = $"Select * from ExterneResource";
            List<dynamic> listexterne = dbHelper.RunQuery("ExterneResource", query);
            var olistexterne = new ObservableCollection<ExterneResource>();
            query = $"Select * from PerseonenResource";
            List<dynamic> listpersonen = dbHelper.RunQuery("PerseonenResource", query);
            var olistpersonen = new ObservableCollection<PerseonenResource>();

            foreach (var externe in listexterne)
            {
                olistexterne.Add((ExterneResource)externe);
            }
            foreach (var person in listpersonen)
            {
                olistpersonen.Add((PerseonenResource)person);
            }
            context.ResourcenE = olistexterne;
            context.ResourcenP = olistpersonen;
            ressourceBearbeitenView.Show();
        }
        //Button Kosten bearbeiten
        ICommand _Kostenbearbeiten;
        public ICommand CMDKostenbearbeiten
        {
            get
            {
                return _Kostenbearbeiten ?? (_Kostenbearbeiten =
                new RelayCommand(p => Kostenbearbeiten()));
            }
        }
        //Funktion für den Command
        void Kostenbearbeiten()
        {
            var dbHelper = new DBHelper();
            var ressourceBearbeitenView = new RessourceBearbeiten();
            var context = (RessourceBearbeitenViewModel)ressourceBearbeitenView.DataContext;
            context.Fkey_Aktivitaet = Pkey;
            context.ParentDataContext = this;

            //ausgewählte Kosten
            var kosten = (GenericKosten)ListKosten[KostenIndex];
            if (kosten.Typ == "ExterneKosten")
            {
                string query = $"Select * from ExterneResource where PKey='{kosten.Pkey}'";
                var externeResource = (ExterneResource)dbHelper.RunQuery("ExterneResource", query)[0];
                context.Pkey = externeResource.Pkey;
                context.KostenG = externeResource.KostenG;
                context.Name = externeResource.Name;
                context.ArtFunktion = externeResource.Art;
                context.ZPkey = kosten.ZPkey;
                query = $"Select * from Z_ExterneResource where PKey='{kosten.ZPkey}'";
                var zexterneResource = (ZExterneResource)dbHelper.RunQuery("ZExterneResource", query)[0];
                context.StartDatum = zexterneResource.StartDatum;
                context.EndDatum = zexterneResource.EndDatum;
                context.Kosten = Convert.ToDecimal(zexterneResource.Kosten);
                context.Art = "ExterneKosten";
            } else
            {
                string query = $"Select * from PerseonenResource where PKey='{kosten.Pkey}'";
                var personenRsourcen = (PerseonenResource)dbHelper.RunQuery("PerseonenResource", query)[0];
                context.Pkey = personenRsourcen.Pkey;
                context.KostenG = personenRsourcen.KostenG;
                context.Name = personenRsourcen.Name;
                context.ArtFunktion = personenRsourcen.Funktion;
                context.ZPkey = kosten.ZPkey;
                query = $"Select * from Z_PerseonenResource where PKey='{kosten.ZPkey}'";
                var zpersonenResource = (ZPerseonenResource)dbHelper.RunQuery("ZPerseonenResource", query)[0];
                context.StartDatum = zpersonenResource.StartDatum;
                context.EndDatum = zpersonenResource.EndDatum;
                context.Kosten = Convert.ToDecimal(zpersonenResource.Kosten);
                context.Art = "PersonenKosten";
            }

            context.OArt = context.Art;

            ressourceBearbeitenView.Show();
        }
        //Button Phase zuweisen
        ICommand _Phasezuweisen;
        public ICommand CMDPhasezuweisen
        {
            get
            {
                return _Phasezuweisen ?? (_Phasezuweisen =
                new RelayCommand(p => Phasezuweisen()));
            }
        }
        //Funktion für den Command
        void Phasezuweisen()
        {
            if (ListPhasen == null || ListPhasen.Count == 0)
            {
                return;
            }
            PhaseName = ListPhasen[PhasenIndex].Name;
            PhasePkey = ListPhasen[PhasenIndex].Pkey;
        }
        //Button Abweichung
        ICommand _Abweichung;
        public ICommand CMDAbweichung
        {
            get
            {
                return _Abweichung ?? (_Abweichung =
                new RelayCommand(p => Abweichung()));
            }
        }
        //Funktion für den Command
        void Abweichung()
        {
            var dbHelper = new DBHelper();
            var abweichungView = new AbweichungView();
            var context = (AbweichungViewModel)abweichungView.DataContext;
            string query = $"Select * from VKostenAbweichungExterne where Fkey_Aktivitaet='{Pkey}' and Kosten!=0";
            var list = dbHelper.RunQuery("VKostenAbweichungExterne", query);
            var listexterne = new ObservableCollection<VKostenAbweichungExterne>();
            foreach (var entry in list)
            {
                listexterne.Add((VKostenAbweichungExterne)entry);
            }
            context.externeListe = listexterne;
            context.externeIndex = -1;

            query = $"Select * from VKostenAbweichungPersonen where FKey_Aktiviteat='{Pkey}' and Kosten!=0";
            list = dbHelper.RunQuery("VKostenAbweichungPersonen", query);
            var listpersonen = new ObservableCollection<VKostenAbweichungPersonen>();
            foreach (var entry in list)
            {
                listpersonen.Add((VKostenAbweichungPersonen)entry);
            }
            context.personenListe = listpersonen;
            context.personenIndex = -1;

            //Show
            abweichungView.Show();
        }
        #endregion
    }
}
