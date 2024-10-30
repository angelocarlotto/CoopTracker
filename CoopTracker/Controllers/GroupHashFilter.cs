using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
namespace CoopTracker.Controllers;

public class GroupHashFilter : IActionFilter
{

    public void OnActionExecuting(ActionExecutingContext context)
    {

          // Skip the filter for API requests
        if (context.HttpContext.Request.Path.StartsWithSegments("/api"))
        {
            return;
        }
        
        var TenantId = context.HttpContext.Session.GetString("TenantId");
        if ((TenantId=="default"|| string.IsNullOrWhiteSpace(TenantId)) && context.Controller.GetType() != typeof(LoginController))
        {
            context.Result = new RedirectToActionResult("Index", "Login", null);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Not used in this case
    }
}
