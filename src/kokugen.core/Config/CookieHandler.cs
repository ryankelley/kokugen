#region Using Directives

using System;
using System.Web;

#endregion

namespace Kokugen.Core.Config
{
    public class CookieHandler : ICookieHandler
    {
        private readonly string _cookiePath;
        private bool _cookieIsThere;
        private string _userId;

        public CookieHandler(string cookie_path)
        {
            _cookiePath = cookie_path;
            _userId = "";
        }

        #region ICookieHandler Members

        public ICookieHandler ForHttpRequest(HttpRequest httpRequest)
        {
            if (httpRequest != null &&
                httpRequest.Cookies != null &&
                httpRequest.Cookies[_cookiePath] != null)
            {
                // ReSharper disable PossibleNullReferenceException
                _userId = HttpContext.Current.Request.Cookies[_cookiePath].Value;
                HttpContext.Current.Request.Cookies[_cookiePath].Expires = DateTime.Now.AddDays(14);
                // ReSharper restore PossibleNullReferenceException
                _cookieIsThere = !string.IsNullOrEmpty(_userId);
            }
            return this;
        }

        public string UserId
        {
            get { return _userId; }
        }

        public bool IsCookieThere
        {
            get { return _cookieIsThere; }
        }

        #endregion
    }
}