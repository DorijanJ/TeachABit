namespace TeachABit.Model.DTOs.Result.Message
{
    public class MessageDescriber
    {
        public static MessageResponse DefaultError() => new("Server error.", MessageTypes.GlobalError, MessageCode.DefaultError);
        public static MessageResponse DefaultError(string errorMessage) => new(errorMessage, MessageTypes.GlobalError, MessageCode.DefaultError);
        public static MessageResponse UserNotFound() => new("User not found.", MessageTypes.AuthenticationError, MessageCode.UserNotFound);
        public static MessageResponse PasswordMismatch() => new("Wrong password.", MessageTypes.AuthenticationError, MessageCode.PasswordMismatch);
        public static MessageResponse DuplicateUsername(string username) => new($"The username '{username}' is already taken.", MessageTypes.AuthenticationError, MessageCode.DuplicateUsername);
        public static MessageResponse DuplicateEmail(string email) => new($"The email '{email}' is already taken.", MessageTypes.AuthenticationError, MessageCode.DuplicateEmail);
        public static MessageResponse RegistrationError(string errorMessage) => new(errorMessage, MessageTypes.AuthenticationError, MessageCode.RegistrationError);
        public static MessageResponse Unauthenticated() => new("Unauthenticated.", MessageTypes.AuthenticationError, MessageCode.Unauthenticated);
        public static MessageResponse Unauthorized() => new("Unauthorized.", MessageTypes.AuthenticationError, MessageCode.Unauthorized);
        public static MessageResponse InvalidModelState(string errorMessage, MessageType messageType) => new(errorMessage, messageType, MessageCode.InvalidModelState);
        public static MessageResponse MethodNotAllowed() => new("Method not allowed.", MessageTypes.BadRequest, MessageCode.MethodNotAllowed);
        public static MessageResponse AccountLockedOut(DateTimeOffset duration)
        {
            var time = duration - DateTimeOffset.UtcNow;
            return time.TotalSeconds <= 0
                ? new MessageResponse("Your account is locked for a brief period due to multiple failed login attempts.", MessageTypes.AuthenticationError, MessageCode.AccountLockedOut)
                : new MessageResponse($"Your account is locked for {string.Format("{0}:{1:00}s", (int)time.TotalMinutes, time.Seconds)} due to multiple failed login attempts.", MessageTypes.AuthenticationError, MessageCode.AccountLockedOut);
        }
        public static MessageResponse SuccessMessage(string? errorMessage = null) => new(errorMessage ?? "Request completed successfully", MessageTypes.Success, MessageCode.Success);
    }

}
