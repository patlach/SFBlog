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
    public class PostService : IPostService
    {
        private readonly IPostRepository postRepository;
        private readonly ITagRepository tagRepositroy;
        private readonly ICommentRepository commentRepository;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public PostService(IPostRepository postRepositroy, ITagRepository tagRepositroy, ICommentRepository commentRepository, UserManager<User> userManager, IMapper mapper)
        {
            this.postRepository = postRepositroy;
            this.tagRepositroy = tagRepositroy;
            this.commentRepository = commentRepository;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<Post> GetPost(Guid id)
        {
            return this.postRepository.GetPost(id);
        } 

        public async Task<List<Post>> GetAllPosts()
        {
            return this.postRepository.GetAllPosts();
        }

        public async Task<PostCreateViewModel> CreatePost()
        {
            var post = new Post();

            var allTags = this.tagRepositroy.GetAllTags().Select(t => new TagViewModel() { Id = t.Id, Name = t.Name }).ToList();

            var model = new PostCreateViewModel
            {
                Title = post.Title = string.Empty,
                Text = post.Text = string.Empty,
                Tags = allTags
            };

            return model;
        }

        public async Task<Guid> CreatePost(PostCreateViewModel model, string name)
        {
            var user = await this.userManager.FindByNameAsync(model.Author);
            var tags = new List<Tag>();

            if(model.Tags != null)
            {
                var postTags = model.Tags.Where(t => t.IsSelected == true).Select(t => t.Name).ToList();
                tags = this.tagRepositroy.GetAllTags().Where(t => postTags.Contains(t.Name)).ToList();
            }

            var post = new Post
            {
                Id = Guid.NewGuid(),
                Title = model.Title,
                Text = model.Text,
                Tags = tags,
                Author = new Guid(user.Id),
                User = user
            };

            await this.postRepository.AddPost(post);

            user.Posts.Add(post);
            
            await this.userManager.UpdateAsync(user);

            return post.Id;
        }

        public async Task<PostEditViewModels> EditPost(Guid id)
        {
            var post = this.postRepository.GetPost(id);

            var tags = this.tagRepositroy.GetAllTags().Select(t => new TagViewModel() { Id = t.Id, Name = t.Name }).ToList();

            foreach (var tag in tags)
            {
                if(post.Tags != null)
                {
                    foreach (var postTag in post.Tags)
                    {
                        if (postTag.Id == tag.Id)
                        {
                            tag.IsSelected = true;
                            break;
                        }
                    }
                }
            }

            var model = new PostEditViewModels()
            {
                Id = id,
                Title = post.Title,
                Text = post.Text,
                Tags = tags
            };

            return model;
        }

        public async Task EditPost(PostEditViewModels model, Guid id)
        {
            var post = this.postRepository.GetPost(id);

            foreach(var modelTag in model.Tags)
            {
                var tag = this.tagRepositroy.GetTag(modelTag.Id);
                if(modelTag.IsSelected)
                {
                    post.Tags.Add(tag);
                }
                else
                {
                    post.Tags.Remove(tag);
                }
            }

            post.Title = model.Title;
            post.Text = model.Text;

            await this.postRepository.UpdatePost(post);
        }

        public async Task DeletePost(Guid id)
        {
            await this.postRepository.DeletePost(id);
        }

  
    }
}
