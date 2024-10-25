namespace TeachABit.Model.DTOs.Result.Message
{
    public record MessageResponse(string Message, MessageCode MessageCode, MessageType MessageType);

    public record MessageType(string Type, string Severity);

    public static class MessageTypes
    {
        public static readonly MessageType AuthenticationError = new("authentication_error", MessageSeverities.Error);
        public static readonly MessageType GlobalError = new("global_error", MessageSeverities.Error);
        public static readonly MessageType BadRequest = new("request_error", MessageSeverities.Error);
        public static readonly MessageType Success = new("succcess_response", MessageSeverities.Success);
    }

    public static class MessageSeverities
    {
        public static readonly string Warning = "Warning";
        public static readonly string Error = "Error";
        public static readonly string Success = "Success";
    }

    public enum MessageCode
    {
        DefaultError,
        UserNotFound,
        PasswordMismatch,
        DuplicateUsername,
        DuplicateEmail,
        RegistrationError,
        Unauthenticated,
        Unauthorized,
        InvalidModelState,
        MethodNotAllowed,
        AccountLockedOut,
        Success
    }
}
