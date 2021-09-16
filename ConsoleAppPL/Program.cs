using Internal;
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
                Console.WriteLine("|  WELLCOME TO PETSTORE  |");
                Console.WriteLine("--------------------------");
                Console.WriteLine("| 1. Search item         |");
                Console.WriteLine("| 2. Create invoice      |");
                Console.WriteLine("| 3. Exit                |");
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
                            Console.WriteLine("|    SELECT TO SEARCH    |");
                            Console.WriteLine("--------------------------");
                            Console.WriteLine("| 1. Search by name      |");
                            Console.WriteLine("| 2. Search by category  |");
                            Console.WriteLine("| 3. Search by brand     |");
                            Console.WriteLine("| 4. Search by ID        |");
                            Console.WriteLine("| 5. Retrun to main menu |");
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
                                    if(item.ItemId == 0)
                                    {
                                        Console.WriteLine(" Invalid ID");    
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("===================================================================================");
                                        Console.WriteLine("|                                 ITEM INFOMATION                                 |");
                                        Console.WriteLine("===================================================================================");
                                        Console.WriteLine("| Item ID:           | {0,-58} |", item.ItemId);
                                        Console.WriteLine("-----------------------------------------------------------------------------------");
                                        Console.WriteLine("| Item Name:         | {0,-58} |", item.ItemName);
                                        Console.WriteLine("-----------------------------------------------------------------------------------");
                                        Console.WriteLine("| Item Price:        | {00:#,##0} VND                                                 |", item.ItemPrice);
                                        Console.WriteLine("-----------------------------------------------------------------------------------");
                                        Console.WriteLine("| Item Brand:        | {0,-58} |", item.ItemBrand);
                                        Console.WriteLine("-----------------------------------------------------------------------------------");
                                        Console.WriteLine("| Item Category:     | {0,-58} |", item.ItemCategory);
                                        Console.WriteLine("-----------------------------------------------------------------------------------");
                                        Console.WriteLine("| Item Quantity:     | {0,-58} |", item.ItemQuantity);
                                        Console.WriteLine("-----------------------------------------------------------------------------------");
                                        Console.WriteLine("| Item Weight:       | {0,-58} |", item.ItemWeight);
                                        Console.WriteLine("-----------------------------------------------------------------------------------");
                                        Console.Write("| Item Description:  |");
                                        string str = item.ItemDescription;
                                        string subStr;
                                        int i = 73;
                                        while(str.Length > 0 && i < str.Length){
                                            i = 50;
                                            if (str.Length >= i)
                                            {
                                            while(str[i] != ' ') i = i + 1;
                                            subStr = str.Substring(0, i);
                                            str = str.Remove(0, i);
                                            str = str.Remove(0,1);
                                            Console.WriteLine(" {0,-58} |", subStr);
                                            Console.Write("|                    |");
                                            }
                                        }
                                        Console.WriteLine(" {0,-58} |", str);
                                        Console.WriteLine("===================================================================================");
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
            Console.WriteLine("--------------------------");
            Console.Write(" Enter Search KeyWord: ");
            searchKeyWord = Console.ReadLine();
            return searchKeyWord;
        }
    }
}
