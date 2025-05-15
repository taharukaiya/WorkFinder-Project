using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

public class RoleAuthorizeAttribute : ActionFilterAttribute
{
    private readonly string[] _roles;

    public RoleAuthorizeAttribute(params string[] roles)
    {
        _roles = roles;
    }

    //public override void OnActionExecuting(ActionExecutingContext context)
    //{
    //    var userRole = context.HttpContext.Session.GetString("UserRole");

    //    if (string.IsNullOrEmpty(userRole) || !_roles.Contains(userRole))
    //    {
    //        context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
    //    }

    //    base.OnActionExecuting(context);
    //}
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // 👇 Check if session is ready
        var session = context.HttpContext?.Session;
        if (session == null)
        {
            context.Result = new RedirectToActionResult("Login", "Account", null);
            return;
        }

        var userRole = session.GetString("UserRole");

        //if (string.IsNullOrEmpty(userRole) || !_roles.Contains(userRole))
        //{
        //    context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
        //    return;
        //}

        if (string.IsNullOrEmpty(userRole) || !_roles.Any(r => r.Equals(userRole, StringComparison.OrdinalIgnoreCase)))
        {
            context.Result = new RedirectToActionResult("AccessDenied", "Account", null);
            return;
        }



        base.OnActionExecuting(context);
    }
}




