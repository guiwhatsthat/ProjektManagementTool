using ProjektManagementTool.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ProjektManagementTool.ViewModels
{
    class VisumViewModel : BaseViewModel
    {
        //ParentConext
        PhasenDatenViewModel _ParentConext;
        public PhasenDatenViewModel ParentConext
        {
            get { return _ParentConext; }
            set
            {
                _ParentConext = value;
                OnPropertyChanged("ParentConext");
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

        //Visum hinzufügen
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
            if (string.IsNullOrEmpty(Visum))
            {
                MessageBox.Show("Visum kann nicht leer sein", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            ParentConext.Visum = Visum;
            ParentConext.FreigabeDatum = DateTime.Now;
            ParentConext.Status = "Freigegeben";
            ParentConext.Phasespeichern();
            MessageBox.Show("Visum hinzugefügt", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
