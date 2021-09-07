using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace MAD.DataWarehouse.SignOnSite.Api
{
    public static class QueryUtil
    {
        public static string CreateQueryString(this IDictionary<string, object> queryParams)
        {
            var queryBuilder = HttpUtility.ParseQueryString(string.Empty);

            foreach (var qp in queryParams)
            {
                if (qp.Value is null)
                    continue;

                if (qp.Value is DateTime dt)
                {
                    queryBuilder[qp.Key] = dt.ToString("yyyy-MM-dd");
                }
                else
                {
                    queryBuilder[qp.Key] = qp.Value.ToString();
                }
            }

            return queryBuilder.ToString();
        }
    }
}
