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
            if (string.IsNullOrEmpty(Vorname) || string.IsNullOrEmpty(Nachname) || string.IsNullOrEmpty(Funktion))
            {
                MessageBox.Show("Nicht alle Felder sind ausgefüllt", "Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var mitarbeiter = new Mitarbeiter(0,Vorname,Nachname,Funktion);
            mitarbeiter.CreateInDB();
            MessageBox.Show("Mitarbeiter wurde erstellt", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        #endregion
    }
}
