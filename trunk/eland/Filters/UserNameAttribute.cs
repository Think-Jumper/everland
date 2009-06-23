using System.Web.Mvc;

namespace eland.Filters
{
        public class UserNameAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                const string key = "userName";

                if (filterContext.ActionParameters.ContainsKey(key))
                {
                    if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                        filterContext.ActionParameters[key] = filterContext.HttpContext.User.Identity.Name;
                }

                base.OnActionExecuting(filterContext);
            }
        }
    }

