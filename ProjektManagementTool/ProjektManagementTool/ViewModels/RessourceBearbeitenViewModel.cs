using ProjektManagementTool.Helper;
using ProjektManagementTool.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;

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
        //Pkey
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

        //Pkey
        int _ZPkey;
        public int ZPkey
        {
            get { return _ZPkey; }
            set
            {
                _ZPkey = value;
                OnPropertyChanged("ZPkey");
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
        //Art
        string _Art;
        public string Art
        {
            get { return _Art; }
            set
            {
                _Art = value;
                OnPropertyChanged("Art");
            }
        }

        //soll verhindern das beim Update der Typ geändert werden kann
        public string OArt;

        //ArtFunktion
        string _ArtFunktion;
        public string ArtFunktion
        {
            get { return _ArtFunktion; }
            set
            {
                _ArtFunktion = value;
                OnPropertyChanged("ArtFunktion");
            }
        }

        //ArtFunktion
        int _Fkey_Aktivitaet;
        public int Fkey_Aktivitaet
        {
            get { return _Fkey_Aktivitaet; }
            set
            {
                _Fkey_Aktivitaet = value;
                OnPropertyChanged("Fkey_Aktivitaet");
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
        //StartDatum
        DateTime _StartDatum;
        public DateTime StartDatum
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
        //Resourcen
        ObservableCollection<PerseonenResource> _ResourcenP;
        public ObservableCollection<PerseonenResource> ResourcenP
        {
            get { return _ResourcenP; }
            set
            {
                _ResourcenP = value;
                OnPropertyChanged("ResourcenP");
            }
        }
        //ResourcenIndex
        Nullable<int> _ResourcenPIndex;
        public Nullable<int> ResourcenPIndex
        {
            get { return _ResourcenPIndex; }
            set
            {
                _ResourcenPIndex = value;

                OnPropertyChanged("ResourcenPIndex");
            }
        }
        public string ProjektStatus;
        //Resourcen
        ObservableCollection<ExterneResource> _ResourcenE;
        public ObservableCollection<ExterneResource> ResourcenE
        {
            get { return _ResourcenE; }
            set
            {
                _ResourcenE = value;
                OnPropertyChanged("ResourcenE");
            }
        }
        //ResourcenIndex
        Nullable<int> _ResourcenEIndex;
        public Nullable<int> ResourcenEIndex
        {
            get { return _ResourcenEIndex; }
            set
            {
                _ResourcenEIndex = value;
                OnPropertyChanged("ResourcenEIndex");
            }
        }
        #endregion

        #region Buttons
        //CMDSpeichern
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
            //Daten überprüfen
            if (string.IsNullOrEmpty(Name))
            {
                MessageBox.Show("Name darf nicht leer sein", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } else if (string.IsNullOrEmpty(Art))
            {
                if (Art == "ExterneKosten")
                {
                    MessageBox.Show("Es muss die Art der ExterneKosten angegeben werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                } else
                {
                    MessageBox.Show("Es muss die Funktion der PersonenKosten angegeben werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            //Check Daten
            if (!DateTime.TryParse(StartDatum.ToString(), out DateTime a))
            {
                System.Windows.MessageBox.Show("Startdatum hat das falsche Format", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } else if (StartDatum == DateTime.MinValue)
            {
                System.Windows.MessageBox.Show("Startdatum wurden nicht spezifiziert", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            if (EndDatum != null)
            {
                if (!DateTime.TryParse(EndDatum.ToString(), out DateTime b))
                {
                    System.Windows.MessageBox.Show("Enddatum hat das falsche Format", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                } else if (EndDatum == DateTime.MinValue)
                {
                    System.Windows.MessageBox.Show("Enddatum wurden nicht spezifiziert", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else if (StartDatum < EndDatum)
                {
                    System.Windows.MessageBox.Show("Startdatum muss vor dem enddatum liegen", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            //abweichung
            decimal abweichung = 0;
            if (Kosten > 0)
            {
                abweichung = KostenG - Kosten;
            }

            //objekt erstellen
            if (Art == "ExterneKosten")
            {
                if (Pkey == 0)
                {
                    //Checken ob es so eine kostenart schon gibt
                    var dbHelper = new DBHelper();
                    string query = $"Select * from ExterneResource where Name='{Name}' and Art='{ArtFunktion}' and KostenG='{KostenG}'";
                    var doppleteKosten = dbHelper.RunQuery("ExterneResource", query);
                    if (doppleteKosten.Count != 0)
                    {
                        Pkey = doppleteKosten[0].Pkey;
                        OArt = doppleteKosten[0].Art;
                    } else
                    {
                        var externeKosten = new ExterneResource(0, Name, KostenG, ArtFunktion, Fkey_Aktivitaet);
                        Pkey = externeKosten.CreateInDB();
                        OArt = Art;
                    }
                    
                    //Create link zwischen aktivitaet und Kosten
                    var link = new ZExterneResource(0,Fkey_Aktivitaet,Pkey, StartDatum, EndDatum, Kosten, abweichung, "");
                    ZPkey = link.CreateInDB();

                    //Liste in der view updaten
                    if (ParentDataContext.ListKosten == null)
                    {
                        ParentDataContext.ListKosten = new ObservableCollection<GenericKosten>();
                    }
                    var genericKosten = new GenericKosten(Art, Name, Pkey, Fkey_Aktivitaet, ZPkey);
                    ParentDataContext.ListKosten.Add(genericKosten);
                    System.Windows.MessageBox.Show("Kosten erfasst", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                } else
                {
                    //Checken ob der Typ geändert wurde
                    if (OArt != Art)
                    {
                        System.Windows.MessageBox.Show("Typ kann für Update nicht geändert werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    //Update
                    var externeKosten = new ExterneResource(Pkey, Name, KostenG, ArtFunktion, Fkey_Aktivitaet);
                    externeKosten.Update();


                    //Kommentar laden
                    var dbHelper = new DBHelper();
                    string query = $"Select * from Z_ExterneResource where Pkey='{ZPkey}'";
                    var zexterne = (ZExterneResource)dbHelper.RunQuery("ZExterneResource", query)[0];
                    var link = new ZExterneResource(zexterne.Pkey, Fkey_Aktivitaet, Pkey, StartDatum, EndDatum, Kosten, abweichung, zexterne.Kommentar);
                    link.Update();
                    System.Windows.MessageBox.Show("Aktualisiert", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                
            } else
            {
                if (Pkey == 0)
                {
                    //Checken ob es so eine kostenart schon gibt
                    var dbHelper = new DBHelper();
                    string query = $"Select * from PerseonenResource where Name='{Name}' and Funktion='{ArtFunktion}' and KostenG='{KostenG}'";
                    var doppleteKosten = dbHelper.RunQuery("PerseonenResource", query);
                    if (doppleteKosten.Count != 0)
                    {
                        Pkey = doppleteKosten[0].Pkey;
                        OArt = doppleteKosten[0].Funktion;
                    } else
                    {
                        var personenKosten = new PerseonenResource(0, Name, KostenG, ArtFunktion, Fkey_Aktivitaet);
                        Pkey = personenKosten.CreateInDB();
                        OArt = Art;
                    }
                    
                    //Create link zwischen aktivitaet und Kosten
                    var link = new ZPerseonenResource(0, Fkey_Aktivitaet, Pkey, StartDatum, EndDatum, Kosten, abweichung, "");
                    ZPkey = link.CreateInDB();

                    //Liste in der view updaten
                    if (ParentDataContext.ListKosten == null)
                    {
                        ParentDataContext.ListKosten = new ObservableCollection<GenericKosten>();
                    }
                    var genericKosten = new GenericKosten(Art, Name, Pkey, Fkey_Aktivitaet, ZPkey);
                    ParentDataContext.ListKosten.Add(genericKosten);
                    System.Windows.MessageBox.Show("Kosten erfasst", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    KostenRechner();
                    return;
                }
                else
                {
                    //Checken ob der Typ geändert wurde
                    if (OArt != Art)
                    {
                        System.Windows.MessageBox.Show("Typ kann für Update nicht geändert werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    //Update 
                    var personenKosten = new PerseonenResource(Pkey, Name, KostenG, ArtFunktion, Fkey_Aktivitaet);
                    personenKosten.Update();


                    //Kommentar laden
                    var dbHelper = new DBHelper();
                    string query = $"Select * from Z_PerseonenResource where PKey='{ZPkey}'";
                    var zperson = (ZPerseonenResource)dbHelper.RunQuery("ZPerseonenResource", query)[0];
                    var link = new ZPerseonenResource(zperson.Pkey, Fkey_Aktivitaet, Pkey, StartDatum, EndDatum, Kosten, abweichung, zperson.Kommentar);
                    link.Update();
                    KostenRechner();
                    System.Windows.MessageBox.Show("Aktualisiert", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            
        }
        //CMDWaehlen
        ICommand _WaehlenE;
        public ICommand CMDWaehlenE
        {
            get
            {
                return _WaehlenE ?? (_WaehlenE =
                new RelayCommand(p => WaehlenE()));
            }
        }
        //Funktion für den Command
        void WaehlenE()
        {
            if (ResourcenE == null || ResourcenE.Count == 0)
            {
                return;
            }

            if (ResourcenEIndex == null)
            {
                System.Windows.MessageBox.Show("Kein Eintrag ausgewählt", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var externeKosten = (ExterneResource)ResourcenE[Convert.ToInt32(ResourcenEIndex)];
            Name = externeKosten.Name;
            KostenG = externeKosten.KostenG;
            ArtFunktion = externeKosten.Art;
            Art = "ExterneKosten";

        }
        //CMDWaehlen
        ICommand _WaehlenP;
        public ICommand CMDWaehlenP
        {
            get
            {
                return _WaehlenP ?? (_WaehlenP =
                new RelayCommand(p => WaehlenP()));
            }
        }
        //Funktion für den Command
        void WaehlenP()
        {
            if (ResourcenP == null || ResourcenP.Count == 0)
            {
                return;
            }

            if (ResourcenPIndex == null)
            {
                System.Windows.MessageBox.Show("Kein Eintrag ausgewählt", "Information", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var personenKosten = (PerseonenResource)ResourcenP[Convert.ToInt32(ResourcenPIndex)];
            Name = personenKosten.Name;
            KostenG = personenKosten.KostenG;
            ArtFunktion = personenKosten.Funktion;
            Art = "PersonenKosten";
        }

        //CMDLoeschen
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
            //Darf nicht gelöscht werden wenn Projekt archiviert oder beendet ist
            if (ProjektStatus == "Archiviert" || ProjektStatus == "Abgeschlossen")
            {
                System.Windows.MessageBox.Show("Es können keine Kosten entfernt werden, wenn das Projekt abgeschlossen oder archiviert ist", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            if (Pkey != 0 && ZPkey != 0)
            {
                var dbHelper = new DBHelper();
               
                if (Art == "ExterneKosten")
                {
                    string query = $"Select * from Z_ExterneResource where Pkey='{ZPkey}'";
                    var zexterne = (ZExterneResource)dbHelper.RunQuery("ZExterneResource", query)[0];
                    var link = new ZExterneResource(zexterne.Pkey, Fkey_Aktivitaet, Pkey, StartDatum, EndDatum, Kosten, 0, zexterne.Kommentar);
                    link.Remove();
                } else
                {
                    string query = $"Select * from Z_PerseonenResource where PKey='{ZPkey}'";
                    var zperson = (ZPerseonenResource)dbHelper.RunQuery("ZPerseonenResource", query)[0];
                    var link = new ZPerseonenResource(zperson.Pkey, Fkey_Aktivitaet, Pkey, StartDatum, EndDatum, Kosten, 0, zperson.Kommentar);
                    link.Remove();
                }

                //Entfernen vom liste in parentview
                int index = 0;
                for (int i = 0; i < ParentDataContext.ListKosten.Count; i++)
                {
                    if (ParentDataContext.ListKosten[i].Pkey == ZPkey)
                    {
                        index = i;
                        i = ParentDataContext.ListKosten.Count;
                    }
                }
                System.Windows.MessageBox.Show("Entfernt", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                ParentDataContext.ListKosten.RemoveAt(index);
            }
        }
        #endregion

        void KostenRechner()
        {
            var dbHelper = new DBHelper();
            string query = $"Select * from Z_PerseonenResource where FKey_Aktiviteat='{Fkey_Aktivitaet}'";
            var listP = dbHelper.RunQuery("ZPerseonenResource", query);
            decimal pKosten = 0;
            foreach (var p in listP)
            {
                pKosten += p.Kosten;
            }

            query = $"Select * from Z_ExterneResource where FKey_Aktiviteat='{Fkey_Aktivitaet}'";
            var listE = dbHelper.RunQuery("ZExterneResource", query);
            decimal eKosten = 0;
            foreach (var e in listE)
            {
                eKosten += e.Kosten;
            }
            ParentDataContext.PersonenKosten = pKosten;
            ParentDataContext.ExterneKosten = eKosten;
            ParentDataContext.Speichern();
        }
    }
}
