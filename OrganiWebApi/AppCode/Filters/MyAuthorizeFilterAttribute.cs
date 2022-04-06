using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganiWebApi.AppCode.Filters
{
    public class MyAuthorizeFilterAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Request.Headers.Add("author", "abbas");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string user, password;
            bool hasUserName = context.HttpContext.Request.Headers.TryGetValue("user", out StringValues userNameValue);
            bool hasPassword = context.HttpContext.Request.Headers.TryGetValue("password", out StringValues passwordValue);

            if (hasUserName && hasPassword)
            {
                user = userNameValue.FirstOrDefault();
                password = passwordValue.FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(password))
                {
                    if (user == "admin" && password == "admin")
                    {
                        //ok
                        return;
                    }
                    else
                    {
                        //error
                        goto unAuthorized;
                    }
                }
                else
                {
                    //error
                    goto unAuthorized;
                }
            }
            else
            {
                //error
                goto unAuthorized;
            }

        unAuthorized:
            context.Result = new JsonResult(new
            {
                error = true,
                message = "istifadeci melumatlari duzgun deyil"
            });
            
        }
    }
}
