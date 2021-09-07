using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public class BriefingLogActiveDay
    {
        public DateTime Day { get; set; }

        public IEnumerable<BriefingLog> Users { get; set; }
    }
}
