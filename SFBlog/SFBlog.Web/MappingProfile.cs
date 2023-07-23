using AutoMapper;
using SFBlog.BLL.ViewModel;
using SFBlog.DAL.Models;

namespace SFBlog.Web
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegisterViewModel, User>()
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.Email))
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.UserName));
            CreateMap<TagCreateViewModel, Tag>();
            CreateMap<TagEditViewModel, Tag>();
            CreateMap<CommentEditViewModel, Comment>();
            CreateMap<CommentCreateViewModel, Comment>();
            CreateMap<PostCreateViewModel, Post>();
            CreateMap<PostEditViewModels, Post>();
            CreateMap<UserEditViewModel, User>();
        }
    }
}
