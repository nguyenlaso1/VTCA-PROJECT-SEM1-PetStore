using System.Data;
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
                    command.CommandText = "SELECT Items.item_id, Items.item_name, Items.item_price, Items.item_quantity, Items.item_weight, Items.item_description, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id WHERE Items.item_id = "+searchKeyWord+";";
                    MySqlDataReader reader = command.ExecuteReader();
                    if(reader.Read())
                    {
                        item.ItemId = reader.GetInt32("item_id");
                        item.ItemName = reader.GetString("item_name");
                        item.ItemPrice = reader.GetDouble("item_price");
                        item.ItemBrand = reader.GetString("brand_name");
                        item.ItemCategory = reader.GetString("category_name");
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
                        item.ItemQuantity = reader.GetInt32("item_quantity");
                        item.ItemWeight = reader.GetString("item_weight");
                        item.ItemDescription = reader.GetString("item_description");
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

        // public InsertItem(Item item)
        // {
        //     if(item == null) return false;
        //     bool result = false;
        //     try
        //     {
        //         connection.Open();
        //         MySqlCommand command = connection.CreateCommand();
        //         command.Connection = connection;
        //         command.CommandText = @"insert into Items (item_name, item_brand, item_price, item_weight, item_quantity, item_category, item_description)
        //                                 values ('"+item.ItemName+"', '"+item.ItemBrand+"', "+item.ItemPrice+", '"+item.ItemWeight+"', "+item.ItemQuantity+", '""');";
                
        //     catch { }
        //     finally
        //     {
        //         connection.Close();
        //     }
        //     return result;
        // }
    }
}