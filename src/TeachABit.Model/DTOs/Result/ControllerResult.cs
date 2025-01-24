using System.Text.Json.Serialization;
using TeachABit.Model.DTOs.Authentication;
using TeachABit.Model.DTOs.Result.Message;

namespace TeachABit.Model.DTOs.Result
{
    public class ControllerResult
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Data { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MessageResponse? Message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public RefreshUserInfoDto? RefreshUserInfo { get; set; } = null;
    }
}
