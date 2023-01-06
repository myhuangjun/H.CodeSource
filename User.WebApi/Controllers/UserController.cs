using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Domain;
using User.Domain.Entities;
using User.Infrastracture;

namespace User.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        [UnitOfWork(typeof(UserDbContext)),HttpPost]
        public async Task<IActionResult> AddUser(PhoneNumber phoneNumber)
        {
            if (await userRepository.FindOneAsync(phoneNumber) != null) return BadRequest("用户已存在");
            await userRepository.AddUser(phoneNumber); ;
            return Ok("新增成功");
        }
    }
}
