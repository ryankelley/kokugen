#region Using Directives

using System.Web;

#endregion

namespace Kokugen.Core.Config
{
    public interface ICookieHandler
    {
        string UserId { get; }
        bool IsCookieThere { get; }
        ICookieHandler ForHttpRequest(HttpRequest httpRequest);
    }
}