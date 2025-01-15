using System.Text.Json.Serialization;

namespace TeachABit.Model.DTOs.Result.Message
{
    public class MessageResponse(string message, string severity, string type = "global", string? code = null, MessageStatusCode? messageStatusCode = null)
    {
        public string Message { get; } = message;
        public string Severity { get; } = severity;
        public string Type { get; } = type;
        public string? Code { get; } = code;
        [JsonIgnore]
        public MessageStatusCode? MessageStatusCode { get; } = messageStatusCode;
    }

    public static class MessageSeverities
    {
        public const string Warning = "warning";
        public const string Error = "error";
        public const string Success = "success";
        public const string Info = "info";
    }

    // Ovo sluzi kako bi frontend znao gdje prikazati koju poruku
    public class MessageTypes
    {
        // Prikažu globalnu obavijest
        public const string Global = "global";
        // Ne prikažu globalnu obavijest
        public const string Hidden = "hidden";
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
