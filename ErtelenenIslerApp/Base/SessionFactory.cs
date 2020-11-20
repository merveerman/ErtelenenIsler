using ErtelenenIslerApp.Data;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErtelenenIslerApp.Base
{
    public class SessionFactory
    {
        private static ISessionFactory _sessionFactory;

        private static readonly object Padlock = new object();

        private SessionFactory()
        {
        }

        public static ISessionFactory GetFactory()
        {
            lock (Padlock)
            {
                if (_sessionFactory == null)
                {
                    try
                    {
                        string connStr = DbSettings.GetConnectionString("ConnectionString");

                        _sessionFactory = Fluently.Configure().Database(OracleClientConfiguration.Oracle10.ConnectionString(connStr))
                           .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PersonnelEntity>())
                           .ExposeConfiguration(x => x.SetProperty("connection.release_mode", "on_close"))
                           .BuildSessionFactory();
                    }
                    catch (Exception exc)
                    {
                        throw new Exception(string.Format("Bağlantı sağlanamadı.Detay:{0}", exc.Message + " \n" + exc.InnerException));
                    }
                }
            }

            return _sessionFactory;
        }

        public static ISessionFactory GetFactory(String connectionString)
        {
            lock (Padlock)
            {
                if (_sessionFactory == null)
                {
                    try
                    {
                        _sessionFactory = Fluently.Configure().Database(OracleClientConfiguration.Oracle10.ConnectionString(connectionString))
                           .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PersonnelEntity>())
                           .ExposeConfiguration(x => x.SetProperty("connection.release_mode", "on_close"))
                           .BuildSessionFactory();


                    }
                    catch (Exception exc)
                    {
                        throw new Exception(string.Format("Bağlantı sağlanamadı.Detay:{0}", exc.Message));
                    }
                }
            }

            return _sessionFactory;
        }
    }
}
