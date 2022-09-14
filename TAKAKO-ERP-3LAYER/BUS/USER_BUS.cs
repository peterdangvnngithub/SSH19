using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAKAKO_ERP_3LAYER.DAO;
using TAKAKO_ERP_3LAYER.DAL;

namespace TAKAKO_ERP_3LAYER.BUS
{
    /// <summary>
    /// 
    /// </summary>
    public class USER_BUS
    {
        private USER_DAO _userDAO;

        /// <constructor>
        /// Constructor UserBUS
        /// </constructor>
        public USER_BUS()
        {
            _userDAO = new USER_DAO();
        }

        /// <method>
        /// Get UserName and PassWord and return USER_DAL
        /// </method>
        public USER_DAL getUser(string _companyCode
                              , string _userName
                              , string _passWord)
        {
            USER_DAL _userDAL = new USER_DAL();
            DataTable _dtUser = new DataTable();

            _dtUser = _userDAO.searchUser(_companyCode, _userName, _passWord);

            if (_dtUser != null && _dtUser.Rows.Count > 0)
            {
                foreach (DataRow drUser in _dtUser.Rows)
                {
                    _userDAL.CompanyCode = drUser["COMPANY_CODE"].ToString();
                    _userDAL.UserName = drUser["USERNAME"].ToString();
                    _userDAL.PassWord = drUser["PASSWORD"].ToString();
                    _userDAL.Department = drUser["DEPARTMENT"].ToString();
                }
            }

            return _userDAL;
        }
    }
}
