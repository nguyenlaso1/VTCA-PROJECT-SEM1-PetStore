using System;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class StaffDal
    {
        private MySqlConnection connection = DbHelper.GetConnection();
        public Staff Login(Staff staff)
        {
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "select *from Staffs where staff_username = @userName and staff_password = @userPass;";
                    command.Parameters.AddWithValue("@userName", staff.UserName);
                    command.Parameters.AddWithValue("@userPass", Md5Algorithms.CreateMD5(staff.Password));
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        staff.Role = reader.GetInt32("staff_role");
                        staff.StaffName = reader.GetString("staff_name");
                        staff.StaffID = reader.GetInt32("staff_id");
                    }
                    else
                    {
                        staff.Role = 0;
                    }
                    reader.Close();
                }
                catch
                {
                    staff.Role = -1;
                }
                finally
                {
                    connection.Close();
                }
            }
            return staff;
        }

        public bool InsertStaff(string name, string userName, string pass, string numberPhone, string gmail)
        {
            bool result = false;
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.Connection = connection;
                MySqlDataReader reader = null;
                try
                {
                    command.CommandText = "select *from Staffs where staff_username = '" + userName + "'";
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        throw new Exception("Ten dang nhap da ton tai!");
                    }
                    reader.Close();

                    command.CommandText = "select *from Staffs where staff_phonenumber = '" + numberPhone + "'";
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        throw new Exception("So dien thoai da ton tai!");
                    }
                    reader.Close();

                    command.CommandText = "select *from Staffs where staff_email = '" + gmail + "'";
                    reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        throw new Exception("Gmail da ton tai!");
                    }
                    reader.Close();

                    command.CommandText = @"insert into Staffs (staff_name, staff_username, staff_password, staff_phonenumber, staff_gmail) 
                                        values('" + name + "', '" + userName + "', '" + Md5Algorithms.CreateMD5(pass) + "', '" + numberPhone + "', '" + gmail + "');";
                    command.ExecuteNonQuery();
                    result = true;
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            finally
            {
                connection.Close();
            }
            return result;
        }
    }
}