using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAKAKO_ERP_3LAYER.DAO
{
    public class USER_DAO
    {
        private DBConnection conn;

        /// <constructor>
        /// Constructor UserDAO
        /// </constructor>
        public USER_DAO()
        {
            conn = new DBConnection();
        }

        /// <method>
        /// Get User Name and PassWord
        /// </method>
        public DataTable searchUser(string _companyCode,string _userName, string _passWord)
        {
            string query = string.Format("select COMPANY_CODE, USERNAME, PASSWORD, DEPARTMENT from [TVC_USER_MS] where COMPANY_CODE = @companyCode and USERNAME = @userName and PASSWORD = @passWord");
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@companyCode", SqlDbType.VarChar);
            sqlParameters[0].Value = Convert.ToString(_companyCode);
            sqlParameters[1] = new SqlParameter("@userName", SqlDbType.VarChar);
            sqlParameters[1].Value = Convert.ToString(_userName);
            sqlParameters[2] = new SqlParameter("@passWord", SqlDbType.VarChar);
            sqlParameters[2].Value = Convert.ToString(_passWord);
            return conn.executeSelectQuery(query, sqlParameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Merge_Data()
        {
            return conn.Merge_Data();
        }
    }
}
