using System.Web.Mvc;
using System.Web.Security;

namespace eland.Filters
{
    public class AuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.HttpContext.User.Identity.IsAuthenticated) return;
            var loginUrl = FormsAuthentication.LoginUrl + "?ReturnUrl=/Users/Index/";

            filterContext.HttpContext.Response.Redirect(loginUrl, true);
        }
    }
}
