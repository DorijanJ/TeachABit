using Microsoft.AspNetCore.Mvc;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;

namespace TeachABit.API.Controllers
{
    public class BaseController : ControllerBase
    {
        private static readonly Dictionary<MessageCode, int> ErrorCodeMapping = new()
        {
            { MessageCode.RegistrationError, 400 },
            { MessageCode.InvalidModelState, 400 },
            { MessageCode.PasswordMismatch, 401 },
            { MessageCode.Unauthenticated, 401 },
            { MessageCode.AccountLockedOut, 403 },
            { MessageCode.Unauthorized, 403 },
            { MessageCode.UserNotFound, 404 },
            { MessageCode.MethodNotAllowed, 405 },
            { MessageCode.DuplicateUsername, 409 },
            { MessageCode.DuplicateEmail, 409 },
            { MessageCode.DefaultError, 500 }
        };

        [NonAction]
        public IActionResult Error(ControllerResult controllerResult)
        {
            if (controllerResult.Message == null)
                return StatusCode(500, controllerResult);

            if (ErrorCodeMapping.TryGetValue(controllerResult.Message.MessageCode, out var statusCode))
                return StatusCode(statusCode, controllerResult);

            return StatusCode(500, controllerResult);
        }

        [NonAction]
        public IActionResult GetControllerResult<T>(ServiceResult<T> serviceResult)
        {
            ControllerResult result = CreateControllerResult(serviceResult.Message);
            result.Data = serviceResult.Data;
            return serviceResult.IsError ? Error(result) : Ok(result);
        }

        [NonAction]
        public IActionResult GetControllerResult(ServiceResult serviceResult)
        {
            var result = CreateControllerResult(serviceResult.Message);
            return serviceResult.IsError ? Error(result) : Ok(result);
        }

        [NonAction]
        public IActionResult GetControllerResult(MessageResponse messageResponse)
        {
            var result = CreateControllerResult(messageResponse);
            return messageResponse.MessageType.Severity == MessageSeverities.Error ? Error(result) : Ok(result);
        }

        private static ControllerResult CreateControllerResult(MessageResponse? messageResponse)
        {
            return new ControllerResult
            {
                Message = messageResponse ?? MessageDescriber.SuccessMessage()
            };
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
