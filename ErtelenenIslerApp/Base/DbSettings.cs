using System;
using System.Configuration;

namespace ErtelenenIslerApp.Base
{
    public static class DbSettings
    {
        public static string ConnectionString { get; set; }

        public static string GetConnectionString(string key)
        {
            string connectionString = null;
            try
            {
                connectionString = ConfigurationManager.AppSettings[key].ToString();

                return connectionString;
            }
            catch (Exception exc)
            {
                throw new Exception("Konfigurasyon dosyasından bağlantı ayarları okunamadı!");
            }
        }
    }
}
