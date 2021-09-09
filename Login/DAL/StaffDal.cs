using System.Xml.Linq;
using System;
using MySql.Data.MySqlClient;
using Persistence;

namespace  DAL
{
    public class StaffDal{
        public int Login(Staff staff){
            int login = 0;
            Console.WriteLine(staff.UserName + " - " + staff.Password);
            try{
                MySqlConnection connection = DbHelper.GetConnection();
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "select *from Staffs where staff_username='"+
                    staff.UserName+"' and staff_password='"+
                    Md5Algorithms.CreateMD5(staff.Password)+"';";
                MySqlDataReader reader = command.ExecuteReader();
                if(reader.Read()){
                    login = reader.GetInt32("role");
                }
                else{
                    login = 0;
                }
                reader.Close();
                connection.Close();
            }
            catch{
                login = -1;
            }
            Console.WriteLine(login);
            return login;
        }
    }
}