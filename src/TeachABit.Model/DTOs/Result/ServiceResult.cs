using TeachABit.Model.DTOs.Result.Message;

namespace TeachABit.Model.DTOs.Result
{
    public class ServiceResult
    {
        public MessageResponse? Message { get; set; }

        public bool IsError => Message != null && Message.MessageType.Severity == MessageSeverities.Error;
        public static ServiceResult Success() => new() { };
        public static ServiceResult Success(MessageResponse messageResponse) => new() { Message = messageResponse };
        public static ServiceResult Failure(MessageResponse? messageResponse = null) => new() { Message = messageResponse ?? MessageDescriber.DefaultError() };
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        public static ServiceResult<T> Success(T data, MessageResponse? messageResponse = null) => new() { Data = data, Message = messageResponse };
        new public static ServiceResult<T> Failure(MessageResponse? messageResponse = null) => new() { Message = messageResponse ?? MessageDescriber.DefaultError() };
    }
}
