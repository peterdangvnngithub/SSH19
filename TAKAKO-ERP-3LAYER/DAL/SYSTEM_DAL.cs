using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAKAKO_ERP_3LAYER.DAL
{
    public class SYSTEM_DAL
    {
        private string companyCode;
        private string companyName;
        private string userName;
        private string department;
        private string logNew;
        private string logEdit;
        private string logDelete;
        private string logError;

        public string CompanyCode { get => companyCode; set => companyCode = value; }
        public string CompanyName { get => companyName; set => companyName = value; }
        public string UserName { get => userName; set => userName = value; }
        public string LogNew { get => logNew; set => logNew = value; }
        public string LogEdit { get => logEdit; set => logEdit = value; }
        public string LogDelete { get => logDelete; set => logDelete = value; }
        public string LogError { get => logError; set => logError = value; }
        public string Department { get => department; set => department = value; }
    }
}
