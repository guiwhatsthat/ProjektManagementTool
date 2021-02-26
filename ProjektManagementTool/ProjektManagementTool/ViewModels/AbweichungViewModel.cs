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
    class AbweichungViewModel : BaseViewModel
    {
        #region Eigenschaften
        //ExterneResource liste
        ObservableCollection<VKostenAbweichungExterne> _externeListe;
        public ObservableCollection<VKostenAbweichungExterne> externeListe
        {
            get { return _externeListe; }
            set
            {
                _externeListe = value;
                OnPropertyChanged("externeListe");
            }
        }
        //ExterneResource index
        int _externeIndex;
        public int externeIndex
        {
            get { return _externeIndex; }
            set
            {
                _externeIndex = value;
                OnPropertyChanged("externeIndex");
                if (value != -1)
                {
                    SetValues(externeListe[externeIndex].Name, externeListe[externeIndex].Kommentar, externeListe[externeIndex].Abweichung);
                    Pkey = externeListe[externeIndex].Pkey;
                    Typ = "ExterneResource";
                    personenIndex = -1;
                }
            }
        }
        //PerseonenResource liste
        ObservableCollection<VKostenAbweichungPersonen> _personenListe;
        public ObservableCollection<VKostenAbweichungPersonen> personenListe
        {
            get { return _personenListe; }
            set
            {
                _personenListe = value;
                OnPropertyChanged("personenListe");
            }
        }
        //PerseonenResource index
        int _personenIndex;
        public int personenIndex
        {
            get { return _personenIndex; }
            set
            {
                _personenIndex = value;
                OnPropertyChanged("personenIndex");
                if (value != -1)
                {
                    SetValues(personenListe[personenIndex].Name, personenListe[personenIndex].Kommentar, personenListe[personenIndex].Abweichung);
                    Pkey = personenListe[personenIndex].Pkey;
                    Typ = "PerseonenResource";
                    externeIndex = -1;
                }
            }
        }
        //Kommentar
        string _Kommentar;
        public string Kommentar
        {
            get { return _Kommentar; }
            set
            {
                _Kommentar = value;
                OnPropertyChanged("Kommentar");
            }
        }
        //Kommentar
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
        //Abweichung
        decimal _Abweichung;
        public decimal Abweichung
        {
            get { return _Abweichung; }
            set
            {
                _Abweichung = value;
                OnPropertyChanged("Abweichung");
            }
        }

        int Pkey;
        string Typ;
        #endregion

        void SetValues (string t_Name, string t_Kommentart, decimal t_Abweichung)
        {
            Name = t_Name;
            Kommentar = t_Kommentart;
            Abweichung = t_Abweichung;
        }

        //Button Zuweisen
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
            if (string.IsNullOrEmpty(Typ))
            {
                return;
            }
            var dbHelper = new DBHelper();
            if (Typ == "ExterneResource")
            {
                string query = $"Select * from Z_ExterneResource where PKey='{Pkey}'";
                var obj = (ZExterneResource)dbHelper.RunQuery("ZExterneResource", query)[0];
                obj.Kommentar = Kommentar;
                obj.Update();
                externeListe[externeIndex].Kommentar = Kommentar;
                var temp = externeListe;
                externeListe = new ObservableCollection<VKostenAbweichungExterne>();
                externeListe = temp;
            } else
            {
                string query = $"Select * from Z_PerseonenResource where PKey='{Pkey}'";
                var obj = (ZPerseonenResource)dbHelper.RunQuery("ZPerseonenResource", query)[0];
                obj.Kommentar = Kommentar;
                obj.Update();
                personenListe[personenIndex].Kommentar = Kommentar;
                personenListe = personenListe;
                var temp = personenListe;
                personenListe = new ObservableCollection<VKostenAbweichungPersonen>();
                personenListe = temp;
            }
            System.Windows.MessageBox.Show("Abweichung gespeichert", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
