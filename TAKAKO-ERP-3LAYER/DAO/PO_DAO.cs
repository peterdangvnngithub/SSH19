using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace TAKAKO_ERP_3LAYER.DAO
{
    public class PO_DAO
    {
        private DBConnection conn;

        /// <constructor>
        /// Constructor PO_DAO
        /// </constructor>
        public PO_DAO()
        {
            conn = new DBConnection();
        }

        public DataTable GetInfoPO(DateTime dueDateFrom
                                  ,DateTime dueDateTo
                                  ,string _customerCode
                                  ,string _receiveNo
                                  ,string _TVC_ItemCode
                                  ,string _customerPO
                                  ,string _shippingNo)
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT
                        	 PO_MS.COMPANY_CODE
                        	,PO_MS.CUSTOMER_CODE
                        	,PO_MS.RECEIVE_NO
                        	,PO_MS.TVC_ITEM_CODE
                        	,PO_MS.CUSTOMER_ITEM_CODE
                        	,PO_MS.PARTS_NAME
                        	,PO_MS.ITEM_NAME
                        	,PO_MS.QUANTITY
                        	,PO_MS.UNIT_CURRENCY
                        	,CASE
                        		WHEN UPPER(PO_MS.UNIT_CURRENCY) = 'JPY'
                        		THEN PO_MS.PRICE_JPY
                        		WHEN UPPER(PO_MS.UNIT_CURRENCY) = 'USD'
                        		THEN PO_MS.PRICE_USD
                        	END AS ORDER_PRICE
                        	,CASE
                        		WHEN SHIP_MS.LOCK_STATUS <> 2
                        		THEN ISNULL(SHIP_INV_DT.QUANTITY,0) * ISNULL(SHIP_INV_DT.ORDER_PRICE,0)
                        		WHEN SHIP_MS.LOCK_STATUS = 2
                        		THEN ISNULL(SHIP_INV_DT.QUANTITY_REVISE,0) * ISNULL(SHIP_INV_DT.ORDER_PRICE_REVISE,0)
                        	END AS AMOUNT
                        	,PO_MS.CUSTOMER_PO
                        	,PO_MS.ORDER_DATE
                        	,PO_MS.DUE_DATE
                        	,PO_MS.THIRD_PARTY_ITEM_CODE
                        	,PO_MS.THIRD_PARTY_PO
                        	,PO_MS.NOTE
                        	,SHIP_INV_DT.SHIPPING_NO
                        	,SHIP_MS.INVOICE_NO
                        	,SHIP_MS.DATE_CREATE
                            ,SHIP_MS.ETD                 AS  SAILING_DATE
                        	,CASE
                        		WHEN SHIP_MS.LOCK_STATUS    <> 2
                        		THEN SHIP_INV_DT.QUANTITY
                        		WHEN SHIP_MS.LOCK_STATUS    = 2
                        		THEN SHIP_INV_DT.QUANTITY_REVISE
                        	END AS QUANTITY_INVOICE
                        	,PO_MS.QUANTITY - (CASE
                        		WHEN SHIP_MS.LOCK_STATUS    <> 2
                        		THEN SHIP_INV_DT.QUANTITY
                        		WHEN SHIP_MS.LOCK_STATUS    = 2
                        		THEN SHIP_INV_DT.QUANTITY_REVISE
                        	END) AS BALANCE
                        FROM
                        	dbo.TVC_PO_MS PO_MS
                        INNER JOIN
                            (SELECT
	                        	 DISTINCT
	                        	 PO_MS_LJ.COMPANY_CODE
	                        	,PO_MS_LJ.CUSTOMER_CODE
	                        	,PO_MS_LJ.RECEIVE_NO
	                        	,PO_MS_LJ.TVC_ITEM_CODE
	                        	,PO_MS_LJ.CUSTOMER_PO
	                        	,PO_MS_LJ.DUE_DATE
                                ,SHIP_INV_DT_LJ.SHIPPING_NO
                                ,SHIP_INV_DT_LJ.QUANTITY
								,SHIP_INV_DT_LJ.QUANTITY_REVISE
								,SHIP_INV_DT_LJ.ORDER_PRICE
								,SHIP_INV_DT_LJ.ORDER_PRICE_REVISE
                                ,SHIP_INV_DT_LJ.BALANCE
	                        FROM
	                        	TVC_PO_MS PO_MS_LJ
	                        LEFT JOIN
	                        	TVC_SHIPPING_INV_DETAIL SHIP_INV_DT_LJ
	                        ON	PO_MS_LJ.CUSTOMER_CODE	=	SHIP_INV_DT_LJ.CUSTOMER_CODE
	                        AND	PO_MS_LJ.TVC_ITEM_CODE	=	SHIP_INV_DT_LJ.TVC_ITEM_CODE
	                        AND	PO_MS_LJ.CUSTOMER_PO	=	SHIP_INV_DT_LJ.CUSTOMER_PO
	                        AND	PO_MS_LJ.DUE_DATE		=	SHIP_INV_DT_LJ.DUE_DATE_PO
                            WHERE
                                (   PO_MS_LJ.DUE_DATE   >=  @DueDateFrom
                                AND PO_MS_LJ.DUE_DATE   <=  @DueDateTo)";
            if (!String.IsNullOrEmpty(_customerCode))
            {
                StrQuery += "       AND (PO_MS_LJ.[CUSTOMER_CODE]       =   @customerCode)";
            }
            if (!String.IsNullOrEmpty(_receiveNo))
            {
                StrQuery += "       AND (PO_MS_LJ.[RECEIVE_NO]          =   @receiveNo)";
            }
            if (!String.IsNullOrEmpty(_TVC_ItemCode))
            {
                StrQuery += "       AND (PO_MS_LJ.[TVC_ITEM_CODE]       =   @TVCItemCode)";
            }
            if (!String.IsNullOrEmpty(_customerPO))
            {
                StrQuery += "       AND (PO_MS_LJ.[CUSTOMER_PO]         =   @customerPO)";
            }
            if (!String.IsNullOrEmpty(_shippingNo))
            {
                StrQuery += "       AND (SHIP_INV_DT_LJ.[SHIPPING_NO]   =   @shippingNo)";
            }
                StrQuery += @") SHIP_INV_DT
                        ON  PO_MS.CUSTOMER_CODE     =   SHIP_INV_DT.CUSTOMER_CODE
                        AND PO_MS.RECEIVE_NO        =   SHIP_INV_DT.RECEIVE_NO
                        AND PO_MS.TVC_ITEM_CODE     =   SHIP_INV_DT.TVC_ITEM_CODE
                        AND PO_MS.CUSTOMER_PO       =   SHIP_INV_DT.CUSTOMER_PO
                        AND PO_MS.DUE_DATE          =   SHIP_INV_DT.DUE_DATE
                        LEFT JOIN
                        	TVC_SHIPPING_MS SHIP_MS
                        ON	SHIP_MS.SHIPPING_NO     =	SHIP_INV_DT.SHIPPING_NO
                        WHERE
                            (   PO_MS.DUE_DATE      >=  @DueDateFrom
                            AND PO_MS.DUE_DATE      <=  @DueDateTo)";
            if (!String.IsNullOrEmpty(_customerCode))
            {
                StrQuery += "       AND (PO_MS.[CUSTOMER_CODE]    = @customerCode)";
            }
            if (!String.IsNullOrEmpty(_receiveNo))
            {
                StrQuery += "       AND (PO_MS.[RECEIVE_NO]       = @receiveNo)";
            }
            if (!String.IsNullOrEmpty(_TVC_ItemCode))
            {
                StrQuery += "       AND (PO_MS.[TVC_ITEM_CODE]    = @TVCItemCode)";
            }
            if (!String.IsNullOrEmpty(_customerPO))
            {
                StrQuery += "       AND (PO_MS.[CUSTOMER_PO]      = @customerPO)";
            }
            if (!String.IsNullOrEmpty(_shippingNo))
            {
                StrQuery += "       AND (SHIP_MS.[SHIPPING_NO]    = @shippingNo)";
            }
            StrQuery += @"  ORDER BY
                                 PO_MS.[DUE_DATE] DESC";
            SqlParameter[] sqlParameters = new SqlParameter[7];
            sqlParameters[0] = new SqlParameter("@DueDateFrom", SqlDbType.Date);
            sqlParameters[0].Value = Convert.ToDateTime(dueDateFrom);
            sqlParameters[1] = new SqlParameter("@DueDateTo", SqlDbType.Date);
            sqlParameters[1].Value = Convert.ToDateTime(dueDateTo);
            sqlParameters[2] = new SqlParameter("@customerCode", SqlDbType.VarChar);
            sqlParameters[2].Value = Convert.ToString(_customerCode);
            sqlParameters[3] = new SqlParameter("@receiveNo", SqlDbType.VarChar);
            sqlParameters[3].Value = Convert.ToString(_receiveNo);
            sqlParameters[4] = new SqlParameter("@TVCItemCode", SqlDbType.VarChar);
            sqlParameters[4].Value = Convert.ToString(_TVC_ItemCode);
            sqlParameters[5] = new SqlParameter("@customerPO", SqlDbType.VarChar);
            sqlParameters[5].Value = Convert.ToString(_customerPO);
            sqlParameters[6] = new SqlParameter("@shippingNo", SqlDbType.VarChar);
            sqlParameters[6].Value = Convert.ToString(_shippingNo);
            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        /// <method>
        /// Get Info PO
        /// </method>
        public Boolean insertPO(DataTable _tempData)
        {
            return conn.insertPO(_tempData);
        }
    }
}
