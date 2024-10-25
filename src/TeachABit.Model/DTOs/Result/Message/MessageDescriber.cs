namespace TeachABit.Model.DTOs.Result.Message
{
    public class MessageDescriber
    {
        public static MessageResponse DefaultError() => new("Server error.", MessageCode.DefaultError, MessageTypes.GlobalError);
        public static MessageResponse DefaultError(string errorMessage) => new(errorMessage, MessageCode.DefaultError, MessageTypes.GlobalError);
        public static MessageResponse UserNotFound() => new("User not found.", MessageCode.UserNotFound, MessageTypes.AuthenticationError);
        public static MessageResponse PasswordMismatch() => new("Wrong password.", MessageCode.PasswordMismatch, MessageTypes.AuthenticationError);
        public static MessageResponse DuplicateUsername(string username) => new($"The username '{username}' is already taken.", MessageCode.DuplicateUsername, MessageTypes.AuthenticationError);
        public static MessageResponse DuplicateEmail(string email) => new($"The email '{email}' is already taken.", MessageCode.DuplicateEmail, MessageTypes.AuthenticationError);
        public static MessageResponse RegistrationError(string errorMessage) => new(errorMessage, MessageCode.RegistrationError, MessageTypes.AuthenticationError);
        public static MessageResponse Unauthenticated() => new("Unauthenticated.", MessageCode.Unauthenticated, MessageTypes.AuthenticationError);
        public static MessageResponse Unauthorized() => new("Unauthorized.", MessageCode.Unauthorized, MessageTypes.AuthenticationError);
        public static MessageResponse InvalidModelState(string errorMessage, MessageType messageType) => new(errorMessage, MessageCode.InvalidModelState, messageType);
        public static MessageResponse MethodNotAllowed() => new("Method not allowed.", MessageCode.MethodNotAllowed, MessageTypes.BadRequest);
        public static MessageResponse AccountLockedOut(DateTimeOffset duration)
        {
            var time = duration - DateTimeOffset.UtcNow;
            return time.TotalSeconds <= 0
                ? new MessageResponse("Your account is locked for a brief period due to multiple failed login attempts.", MessageCode.AccountLockedOut, MessageTypes.AuthenticationError)
                : new MessageResponse($"Your account is locked for {string.Format("{0}:{1:00}s", (int)time.TotalMinutes, time.Seconds)} due to multiple failed login attempts.", MessageCode.AccountLockedOut, MessageTypes.AuthenticationError);
        }
        public static MessageResponse SuccessMessage(string? errorMessage = null) => new(errorMessage ?? "Request completed successfully", MessageCode.Success, MessageTypes.Success);
    }

}
