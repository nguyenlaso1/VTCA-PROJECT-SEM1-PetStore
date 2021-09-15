
using System.Text.RegularExpressions;
using System;
using Persistence;
using BL;

namespace ConsoleAppPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("|            Login            |");
            Console.WriteLine("-------------------------------");
            Console.Write(" User Name: ");
            string userName = Console.ReadLine();
            Console.Write(" Password:  ");
            string pass = GetPassword();
            Console.WriteLine();
            if(!(Regex.IsMatch(userName, @"^\w{8,}")) || !(Regex.IsMatch(pass, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$"))){
                Console.WriteLine(" Invalid User Name or Password!");
                Environment.Exit(0);
            }

            Staff staff = new Staff(){UserName = userName, Password = pass};
            StaffBl bl = new StaffBl();
            staff = bl.Login(staff);
            if(staff.Role <= 0){
                Console.Write(" Wrong User Name or Password!");
            }
            else{
                ShowMainMenu();
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
                Console.WriteLine("--------------------------");
                Console.WriteLine("|  WellCome To PetStore  |");
                Console.WriteLine("--------------------------");
                Console.WriteLine("| 1. SEARCH ITEM         |");
                Console.WriteLine("| 2. CREATE INVOICE      |");
                Console.WriteLine("| 3. EXIT                |");
                Console.WriteLine("--------------------------");
                choice = Choice(3);
                switch (choice)
                {
                    case 1:
                        int searchChoice;
                        do
                            {
                            Console.Clear();
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("|    Select to Search    |");
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("| 1. SEARCH BY NAME      |");
                            Console.WriteLine("| 2. SEAECH BY CATEGORY  |");
                            Console.WriteLine("| 3. SEAECH BY BRAND     |");
                            Console.WriteLine("| 4. SEAECH BY ID        |");
                            Console.WriteLine("| 5. RETURN TO MAIN MENU |");
                            Console.WriteLine("--------------------------");
                            searchChoice = Choice(5);
                            switch (searchChoice)
                            {
                                case 1:

                                    break;
                                case 2:

                                    break;
                                case 3:

                                    break;
                                case 4:
                                    string searchByIDKeyWord = EnterSearchKeyWord();
                                    Item item = new Item();
                                    ItemBl iBl = new ItemBl();
                                    item = iBl.SearchByID(searchByIDKeyWord, item);
                                    if(item.ItemId = 0)
                                    {
                                        Console.WriteLine(" Invalid ID");    
                                    }
                                    else
                                    {
                                        Console.WriteLine(" Item ID: {0}", item.ItemId);
                                        Console.WriteLine(" Item Name: {0}", item.ItemName);
                                        Console.WriteLine(" Item Price: {0}", item.ItemPrice);
                                        Console.WriteLine(" Item Brand: {0}", item.ItemBrand);
                                        Console.WriteLine(" Item Category: {0}", item.ItemCategory);
                                        Console.WriteLine(" Item Quantity: {0}", item.ItemQuantity);
                                        Console.WriteLine(" Item Weight: {0}", item.ItemWeight);
                                        Console.WriteLine(" Item Description: {0}", item.ItemDescription);
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

        static string EnterSearchKeyWord(){
            string searchKeyWord;
            Console.Write(" Enter Search KeyWord: ");
            searchKeyWord = Console.ReadLine();
            return searchKeyWord;
        }
    }
}
