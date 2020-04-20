using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using SqlInjectionActionFilter.Utility;
using System;
using System.Linq;
using System.Reflection;

namespace SqlInjectionActionFilter.Controllers
{
    public sealed class SqlInjectionFilter:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (canIgnoreMethod(context))
                return;
            var parameters = context.ActionDescriptor.Parameters;
            foreach (ParameterDescriptor item in parameters)
            {
                context.ActionArguments[item.Name] = ParameterSynthesizer.Synthesize(context.ActionArguments[item.Name]);
            } 
            base.OnActionExecuting(context);
        }

        private bool canIgnoreMethod(ActionExecutingContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor != null)
            {
                var actionAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(IgnoreInjectionFilter)).FirstOrDefault();
                
                if (actionAttributes!=null)
                    return true;
            }
            return false;
        }
    }
}
