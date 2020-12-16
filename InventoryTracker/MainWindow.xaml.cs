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
        private List<Item> items = new List<Item>();
        private Inventory inventory = new Inventory();

        private bool isNewDocument = true; //used to check if the file is new and never saved.
        private string saveLocation; //once saved, keep the location for future saves

        public MainWindow()
        {
            InitializeComponent();
            ItemsStock.ItemsSource = items;
        }

        //Buttons
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            //inventory.load();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("updated");
            //inventory.update();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.ShowDialog();
            if (addForm.ValidateForm())
            {
                items.Add(GetItemObject(addForm));   
                ItemsStock.Items.Refresh();
            }
        }

        private void btnSort_Click(object sender, RoutedEventArgs e)
        {
            inventory.SortItems(items);
        }

        private void btnGeneralReport_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("reported");
            //inventory.GeneralReport();
        }

        private void btnSpecialReport_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("reported *");
            //inventory.SpecialReport();
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
                    return;
            }
            //SaveDataToFile();
        }
        private Item GetItemObject(AddForm addForm)
        {           
            return new Item()
            {             
                Name = addForm.prodName.Text,
                Supplier = addForm.cmbSupplier.Text,
                Category = addForm.cmbCategory.Text,
                AvailableQty = Convert.ToInt32(addForm.qtyAvailable.Text)
            };           
        }

        private void ItemsStock_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            Item I = ItemsStock.SelectedItem as Item;
            if (I != null)
            {
                ItemDetails.Text = I.FullInfo;
            }
        }

        /*
        
        private bool CheckToLoadExit()
        {
            //True: to start the loading process / exit process
            //False: to stop the load process /Exit process

            if (newRecordIndex < items.Count)
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
                if (newRecordIndex < items.Count)
                    return false;

            }
            return true; //Data is saved

        }
        */
    }
}
