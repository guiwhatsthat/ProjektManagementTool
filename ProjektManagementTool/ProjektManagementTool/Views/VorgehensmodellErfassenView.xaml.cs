﻿using ProjektManagementTool.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjektManagementTool.Views
{
    /// <summary>
    /// Interaction logic for VorgehensmodellErfassenView.xaml
    /// </summary>
    public partial class VorgehensmodellErfassenView : Window
    {
        public VorgehensmodellErfassenView()
        {
            InitializeComponent();
            DataContext = new VorgehensmodellErfassenViewModel();
        }
    }
}
