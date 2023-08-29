using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services;
using SFBlog.BLL.Services.IServices;
using SFBlog.BLL.ViewModel;
using NLog;

namespace SFBlog.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly ILogger<UserController> logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            this.logger = logger;
        }


        [Route("User/Get")]
        //[Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await this.userService.GetUsers();

            return View(users);
        }

        [Route("User/Details")]
        //[Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var model = await this.userService.GetUser(id);

            return View(model);
        }

        [Route("User/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("User/Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.userService.Login(model);

                if (result.Succeeded)
                {
                    this.logger.LogInformation($"Вход пользователя {model.Email}");
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await this.userService.Logout();
            return RedirectToAction("Index", "Home");
        }

        [Route("User/Create")]
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [Route("User/Create")]
        [HttpPost]
        public async Task<IActionResult> AddUser(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.userService.CreateUser(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("GetUsers", "User");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [Route("User/Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Route("User/Register")]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.userService.Register(model);

                if (result.Succeeded)
                {
                    this.logger.LogInformation($"Пользователь {model.UserName} был зарегистрирован");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [Route("User/Edit")]
        [HttpGet]
        public async Task<IActionResult> EditUser(Guid id)
        {
            var model = await this.userService.EditUser(id);

            return View(model);
        }

        [Route("User/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditUser(UserEditViewModel model, Guid id)
        {
            if (ModelState.IsValid)
            {
                await this.userService.EditUser(model, id);

                this.logger.LogInformation($"Пользователь {model.UserName}, {id} был отредактироавн");

                return RedirectToAction("GetUsers", "User");
            }

            else
            {
                return View(model);
            }
        }

        [Route("User/Remove")]
        [HttpGet]
        public async Task<IActionResult> RemoveUser(Guid id)
        {
            //var account = await this.userService.GetUser(id);

            await this.userService.DeleteUser(id);

            this.logger.LogInformation($"Пользователь с {id} был удален");

            return RedirectToAction("GetUsers", "User");
        }
    }
}
