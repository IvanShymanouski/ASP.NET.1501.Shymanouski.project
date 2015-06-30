using System.Security.Principal;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace TaskManager.Authentification
{
    public class SupportPrincipalService : IPrincipalService
    {
        private readonly HttpContextBase _context;

        public SupportPrincipalService(HttpContextBase context)
        {
            _context = context;
        }

        #region IPrincipalService Members

        public IPrincipal GetCurrent()
        {            
            if (null != HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName])
            {
                var cookie = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                var userCookie = JsonConvert.DeserializeObject<Cookie>(cookie.UserData);
                if (null != userCookie) _context.User = new Principal(new Identity(userCookie));
            }

            if (null != _context.User && _context.User is Principal) return _context.User;

            return new Principal(new Identity(_context.User));
        }
        #endregion
    }

    public interface IPrincipalService
    {
        IPrincipal GetCurrent();
    }
}