using System;
using Xunit;
using Persistence;
using DAL;

namespace DALTest
{
    public class StaffDalTest
    {
        private StaffDal dal = new StaffDal();
        [Fact]
        public void LoginTest1()
        {
            Staff staff = new Staff(){UserName ="vanan2002", Password = "PetStore123"};
            int expected = 2;
            int result = dal.Login(staff).Role;
            Assert.True(expected == result);
        }

        [Theory]
        [InlineData("pf15", "PF15VTCAcademy", 0)]
        [InlineData("nguyen2504", "NguyenNguyen123", 1)]
        [InlineData("vanan2002", "PetStore1234", 0)]
        [InlineData("vanann2002", "PetStore123", 0)]
        [InlineData("vannan2003", "PetStore123", 0)]
        [InlineData("vanan2002", "Petstore123", 0)]
        [InlineData("vanan2002", "petStore123", 0)]
        [InlineData("nguyen2505", "NguyenNguyen123", 0)]
        [InlineData("vanan2002", "PetStore2002", 0)]
        [InlineData("vanan123", "PetStore123", 0)]
        [InlineData("nguyen2525", "PetStore123", 0)]
        [InlineData("nguyen2504", "nguyennguyen123", 0)]
        public void LoginTest2(string userName, string password, int expected){
            Staff staff = new Staff(){UserName = userName, Password = password};
            int result = dal.Login(staff).Role;
            Assert.True(expected == result);
        }
    }
}
