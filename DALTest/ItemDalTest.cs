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
        string commSearch;
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
        [InlineData("Thuc an", 12, 1)]
        [InlineData("meo", 35, 1)]
        [InlineData("cho", 49, 1)]
        [InlineData("paw", 10, 1)]
        [InlineData("pate", 6, 1)]
        [InlineData("xuong", 1, 1)]
        [InlineData("phu kien", 0, 1)]
        [InlineData("2", 7, 1)]
        [InlineData("10", 5, 1)]
        [InlineData("nguyen", 0, 1)]
        [InlineData("an", 37, 1)]
        [InlineData("hat", 1, 1)]
        [InlineData("tui", 2, 1)]
        [InlineData("phan", 3, 1)]
        [InlineData("kem", 2, 1)]
        [InlineData("kep", 1, 1)]
        [InlineData("xit", 6, 1)]
        [InlineData("huong", 2, 1)]
        [InlineData("dau", 0, 1)]
        [InlineData("socola", 0, 1)]
        [InlineData("meo", 0, 2)]
        [InlineData("cho", 0, 2)]
        [InlineData("paw", 0, 2)]
        [InlineData("pate", 0, 2)]
        [InlineData("xuong", 0, 2)]
        [InlineData("2", 0, 2)]
        [InlineData("10", 0, 2)]
        [InlineData("nguyen", 0, 2)]
        [InlineData("merial", 0, 2)]
        [InlineData("bioline", 0, 2)]
        [InlineData("thuoc", 5, 2)]
        [InlineData("kep", 0, 2)]
        [InlineData("xit", 0, 2)]
        [InlineData("huong", 0, 2)]
        [InlineData("dau", 0, 2)]
        [InlineData("socola", 0, 2)]
        [InlineData("Thuc an", 0, 3)]
        [InlineData("canin", 12, 3)]
        [InlineData("cho", 0, 3)]
        [InlineData("paw", 9, 3)]
        [InlineData("royal", 12, 3)]
        [InlineData("xuong", 0, 3)]
        [InlineData("phu kien", 0, 3)]
        [InlineData("2", 0, 3)]
        [InlineData("tropi", 2, 3)]
        [InlineData("royal canin", 12, 3)]
        [InlineData("merial", 5, 3)]
        [InlineData("bioline", 1, 3)]
        [InlineData("vat dung", 0, 3)]
        [InlineData("joyce", 5, 3)]
        [InlineData("xit", 0, 3)]
        [InlineData("huong", 0, 3)]
        [InlineData("dolls", 5, 3)]
        [InlineData("trixie", 4, 3)]
        [InlineData("bobo", 2, 3)]
        [InlineData("aupet", 2, 3)]
        [InlineData("bio", 1, 3)]
        [InlineData("brand", 1, 3)]
        [InlineData("vege", 1, 3)]
        public void GetItem(string searchKeyWord, int expected, int searchType)
        {
            int d = 0;
            if(searchType == 1) commSearch = "SELECT Items.item_id, Items.item_name, Items.item_price, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id WHERE Items.item_name like '%"+searchKeyWord+"%';";
            else if(searchType == 2) commSearch = "SELECT Items.item_id, Items.item_name, Items.item_price, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id WHERE Categories.category_name like '%"+searchKeyWord+"%';";
            else if(searchType == 3) commSearch = "SELECT Items.item_id, Items.item_name, Items.item_price, Brands.brand_name, Categories.category_name FROM Items INNER JOIN Categories ON Items.item_category = Categories.category_id INNER JOIN Brands ON Items.item_brand = Brands.brand_id WHERE Brands.brand_name like '%"+searchKeyWord+"%';";
            List<Item> list = new List<Item>();
            list = iDal.GetItem(list, commSearch);
            if (expected == 0) Assert.True(list.Count == 0);
            else
            {
                Assert.True(list != null);
                Assert.True(list.Count > 0);
                foreach (Item i in list)
                {
                    if(i.ItemName.ToLower().Contains(searchKeyWord.ToLower())) d += 1;
                }
                Assert.True(expected == d);
            }
        }
    }
}