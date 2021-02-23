using System;
using System.Collections.Generic;
using System.Text;

namespace ProjektManagementTool.Models
{
    class GenericKosten
    {
        public string Typ { get; set; }
        public string Name { get; set; }
        public int Pkey { get; set; }
        public int AktivitaetPkey { get; set; }
        public int ZPkey { get; set; }

        public GenericKosten(string t_Typ, string t_Name, int t_Pkey, int t_AktivitaetPkey, int t_ZPkey)
        {
            Typ = t_Typ;
            Name = t_Name;
            Pkey = t_Pkey;
            AktivitaetPkey = t_AktivitaetPkey;
            ZPkey = t_ZPkey;
        }
    } 
}
