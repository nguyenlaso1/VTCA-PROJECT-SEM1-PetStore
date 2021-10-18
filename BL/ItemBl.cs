using System;
using Persistence;
using DAL;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BL
{
    public class ItemBl
    {
        private ItemDal iDal = new ItemDal();

        public Item SearchItemByID(string searchKeyWord)
        {
            Item item = new Item();
            item = iDal.GetItemByID(searchKeyWord, item);
            string search = '"' + searchKeyWord + '"';
            if (item.ItemId == 0)
            {
                Console.WriteLine(" Khong ton tai san pham co ma la {0}", search);
            }
            else
            {
                ShowItemDetail(item, search);
            }
            return item;
        }

        public void SearchItem(string comm, string searchKeyWord)
        {
            List<Item> itemL = new List<Item>();
            itemL = iDal.GetItem(itemL, comm);
            string search = '"' + searchKeyWord + '"';
            if (itemL.Count == 0)
            {
                Console.WriteLine(" Khong co san pham phu hop voi tu khoa la {0}", search);
                Console.Write(" Nhan nut bat ki de tiep tuc...");
                Console.ReadKey();
            }
            else
            {
                int size = 10;
                int page = 1;
                int pages = (int)Math.Ceiling((double)itemL.Count / size);
                int i, k = 0;
                string chosse, price;
                for (; ; )
                {
                    Console.Clear();
                    Console.WriteLine("================================================================================================================================");
                    Console.WriteLine(@"|                                __________        __   _________ __                                                           |");
                    Console.WriteLine(@"|                                \______   \ _____/  |_/   _____//  |_  ___________   ____                                     |");
                    Console.WriteLine(@"|                                 |     ___// __ \   __\_____  \\   __\/  _ \_  __ \_/ __ \                                    |");
                    Console.WriteLine(@"|                                 |    |   \  ___/|  | /        \|  | (  <_> )  | \/\  ___/                                    |");
                    Console.WriteLine(@"|                                 |____|    \___  >__|/_______  /|__|  \____/|__|    \___  >                                   |");
                    Console.WriteLine(@"|                                               \/            \/                         \/                                    |");
                    Console.WriteLine("|                                            Ket qua tim kiem voi tu khoa la {0,-49} |", search);
                    Console.WriteLine("| Tim thay khoang {0,3} vat pham                                                                                       Trang {1}/{2} |", itemL.Count, page, pages);
                    Console.WriteLine("================================================================================================================================");
                    Console.WriteLine("| Ma SP | Ten san pham                                                      | Gia           | Nhan hieu        | Loai          |");
                    if (itemL.Count < size)
                    {
                        for (i = 0; i < itemL.Count; i++)
                        {
                            price = FormatCurrency(itemL[i].ItemPrice.ToString());
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("| {0,5} | {1,-65} | {2,13} | {3,-16} | {4,-13} |", itemL[i].ItemId, itemL[i].ItemName, price, itemL[i].ItemBrand, itemL[i].ItemCategory);
                        }
                    }
                    else
                    {
                        for (i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            if (i == itemL.Count) break;
                            price = FormatCurrency(itemL[i].ItemPrice.ToString());
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("| {0,5} | {1,-65} | {2,13} | {3,-16} | {4,-13} |", itemL[i].ItemId, itemL[i].ItemName, price, itemL[i].ItemBrand, itemL[i].ItemCategory);
                        }
                    }
                    Console.WriteLine("================================================================================================================================");
                    Console.WriteLine(" Nhan P de xem trang truoc.");
                    Console.WriteLine(" Nhan N de xem trang tiep theo.");
                    Console.WriteLine(" Nhap P kem so trang de xem trang mong muon (VD: P1, P2,...).");
                    Console.WriteLine(" Nhap ID de xem chi tiet san pham.");
                    Console.WriteLine(" Nhan 0 de quay lai.");
                    Console.WriteLine("-----------------------------------------------------------------");
                    Console.Write(" Chon: ");
                    chosse = Console.ReadLine();
                    while (!(Regex.IsMatch(chosse, @"([PpNn]|[1-9]|^0$)")))
                    {
                        Console.Write(" Chon khong hop le! Chon lai: ");
                        chosse = Console.ReadLine();
                    }
                    chosse = chosse.Trim();
                    chosse = chosse.ToLower();
                    string number = Regex.Match(chosse, @"\d+").Value;
                    string pageNum = "p" + number;
                    if (chosse == "n")
                    {
                        if (page == pages)
                        {
                            Console.Write(" Khong co trang sau! Nhan bat ki phim nao de tiep tuc...");
                            Console.ReadKey();
                        }
                        else
                        {
                            page = page + 1;
                            k = k + 10;
                        }
                    }
                    else if (chosse == "p")
                    {
                        if (page == 1)
                        {
                            Console.Write(" Khong co trang truoc! Nhan bat ki phim nao de tiep tuc...");
                            Console.ReadKey();
                        }
                        else
                        {
                            page = page - 1;
                            k = k - 10;
                        }
                    }
                    else if (chosse == pageNum)
                    {
                        if (int.Parse(number) < 0 || int.Parse(number) > pages || int.Parse(number) == 0)
                        {
                            Console.WriteLine(" Khong ton tai trang {0}", int.Parse(number));
                            Console.Write(" Nhan bat ki phim nao de tiep tuc...");
                            Console.ReadKey();
                        }
                        else
                        {
                            page = int.Parse(number);
                            k = (int.Parse(number) - 1) * 10;
                        }
                    }
                    else if (chosse == "0") return;
                    else
                    {
                        bool found = false;
                        string search1 = '"' + chosse + '"';
                        for (i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            try
                            {
                                if (int.Parse(chosse) == itemL[i].ItemId)
                                {
                                    ShowItemDetail(itemL[i], search1);
                                    Console.Write(" Nhan phim bat ki de tiep tuc...");
                                    Console.ReadKey();
                                    found = true;
                                    break;
                                }
                            }
                            catch (System.FormatException) { }
                            catch (System.ArgumentOutOfRangeException) { }
                        }
                        if (!(found))
                        {
                            Console.WriteLine(" ID khong phu hop!");
                            Console.Write(" Nhan phim bat ky de tiep tuc...");
                            Console.ReadKey();
                        }
                    }
                }
            }
        }

        public string FormatCurrency(string currency)
        {
            for (int k = currency.Length - 3; k > 0; k = k - 3)
            {
                currency = currency.Insert(k, ".");
            }
            currency = currency + " VND";
            return currency;
        }

        public void GetAllItem(string comm)
        {
            List<Item> itemL = new List<Item>();
            itemL = iDal.GetItem(itemL, comm);
            if (itemL.Count == 0)
            {
                Console.WriteLine(" Khong co san pham!");
                Console.Write(" Nhan nut bat ki de tiep tuc...");
                Console.ReadKey();
            }
            else
            {
                int size = 10;
                int page = 1;
                int pages = (int)Math.Ceiling((double)itemL.Count / size);
                int i, k = 0;
                string chosse, price;
                for (; ; )
                {
                    Console.Clear();
                    Console.WriteLine("================================================================================================================================");
                    Console.WriteLine(@"|                                __________        __   _________ __                                                           |");
                    Console.WriteLine(@"|                                \______   \ _____/  |_/   _____//  |_  ___________   ____                                     |");
                    Console.WriteLine(@"|                                 |     ___// __ \   __\_____  \\   __\/  _ \_  __ \_/ __ \                                    |");
                    Console.WriteLine(@"|                                 |    |   \  ___/|  | /        \|  | (  <_> )  | \/\  ___/                                    |");
                    Console.WriteLine(@"|                                 |____|    \___  >__|/_______  /|__|  \____/|__|    \___  >                                   |");
                    Console.WriteLine(@"|                                               \/            \/                         \/                                    |");
                    Console.WriteLine("|                                                       Tat ca san pham                                                        |");
                    Console.WriteLine("|                                                                                                                    Trang {0}/{1} |", page, pages);
                    Console.WriteLine("================================================================================================================================");
                    Console.WriteLine("| Ma SP | Ten san pham                                                      | Gia           | Nhan hieu        | Loai          |");
                    if (itemL.Count < size)
                    {
                        for (i = 0; i < itemL.Count; i++)
                        {
                            price = FormatCurrency(itemL[i].ItemPrice.ToString());
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("| {0,5} | {1,-65} | {2,9} | {3,-16} | {4,-13} |", itemL[i].ItemId, itemL[i].ItemName, price, itemL[i].ItemBrand, itemL[i].ItemCategory);
                        }
                    }
                    else
                    {
                        for (i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            if (i == itemL.Count) break;
                            price = FormatCurrency(itemL[i].ItemPrice.ToString());
                            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("| {0,5} | {1,-65} | {2,13} | {3,-16} | {4,-13} |", itemL[i].ItemId, itemL[i].ItemName, price, itemL[i].ItemBrand, itemL[i].ItemCategory);
                        }
                    }
                    Console.WriteLine("================================================================================================================================");
                    Console.WriteLine(" Nhan P de xem trang truoc.");
                    Console.WriteLine(" Nhan N de xem trang tiep theo.");
                    Console.WriteLine(" Nhap P kem so trang de xem trang mong muon (VD: P1, P2,...).");
                    Console.WriteLine(" Nhap ID de xem chi tiet san pham.");
                    Console.WriteLine(" Nhan 0 de quay lai.");
                    Console.WriteLine("-----------------------------------------------------------------");
                    Console.Write(" Chon: ");
                    chosse = Console.ReadLine();
                    while (!(Regex.IsMatch(chosse, @"([PpNn]|[1-9]|^0$)")))
                    {
                        Console.Write(" Chon khong hop le! Chon lai: ");
                        chosse = Console.ReadLine();
                    }
                    chosse = chosse.Trim();
                    chosse = chosse.ToLower();
                    string number = Regex.Match(chosse, @"\d+").Value;
                    string pageNum = "p" + number;
                    if (chosse == "n")
                    {
                        if (page == pages)
                        {
                            Console.Write(" Khong co trang sau! Nhan bat ki phim nao de tiep tuc...");
                            Console.ReadKey();
                        }
                        else
                        {
                            page = page + 1;
                            k = k + 10;
                        }
                    }
                    else if (chosse == "p")
                    {
                        if (page == 1)
                        {
                            Console.Write(" Khong co trang truoc! Nhan bat ki phim nao de tiep tuc...");
                            Console.ReadKey();
                        }
                        else
                        {
                            page = page - 1;
                            k = k - 10;
                        }
                    }
                    else if (chosse == pageNum)
                    {
                        if (int.Parse(number) < 0 || int.Parse(number) > pages || int.Parse(number) == 0)
                        {
                            Console.WriteLine(" Khong ton tai trang {0}", int.Parse(number));
                            Console.Write(" Nhan bat ki phim nao de tiep tuc...");
                            Console.ReadKey();
                        }
                        else
                        {
                            page = int.Parse(number);
                            k = (int.Parse(number) - 1) * 10;
                        }
                    }
                    else if (chosse == "0") return;
                    else
                    {
                        bool found = false;
                        string search1 = '"' + chosse + '"';
                        for (i = ((page - 1)) * size; i < k + 10; i++)
                        {
                            try
                            {
                                if (int.Parse(chosse) == itemL[i].ItemId)
                                {
                                    ShowItemDetail(itemL[i], search1);
                                    Console.Write(" Nhan phim bat ki de tiep tuc...");
                                    Console.ReadKey();
                                    found = true;
                                    break;
                                }
                            }
                            catch (System.FormatException) { }
                            catch (System.ArgumentOutOfRangeException) { }
                        }
                        if (!(found))
                        {
                            Console.WriteLine(" ID khong phu hop!");
                            Console.Write(" Nhan phim bat ky de tiep tuc...");
                            Console.ReadKey();
                        }
                    }
                }
            }
        }

        internal void ShowItemDetail(Item item, string search)
        {
            Console.Clear();
            string price = FormatCurrency(item.ItemPrice.ToString());
            Console.WriteLine("===============================================================================================");
            Console.WriteLine(@"|                 __________        __   _________ __                                         |");
            Console.WriteLine(@"|                 \______   \ _____/  |_/   _____//  |_  ___________   ____                   |");
            Console.WriteLine(@"|                  |     ___// __ \   __\_____  \\   __\/  _ \_  __ \_/ __ \                  |");
            Console.WriteLine(@"|                  |    |   \  ___/|  | /        \|  | (  <_> )  | \/\  ___/                  |");
            Console.WriteLine(@"|                  |____|    \___  >__|/_______  /|__|  \____/|__|    \___  >                 |");
            Console.WriteLine(@"|                                \/            \/                         \/                  |");
            Console.WriteLine("|                             Thong tin chi tiet san pham co ma la {0,-26} |", search);
            Console.WriteLine("===============================================================================================");
            Console.WriteLine("| Ma san pham:       | {0,-70} |", item.ItemId);
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("| Ten san pham:      | {0,-70} |", item.ItemName);
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("| Gia:               | {0,-70} |", price);
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("| Nhan hieu          | {0,-70} |", item.ItemBrand);
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("| Phan loai:         | {0,-70} |", item.ItemCategory);
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("| So luong:          | {0,-70} |", item.ItemQuantity);
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.WriteLine("| Khoi luong:        | {0,-70} |", item.ItemWeight);
            Console.WriteLine("-----------------------------------------------------------------------------------------------");
            Console.Write("| Mo ta:             |");
            string str = ' ' + item.ItemDescription;
            string subStr;
            int i = 65;
            try
            {
                while (str.Length > 0 && i < str.Length)
                {
                    i = 65;
                    while (str[i] != ' ')
                    {
                        i = i + 1;
                        if (i >= str.Length) break;
                    }
                    subStr = str.Substring(1, i);
                    Console.WriteLine(" {0,-70} |", subStr);
                    Console.Write("|                    |");
                    str = str.Remove(0, i);
                }
            }
            catch (System.ArgumentOutOfRangeException) { }
            finally
            {
                Console.WriteLine(" {0,-70} |", str.Remove(0, 1));
                Console.WriteLine("===============================================================================================");
            }
        }

        public List<Category> GetCategory()
        {
            return iDal.GetCategory();
        }

        public bool InsertItem(string name, double price, string brandName, int category, int quantity, string weight, string description)
        {
            return iDal.InsertItem(name, price, brandName, category, quantity, weight, description);
        }
    }
}
