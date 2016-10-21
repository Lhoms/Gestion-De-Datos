using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Globalization;


namespace ClinicaFrba.extras
{
    class Config
    {
        public static string GetConfigValue(String key)
        {
            try
            {

                return ConfigurationManager.AppSettings.Get(key).ToString();

            }
            catch
            {
                return null;
            }
        }



    }
}
