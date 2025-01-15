namespace TeachABit.Model.DTOs.Result.Message
{
    public class MessageDescriber
    {
        #region Authentication
        public static MessageResponse UserNotFound() => new("Korisnik nije pronađen.", MessageSeverities.Error, MessageTypes.Hidden, messageStatusCode: MessageStatusCode.NotFound);
        public static MessageResponse PasswordMismatch() => new("Pogrešna lozinka.", MessageSeverities.Error, MessageTypes.Hidden, messageStatusCode: MessageStatusCode.Unauthorized);
        public static MessageResponse DuplicateUsername(string username) => new($"Korisničko ime '{username}' je zauzeto.", MessageSeverities.Error, MessageTypes.Hidden, messageStatusCode: MessageStatusCode.Conflict);
        public static MessageResponse DuplicateEmail(string email) => new($"Račun sa mail adresom '{email}' već postoji.", MessageSeverities.Error, MessageTypes.Hidden, messageStatusCode: MessageStatusCode.Conflict);
        public static MessageResponse RegistrationError(string errorMessage) => new(errorMessage, MessageSeverities.Error, MessageTypes.Hidden, messageStatusCode: MessageStatusCode.BadRequest);
        public static MessageResponse EmailConfimationSent() => new("Email sa poveznicom za potvrdu je poslan.", MessageTypes.Hidden, MessageSeverities.Info);
        public static MessageResponse EmailConfirmed() => new("Vaš email je potvrđen.", MessageTypes.Hidden, MessageSeverities.Success);
        public static MessageResponse EmailNotConfirmed() => new("Email nije povtrđen.", MessageSeverities.Error, MessageTypes.Hidden, messageStatusCode: MessageStatusCode.Unauthorized);
        public static MessageResponse UsernameNotProvided() => new("Korisničko ime ne smije biti prazno.", MessageSeverities.Error, MessageTypes.Hidden, messageStatusCode: MessageStatusCode.BadRequest, code: MessageCodes.UsernameNotProvided);
        public static MessageResponse AccountLockedOut(DateTimeOffset duration)
        {
            var time = duration - DateTimeOffset.UtcNow;
            return time.TotalSeconds <= 0
                ? new MessageResponse("Vaš račun je zaključan na kratko vrijeme zbog višestrukih neuspijelih pokušaja prijave.", MessageSeverities.Error, MessageTypes.Hidden, messageStatusCode: MessageStatusCode.Forbidden)
                : new MessageResponse($"Vaš račun je zaključan još {string.Format("{0}:{1:00}s", (int)time.TotalMinutes, time.Seconds)} zbog višestrukih neuspijelih pokušaja prijave.", MessageSeverities.Error, MessageTypes.Hidden, messageStatusCode: MessageStatusCode.Forbidden);
        }
        #endregion

        public static MessageResponse DefaultError() => new("Nešto je pošlo po krivom.", MessageSeverities.Error, messageStatusCode: MessageStatusCode.InternalServerError);
        public static MessageResponse DefaultError(string errorMessage) => new(errorMessage, MessageSeverities.Error, messageStatusCode: MessageStatusCode.InternalServerError);
        public static MessageResponse Unauthenticated() => new("Niste prijavljeni.", MessageSeverities.Error, messageStatusCode: MessageStatusCode.Unauthorized);
        public static MessageResponse Unauthorized() => new("Nemate prava za tu akciju.", MessageSeverities.Error, messageStatusCode: MessageStatusCode.Forbidden);
        public static MessageResponse InvalidModelState(string errorMessage, string type = MessageTypes.Global) => new(errorMessage, MessageSeverities.Error, type, messageStatusCode: MessageStatusCode.BadRequest);
        public static MessageResponse MethodNotAllowed() => new("Method not allowed.", MessageSeverities.Error, messageStatusCode: MessageStatusCode.MethodNotAllowed);
        public static MessageResponse BadRequest(string errorMessage) => new(errorMessage, MessageSeverities.Error, messageStatusCode: MessageStatusCode.BadRequest);
        public static MessageResponse MissingConfiguration() => new("Server trenutno ne može obaviti ovaj zahtjev.", MessageSeverities.Error, messageStatusCode: MessageStatusCode.InternalServerError);
        public static MessageResponse ItemNotFound() => new("Nije pronađeno.", MessageSeverities.Error, messageStatusCode: MessageStatusCode.NotFound);
        public static MessageResponse SuccessMessage(string? message = null) => new(message ?? "Uspješno.", MessageSeverities.Success);
    }

    public static class MessageCodes
    {
        public static string UsernameNotProvided => "username_not_provided";
    }

}
