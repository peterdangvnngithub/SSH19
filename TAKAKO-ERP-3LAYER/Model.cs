using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAKAKO_ERP_3LAYER
{
    public class Model
    {
        public class Search_Shipping
        {
            public string ISSUEDTO_CUSTOMER_CODE { set; get; }
            public string SHIPTO_CUSTOMER_CODE { set; get; }
            public string SHIPPING_NO { set; get; }
            public string INVOICE_NO { set; get; }
            public string LOCK_STATUS { set; get; }
            public string UNIT_CURRENCY { set; get; }
            public DateTime ETD { set; get; }
            public DateTime DateCreate { set; get; }
            public decimal AMOUNT { set; get; }
        }
    }
}
