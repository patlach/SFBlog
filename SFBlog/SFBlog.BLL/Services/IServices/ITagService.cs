using SFBlog.BLL.ViewModel;
using SFBlog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.BLL.Services.IServices
{
    public interface ITagService
    {
        Task<Tag> GetTag(Guid id);
        Task<List<Tag>> GetAllTags();
        Task<Guid> CreateTag(TagCreateViewModel model);
        Task EditTag(TagEditViewModel model, Guid id);
        Task<TagEditViewModel> EditTag(Guid id);
        Task DeleteTag(Guid Id);
    }
}
