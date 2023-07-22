using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SFBlog.DAL.Models;

namespace SFBlog.DAL
{
    public class BlogContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Post> Posts { get; set; }
    
        public DbSet<Tag> Tags { get; set; }
    
        public DbSet<Comment> Comments { get; set; }
    
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}