using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Blog_Website.Data;

namespace My_Blog_Website.Components
{
    public class LastModifiedPostsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public LastModifiedPostsViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Retrieve 9 published posts ordered by last modified date descending
            var posts = await _db.posts
                .Where(p => p.LastModifiedDate != null && p.PostStatus == "Published")
                .OrderByDescending(p => p.LastModifiedDate)
                .Take(9)
                .ToListAsync();

            return View(posts);
        }
    }
}