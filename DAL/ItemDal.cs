using System.Data;
using System;
using MySql.Data.MySqlClient;
using Persistence;
using System.Collections.Generic;

namespace DAL
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
                    command.CommandText = "SELECT Items.item_id, Items.item_name, Items.item_price, Items.item_quantity, Items.item_weight, Items.item_description, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id WHERE Items.item_id = " + searchKeyWord + ";";
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
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
                    while (reader.Read())
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

        public List<Category> GetCategory()
        {
            lock (connection)
            {
                List<Category> cL = new List<Category>();
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "select *from Categories";
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Category c = new Category();
                        c.CategoryId = reader.GetInt32("category_id");
                        c.CategoryName = reader.GetString("category_name");
                        cL.Add(c);
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
                return cL;
            }
        }

        public bool InsertItem(string name, double price, string brandName, int category, int quantity, string weight, string description)
        {
            bool result = false;
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                // command.CommandText = "lock tables Brands write;";
                // command.ExecuteNonQuery();
                // MySqlTransaction trans = connection.BeginTransaction();
                // command.Transaction = trans;
                MySqlDataReader reader = null;
                try
                {
                    int? brandId = null;
                    bool checkBrand = false;
                    command.CommandText = "select *from Brands where brand_name = '" + brandName + "';";
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        brandId = reader.GetInt32("brand_id");
                        checkBrand = true;
                    }
                    reader.Close();
                    if (!checkBrand)
                    {
                        command.CommandText = "insert into Brands (brand_name) values('" + brandName + "');";
                        command.ExecuteNonQuery();
                        command.CommandText = "select LAST_INSERT_ID() as brand_id;";
                        reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            brandId = reader.GetInt32("brand_id");
                        }
                        reader.Close();
                    }
                    command.CommandText = @"insert into Items (item_name, item_price, item_brand, item_category, item_quantity, item_weight, item_description) 
                                        values('" + name + "', " + price + ", " + brandId + ", " + category + ", " + quantity + ", '" + weight + "', '" + description + "');";
                    command.ExecuteNonQuery();
                    result = true;
                }
                catch { }
            }
            catch { }
            finally
            {
                connection.Close();
            }
            return result;
        }
    }
}