using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Uloge;

namespace TeachABit.Service.Services.Uloge
{
    public interface IUlogeService
    {
        public Task<ServiceResult> AddUserToUloga(string userName, string roleName);
        public Task<ServiceResult<List<UlogaDto>>> GetAllUloge();
    }
}
