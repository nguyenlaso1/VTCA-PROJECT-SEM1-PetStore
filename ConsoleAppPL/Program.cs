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
            Console.WriteLine("=====================================================================");
            Console.WriteLine(@"|     __________        __   _________ __                           |");                     
            Console.WriteLine(@"|    \______   \ _____/  |_/   _____//  |_  ___________   ____      |");
            Console.WriteLine(@"|     |     ___// __ \   __\_____  \\   __\/  _ \_  __ \_/ __ \     |");
            Console.WriteLine(@"|     |    |   \  ___/|  | /        \|  | (  <_> )  | \/\  ___/     |");
            Console.WriteLine(@"|     |____|    \___  >__|/_______  /|__|  \____/|__|    \___  >    |");
            Console.WriteLine(@"|                   \/            \/                         \/     |");
            Console.WriteLine("|                             Dang nhap                             |");
            Console.WriteLine("=====================================================================");
            for(; ;)
            {
                Console.Write(" Ten dang nhap: ");
                string userName = Console.ReadLine();
                while(!(Regex.IsMatch(userName, @"^\w{8,}")))
                {
                    Console.WriteLine(" Ten dang nhap phai tu 8 ki tu tro len va khong co ki tu dac biet!");
                    Console.Write(" Ten dang nhap: ");
                    userName = Console.ReadLine();
                }
                Console.Write(" Mat khau:  ");
                string pass = GetPassword();
                Console.WriteLine();
                while(!(Regex.IsMatch(pass, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")))
                {
                    Console.WriteLine(" Mat khau phai tu 8 ki tu tro len, co it nhat 1 chu hoa 1 chu thuong\n 1 so va khong chua ki tu dac viet");
                    Console.Write(" Mat khau:  ");
                    pass = GetPassword();
                    Console.WriteLine();
                }

                Staff staff = new Staff(){UserName = userName, Password = pass};
                StaffBl bl = new StaffBl();
                staff = bl.Login(staff);
                if(staff.Role <= 0){
                    Console.WriteLine(" Sai ten dang nhap hoac mat khau!");
                }
                else
                {
                    ShowMainMenu(staff);
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

        static void ShowMainMenu(Staff staff)
        {
            StaffBl bl = new StaffBl();
            ItemBl iBl = new ItemBl();
            InvoiceBl inBl = new InvoiceBl();
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("===============================================================");
                Console.WriteLine(@"|  __________        __   _________ __                        |");                     
                Console.WriteLine(@"| \______   \ _____/  |_/   _____//  |_  ___________   ____   |");
                Console.WriteLine(@"|  |     ___// __ \   __\_____  \\   __\/  _ \_  __ \_/ __ \  |");
                Console.WriteLine(@"|  |    |   \  ___/|  | /        \|  | (  <_> )  | \/\  ___/  |");
                Console.WriteLine(@"|  |____|    \___  >__|/_______  /|__|  \____/|__|    \___  > |");
                Console.WriteLine(@"|                \/            \/                         \/  |");
                Console.WriteLine("|                     Cac chuc nang chinh                     |");
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("| 1. Tim kiem san pham                                        |");
                Console.WriteLine("| 2. Tao hoa don moi                                          |");
                Console.WriteLine("| 3. Thoat                                                    |");
                Console.WriteLine("===============================================================");
                choice = Choice(3);
                switch (choice)
                {
                    case 1:
                        int searchChoice;
                        do
                            {
                            Console.Clear();
                            Console.WriteLine("===============================================================");
                            Console.WriteLine(@"| __________        __   _________ __                         |");                     
                            Console.WriteLine(@"| \______   \ _____/  |_/   _____//  |_  ___________   ____   |");
                            Console.WriteLine(@"|  |     ___// __ \   __\_____  \\   __\/  _ \_  __ \_/ __ \  |");
                            Console.WriteLine(@"|  |    |   \  ___/|  | /        \|  | (  <_> )  | \/\  ___/  |");
                            Console.WriteLine(@"|  |____|    \___  >__|/_______  /|__|  \____/|__|    \___  > |");
                            Console.WriteLine(@"|                \/            \/                         \/  |");
                            Console.WriteLine("|                     Tim kiem san pham                       |");
                            Console.WriteLine("---------------------------------------------------------------");
                            Console.WriteLine("| 1. Search by name                                           |");
                            Console.WriteLine("| 2. Search by category                                       |");
                            Console.WriteLine("| 3. Search by brand                                          |");
                            Console.WriteLine("| 4. Search by ID                                             |");
                            Console.WriteLine("| 5. Return to main menu                                      |");
                            Console.WriteLine("=============================================================== ");
                            searchChoice = Choice(5);
                            switch (searchChoice)
                            {
                                case 1:
                                    string searchKeyWordByName = EnterSearchKeyWord();
                                    string commSearchByName = "SELECT Items.item_id, Items.item_name, Items.item_price, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id WHERE Items.item_name like '%"+searchKeyWordByName+"%';";
                                    iBl.SearchItem(commSearchByName, searchKeyWordByName);
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    string searchKeyWordByCategory = EnterSearchKeyWord();
                                    string commSearchByCategory = "SELECT Items.item_id, Items.item_name, Items.item_price, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id WHERE Categories.category_name like '%"+searchKeyWordByCategory+"%';";
                                    iBl.SearchItem(commSearchByCategory, searchKeyWordByCategory);
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    string searchKeyWordByBrand = EnterSearchKeyWord();
                                    string commSearchByBrand = "SELECT Items.item_id, Items.item_name, Items.item_price, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id WHERE Brands.brand_name like '%"+searchKeyWordByBrand+"%';";
                                    iBl.SearchItem(commSearchByBrand, searchKeyWordByBrand);
                                    Console.ReadKey();
                                    break;
                                case 4:
                                    string searchKeyWordById = EnterSearchKeyWord();
                                    iBl.SearchItemByID(searchKeyWordById);
                                    Console.Write(" Press any key to continue...");
                                    Console.ReadKey();
                                    break;
                                case 5:
                                    ShowMainMenu(staff);
                                    break; 
                            }
                        } while (choice !=5);
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("===============================================================");
                        Console.WriteLine(@"|  __________        __   _________ __                        |");                     
                        Console.WriteLine(@"| \______   \ _____/  |_/   _____//  |_  ___________   ____   |");
                        Console.WriteLine(@"|  |     ___// __ \   __\_____  \\   __\/  _ \_  __ \_/ __ \  |");
                        Console.WriteLine(@"|  |    |   \  ___/|  | /        \|  | (  <_> )  | \/\  ___/  |");
                        Console.WriteLine(@"|  |____|    \___  >__|/_______  /|__|  \____/|__|    \___  > |");
                        Console.WriteLine(@"|                \/            \/                         \/  |");
                        Console.WriteLine("|                         Tao hoa don                         |");
                        Console.WriteLine("===============================================================");
                        Invoice invoice = new Invoice();
                        invoice.InvoiceStaff = staff;
                        do
                        {
                            Console.Write(" Nhap ID cua san pham de them vao hoa don: ");
                            string ID = Console.ReadLine();
                            Item item = iBl.SearchItemByID(ID);
                            if(item.ItemId == 0) continue;
                            else
                            {
                            string strQuantity;
                            bool isSuccess;
                            int quantity;
                            Console.Write(" Nhap so luong: ");
                            strQuantity = Console.ReadLine();
                            isSuccess = int.TryParse(strQuantity, out quantity);
                            while (!isSuccess)
                            {
                                Console.Write(" So luong khong hop le! Nhap so luong: ");
                                strQuantity = Console.ReadLine();
                                isSuccess = int.TryParse(strQuantity, out quantity);
                            }     
                            item.Quantity = quantity;          
                            if(item.ItemQuantity <= 0)
                            {
                                Console.WriteLine(" Them khong thanh cong");
                                Console.WriteLine(" San pham nay da het!");
                                continue;
                            }  
                            if(item.Quantity > item.ItemQuantity)
                            {
                                Console.WriteLine(" Them khong thanh cong");
                                Console.WriteLine(" So luong nhap vao vuot qua so luong co san!");
                                continue;
                            }
                            item.Amount = (double)item.Quantity * item.ItemPrice;
                            invoice.itemsList.Add(item);
                            }
                        } while(IsContinue());
                        Console.Write(" Nhap ten khach hang: ");
                        string customerName = Console.ReadLine();
                        while(!(Regex.IsMatch(customerName, @"(^[A-Z,a-z]+$)|^([A-Z,a-z]+ *)+[A-Z,a-z]$")))
                        {
                            Console.WriteLine(" Ten khach hang khong hop le!");
                            Console.Write(" Nhap ten khach hang: ");
                            customerName = Console.ReadLine();
                        }
                        Console.Write(" Nhap so dien thoai khach hang: ");
                        string customerNumberPhone = Console.ReadLine();
                        while(!(Regex.IsMatch(customerNumberPhone, @"^(0|\+84)\d{9}$")))
                        {
                            Console.WriteLine(" So dien thoai khong hop le!");
                            Console.Write(" Nhap so dien thoai khach hang: ");
                            customerNumberPhone = Console.ReadLine();
                        }
                        customerName = FixString(customerName);
                        invoice.InvoiceCustomer = new Customer {CustomerName = customerName, CustomerNumberPhone = customerNumberPhone};
                        if(invoice.itemsList == null || invoice.itemsList.Count == 0) Console.WriteLine(" Hoa don chua co san pham!");
                        if(inBl.CreateInvoice(invoice))
                        {
                            Console.WriteLine(" Tao hoa don thanh cong!");
                            Console.Write(" Nhan nut bat ki de xem hoa don...");
                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("=================================================================================================");
                            Console.WriteLine(@"|                  __________        __   _________ __                                          |");                     
                            Console.WriteLine(@"|                  \______   \ _____/  |_/   _____//  |_  ___________   ____                    |");
                            Console.WriteLine(@"|                   |     ___// __ \   __\_____  \\   __\/  _ \_  __ \_/ __ \                   |");
                            Console.WriteLine(@"|                   |    |   \  ___/|  | /        \|  | (  <_> )  | \/\  ___/                   |");
                            Console.WriteLine(@"|                   |____|    \___  >__|/_______  /|__|  \____/|__|    \___  >                  |");
                            Console.WriteLine(@"|                                 \/            \/                         \/                   |");
                            Console.WriteLine("|                                       Hoa don ban hang                                        |");
                            Console.WriteLine("-------------------------------------------------------------------------------------------------");
                            Console.WriteLine("| Thoi gian: {0,-61}    Ma hoa don: {1,5} |", invoice.InvoiceDate, invoice.InvoiceId);
                            Console.WriteLine("| Nhan vien ban hang: {0,-41} Dia chi: Q.Long Bien, TP.Ha Noi |", invoice.InvoiceStaff.StaffName);
                            Console.WriteLine("-------------------------------------------------------------------------------------------------");
                            Console.WriteLine("| Mat hang                                                            Don gia    SL      T.Tien |");
                            foreach (Item item in invoice.itemsList)
                            {
                                Console.WriteLine("| {0,-65} {01,9:#,##0} {2,5} {03,11:#,##0} |", item.ItemName, item.ItemPrice, item.Quantity, item.Amount);
                                invoice.total += item.Amount;
                            }
                            Console.WriteLine("-------------------------------------------------------------------------------------------------");
                            Console.WriteLine("| TONG TIEN PHAI THANH TOAN {00,67:#,##0} |", invoice.total);
                            Console.WriteLine("-------------------------------------------------------------------------------------------------");
                            Console.WriteLine("| Ten khach hang: {0,-49} So dien thoai: {1,12} |", invoice.InvoiceCustomer.CustomerName, invoice.InvoiceCustomer.CustomerNumberPhone);
                            Console.WriteLine("-------------------------------------------------------------------------------------------------");
                            Console.WriteLine("|                               CAM ON QUY KHACH VA HEN GAP LAI!                                |");
                            Console.WriteLine("|                        Hotline: 18006968             Website: www.petstore.com                |");
                            Console.WriteLine("=================================================================================================");
                        }
                        else
                        {
                            Console.WriteLine(" Tao hoa don that bai!");
                            Console.ReadKey();
                        }
                        Console.ReadKey();
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

        static string EnterSearchKeyWord()
        {
            string searchKeyWord;
            Console.WriteLine("---------------------------------------------------");
            Console.Write(" Enter Search KeyWord: ");
            searchKeyWord = Console.ReadLine();
            Console.WriteLine("---------------------------------------------------");
            return searchKeyWord;
        }

        static bool IsContinue()
        {
            string Continue;
            bool isMatch;
            Console.Write(" Ban co muon them san pham khac vao hoa don khong? (Y/N): ");
            Continue = Console.ReadLine();
            isMatch = Regex.IsMatch(Continue, @"^[yYnN]$");
            while (!isMatch)
            {
                Console.Write(" Choice (Y/N)!!!: ");
                Continue = Console.ReadLine();
                isMatch = Regex.IsMatch(Continue, @"^[yYnN]$");
            }
            if (Continue == "y" || Continue == "Y") return true;
            return false;
        }

        static string FixString(string str)
        {
            char[] a = str.ToLower().ToCharArray();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = i == 0 || a[i - 1] == ' ' ? char.ToUpper(a[i]) : a[i];
            }
            return new string(a);
        }
    }
}
