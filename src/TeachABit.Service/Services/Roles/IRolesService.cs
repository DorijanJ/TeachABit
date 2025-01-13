using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachABit.Model.DTOs.Result;

namespace TeachABit.Service.Services.Uloge
{
    public interface IRolesService
    {
        public Task<ServiceResult> AddUserToRole(string userName, string roleName);
        public Task<ServiceResult> RemoveUserFromRole(string userName, string roleName);
        public Task<ServiceResult<List<string?>>> GetAllRoles();
    }
}
