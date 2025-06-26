using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniAccountManagement.Web.Domain.DTOS;
using MiniAccountManagement.Web.Domain.Repositories;
using MiniAccountManagement.Web.Infrastructure.Identity;

namespace MiniAccountManagement.Web.Infrastructure.Repositories
{
    public class UserRepository :IUserRepository
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<bool> CreateUserAsync(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<UserWithRolesDto>> GetAllUsersWithRolesAsync()
        {
            var users = await _userManager.Users.ToListAsync();

            var list = new List<UserWithRolesDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                list.Add(new UserWithRolesDto
                {
                    UserId = user.Id,
                    UserName = user.UserName ?? "",
                    Email = user.Email ?? "",
                    Roles = roles.ToList()
                });
            }

            return list;
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
