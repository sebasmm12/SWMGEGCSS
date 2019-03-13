using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
namespace SWMGEGCSS_DA.Base
{
    public class BaseConexion
    {
        private Database _Database;
        internal Database Database
        {
            get { return _Database; }
            private set { _Database = value; }
        }
        public BaseConexion()
        {
            DatabaseProviderFactory factory = new DatabaseProviderFactory();
            Database = factory.Create("cndatabase") as Database;
        }
    }
}
