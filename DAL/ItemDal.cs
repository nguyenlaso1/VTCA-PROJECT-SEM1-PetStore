using System;
using MySql.Data.MySqlClient;
using Persistence;
using System.Collections.Generic;

namespace  DAL
{
 
    public class ItemDal
    {
        private MySqlConnection connection = DbHelper.GetConnection();
        public Item GetItemByID(string searchKeyWord, Item item)
        {
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "select *from Items where item_id = '"+searchKeyWord+"';";
                    MySqlDataReader reader = command.ExecuteReader();
                    if(reader.Read())
                    {
                        item.ItemId = reader.GetInt32("item_id");
                        item.ItemName = reader.GetString("item_name");
                        item.ItemPrice = reader.GetDouble("item_price");
                        item.ItemBrand = reader.GetString("item_brand");
                        item.ItemCategory = reader.GetString("item_category");
                        item.ItemQuantity = reader.GetInt32("item_quantity");
                        item.ItemWeight = reader.GetString("item_weight");
                        item.ItemDescription = reader.GetString("item_description");
                    }
                    else
                    {
                        item.ItemId = 0;
                    }
                    reader.Close();
                }
                catch
                {
                    item.ItemId = -1;
                }
                finally
                {
                    connection.Close();
                }
            return item;
            }
        }

        public List<Item> GetItem(List<Item> itemL, string comm)
        {
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = comm;
                    MySqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        Item item = new Item();
                        item.ItemId = reader.GetInt32("item_id");
                        item.ItemName = reader.GetString("item_name");
                        item.ItemPrice = reader.GetDouble("item_price");
                        item.ItemBrand = reader.GetString("brand_name");
                        item.ItemCategory = reader.GetString("category_name");
                        itemL.Add(item);
                    }
                    reader.Close();
                }
                catch
                {
                    Console.WriteLine("Lost Database Connection");
                }
                finally
                {
                    connection.Close();
                }
                return itemL;
            }
        }
    }
}