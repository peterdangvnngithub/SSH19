using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAKAKO_ERP_3LAYER.DAO
{
    public class SEARCH_DAO
    {
        private DBConnection conn;

        /// <constructor>
        /// Constructor SEARCH_DAO
        /// </constructor>
        public SEARCH_DAO()
        {
            conn = new DBConnection();
        }

        public DataTable GetInfoCustomer(string _searchValue)
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT  [CUSTOMER_CODE]
                                ,[CUSTOMER_NAME1]
                                ,CONCAT([ADDRESS1], [ADDRESS2], [ADDRESS3]) AS ADDRESS
                                ,[TEL_NO]
                                ,[FAX_NO]
                                ,[INVOICE_FORMAT]
                        FROM
                                [dbo].[CUSTOMMF]
                        WHERE
                                [CUSTOMER_CLASS] IN (1, 3)   
                            AND CUSTOMER_CODE LIKE CONCAT('%',@searchValue,'%')";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@searchValue", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_searchValue);
            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetCustomer_IssuedTo(string _searchValue)
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT  [CUSTOMER_CODE]
                                ,[CUSTOMER_NAME1]
                                ,CONCAT([ADDRESS1], [ADDRESS2], [ADDRESS3]) AS ADDRESS
                                ,[TEL_NO]
                                ,[FAX_NO]
                                ,[INVOICE_FORMAT]
                        FROM
                                [dbo].[CUSTOMMF]
                        WHERE
                                [CUSTOMER_CLASS] IN (1, 3)   
                            AND CUSTOMER_CODE LIKE CONCAT('%',@searchValue,'%')";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@searchValue", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_searchValue);
            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetCustomer_ShipTo(string _searchValue)
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT  [CUSTOMER_CODE]
                                ,[CUSTOMER_NAME1]
                                ,CONCAT([ADDRESS1], [ADDRESS2], [ADDRESS3]) AS ADDRESS
                                ,[TEL_NO]
                                ,[FAX_NO]
                                ,[INVOICE_FORMAT]
                        FROM
                                [dbo].[CUSTOMMF]
                        WHERE
                                [CUSTOMER_CLASS] IN (1, 3)   
                            AND CUSTOMER_CODE LIKE CONCAT('%',@searchValue,'%')";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@searchValue", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_searchValue);
            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetInfoPO(string _searchValue)
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT  [RECEIVE_NO]
                                ,MIN([DUE_DATE]) AS DUE_DATE_FROM
                         		,MAX([DUE_DATE]) AS DUE_DATE_TO
                         FROM
                                [dbo].[TVC_PO_MS]
                         WHERE
                                RECEIVE_NO LIKE CONCAT('%',@searchValue,'%')
                         GROUP BY
                                [RECEIVE_NO]
                         ORDER BY
                                [RECEIVE_NO]";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@searchValue", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_searchValue);
            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetInfoInvoice(string _searchValue)
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT
                             CASE
                                WHEN MS.[LOCK_STATUS] = 0 THEN '0'
                                WHEN MS.[LOCK_STATUS] = 1 THEN '1'
                                WHEN MS.[LOCK_STATUS] = 2 THEN '2'
                        	 END AS LOCK_STATUS
                            ,MS.[ISSUEDTO_CUSTOMER_CODE]
                            ,MS.[SHIPTO_CUSTOMER_CODE]
                            ,MS.[SHIPPING_NO]
                            ,MS.[INVOICE_NO]
                            ,MS.[DATE_CREATE]
                            ,MS.[ETD]
                        	,ISNULL(DT.[REVISE_VERSION], 0)	AS	REVISE_VERSION
	                        ,DT.UNIT_CURRENCY
	                        ,DT.AMOUNT
                        FROM 
                            [dbo].[TVC_INV_MS] MS
                        LEFT JOIN
                        	(
                        		SELECT
                        			 INVOICE_NO
                                    ,UNIT_CURRENCY
                        			,MAX(REVISE_VERSION)    AS REVISE_VERSION
                                    ,SUM(ISNULL(AMOUNT,0))	AS AMOUNT	
                        		FROM [dbo].[TVC_INV_DETAIL]
                                GROUP BY INVOICE_NO,UNIT_CURRENCY
                            ) DT
                        ON	MS.INVOICE_NO = DT.INVOICE_NO
                        WHERE
                            MS.[INVOICE_NO] LIKE CONCAT('%',@searchValue,'%')
                        ORDER BY
                            MS.[DATE_CREATE] DESC";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@searchValue", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_searchValue);
            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetInfoShipping(string _searchValue,string _filter1)
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT 
                             MS.[ISSUEDTO_CUSTOMER_CODE]
                            ,MS.[SHIPTO_CUSTOMER_CODE]
                            ,MS.[SHIPPING_NO]
                            ,MS.[INVOICE_NO]
                            ,CASE
                                WHEN MS.[LOCK_STATUS] = 0 THEN '0'
                                WHEN MS.[LOCK_STATUS] = 1 THEN '1'
                                WHEN MS.[LOCK_STATUS] = 2 THEN '2'
                        	 END AS LOCK_STATUS  
                            ,DT.[UNIT_CURRENCY]
                            ,MS.[ETD]
                            ,MS.[DATE_CREATE]
	                        ,DT.[AMOUNT]
                        FROM 
                            [dbo].[TVC_SHIPPING_MS] MS
                        INNER JOIN
                        	(
                        		SELECT
                        			 MS.SHIPPING_NO
                                    ,DT.UNIT_CURRENCY
                        			,MAX(DT.REVISE_VERSION)     AS REVISE_VERSION
                                    ,SUM(ISNULL(DT.AMOUNT,0))   AS AMOUNT	
                        		FROM
                                    [dbo].[TVC_SHIPPING_MS] MS
                                LEFT JOIN
                                    [dbo].[TVC_SHIPPING_INV_DETAIL] DT
                                ON  MS.SHIPPING_NO = DT.SHIPPING_NO
                                GROUP BY
                                     MS.SHIPPING_NO
                                    ,DT.UNIT_CURRENCY
                            ) DT
                        ON	MS.SHIPPING_NO = DT.SHIPPING_NO
                        WHERE
                            MS.[SHIPPING_NO] LIKE CONCAT('%',@searchValue,'%')
                        AND MS.[ISSUEDTO_CUSTOMER_CODE] LIKE CONCAT('%',@filter1,'%')
                        ORDER BY
                             MS.[ETD] DESC
                            ,MS.[INVOICE_NO] ASC";
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@searchValue", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_searchValue);
            sqlParameters[1] = new SqlParameter("@filter1", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(_filter1);
            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetInfoPriceCondition(string _searchValue)
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT
                            [PRICE_COND]
                         FROM 
                            [dbo].[PRICE_CONDITIONMF]
                        WHERE
                            [PRICE_COND] LIKE CONCAT('%',@searchValue,'%')";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@searchValue", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_searchValue);
            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetInfoPaymentTerm(string _searchValue)
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT 
                            [PAYMENT_ID]
                         FROM
                            [dbo].[PAYMENT_TERMMF]
                        WHERE
                            [PAYMENT_ID] LIKE CONCAT('%',@searchValue,'%')";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@searchValue", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_searchValue);
            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetInfoDestination(string _searchValue)
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT
                            [DESTINATION_ID]
                         FROM
                            [dbo].[DESTINATIONMF]
                         WHERE
                            [DESTINATION_ID] LIKE CONCAT('%',@searchValue,'%')";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@searchValue", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_searchValue);
            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetInfoItemWithCurrency(string _searchValue
                                                ,string _customerPO
                                                ,string _customerCode
                                                ,Boolean _isCurrencyJPY
                                                ,int _companyValue
                                                ,int _TypeSearch)
        {
            string StrQuery = "";
            int _listCustomerCout = 0;
            List<string> _listPO = new List<string>();
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT
                                 MS.[CUSTOMER_CODE]
                                ,MS.[RECEIVE_NO]
                                ,MS.[CUSTOMER_ITEM_CODE]		                    AS CUS_ITEM_CODE
                        		,MS.[TVC_ITEM_CODE]				                    AS ITEM_CODE
                        		,MS.[ITEM_NAME]				                        AS ITEM_NAME
                        		,MS.[DUE_DATE]                                      AS DUE_DATE
                                ,MS.[ORDER_DATE]
                                ,(MS.[QUANTITY] - ISNULL(MS.[QUANTITY_ORDER], 0))   AS BALANCE
                        		,ISNULL(P.[BOX_QUANTITY], 0)	                    AS BOX_QUANTITY
                        		,ISNULL(P.[WEIGHT], 0)			                    AS WEIGHT
                        		,MS.[CUSTOMER_PO]
                                ,MS.[THIRD_PARTY_ITEM_CODE]
                                ,MS.[THIRD_PARTY_PO]
                        		,MS.[UNIT_CURRENCY]				                    AS UNIT_CURRENCY
                        		,CASE
                        			WHEN MS.UNIT_CURRENCY ='JPY' THEN ISNULL(MS.[PRICE_JPY], 0)
                        			WHEN MS.UNIT_CURRENCY ='USD' THEN ISNULL(MS.[PRICE_USD], 0)
                        		 END AS ORDER_PRICE
                        		,MS.[UNIT_CURRENCY]
                        		,ISNULL(S.[PRICE], 0)			                    AS PRICE
                                ,MS.[NOTE]
                        FROM
                        		[DBO].[TVC_PO_MS] MS 
                        LEFT JOIN
                                [DBO].[PRODUCTMF] P 
                        ON      MS.[TVC_ITEM_CODE]	    =	P.[ITEM_CODE]
                        LEFT JOIN
                                (
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
                                                 GLB.GLOBAL_CODE
                                                ,MAX(GLB.APPLYDATE)     AS  APPLYDATE
                		                    FROM
                                                [dbo].[SPRICE_GLOBALMF] GLB
                                            INNER JOIN
                                                [dbo].[TVC_PO_MS] MS
                                            ON	MS.CUSTOMER_ITEM_CODE   =   GLB.GLOBAL_CODE   
                                            AND	MS.UNIT_CURRENCY        =   GLB.PRICE_UNIT
						                    WHERE
                                                GLB.APPLYDATE           <=  GETDATE()
                                            AND GLB.INV_DV              <> '*'
                		                    GROUP BY
                                                GLOBAL_CODE
                                    	) GLB_MAX
                                    ON
                                    	GLB.GLOBAL_CODE		=	GLB_MAX.GLOBAL_CODE
                                    AND	GLB.APPLYDATE		=	GLB_MAX.APPLYDATE
                                    AND GLB.INV_DV          <> '*'
                                ) S 
                        ON		MS.[CUSTOMER_ITEM_CODE]	    =	S.[GLOBAL_CODE] 
                        AND     MS.[UNIT_CURRENCY]          =	S.[PRICE_UNIT]
                        WHERE
                        		(MS.[QUANTITY] - ISNULL(MS.[QUANTITY_ORDER], 0)) > 0";
            if (!String.IsNullOrEmpty(_searchValue))
            {
                StrQuery += "   AND MS.[TVC_ITEM_CODE] LIKE CONCAT('%',@searchValue, '%')";
            }           
            if (_companyValue == 1)
            {
                StrQuery += "   AND   MS.[COMPANY_CODE] = '00001'";
            } else if (_companyValue == 2)
            {
                StrQuery += "   AND   MS.[COMPANY_CODE] = '00002'";
            }
            if (!String.IsNullOrEmpty(_customerPO) && _TypeSearch == 0)
            {
                StrQuery += "   AND   MS.[CUSTOMER_PO] LIKE CONCAT('%',@customerPO, '%')";
            } else if (!String.IsNullOrEmpty(_customerPO) && _TypeSearch == 1)
            {
                _listPO = _customerPO.Split(',').ToList();
                _listCustomerCout = _listPO.Count();
                if(_listCustomerCout > 0)
                {
                    StrQuery += "   AND (";
                    int index = 0;
                    foreach (var item in _listPO)
                    {
                        if (index == _listCustomerCout - 1)
                        { 
                            StrQuery += "   (MS.[CUSTOMER_PO] = " + "@CustomerPO" + index + "))";
                        } else if (index < _listCustomerCout - 1)
                        {
                            StrQuery += "   (MS.[CUSTOMER_PO] = " + "@CustomerPO" + index + ") OR";
                        }
                        index++;
                    }
                }
            }
            //if (!String.IsNullOrEmpty(_customerPO))
            //{
            //    StrQuery += "   AND   MS.[CUSTOMER_PO] IN (@customerPO)";
            //}
            if (!String.IsNullOrEmpty(_customerCode))
            {
                StrQuery += "   AND   MS.[CUSTOMER_CODE] = @CustomerCode";
            }
            //CONDITION UNIT CURRENCY
            if (_isCurrencyJPY)
            {
                StrQuery += "   AND   MS.[UNIT_CURRENCY]  = 'JPY'";
            }
            else
            {
                StrQuery += "   AND   MS.[UNIT_CURRENCY]  = 'USD'";
            }
            StrQuery += @"  ORDER BY
                                MS.[DUE_DATE]";
            //
            List<SqlParameter> cParameters = new List<SqlParameter>();
            if (_TypeSearch == 0)
            {
                cParameters.Add(new SqlParameter("@customerPO", Convert.ToString(_customerPO)));
            }
            else if (_TypeSearch == 1)
            {
                for (int i = 0; i < _listCustomerCout; i++)
                {
                    cParameters.Add(new SqlParameter("@customerPO" + i, Convert.ToString(_listPO[i])));
                }
            }
            cParameters.Add(new SqlParameter("@searchValue", Convert.ToString(_searchValue)));
            cParameters.Add(new SqlParameter("@CustomerCode", Convert.ToString(_customerCode)));
            return conn.executeSelectQuery(StrQuery, cParameters.ToArray());
        }

        public DataTable GetInfoShippingNo(string _searchValue)
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT
                        	 CASE
                                WHEN MS.[LOCK_STATUS] = 0 THEN 'NORMAL'
                                WHEN MS.[LOCK_STATUS] = 1 THEN 'LOCK'
                                WHEN MS.[LOCK_STATUS] = 2 THEN 'REVISE'
                        	 END AS LOCK_STATUS
                        	,MS.[COMPANY_CODE]
                        	,MS.[SHIPPING_NO]
                        	,MS.[INVOICE_NO]
                            ,DT.[UNIT_CURRENCY]
                        	,MS.[ISSUEDTO_CUSTOMER_CODE]
                        	,MS.[SHIPTO_CUSTOMER_CODE]
                        	,MS.[ETD]
                        FROM
                        	[dbo].[TVC_SHIPPING_MS] MS
                        INNER JOIN
                            (
                                SELECT 
                                     SHIPPING_NO
                                    ,UNIT_CURRENCY
                                FROM
                                    [dbo].[TVC_SHIPPING_INV_DETAIL]
                                WHERE
                                    (SHIPPING_NO LIKE    '%' + @searchValue + '%')
                                GROUP BY
                                     SHIPPING_NO
                                    ,UNIT_CURRENCY
                            ) DT
                        ON  MS.SHIPPING_NO      =   DT.SHIPPING_NO
                        WHERE
                            (MS.[SHIPPING_NO]   LIKE    '%' + @searchValue + '%')
                        AND (MS.[INVOICE_NO] IS NULL OR MS.[INVOICE_NO] = '')
                        AND (MS.[LOCK_STATUS]    <>   0)
                        ORDER BY
                            MS.[ETD] DESC";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@searchValue", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_searchValue);
            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetInfoTVC_CodeItem(string _searchValue)
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT
                                 PO.[CUSTOMER_CODE]
                                ,PO.[TVC_ITEM_CODE]
                                ,PO.[ITEM_NAME]
                        FROM
                                [dbo].[TVC_PO_MS]    PO  ";
            if (!String.IsNullOrEmpty(_searchValue))
            {
                StrQuery += "   WHERE       ([TVC_ITEM_CODE]    LIKE    '%' + @TVCItemCode + '%')   ";
            }
            StrQuery += "GROUP BY PO.[CUSTOMER_CODE] ,PO.[TVC_ITEM_CODE] ,PO.[ITEM_NAME]";
            StrQuery += "ORDER BY PO.[CUSTOMER_CODE] ";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@TVCItemCode", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_searchValue);
            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetInfoCustomerPO(string _searchValue)
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT
                                 PO.[COMPANY_CODE]
                                ,PO.[CUSTOMER_CODE]
                                ,PO.[RECEIVE_NO]
                                ,PO.[CUSTOMER_PO]
                                ,PO.[DUE_DATE]
                        FROM
                                [dbo].[TVC_PO_MS]    PO";
            if (!String.IsNullOrEmpty(_searchValue))
            {
                StrQuery += "   WHERE       ([CUSTOMER_PO]    LIKE    '%' + @customerPO + '%')";
            }
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@customerPO", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_searchValue);
            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public DataTable GetInfoInvoiceNo(string _searchValue)
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT
                        	 [INVOICE_NO]
                        	,MIN([DUE_DATE_PO]) AS DUE_DATE_FROM
                            ,MAX([DUE_DATE_PO]) AS DUE_DATE_TO
                        FROM
                        	[dbo].[TVC_INV_DETAIL]";
            if (!String.IsNullOrEmpty(_searchValue))
            {
                StrQuery += "   WHERE   ([INVOICE_NO]    LIKE    '%' + @InvoiceNo + '%')";
            }
            StrQuery += "   GROUP BY";
            StrQuery += "   [INVOICE_NO]";
            StrQuery += "   ORDER BY [INVOICE_NO] DESC";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@InvoiceNo", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_searchValue);
            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }
    }
}
