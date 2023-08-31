using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services;
using SFBlog.BLL.ViewModel;
using SFBlog.DAL.Models;
using System.Security.Authentication;
using System.Security.Claims;

namespace SFBlog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly UserManager<User> userManager;

        public UserController(IUserService userService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        /// <summary>
        /// Получение данных пользователя
        /// </summary>
        /// <param name="User Id"></param>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("GetUser")]
        public async Task<UserViewModel> GetUser(Guid id)
        {
            var result = await this.userService.GetUserById(id);

            return result;
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name=""></param>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterViewModel model)
        {
            var result = await this.userService.Register(model);

            return result.Succeeded ? StatusCode(201) : StatusCode(204);
        }

        /// <summary>
        /// Вход пользователя
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                throw new ArgumentNullException("Введены не корректные данные");
            }

            var result = await this.userService.Login(model);

            if (!result.Succeeded)
            {
                throw new AuthenticationException("Аккаунт не найден");
            }

            var user = await this.userManager.FindByEmailAsync(model.Email);
            var roles = await this.userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
            };

            if (roles.Contains("Admin"))
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin"));
            }
            else
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, roles.FirstOrDefault()));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            return StatusCode(200);
        }

        /// <summary>
        /// Выход пользователя
        /// </summary>
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            await this.userService.Logout();

            return StatusCode(200);
        }

        /// <summary>
        /// Получение данных пользователя для редактирования
        /// </summary>
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("Edit")]
        public async Task<UserEditViewModel> EditUser(Guid id)
        {
            var model = await this.userService.EditUser(id);

            return model;
        }

        /// <summary>
        /// Сохранение данных пользователя после редактирования
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiVersion("1.0")]
        [Route("Edit")]
        public async Task<IActionResult> EditUser(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.userService.EditUser(model, model.Id);

                return StatusCode(201);
            }
            else
            {
                return StatusCode(204);
            }
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="User id"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("Remove")]
        public async Task<IActionResult> RemoveUser(Guid id)
        {
            await this.userService.DeleteUser(id);

            return StatusCode(201);
        }

    }
}
