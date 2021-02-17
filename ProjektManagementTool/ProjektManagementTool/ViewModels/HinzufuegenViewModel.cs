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
            
        }
    }
}
