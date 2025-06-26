using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniAccountManagement.Web.Domain.DTOS;
using MiniAccountManagement.Web.Domain.Repositories;
using MiniAccountManagement.Web.Domain.Services;

namespace MiniAccountManagement.Web.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<bool> CreateUserAsync(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserWithRolesDto>> GetAllUsersAsync()
        {
            return _userRepository.GetAllUsersWithRolesAsync();
        }

        public Task<UserDto?> GetUserByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
