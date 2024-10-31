using TeachABit.Model.DTOs.Result.Message;

namespace TeachABit.Model.DTOs.Result
{
    public class ServiceResult
    {
        public MessageResponse? Message { get; set; }

        public bool IsError => Message != null && Message.MessageType.Severity == MessageSeverities.Error;
        public static ServiceResult Success() => new() { Message = MessageDescriber.SuccessMessage() };
        public static ServiceResult Success(string message) => new() { Message = MessageDescriber.SuccessMessage(message) };
        public static ServiceResult Failure(MessageResponse? messageResponse = null) => new() { Message = messageResponse ?? MessageDescriber.DefaultError() };
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        public static ServiceResult<T> Success(T data, string? message = null) => new() { Data = data, Message = message == null ? null : MessageDescriber.SuccessMessage(message) };
        new public static ServiceResult<T> Failure(MessageResponse? messageResponse = null) => new() { Message = messageResponse ?? MessageDescriber.DefaultError() };
    }
}
