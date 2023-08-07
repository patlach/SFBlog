using SFBlog.DAL.Models;
using SFBlog.DAL.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.DAL.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogContext blogContext;

        public CommentRepository(BlogContext blogContext)
        {
            this.blogContext = blogContext;
        }

        public async Task AddComment(Comment comment)
        {
            this.blogContext.Comments.Add(comment);
            await this.blogContext.SaveChangesAsync();
        }

        public async Task DeleteComment(Guid id)
        {
            var comment = this.blogContext.Comments.FirstOrDefault(c => c.Id == id);
            if(comment != null)
            {
                this.blogContext.Comments.Remove(comment);
            }

            await this.blogContext.SaveChangesAsync();
        }

        public List<Comment> GetAllComments()
        {
            return this.blogContext.Comments.ToList();
        }

        public List<Comment> GetAllCommentByPostId(Guid id)
        {
            return this.blogContext.Comments.Where(x => x.Post.Id == id).ToList();
        }

        public Comment GetComment(Guid id)
        {
            return this.blogContext.Comments.FirstOrDefault(c => c.Id == id);
        }

        public async Task UpdateComment(Comment comment)
        {
            this.blogContext.Comments.Update(comment);
            await this.blogContext.SaveChangesAsync();
        }
    }
}
