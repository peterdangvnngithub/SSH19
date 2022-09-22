using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using DevExpress.XtraBars;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using TAKAKO_ERP_3LAYER.DAL;
using System.Data.SqlClient;
using static TAKAKO_ERP_3LAYER.Common;

namespace TAKAKO_ERP_3LAYER
{
    public partial class Form_Search_PO_New : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        SYSTEM_DAL systemDAL;
        string customerCode;
        string unitCurrency;
        string companyCode;
        DateTime dateCreateInvoice;
        private readonly Form_Shipping_Instruction form_Shipping_Instruction;

        private GridColumn gridCol_Company_Code = new GridColumn();
        private GridColumn gridCol_Customer_Code = new GridColumn();
        private GridColumn gridCol_ReceiveNo = new GridColumn();
        private GridColumn gridCol_Cus_Item_Code = new GridColumn();
        private GridColumn gridCol_TVC_Item_Code = new GridColumn();        
        private GridColumn gridCol_Item_Name = new GridColumn();
        private GridColumn gridCol_DueDate = new GridColumn();
        private GridColumn gridCol_OrderDate = new GridColumn();
        private GridColumn gridCol_Unit_Currency = new GridColumn();
        private GridColumn gridCol_Customer_PO = new GridColumn();
        private GridColumn gridCol_Qty_Balance_Qty = new GridColumn();
        private GridColumn gridCol_Order_Price = new GridColumn();
        private GridColumn gridCol_Note = new GridColumn();        

        public Form_Search_PO_New(SYSTEM_DAL _systemDAL,string _companyCode,string _customerCode, string _unitCurrency, DateTime _dateCreateInvoice, Form_Shipping_Instruction _formShippingIns)
        {
            InitializeComponent();

            systemDAL = _systemDAL;
            companyCode = _companyCode;
            customerCode = _customerCode;
            unitCurrency = _unitCurrency;
            dateCreateInvoice = _dateCreateInvoice;

            form_Shipping_Instruction = _formShippingIns;
        }

        private void Form_Import_PO_To_Shipping_Excel_Load(object sender, EventArgs e)
        {
            Define_GridView();

            GetData();
        }

        private void GetData()
        {
            using(Takako_Entities db = new Takako_Entities())
            {
                object[] xparams = 
                    {
                    new SqlParameter("@CompanyCode", companyCode),
                    new SqlParameter("@CustomerCode", customerCode),
                    new SqlParameter("@UnitCurrency", unitCurrency)
                };

                string sqlQuery =
                    @"SELECT
                         MS.[COMPANY_CODE]
		        		,MS.[CUSTOMER_CODE]
		        		,MS.[RECEIVE_NO]
		        		,MS.[CUSTOMER_ITEM_CODE]
		        		,MS.[TVC_ITEM_CODE]				                    AS TVC_ITEM_CODE
		        		,MS.[ITEM_NAME]				                        AS ITEM_NAME
		        		,MS.[DUE_DATE]                                      AS DUE_DATE
		        		,MS.[ORDER_DATE]
		        		,(MS.[QUANTITY] - ISNULL(MS.[QUANTITY_ORDER], 0))   AS BALANCE
		        		,ISNULL(P.[BOX_QUANTITY], 0)	                    AS BOX_QUANTITY
		        		,ISNULL(P.[WEIGHT], 0)			                    AS WEIGHT
		        		,MS.[CUSTOMER_PO]
		        		,MS.[THIRD_PARTY_ITEM_CODE]
		        		,MS.[THIRD_PARTY_PO]
		        		,CASE
		        			WHEN MS.UNIT_CURRENCY ='JPY' THEN ISNULL(MS.[PRICE_JPY], 0)
		        			WHEN MS.UNIT_CURRENCY ='USD' THEN ISNULL(MS.[PRICE_USD], 0)
		        		 END                                                AS ORDER_PRICE
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
		            					AND GLB.CUSTOMER_CODE		=	@CustomerCode
		            					AND	GLB.PRICE_UNIT			=	@UnitCurrency
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
		            		MS.COMPANY_CODE				=	@CompanyCode
		            AND		MS.CUSTOMER_CODE			=	@CustomerCode
		            AND		MS.UNIT_CURRENCY			=	@UnitCurrency
		            AND		(MS.[QUANTITY] - ISNULL(MS.[QUANTITY_ORDER], 0)) > 0
		            ORDER BY
		            		MS.[DUE_DATE]";
                var result = db.Database.SqlQuery<SearchPOInfo>(sqlQuery, xparams).ToList();

                gridControl_Search_PO.DataSource = result;
            }
        }

        private void Define_GridView()
        {
            // COMPANY_CODE
            gridCol_Company_Code.Name = "gridCol_Company_Code";
            gridCol_Company_Code.Caption = "COMPANY CODE";
            gridCol_Company_Code.FieldName = "COMPANY_CODE";
            gridCol_Company_Code.VisibleIndex = 0;
            gridCol_Company_Code.Width = 90;

            gridCol_Company_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // CUSTOMER CODE
            gridCol_Customer_Code.Name = "gridCol_Customer_Code";
            gridCol_Customer_Code.Caption = "CUSTOMER CODE";
            gridCol_Customer_Code.FieldName = "CUSTOMER_CODE";
            gridCol_Customer_Code.VisibleIndex = 0;
            gridCol_Customer_Code.Width = 100;

            gridCol_Customer_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // RECEIVE NO
            gridCol_ReceiveNo.Name = "gridCol_ReceiveNo";
            gridCol_ReceiveNo.Caption = "RECEIVE NO";
            gridCol_ReceiveNo.FieldName = "RECEIVE_NO";
            gridCol_ReceiveNo.VisibleIndex = 0;
            gridCol_ReceiveNo.Width = 120;

            gridCol_ReceiveNo.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // CUSTOMER ITEM CODE
            gridCol_Cus_Item_Code.Name = "gridCol_Cus_Item_Code";
            gridCol_Cus_Item_Code.Caption = "CUSTOMER ITEM CODE";
            gridCol_Cus_Item_Code.FieldName = "CUSTOMER_ITEM_CODE";
            gridCol_Cus_Item_Code.VisibleIndex = 0;
            gridCol_Cus_Item_Code.Width = 120;
            
            gridCol_Cus_Item_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // TVC ITEM CODE
            gridCol_TVC_Item_Code.Name = "gridCol_TVC_Item_Code";
            gridCol_TVC_Item_Code.Caption = "TVC ITEM CODE";
            gridCol_TVC_Item_Code.FieldName = "TVC_ITEM_CODE";
            gridCol_TVC_Item_Code.VisibleIndex = 0;
            gridCol_TVC_Item_Code.Width = 120;

            gridCol_TVC_Item_Code.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // CUSTOMER PO
            gridCol_Customer_PO.Name = "gridCol_Customer_PO";
            gridCol_Customer_PO.Caption = "CUSTOMER PO";
            gridCol_Customer_PO.FieldName = "CUSTOMER_PO";
            gridCol_Customer_PO.VisibleIndex = 0;
            gridCol_Customer_PO.Width = 120;

            gridCol_Customer_PO.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // ITEM NAME
            gridCol_Item_Name.Name = "gridCol_Item_Name";
            gridCol_Item_Name.Caption = "ITEM NAME";
            gridCol_Item_Name.FieldName = "ITEM_NAME";
            gridCol_Item_Name.VisibleIndex = 0;
            gridCol_Item_Name.Width = 140;

            gridCol_Item_Name.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // DUE DATE
            gridCol_DueDate.Name = "gridCol_DueDate";
            gridCol_DueDate.Caption = "DUE DATE";
            gridCol_DueDate.FieldName = "DUE_DATE";
            gridCol_DueDate.VisibleIndex = 0;
            gridCol_DueDate.Width = 110;
            gridCol_DueDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            gridCol_DueDate.DisplayFormat.FormatType = FormatType.DateTime;

            gridCol_DueDate.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // ORDER DATE
            gridCol_OrderDate.Name = "gridCol_OrderDate";
            gridCol_OrderDate.Caption = "ORDER DATE";
            gridCol_OrderDate.FieldName = "ORDER_DATE";
            gridCol_OrderDate.VisibleIndex = 0;
            gridCol_OrderDate.Width = 110;
            gridCol_OrderDate.DisplayFormat.FormatString = "dd/MM/yyyy";
            gridCol_OrderDate.DisplayFormat.FormatType = FormatType.DateTime;
                
            gridCol_OrderDate.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // UNIT CURRENCY
            gridCol_Unit_Currency.Name = "gridCol_Unit_Currency";
            gridCol_Unit_Currency.Caption = "UNIT CURRENTCY";
            gridCol_Unit_Currency.FieldName = "UNIT_CURRENCY";
            gridCol_Unit_Currency.VisibleIndex = 0;
            gridCol_Unit_Currency.Width = 100;

            gridCol_Unit_Currency.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

            // QUANTITY BALANCE
            gridCol_Qty_Balance_Qty.Name = "gridCol_Qty_Balance_Qty";
            gridCol_Qty_Balance_Qty.Caption = "QUANTITY BALANCE";
            gridCol_Qty_Balance_Qty.FieldName = "BALANCE";
            gridCol_Qty_Balance_Qty.VisibleIndex = 0;
            gridCol_Qty_Balance_Qty.Width = 120;
            gridCol_Qty_Balance_Qty.DisplayFormat.FormatString = "#,##0";
            gridCol_Qty_Balance_Qty.DisplayFormat.FormatType = FormatType.Numeric;

            gridCol_Qty_Balance_Qty.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

            // ORDER PRICE
            gridCol_Order_Price.Name = "gridCol_Order_Price";
            gridCol_Order_Price.Caption = "ORDER PRICE";
            gridCol_Order_Price.FieldName = "ORDER_PRICE";
            gridCol_Order_Price.VisibleIndex = 0;
            gridCol_Order_Price.Width = 120;
            gridCol_Order_Price.DisplayFormat.FormatString = "#,##0";
            gridCol_Order_Price.DisplayFormat.FormatType = FormatType.Numeric;

            gridCol_Order_Price.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;

            // NOTE
            gridCol_Note.Name = "gridCol_Note";
            gridCol_Note.Caption = "NOTE";
            gridCol_Note.FieldName = "NOTE";
            gridCol_Note.VisibleIndex = 0;
            gridCol_Note.Width = 150;

            // Not allow edit data gridview
            View_PO_List.OptionsBehavior.Editable = false;
            View_PO_List.OptionsPrint.AutoWidth = false;
            View_PO_List.OptionsView.ColumnAutoWidth = false;
            View_PO_List.OptionsView.ColumnHeaderAutoHeight = DefaultBoolean.True;

            // Add column to gridview
            View_PO_List.Columns.Add(gridCol_Company_Code);
            View_PO_List.Columns.Add(gridCol_ReceiveNo);
            View_PO_List.Columns.Add(gridCol_Customer_Code);
            View_PO_List.Columns.Add(gridCol_Cus_Item_Code);
            View_PO_List.Columns.Add(gridCol_TVC_Item_Code);
            View_PO_List.Columns.Add(gridCol_Item_Name);
            View_PO_List.Columns.Add(gridCol_DueDate);
            View_PO_List.Columns.Add(gridCol_OrderDate);
            View_PO_List.Columns.Add(gridCol_Unit_Currency);
            View_PO_List.Columns.Add(gridCol_Customer_PO);
            View_PO_List.Columns.Add(gridCol_Qty_Balance_Qty);
            View_PO_List.Columns.Add(gridCol_Order_Price);
            View_PO_List.Columns.Add(gridCol_Note); 

            // Set common attribute
            foreach (GridColumn c in View_PO_List.Columns)
            {
                c.AppearanceHeader.Options.UseFont = true;
                c.AppearanceHeader.Options.UseForeColor = true;
                c.AppearanceHeader.Options.UseTextOptions = true;
                c.AppearanceHeader.ForeColor = Color.Black;
                c.AppearanceHeader.TextOptions.WordWrap = WordWrap.Wrap;
                c.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                c.AppearanceHeader.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

                c.AppearanceCell.Options.UseBackColor = true;
                c.AppearanceCell.Options.UseTextOptions = true;
            }
        }

        private void barBtn_SaveData_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barBtn_ImportData_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void View_PO_List_DoubleClick(object sender, EventArgs e)
        {
            //int balance = Convert.ToInt32(View_PO_List.GetFocusedRowCellValue("BALANCE"));
            //MessageBox.Show(balance.ToString());
            SearchPOInfo curSearchPOInfo = (SearchPOInfo)View_PO_List.GetRow(View_PO_List.GetFocusedDataSourceRowIndex());
            MessageBox.Show(curSearchPOInfo.BALANCE.ToString());
            form_Shipping_Instruction.RefreshGridView(sender, e);
        }

        private void gridControl_Search_PO_Click(object sender, EventArgs e)
        {

        }
    }
}