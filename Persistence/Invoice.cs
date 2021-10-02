using System;
using System.Collections.Generic;

namespace Persistence
{
    public static class InvoiceStatus
    {
        public const int CREATE_NEW_INVOICE = 1;
        public const int INVOICE_INPROGRESS = 2;
    }

    public class Invoice
    {
        public int InvoiceId {set; get;}
        public DateTime InvoiceDate {set; get;}
        public Customer InvoiceCustomer {set; get;}
        public Staff InvoiceStaff {set; get;}
        public int? Status {set;get;}
        public List<Item> itemsList {set; get;}
        public double total;

        public Item this[int index]
        {
            get
            {
                if (itemsList == null || itemsList.Count == 0 || index < 0 || itemsList.Count < index) return null;
                return itemsList[index];
            }
            set
            {
                if (itemsList == null) itemsList = new List<Item>();
                itemsList.Add(value);
            }
        }

        public Invoice()
        {
            itemsList = new List<Item>();
        }  
    }
}

