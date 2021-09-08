using System;
using System.Collections.Generic;
using System.Text;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public class UserSitesApiResponse
    {
        public string Status { get; set; }
        public IEnumerable<UserSite> Sites { get; set; }
    }
}
