using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAKAKO_ERP_3LAYER.DAO
{
    public class EXPORT_DAO
    {
        private DBConnection conn;

        /// <constructor>
        /// Constructor EXPORT_DAO
        /// </constructor>
        public EXPORT_DAO()
        {
            conn = new DBConnection();
        }

        public DataTable GetTotalData_Revenue(String _revenue)
        {
            string StrQuery = "";

            StrQuery = @"SELECT
                        	 INV_DT.CUSTOMER_CODE
                            ,CU_MS.CUSTOMER_NAME1
                        	,SUM(INV_DT.QUANTITY) AS  SUM_QUANTITY
                        	,SUM(CASE
                        			WHEN INV_DT.UNIT_CURRENCY ='JPY' AND PO_MS.COMPANY_CODE = '00001'
                        			THEN INV_DT.AMOUNT 
                        			WHEN INV_DT.UNIT_CURRENCY ='USD' AND PO_MS.COMPANY_CODE = '00001'
                        			THEN INV_DT.AMOUNT * INV_DT.USD_RATE
                        			ELSE 0 
                        		END) AS  SUM_JPY_TVC1
                        	,SUM(CASE
                        			WHEN INV_DT.UNIT_CURRENCY ='JPY' AND PO_MS.COMPANY_CODE = '00002'
                        			THEN INV_DT.AMOUNT 
                        			WHEN INV_DT.UNIT_CURRENCY ='USD' AND PO_MS.COMPANY_CODE = '00002'
                        			THEN INV_DT.AMOUNT * INV_DT.USD_RATE
                        			ELSE 0
                        		END) AS  SUM_JPY_TVC2
                        FROM
                        	TVC_INV_DETAIL INV_DT
                        INNER JOIN
                        	TVC_INV_MS INV_MS
                        ON  INV_MS.INVOICE_NO   = INV_DT.INVOICE_NO
                        INNER JOIN
                        	TVC_PO_MS PO_MS
                        ON	PO_MS.TVC_ITEM_CODE = INV_DT.TVC_ITEM_CODE
                        AND	PO_MS.CUSTOMER_PO	= INV_DT.CUSTOMER_PO
                        AND	PO_MS.DUE_DATE		= INV_DT.DUE_DATE_PO
                        LEFT JOIN
                            CUSTOMMF CU_MS
                        ON  INV_DT.CUSTOMER_CODE = CU_MS.CUSTOMER_CODE
                        WHERE
                            INV_MS.REVENUE      =   @revenue
                        GROUP BY
                        	 INV_DT.CUSTOMER_CODE
                        	,CU_MS.CUSTOMER_NAME1
                        ORDER BY
                             INV_DT.CUSTOMER_CODE";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@revenue", SqlDbType.NVarChar);
            sqlParameters[0].Value = Convert.ToString(_revenue);

            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetDetailData_Revenue(String _revenue)
        {
            string StrQuery = "";

            StrQuery = @"SELECT
                        	 PO_MS.COMPANY_CODE
                        	,INV_MS.INVOICE_NO
                        	,INV_MS.SHIPPING_NO
                        	,INV_MS.DATE_CREATE
                        	,INV_MS.ETD
                            ,INV_MS.REVENUE
                        	,INV_DT.CUSTOMER_CODE
                            ,INV_DT.UNIT_CURRENCY
                        	,INV_DT.USD_RATE
                            ,INV_DT.NOTE
                        	,SUM(INV_DT.AMOUNT) AS  AMOUNT
                        FROM
                        	TVC_INV_DETAIL INV_DT
                        INNER JOIN
                        	TVC_INV_MS INV_MS
                        ON
                        	INV_MS.INVOICE_NO   = INV_DT.INVOICE_NO
                        INNER JOIN
                        	TVC_PO_MS PO_MS
                        ON	PO_MS.TVC_ITEM_CODE = INV_DT.TVC_ITEM_CODE
                        AND	PO_MS.CUSTOMER_PO	= INV_DT.CUSTOMER_PO
                        AND	PO_MS.DUE_DATE		= INV_DT.DUE_DATE_PO
                        WHERE
                            INV_MS.REVENUE      =  @revenue
                        GROUP BY
                        	 PO_MS.COMPANY_CODE
                        	,INV_MS.INVOICE_NO
                        	,INV_MS.SHIPPING_NO
                        	,INV_MS.DATE_CREATE
                        	,INV_MS.ETD
                            ,INV_MS.REVENUE
                        	,INV_DT.CUSTOMER_CODE
                            ,INV_DT.UNIT_CURRENCY
                        	,INV_DT.USD_RATE
                            ,INV_DT.NOTE
                        ORDER BY
                             INV_DT.CUSTOMER_CODE
                        	,INV_MS.INVOICE_NO
                        	,PO_MS.COMPANY_CODE";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@revenue", SqlDbType.NVarChar);
            sqlParameters[0].Value = Convert.ToString(_revenue);

            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetData_InternalACC(string _revenue)
        {
            string StrQuery = "";

            StrQuery = @"SELECT
                        	 PO_MS.COMPANY_CODE
                        	,INV_MS.INVOICE_NO
                        	,INV_MS.SHIPPING_NO
                        	,INV_MS.DATE_CREATE
                        	,INV_MS.ETD
                        	,INV_DT.CUSTOMER_CODE
                            ,INV_DT.UNIT_CURRENCY
                        	,INV_DT.USD_RATE
                        	,SUM(INV_DT.AMOUNT) AS  AMOUNT
                        FROM
                        	TVC_INV_DETAIL INV_DT
                        INNER JOIN
                        	TVC_INV_MS INV_MS
                        ON
                        	INV_MS.INVOICE_NO   = INV_DT.INVOICE_NO
                        INNER JOIN
                        	TVC_PO_MS PO_MS
                        ON	PO_MS.TVC_ITEM_CODE = INV_DT.TVC_ITEM_CODE
                        AND	PO_MS.CUSTOMER_PO	= INV_DT.CUSTOMER_PO
                        AND	PO_MS.DUE_DATE		= INV_DT.DUE_DATE_PO
                        WHERE
                            INV_MS.REVENUE      =  @revenue
                        GROUP BY
                        	 PO_MS.COMPANY_CODE
                        	,INV_MS.INVOICE_NO
                        	,INV_MS.SHIPPING_NO
                        	,INV_MS.DATE_CREATE
                        	,INV_MS.ETD
                        	,INV_DT.CUSTOMER_CODE
                            ,INV_DT.UNIT_CURRENCY
                        	,INV_DT.USD_RATE
                        ORDER BY
                             INV_DT.CUSTOMER_CODE
                        	,INV_MS.INVOICE_NO
                        	,PO_MS.COMPANY_CODE";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@revenue", SqlDbType.NVarChar);
            sqlParameters[0].Value = Convert.ToString(_revenue);

            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetData_TechnicalSupport(DateTime _dueDateFrom
                                                , DateTime _dueDateTo)
        {
            string StrQuery = "";

            StrQuery = @"SELECT
                        		 MS.DATE_CREATE		AS	DATE_CREATE
                        		,MS.INVOICE_NO		AS	INVOICE_NO
                        		,DT.TVC_ITEM_CODE
                        		,DT.PART_DESCRIPTION
                        		,CASE
                        			WHEN MS.LOCK_STATUS = 0 THEN DT.QUANTITY ELSE DT.QUANTITY_REVISE
                        		END AS	QUANTITY
                        		,CASE
                        			WHEN MS.LOCK_STATUS = 0 THEN DT.ORDER_PRICE ELSE DT.ORDER_PRICE_REVISE
                        		END AS	ORDER_PRICE
                        FROM
                        		TVC_INV_MS MS
                        INNER JOIN
                        		TVC_INV_DETAIL DT
                        ON
                        		MS.INVOICE_NO	=	DT.INVOICE_NO
                        WHERE
                                MS.DATE_CREATE  >=  @createDateFrom
                        AND     MS.DATE_CREATE  <=  @createDateTo";

            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@createDateFrom", SqlDbType.Date);
            sqlParameters[0].Value = Convert.ToString(_dueDateFrom);
            sqlParameters[1] = new SqlParameter("@createDateTo", SqlDbType.Date);
            sqlParameters[1].Value = Convert.ToString(_dueDateTo);

            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetData_DetailInvoice(string _revenue)
        {
            string StrQuery = "";

            StrQuery = @"SELECT
                        	 DT.INVOICE_NO
                        	,INV_MS.DATE_CREATE
                            ,INV_MS.SHIPTO_CUSTOMER_CODE
                            ,INV_MS.SHIP_VIA
                            ,INV_MS.PORT_OF_DESTINATION
                            ,DT.CUS_ITEM_CODE
                            ,DT.TVC_ITEM_CODE
                            ,MF.ACC_ITEM_CODE
                        	,DT.CUSTOMER_CODE
                            ,CONCAT(DT.TVC_ITEM_CODE, '-', DT.PART_DESCRIPTION) AS CODE_AND_NAME
                            ,INV_MS.ETD
                            ,DT.USD_RATE
                        	,SUM(DT.QUANTITY)	AS QUANTITY
                        	,SUM(DT.AMOUNT)		AS AMOUNT
                            ,DT.ORDER_PRICE     AS PRICE
                        	,CASE
                        		WHEN PO_MS.COMPANY_CODE = '00001' THEN 'XBTP1'
                        		WHEN PO_MS.COMPANY_CODE = '00002' THEN 'XBTP2'
                             END AS	REASON_CODE
                        FROM
                        	TVC_INV_DETAIL DT
                        LEFT JOIN
                        	TVC_PO_MS PO_MS
                        ON
                        	DT.CUSTOMER_CODE	=	PO_MS.CUSTOMER_CODE
                        AND	DT.TVC_ITEM_CODE	=	PO_MS.TVC_ITEM_CODE
                        AND	DT.CUSTOMER_PO		=	PO_MS.CUSTOMER_PO
                        AND	DT.DUE_DATE_PO		=	PO_MS.DUE_DATE
                        INNER JOIN
                        	TVC_INV_MS INV_MS
                        ON
                        	DT.INVOICE_NO		=	INV_MS.INVOICE_NO
                        INNER JOIN
                        	(
                        		SELECT DISTINCT
                        			 DT.TVC_ITEM_CODE
                        			,MF.ACC_ITEM_CODE
                        			,MF.ITEM_CODE
                        			,DT.CUSTOMER_CODE
                                    ,DT.CUSTOMER_PO
			                        ,DT.DUE_DATE_PO	
                        		FROM
                        			TVC_INV_DETAIL DT
                        		LEFT JOIN
                        			PRODUCTMF MF
                        		ON
                        			DT.TVC_ITEM_CODE    =   MF.ITEM_CODE
                        		AND DT.CUSTOMER_CODE	=	MF.CUSTOMER
                        	) MF
                        ON
                            DT.TVC_ITEM_CODE    =   MF.TVC_ITEM_CODE
                        AND DT.CUSTOMER_CODE	=	MF.CUSTOMER_CODE
                        AND DT.CUSTOMER_PO	    =	MF.CUSTOMER_PO
                        AND DT.DUE_DATE_PO	    =	MF.DUE_DATE_PO
                        WHERE
                            INV_MS.REVENUE      =  @revenue
                        GROUP BY
                        	 DT.INVOICE_NO
                        	,INV_MS.DATE_CREATE
                            ,INV_MS.SHIPTO_CUSTOMER_CODE
                            ,INV_MS.SHIP_VIA
                            ,INV_MS.PORT_OF_DESTINATION
                            ,DT.CUS_ITEM_CODE
                            ,CONCAT(DT.TVC_ITEM_CODE, '-', DT.PART_DESCRIPTION)
                            ,DT.TVC_ITEM_CODE
                            ,MF.ACC_ITEM_CODE
                        	,DT.CUSTOMER_CODE
                            ,INV_MS.ETD
                            ,DT.USD_RATE
                            ,DT.ORDER_PRICE
                        	,CASE
                        		WHEN PO_MS.COMPANY_CODE = '00001' THEN 'XBTP1'
                        		WHEN PO_MS.COMPANY_CODE = '00002' THEN 'XBTP2'
                             END";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@revenue", SqlDbType.NVarChar);
            sqlParameters[0].Value = Convert.ToString(_revenue);

            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }
    }
}
