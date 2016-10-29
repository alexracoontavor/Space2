using System.Collections.Generic;

namespace Assets.Infrastructure.CoreTools.Instanced.HttpTools
{
    public interface IHttpTools
    {
        string FormatUrl(string url, Dictionary<string, object> parameters);
    }
}