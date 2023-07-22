using SFBlog.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.DAL.Repository.Contract
{
    public interface ITagRepository
    {
        Tag GetTag(Guid Id);
        List<Tag> GetAllTags();
        Task AddTag(Tag tag);
        Task UpdateTag(Tag tag);
        Task DeleteTag(Guid id);
    }
}
