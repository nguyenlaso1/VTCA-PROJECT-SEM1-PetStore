using System;
using MySql.Data.MySqlClient;
using Persistence;
using System.Collections.Generic;

namespace  DAL
{
    public class InvoiceDal
    {
        private MySqlConnection connection = DbHelper.GetConnection();
        public bool CreateInvoice(Invoice invoice)
        {
            if(invoice == null || invoice.itemsList == null || invoice.itemsList.Count == 0) return false;
            bool result = false;
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                command.CommandText = "lock tables Customers write, Invoices write, Items write, InvoiceDetails write;";
                command.ExecuteNonQuery();
                MySqlTransaction trans = connection.BeginTransaction();
                command.Transaction = trans;
                MySqlDataReader reader = null;
                try
                {
                    command.CommandText = @"insert into Customers(customer_name, customer_phonenumber)
                    values ('" + invoice.InvoiceCustomer.CustomerName + "','" + (invoice.InvoiceCustomer.CustomerNumberPhone ?? "") + "');";
                    command.ExecuteNonQuery();
                    command.CommandText = "select LAST_INSERT_ID() as customer_id;";
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                    invoice.InvoiceCustomer.CustomerId = reader.GetInt32("customer_id");
                    }
                    reader.Close();

                    command.CommandText = "insert into Invoices(staff_id, customer_id, invoice_status) values (@staffId, @customerId, @invoiceStatus);";
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@staffId", invoice.InvoiceStaff.StaffID);
                    command.Parameters.AddWithValue("@customerId", invoice.InvoiceCustomer.CustomerId);
                    command.Parameters.AddWithValue("@invoiceStatus", InvoiceStatus.CREATE_NEW_INVOICE);
                    command.ExecuteNonQuery();

                    command.CommandText = "select LAST_INSERT_ID() as invoice_id;";
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        invoice.InvoiceId = reader.GetInt32("invoice_id");
                    }
                    reader.Close();

                    command.CommandText = "SELECT invoice_date FROM Invoices ORDER BY invoice_id DESC LIMIT 1;";
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        invoice.InvoiceDate = reader.GetDateTime("invoice_date");
                    }
                    reader.Close();

                    foreach (Item item in invoice.itemsList)
                    {
                        command.CommandText = "select item_price from Items where item_id=@itemId;";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@itemId", item.ItemId);
                        reader = command.ExecuteReader();
                        if (!reader.Read())
                        {
                            throw new Exception("Khong ton tai san pham");
                        }
                        item.ItemPrice = reader.GetDouble("item_price");
                        reader.Close();

                        command.CommandText = @"insert into InvoiceDetails(invoice_id, item_id, unit_price, quantity) 
                                                values (" + invoice.InvoiceId + ", " + item.ItemId + ", " + item.ItemPrice + ", " + item.Quantity + ");";
                        command.ExecuteNonQuery();
                        command.CommandText = "update Items set item_quantity=item_quantity-@q where item_id=" + item.ItemId + ";";
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@q", item.Quantity);
                        command.ExecuteNonQuery();
                    }
                    trans.Commit();
                    result = true;
                }
                catch
                {
                    
                    try
                    {
                        trans.Rollback();
                    }
                    catch { }
                }
                finally
                {
                    command.CommandText = "unlock tables;";
                    command.ExecuteNonQuery();
                }
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