using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAKAKO_ERP_3LAYER.DAL;

namespace TAKAKO_ERP_3LAYER.DAO
{
    public class INV_DAO
    {
        private DBConnection conn;

        /// <constructor>
        /// Constructor PO_DAO
        /// </constructor>
        public INV_DAO()
        {
            conn = new DBConnection();
        }

        public DataTable GetInfoInv(string _invoiceNo)
        {
            string StrQuery = "";

            StrQuery = @"SELECT
                        		 I.[INVOICE_NO]
                        		,I.[PART_DESCRIPTION]
                        		,I.[ITEM_CODE]
                        		,I.[REF_PO_NO]
                        		,I.[QUANTITY]
                        		,I.[UNIT_CURRENCY]
                        		,I.[PRICE]
                        		,I.[AMOUNT]
                        FROM
                        		    [dbo].[TVC_INV_MS]		M
                        INNER JOIN	[dbo].[TVC_INV_DETAIL]	I
                        	ON	    M.INVOICE_NO = I.INVOICE_NO
                        WHERE
                        	        M.INVOICE_NO = CONCAT('',@invoiceNo,'')";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_invoiceNo);

            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable CheckInv(string _invoiceNo)
        {
            string StrQuery = "";

            StrQuery = @"SELECT
                        		 M.[COMPANY_CODE]
                        		,M.[INVOICE_NO]
                        		,M.[DATE_CREATE]
                        FROM
                                 [dbo].[TVC_INV_MS] M
                        WHERE
                                 M.INVOICE_NO = CONCAT('',@invoiceNo,'')";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_invoiceNo);

            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public Boolean insertInvoice(DataTable _invoiceMS
                                    ,DataTable _invoiceDetail
                                    ,DataTable _plDetail
                                    ,DataTable _shippingNo)
        {
            return conn.Update_InvoiceNo(_invoiceMS, _invoiceDetail, _plDetail, _shippingNo);
        }

        public DataTable GetInfo_Invoice(string _invoiceNo)
        {
            string StrQuery = "";

            StrQuery = @"SELECT 
                                     MS.[COMPANY_CODE]
                                    ,MS.[SHIPPING_NO]
                                    ,MS.[INVOICE_NO]
                                    ,MS.[DATE_CREATE]
                                    ,MS.[ISSUEDTO_CUSTOMER_CODE]
                                    ,MS.[ISSUEDTO_CUSTOMER_NAME]
                                    ,MS.[ISSUEDTO_CUSTOMER_ADDRESS]
                                    ,MS.[ISSUEDTO_CUSTOMER_TEL_NO]
                                    ,MS.[ISSUEDTO_CUSTOMER_FAX_NO]
                                    ,MS.[SHIPTO_CUSTOMER_CODE]
                                    ,MS.[SHIPTO_CUSTOMER_NAME]
                                    ,MS.[SHIPTO_CUSTOMER_ADDRESS]
                                    ,MS.[SHIPTO_CUSTOMER_TEL_NO]
                                    ,MS.[SHIPTO_CUSTOMER_FAX_NO]
                                    ,MS.[REVENUE]
                                    ,MS.[SHIP_VIA]
                                    ,MS.[FREIGHT]
                                    ,MS.[VESSEL]
                                    ,MS.[PORT_OF_LOADING]
                                    ,MS.[PORT_OF_DESTINATION]
                                    ,MS.[ETD]
                                    ,MS.[ETA]
                                    ,MS.[TRADE_CONDITION]
                                    ,MS.[PAYMENT_TERM]
                                    ,INV.[SHIPPING_NO]                      AS  INV_SHIPPING_NO
                                    ,INV.[CUSTOMER_CODE]                    AS  INV_CUSTOMER_CODE
                                    ,INV.[PART_DESCRIPTION]                 AS  INV_ITEM_NAME
                                    ,INV.[CUS_ITEM_CODE]                    AS  INV_CUS_ITEM_CODE
                                    ,INV.[TVC_ITEM_CODE]                    AS  INV_ITEM_CODE
                                    ,INV.[CUSTOMER_PO]                      AS  INV_REF_PO_NO
                                    ,INV.[THIRD_PARTY_PO]                   AS  THIRD_PARTY_PO
                                    ,INV.[ORDER_DATE]                       AS  ORDER_DATE
                                    ,INV.[DUE_DATE_PO]                      AS  DUE_DATE_PO
                                    ,ISNULL(INV.[QUANTITY],0)               AS  INV_QUANTITY
                                    ,ISNULL(INV.[QUANTITY_REVISE],0)        AS  INV_QUANTITY_REVISE
                                    ,ISNULL(INV.[BALANCE],0)                AS  INV_BALANCE
                                    ,INV.[UNIT_CURRENCY]                    AS  INV_UNIT_CURRENCY
                                    ,ISNULL(INV.[USD_RATE],0)               AS  INV_USD_RATE
                                    ,ISNULL(INV.[ORDER_PRICE],0)            AS  INV_ORDER_PRICE
                                    ,ISNULL(INV.[ORDER_PRICE_REVISE],0)     AS  INV_ORDER_PRICE_REVISE
                                    ,ISNULL(GLB.[PRICE],0)                  AS  GLOBAL_PRICE
                                    ,INV.[AMOUNT]                           AS  INV_AMOUNT
                                    ,MS.[CREATE_BY]
                        FROM 
                                    [dbo].[TVC_INV_MS] MS
                        INNER JOIN  [dbo].[TVC_INV_DETAIL]  INV
                            ON      MS.INVOICE_NO          =   INV.INVOICE_NO
                        LEFT JOIN	(
                                        SELECT DISTINCT
                                        	    GLB.GLOBAL_CODE
                                        	    ,PRICE_UNIT
                                        	    ,PRICE
                                        	    ,GLB.APPLYDATE
                                        FROM
                                        	    [dbo].[SPRICE_GLOBALMF] GLB
                                        JOIN
                                        	(
                                        		SELECT
                                                         GLOBAL_CODE
                                                        ,MAX(GLB.APPLYDATE) AS  APPLYDATE
                		                        FROM
                                                        [dbo].[SPRICE_GLOBALMF] GLB
						                        JOIN
                                                        TVC_INV_DETAIL DT
						                            ON  GLB.GLOBAL_CODE =	DT.CUS_ITEM_CODE
						                        JOIN
                                                        TVC_INV_MS MS
						                            ON  MS.SHIPPING_NO	=	DT.SHIPPING_NO
						                        WHERE
                                                        GLB.APPLYDATE   <=  MS.DATE_CREATE
                                                    AND GLB.INV_DV      <> '*'          --PRICE ACTIVE
                		                        GROUP BY
                                                    GLOBAL_CODE
                                        	) GLB_MAX
                                        ON
                                        	    GLB.GLOBAL_CODE		=	GLB_MAX.GLOBAL_CODE
                                            AND GLB.APPLYDATE		=	GLB_MAX.APPLYDATE
                                            AND GLB.INV_DV          <> '*'          --PRICE ACTIVE
                                        JOIN
                                        	    [dbo].[TVC_INV_DETAIL] INV
                                            ON  GLB.GLOBAL_CODE     =	INV.CUS_ITEM_CODE 
                                            AND	INV.UNIT_CURRENCY   =   GLB.PRICE_UNIT
                        				WHERE
                                            INV.INVOICE_NO          =   CONCAT('',@invoiceNo,'')
                                    ) GLB
                            ON  INV.CUS_ITEM_CODE   =   GLB.GLOBAL_CODE   
                            AND INV.UNIT_CURRENCY   =   GLB.PRICE_UNIT
                        WHERE
                                MS.INVOICE_NO       =   CONCAT('',@invoiceNo,'')";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_invoiceNo);

            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetInfo_ShipPL(string _shippingNo)
        {
            string StrQuery = "";

            StrQuery = @"SELECT 
                                     PL.[SHIPPING_NO]                       AS  PL_SHIPPING_NO
                                    ,PL.[CUSTOMER_CODE]	                    AS	PL_CUSTOMER_CODE
                                    ,PL.[PACKAGES_NO]	                    AS	PL_PACKAGES_NO
                                    ,PL.[CUS_ITEM_CODE]	                    AS	PL_ITEM_CODE
                                    ,PL.[TVC_ITEM_CODE]	                    AS	PL_TVC_ITEM_CODE
                                    ,PL.[CUSTOMER_PO]	                    AS	PL_CUSTOMER_PO
                                    ,ISNULL(PL.[QTY_CARTON],0)	    	    AS	PL_QTY_CARTON
                                    ,ISNULL(PL.[QTY_PER_CARTON],0)  	    AS	PL_QTY_PER_CARTON
                                    ,ISNULL(PL.[QTY_TOTAL], 0)	            AS	PL_QTY_TOTAL
                                    ,ISNULL(PL.[QTY_TOTAL_REVISE],0)        AS	PL_QTY_TOTAL_REVISE
                                    ,ISNULL(PL.[NET_WEIGHT],0)		        AS	PL_NET_WEIGHT
                                    ,ISNULL(PL.[NET_WEIGHT_TOTAL],0)		AS	PL_NET_WEIGHT_TOTAL
                                    ,ISNULL(PL.[GROSS_WEIGHT],0)	        AS	PL_GROSS_WEIGHT
                                    ,PL.[LOT_NO]			                AS	PL_LOT_NO
                        FROM 
                                    [dbo].[TVC_SHIPPING_MS] MS
                        INNER JOIN  [dbo].[TVC_SHIPPING_PL_DETAIL]  PL
                            ON      MS.SHIPPING_NO   =   PL.SHIPPING_NO
                        WHERE
                                    MS.SHIPPING_NO  =   CONCAT('',@shippingNo,'')";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@shippingNo", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_shippingNo);

            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetInfo_PL(string _invoiceNo)
        {
            string StrQuery = "";

            StrQuery = @"SELECT 
                                     PL.[SHIPPING_NO]                       AS  PL_SHIPPING_NO
                                    ,PL.[CUSTOMER_CODE]	                    AS	PL_CUSTOMER_CODE
                                    ,PL.[PACKAGES_NO]	                    AS	PL_PACKAGES_NO
                                    ,PL.[TVC_ITEM_CODE]	                    AS	PL_ITEM_CODE
                                    ,PL.[CUSTOMER_PO]	                    AS	PL_CUSTOMER_PO
                                    ,ISNULL(PL.[QTY_CARTON],0)	    	    AS	PL_QTY_CARTON
                                    ,ISNULL(PL.[QTY_PER_CARTON],0)  	    AS	PL_QTY_PER_CARTON
                                    ,ISNULL(PL.[QTY_TOTAL], 0)	            AS	PL_QTY_TOTAL
                                    ,ISNULL(PL.[QTY_TOTAL_REVISE],0)        AS	PL_QTY_TOTAL_REVISE
                                    ,ISNULL(PL.[NET_WEIGHT],0)		        AS	PL_NET_WEIGHT
                                    ,ISNULL(PL.[NET_WEIGHT_TOTAL],0)		AS	PL_NET_WEIGHT_TOTAL
                                    ,ISNULL(PL.[GROSS_WEIGHT],0)	        AS	PL_GROSS_WEIGHT
                                    ,PL.[LOT_NO]			                AS	PL_LOT_NO
                        FROM 
                                    [dbo].[TVC_INV_MS] MS
                        INNER JOIN  [dbo].[TVC_PL_DETAIL]  PL
                            ON      MS.INVOICE_NO   =   PL.INVOICE_NO
                        WHERE
                                    MS.INVOICE_NO  =   CONCAT('',@invoiceNo,'')";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@invoiceNo", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_invoiceNo);

            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_invoiceNo"></param>
        /// <returns></returns>
        public DataTable GetInvoice_ByShippingNo(DataTable _shippingNo)
        {
            return conn.executeSelectInv_ByShippingNo(_shippingNo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_shippingNo"></param>
        /// <returns></returns>
        public DataTable GetPL_ByShippingNo(DataTable _shippingNo)
        {
            return conn.executeSelectPL_ByShippingNo(_shippingNo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_invoiceNo"></param>
        /// <returns></returns>
        public bool ReviseInvoice(string _invoiceNo)
        {
            return conn.ReviseInvoice(_invoiceNo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_invoiceNo"></param>
        /// <returns></returns>
        public bool DeleteInvoice(string _invoiceNo, DataTable _tempListShipping)
        {
            return conn.DeleteInvoice(_invoiceNo, _tempListShipping);
        }
    }
}

