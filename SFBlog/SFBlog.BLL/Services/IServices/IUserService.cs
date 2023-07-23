using Microsoft.AspNetCore.Identity;
using SFBlog.BLL.ViewModel;
using SFBlog.DAL.Models;

namespace SFBlog.BLL.Services
{
    public interface IUserService
    {
        Task<User> GetUser(Guid id);
        Task<List<User>> GetUsers();
        Task<IdentityResult> Register(UserRegisterViewModel model);
        Task<SignInResult> Login(UserLoginViewModel model);
        Task Logout();
        Task<IdentityResult> CreateUser(UserCreateViewModel model);
        Task<UserEditViewModel> EditUser(Guid id);
        Task<IdentityResult> EditUser(UserEditViewModel model);
        Task DeleteUser(Guid id);
    }
}