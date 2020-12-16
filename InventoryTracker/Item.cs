using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryTracker
{
    public enum category
    {
        Pantry,
        Diary,
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
        private int _availableQty;

        /*********************default constructor*********************/
        public Item() { }

        /*********************properties*********************/
        public string Name
        {
            get
            {
                return GetName();
            }
            set
            {
                _name = value;
                //SetName(_name);
            }
        }

        public int Location
        {
            get
            {
                return (int)GetLocation(_userCategory);
            }
            set
            {
                
            }
        }

        public string Supplier
        {
            get
            {
                return GetSupplier(_userSupplier);
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
                return GetMinimumQuantity();
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
                return GetAvailableQuantity();
            }
            set
            {
                SetAvailableQuantity(_availableQty);
            }

        }

        public string Category
        {
            get
            {
                return GetCategory(_userCategory);
            }
            set
            {
                _userCategory = value;
            }
        }



        /*********************Methods*********************/
        public string GetCategory(string userChoice)
        {
            foreach (string cat in Enum.GetNames(typeof(category)))
            {
                if (userChoice == cat)
                {
                    return cat;
                }
            }

            return userChoice;
            
        }

        public string GetSupplier(string userChoice)
        {
            //"Costco", "Walmart", "Super C", "Maxi", "Provigo", "IGA", "Other" 
            for (int i = 0; i < supplier.Count; i++)
            {
                if (userChoice == supplier[i])
                {
                    return supplier[i];
                }
            }
            return userChoice;
        }

        //the minimum quantity that a store should always have at all times
        public int GetMinimumQuantity()
        {

            switch (_userCategory)
            {
                case "Pantry":
                    _minQty = 2;
                    return _minQty;

                case "Dairy":
                    _minQty = 3;
                    return _minQty;

                case "Drinks":
                    _minQty = 4;
                    return _minQty;

                case "FrozenFood":
                    _minQty = 5;
                    return _minQty;

                case "Fruits":
                    _minQty = 6;
                    return _minQty;

                case "Vegetables":
                    _minQty = 7;
                    return _minQty;

                case "Bakery":
                    _minQty = 8;
                    return _minQty;

                case "CleaningSupplies":
                    _minQty = 9;
                    return _minQty;

                case "Meats":
                    _minQty = 10;
                    return _minQty;

                case "Other":
                    _minQty = 1;
                    return _minQty;

                default:
                    break;

            }
            return 1;
        }

        //Provides information where the item is located in the store
        public int? GetLocation(string userChoice)
        {
            int isleNum;
            switch (userChoice)
            {
                case "Pantry":
                    isleNum = (int)category.Pantry + 1;
                    return isleNum;

                case "Dairy":
                    isleNum = (int)category.Diary + 1;
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

        //quantity available in store
        public int GetAvailableQuantity()
        {
            return _availableQty;
        }

        public void SetAvailableQuantity(int _availableQty)
        {
            this._availableQty = _availableQty;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetName(string _name)
        {
            /*
            if (string.IsNullOrEmpty(_name))
            {
                throw new ArgumentException("Name cannot be empty", "Name");
            }
            this._name = _name;
            */
        }
        public string FullInfo
        {
            get
            {
                return string.Format(
                "{0,-20}" + Name + "\n" +
                "{1,-21}" + Supplier + "\n" +
                "{2,-20}" + Location + "\n" +
                "{3,-21}" + Category + "\n" +
                "{4,-20}" + AvailableQty + "\n",            
                "Name:", "Supplier:", "Location:", "Category:", "Available Quantity:");
            }
        }

        /*public string FullInfo()
        {
            return string.Format("Product Name: {0}\nSupplier: {1}\nLocation: Isle {2}\nCategory: {3}\nAvailable Quantity: {4}", this.Name, this.Supplier, this.Location, this.Category, this.AvailableQty);
        }*/
    }

}
