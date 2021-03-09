using ProjektManagementTool.Helper;
using ProjektManagementTool.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ProjektManagementTool.ViewModels
{
    class MitarbeiterViewModel : BaseViewModel
    {
        #region Eigenschaften
        string _Vorname;
        public string Vorname
        {
            get { return _Vorname; }
            set
            {
                _Vorname = value;
                OnPropertyChanged("Vorname");
            }
        }
        string _Nachname;
        public string Nachname
        {
            get { return _Nachname; }
            set
            {
                _Nachname = value;
                OnPropertyChanged("Nachname");
            }
        }
        string _Funktion;
        public string Funktion
        {
            get { return _Funktion; }
            set
            {
                _Funktion = value;
                OnPropertyChanged("Funktion");
            }
        }
        string _Abteilung;
        public string Abteilung
        {
            get { return _Abteilung; }
            set
            {
                _Abteilung = value;
                OnPropertyChanged("Abteilung");
            }
        }
        int _Pensum;
        public int Pensum
        {
            get { return _Pensum; }
            set
            {
                _Pensum = value;
                OnPropertyChanged("Pensum");
            }
        }
        #endregion
        #region Buttons
        ICommand _Hinzufuegen;
        public ICommand CMDHinzufuegen
        {
            get
            {
                return _Hinzufuegen ?? (_Hinzufuegen =
                new RelayCommand(p => Hinzufuegen()));
            }
        }
        //Funktion für den Command
        void Hinzufuegen()
        {
            if (string.IsNullOrEmpty(Vorname) || string.IsNullOrEmpty(Nachname) || string.IsNullOrEmpty(Funktion) || string.IsNullOrEmpty(Pensum.ToString()) || string.IsNullOrEmpty(Abteilung))
            {
                MessageBox.Show("Nicht alle Felder sind ausgefüllt", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (Pensum > 100)
            {
                MessageBox.Show("Pensu kann nicht mehr als 100% sein", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            } else if (Pensum == 0)
            {
                MessageBox.Show("Pensu kann nicht 0% sein", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var mitarbeiter = new Mitarbeiter(0,Vorname,Nachname,Funktion, Pensum, Abteilung);
            mitarbeiter.CreateInDB();
            MessageBox.Show("Mitarbeiter wurde erstellt", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
    }
}
