using SFBlog.BLL.ViewModel;
using SFBlog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.BLL.Services.IServices
{
    public interface ICommentService
    {
        Task<Comment> GetComment(Guid id);
        Task<List<Comment>> GetComments();
        Task<Guid> CreateComment(CommentCreateViewModel model, Guid userId);
        Task<CommentEditViewModel> EditComment(Guid id);
        Task EditComment(CommentEditViewModel model, Guid id);
        Task DeleteComment(Guid id);
    }
}
