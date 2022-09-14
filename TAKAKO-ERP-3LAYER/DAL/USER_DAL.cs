using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAKAKO_ERP_3LAYER.DAL
{
    public class USER_DAL
    {
        private string _companyCode;
        private string _userName;
        private string _passWord;
        private string _department;
        private string _companyLogin;

        /// <constructor>
        /// Constructor DAL_USER
        /// </constructor>
        public USER_DAL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string CompanyCode { get => _companyCode; set => _companyCode = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string PassWord { get => _passWord; set => _passWord = value; }
        public string Department { get => _department; set => _department = value; }
        public string CompanyLogin { get => _companyLogin; set => _companyLogin = value; }
    }
}
