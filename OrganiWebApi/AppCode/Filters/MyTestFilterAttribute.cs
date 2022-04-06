using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OrganiWebApi.AppCode.Filters
{
    public class MyTestFilterAttribute : IActionFilter
    {
        const string purePath = "log-api.txt";
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), purePath);
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var cad = context.ActionDescriptor as ControllerActionDescriptor;

            using (var sw = new StreamWriter(filePath,true))
            {
                sw.WriteLine(@$"{cad.ControllerName}-{cad.ActionName}/OnActionExecuting {DateTime.Now: dd:MM:yy hh.mm.ss}");
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var cad = context.ActionDescriptor as ControllerActionDescriptor;
            using (var sw = new StreamWriter(filePath,true))
            {
                sw.WriteLine(@$"{cad.ControllerName}-{cad.ActionName}/OnActionExecuted {DateTime.Now: dd:MM:yy hh.mm.ss}");
            }

            context.HttpContext.Response.Headers.Add("author", "P508");
        }
    }
}
