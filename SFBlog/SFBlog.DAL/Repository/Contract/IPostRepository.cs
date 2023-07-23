using SFBlog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.DAL.Repository.Contract
{
    public interface IPostRepository
    {
        Post GetPost(Guid id);
        List<Post> GetAllPosts();
        Task AddPost(Post post);
        Task UpdatePost(Post post);
        Task DeletePost(Guid id);
    }
}
