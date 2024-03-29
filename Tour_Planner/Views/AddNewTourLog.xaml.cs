﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tour_Planner.BL;
using Tour_Planner.DAL;
using Tour_Planner.Model;
using Tour_Planner.ViewModels;

namespace Tour_Planner.Views {
    /// <summary>
    /// Interaction logic for AddNewTourLog.xaml
    /// </summary>
    public partial class AddNewTourLog : Window {
        public AddNewTourLog(int TourId) {
            InitializeComponent();
            DataContext = new AddNewTourLogViewModel(new TourManager(new DataManagerEntityFrameworkImpl()), TourId);
        }

        public AddNewTourLog(TourLogs selectedTL) {
            InitializeComponent();
            DataContext = new AddNewTourLogViewModel(new TourManager(new DataManagerEntityFrameworkImpl()), selectedTL);
        }


    }
}
