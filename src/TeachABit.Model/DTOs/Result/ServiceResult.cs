using System.Text.Json.Serialization;
using TeachABit.Model.DTOs.Authentication;
using TeachABit.Model.DTOs.Result.Message;

namespace TeachABit.Model.DTOs.Result
{
    public class ServiceResult()
    {
        public MessageResponse? Message { get; set; }
        public bool IsError => Message != null && Message.Severity == MessageSeverities.Error;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public RefreshUserInfoDto? RefreshUserInfo { get; set; } = null;

        public static ServiceResult<T> Success<T>(T data, string? message = null) => new() { Data = data, Message = message == null ? null : MessageDescriber.SuccessMessage(message) };
        public static ServiceResult<T> Success<T>(T data, MessageResponse message) => new() { Data = data, Message = message };
        public static ServiceResult<T> Success<T>() => new() { Message = MessageDescriber.SuccessMessage() };
        public static ServiceResult<T> Success<T>(string message) => new() { Message = MessageDescriber.SuccessMessage(message) };
        public static ServiceResult<T> Success<T>(MessageResponse message) => new() { Message = message };
        public static ServiceResult Success(string message) => new() { Message = MessageDescriber.SuccessMessage(message) };
        public static ServiceResult Success(MessageResponse message) => new() { Message = message };
        public static ServiceResult Success() => new() { Message = MessageDescriber.SuccessMessage() };
        public static ServiceResult Success(RefreshUserInfoDto refresh) => new() { Message = MessageDescriber.SuccessMessage(), RefreshUserInfo = refresh };
        public static ServiceResult Failure(MessageResponse? messageResponse = null) => new() { Message = messageResponse ?? MessageDescriber.DefaultError() };
        public static ServiceResult<T?> NullableSuccess<T>(T? data) where T : class
        {
            return new() { Data = data };
        }
    }

    public class ServiceResult<T>
    {
        public bool Succeeded { get; set; }
        public MessageResponse? Message { get; set; }
        public T Data { get; set; } = default!;
        public bool IsError => Message != null && Message.Severity == MessageSeverities.Error;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public RefreshUserInfoDto? RefreshUserInfo { get; set; } = null;

        public static implicit operator ServiceResult<T>(ServiceResult result)
        {
            return new ServiceResult<T> { Succeeded = false, Message = result.Message, Data = default! };
        }

    }
}
