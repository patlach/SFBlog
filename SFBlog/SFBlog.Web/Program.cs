using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SFBlog.BLL.Services;
using SFBlog.BLL.Services.IServices;
using SFBlog.DAL;
using SFBlog.DAL.Models;
using SFBlog.DAL.Repository;
using SFBlog.DAL.Repository.Contract;
using SFBlog.Web;

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
    .AddEntityFrameworkStores<BlogContext>(); ;

var mapperConfig = new MapperConfiguration((m) =>
{
    m.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper)
                .AddTransient<IHomeService, HomeService>()
                .AddTransient<ITagRepository, TagRepository>()
                .AddTransient<ITagService, TagService>()
                .AddControllersWithViews();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("Shared/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
