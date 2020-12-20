using System;
using System.Collections.Generic;
using System.Text;

/*Names: Angela Shanmugam and Reigina Mae Martin
 * Student ID: 1946356 and 1965312
 * Programming III: Final Project - Inventory Tracker
 * Teacher: Aref Mourtada
 * Due date: December 19,2020
*/

namespace InventoryTracker
{
    public enum category
    {
        Pantry,
        Dairy,
        Drinks,
        FrozenFood,
        Fruits,
        Vegetables,
        Bakery,
        CleaningSupplies,
        Meats,
        Other
    }
    public class Item
    {
        /*********************data fields*********************/

        //A predefined list of suppliers. This will be used in a dropdown menu
        public List<string> supplier = new List<string>() { "Costco", "Walmart", "Super C", "Maxi", "Provigo", "IGA", "Other" };

        private string _name;
        private int _minQty;
        private string _userCategory;
        private string _userSupplier;
        private int _location;
        private int _availableQty;

        /*********************default constructor*********************/
        public Item() { }

        /*********************properties*********************/
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                
                _name = value;
         
            }
        }

        public int Location
        {
            get
            {
                return (int)GetLocation();
            }
            set
            {
                _location = value;
            }
        }

        public string Supplier
        {
            get
            {
                return GetSupplier();
            }
            set
            {
                _userSupplier = value;
            }
        }

        public int MinQty
        {
            get
            {
                return _minQty;
            }
            set
            {
                _minQty = value;
            }

        }

        public int AvailableQty
        {
            get
            {
                return _availableQty;
            }
            set
            {
                _availableQty = value;
   
            }

        }

        public string Category
        {
            get
            {
                return GetCategory();
            }
            set
            {
                _userCategory = value;
            }
        }

        /*********************Methods*********************/
        public string GetCategory()
        {
            foreach (string cat in Enum.GetNames(typeof(category)))
            {
                if (_userCategory == cat)
                {
                    return cat;
                }
            }

            return _userCategory;
            
        }

        public string GetSupplier()
        {
            //"Costco", "Walmart", "Super C", "Maxi", "Provigo", "IGA", "Other" 
            for (int i = 0; i < supplier.Count; i++)
            {
                if (_userSupplier == supplier[i])
                {
                    return supplier[i];
                }
            }
            return _userSupplier;
        }

        //Provides information where the item is located in the store
        public int? GetLocation()
        {
            int isleNum;
            switch (_userCategory)
            {
                case "Pantry":
                    isleNum = (int)category.Pantry + 1;
                    return isleNum;

                case "Dairy":
                    isleNum = (int)category.Dairy + 1;
                    return isleNum;

                case "Drinks":
                    isleNum = (int)category.Drinks + 1;
                    return isleNum;

                case "FrozenFood":
                    isleNum = (int)category.FrozenFood + 1;
                    return isleNum;

                case "Fruits":
                    isleNum = (int)category.Fruits + 1;
                    return isleNum;

                case "Vegetables":
                    isleNum = (int)category.Vegetables + 1;
                    return isleNum;

                case "Bakery":
                    isleNum = (int)category.Bakery + 1;
                    return isleNum;

                case "CleaningSupplies":
                    isleNum = (int)category.CleaningSupplies + 1;
                    return isleNum;

                case "Meats":
                    isleNum = (int)category.Meats + 1;
                    return isleNum;

                case "Other":
                    isleNum = (int)category.Other + 1;
                    return isleNum;

                default:
                    break;

            }
            //isle number can be null
            return null;
        }

        //Provides all details regarding each item
        public string FullInfo
        {
            get
            {
                return string.Format(
                "{0,-20}" + Name + "\n" +
                "{1,-20}" + Supplier + "\n" +
                "{2,-20}" + Location + "\n" +
                "{3,-20}" + Category + "\n" +
                "{4,-20}" + AvailableQty + "\n" +
                "{5,-20}" + MinQty + "\n",            
                "Name:", "Supplier:", "Location (isle):", "Category:", "Available Quantity:", "Minimum Quantity:");
            }
        }

        //CSVData is used to save and load data
        public string CSVData
        {
            get { return string.Format($"{Name},{Supplier},{Location},{Category},{AvailableQty},{MinQty}"); }
            set
            {
                try
                {
                    string[] data = value.Split(",");
                    Name = data[0];
                    Supplier = data[1];
                    Location = Convert.ToInt32(data[2]);
                    Category = data[3];
                    AvailableQty = Convert.ToInt32(data[4]);
                    MinQty = Convert.ToInt32(data[5]);         
                }
                catch (Exception)
                {
                    throw new ArgumentException("CSV Data property not valid input", "CVSData");
                }
            }
        }
    }

}
