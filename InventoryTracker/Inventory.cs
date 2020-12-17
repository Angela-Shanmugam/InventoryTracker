using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace InventoryTracker
{
    class Inventory
    {

        const string FILE = "./Items.txt";
        private List<Item> _items;
        
        //Adds the item from the list
        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        //Removes the item from the list
        public void RemoveItem(Item item)
        {
            _items.Remove(item);
        }

        //Provide functionality to update all the Item data fields(update available quantities)
        //public void UpdateItem(int quantity)
        //{
        //    foreach  (Item theItem in _items)
        //    {
        //        if()
        //    }
        //}

        //Provide a report that shows all items with available quantities and minimum quantities
        public string GeneralReport(List<Item> items)
        {
            StringBuilder report = new StringBuilder();
            foreach (Item theItem in items)
            {
                report.AppendFormat("Item: {0}, Available quantity: {1}, Miminum quantity: {2}\n\n\n", theItem.Name, theItem.AvailableQty, theItem.MinQty, Environment.NewLine);
            }
            return report.ToString();
        }


        //Provided a report for items that need to be purchased because there is not enough quantity available
        public string ShoppingList(List<Item> items)
        {
            StringBuilder shopping = new StringBuilder();
            foreach (Item theItem in items)
            {
                if (theItem.AvailableQty < theItem.MinQty)
                {
                    shopping.AppendLine(theItem.Name);
                }
            }
            return shopping.ToString();
        }

        //A method to load items from a file(s)
        private void LoadItems()
        {
            try
            {
                if (File.Exists(FILE))
                {
                    //listOfItems = File.ReadAllLines(FILE);
                    //foreach (string item in listOfItems)
                    //{

                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        //Saves any changes made on the item list or the items.
        public string SaveItems(int recordCount,string saveLocation, List<Item> items)
        {
            try
            {
                StringBuilder records = new StringBuilder();
                //loop over all elements in the list and save them to a file
                for (int i = recordCount; i < items.Count; i++)
                {
                    records.AppendLine(items[i].CSVData);
                }
                File.AppendAllText(saveLocation, records.ToString());
                recordCount = items.Count;

                return "Save Successful";
            }
            catch (Exception e)
            {
                return "Cannot Save";
            }
        }

        //add a method to sort the inventory list in alphabetical order
        //Using Bubble Sort
         public List<Item> SortItems(List<Item> items)
        {
            List<Item> sortedList = new List<Item>();
            Item temp;
            for (int i = 0; i < items.Count; i++)
            {
                for (int j = 0; j < items.Count - 1; j++)
                {
                    if (items[j].Name.CompareTo(items[j + 1].Name) > 0)
                    {
                        temp = items[j];
                        items[j] = items[j + 1];
                        items[j + 1] = temp;
                    }
                }
            }
            for (int i = 0; i < items.Count; i++)
            {
                sortedList.Add(items[i]);
            }
            return sortedList;
        }
    }
}
