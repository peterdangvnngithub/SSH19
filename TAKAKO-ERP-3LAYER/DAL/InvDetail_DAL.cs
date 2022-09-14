using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAKAKO_ERP_3LAYER.DAL
{
    public class InvDetail_DAL
    {
        private string invoiceNo;
        private string itemCode;
        private string itemName;
        private string refPoNo;
        private decimal qty;
        private int priceUnit;
        private decimal price;
        private decimal amount;
        private string createBy;
        private DateTime createAt;
        private string editBy;
        private DateTime editAt;

            public string InvoiceNo { get => invoiceNo; set => invoiceNo = value; }
            public string ItemCode { get => itemCode; set => itemCode = value; }
            public string ItemName { get => itemName; set => itemName = value; }
            public string RefPoNo { get => refPoNo; set => refPoNo = value; }
            public decimal Qty { get => qty; set => qty = value; }
            public int PriceUnit { get => priceUnit; set => priceUnit = value; }
            public decimal Price { get => price; set => price = value; }
            public decimal Amount { get => amount; set => amount = value; }
            public string CreateBy { get => createBy; set => createBy = value; }
            public DateTime CreateAt { get => createAt; set => createAt = value; }
            public string EditBy { get => editBy; set => editBy = value; }
            public DateTime EditAt { get => editAt; set => editAt = value; }
    }
}
