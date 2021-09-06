using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.DataWarehouse.SignOnSite
{
    public class AppConfig
    {
        public const int ApiPageSize = 100;

        public string ConnectionString { get; set; }
        public string SignOnSiteKey { get; set; }
    }
}
