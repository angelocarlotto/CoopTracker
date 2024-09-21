using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
namespace CoopTracker.Controllers;

public class GroupHashFilter : IActionFilter
{

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var TenantId = context.HttpContext.Session.GetString("TenantId");
        if (string.IsNullOrWhiteSpace(TenantId) && context.Controller.GetType() != typeof(LoginController))
        {
            context.Result = new RedirectToActionResult("Index", "Login", null);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Not used in this case
    }
}
