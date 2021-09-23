using System;
using MySql.Data.MySqlClient;
using Persistence;

namespace  DAL
{
    public class StaffDal{
        private MySqlConnection connection = DbHelper.GetConnection();
        public Staff Login(Staff staff){
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
                if(reader.Read()){
                    staff.Role = reader.GetInt32("staff_role");
                }
                else{
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
    }
}