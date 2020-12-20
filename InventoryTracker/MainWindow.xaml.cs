using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Transactions;

namespace InventoryTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Inventory inventory = new Inventory();

        private bool isNewDocument = true; //used to check if the file is new and never saved
        private string saveLocation; //keeps track of the file location
        private int recordCount = 0; //used to keep track of saved records

        public MainWindow()
        {
            InitializeComponent();
            ItemsStock.ItemsSource = inventory.Items;
        }

        //Buttons
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            // Check before opening a new file that the current file is saved.
            if (CheckToLoadExit())
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "CSV Files|*.csv";

                if (openFileDialog.ShowDialog() == true)
                {
                    //Clear any current data
                    inventory.Items.Clear();
                    
                    //Read from the CSV file
                    saveLocation = openFileDialog.FileName;

                    inventory.LoadItems(saveLocation);
                    
                    //Refresh UI
                    ItemsStock.Items.Refresh();
                    
                    //Logic related to saving variables
                    isNewDocument = false;
                    recordCount = inventory.Items.Count; //indicates that data is already saved
                    
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdateItem update = new UpdateItem();
            update.ShowDialog();
            Item updateItem = ItemsStock.SelectedItem as Item;

            //verify if the user wants the item to be deleted
            if (update.deleteItem == true)
            {
                inventory.RemoveItem(updateItem);
            }
            else if (update.deleteItem == false)
            {
                inventory.UpdateItem(updateItem, Convert.ToInt32(update.qtyAvailable.Text), update.cmbSupplier.Text);
                ItemDetails.Text = updateItem.FullInfo; //refresh item details as soon as it's updated
            }
            else
            {
                //do nothing
            }
            ItemsStock.Items.Refresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.ShowDialog();

            //verify if the user clicks cancel on the add form
            if (addForm.cancel)
            {
                //do nothing
            }
            else
            {
                string message = inventory.AddItem(GetItemObject(addForm));
                
                //display confirming message
                MessageBox.Show(message, "Adding Status", MessageBoxButton.OK);
            }          
            ItemsStock.Items.Refresh();
        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            //Items are sorted in alphabetical order
            ItemsStock.ItemsSource = inventory.SortItems();
            ItemsStock.Items.Refresh();
        }

        private void btnGeneralReport_Click(object sender, RoutedEventArgs e)
        {
            //all items in the list will be displayed
            Reports generalReport = new Reports(inventory.GeneralReport());
            generalReport.ShowDialog();
           
        }

        private void btnShoppingList_Click(object sender, RoutedEventArgs e)
        {
            //any items that need to be purchased will show up on this report
            Reports shoppingList = new Reports(inventory.ShoppingList());
            shoppingList.ShowDialog();
        }


        private void btnAddQty(object sender, RoutedEventArgs e)
        {
            //quick add button for selected item in list
            Item I = ItemsStock.SelectedItem as Item;
            if (I.AvailableQty >= 0)
            {
                I.AvailableQty++;
            }
            //refresh UI
            ItemsStock.Items.Refresh();
            //updates the item details as there is an increase of quantity
            ItemDetails.Text = I.FullInfo;
        }

        private void btnRemoveQty(object sender, RoutedEventArgs e)
        {
            //quick minus button for selected item in list
            Item I = ItemsStock.SelectedItem as Item;
            if (I.AvailableQty > 0)
            {
                I.AvailableQty--;
            }
            //refresh UI
            ItemsStock.Items.Refresh();
            //updates the item details as there is a decrease of quantity
            ItemDetails.Text = I.FullInfo;
        }

        //Methods
        private void Save()
        {
            if (isNewDocument)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text File|*.csv";
                if (saveFileDialog.ShowDialog() == true)
                {
                    MessageBox.Show(saveFileDialog.FileName);
                    saveLocation = saveFileDialog.FileName;
                    isNewDocument = false;
                }
                else
                {
                    return;
                }
                    
            }
            string message = inventory.SaveItems(recordCount, saveLocation);
            //display confirmation message
            MessageBox.Show(message, "Save Status", MessageBoxButton.OK);
        }

        private Item GetItemObject(AddForm addForm)
        {
            //return new item to be added to the inventory list
            return new Item()
            {
                Name = addForm.prodName.Text,
                Supplier = addForm.cmbSupplier.Text,
                Category = addForm.cmbCategory.Text,
                AvailableQty = Convert.ToInt32(addForm.qtyAvailable.Text),
                MinQty = Convert.ToInt32(addForm.minQty.Text)
            };           
        }

        private void ItemsStock_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //displays all item details in Items Details Textbox
            Item I = ItemsStock.SelectedItem as Item;
            if (I != null)
            {
                ItemDetails.Text = I.FullInfo;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!CheckToLoadExit())
                e.Cancel = true; //true: means stay in the application, do not close
        }

        private bool CheckToLoadExit()
        {
            //True: to start the loading process / exit process
            //False: to stop the load process /Exit process

            if (recordCount < inventory.Items.Count)
            {
                //Data the is not saved >> do not load give warning msg
                MessageBoxResult result = MessageBox.Show("Data not saved.\nDo you want to save changes?", "Save Data Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                //Cancel >> return false
                if (result == MessageBoxResult.Cancel)
                    return false;

                //No >> return true
                if (result == MessageBoxResult.No)
                    return true;

                //Yes
                if (result == MessageBoxResult.Yes)
                    Save();

                //Data is saved: true will be return outside

                //Yes with no file name provide (Save As canceled) >> false (Data is still not saved after asking to save)
                if (recordCount < inventory.Items.Count)
                    return false;

            }
            return true; //Data is saved
        }

    }
}
