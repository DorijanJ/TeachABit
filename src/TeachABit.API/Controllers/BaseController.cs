using Microsoft.AspNetCore.Mvc;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;

namespace TeachABit.API.Controllers
{
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult Error(ControllerResult controllerResult)
        {
            if (controllerResult.Message == null || controllerResult.Message.MessageStatusCode == null)
                return StatusCode(500, controllerResult);

            return StatusCode((int)controllerResult.Message.MessageStatusCode, controllerResult);
        }

        [NonAction]
        public IActionResult GetControllerResult<T>(ServiceResult<T> serviceResult)
        {
            ControllerResult result = new()
            {
                Message = serviceResult.Message ?? MessageDescriber.SuccessMessage(),
                Data = serviceResult.Data
            };
            return serviceResult.IsError ? Error(result) : Ok(result);
        }

        [NonAction]
        public IActionResult GetControllerResult(ServiceResult serviceResult)
        {
            ControllerResult result = new()
            {
                Message = serviceResult.Message,
            };
            return serviceResult.IsError ? Error(result) : Ok(result);
        }

        [NonAction]
        public IActionResult GetControllerResult(MessageResponse messageResponse)
        {
            ControllerResult result = new()
            {
                Message = messageResponse
            };
            return messageResponse.MessageType.Severity == MessageSeverities.Error ? Error(result) : Ok(result);
        }

        [NonAction]
        public MessageResponse? GetModelStateError(MessageType? messageType = null)
        {
            if (ModelState.IsValid || !ModelState.Values.Any()) return null;

            var modelStateError = ModelState.Values.First().Errors.FirstOrDefault();
            if (modelStateError != null)
            {
                return MessageDescriber.InvalidModelState(modelStateError.ErrorMessage, messageType ?? MessageTypes.GlobalError);
            }

            return null;
        }
    }
}
