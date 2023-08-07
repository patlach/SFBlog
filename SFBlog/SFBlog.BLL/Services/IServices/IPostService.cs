using SFBlog.BLL.ViewModel;
using SFBlog.DAL.Models;

namespace SFBlog.BLL.Services.IServices
{
    public interface IPostService
    {
        Task<Post> GetPost(Guid id);
        Task<List<Post>> GetAllPosts();
        Task<PostCreateViewModel> CreatePost();
        Task<Guid> CreatePost(PostCreateViewModel model, string name);
        Task<PostEditViewModels> EditPost(Guid id);
        Task EditPost(PostEditViewModels model, Guid id);
        Task DeletePost(Guid id);
    }
}