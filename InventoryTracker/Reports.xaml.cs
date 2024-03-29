﻿using System;
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

/*Names: Angela Shanmugam and Reigina Mae Martin
 * Student ID: 1946356 and 1965312
 * Programming III: Final Project - Inventory Tracker
 * Teacher: Aref Mourtada
 * Due date: December 19,2020
*/

namespace InventoryTracker
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        //inventory list (containing the items) are passed the reports class to be displayed in the data grid
        public Reports(List<Item> items)
        {
            InitializeComponent();
            dgItems.ItemsSource = items;
        }
    }
}
