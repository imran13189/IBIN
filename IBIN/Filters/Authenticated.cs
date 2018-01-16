using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.Routing;

namespace IBIN.Filters
{
    public class Authenticated : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                if(!HttpContext.Current.User.Identity.IsAuthenticated)
                //if (BuildersAlliances.Common.SessionManager.LoggedInUser.RoleId==null)
                {
                    if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                    {
                        HttpContext.Current.Response.Redirect("~/Admin");
                    }
                }
            }
            catch
            {

            }
            
        }
    }
}