using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAKAKO_ERP_3LAYER.DAL
{
    public class InvMS_DAL
    {
        private string companyCode;
        private string invoiceNo;
        private DateTime dateCreate;
        private string issuedToName;
        private string issuedToAddress;
        private string shipToName;
        private string shipToAddress;
        private string shipVia;
        private int freight;
        private string vessel;
        private string portLoading;
        private string portDestination;
        private DateTime eTD;
        private DateTime eTA;
        private string tradeCondition;
        private string paymentTerm;
        private string createBy;
        private DateTime createAt;
        private string editBy;
        private DateTime editAt;

        public string CompanyCode { get => companyCode; set => companyCode = value; }
        public string InvoiceNo { get => invoiceNo; set => invoiceNo = value; }
        public DateTime DateCreate { get => dateCreate; set => dateCreate = value; }
        public string IssuedToName { get => issuedToName; set => issuedToName = value; }
        public string IssuedToAddress { get => issuedToAddress; set => issuedToAddress = value; }
        public string ShipToName { get => shipToName; set => shipToName = value; }
        public string ShipToAddress { get => shipToAddress; set => shipToAddress = value; }
        public string ShipVia { get => shipVia; set => shipVia = value; }
        public int Freight { get => freight; set => freight = value; }
        public string Vessel { get => vessel; set => vessel = value; }
        public string PortLoading { get => portLoading; set => portLoading = value; }
        public string PortDestination { get => portDestination; set => portDestination = value; }
        public DateTime ETD { get => eTD; set => eTD = value; }
        public DateTime ETA { get => eTA; set => eTA = value; }
        public string TradeCondition { get => tradeCondition; set => tradeCondition = value; }
        public string PaymentTerm { get => paymentTerm; set => paymentTerm = value; }
        public string CreateBy { get => createBy; set => createBy = value; }
        public DateTime CreateAt { get => createAt; set => createAt = value; }
        public string EditBy { get => editBy; set => editBy = value; }
        public DateTime EditAt { get => editAt; set => editAt = value; }
    }
}
