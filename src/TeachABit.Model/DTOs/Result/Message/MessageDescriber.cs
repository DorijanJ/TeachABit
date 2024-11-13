namespace TeachABit.Model.DTOs.Result.Message
{
    public class MessageDescriber
    {
        public static MessageResponse DefaultError() => new("Nešto je pošlo po krivom.", MessageSeverities.Error, MessageStatusCode.InternalServerError);
        public static MessageResponse DefaultError(string errorMessage) => new(errorMessage, MessageSeverities.Error, MessageStatusCode.InternalServerError);
        public static MessageResponse UserNotFound() => new("Korisnik nije pronađen.", MessageSeverities.Error, MessageStatusCode.NotFound);
        public static MessageResponse PasswordMismatch() => new("Pogrešna lozinka.", MessageSeverities.Error, MessageStatusCode.Unauthorized);
        public static MessageResponse DuplicateUsername(string username) => new($"Korisničko ime '{username}' je zauzeto.", MessageSeverities.Error, MessageStatusCode.Conflict);
        public static MessageResponse DuplicateEmail(string email) => new($"Račun sa mail adresom '{email}' već postoji.", MessageSeverities.Error, MessageStatusCode.Conflict);
        public static MessageResponse RegistrationError(string errorMessage) => new(errorMessage, MessageSeverities.Error, MessageStatusCode.BadRequest);
        public static MessageResponse Unauthenticated() => new("Unauthenticated.", MessageSeverities.Error, MessageStatusCode.Unauthorized);
        public static MessageResponse Unauthorized() => new("Unauthorized.", MessageSeverities.Error, MessageStatusCode.Forbidden);
        public static MessageResponse InvalidModelState(string errorMessage) => new(errorMessage, MessageSeverities.Error, MessageStatusCode.BadRequest);
        public static MessageResponse MethodNotAllowed() => new("Method not allowed.", MessageSeverities.Error, MessageStatusCode.MethodNotAllowed);
        public static MessageResponse BadRequest(string errorMessage) => new(errorMessage, MessageSeverities.Error, MessageStatusCode.BadRequest);
        public static MessageResponse MissingConfiguration() => new("Server trenutno ne može obaviti ovaj zahtjev.", MessageSeverities.Error, MessageStatusCode.InternalServerError);
        public static MessageResponse ItemNotFound() => new("Nije pronađeno.", MessageSeverities.Error, MessageStatusCode.NotFound);
        public static MessageResponse EmailConfimationSent() => new("Email sa poveznicom za potvrdu je poslan.", MessageSeverities.Info);
        public static MessageResponse EmailConfirmed() => new("Vaš email je potvrđen.", MessageSeverities.Success);
        public static MessageResponse EmailNotConfirmed() => new("Email nije povtrđen.", MessageSeverities.Error, MessageStatusCode.Unauthorized);
        public static MessageResponse UsernameNotProvided() => new("Korisničko ime ne smije biti prazno.", MessageSeverities.Error, MessageStatusCode.BadRequest, MessageCodes.UsernameNotProvided);

        public static MessageResponse AccountLockedOut(DateTimeOffset duration)
        {
            var time = duration - DateTimeOffset.UtcNow;
            return time.TotalSeconds <= 0
                ? new MessageResponse("Vaš račun je zaključan na kratko vrijeme zbog višestrukih neuspijelih pokušaja prijave.", MessageSeverities.Error, MessageStatusCode.Forbidden)
                : new MessageResponse($"Vaš račun je zaključan još {string.Format("{0}:{1:00}s", (int)time.TotalMinutes, time.Seconds)} zbog višestrukih neuspijelih pokušaja prijave.", MessageSeverities.Error, MessageStatusCode.Forbidden);
        }
        public static MessageResponse SuccessMessage(string? message = null) => new(message ?? "Request completed successfully", MessageSeverities.Success);
    }

    public static class MessageCodes
    {
        public static string UsernameNotProvided => "username_not_provided";
    }

}
