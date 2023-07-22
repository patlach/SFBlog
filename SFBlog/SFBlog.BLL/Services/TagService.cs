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
        private readonly ITagRepository _repo;
        private readonly IMapper _mapper;

        public TagService(ITagRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Tag> GetTag(Guid id)
        {
            return _repo.GetTag(id);
        }

        public async Task<List<Tag>> GetAllTags()
        {
            return _repo.GetAllTags();
        }

        public async Task<Guid> CreateTag(TagCreateViewModel model)
        {
            var tag = _mapper.Map<Tag>(model);
            await _repo.AddTag(tag);

            return tag.Id;
        }

        public async Task<TagEditViewModel> EditTag(Guid id)
        {
            var tag = _repo.GetTag(id);
            var result = new TagEditViewModel()
            {
                Name = tag.Name
            };

            return result;
        }

        public async Task EditTag(TagEditViewModel model, Guid id)
        {
            var tag = _repo.GetTag(id);
            tag.Name = model.Name;
            await _repo.UpdateTag(tag);
        }

        public async Task DeleteTag(Guid Id)
        {
            await _repo.DeleteTag(Id);
        }
    }
}
