using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
    public class CommentService : ICommentService
    {
        private ICommentRepository commentRepository;
        private IPostRepository postRepository;
        private UserManager<User> userManager;
        private IMapper mapper;

        public CommentService(ICommentRepository commentRepository, IPostRepository postRepository, UserManager<User> userManager, IMapper mapper)
        {
            this.commentRepository = commentRepository;
            this.postRepository = postRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<Comment> GetComment(Guid id)
        {
            return this.commentRepository.GetComment(id);
        }

        public async Task<List<Comment>> GetComments()
        {
            return this.commentRepository.GetAllComments().ToList();
        }

        public async Task<Guid> CreateComment(CommentCreateViewModel model, Guid userId)
        {
            var post = this.postRepository.GetPost(model.PostId);
            var user = await this.userManager.FindByIdAsync(model.AuthorId.ToString());

            var comment = new Comment
            {
                Id = Guid.NewGuid(),
                Text = model.Text,
                Post = post,
                User = user
            };

            await this.commentRepository.AddComment(comment);

            return comment.Id;
        }

        public async Task<CommentEditViewModel> EditComment(Guid id)
        {
            var comment = this.commentRepository.GetComment(id);

            var result = new CommentEditViewModel
            {
                Text = comment.Text,
            };

            return result;
        }

        public async Task EditComment(CommentEditViewModel model, Guid id)
        {
            var comment = this.commentRepository.GetComment(id);

            comment.Text = model.Text;

            await this.commentRepository.UpdateComment(comment);
        }

        public async Task DeleteComment(Guid id)
        {
            await this.commentRepository.DeleteComment(id);
        }
    }
}
