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

namespace InventoryTracker
{
    /// <summary>
    /// Interaction logic for Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        Inventory inventory = new Inventory();
        public Reports()
        {
            InitializeComponent();   
            
        }

        public void ReportType(string type)
        {
            if (type == "General")
            {
                foreach (Item item in inventory.Items)
                {
                    string[] row = new string[] { item.Name, Convert.ToString(item.AvailableQty), Convert.ToString(item.MinQty) };
                    dgItems.Items.Add(row);
                }
            }
        }
    }
}
