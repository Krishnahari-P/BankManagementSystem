using BankManagementSystem.Entity.Dto;
using BankManagementSystem.Entity.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Services.Repository
{
    public interface IUserRepository
    {
        Task<Result<UserResponse>> Authenticate(UserRequest request);
        Task<Result<UserResponse>> Register(UserRequest request);
        Task<bool> IsAValidUser(string username, string password);
    }
}
