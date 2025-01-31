using BloggerApp.Data.Abstract;
using BloggerApp.Data.Concrete.EfCore;
using BloggerApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BloggerApp.Controllers {
    public class PostsController(IPostRepository postRepository, ITagRepository tagRepository) : Controller {
        private IPostRepository _postRepository = postRepository;
        private ITagRepository _tagRepository =  tagRepository;
        public IActionResult Index() {
            
            return View(
                new PostsViewModel {
                    Posts = _postRepository.Posts.ToList(),
                    Tags = _tagRepository.Tags.ToList()
                }
            );
        }
    }
}