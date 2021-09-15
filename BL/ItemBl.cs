using System;
using Persistence;
using DAL;

namespace BL
{
    public class ItemBl
    {
        private ItemDal iDal = new ItemDal();
        public Item SearchByID(string searchKeyWord, Item item){
            return iDal.SearchByID(searchKeyWord, item);
        }
    }
}
