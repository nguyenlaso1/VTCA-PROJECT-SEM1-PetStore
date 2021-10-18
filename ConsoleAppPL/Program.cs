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
            for (; ; )
            {
                Console.Clear();
                Console.WriteLine("=====================================================================");
                Console.WriteLine(@"|    __________        __   _________ __                            |");
                Console.WriteLine(@"|    \______   \ _____/  |_/   _____//  |_  ___________   ____      |");
                Console.WriteLine(@"|     |     ___// __ \   __\_____  \\   __\/  _ \_  __ \_/ __ \     |");
                Console.WriteLine(@"|     |    |   \  ___/|  | /        \|  | (  <_> )  | \/\  ___/     |");
                Console.WriteLine(@"|     |____|    \___  >__|/_______  /|__|  \____/|__|    \___  >    |");
                Console.WriteLine(@"|                   \/            \/                         \/     |");
                Console.WriteLine("|                   Chao mung den voi PetStore!                     |");
                Console.WriteLine("=====================================================================");
                Console.WriteLine("| 1. Dang nhap                                                      |");
                Console.WriteLine("| 2. Thoat                                                          |");
                Console.WriteLine("=====================================================================");
                bool checkLogin = false;
                int choice;
                choice = Choice(2);
                switch (choice)
                {
                    case 1:
                        do
                        {
                            Console.Clear();
                            Console.WriteLine("=====================================================================");
                            Console.WriteLine(@"|    __________        __   _________ __                            |");
                            Console.WriteLine(@"|    \______   \ _____/  |_/   _____//  |_  ___________   ____      |");
                            Console.WriteLine(@"|     |     ___// __ \   __\_____  \\   __\/  _ \_  __ \_/ __ \     |");
                            Console.WriteLine(@"|     |    |   \  ___/|  | /        \|  | (  <_> )  | \/\  ___/     |");
                            Console.WriteLine(@"|     |____|    \___  >__|/_______  /|__|  \____/|__|    \___  >    |");
                            Console.WriteLine(@"|                   \/            \/                         \/     |");
                            Console.WriteLine("|                            Dang nhap                              |");
                            Console.WriteLine("=====================================================================");
                            Console.Write(" Ten dang nhap: ");
                            string userName = Console.ReadLine();
                            while (!(Regex.IsMatch(userName, @"^[a-z,A-Z,0-9]{8,}$")))
                            {
                                Console.WriteLine(" Ten dang nhap phai tu 8 ki tu tro len va khong co ki tu dac biet!");
                                Console.Write(" Ten dang nhap: ");
                                userName = Console.ReadLine();
                            }
                            Console.Write(" Mat khau: ");
                            string pass = GetPassword();
                            Console.WriteLine();
                            while (!(Regex.IsMatch(pass, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")))
                            {
                                Console.WriteLine(" Mat khau phai tu 8 ki tu tro len, co it nhat 1 chu hoa 1 chu thuong\n 1 so va khong chua ki tu dac biet!");
                                Console.Write(" Mat khau: ");
                                pass = GetPassword();
                                Console.WriteLine();
                            }

                            Staff staff = new Staff() { UserName = userName, Password = pass };
                            StaffBl bl = new StaffBl();
                            staff = bl.Login(staff);
                            if (staff.Role <= 0)
                            {
                                Console.WriteLine(" Sai ten dang nhap hoac mat khau!");
                                Console.Write(" Nhan phim bat ki de tiep tuc...");
                                Console.ReadKey();
                            }
                            else
                            {
                                ShowMainMenu(staff);
                                checkLogin = true;

                            }
                        } while (checkLogin != true);
                        break;
                    case 2:
                        if (IsContinue(" Ban co chac la muon thoat?(Y/N): "))
                        {
                            Console.WriteLine(" Da thoat ung dung!");
                            Environment.Exit(0);
                        }
                        break;
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
            string role;
            if (staff.Role == 1) role = "Cua hang truong";
            else role = "Nhan vien";
            ItemBl iBl = new ItemBl();
            InvoiceBl inBl = new InvoiceBl();
            int choice;
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
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("| {0,-59} |", role + ": " + staff.StaffName);
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("|                     Cac chuc nang chinh                     |");
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("| 1. Tim kiem san pham                                        |");
                Console.WriteLine("| 2. Tao hoa don moi                                          |");
                Console.WriteLine("| 3. Tao tai khoan nhan vien                                  |");
                Console.WriteLine("| 4. Dang xuat                                                |");
                Console.WriteLine("===============================================================");
                choice = Choice(4);
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
                            Console.WriteLine("| 1. Tim kiem theo ten                                        |");
                            Console.WriteLine("| 2. Tim kiem theo loai                                       |");
                            Console.WriteLine("| 3. Tim kiem theo nhan hieu                                  |");
                            Console.WriteLine("| 4. Tim kiem theo ID                                         |");
                            Console.WriteLine("| 5. Xem tat ca san pham                                      |");
                            Console.WriteLine("| 6. Them san pham moi                                        |");
                            Console.WriteLine("| 7. Quay lai cac chuc nang chinh                             |");
                            Console.WriteLine("=============================================================== ");
                            searchChoice = Choice(7);
                            switch (searchChoice)
                            {
                                case 1:
                                    Console.WriteLine("---------------------------------------------------");
                                    Console.WriteLine(" Goi y tu khoa: \"nho gay\", \"pate\", \"cho\", \"meo\",...");
                                    string searchKeyWordByName = EnterSearchKeyWord();
                                    string commSearchByName = "SELECT Items.item_id, Items.item_name, Items.item_price, Items.item_quantity, Items.item_weight, Items.item_description, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id WHERE Items.item_name like '%" + searchKeyWordByName + "%';";
                                    iBl.SearchItem(commSearchByName, searchKeyWordByName);
                                    break;
                                case 2:
                                    Console.WriteLine("---------------------------------------------------");
                                    Console.WriteLine(" Goi y tu khoa: \"thuc an\", \"vat dung\", \"thuoc thu y\", \"phu kien\",...");
                                    string searchKeyWordByCategory = EnterSearchKeyWord();
                                    string commSearchByCategory = "SELECT Items.item_id, Items.item_name, Items.item_price, Items.item_quantity, Items.item_weight, Items.item_description, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id WHERE Categories.category_name like '%" + searchKeyWordByCategory + "%';";
                                    iBl.SearchItem(commSearchByCategory, searchKeyWordByCategory);
                                    break;
                                case 3:
                                    Console.WriteLine("---------------------------------------------------");
                                    Console.WriteLine(" Goi y tu khoa: \"whiskas\", \"royal canin\", \"paw\", \"merial\",...");
                                    string searchKeyWordByBrand = EnterSearchKeyWord();
                                    string commSearchByBrand = "SELECT Items.item_id, Items.item_name, Items.item_price, Items.item_quantity, Items.item_weight, Items.item_description, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id WHERE Brands.brand_name like '%" + searchKeyWordByBrand + "%';";
                                    iBl.SearchItem(commSearchByBrand, searchKeyWordByBrand);
                                    break;
                                case 4:
                                    Console.WriteLine("---------------------------------------------------");
                                    Console.WriteLine(" Goi y tu khoa: \"1\", \"2\", \"10\", \"21\",...");
                                    string searchKeyWordById = EnterSearchKeyWord();
                                    iBl.SearchItemByID(searchKeyWordById);
                                    Console.Write(" Nhan phim bat ki de tiep tuc...");
                                    Console.ReadKey();
                                    break;
                                case 5:
                                    string commGetAllItem = "SELECT Items.item_id, Items.item_name, Items.item_price, Items.item_quantity, Items.item_weight, Items.item_description, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id;";
                                    iBl.GetAllItem(commGetAllItem);
                                    break;
                                case 6:
                                    Console.Clear();
                                    Console.WriteLine("===============================================================");
                                    Console.WriteLine(@"| __________        __   _________ __                         |");
                                    Console.WriteLine(@"| \______   \ _____/  |_/   _____//  |_  ___________   ____   |");
                                    Console.WriteLine(@"|  |     ___// __ \   __\_____  \\   __\/  _ \_  __ \_/ __ \  |");
                                    Console.WriteLine(@"|  |    |   \  ___/|  | /        \|  | (  <_> )  | \/\  ___/  |");
                                    Console.WriteLine(@"|  |____|    \___  >__|/_______  /|__|  \____/|__|    \___  > |");
                                    Console.WriteLine(@"|                \/            \/                         \/  |");
                                    Console.WriteLine("|                      Them san pham moi                      |");
                                    Console.WriteLine("=============================================================== ");
                                    string itemName;
                                    Console.Write(" Nhap ten san pham: ");
                                    itemName = Console.ReadLine();
                                    while (itemName.Length <= 0)
                                    {
                                        Console.WriteLine(" Ten san pham khong duoc de trong!");
                                        Console.Write(" Nhap ten san pham: ");
                                        itemName = Console.ReadLine();
                                    }
                                    itemName = itemName.Trim();
                                    itemName = FirstLetterToUpper(itemName);
                                    Console.Write(" Nhap gia san pham: ");
                                    double itemPrice = double.Parse(Console.ReadLine());
                                    while (!(Regex.IsMatch(itemPrice.ToString(), @"\d+")))
                                    {
                                        Console.WriteLine(" Gia san pham khong hop le!");
                                        Console.Write(" Nhap gia san pham: ");
                                        itemPrice = double.Parse(Console.ReadLine());
                                    }
                                    Console.Write(" Nhap ten nhan hieu: ");
                                    string itemBrand = Console.ReadLine();
                                    while (itemBrand.Length <= 0)
                                    {
                                        Console.WriteLine(" Nhan hieu san pham khong duoc de trong!");
                                        Console.Write(" Nhap ten nhan hieu: ");
                                        itemBrand = Console.ReadLine();
                                    }
                                    itemBrand = itemBrand.ToUpper();
                                    List<Category> cL = iBl.GetCategory();
                                    Console.WriteLine("-----------------------------------");
                                    Console.WriteLine("| Ma loai | Ten loai              |");
                                    Console.WriteLine("-----------------------------------");
                                    foreach (Category c in cL)
                                    {
                                        Console.WriteLine("| {0, 7} | {1, -21} |", c.CategoryId, c.CategoryName);
                                    }
                                    Console.WriteLine("-----------------------------------");
                                    Console.Write(" Nhap ma loai de chon loai: ");
                                    int categoryId = int.Parse(Console.ReadLine());
                                    while (categoryId < 1 || categoryId > cL.Count)
                                    {
                                        Console.WriteLine(" Ma loai khong hop le!");
                                        Console.Write(" Nhap ma loai de chon loai: ");
                                        categoryId = int.Parse(Console.ReadLine());
                                    }
                                    Console.Write(" Nhap so luong san pham: ");
                                    int itemQuantity = int.Parse(Console.ReadLine());
                                    while (!(Regex.IsMatch(itemPrice.ToString(), @"\d+")) || itemQuantity <= 0)
                                    {
                                        Console.WriteLine(" So luong san pham khong hop le!");
                                        Console.Write(" Nhap so luong san pham: ");
                                        itemQuantity = int.Parse(Console.ReadLine());
                                    }
                                    Console.Write(" Nhap khoi luong san pham: ");
                                    string itemWeight = Console.ReadLine();
                                    while (!(Regex.IsMatch(itemPrice.ToString(), @"[1-9]+.{0,1}[1-9]+[k][g]|[1-9]+[g]|[kg]")))
                                    {
                                        if (itemWeight.Length <= 0) break;
                                        Console.WriteLine(" Khoi luong san pham khong hop le!");
                                        Console.Write(" Nhap khoi luong san pham: ");
                                        itemWeight = Console.ReadLine();
                                    }
                                    Console.Write(" Mo ta san pham: ");
                                    string itemDescription = Console.ReadLine();
                                    if (itemDescription.Length > 0)
                                    {
                                        itemDescription.Trim();
                                        itemDescription = FirstLetterToUpper(itemDescription);
                                    }
                                    if (iBl.InsertItem(itemName, itemPrice, itemBrand, categoryId, itemQuantity, itemWeight, itemDescription))
                                    {
                                        Console.WriteLine(" Them san pham moi thanh cong");
                                        Console.WriteLine(" Nhan nut bat ki de tiep tuc...");
                                    }
                                    else Console.WriteLine(" Them san pham moi that bai");
                                    Console.ReadKey();
                                    break;
                                case 7:
                                    ShowMainMenu(staff);
                                    break;
                            }
                        } while (choice != 7);
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("===============================================================");
                        Console.WriteLine(@"| __________        __   _________ __                         |");
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
                            Console.Write(" Nhap ID cua san pham de them vao hoa don.");
                            Console.Write(" (Vi du: \"1\", \"2\", \"10\", \"21\",...): ");
                            string ID = Console.ReadLine();
                            Item item = iBl.SearchItemByID(ID);
                            if (item.ItemId == 0) continue;
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
                                if (quantity <= 0)
                                {
                                    Console.WriteLine(" Them khong thanh cong");
                                    Console.WriteLine(" San pham nay da het!");
                                    continue;
                                }
                                if (quantity <= 0)
                                {
                                    Console.WriteLine(" Them khong thanh cong");
                                    Console.WriteLine(" So luong nhap vao khong hop le!");
                                    continue;
                                }
                                if (quantity > item.ItemQuantity)
                                {
                                    Console.WriteLine(" Them khong thanh cong");
                                    Console.WriteLine(" So luong nhap vao vuot qua so luong co san!");
                                    continue;
                                }
                                double amount = (double)quantity * item.ItemPrice;
                                item.Quantity = quantity;
                                item.Amount = amount;
                                bool add = true;
                                if (invoice.itemsList == null || invoice.itemsList.Count == 0)
                                {
                                    invoice.itemsList.Add(item);
                                }
                                else
                                {
                                    for (int n = 0; n < invoice.itemsList.Count; n++)
                                    {
                                        if (int.Parse(ID) == invoice.itemsList[n].ItemId)
                                        {
                                            invoice.itemsList[n].Quantity += quantity;
                                            invoice.itemsList[n].Amount += amount;
                                            add = false;
                                        }
                                    }
                                    if (add) invoice.itemsList.Add(item);
                                }
                            }
                        } while (IsContinue(" Ban co muon them san pham khac vao hoa don khong? (Y/N): "));

                        if (invoice.itemsList == null || invoice.itemsList.Count == 0) Console.WriteLine(" Hoa don chua co san pham!");
                        if (inBl.CreateInvoice(invoice))
                        {
                            Console.WriteLine(" Tao hoa don thanh cong!");
                            Console.Write(" Nhan nut bat ki de xem hoa don...");
                            Console.ReadKey();
                            Console.Clear();
                            string price, amount;
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
                                price = FormatCurrency(item.ItemPrice.ToString());
                                amount = FormatCurrency(item.Amount.ToString());
                                Console.WriteLine("| {0,-65} {1,9} {2,5} {3,11} |", item.ItemName, price, item.Quantity, amount);
                                invoice.total += item.Amount;
                            }
                            string total = FormatCurrency(invoice.total.ToString());
                            Console.WriteLine("-------------------------------------------------------------------------------------------------");
                            Console.WriteLine("| TONG TIEN PHAI THANH TOAN {0,63} VND |", total);
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
                            Console.Write(" Nhan nut bat ki de tiep tuc...");
                        }
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("===============================================================");
                        Console.WriteLine(@"| __________        __   _________ __                         |");
                        Console.WriteLine(@"| \______   \ _____/  |_/   _____//  |_  ___________   ____   |");
                        Console.WriteLine(@"|  |     ___// __ \   __\_____  \\   __\/  _ \_  __ \_/ __ \  |");
                        Console.WriteLine(@"|  |    |   \  ___/|  | /        \|  | (  <_> )  | \/\  ___/  |");
                        Console.WriteLine(@"|  |____|    \___  >__|/_______  /|__|  \____/|__|    \___  > |");
                        Console.WriteLine(@"|                \/            \/                         \/  |");
                        Console.WriteLine("|                   Tao tai khoan nhan vien                   |");
                        Console.WriteLine("===============================================================");
                        if (staff.Role != 1)
                        {
                            Console.WriteLine("Cua hang truong moi duoc su dung tinh nang nay!");
                            Console.Write("Nhan nut bat ki de tiep tuc...");
                        }
                        else
                        {
                            Console.Write(" Nhap ten nhan vien: ");
                            string staffName = Console.ReadLine();
                            while (!(Regex.IsMatch(staffName, @"(^[A-Z,a-z]+$)|^([A-Z,a-z]+ *)+[A-Z,a-z]$")))
                            {
                                Console.WriteLine(" Ten nhan vien khong hop le!");
                                Console.Write(" Nhap ten nhan vien: ");
                                staffName = Console.ReadLine();
                            }
                            staffName = FixString(staffName);
                            Console.Write(" Ten dang nhap: ");
                            string userName = Console.ReadLine();
                            while (!(Regex.IsMatch(userName, @"^[a-z,A-Z,0-9]{8,}$")))
                            {
                                Console.WriteLine(" Ten dang nhap phai tu 8 ki tu tro len va khong co ki tu dac biet!");
                                Console.Write(" Ten dang nhap: ");
                                userName = Console.ReadLine();
                            }
                            Console.Write(" Mat khau: ");
                            string pass = GetPassword();
                            Console.WriteLine();
                            while (!(Regex.IsMatch(pass, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")))
                            {
                                Console.WriteLine(" Mat khau phai tu 8 ki tu tro len, co it nhat 1 chu hoa 1 chu thuong\n 1 so va khong chua ki tu dac biet!");
                                Console.Write(" Mat khau: ");
                                pass = GetPassword();
                                Console.WriteLine();
                            }
                            Console.Write(" Nhap lai mat khau: ");
                            string repass = GetPassword();
                            Console.WriteLine();
                            while (pass != repass)
                            {
                                Console.Write(" Mat khau khong trung khop!");
                                Console.Write(" Nhap lai mat khau: ");
                                repass = GetPassword();
                                Console.WriteLine();
                            }
                            Console.Write(" Nhap so dien thoai nhan vien: ");
                            string staffNumberPhone = Console.ReadLine();
                            while (!(Regex.IsMatch(staffNumberPhone, @"^(0|\+84)\d{9}$")))
                            {
                                Console.WriteLine(" So dien thoai khong hop le!");
                                Console.Write(" Nhap so dien thoai nhan vien: ");
                                staffNumberPhone = Console.ReadLine();
                            }
                            Console.Write(" Nhap gmail nhan vien: ");
                            string staffEmail = Console.ReadLine();
                            while (!(Regex.IsMatch(staffEmail, @"^([a-z,A-Z,0-9]+@[a-z]+\.[a-z]{3}\.[a-z]{2})|([a-z,A-Z,0-9]+@[a-z]+\.[a-z]{3})$")))
                            {
                                Console.WriteLine(" Gmail khong hop le!");
                                Console.Write(" Nhap gmail nhan vien: ");
                                staffEmail = Console.ReadLine();
                            }
                            StaffBl bl = new StaffBl();
                            if (bl.InsertStaff(staffName, userName, pass, staffNumberPhone, staffEmail))
                            {
                                Console.WriteLine(" Tao tai khoan thanh cong!");
                                Console.Write(" Nhan nut bat ki de tiep tuc...");
                            }
                            else
                            {
                                Console.WriteLine(" Tao tai khoan that bai!");
                                Console.Write(" Nhan nut bat ki de tiep tuc...");
                            }
                        }
                        Console.ReadKey();
                        break;
                    case 4:
                        if (IsContinue(" Ban co chac muon dang xuat?(Y/N): "))
                        {
                            Console.WriteLine(" Dang xuat thanh cong!");
                            Console.Write(" Nhan phim bat ki de tiep tuc...");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            ShowMainMenu(staff);
                        }
                        break;
                }
            } while (choice != 4);

        }

        static int Choice(int menuIndex)
        {
            int choice;
            string strChoice;
            bool isSuccess;
            Console.Write(" Chon: ");
            strChoice = Console.ReadLine();
            isSuccess = int.TryParse(strChoice, out choice);
            while (!isSuccess || choice < 1 || choice > menuIndex)
            {
                Console.Write(" Chon lai (1-{0}): ", menuIndex);
                strChoice = Console.ReadLine();
                isSuccess = int.TryParse(strChoice, out choice);
            }
            return choice;
        }

        static string EnterSearchKeyWord()
        {
            string searchKeyWord;
            Console.Write(" Nhap tu khoa de tim kiem: ");
            searchKeyWord = Console.ReadLine();
            Console.WriteLine("---------------------------------------------------");
            return searchKeyWord;
        }

        static bool IsContinue(string text)
        {
            string Continue;
            bool isMatch;
            Console.Write(text);
            Continue = Console.ReadLine();
            isMatch = Regex.IsMatch(Continue, @"^[yYnN]$");
            while (!isMatch)
            {
                Console.Write(" Chon (Y/N)!!!: ");
                Continue = Console.ReadLine();
                isMatch = Regex.IsMatch(Continue, @"^[yYnN]$");
            }
            if (Continue == "y" || Continue == "Y") return true;
            return false;
        }

        static string FormatCurrency(string currency)
        {
            for (int k = currency.Length - 3; k > 0; k = k - 3)
            {
                currency = currency.Insert(k, ".");
            }
            return currency;
        }

        static string FirstLetterToUpper(string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        public static string FixString(string str)
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
