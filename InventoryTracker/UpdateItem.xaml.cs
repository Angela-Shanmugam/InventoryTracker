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

/*Names: Angela Shanmugam and Reigina Mae Martin
 * Student ID: 1946356 and 1965312
 * Programming III: Final Project - Inventory Tracker
 * Teacher: Aref Mourtada
 * Due date: December 19,2020
*/

namespace InventoryTracker
{
    /// <summary>
    /// Interaction logic for UpdateItem.xaml
    /// </summary>
    public partial class UpdateItem : Window
    {
        //data fields
        Item tempItem = new Item();
        List<string> dropDownSuppliers = new List<string>() { };
        //keeps track if the user presses delete item
        public bool? deleteItem;

        public UpdateItem()
        {
            InitializeComponent();
            SetSupplier();
        }

        //adds supplier list to the comboBox
        private void SetSupplier()
        {
            for (int i = 0; i < tempItem.supplier.Count; i++)
            {
                dropDownSuppliers.Add(tempItem.supplier[i]);
            }

            cmbSupplier.ItemsSource = dropDownSuppliers;
            cmbSupplier.SelectedIndex = -1;
        }

        //validates available quantity, category and supplier 
        public bool ValidateForm()
        {
            StringBuilder msg = new StringBuilder();

            //Available quantity
            if (string.IsNullOrEmpty(qtyAvailable.Text) || Convert.ToInt32(qtyAvailable.Text) <= 0)
            {
                msg.AppendLine("Available Quantity is a required field.");
            }

            //category and Supplier: (-1 mean not selected index)
            if (cmbSupplier.SelectedIndex == -1)
            {
                msg.AppendLine("A supplier was not selected.");
            }

            //Display a message
            if (string.IsNullOrEmpty(msg.ToString()))
            {
                return true;
            }

            MessageBox.Show(msg.ToString(), "Form missing data", MessageBoxButton.OK, MessageBoxImage.Error);
            return false;

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //validate form before closing window
            if (ValidateForm())
            {
                deleteItem = false;
                this.Close();
            }       
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            deleteItem = null;
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //user clicks delete item
            deleteItem = true;
            this.Close();
        }

     
    }
}
