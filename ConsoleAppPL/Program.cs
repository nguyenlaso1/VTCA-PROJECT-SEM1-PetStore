using System.Text.RegularExpressions;
using System;
using Persistence;
using BL;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=============================================================================");
            Console.WriteLine("|                                   Login                                   |");
            Console.WriteLine("=============================================================================");
            for(; ;)
            {
                Console.Write(" User Name: ");
                string userName = Console.ReadLine();
                while(!(Regex.IsMatch(userName, @"^\w{8,}")))
                {
                    Console.WriteLine(" User Name must be over 8 characters and doesn't contain special characters!\n");
                    Console.Write(" User Name: ");
                    userName = Console.ReadLine();
                }
                Console.Write(" Password:  ");
                string pass = GetPassword();
                Console.WriteLine();
                while(!(Regex.IsMatch(pass, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")))
                {
                    Console.WriteLine(" Password must be over 8 characters include lowercase letter, numbers and at \n least one uppercase letter and doesn't contain special characters!\n");
                    Console.Write(" Password:  ");
                    pass = GetPassword();
                    Console.WriteLine();
                }

                Staff staff = new Staff(){UserName = userName, Password = pass};
                StaffBl bl = new StaffBl();
                staff = bl.Login(staff);
                if(staff.Role <= 0){
                    Console.WriteLine(" Wrong User Name or Password!");
                }
                else
                {
                    ShowMainMenu();
                }
            }
        }

        static string GetPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return pass;
        }

        static void ShowMainMenu()
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("==========================");
                Console.WriteLine("|        PETSTORE        |");
                Console.WriteLine("|       Main menu        |");
                Console.WriteLine("--------------------------");
                Console.WriteLine("| 1. Search item         |");
                Console.WriteLine("| 2. Create invoice      |");
                Console.WriteLine("| 3. Exit                |");
                Console.WriteLine("==========================");
                choice = Choice(3);
                switch (choice)
                {
                    case 1:
                        int searchChoice;
                        do
                            {
                            Console.Clear();
                            Console.WriteLine("==========================");
                            Console.WriteLine("|        PETSTORE        |");
                            Console.WriteLine("|     Search feature     |");
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("| 1. Search by name      |");
                            Console.WriteLine("| 2. Search by category  |");
                            Console.WriteLine("| 3. Search by brand     |");
                            Console.WriteLine("| 4. Search by ID        |");
                            Console.WriteLine("| 5. Return to main menu |");
                            Console.WriteLine("==========================");
                            searchChoice = Choice(5);
                            switch (searchChoice)
                            {
                                case 1:
                                    string searchKeyWordByName = EnterSearchKeyWord();
                                    string commSearchByName = "SELECT Items.item_id, Items.item_name, Items.item_price, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id WHERE Items.item_name like '%"+searchKeyWordByName+"%';";
                                    List<Item> itemL1 = new List<Item>();
                                    ItemBl iBl1 = new ItemBl();
                                    itemL1 = iBl1.GetItem(itemL1, commSearchByName);
                                    ShowSearchResult(itemL1, searchKeyWordByName);
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    string searchKeyWordByCategory = EnterSearchKeyWord();
                                    string commSearchByCategory = "SELECT Items.item_id, Items.item_name, Items.item_price, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id WHERE Categories.category_name like '%"+searchKeyWordByCategory+"%';";
                                    List<Item> itemL2 = new List<Item>();
                                    ItemBl iBl2 = new ItemBl();
                                    itemL2 = iBl2.GetItem(itemL2, commSearchByCategory);
                                    ShowSearchResult(itemL2, searchKeyWordByCategory);
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    string searchKeyWordByBrand = EnterSearchKeyWord();
                                    string commSearchByBrand = "SELECT Items.item_id, Items.item_name, Items.item_price, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id WHERE Brands.brand_name like '%"+searchKeyWordByBrand+"%';";
                                    List<Item> itemL3 = new List<Item>();
                                    ItemBl iBl3 = new ItemBl();
                                    itemL3 = iBl3.GetItem(itemL3, commSearchByBrand);
                                    ShowSearchResult(itemL3, searchKeyWordByBrand);
                                    Console.ReadKey();
                                    break;
                                case 4:
                                    string searchKeyWordById = EnterSearchKeyWord();
                                    Item item = new Item();
                                    ItemBl iBl4 = new ItemBl();
                                    item = iBl4.GetItemByID(searchKeyWordById, item);
                                    if(item.ItemId == 0)
                                    {
                                        Console.Write(" No search results for key word = {0}", searchKeyWordById);    
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("===============================================================================================");
                                        Console.WriteLine("|                           SEARCH RESULTS FOR SEARCH KEY WORD = {0,-29}|", searchKeyWordById);
                                        Console.WriteLine("===============================================================================================");
                                        Console.WriteLine("| Item ID:           | {0,-70} |", item.ItemId);
                                        Console.WriteLine("-----------------------------------------------------------------------------------------------");
                                        Console.WriteLine("| Item Name:         | {0,-70} |", item.ItemName);
                                        Console.WriteLine("-----------------------------------------------------------------------------------------------");
                                        Console.WriteLine("| Item Price:        | {00,-70:#,##0} |", item.ItemPrice);
                                        Console.WriteLine("-----------------------------------------------------------------------------------------------");
                                        Console.WriteLine("| Item Brand:        | {0,-70} |", item.ItemBrand);
                                        Console.WriteLine("-----------------------------------------------------------------------------------------------");
                                        Console.WriteLine("| Item Category:     | {0,-70} |", item.ItemCategory);
                                        Console.WriteLine("-----------------------------------------------------------------------------------------------");
                                        Console.WriteLine("| Item Quantity:     | {0,-70} |", item.ItemQuantity);
                                        Console.WriteLine("-----------------------------------------------------------------------------------------------");
                                        Console.WriteLine("| Item Weight:       | {0,-70} |", item.ItemWeight);
                                        Console.WriteLine("-----------------------------------------------------------------------------------------------");
                                        Console.Write("| Item Description:  |");
                                        
                                        string str = ' ' + item.ItemDescription;
                                        string subStr;
                                        int i = 65;
                                        try
                                        {
                                            while(str.Length > 0 && i < str.Length)
                                            {
                                                i = 65;
                                                while(str[i] != ' ') {
                                                    i = i + 1;
                                                    if (i >= str.Length) break;
                                                }
                                                subStr = str.Substring(1,i);
                                                Console.WriteLine(" {0,-70} |", subStr);
                                                Console.Write("|                    |");
                                               str = str.Remove(0,i);
                                            }
                                        }
                                        catch(System.ArgumentOutOfRangeException){}
                                        finally
                                        {
                                            Console.WriteLine(" {0,-70} |", str.Remove(0,1));
                                            Console.WriteLine("===============================================================================================");
                                            Console.Write(" Press any key to continue...");
                                        }
                                    }
                                    Console.ReadKey();
                                    break;
                                case 5:
                                    ShowMainMenu();
                                    break;
                                
                            }
                        } while (choice !=5);
                        break;
                    case 2:
                            
                        break;
                    case 3:     
                        Console.WriteLine("--------------------------");
                        Console.Write(" Exited.");
                        Environment.Exit(0);
                        break;
                }
            } while (choice != 3);

        }

        static int Choice(int menuIndex)
        {
            int choice;
            string strChoice;
            bool isSuccess;
            Console.Write(" Choice: ");
            strChoice = Console.ReadLine();
            isSuccess = int.TryParse(strChoice, out choice);
            while (!isSuccess || choice < 1 || choice > menuIndex)
            {
                Console.Write(" Re-choice (1-{0}): ", menuIndex);
                strChoice = Console.ReadLine();
                isSuccess = int.TryParse(strChoice, out choice);
            }
            return choice;
        }

        static void ShowSearchResult(List<Item> itemL, string searchKeyWord)
        {
            if (itemL.Count == 0)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine(" No search results for key word = {0}", searchKeyWord);
                Console.Write(" Press any key to continue...");
            }
            else
            {
                Console.Clear();
                Console.WriteLine(" About {0} results", itemL.Count);
                Console.WriteLine("=========================================================================================================================");
                Console.WriteLine("|                                          SEARCH RESUTL FOR KEY WORD = {0,-48}|", searchKeyWord);
                Console.WriteLine("=========================================================================================================================");
                Console.WriteLine("| ID | Item Name                                                         | Price     | Brand            | Category      |");
                foreach (Item i in itemL)
                {
                    Console.WriteLine("-------------------------------------------------------------------------------------------------------------------------");
                    Console.WriteLine("| {0,-2} | {1,-65} | {02,-9:#,##0} | {3,-16} | {4,-13} |", i.ItemId, i.ItemName, i.ItemPrice, i.ItemBrand, i.ItemCategory);
                }
                Console.WriteLine("=========================================================================================================================");
                Console.Write(" Press any key to return...");
            }
        }

        static string EnterSearchKeyWord()
        {
            string searchKeyWord;
            Console.WriteLine("---------------------------------------------------");
            Console.Write(" Enter Search KeyWord: ");
            searchKeyWord = Console.ReadLine();
            Console.WriteLine("---------------------------------------------------");
            return searchKeyWord;
        }

    }
}
