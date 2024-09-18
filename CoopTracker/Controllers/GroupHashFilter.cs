using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
namespace CoopTracker.Controllers;

public class GroupHashFilter : IActionFilter
{

    public  void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.HttpContext.Request;
        var query = request.Query;
        var db = context.HttpContext.RequestServices.GetRequiredService<CoopTrackerDbContext>();
        // Check if the grouphash exists
        if (!query.ContainsKey("grouphash"))
        {
            // Generate a random 10-character hash
            var groupHash = GenerateRandomString(10);
            context.HttpContext.Session.SetString("groupHashGroup", groupHash);
            var newGroup = new GroupKeyMaster { HashGroup = groupHash };
            db.GroupKeyMasters.Add(newGroup);
             db.SaveChanges();
            context.HttpContext.Session.SetInt32("groupkey", newGroup.GroupKeyMasterId);
            // Redirect to the same URL with the new grouphash
            var url = $"{request.Scheme}://{request.Host}{request.Path}?grouphash={groupHash}";

            foreach (var key in query.Keys)
            {
                if (key != "grouphash")
                {
                    url += $"&{key}={query[key]}";
                }
            }

            context.Result = new RedirectResult(url);
        }
        else if (query.ContainsKey("grouphash"))
        {

            var group = db.GroupKeyMasters.SingleOrDefault(e => e.HashGroup == query["grouphash"].Single());
            if (group == null) {
                group = new GroupKeyMaster { HashGroup = query["grouphash"].Single() };
                db.GroupKeyMasters.Add(group);
                db.SaveChanges();
            }
            //// Generate a random 10-character hash
            //var groupHash = string.IsNullOrWhiteSpace(query["grouphash"]) ? GenerateRandomString(10) : query["grouphash"].Single();
            ////var groupHash = query.Keys.Count(e => e == "grouphash") == 0 ? GenerateRandomString(10) : query.Keys.Single(e => e == "grouphash");
            context.HttpContext.Session.SetString("groupHashGroup", group.HashGroup);
            context.HttpContext.Session.SetInt32("groupkey", group.GroupKeyMasterId);
            //// Redirect to the same URL with the new grouphash
            //var url = $"{request.Scheme}://{request.Host}{request.Path}?grouphash={groupHash}";

            //foreach (var key in query.Keys)
            //{
            //    if (key != "grouphash")
            //    {
            //        url += $"&{key}={query[key]}";
            //    }
            //}

            // context.Result = new RedirectResult(url);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // Not used in this case
    }

    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var stringBuilder = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            stringBuilder.Append(chars[random.Next(chars.Length)]);
        }

        return stringBuilder.ToString();
    }
}
