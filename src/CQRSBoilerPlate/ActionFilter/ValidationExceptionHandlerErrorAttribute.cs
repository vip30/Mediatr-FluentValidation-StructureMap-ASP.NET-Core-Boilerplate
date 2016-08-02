using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CQRSBoilerPlate.Models.SystemViewModels;

namespace CQRSBoilerPlate.ActionFilter
{
    public class ValidationExceptionHandlerErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //only handle ValidationExceptions
            if ((context.Exception as ValidationException) != null)
            {
                var errorList = new List<ErrorViewModel>();
                foreach (var validationsfailures in (context.Exception as ValidationException).Errors)
                {
                    errorList.Add(new ErrorViewModel
                    {
                        ErrorCode = validationsfailures.ErrorCode,
                        ErrorMessage = validationsfailures.ErrorMessage
                    });
                }
                var result = new JsonResult(errorList);
                result.ContentType = "application/json";
                // TODO: Pass additional detailed data via ViewData
                context.ExceptionHandled = true; // mark exception as handled
                context.Result = result;
                context.HttpContext.Response.StatusCode = 400;
            }
        }
    }
}
