using System;
using Persistence;
using DAL;

namespace BL
{
    public class StaffBl
    {
        private StaffDal dal = new StaffDal();
        public Staff Login(Staff staff){
            return dal.Login(staff);
        }

        public bool InsertStaff(string name, string userName, string pass, string numberPhone, string gmail){
            return dal.InsertStaff(name, userName, pass, numberPhone, gmail);
        }
    }
}
