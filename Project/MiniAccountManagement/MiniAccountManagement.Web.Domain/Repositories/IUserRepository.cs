using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.DTOS;
namespace MiniAccountManagement.Web.Domain.Repositories
{
    public interface IUserRepository
    {

        Task<List<UserWithRolesDto>> GetAllUsersWithRolesAsync();
        Task<UserDto?> GetUserByIdAsync(string userId);
        Task<bool> CreateUserAsync(UserDto userDto);
        Task<bool> UpdateUserAsync(UserDto userDto);
        Task<bool> DeleteUserAsync(string userId);
    }
}
