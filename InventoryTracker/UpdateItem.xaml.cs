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
    /// Interaction logic for UpdateItem.xaml
    /// </summary>
    public partial class UpdateItem : Window
    {
        Item tempItem = new Item();
        List<string> dropDownSuppliers = new List<string>() { };
        public bool? deleteItem;

        public UpdateItem()
        {
            InitializeComponent();
            SetSupplier();
        }

        private void SetSupplier()
        {
            for (int i = 0; i < tempItem.supplier.Count; i++)
            {
                dropDownSuppliers.Add(tempItem.supplier[i]);
            }

            cmbSupplier.ItemsSource = dropDownSuppliers;
            cmbSupplier.SelectedIndex = -1;
        }

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
            deleteItem = true;
            this.Close();
        }

     
    }
}
