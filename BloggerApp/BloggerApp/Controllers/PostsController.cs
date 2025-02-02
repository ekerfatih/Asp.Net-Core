using BloggerApp.Data.Abstract;
using BloggerApp.Data.Concrete.EfCore;
using BloggerApp.Entity;
using BloggerApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggerApp.Controllers {
    public class PostsController(IPostRepository postRepository, ICommentRepository commentRepository) : Controller {
        private IPostRepository _postRepository = postRepository;
        private ICommentRepository _commentRepository = commentRepository;
        public async Task<IActionResult> Index(string tag) {
            var claims = User.Claims;
            var posts = _postRepository.Posts;
            if (!string.IsNullOrEmpty(tag)) {
                posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));
            }

            return View(new PostsViewModel { Posts = await posts.ToListAsync() });
        }

        public async Task<IActionResult> Details(string url) {
            return View(await _postRepository
                .Posts
                .Include(x => x.Tags)
                .Include(x => x.Comments)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Url == url));
        }

        [HttpPost]
        public JsonResult AddComment(int PostId, string UserName, string Text) {
            var entity = new Comment {
                Text = Text,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                User = new User { UserName = UserName, Image = "5.jpg" }
            };
            

            _commentRepository.CreateComment(entity);
            //return Redirect("/blogs/details/"+Url);
            //return RedirectToRoute("post_details", new { url = Url });
            return Json(new {
                UserName,
                Text,
                PublishedOn  = entity.PublishedOn.ToString("MM/dd/yyyy"),
                entity.User.Image
            });
        }
    }
}