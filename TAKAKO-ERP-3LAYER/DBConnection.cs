using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace TAKAKO_ERP_3LAYER
{
    public class DBConnection
    {
        private SqlDataAdapter myAdapter;
        private SqlConnection conn;

        /// <constructor>
        /// Initialise Connection
        /// </constructor>
        public DBConnection()
        {
            myAdapter = new SqlDataAdapter();
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString);
        }

        /// <method>
        /// Open Database Connection if Closed or Broken
        /// </method>
        private SqlConnection openConnection()
        {
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }

        /// <method>
        /// Select Query
        /// </method>
        public DataTable executeSelectQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet ds = new DataSet();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myCommand.CommandTimeout = 300;
                myCommand.ExecuteNonQuery();
                myAdapter.SelectCommand = myCommand;
                myAdapter.Fill(ds);
                dataTable = ds.Tables[0];
            }
            catch (SqlException e)
            {
                MessageBox.Show("Error - Connection.executeSelectQuery - Query: " + _query + " \nException: " + e.StackTrace.ToString());
                return null;
            }
            finally
            {
                conn.Close();
            }
            return dataTable;
        }

        /// <method>
        /// Insert Query
        /// </method>
        public bool executeInsertQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            SqlTransaction transaction;                         //khai báo một transaction

            myCommand.Connection = openConnection();
            transaction = conn.BeginTransaction();              //bắt đầu quá trình quản lý transaction
            myCommand.CommandText = _query;
            myCommand.Parameters.AddRange(sqlParameter);
            myAdapter.InsertCommand = myCommand;
            myCommand.Transaction = transaction;                //gắn transaction
            myCommand.CommandTimeout = 300;

            try
            {
                myCommand.ExecuteNonQuery();
                transaction.Commit();                           //cam kết thực hiện thành công
            }
            catch (SqlException e)
            {
                MessageBox.Show("Error - Connection.executeInsertQuery - Query: \n" + _query + " \nException: \n" + e.StackTrace.ToString());
                transaction.Rollback();                         //quay lùi
                return false;
            }
            finally
            {
                conn.Close();
            }
            return true;
        }

        /// <method>
        /// SP Insert PO Query
        /// </method>
        public bool insertPO(DataTable _tempData)
        {
            conn.Open();
            var cmd = new SqlCommand("SP_TVC_INSERT_PO", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlParameter param = cmd.Parameters.AddWithValue("@tblPO", _tempData);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
               return false;
            }
            finally
            {
                // Close the SqlDataReader. The SqlBulkCopy
                // object is automatically closed at the end
                // of the using block.
                conn.Close();
            }
            conn.Close();
            return true;
        }

        /// <method>
        /// SP Insert Invoice
        /// </method>
        public bool Update_InvoiceNo(DataTable _invoiceMS,DataTable _invoiceDetail,DataTable _PackingListDetail,DataTable _shippingNo)
        {
            conn.Open();
            var cmd = new SqlCommand("SP_TVC_UPDATE_INV_NORMAL", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            //Set timeout
            cmd.CommandTimeout = 300;
            //Add param
            SqlParameter param = cmd.Parameters.AddWithValue("@tblInvoiceMS", _invoiceMS);
            param = cmd.Parameters.AddWithValue("@tblInvoiceDetail", _invoiceDetail);
            param = cmd.Parameters.AddWithValue("@tblPackingListDetail", _PackingListDetail);
            param = cmd.Parameters.AddWithValue("@tblShippingNo", _shippingNo);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                // Close the SqlDataReader. The SqlBulkCopy
                // object is automatically closed at the end
                // of the using block.
                conn.Close();
            }
            conn.Close();
            return true;
        }

        /// <method>
        /// SP Insert Invoice
        /// </method>
        public bool Update_ShippingNo(DataTable _invoiceMS, DataTable _invoiceDetail_Init, DataTable _invoiceDetail, DataTable _PackingListDetail)
        {
            conn.Open();
            var cmd = new SqlCommand("SP_TVC_UPDATE_SHIPPING_NORMAL", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            //Set timeout
            cmd.CommandTimeout = 300;
            //Add param
            SqlParameter param = cmd.Parameters.AddWithValue("@tblInvoiceMS", _invoiceMS);
            param = cmd.Parameters.AddWithValue("@tblInvoiceInitDetail", _invoiceDetail_Init);
            param = cmd.Parameters.AddWithValue("@tblInvoiceDetail", _invoiceDetail);
            param = cmd.Parameters.AddWithValue("@tblPackingListDetail", _PackingListDetail);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                // Close the SqlDataReader. The SqlBulkCopy
                // object is automatically closed at the end
                // of the using block.
                conn.Close();
            }
            conn.Close();
            return true;
        }

        /// <method>
        /// SP Revise Invoice
        /// </method>
        public bool ReviseInvoice(string _invoiceNo)
        {
            conn.Open();
            var cmd = new SqlCommand("SP_TVC_LOCK_INV", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlParameter param = cmd.Parameters.AddWithValue("@InvoiceNo", _invoiceNo);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                // Close the SqlDataReader. The SqlBulkCopy
                // object is automatically closed at the end
                // of the using block.
                conn.Close();
            }
            conn.Close();
            return true;
        }

        /// <method>
        /// SP Delete Invoice
        /// </method>
        public bool DeleteInvoice(string _invoiceNo, DataTable _tempListShipping)
        {
            conn.Open();
            var cmd = new SqlCommand("SP_TVC_DELETE_INV", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlParameter param = cmd.Parameters.AddWithValue("@InvoiceNo", _invoiceNo);
            param = cmd.Parameters.AddWithValue("@tblShippingNo", _tempListShipping);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                // Close the SqlDataReader. The SqlBulkCopy
                // object is automatically closed at the end
                // of the using block.
                conn.Close();
            }
            conn.Close();
            return true;
        }

        /// <method>
        /// Lock Shipping Instruction
        /// </method>
        public bool Lock_ShippingInstruction(string _shippingNo)
        {
            conn.Open();
            var cmd = new SqlCommand("SP_TVC_LOCK_SHIPPING_INSTRUCTION", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlParameter param = cmd.Parameters.AddWithValue("@ShippingNo", _shippingNo);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                // Close the SqlDataReader. The SqlBulkCopy
                // object is automatically closed at the end
                // of the using block.
                conn.Close();
            }
            conn.Close();
            return true;
        }

        /// <method>
        /// UnLock Shipping Instruction
        /// </method>
        public bool UnLock_ShippingInstruction(string _shippingNo)
        {
            conn.Open();
            var cmd = new SqlCommand("SP_TVC_UNLOCK_SHIPPING_INSTRUCTION", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlParameter param = cmd.Parameters.AddWithValue("@ShippingNo", _shippingNo);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                // Close the SqlDataReader. The SqlBulkCopy
                // object is automatically closed at the end
                // of the using block.
                conn.Close();
            }
            conn.Close();
            return true;
        }

        /// <method>
        /// Revise Shipping Instruction
        /// </method>
        public bool Revise_ShippingInstruction(string _shippingNo)
        {
            conn.Open();
            var cmd = new SqlCommand("SP_TVC_REVISE_SHIPPING_INSTRUCTION", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            SqlParameter param = cmd.Parameters.AddWithValue("@ShippingNo", _shippingNo);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                // Close the SqlDataReader. The SqlBulkCopy
                // object is automatically closed at the end
                // of the using block.
                conn.Close();
            }
            conn.Close();
            return true;
        }

        /// <method>
        /// SP Merge Data
        /// </method>
        public bool Merge_Data()
        {
            conn.Open();
            var cmd = new SqlCommand("SP_TVC_MERGE_DATA", conn)
            {
                CommandType = CommandType.StoredProcedure
            };
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                // Close the SqlDataReader. The SqlBulkCopy
                // object is automatically closed at the end
                // of the using block.
                conn.Close();
            }
            conn.Close();
            return true;
        }

        /// <method>
        /// Update Query
        /// </method>
        public bool executeUpdateQuery(String _query, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                myCommand.Connection = openConnection();
                myCommand.CommandText = _query;
                myCommand.Parameters.AddRange(sqlParameter);
                myAdapter.UpdateCommand = myCommand;
                myCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Error - Connection.executeUpdateQuery - Query: " + _query + " \nException: " + e.StackTrace.ToString());
                return false;
            }
            finally
            {
                conn.Close();
            }
            return true;
        }

        /// <method>
        /// Select Invoice By Shipping No
        /// </method>
        public DataTable executeSelectInv_ByShippingNo(DataTable _shippingNo)
        {
            DataTable dt = new DataTable();
            conn.Open();
            try
            {
                using (var cmd = new SqlCommand("SP_TVC_SELECT_INV_BY_SHIPNO", conn))
                using (var da = new SqlDataAdapter(cmd))
                { 
                    SqlParameter param = cmd.Parameters.AddWithValue("@tblShippingNo", _shippingNo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close the SqlDataReader. The SqlBulkCopy
                // object is automatically closed at the end
                // of the using block.
                conn.Close();
            }
            conn.Close();
            return dt;
        }

        /// <method>
        /// Select Packing List By Shipping No
        /// </method>
        public DataTable executeSelectPL_ByShippingNo(DataTable _shippingNo)
        {
            DataTable dt = new DataTable();
            conn.Open();
            try
            {
                using (var cmd = new SqlCommand("SP_TVC_SELECT_PL_BY_SHIPNO", conn))
                using (var da = new SqlDataAdapter(cmd))
                {
                    SqlParameter param = cmd.Parameters.AddWithValue("@tblShippingNo", _shippingNo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Close the SqlDataReader. The SqlBulkCopy
                // object is automatically closed at the end
                // of the using block.
                conn.Close();
            }
            conn.Close();
            return dt;
        }
    }
}
