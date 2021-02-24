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
        //Button Mitarbeiter zuweisen
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
            } else if (ResourcenIndex != 0)
            {
                MessageBox.Show("Es muss ein typ ausgewählt werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                else if (StartDatum > EndDatum)
                {
                    System.Windows.MessageBox.Show("Startdatum muss vor dem enddatum liegen", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }


            //objekt erstellen
            if (Art == "ExterneKosten")
            {
                //abweichung
                decimal abweichung = 0;
                if (Kosten > 0)
                {
                    abweichung = KostenG - Kosten;
                }

                if (Pkey == 0)
                {
                    var externeKosten = new ExterneResource(0, Name, KostenG, Kosten, abweichung,"",Art, Fkey_Aktivitaet);
                    Pkey = externeKosten.CreateInDB();
                    //Create link zwischen aktivitaet und Kosten
                    var link = new ZExterneResource(0,Fkey_Aktivitaet,Pkey, StartDatum, EndDatum);
                    int ZPkey = link.CreateInDB();

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
                    //Update 
                    //Kommentar laden und unverändert übergeben beim update
                }
                
            } else
            {
                //abweichung
                decimal abweichung = 0;
                if (Kosten > 0)
                {
                    abweichung = KostenG - Kosten;
                }

                if (Pkey == 0)
                {
                    var personenKosten = new PerseonenResource(0, Name, KostenG, Kosten, abweichung, "", Art, Fkey_Aktivitaet);
                    Pkey = personenKosten.CreateInDB();
                    //Create link zwischen aktivitaet und Kosten
                    var link = new ZPerseonenResource(0, Fkey_Aktivitaet, Pkey, StartDatum, EndDatum);
                    int ZPkey = link.CreateInDB();

                    //Liste in der view updaten
                    if (ParentDataContext.ListKosten == null)
                    {
                        ParentDataContext.ListKosten = new ObservableCollection<GenericKosten>();
                    }
                    var genericKosten = new GenericKosten(Art, Name, Pkey, Fkey_Aktivitaet, ZPkey);
                    ParentDataContext.ListKosten.Add(genericKosten);
                    System.Windows.MessageBox.Show("Kosten erfasst", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    //Update 
                    //Kommentar laden und unverändert übergeben beim update
                }
            }
            
        }
        //CMDLöschen
        //CMDWaehlen
        #endregion
    }
}
