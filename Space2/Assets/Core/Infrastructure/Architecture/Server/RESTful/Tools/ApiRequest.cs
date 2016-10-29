using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Infrastructure.Architecture.Server.RESTful.Tools
{
    /// <summary>
    /// All the data and functionality needed to dispatch a request and process the reply
    /// </summary>
    /// <typeparam name="TCallRawDataType">Data type expected from server</typeparam>
    /// <typeparam name="TReplyDataType">The data type to return after server answer is processed</typeparam>
    public abstract class ApiRequest<TCallRawDataType, TReplyDataType>
    {
        /// <summary>
        /// Dictionary for parameters to send in the RESTful request
        /// </summary>
        public Dictionary<string, object> Params = new Dictionary<string, object>();
            
        /// <summary>
        /// Url to send to
        /// </summary>
        public string Url;

        /// <summary>
        /// Any extra strings to add after URL but before params
        /// </summary>
        public string Path;

        /// <summary>
        /// Web method to use
        /// </summary>
        public WebMethod Method = WebMethod.Get;

        /// <summary>
        /// Function for processing the reply. Takes a server answer and returns parsed data.
        /// </summary>
        public Func<TCallRawDataType, TReplyDataType> ReplyHandler;

        /// <summary>
        /// Number of times to retry if connection fails
        /// </summary>
        public int TimeoutRetries = 3;

        /// <summary>
        /// Adds or removes a param to send in the RESTful request
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        protected void AddOrReplaceParam(string key, object value)
        {
            if (Params.ContainsKey(key))
                Params[key] = value;
            else
                Params.Add(key, value);
        }

        /// <summary>
        /// Generates a string from the params, to pass to a POST request
        /// </summary>
        /// <param name="isEscapingValues">if yes, values will be URLescaped</param>
        /// <returns>string to set as params of the POST</returns>
        public virtual string ToParamsString(bool isEscapingValues = false)
        {
            List<KeyValuePair<string, object>> p;
            if (isEscapingValues)
                p = Params.ToList();
            else
                p = Params.Select(sp =>
                {
                    var kvp = new KeyValuePair<string, object>(sp.Key, WWW.EscapeURL(sp.Value.ToString()));
                    return kvp;
                }).ToList();

            return
                Params.Count == 0
                    ? Path
                    : string.Format(Path + "?{0}",
                        string.Join("&", p.Select(kvp => string.Format("{0}={1}", kvp.Key, kvp.Value)).ToArray()));
        }

        /// <summary>
        /// Generates a string from the params, to pass to a GET request
        /// </summary>
        /// <param name="isEscapingValues">if yes, values will be URLescaped</param>
        /// <returns>string to set as params of the GET</returns>
        internal string ToGetString(bool isEscapingValues = false)
        {
            return Url + ToParamsString(isEscapingValues);
        }

        /// <summary>
        /// Returns a combination of url and path strings
        /// </summary>
        /// <returns>full path string</returns>
        internal string FullPath()
        {
            return Url + Path;
        }

        /// <summary>
        /// Returns the request's params as WWWForm
        /// </summary>
        /// <returns>WWWForm filled with params</returns>
        internal WWWForm ToWWWForm()
        {
            var form = new WWWForm();

            foreach (KeyValuePair<string, object> kvp in Params)
            {
                form.AddField(kvp.Key, kvp.Value.ToString());
            }

            return form;
        }
    }
}