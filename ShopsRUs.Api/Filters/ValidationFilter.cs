using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsRUs.Api.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(pair => pair.Key, pair => pair.Value.Errors.Select(x => x.ErrorMessage))
                    .ToArray();

                var errorResponse = new ErrorResponse();

                foreach (var (key, value) in errorsInModelState)
                {
                    foreach (var subError in value)
                    {
                        errorResponse.Errors.Add(new ErrorModel
                        {
                            FieldName = key,
                            Message = subError
                        });
                    }
                }

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }
            await next();
        }
    }
}
