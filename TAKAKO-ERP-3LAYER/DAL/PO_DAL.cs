using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAKAKO_ERP_3LAYER.DAL
{
    public class PO_DAL
    {
        private string _companyCode;

        private string _receiveNo;

        private string _pOCode;

        private string _tVCCode;

        private string _tSPCode;

        private string _name1;

        private string _name2;

        private string _quantity;

        private string _priceJPY;

        private string _priceUSD;

        private string _pO_TSP;

        private DateTime _orderDate;

        private DateTime _dueDate;

        private string _customerCode;

        private string _pONoCustomer;

        private string _comment;

        private string _createBy;

        private DateTime _createAt;

        private string _editBy;

        private DateTime _editAt;

        //
        public string CompanyCode { get => _companyCode; set => _companyCode = value; }
        public string ReceiveNo { get => _receiveNo; set => _receiveNo = value; }
        public string POCode { get => _pOCode; set => _pOCode = value; }
        public string TVCCode { get => _tVCCode; set => _tVCCode = value; }
        public string TSPCode { get => _tSPCode; set => _tSPCode = value; }
        public string Name1 { get => _name1; set => _name1 = value; }
        public string Name2 { get => _name2; set => _name2 = value; }
        public string Quantity { get => _quantity; set => _quantity = value; }
        public string PriceJPY { get => _priceJPY; set => _priceJPY = value; }
        public string PriceUSD { get => _priceUSD; set => _priceUSD = value; }
        public string PO_TSP { get => _pO_TSP; set => _pO_TSP = value; }
        public DateTime OrderDate { get => _orderDate; set => _orderDate = value; }
        public DateTime DueDate { get => _dueDate; set => _dueDate = value; }
        public string CustomerCode { get => _customerCode; set => _customerCode = value; }
        public string PONoCustomer { get => _pONoCustomer; set => _pONoCustomer = value; }
        public string Comment { get => _comment; set => _comment = value; }
        public string CreateBy { get => _createBy; set => _createBy = value; }
        public DateTime CreateAt { get => _createAt; set => _createAt = value; }
        public string EditBy { get => _editBy; set => _editBy = value; }
        public DateTime EditAt { get => _editAt; set => _editAt = value; }
    }
}
