using SFBlog.DAL.Models;
using SFBlog.DAL.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.DAL.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogContext blogContext;

        public PostRepository(BlogContext blogContext)
        {
            this.blogContext = blogContext;
        }

        public Post GetPost(Guid id)
        {
            return this.blogContext.Posts.FirstOrDefault(x => x.Id == id);
        }

        public List<Post> GetAllPosts()
        {
            return this.blogContext.Posts.ToList();
        }

        public async Task AddPost(Post post)
        {
            this.blogContext.Posts.Add(post);
            await this.blogContext.SaveChangesAsync();
        }

        public async Task UpdatePost(Post post)
        {
            this.blogContext.Posts.Update(post);
            await this.blogContext.SaveChangesAsync();
        }

        public async Task DeletePost(Guid id)
        {
            var post = this.blogContext.Posts.FirstOrDefault(x => x.Id == id);
            if (post != null)
            {
                this.blogContext.Posts.Remove(post);
                await this.blogContext.SaveChangesAsync();
            }
        }

    }
}
