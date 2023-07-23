using SFBlog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.DAL.Repository.Contract
{
    public interface ICommentRepository
    {
        Comment GetComment(Guid id);
        List<Comment> GetAllComments();
        Task AddComment(Comment comment);
        Task UpdateComment(Comment comment);
        Task DeleteComment(Guid id);
    }
}
