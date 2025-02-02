using BloggerApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggerApp.ViewComponents {
    public class NewPosts(IPostRepository postRepository) : ViewComponent {
        private IPostRepository _postRepository = postRepository;

        public async Task<IViewComponentResult> InvokeAsync() {
            return View(await _postRepository
                .Posts
                .OrderByDescending(p => p.PublishedOn)
                .Take(5)
                .ToListAsync()
                );
        }
    }
}