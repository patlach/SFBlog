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
            var user2 = new UserRegisterViewModel {UserName = "User", Email = "ivan@sf.ru", Password = "12345", PasswordReg = "12345", FirstName = "Ivan", LastName = "Ivanov" };
            var user3 = new UserRegisterViewModel {UserName = "Moderator", Email = "fedor@sf.ru", Password = "12345", PasswordReg = "12345", FirstName = "Fedor", LastName = "Fedorov" };

            var userRole1 = new Role() { Name = "Admin", Description = "Admin user role" };
            var userRole2 = new Role() { Name = "User", Description = "Default user role" };
            var userRole3 = new Role() { Name = "Moderator", Description = "Moderator user role" };
            
            var mapUser1 = this.mapper.Map<User>(user1);
            var mapUser2 = this.mapper.Map<User>(user2);
            var mapUser3 = this.mapper.Map<User>(user3);

            await this.userManager.CreateAsync(mapUser1, user1.Password);
            await this.userManager.CreateAsync(mapUser2, user2.Password);
            await this.userManager.CreateAsync(mapUser3, user3.Password);

            await this.roleManager.CreateAsync(userRole1);
            await this.roleManager.CreateAsync(userRole2);
            await this.roleManager.CreateAsync(userRole3);
            
            await this.userManager.AddToRoleAsync(mapUser1, userRole1.Name);
            await this.userManager.AddToRoleAsync(mapUser2, userRole2.Name);
            await this.userManager.AddToRoleAsync(mapUser3, userRole3.Name);
        }
    }
}
