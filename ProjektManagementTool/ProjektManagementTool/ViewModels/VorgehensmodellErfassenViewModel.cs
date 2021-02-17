using ProjektManagementTool.Helper;
using ProjektManagementTool.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ProjektManagementTool.ViewModels
{
    class VorgehensmodellErfassenViewModel : BaseViewModel
    {
        //Button: Phase erfassen
        ICommand _PhaseErfassen;
        public ICommand CMDPhaseErfassen
        {
            get
            {
                return _PhaseErfassen ?? (_PhaseErfassen =
                new RelayCommand(p => ShowPhaseErfassen()));
            }
        }
        //Funktion für den Command
        void ShowPhaseErfassen()
        {
            //Hier die neue View anzeigen
            var phaseBearbeitenView = new PhaseBearbeitenView();
            PhaseBearbeitenViewModel childContext = (PhaseBearbeitenViewModel)phaseBearbeitenView.DataContext;
            childContext.ParentDataContext = this;
            phaseBearbeitenView.Show();
        }

        //Button: Phase bearbeiten
        ICommand _PhaseBearbeiten;
        public ICommand CMDPhaseBearbeiten
        {
            get
            {
                return _PhaseBearbeiten ?? (_PhaseBearbeiten =
                new RelayCommand(p => ShowPhasBearbeiten()));
            }
        }
        //Funktion für den Command
        void ShowPhasBearbeiten()
        {
            //Hier die neue View anzeigen, muss noch als Parameter die aktuell ausgewählte phase übergeben
            var phaseBearbeitenView = new PhaseBearbeitenView();
            PhaseBearbeitenViewModel childContext = (PhaseBearbeitenViewModel)phaseBearbeitenView.DataContext;
            childContext.ParentDataContext = this;
            phaseBearbeitenView.Show();
        }

        //Liste mit den Phasen
        ObservableCollection<string> _Phasen;
        public ObservableCollection<string> Phasen
        {
            get { return _Phasen; }
            set
            {
                _Phasen = value;
                OnPropertyChanged("Phasen");
            }
        }
    }
}
