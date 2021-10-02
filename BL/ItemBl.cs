using System;
using Persistence;
using DAL;
using System.Collections.Generic;

namespace BL
{
    public class ItemBl
    {
        private ItemDal iDal = new ItemDal();

        public Item SearchItemByID(string searchKeyWord){
            Item item = new Item();
            item = iDal.GetItemByID(searchKeyWord, item);
            if(item.ItemId == 0)
            {
                Console.WriteLine(" No search results for key word = {0}", searchKeyWord);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("===============================================================================================");
                Console.WriteLine(@"|                 __________        __   _________ __                                         |");                     
                Console.WriteLine(@"|                 \______   \ _____/  |_/   _____//  |_  ___________   ____                   |");
                Console.WriteLine(@"|                 |     ___// __ \   __\_____  \\   __\/  _ \_  __ \_/ __ \                   |");
                Console.WriteLine(@"|                 |    |   \  ___/|  | /        \|  | (  <_> )  | \/\  ___/                   |");
                Console.WriteLine(@"|                 |____|    \___  >__|/_______  /|__|  \____/|__|    \___  >                  |");
                Console.WriteLine(@"|                               \/            \/                         \/                   |");
                Console.WriteLine("|                           SEARCH RESULTS FOR SEARCH KEY WORD = {0,-29}|", searchKeyWord);
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
                }
            }
            return item;
        }

        public void SearchItem(string comm, string searchKeyWord)
        {
            List<Item> itemL = new List<Item>();
            itemL = iDal.GetItem(itemL, comm);
            if (itemL.Count == 0)
            {
                Console.WriteLine(" No search results for key word = {0}", searchKeyWord);
                Console.Write(" Press any key to continue...");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("=========================================================================================================================");
                Console.WriteLine(@"|                              __________        __   _________ __                                                      |");                     
                Console.WriteLine(@"|                              \______   \ _____/  |_/   _____//  |_  ___________   ____                                |");
                Console.WriteLine(@"|                              |     ___// __ \   __\_____  \\   __\/  _ \_  __ \_/ __ \                                |");
                Console.WriteLine(@"|                              |    |   \  ___/|  | /        \|  | (  <_> )  | \/\  ___/                                |");
                Console.WriteLine(@"|                              |____|    \___  >__|/_______  /|__|  \____/|__|    \___  >                               |");
                Console.WriteLine(@"|                                            \/            \/                         \/                                |");
                Console.WriteLine("|                                          SEARCH RESUTL FOR KEY WORD = {0,-48}|", searchKeyWord);
                Console.WriteLine("| About {0} results                                                                                                      |", itemL.Count);
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


    }
}
