using System.Collections.Generic;
using System.Linq;

namespace Assets.Infrastructure.CoreTools.Instanced.HttpTools
{
    public class DefaultHttpTools : IHttpTools
    {
        public string FormatUrl(string url, Dictionary<string, object> parameters)
        {
            return string.Format(url + "?{0}",
                string.Join("&", parameters.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)).ToArray()));
        }
    }
}