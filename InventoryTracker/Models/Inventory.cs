using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

/*Names: Angela Shanmugam and Reigina Mae Martin
 * Student ID: 1946356 and 1965312
 * Programming III: Final Project - Inventory Tracker
 * Teacher: Aref Mourtada
 * Due date: December 19,2020
*/

namespace InventoryTracker
{
    class Inventory
    {
        //data fields
        private List<Item> _items = new List<Item>() { };
        
        //getter for List
        public List<Item> Items { get { return _items; }}

        //methods
        //Adds the item from the list
        public string AddItem(Item item)
        {
            string msg;
            //loops over the list to see if the item was already added
            foreach  (Item theItem in _items)
            {
                if(theItem.Name == item.Name)
                {
                    msg = "Item already added. Adding item is unsuccessful.";
                    return msg;
                }
            }
            _items.Add(item);
             msg = "Adding item is successful.";
            return msg;
        }
        //Removes the item from the list
        public void RemoveItem(Item item)
        {
            _items.Remove(item);
        }

        //Provide functionality to update all the Item data fields(update available quantities)
        public Item UpdateItem(Item item,int quantity,string supplier)
        {
            foreach (Item theItem in _items)
            {
                if(theItem.Name == item.Name)
                {
                    theItem.AvailableQty = quantity;
                    theItem.Supplier = supplier;
                    return theItem;
                }
            }
            return item;
        }

        //Provide a report that shows all items with available quantities and minimum quantities
        public List<Item> GeneralReport()
        {
            return _items;
        }


        //Provided a report for items that need to be purchased because there is not enough quantity available
        public List<Item> ShoppingList()
        {
            List<Item> shoppingList = new List<Item>();
            foreach (Item theItem in _items)
            {
                if (theItem.AvailableQty < theItem.MinQty)
                {
                    shoppingList.Add(theItem);
                }
            }
            return shoppingList;
        }

        //A method to load items from a file(s)
        public void LoadItems(string saveLocation)
        {
            List<string> loadedItems = new List<string>();
            if (File.Exists(saveLocation))
            {
                StreamReader streamReader = null;
                try
                {
                    streamReader = new StreamReader(saveLocation);
                    foreach (string line in File.ReadLines(saveLocation))
                    {
                        loadedItems.Add(line);                      
                    }
                }
                catch (FormatException E)
                {
                    Console.WriteLine("Error! " + E.Message);
                    streamReader.Close();
                }
            }
            //runs through each item and assigns the data accordingly
            foreach (string item in loadedItems)
            {
                Item theLoadedItem = new Item();
                string[] data = item.Split(",");
                theLoadedItem.Name = data[0];
                theLoadedItem.Supplier = data[1];
                theLoadedItem.Location = Convert.ToInt32(data[2]);
                theLoadedItem.Category = data[3];
                theLoadedItem.AvailableQty = Convert.ToInt32(data[4]);
                theLoadedItem.MinQty = Convert.ToInt32(data[5]);
                _items.Add(theLoadedItem);
            }
        }
        //Saves any changes made on the item list or the items.
        public string SaveItems(int recordCount,string saveLocation)
        {
            try
            {
                StringBuilder records = new StringBuilder();
                
                //loop over all elements in the list and save them to a file
                for (int i = recordCount; i < _items.Count; i++)
                {
                    records.AppendLine(_items[i].CSVData);
                }
                File.AppendAllText(saveLocation, records.ToString());
                recordCount = _items.Count;

                return "Save Successful";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Cannot Save";              
            }
        }

       
        //add a method to sort the inventory list in alphabetical order, using insertion sort
         public List<Item> SortItems()
        {
            Item temp;
            int j;
            for (int i = 1; i < _items.Count; i++)
            {
                temp = _items[i];
                j = i - 1;

                while (j >= 0 && _items[j].Name.CompareTo(temp.Name) > 0)
                {
                    _items[j + 1] = _items[j];
                    j--;
                }
                _items[j + 1] = temp;
            }

            return _items;
        }
    }
}
