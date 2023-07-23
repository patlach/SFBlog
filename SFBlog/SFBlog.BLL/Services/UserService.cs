using AutoMapper;
using SFBlog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SFBlog.BLL.ViewModel;

namespace SFBlog.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;


        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> singInManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = singInManager;
            this.mapper = mapper;
        }

        public async Task<User> GetUser(Guid id)
        {
            return await this.userManager.FindByIdAsync(id.ToString());
        }

        public async Task<List<User>> GetUsers()
        {
            return this.userManager.Users.ToList();
        }

        public async Task<IdentityResult> Register(UserRegisterViewModel model)
        {
            var user = this.mapper.Map<User>(model);

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await this.signInManager.SignInAsync(user, false);

                var userRole = new Role() { Name = "User", Description = "User Default Role" };

                await this.roleManager.CreateAsync(userRole);

                var currentUser = await this.userManager.FindByIdAsync(user.Id.ToString());

                await this.userManager.AddToRoleAsync(currentUser, userRole.Name);

                return result;
            }
            else
            {
                return result;
            }
        }

        public async Task<SignInResult> Login(UserLoginViewModel model)
        {
            var user = await this.userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return SignInResult.Failed;
            }

            var result = await this.signInManager.PasswordSignInAsync(user, model.Password, true, false);

            return result;
        }

        public async Task Logout()
        {
            await this.signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> CreateUser(UserCreateViewModel model)
        {
            var user = new User();

            if (model.FirstName != null)
            {
                user.FirstName = model.FirstName;
            }
            if (model.LastName != null)
            {
                user.LastName = model.LastName;
            }
            if (model.Email != null)
            {
                user.Email = model.Email;
            }
            if (model.UserName != null)
            {
                user.UserName = model.UserName;
            }

            var roleUser = new Role() { Name = "User", Description = "Default user role" };

            var result = await this.userManager.CreateAsync(user, model.Password);

            await this.userManager.AddToRoleAsync(user, roleUser.Name);

            return result;
        }

        public async Task<UserEditViewModel> EditUser(Guid id)
        {
            var user = await this.userManager.FindByIdAsync(id.ToString());

            var roles = this.roleManager.Roles.ToList();

            var model = new UserEditViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                NewPassword = string.Empty,
                Id = id,
                Roles = roles.Select(r => new RoleViewModel() { Id = r.Id, Name = r.Name }).ToList(),
            };

            return model;
        }

        public async Task<IdentityResult> EditUser(UserEditViewModel model)
        {
            var user = await this.userManager.FindByIdAsync(model.Id.ToString());

            if (model.FirstName != null)
            {
                user.FirstName = model.FirstName;
            }
            if (model.LastName != null)
            {
                user.LastName = model.LastName;
            }
            if (model.Email != null)
            {
                user.Email = model.Email;
            }
            if (model.NewPassword != null)
            {
                user.PasswordHash = this.userManager.PasswordHasher.HashPassword(user, model.NewPassword);
            }
            if (model.UserName != null)
            {
                user.UserName = model.UserName;
            }

            foreach (var role in model.Roles)
            {
                var roleName = this.roleManager.FindByIdAsync(role.Id.ToString()).Result.Name;

                if (role.IsSelected)
                {
                    await this.userManager.AddToRoleAsync(user, roleName);
                }
                else
                {
                    await this.userManager.RemoveFromRoleAsync(user, roleName);
                }
            }

            var result = await this.userManager.UpdateAsync(user);

            return result;
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await this.userManager.FindByIdAsync(id.ToString());

            await this.userManager.DeleteAsync(user);
        }
    }
}
