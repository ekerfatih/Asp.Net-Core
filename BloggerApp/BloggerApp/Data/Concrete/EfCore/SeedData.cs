using BloggerApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BloggerApp.Data.Concrete.EfCore {
    public static class SeedData {
        public static void FillDataToDatabase(IApplicationBuilder app) {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
            if (context != null) {
                if (context.Database.GetPendingMigrations().Any()) { // Uygulanmamış migrations var mı?
                    context.Database.Migrate();
                }
                if(!context.Tags.Any()) {
                    context.Tags.AddRange(
                        new Tag {Text="Web Programlama"},
                        new Tag {Text="Backend"},
                        new Tag {Text="Frontend"},
                        new Tag {Text="Fullstack"},
                        new Tag {Text="Php"}
                    );
                    context.SaveChanges();
                }
                if(!context.Users.Any()) {
                    context.Users.AddRange(
                        new User {UserName="sadikturan"},
                        new User {UserName="ahmetyilmaz"}
                    );
                    context.SaveChanges();
                }
                if(!context.Posts.Any()){
                    context.Posts.AddRange(
                        new Post {
                            Title="Asp.net core",
                            Content = "Asp.net core dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-10),
                            Tags = context.Tags.Take(3).ToList(),
                            Image = "1.jpg",
                            UserId = 1,
                        },
                        new Post {
                            Title="Php",
                            Content = "Php core dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-20),
                            Tags = context.Tags.Take(2).ToList(),
                            Image = "2.jpg",
                            UserId = 1,
                        },
                        new Post {
                            Title="Django",
                            Content = "Django dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-5),
                            Tags = context.Tags.Take(4).ToList(),
                            Image = "8.jpg",
                            UserId = 2,
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}