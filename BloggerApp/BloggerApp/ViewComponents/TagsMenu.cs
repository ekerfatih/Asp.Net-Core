using BloggerApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BloggerApp.ViewComponents
{
    public class TagsMenu(ITagRepository tagRepository) : ViewComponent{
        private ITagRepository _tagRepository = tagRepository;

        public async Task<IViewComponentResult> InvokeAsync(){
            return View(await _tagRepository.Tags.ToListAsync());
        }
        
    }
}