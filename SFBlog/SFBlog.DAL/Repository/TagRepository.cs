using SFBlog.DAL.Models;
using SFBlog.DAL.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.DAL.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogContext _context;

        public TagRepository(BlogContext context)
        {
            _context = context;
        }

        public Tag GetTag(Guid id)
        {
            return _context.Tags.FirstOrDefault(t => t.Id == id);
        }

        public List<Tag> GetAllTags()
        {
            return _context.Tags.ToList();
        }

        public async Task AddTag(Tag tag)
        {
            _context.Tags.Add(tag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTag(Guid id)
        {
            var tag = _context.Tags.FirstOrDefault(t => t.Id == id);
            
            if (tag != null)
            {
                _context.Remove(tag);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateTag(Tag tag)
        {
            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();
        }
    }
}
