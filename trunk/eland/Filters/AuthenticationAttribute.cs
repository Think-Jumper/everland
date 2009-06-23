using System.Web.Mvc;
using System.Web.Security;

namespace eland.Filters
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated) return;
            var redirectOnSuccess = filterContext.HttpContext.Request.Url.AbsolutePath;
            var redirectUrl = string.Format("?ReturnUrl={0}", redirectOnSuccess);
            var loginUrl = FormsAuthentication.LoginUrl + redirectUrl;

            filterContext.HttpContext.Response.Redirect(loginUrl, true);
        }
    }
}
