using System.Text.Json.Serialization;

namespace TeachABit.Model.DTOs.Result.Message
{
    public class MessageResponse(string message, string severity, MessageStatusCode? messageStatusCode = null, string? code = null)
    {
        public string Message { get; } = message;
        public string? Code { get; } = code;
        public string Severity { get; } = severity;
        [JsonIgnore]
        public MessageStatusCode? MessageStatusCode { get; } = messageStatusCode;
    }

    public static class MessageSeverities
    {
        public static readonly string Warning = "warning";
        public static readonly string Error = "error";
        public static readonly string Success = "success";
        public static readonly string Info = "info";
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
