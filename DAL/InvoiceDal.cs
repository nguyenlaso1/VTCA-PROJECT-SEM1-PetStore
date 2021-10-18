using System;
using MySql.Data.MySqlClient;
using Persistence;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DAL
{
    public class InvoiceDal
    {
        private MySqlConnection connection = DbHelper.GetConnection();
        public bool CreateInvoice(Invoice invoice)
        {
            if (invoice == null || invoice.itemsList == null || invoice.itemsList.Count == 0) return false;
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
                bool check = false;
                if (invoice.InvoiceCustomer == null || invoice.InvoiceCustomer.CustomerName == null || invoice.InvoiceCustomer.CustomerName == "")
                {
                    invoice.InvoiceCustomer = new Customer() { CustomerId = 1 };
                }
                try
                {
                    Console.Write(" Nhap so dien thoai khach hang: ");
                    string customerNumberPhone = Console.ReadLine();
                    while (!(Regex.IsMatch(customerNumberPhone, @"^(0|\+84)\d{9}$")))
                    {
                        Console.WriteLine(" So dien thoai khong hop le!");
                        Console.Write(" Nhap so dien thoai khach hang: ");
                        customerNumberPhone = Console.ReadLine();
                    }


                    command.CommandText = "select *from Customers where customer_phonenumber = '" + customerNumberPhone + "';";
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Console.WriteLine(" Nguoi mua hang la khach hang cu!");
                        Console.WriteLine(" Ten khach hang: {0}", reader.GetString("customer_name"));
                        invoice.InvoiceCustomer.CustomerNumberPhone = reader.GetString("customer_phonenumber");
                        invoice.InvoiceCustomer.CustomerId = reader.GetInt32("customer_id");
                        invoice.InvoiceCustomer.CustomerName = reader.GetString("customer_name");
                        check = true;
                    }
                    reader.Close();

                    if (!check)
                    {
                        Console.Write(" Nhap ten khach hang: ");
                        string customerName = Console.ReadLine();
                        while (!(Regex.IsMatch(customerName, @"(^[A-Z,a-z]+$)|^([A-Z,a-z]+ *)+[A-Z,a-z]$")))
                        {
                            Console.WriteLine(" Ten khach hang khong hop le!");
                            Console.Write(" Nhap ten khach hang: ");
                            customerName = Console.ReadLine();
                        }
                        customerName = FixString(customerName);
                        invoice.InvoiceCustomer = new Customer { CustomerName = customerName, CustomerNumberPhone = customerNumberPhone };
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
                    }
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
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