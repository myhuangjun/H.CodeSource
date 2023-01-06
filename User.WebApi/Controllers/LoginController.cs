using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Domain;
using User.Domain.Entities;
using User.Infrastracture;

namespace User.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        private readonly UserDomainService userService;

        public LoginController(UserDomainService userService)
        {
            this.userService = userService;
        }
        [UnitOfWork(typeof(UserDbContext)),HttpPost]
        public async Task<IActionResult> LoginByPhoneNumberPassword(PhoneNumber phoneNumber, string password)
        {
            if (password.Length < 3) return BadRequest("密码长度小于3");
            var result = await userService.CheckPassword(phoneNumber, password);
            switch (result)
            {
                case UserAccessResult.OK: return Ok("登录成功");
                case UserAccessResult.PhoneNumberNotFound:
                case UserAccessResult.NotPassword:
                case UserAccessResult.PasswordError: return BadRequest("登录失败");
                case UserAccessResult.LockOut: return BadRequest("账号已被锁定");
                default: throw new ApplicationException("未知值");
            }
        }
    }
}
