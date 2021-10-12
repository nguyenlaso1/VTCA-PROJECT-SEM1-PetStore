using System;
using System.Collections.Generic;
using Persistence;
using DAL;

namespace BL
{
    public class InvoiceBl
    {
        private InvoiceDal inDal = new InvoiceDal();
        public bool CreateInvoice(Invoice invoice)
        {
            bool result = inDal.CreateInvoice(invoice);
            return result;
        }

    }
}
