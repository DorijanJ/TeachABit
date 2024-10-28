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
