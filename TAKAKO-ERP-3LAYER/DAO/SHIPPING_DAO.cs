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
    public class SHIPPING_DAO
    {
        private DBConnection conn;

        /// <constructor>
        /// Constructor PO_DAO
        /// </constructor>
        public SHIPPING_DAO()
        {
            conn = new DBConnection();
        }

        public DataTable Check_Shipping(string _shippingNo)
        {
            string StrQuery = "";

            StrQuery = @"SELECT
                                 MS.[COMPANY_CODE]
                        		,MS.[SHIPPING_NO]
                                ,MS.[CREATE_BY]
                        FROM    [dbo].[TVC_SHIPPING_MS] MS
                        WHERE   MS.SHIPPING_NO = CONCAT('',@shippingNo,'')";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@shippingNo", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_shippingNo);

            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public Boolean insertShipping(DataTable _invoiceMS
                                    , DataTable _invoiceDetail_Init
                                    , DataTable _invoiceDetail
                                    , DataTable _plDetail)
        {
            return conn.Update_ShippingNo(_invoiceMS, _invoiceDetail_Init, _invoiceDetail, _plDetail);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_shippingNo"></param>
        /// <returns></returns>
        public DataTable GetHeader_ShipInv(string _shippingNo)
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
                                ,MS.[CREATE_BY]
                        FROM 
                                [dbo].[TVC_SHIPPING_MS] MS
                        WHERE
                                MS.SHIPPING_NO   =   CONCAT('',@shippingNo,'')";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@shippingNo", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_shippingNo);

            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_shippingNo"></param>
        /// <returns></returns>
        public List<TVC_SHIPPING_INV_DETAIL> GetDetail_ShipInv(string _shippingNo)
        {
            string StrQuery = "";
            using (Takako_Entities db = new Takako_Entities())
            {
                StrQuery =
                @"SELECT 
                         INV.[INV_DETAIL_NUM]
                        ,INV.[COMPANY_CODE]
                        ,INV.[CUSTOMER_CODE]
                        ,INV.[SHIPPING_NO]
                        ,INV.[INVOICE_NO]
                        ,INV.[REVISE_NO]
                        ,INV.[REVISE_DATE]
                        ,INV.[REVISE_VERSION]
                        ,INV.[PART_DESCRIPTION]
                        ,INV.[CUS_ITEM_CODE]
                        ,INV.[TVC_ITEM_CODE]
                        ,INV.[CUSTOMER_PO]
                        ,INV.[THIRD_PARTY_ITEM_CODE]
                        ,INV.[THIRD_PARTY_PO]
                        ,INV.[ORDER_DATE]
                        ,INV.[DUE_DATE_PO]
                        ,ISNULL(INV.[QUANTITY],0)               AS  QUANTITY
                        ,ISNULL(INV.[QUANTITY_REVISE],0)        AS  QUANTITY_REVISE
                        ,ISNULL(INV.[BALANCE],0)                AS  BALANCE
                        ,INV.[UNIT_CURRENCY]
                        ,ISNULL(INV.[USD_RATE],0)               AS  USD_RATE
                        ,ISNULL(INV.[ORDER_PRICE],0)            AS  ORDER_PRICE
                        ,ISNULL(INV.[ORDER_PRICE_REVISE],0)     AS  ORDER_PRICE_REVISE
                        ,ISNULL(GLB.[PRICE],0)                  AS  GLOBAL_PRICE
                        ,INV.[AMOUNT]
                        ,INV.[NOTE]
                        ,INV.[CREATE_BY]
                        ,INV.[CREATE_AT]
                        ,INV.[EDIT_BY]
                        ,INV.[EDIT_AT]
                FROM 
                        [dbo].[TVC_SHIPPING_INV_DETAIL]  INV
                LEFT JOIN (
                        SELECT DISTINCT
                        	     GLB.GLOBAL_CODE
                        	    ,PRICE_UNIT
                        	    ,PRICE
                        	    ,GLB.APPLYDATE
                        FROM
                        	    [dbo].[SPRICE_GLOBALMF] GLB
                        INNER JOIN (
                                SELECT
                                     GLOBAL_CODE
                                    ,MAX(GLB.APPLYDATE) AS  APPLYDATE
                                FROM
                                    [dbo].[SPRICE_GLOBALMF] GLB
				                INNER JOIN
                                    TVC_SHIPPING_INV_DETAIL DT
				                ON  GLB.GLOBAL_CODE =	DT.CUS_ITEM_CODE
				                WHERE
                                    GLB.APPLYDATE   <=  GETDATE()
                                AND GLB.INV_DV      <> '*'          --PRICE ACTIVE
                                AND DT.SHIPPING_NO  =   CONCAT('',@shippingNo,'')
                                GROUP BY
                                    GLOBAL_CODE
                        ) GLB_MAX
                        ON
                        	GLB.GLOBAL_CODE	=	GLB_MAX.GLOBAL_CODE
                        AND	GLB.APPLYDATE	=	GLB_MAX.APPLYDATE
                        WHERE
                            GLB.INV_DV      <> '*'
                ) GLB
                ON      INV.CUS_ITEM_CODE   =   GLB.GLOBAL_CODE   
                AND     INV.UNIT_CURRENCY   =   GLB.PRICE_UNIT
                WHERE
                        INV.SHIPPING_NO     =   @shippingNo";

                List<TVC_SHIPPING_INV_DETAIL> result = db.Database.SqlQuery<TVC_SHIPPING_INV_DETAIL>(StrQuery, new SqlParameter("@shippingNo",_shippingNo)).ToList();

                return result;
            }
        }

        public DataTable GetDetail_ShipPL(string _shippingNo)
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
        /// <param name="_shippingNo"></param>
        /// <returns></returns>
        public bool Lock_ShippingInstruction(string _shippingNo)
        {
            return conn.Lock_ShippingInstruction(_shippingNo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_shippingNo"></param>
        /// <returns></returns>
        public bool UnLock_ShippingInstruction(string _shippingNo)
        {
            return conn.UnLock_ShippingInstruction(_shippingNo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_shippingNo"></param>
        /// <returns></returns>
        public bool Revise_ShippingInstruction(string _shippingNo)
        {
            return conn.Revise_ShippingInstruction(_shippingNo);
        }
    }
}

