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
        private readonly BlogContext context;

        public TagRepository(BlogContext context)
        {
            this.context = context;
        }

        public Tag GetTag(Guid id)
        {
            return this.context.Tags.FirstOrDefault(t => t.Id == id);
        }

        public List<Tag> GetAllTags()
        {
            return this.context.Tags.ToList();
        }

        public async Task AddTag(Tag tag)
        {
            this.context.Tags.Add(tag);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteTag(Guid id)
        {
            var tag = this.context.Tags.FirstOrDefault(t => t.Id == id);
            
            if (tag != null)
            {
                this.context.Remove(tag);
            }

            await this.context.SaveChangesAsync();
        }

        public async Task UpdateTag(Tag tag)
        {
            this.context.Tags.Update(tag);
            await this.context.SaveChangesAsync();
        }
    }
}
