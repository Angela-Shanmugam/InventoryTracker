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
    /// Interaction logic for AddForm.xaml
    /// </summary>
    public partial class AddForm : Window
    {
        //data fields
        Item tempItem = new Item(); 
        List<string> dropDownSuppliers = new List<string>() { };
        List<string> categories = new List<string>() { };
        //checks if the user clicks cancel
        public bool cancel; 
        
        public AddForm()
        {
            InitializeComponent();
            SetSupplier();
            SetCategories();

        }
        //sets up supplier list in comboBox
        private void SetSupplier()
        {
            for (int i = 0; i < tempItem.supplier.Count; i++)
            {
                dropDownSuppliers.Add(tempItem.supplier[i]);
            }

            cmbSupplier.ItemsSource = dropDownSuppliers;
            cmbSupplier.SelectedIndex = -1;
        }

        //sets up category in comboBox
        private void SetCategories()
        {
            foreach (string categoryName in Enum.GetNames(typeof(category)))
            {
                categories.Add(categoryName);
            }

            cmbCategory.ItemsSource = categories;
            cmbCategory.SelectedIndex = -1;
        }

        //validates name, available quantity, minimum quantity, category and supplier
        public bool ValidateForm()
        {
            StringBuilder msg = new StringBuilder();

            //Name
            if (string.IsNullOrEmpty(prodName.Text))
            {
                msg.AppendLine("Name is a required field.");
            }

            //Available quantity and minimum quantity
            if (Convert.ToInt32(qtyAvailable.Text) <= 0 || string.IsNullOrEmpty(qtyAvailable.Text))
            {
                msg.AppendLine("Available Quantity is a required field.");               
            }

            if (Convert.ToInt32(minQty.Text) < 1 || string.IsNullOrEmpty(qtyAvailable.Text))
            {
                msg.AppendLine("Minimum Quantity is a required field.");
            }

            //category and Supplier: (-1 mean not selected index)
            if (cmbCategory.SelectedIndex == -1)
            {
                msg.AppendLine("A category was not selected.");
            }

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
            //validates form before closing the window
            if (ValidateForm())
            {
                cancel = false;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //user clicks cancel
            cancel = true;
            this.Close();
        }
    }
}
