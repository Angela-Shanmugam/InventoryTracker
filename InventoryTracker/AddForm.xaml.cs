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
    /// Interaction logic for AddForm.xaml
    /// </summary>
    public partial class AddForm : Window
    {
        Item tempItem = new Item(); 
        List<string> dropDownSuppliers = new List<string>() { };
        List<string> categories = new List<string>() { };
        
        public AddForm()
        {
            InitializeComponent();
            SetSupplier();
            SetCategories();

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

        private void SetCategories()
        {
            foreach (string categoryName in Enum.GetNames(typeof(category)))
            {
                categories.Add(categoryName);
            }

            cmbCategory.ItemsSource = categories;
            cmbCategory.SelectedIndex = -1;
        }

        public bool ValidateForm()
        {
            StringBuilder msg = new StringBuilder();

            //Name
            if (string.IsNullOrEmpty(prodName.Text))
            {
                msg.AppendLine("Name is a required field.");
            }

            //Available Qty
            if (Convert.ToInt32(qtyAvailable.Text) <= 0 || string.IsNullOrEmpty(qtyAvailable.Text))
            {
                msg.AppendLine("Quantity is a required field.");               
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
            if (ValidateForm())
            {
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
