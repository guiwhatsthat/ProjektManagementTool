using ProjektManagementTool.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ProjektManagementTool.ViewModels
{
    class PhaseBearbeitenViewModel : BaseViewModel
    {
        //Parent DataContext
        VorgehensmodellErfassenViewModel _VorgehensmodellErfassenViewModel;
        public VorgehensmodellErfassenViewModel ParentDataContext
        {
            get { return _VorgehensmodellErfassenViewModel; }
            set
            {
                _VorgehensmodellErfassenViewModel = value;
                OnPropertyChanged();
            }
        }

        //Phasename Binding
        string _PhaseName;
        public string PhaseName
        {
            get { return _PhaseName; }
            set
            {
                _PhaseName = value;
                OnPropertyChanged();
            }
        }

        //Button: Phase speichern
        ICommand _PhaseSpeichern;
        public ICommand CMDPhaseSpeichern
        {
            get
            {
                return _PhaseSpeichern ?? (_PhaseSpeichern =
                new RelayCommand(p => PhaseSpeichern()));
            }
        }
        //Funktion für den Command
        void PhaseSpeichern()
        {
            //Phase der Liste vom Vorgehensmodellviewmodel hizufügen
            CreatePhaseListe();
            if (!string.IsNullOrEmpty(PhaseName))
            {
                if (ParentDataContext.Phasen.Any(x => x.ToLower() == PhaseName.ToLower()))
                {
                    MessageBox.Show("Phase existiert bereits", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                } else
                {
                    ParentDataContext.Phasen.Add(PhaseName);
                    MessageBox.Show("Phase wurde hinzugefügt", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        //Button: Phase löschen
        ICommand _PhaseLöschen;
        public ICommand CMDPhaseLöschen
        {
            get
            {
                return _PhaseLöschen ?? (_PhaseLöschen =
                new RelayCommand(p => PhaseLöschen()));
            }
        }
        //Funktion für den Command
        void PhaseLöschen()
        {
            //Phase der Liste vom Vorgehensmodellviewmodel entfernen
            CreatePhaseListe();
            if (!ParentDataContext.Phasen.Contains(PhaseName))
            {
                MessageBox.Show("Phase existiert nicht", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            ParentDataContext.Phasen.Remove(PhaseName);
            if (ParentDataContext.Phasen.Contains(PhaseName))
            {
                MessageBox.Show("Phase konnte nicht entfernt werden", "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else
            {
                MessageBox.Show("Phase wurde entfernt", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //Helper wenn das Vorgehensmodell noch keine Phasen hat, um die Liste zu instanzieren
        void CreatePhaseListe()
        {
            if (ParentDataContext.Phasen == null)
            {
                ParentDataContext.Phasen = new ObservableCollection<string>();
            }
        }

    }
}
