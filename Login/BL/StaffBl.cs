using System.Threading.Tasks.Dataflow;
using System;
using Persistence;
using DAL;

namespace BL
{
    public class StaffBl
    {
        private StaffDal dal = new StaffDal();
        public int Login(Staff staff){
            return dal.Login(staff);
        }
    }
}
