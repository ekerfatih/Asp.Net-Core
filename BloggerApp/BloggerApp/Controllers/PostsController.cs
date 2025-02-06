using BloggerApp.Data.Abstract;
using BloggerApp.Data.Concrete.EfCore;
using BloggerApp.Entity;
using BloggerApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BloggerApp.Controllers {
    public class PostsController(IPostRepository postRepository, ICommentRepository commentRepository, ITagRepository tagRepository) : Controller {
        private IPostRepository _postRepository = postRepository;
        private ICommentRepository _commentRepository = commentRepository;
        private ITagRepository _tagsRepository = tagRepository; 
        public async Task<IActionResult> Index(string tag) {

            // if(!User.Identity!.IsAuthenticated) {
            //     return RedirectToAction("Login","Posts");
            // }

            var claims = User.Claims;
            var posts = _postRepository.Posts.Where(i => i.IsActive);
            if (!string.IsNullOrEmpty(tag)) {
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));
            }

            return View(new PostsViewModel { Posts = await posts.ToListAsync() });
        }

        public async Task<IActionResult> Details(string url) {
            return View(await _postRepository
                .Posts
                .Include(x=> x.User)
                .Include(x => x.Tags)
                .Include(x => x.Comments)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Url == url));
        }

        [HttpPost]
        public JsonResult AddComment(int PostId, string Text) {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var avatar = User.FindFirstValue(ClaimTypes.UserData);

            var entity = new Comment {
                PostId = PostId,
                Text = Text,
                PublishedOn = DateTime.Now,
                UserId = int.Parse(userId ?? ""),
            };


            _commentRepository.CreateComment(entity);
            //return Redirect("/blogs/details/"+Url);
            //return RedirectToRoute("post_details", new { url = Url });
            return Json(new {
                userName,
                Text,
                PublishedOn = entity.PublishedOn.ToString("MM/dd/yyyy"),
                avatar
            });
        }

        [Authorize]
        public IActionResult Create() {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(PostCreateViewModel model) {
            if (ModelState.IsValid) {

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _postRepository.CreatePost(
                    new Post {
                        Title = model.Title,
                        Content = model.Content,
                        Url = model.Url,
                        UserId = int.Parse(userId),
                        PublishedOn = DateTime.Now,
                        Image = "avatar.jpg",
                        IsActive = false
                    }
                );
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> ListAsync() {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
            var role = User.FindFirstValue(ClaimTypes.Role);

            var posts = _postRepository.Posts;
            if (string.IsNullOrEmpty(role)) {
                posts = posts.Where(p => p.UserId == userId);
            }
            return View(await posts.ToListAsync());
        }

        [Authorize]
        public IActionResult Edit(int? id) {
            if (id == null) {
                return NotFound();
            }
            var post = _postRepository.Posts.Include(x=> x.Tags).FirstOrDefault(i => i.PostId == id);
            if (post == null) {
                return NotFound();
            }

            ViewBag.Tags = _tagsRepository.Tags.ToList();

            return View(new PostCreateViewModel {
                PostId = post.PostId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Url = post.Url,
                IsActive = post.IsActive,
                Tags = post.Tags
            });
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(PostCreateViewModel model, int[] tagIds) {
            if (ModelState.IsValid) {
                var entityToUpdate = new Post {
                    PostId = model.PostId,
                    Title = model.Title,
                    Description = model.Description,
                    Content = model.Content,
                    Url = model.Url,
                };

                if (User.FindFirstValue(ClaimTypes.Role) == "admin") {
                    entityToUpdate.IsActive = model.IsActive;
                }
                _postRepository.EditPost(entityToUpdate,tagIds);
                return RedirectToAction("List");
            }
            ViewBag.Tags = _tagsRepository.Tags.ToList();
            return View(model);
        }

    }

}