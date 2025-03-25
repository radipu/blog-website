using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Blog_Website.Data;

namespace My_Blog_Website.Components
{
    public class MostViewedPostsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public MostViewedPostsViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Retrieve the 3 most viewed posts (assuming you have a ViewCount property)
            var posts = await _db.posts
                .Where(p => p.PostStatus == "Published")
                .OrderByDescending(p => p.ViewCount) // Order by the most viewed
                .Take(3) // Limit to 3 posts
                .ToListAsync();

            return View(posts);
        }
    }
}