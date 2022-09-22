using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TAKAKO_ERP_3LAYER
{
    public static class Common
    {
        //Form Search 
        public class CompanyInfo
        {
            public string CompanyCode { set; get; }
            public string CompanyName { set; get; }
            public string CompanyAddress { set; get; }
            public string CompanyTelNo { set; get; }
            public string CompanyFaxNo { set; get; }
            public string InvoiceFormat { set; get; }
        }

        public class POInfo
        {
            public DateTime DueDate_To { set; get; }
            public DateTime DueDate_From { set; get; }
            public string ReceiveNo { set; get; }
        }

        public class ShippingInfo
        {
            public DateTime DateCreate { set; get; }
            public DateTime ETD { set; get; }
            public DateTime Due_Date_From { set; get; }
            public DateTime Due_Date_To { set; get; }
            public string ShippingNo { set; get; }
            public string InvoiceNo { set; get; }
            public string Lock_Status { set; get; }
        }

        public class InvoiceInfo
        {
            public DateTime DateCreate { set; get; }
            public DateTime ETD { set; get; }
            public DateTime Due_Date_From { set; get; }
            public DateTime Due_Date_To { set; get; }
            public string Unit_Currency { set; get; }
            public string Shipping_No { set; get; }
            public string InvoiceNo { set; get; }
            public string LockStatus { set; get; }
        }

        public class PriceConditionInfo
        {
            public string PriceCondition { set; get; }
        }

        public class PaymentTermInfo
        {
            public string PaymentID { set; get; }
        }

        public class DestinationInfo
        {
            public string DestinationID { set; get; }
        }

        public class ItemCodeInfo
        {
            public string CustomerCode { set; get; }
            public string ReceiveNo { set; get; }
            public string Customer_ItemCode { set; get; }
            public string TVC_ItemCode { set; get; }
            public string Item_Name { set; get; }
            public decimal BoxQuantity { set; get; }
            public decimal Weight { set; get; }
            public string CustomerPO { set; get; }
            public string ThirdPartyItemCode { set; get; }
            public string ThirdPartyPO { set; get; }
            public string OrderUnitCurrency { set; get; }
            public decimal Balance { set; get; }
            public decimal OrderPrice { set; get; }
            public decimal CustomerUnitCurrency { set; get; }
            public decimal CustomerPrice { set; get; }
            public DateTime Order_Date { set; get; }
            public DateTime DueDate_PO { set; get; }
        }

        public class SearchPOInfo
        {
            public string COMPANY_CODE { set; get; }
            public string CUSTOMER_CODE { set; get; }
            public string RECEIVE_NO { set; get; }
            public string CUSTOMER_ITEM_CODE { set; get; }
            public string TVC_ITEM_CODE { set; get; }
            public string ITEM_NAME { set; get; }
            public DateTime DUE_DATE { set; get; }
            public DateTime ORDER_DATE { set; get; }
            public int BALANCE { set; get; }
            public decimal BOX_QUANTITY { set; get; }
            public decimal WEIGHT { set; get; }
            public string CUSTOMER_PO { set; get; }
            public string THIRD_PARTY_ITEM_CODE { set; get; }
            public string THIRD_PARTY_PO { set; get; }
            public string UNIT_CURRENCY { set; get; }
            public decimal ORDER_PRICE { set; get; }
            public decimal PRICE { set; get; }
            public string NOTE { set; get; }
        }

        public static List<ItemCodeInfo> _listItemCodeInfo;

        public static List<ShippingInfo> _listShippingInfo;

        public enum EnumCompanyCode {
             WholeCompany = 0
            ,TVC1 = 1
            ,TVC2 = 2
        };

        public enum EnumShippingStatus
        {
             Normal = 0
            ,Lock = 1
            ,Revise = 2
        };

        public enum EnumInvoiceStatus
        {
             Normal = 0
            ,Revise = 1
        };
    }
}
