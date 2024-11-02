namespace TeachABit.Model.DTOs.Result.Message
{
    public class MessageDescriber
    {
        public static MessageResponse DefaultError() => new("Server error.", MessageTypeDescriber.GlobalError, MessageStatusCode.InternalServerError);
        public static MessageResponse DefaultError(string errorMessage) => new(errorMessage, MessageTypeDescriber.GlobalError, MessageStatusCode.InternalServerError);
        public static MessageResponse UserNotFound() => new("User not found.", MessageTypeDescriber.AuthenticationError, MessageStatusCode.NotFound);
        public static MessageResponse PasswordMismatch() => new("Wrong password.", MessageTypeDescriber.AuthenticationError, MessageStatusCode.Unauthorized);
        public static MessageResponse DuplicateUsername(string username) => new($"The username '{username}' is already taken.", MessageTypeDescriber.AuthenticationError, MessageStatusCode.Conflict);
        public static MessageResponse DuplicateEmail(string email) => new($"The email '{email}' is already taken.", MessageTypeDescriber.AuthenticationError, MessageStatusCode.Conflict);
        public static MessageResponse RegistrationError(string errorMessage) => new(errorMessage, MessageTypeDescriber.AuthenticationError, MessageStatusCode.BadRequest);
        public static MessageResponse Unauthenticated() => new("Unauthenticated.", MessageTypeDescriber.AuthenticationError, MessageStatusCode.Unauthorized);
        public static MessageResponse Unauthorized() => new("Unauthorized.", MessageTypeDescriber.AuthenticationError, MessageStatusCode.Forbidden);
        public static MessageResponse InvalidModelState(string errorMessage, MessageType messageType) => new(errorMessage, messageType, MessageStatusCode.BadRequest);
        public static MessageResponse MethodNotAllowed() => new("Method not allowed.", MessageTypeDescriber.GlobalError, MessageStatusCode.MethodNotAllowed);
        public static MessageResponse BadRequest(string errorMessage) => new(errorMessage, MessageTypeDescriber.GlobalError, MessageStatusCode.BadRequest);
        public static MessageResponse MissingConfiguration() => new("The server currently can't complete this operation.", MessageTypeDescriber.GlobalError, MessageStatusCode.InternalServerError);
        public static MessageResponse ItemNotFound() => new("Item not found.", MessageTypeDescriber.GlobalError, MessageStatusCode.NotFound);
        public static MessageResponse EmailConfimationSent() => new("An email with a confirmation link has been sent.", MessageTypeDescriber.AuthenticationInfo);
        public static MessageResponse EmailConfirmed() => new("Your email has been confirmed", MessageTypeDescriber.SpecificInfo);
        public static MessageResponse EmailNotConfirmed() => new("Email has not been confirmed", MessageTypeDescriber.AuthenticationError, MessageStatusCode.Unauthorized);

        public static MessageResponse AccountLockedOut(DateTimeOffset duration)
        {
            var time = duration - DateTimeOffset.UtcNow;
            return time.TotalSeconds <= 0
                ? new MessageResponse("Your account is locked for a brief period due to multiple failed login attempts.", MessageTypeDescriber.AuthenticationError, MessageStatusCode.Forbidden)
                : new MessageResponse($"Your account is locked for {string.Format("{0}:{1:00}s", (int)time.TotalMinutes, time.Seconds)} due to multiple failed login attempts.", MessageTypeDescriber.AuthenticationError, MessageStatusCode.Forbidden);
        }
        public static MessageResponse SuccessMessage(string? message = null) => new(message ?? "Request completed successfully", MessageTypeDescriber.Success);
    }

}
