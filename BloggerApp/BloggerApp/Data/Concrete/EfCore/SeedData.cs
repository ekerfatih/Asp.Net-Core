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
                if (!context.Tags.Any()) {
                    context.Tags.AddRange(
                        new Tag { Text = "Web Programlama", Url = "web-programlama", TagColor = TagColors.warning },
                        new Tag { Text = "Backend", Url = "backend", TagColor = TagColors.info },
                        new Tag { Text = "Frontend", Url = "frontend", TagColor = TagColors.success },
                        new Tag { Text = "Fullstack", Url = "fullstack", TagColor = TagColors.secondary },
                        new Tag { Text = "Php", Url = "php", TagColor = TagColors.primary }
                    );
                    context.SaveChanges();
                }
                if (!context.Users.Any()) {
                    context.Users.AddRange(
                        new User { UserName = "sadikturan",Name="Sadik Turan",Email="info@sadikturan.com",Password="123123" ,Image = "p1.jpg" },
                        new User { UserName = "cinarturan",Name="Çınar Turan",Email="info@cinarturan.com",Password="321321" ,Image = "p2.jpg" }
                    );
                    context.SaveChanges();
                }
                if (!context.Posts.Any()) {
                    context.Posts.AddRange(
                        new Post {
                            Title = "Asp.net core",
                            Description="Asp.net core dersleri",
                            Content = "Asp.net core dersleri",
                            Url = "aspnet-core",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-10),
                            Tags = context.Tags.Take(3).ToList(),
                            Image = "1.jpg",
                            UserId = 1,
                            Comments = new List<Comment> {
                                new Comment {Text = "İyi bir kurs", PublishedOn = DateTime.Now.AddHours(-10), UserId = 1},
                                new Comment {Text = "Çok faydalandığım bir kurs", PublishedOn = DateTime.Now, UserId = 2}
                            }
                        },
                        new Post {
                            Title = "Php",
                            Content = "Php core dersleri",
                            Description = "Php core dersleri",
                            Url = "php",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-20),
                            Tags = context.Tags.Take(2).ToList(),
                            Image = "2.jpg",
                            UserId = 1,
                        },
                        new Post {
                            Title = "Django",
                            Content = "Django dersleri",
                            Description = "Django dersleri",
                            Url = "django",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-5),
                            Tags = context.Tags.Take(4).ToList(),
                            Image = "8.jpg",
                            UserId = 2,
                        },
                        new Post {
                            Title = "React Dersleri",
                            Content = "React dersleri",
                            Description = "React dersleri",
                            Url = "react-dersleri",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-30),
                            Tags = context.Tags.Take(2).ToList(),
                            Image = "3.jpg",
                            UserId = 2,
                        },
                        new Post {
                            Title = "Angular",
                            Content = "Angular dersleri",
                            Description = "Angular dersleri",
                            Url = "angular",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-40),
                            Tags = context.Tags.Take(1).ToList(),
                            Image = "4.jpg",
                            UserId = 2,
                        },
                        new Post {
                            Title = "Web Tasarım",
                            Content = "Web Tasarım dersleri",
                            Description = "Web Tasarım dersleri",
                            Url = "web-tasarim",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-50),
                            Tags = context.Tags.Take(2).ToList(),
                            Image = "6.jpg",
                            UserId = 2,
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}