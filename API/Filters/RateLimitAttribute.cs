using System;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Linq;
using System.ServiceModel.Channels;
using System.Collections.Concurrent;
using System.Configuration;

namespace API.Filters
{
    public class RateLimitAttribute : ActionFilterAttribute
    {
        private const string DefApiKey = "apikey";
        private const string DefDefaultMessageTemplate = "You can perform this action every {0} seconds.";
        private const string DefApiKeyNotProvided = "Api key not provided";
        private static ConcurrentDictionary<string, int> _customersLimits = new ConcurrentDictionary<string, int>();
        private static int _defaultLimit = int.Parse(ConfigurationManager.AppSettings["defaultlimit"]);
        public string Message { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var key = GetQueryString(actionContext.Request, DefApiKey);

            if (string.IsNullOrEmpty(key))
            {
                BlockResponse(actionContext, DefApiKeyNotProvided);
                return;
            }

            var limit = _defaultLimit;
            if (!_customersLimits.ContainsKey(key))
            {
                int customerRateLimit;
                if(int.TryParse(ConfigurationManager.AppSettings[key], out customerRateLimit))
                    _customersLimits.TryAdd(key, customerRateLimit);
            }

            if(_customersLimits.ContainsKey(key))
                limit = _customersLimits.ContainsKey(key) ? _customersLimits[key] : _defaultLimit;

            var allowExecute = false;

            if (HttpRuntime.Cache[key] == null)
            {
                HttpRuntime.Cache.Add(key, true, null, DateTime.Now.AddSeconds(limit), 
                    Cache.NoSlidingExpiration, CacheItemPriority.Low, null);

                allowExecute = true;
            }

            if (!allowExecute)
                BlockResponse(actionContext, string.Format(DefDefaultMessageTemplate, limit));
        }

        private void BlockResponse(HttpActionContext actionContext, string message)
        {
            if (string.IsNullOrEmpty(Message))
                Message = message;

            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.Conflict, Message);
        }

        private static string GetQueryString(HttpRequestMessage request, string key)
        {
            // IEnumerable<KeyValuePair<string,string>> - right!
            var queryStrings = request.GetQueryNameValuePairs();
            if (queryStrings == null)
                return null;

            var match = queryStrings.FirstOrDefault(kv => string.Compare(kv.Key, key, true) == 0);
            if (string.IsNullOrEmpty(match.Value))
                return null;

            return match.Value;
        }
    }
}