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
            return _userRepository.CreateUserAsync(userDto);
        }

        public Task<bool> DeleteUserAsync(string userId)
        {
            return _userRepository.DeleteUserAsync(userId);
        }

        public Task<List<UserWithRolesDto>> GetAllUsersAsync()
        {
            return _userRepository.GetAllUsersWithRolesAsync();
        }

        public Task<UserDto?> GetUserByIdAsync(string userId)
        {
            return _userRepository.GetUserByIdAsync(userId);
        }

        public Task<bool> UpdateUserAsync(UserDto userDto)
        {
            return _userRepository.UpdateUserAsync(userDto);
        }

    }
}
