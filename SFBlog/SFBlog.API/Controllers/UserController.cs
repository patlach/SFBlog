using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services;
using SFBlog.BLL.ViewModel;
using SFBlog.DAL.Models;

namespace SFBlog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
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
            var result = await this.userService.Login(model);

            if (result.Succeeded)
            {
                return StatusCode(201);
            }
            else
            {
                return StatusCode(204);
            }
        }

        /// <summary>
        /// Выход пользователя
        /// </summary>
        [HttpGet]
        [ApiVersion("1.0")]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await this.userService.Logout();
            return StatusCode(201);
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
