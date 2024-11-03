namespace TeachABit.Model.DTOs.Result.Message
{
    public class MessageDescriber
    {
        public static MessageResponse DefaultError() => new("Server error.", MessageSeverities.Error, MessageStatusCode.InternalServerError);
        public static MessageResponse DefaultError(string errorMessage) => new(errorMessage, MessageSeverities.Error, MessageStatusCode.InternalServerError);
        public static MessageResponse UserNotFound() => new("User not found.", MessageSeverities.Error, MessageStatusCode.NotFound);
        public static MessageResponse PasswordMismatch() => new("Wrong password.", MessageSeverities.Error, MessageStatusCode.Unauthorized);
        public static MessageResponse DuplicateUsername(string username) => new($"The username '{username}' is already taken.", MessageSeverities.Error, MessageStatusCode.Conflict);
        public static MessageResponse DuplicateEmail(string email) => new($"The email '{email}' is already taken.", MessageSeverities.Error, MessageStatusCode.Conflict);
        public static MessageResponse RegistrationError(string errorMessage) => new(errorMessage, MessageSeverities.Error, MessageStatusCode.BadRequest);
        public static MessageResponse Unauthenticated() => new("Unauthenticated.", MessageSeverities.Error, MessageStatusCode.Unauthorized);
        public static MessageResponse Unauthorized() => new("Unauthorized.", MessageSeverities.Error, MessageStatusCode.Forbidden);
        public static MessageResponse InvalidModelState(string errorMessage) => new(errorMessage, MessageSeverities.Error, MessageStatusCode.BadRequest);
        public static MessageResponse MethodNotAllowed() => new("Method not allowed.", MessageSeverities.Error, MessageStatusCode.MethodNotAllowed);
        public static MessageResponse BadRequest(string errorMessage) => new(errorMessage, MessageSeverities.Error, MessageStatusCode.BadRequest);
        public static MessageResponse MissingConfiguration() => new("The server currently can't complete this operation.", MessageSeverities.Error, MessageStatusCode.InternalServerError);
        public static MessageResponse ItemNotFound() => new("Item not found.", MessageSeverities.Error, MessageStatusCode.NotFound);
        public static MessageResponse EmailConfimationSent() => new("An email with a confirmation link has been sent.", MessageSeverities.Info);
        public static MessageResponse EmailConfirmed() => new("Your email has been confirmed", MessageSeverities.Info);
        public static MessageResponse EmailNotConfirmed() => new("Email has not been confirmed", MessageSeverities.Error, MessageStatusCode.Unauthorized);
        public static MessageResponse UsernameNotProvided() => new("Username can't be empty.", MessageSeverities.Error, MessageStatusCode.BadRequest, MessageCodes.UsernameNotProvided);

        public static MessageResponse AccountLockedOut(DateTimeOffset duration)
        {
            var time = duration - DateTimeOffset.UtcNow;
            return time.TotalSeconds <= 0
                ? new MessageResponse("Your account is locked for a brief period due to multiple failed login attempts.", MessageSeverities.Error, MessageStatusCode.Forbidden)
                : new MessageResponse($"Your account is locked for {string.Format("{0}:{1:00}s", (int)time.TotalMinutes, time.Seconds)} due to multiple failed login attempts.", MessageSeverities.Error, MessageStatusCode.Forbidden);
        }
        public static MessageResponse SuccessMessage(string? message = null) => new(message ?? "Request completed successfully", MessageSeverities.Success);
    }

    public static class MessageCodes
    {
        public static string UsernameNotProvided => "username_not_provided";
    }

}
