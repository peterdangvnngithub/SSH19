using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAKAKO_ERP_3LAYER.DAO
{
    public class LOG_DAO
    {
        private DBConnection conn;

        /// <constructor>
        /// Constructor LOG_DAO
        /// </constructor>
        public LOG_DAO()
        {
            conn = new DBConnection();
        }

        public DataTable GetInfoLog(DateTime DateCreateFrom
                                  , DateTime DateCreateTo
                                  , string TypeSearch)
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT   
                                 [COMPANY_CODE]
                                ,[USER_NAME]
                                ,[DATE_CREATE]
                                ,[TYPE]
                                ,[CONTENT_LOG]
                        FROM
                                [dbo].[TVC_HANDLE_LOG]
                        WHERE
                                (   [DATE_CREATE]  >=   @dateCreateFrom
                                AND [DATE_CREATE]  <=   @dateCreateTo)
                        AND     TYPE                =   @typeSearch
                        ORDER BY
                                 [DATE_CREATE]
                                ,[TYPE]";
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@dateCreateFrom", SqlDbType.Date);
            sqlParameters[0].Value = Convert.ToDateTime(DateCreateFrom);
            sqlParameters[1] = new SqlParameter("@dateCreateTo", SqlDbType.Date);
            sqlParameters[1].Value = Convert.ToDateTime(DateCreateTo);
            sqlParameters[2] = new SqlParameter("@typeSearch", SqlDbType.VarChar);
            sqlParameters[2].Value = Convert.ToString(TypeSearch);
            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }

        public bool InsertLog(string companyCode, string username, string _typeLog,string content)
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"INSERT INTO [TVC_HANDLE_LOG]
                                ([COMPANY_CODE]
                                ,[USER_NAME]
                                ,[DATE_CREATE]
                                ,[TYPE]
                                ,[CONTENT_LOG])
                            VALUES
                                (@companyCode
                                ,@username
                                ,GETDATE()
                                ,@typeLog
                                ,@content)";
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@companyCode", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(companyCode);
            sqlParameters[1] = new SqlParameter("@username", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(username);
            sqlParameters[2] = new SqlParameter("@typeLog", SqlDbType.VarChar);
            sqlParameters[2].Value = Convert.ToString(_typeLog);
            sqlParameters[3] = new SqlParameter("@content", SqlDbType.VarChar);
            sqlParameters[3].Value = Convert.ToString(content);
            return conn.executeInsertQuery(StrQuery, sqlParameters);
        }

        public DataTable Count_Log()
        {
            string StrQuery = "";
            DataTable _tempDataTable = new DataTable();

            StrQuery = @"SELECT
                         	TYPE, COUNT(TYPE)  AS COUNT_BY_TYPE
                         FROM
                         	TVC_HANDLE_LOG
                         GROUP BY
                         	TYPE";

            SqlParameter[] sqlParameters = new SqlParameter[0];
            return conn.executeSelectQuery(StrQuery, sqlParameters);
        }
    }
}
