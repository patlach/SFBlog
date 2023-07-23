using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SFBlog.BLL.Services;
using SFBlog.BLL.ViewModel;

namespace SFBlog.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }


        [Route("Account/Get")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await this.userService.GetUsers();

            return View(users);
        }

        [Route("Account/Details")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var model = await this.userService.GetUser(id);

            return View(model);
        }

        [Route("Account/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Account/Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.userService.Login(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [Route("Account/Create")]
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [Route("Account/Create")]
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

        [Route("Account/Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Account/Register")]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await this.userService.Register(model);

                if (result.Succeeded)
                {
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

        [Route("Account/Edit")]
        [HttpGet]
        public async Task<IActionResult> EditUser(Guid id)
        {
            var model = await this.userService.EditUser(id);

            return View(model);
        }

        [Route("Account/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditUser(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                await this.userService.EditUser(model);

                return RedirectToAction("GetUsers", "User");
            }

            else
            {
                return View(model);
            }
        }

        [Route("Account/Remove")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var account = await this.userService.GetUser(id);

            await this.userService.DeleteUser(id);

            return RedirectToAction("GetUsers", "User");
        }


    }
}
