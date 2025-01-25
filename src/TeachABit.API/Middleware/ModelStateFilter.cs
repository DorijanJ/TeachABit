using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;

namespace TeachABit.API.Middleware
{
    public class ModelStateFilter(string messageType = MessageTypes.Global) : ActionFilterAttribute
    {
        private readonly string _messageType = messageType;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument is IValidatableObject validatable)
                {
                    var validationContext = new ValidationContext(validatable);
                    var results = validatable.Validate(validationContext);

                    foreach (var result in results)
                    {
                        context.ModelState.AddModelError(result.MemberNames.FirstOrDefault() ?? string.Empty, result.ErrorMessage!);
                    }
                }
            }

            var modelStateError = context.ModelState.Values.FirstOrDefault()?.Errors.FirstOrDefault();

            if (modelStateError != null)
            {
                ControllerResult result = new()
                {
                    Message = MessageDescriber.InvalidModelState(modelStateError.ErrorMessage, _messageType)
                };

                context.Result = new BadRequestObjectResult(result);
            }
        }
    }
}
