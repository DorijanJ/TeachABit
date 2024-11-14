using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;

namespace TeachABit.API.Middleware
{
    public class ModelStateFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var modelStateError = context.ModelState.Values.FirstOrDefault()?.Errors.FirstOrDefault();

            if (modelStateError != null)
            {
                ControllerResult result = new()
                {
                    Message = MessageDescriber.InvalidModelState(modelStateError.ErrorMessage)
                };

                context.Result = new BadRequestObjectResult(result);
            }
        }
    }
}
