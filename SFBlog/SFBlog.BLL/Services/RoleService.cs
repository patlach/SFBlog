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
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<Guid> CreateRole(RoleCreateViewModel model)
        {
            var role = new Role() { Name = model.Name, Description = model.Description };

            await this.roleManager.CreateAsync(role);

            return Guid.Parse(role.Id);
        }

        public async Task EditRole(RoleEditViewModel model)
        {
            if (string.IsNullOrEmpty(model.Name) && model.Description == null)

                return;

            var role = await this.roleManager.FindByIdAsync(model.Id.ToString());

            if (!string.IsNullOrEmpty(model.Name))

                role.Name = model.Name;

            if (model.Description != null)

                role.Description = model.Description;

            await this.roleManager.UpdateAsync(role);
        }

        public async Task RemoveRole(Guid id)
        {
            var role = await this.roleManager.FindByIdAsync(id.ToString());

            await this.roleManager.DeleteAsync(role);
        }

        public async Task<List<Role>> GetRoles()
        {
            return this.roleManager.Roles.ToList();
        }

        public async Task<Role?> GetRole(Guid id)
        {
            return await this.roleManager.FindByIdAsync(id.ToString());
        }
    }
}
