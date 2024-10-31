using System.Text.Json.Serialization;

namespace TeachABit.Model.DTOs.Result.Message
{
    public class MessageResponse(string message, MessageType messageType, MessageStatusCode? messageStatusCode = null)
    {
        public string Message { get; } = message;
        public MessageType MessageType { get; } = messageType;
        [JsonIgnore]
        public MessageStatusCode? MessageStatusCode { get; } = messageStatusCode;
    }

    public record MessageType(string Type, string Severity);

    public static class MessageTypeDescriber
    {
        public static readonly MessageType AuthenticationError = new(MessageTypes.Authentication, MessageSeverities.Error);
        public static readonly MessageType AuthenticationInfo = new(MessageTypes.Authentication, MessageSeverities.Info);
        public static readonly MessageType GlobalError = new(MessageTypes.Global, MessageSeverities.Error);
        public static readonly MessageType Success = new(MessageTypes.Hidden, MessageSeverities.Success);

        // Ovakve poruke se handlaju na specificnim mjestima na frontendu
        public static readonly MessageType SpecificInfo = new(MessageTypes.Hidden, MessageSeverities.Info);
    }

    public static class MessageSeverities
    {
        public static readonly string Warning = "Warning";
        public static readonly string Error = "Error";
        public static readonly string Success = "Success";
        public static readonly string Info = "Info";
    }

    // Ovo sluzi kako bi frontend znao gdje prikazati koju poruku
    public static class MessageTypes
    {
        public static readonly string Authentication = "authentication";
        public static readonly string Global = "global";
        public static readonly string Hidden = "hidden";
    }

    public enum MessageStatusCode
    {
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        Conflict = 409,
        InternalServerError = 500
    }
}
