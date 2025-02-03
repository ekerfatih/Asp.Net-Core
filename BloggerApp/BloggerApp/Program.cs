using BloggerApp.Data.Abstract;
using BloggerApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogContext>(options => {
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("ms_sql_connection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    options => options.LoginPath ="/Users/Login"
);


builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ITagRepository, EfTagRepository>();
builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

SeedData.FillDataToDatabase(app);

// app.MapDefaultControllerRoute();
app.MapControllerRoute(
    name:"post_details",
    pattern : "blogs/details/{url}",
    defaults : new { controller = "Posts", action="Details"}
);

app.MapControllerRoute(
    name:"posts_by_tag",
    pattern : "blogs/tags/{tag}",
    defaults : new { controller = "Posts", action="Index"}
);

app.MapControllerRoute(
    name:"user_profile",
    pattern : "profile/{username}",
    defaults : new { controller = "Users", action="Profile"}
);

app.MapControllerRoute(
    name:"defaut",
    pattern : "{controller=Posts}/{action=Index}/{id?}"
);

app.Run();
