using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAKAKO_ERP_3LAYER.DAL
{
    public class PLDetail_DAL
    {
        private string invoiceNo;
        private decimal packageNo;
        private string itemCode;
        private decimal qtyCarton;
        private decimal qtyPerCarton;
        private decimal netWeight;
        private decimal grossWeight;
        private string lotNo;
        private string createBy;
        private DateTime createAt;
        private string editBy;
        private DateTime editAt;

        public string InvoiceNo { get => invoiceNo; set => invoiceNo = value; }
        public string ItemCode { get => itemCode; set => itemCode = value; }
        public decimal QtyCarton { get => qtyCarton; set => qtyCarton = value; }
        public decimal QtyPerCarton { get => qtyPerCarton; set => qtyPerCarton = value; }
        public decimal NetWeight { get => netWeight; set => netWeight = value; }
        public decimal GrossWeight { get => grossWeight; set => grossWeight = value; }
        public string LotNo { get => lotNo; set => lotNo = value; }
        public string CreateBy { get => createBy; set => createBy = value; }
        public DateTime CreateAt { get => createAt; set => createAt = value; }
        public string EditBy { get => editBy; set => editBy = value; }
        public DateTime EditAt { get => editAt; set => editAt = value; }
        public decimal PackageNo { get => packageNo; set => packageNo = value; }
    }
}
