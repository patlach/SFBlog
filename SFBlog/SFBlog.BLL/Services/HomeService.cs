using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SFBlog.BLL.Services.IServices;
using SFBlog.BLL.ViewModel;
using SFBlog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.BLL.Services
{
    public class HomeService : IHomeService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;
        public IMapper mapper;

        public HomeService(UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public async Task GenerateData()
        {
            var user1 = new UserRegisterViewModel {UserName = "Admin", Email = "egor@sf.ru", Password = "12345", PasswordReg = "12345", FirstName = "Egor", LastName = "Pol" };

            var userRole = new Role() { Name = "User", Description = "Default user role" };
            
            var mapUser = this.mapper.Map<User>(user1);

            await this.userManager.CreateAsync(mapUser, user1.Password);
            await this.roleManager.CreateAsync(userRole);
            await this.userManager.AddToRoleAsync(mapUser, userRole.Name);
        }
    }
}
