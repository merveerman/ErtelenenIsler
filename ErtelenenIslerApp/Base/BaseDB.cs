using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErtelenenIslerApp.Base
{
    public class BaseDB
    {
        public string ConnectionString { get; set; }
        public ISessionFactory Session { get; set; }


        public BaseDB()
        {
            ConnectionString = DbSettings.GetConnectionString("ConnectionString");

            Session = SessionFactory.GetFactory(ConnectionString);

        }

    }
}
