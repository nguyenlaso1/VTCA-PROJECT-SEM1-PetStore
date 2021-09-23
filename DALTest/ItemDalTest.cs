using System.Collections.Generic;
using System.Reflection;
using System;
using Xunit;
using Persistence;
using DAL;

namespace DALTest
{
    public class ItemDalTest
    {
        private ItemDal iDal = new ItemDal();
        [Theory]
        [InlineData("123456", 0)]
        [InlineData("1", 1)]
        [InlineData("987456", 0)]
        [InlineData("2", 2)]
        [InlineData("nguyennguyen", 0)]
        [InlineData("3", 3)]
        [InlineData("ngoquangnguyen", 0)]
        [InlineData("maivanan", 0)]
        [InlineData("4", 4)]
        [InlineData("0", 0)]
        [InlineData("-1", 0)]
        [InlineData("5", 5)]
        [InlineData("-123456", 0)]
        [InlineData("-987654123", 0)]
        [InlineData("6", 6)]
        [InlineData("10", 10)]
        [InlineData("11", 11)]
        [InlineData("20", 20)]
        [InlineData("22", 22)]
        [InlineData("!@#EWE", 0)]
        [InlineData(" ", 0)]
        [InlineData("25", 25)]
        public void GetItemByIDTest(string searchKeyWord, int expected){
            Item item = new Item();
            int result = iDal.GetItemByID(searchKeyWord, item).ItemId;
            Assert.True(expected == result);
        }

        [Theory]
        [InlineData("Thuc an", "item_name", 12)]
        public void GetItem(string searchKeyWord, string searchType, int expected)
        {
            List<Item> list = new List<Item>();
            list = iDal.GetItem(list, searchType, searchKeyWord);
            if (expected == 0) Assert.True(list.Count == 0);
            else
            {
                Assert.True(list != null);
                Assert.True(list.Count > 0);
                foreach (Item i in list)
                {
                    Assert.True(i.ItemName.ToLower().Contains(searchKeyWord.ToLower()));
                }
            }
        }
    }
}