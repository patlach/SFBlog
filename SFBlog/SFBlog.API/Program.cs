using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SFBlog.API;
using SFBlog.BLL.Services;
using SFBlog.BLL.Services.IServices;
using SFBlog.DAL;
using SFBlog.DAL.Models;
using SFBlog.DAL.Repository;
using SFBlog.DAL.Repository.Contract;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton)
    .AddIdentity<User, Role>(opts =>
    {
        opts.Password.RequiredLength = 5;
        opts.Password.RequireNonAlphanumeric = false;
        opts.Password.RequireLowercase = false;
        opts.Password.RequireUppercase = false;
        opts.Password.RequireDigit = false;
        opts.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<BlogContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddSwaggerGen(c =>
{
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "SFBlog.API.xml");
    c.IncludeXmlComments(filePath);
});

var mapperConfig = new MapperConfiguration((m) =>
{
    m.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper)
    .AddTransient<ICommentRepository, CommentRepository>()
    .AddTransient<IPostRepository, PostRepository>()
    .AddTransient<ITagRepository, TagRepository>()
    .AddTransient<IHomeService, HomeService>()
    .AddTransient<ITagService, TagService>()
    .AddTransient<IUserService, UserService>()
    .AddTransient<ICommentService, CommentService>()
    .AddTransient<IPostService, PostService>()
    .AddTransient<IRoleService, RoleService>();

builder.Services.AddAuthentication(optionts => optionts.DefaultScheme = "Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
        {
            OnRedirectToLogin = redirectContext =>
            {
                redirectContext.HttpContext.Response.StatusCode = 401;
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
