using ProjektManagementTool.Helper;
using ProjektManagementTool.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ProjektManagementTool.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        //Button: Vorgehensmodell erfassen
        ICommand _VorgehensmodellErfassen;
        public ICommand CMDVorgehensmodellErfassen
        {
            get
            {
                return _VorgehensmodellErfassen ?? (_VorgehensmodellErfassen =
                new RelayCommand(p => ShowVorgehensmodellErfassen()));
            }
        }
        //Funktion für den Command
        void ShowVorgehensmodellErfassen()
        {
            //Hier die neue View anzeigen
            var vorgehensmodellErfassenView = new VorgehensmodellErfassenView();
            vorgehensmodellErfassenView.Show();
        }

        //Button: Projekt erfassen
        ICommand _ProjektErfassen;
        public ICommand CMDProjektErfassen
        {
            get
            {
                return _ProjektErfassen ?? (_ProjektErfassen =
                new RelayCommand(p => ShowProjektErfassen()));
            }
        }
        //Funktion für den Command
        void ShowProjektErfassen()
        {
            //Show Projektbearbeiten UI
            var projektBearbeitenView = new ProjektBearbeitenView();
            ProjektBearbeitenViewModel contex = (ProjektBearbeitenViewModel)projektBearbeitenView.DataContext;
            contex.Aktion = "Erfassen";
            contex.ReadOnlyFortschritt = true;
            contex.Fortschritt = 0;
            contex.Status = "Erfasst";
            contex.ReadOnlyStatus = true;
            projektBearbeitenView.Show();
        }

        //Button: Vorgehensmodell bearbeiten
        ICommand _VorgehensmodellBearbeiten;
        public ICommand CMDVorgehensmodellBearbeiten
        {
            get
            {
                return _VorgehensmodellBearbeiten ?? (_VorgehensmodellBearbeiten =
                new RelayCommand(p => ShowVorgehensmodellBearbeiten()));
            }
        }
        //Funktion für den Command
        void ShowVorgehensmodellBearbeiten()
        {
            ShowWaehlerView("Vorgehensmodell");
        }

        //Button: Projekt bearbeiten
        ICommand _ProjektBearbeiten;
        public ICommand CMDShowProjektBearbeiten
        {
            get
            {
                return _ProjektBearbeiten ?? (_ProjektBearbeiten =
                new RelayCommand(p => ShowProjektBearbeiten()));
            }
        }
        //Funktion für den Command
        void ShowProjektBearbeiten()
        {
            ShowWaehlerView("Projekt");
        }

        //Helper Funktion für den Waehlertyp
        void ShowWaehlerView(string t_Typ)
        {
            var dbhelper = new DBHelper();
            var bearbeitenWaehler = new BearbeitenWaehlerView();
            var context = (BearbeitenWaehlerViewModel)bearbeitenWaehler.DataContext;
            context.Header = t_Typ;
            context.ListObj = dbhelper.RunQuery(t_Typ, $"Select * from {t_Typ}");
            bearbeitenWaehler.Show();
        }
    }
}
