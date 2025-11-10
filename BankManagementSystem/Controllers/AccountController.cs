using BankManagementSystem.Entity.Dto;
using BankManagementSystem.Services.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public AccountController(IUserRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(UserRequest model)
        {
            var result = await _repository.Authenticate(model);
            return Ok(result);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRequest model)
        {
            var result = await _repository.Register(model);
            return Ok(result);
        }
    }
}
