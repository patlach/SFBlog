using AutoMapper;
using SFBlog.BLL.Services.IServices;
using SFBlog.BLL.ViewModel;
using SFBlog.DAL.Models;
using SFBlog.DAL.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.BLL.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;
        private readonly IMapper mapper;

        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            this.tagRepository = tagRepository;
            this.mapper = mapper;
        }

        public async Task<Tag> GetTag(Guid id)
        {
            return this.tagRepository.GetTag(id);
        }

        public async Task<List<Tag>> GetAllTags()
        {
            return this.tagRepository.GetAllTags();
        }

        public async Task<Guid> CreateTag(TagCreateViewModel model)
        {
            var tag = this.mapper.Map<Tag>(model);
            await this.tagRepository.AddTag(tag);

            return tag.Id;
        }

        public async Task<TagEditViewModel> EditTag(Guid id)
        {
            var tag = this.tagRepository.GetTag(id);
            var result = new TagEditViewModel()
            {
                Name = tag.Name
            };

            return result;
        }

        public async Task EditTag(TagEditViewModel model, Guid id)
        {
            var tag = this.tagRepository.GetTag(id);
            tag.Name = model.Name;
            await this.tagRepository.UpdateTag(tag);
        }

        public async Task DeleteTag(Guid Id)
        {
            await this.tagRepository.DeleteTag(Id);
        }
    }
}
