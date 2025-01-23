using TeachABit.Model.DTOs.Uloge;

namespace TeachABit.Model.DTOs.Authentication
{
    public class RefreshUserInfoDto
    {
        public string? Id { get; set; } = string.Empty;
        public string? UserName { get; set; } = string.Empty;
        public List<UlogaDto> Roles { get; set; } = [];
        public bool IsAuthenticated { get; set; }
    }
}
