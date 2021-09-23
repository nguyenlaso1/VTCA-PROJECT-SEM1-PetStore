using System;
using Persistence;
using DAL;
using System.Collections.Generic;

namespace BL
{
    public class ItemBl
    {
        private ItemDal iDal = new ItemDal();
        public Item GetItemByID(string searchKeyWord, Item item){
            return iDal.GetItemByID(searchKeyWord, item);
        }
        public List<Item> GetItem(List<Item> itemL, string comm){
            return iDal.GetItem(itemL, comm);
        }
    }
}
