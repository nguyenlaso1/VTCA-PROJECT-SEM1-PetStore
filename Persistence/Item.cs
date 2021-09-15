using System.Runtime.Intrinsics.X86;
using System;

namespace Persistence
{
    public class Item
    {
        public int ItemId {get; set;}
        public string ItemName {get; set;}
        public string ItemWeight {get; set;}
        public string ItemDescription {get; set;}
        public double ItemPrice {get; set;}
        public string ItemBrand {get; set;}
        public int ItemQuantity {get; set;}
        public string ItemCategory {get; set;}
    }
}