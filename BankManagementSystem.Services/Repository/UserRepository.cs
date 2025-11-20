using BankManagementSystem.Entity.Dto;
using BankManagementSystem.Entity.Responses;
using BankManagementSystem.Entity.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Services.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        public async Task<Result<UserResponse>> Authenticate(UserRequest request)
        {
            Result<UserResponse> response = new();

            var user = await _userManager.FindByNameAsync(request.UserName);
            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (result)
            {
                response.Response = new UserResponse
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
            }
            else
            {
                response.Errors.Add(new Errors { ErrorCode = "101", ErrorMessage = "Invalid Credential" });
            }


            return response;

        }

        public async Task<bool> IsAValidUser(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            return await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
