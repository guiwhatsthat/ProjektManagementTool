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
    /// Interaction logic for MeilensteinBearbeitenView.xaml
    /// </summary>
    public partial class MeilensteinBearbeitenView : Window
    {
        public MeilensteinBearbeitenView()
        {
            InitializeComponent();
            DataContext = new MeilensteinBearbeitenViewModel();
        }
    }
}
