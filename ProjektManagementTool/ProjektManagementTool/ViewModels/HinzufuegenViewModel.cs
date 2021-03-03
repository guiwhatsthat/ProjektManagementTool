using ProjektManagementTool.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ProjektManagementTool.ViewModels
{
    class HinzufuegenViewModel : BaseViewModel
    {
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

        //Parent DataContext
        AktiviteatBearbeitenViewModel _AktiviteatBearbeitenViewModel;
        public AktiviteatBearbeitenViewModel ParentDataContextAktiviteat
        {
            get { return _AktiviteatBearbeitenViewModel; }
            set
            {
                _AktiviteatBearbeitenViewModel = value;
                OnPropertyChanged();
            }
        }

        //Typ
        string _Header;
        public string Header
        {
            get { return _Header; }
            set
            {
                _Header = value;
                OnPropertyChanged("Header");
            }
        }

        int _Index;
        public int Index
        {
            get { return _Index; }
            set
            {
                _Index = value;
                OnPropertyChanged("Index");
            }
        }

        //Liste mit den Objekten
        List<dynamic> _ListObj;
        public List<dynamic> ListObj
        {
            get { return _ListObj; }
            set
            {
                _ListObj = value;
                OnPropertyChanged("ListObj");
            }
        }

        //Button Zuweisen
        ICommand _Zuweisen;
        public ICommand CMDZuweisen
        {
            get
            {
                return _Zuweisen ?? (_Zuweisen =
                new RelayCommand(p => Zuweisen()));
            }
        }
        //Funktion für den Command
        void Zuweisen()
        {
            if (ListObj == null || ListObj.Count == 0)
            {
                return;
            }

            if (Header == "Mitarbeiter")
            {
                string mitarbeiterName = ListObj[Index].Vorname + " " + ListObj[Index].Nachname;
                if (ParentDataContextAktiviteat == null)
                {
                    ParentDataContext.Mitarbeiter = mitarbeiterName;
                    ParentDataContext.ProjektleiterID = ListObj[Index].Pkey;
                } else
                {
                    ParentDataContextAktiviteat.MitarbeiterName = mitarbeiterName;
                    ParentDataContextAktiviteat.MitarbeiterPkey = ListObj[Index].Pkey;
                }
                
            } else
            {
                ParentDataContext.Modell = ListObj[Index].Name;
                ParentDataContext.VorgehensmodellID = ListObj[Index].Pkey;
            }
        }
    }
}
