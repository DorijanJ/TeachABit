namespace TeachABit.Model.DTOs.Result.Message
{
    public class MessageDescriber
    {
        public static MessageResponse DefaultError() => new("Server error.", MessageTypes.GlobalError, MessageStatusCode.InternalServerError);
        public static MessageResponse DefaultError(string errorMessage) => new(errorMessage, MessageTypes.GlobalError, MessageStatusCode.InternalServerError);
        public static MessageResponse UserNotFound() => new("User not found.", MessageTypes.AuthenticationError, MessageStatusCode.NotFound);
        public static MessageResponse PasswordMismatch() => new("Wrong password.", MessageTypes.AuthenticationError, MessageStatusCode.Unauthorized);
        public static MessageResponse DuplicateUsername(string username) => new($"The username '{username}' is already taken.", MessageTypes.AuthenticationError, MessageStatusCode.Conflict);
        public static MessageResponse DuplicateEmail(string email) => new($"The email '{email}' is already taken.", MessageTypes.AuthenticationError, MessageStatusCode.Conflict);
        public static MessageResponse RegistrationError(string errorMessage) => new(errorMessage, MessageTypes.AuthenticationError, MessageStatusCode.BadRequest);
        public static MessageResponse Unauthenticated() => new("Unauthenticated.", MessageTypes.AuthenticationError, MessageStatusCode.Unauthorized);
        public static MessageResponse Unauthorized() => new("Unauthorized.", MessageTypes.AuthenticationError, MessageStatusCode.Forbidden);
        public static MessageResponse InvalidModelState(string errorMessage, MessageType messageType) => new(errorMessage, messageType, MessageStatusCode.BadRequest);
        public static MessageResponse MethodNotAllowed() => new("Method not allowed.", MessageTypes.BadRequest, MessageStatusCode.MethodNotAllowed);
        public static MessageResponse BadRequest(string errorMessage) => new(errorMessage, MessageTypes.BadRequest, MessageStatusCode.BadRequest);
        public static MessageResponse MissingConfiguration() => new("The server currently can't complete this operation.", MessageTypes.GlobalError, MessageStatusCode.InternalServerError);
        public static MessageResponse AccountLockedOut(DateTimeOffset duration)
        {
            var time = duration - DateTimeOffset.UtcNow;
            return time.TotalSeconds <= 0
                ? new MessageResponse("Your account is locked for a brief period due to multiple failed login attempts.", MessageTypes.AuthenticationError, MessageStatusCode.Forbidden)
                : new MessageResponse($"Your account is locked for {string.Format("{0}:{1:00}s", (int)time.TotalMinutes, time.Seconds)} due to multiple failed login attempts.", MessageTypes.AuthenticationError, MessageStatusCode.Forbidden);
        }
        public static MessageResponse SuccessMessage(string? message = null) => new(message ?? "Request completed successfully", MessageTypes.Success);
    }

}
