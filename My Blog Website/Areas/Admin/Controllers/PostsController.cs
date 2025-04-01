using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Blog_Website.Areas.Admin.Models;
using My_Blog_Website.Areas.Public.Interfaces;
using My_Blog_Website.Areas.Public.Models;
using My_Blog_Website.Data;
using My_Blog_Website.Helpers;

namespace My_Blog_Website.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IEmailService _emailService;

        public PostsController(ApplicationDbContext db, IEmailService emailService)
        {
            _db = db;
            _emailService = emailService;
        }

        #region admin

        [HttpGet]
        [Route("admin/posts")]
        public IActionResult Index(string searchTerm)
        {
            var posts = _db.posts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var search = searchTerm.Trim().ToLower();
                posts = posts.Where(p =>
                    p.Title.ToLower().Contains(search) ||
                    p.PostDescription.ToLower().Contains(search) ||
                    p.Categories.ToLower().Contains(search) ||
                    p.Tags.ToLower().Contains(search)
                );
            }

            ViewData["CurrentFilter"] = searchTerm; // Preserve search term

            var orderedPost = posts.OrderByDescending(p => p.LastModifiedDate ?? p.PublishedDate).ToList();

            return View(orderedPost);
        }

        [HttpGet]
        [Route("/admin/posts/search")]
        public IActionResult SearchPosts(string term)
        {
            var query = _db.posts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(term))
            {
                query = query.Where(p =>
                    p.Title.Contains(term) ||
                    p.PostDescription.Contains(term) ||
                    p.Categories.Contains(term) ||
                    p.Tags.Contains(term));
            }

            var results = query.OrderByDescending(p => p.LastModifiedDate ?? p.PublishedDate)
                .Select(p => new
                {
                    p.PostId,
                    p.Title,
                    PostContent = p.PostContent,
                    p.PostDescription,
                    FeatureImageUrl = p.FeatureImageUrl,
                    p.Categories,
                    p.Tags,
                    PostStatus = p.PostStatus,
                    p.PublishedDate
                })
                .ToList();

            return Json(results);
        }

        [HttpPost]
        [Route("admin/post/update-status/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusUpdateModel model)
        {
            var post = await _db.posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            post.PostStatus = model.NewStatus;

            // Update publish date if publishing
            if (model.NewStatus == "Published" && !post.PublishedDate.HasValue)
            {
                post.PublishedDate = DateTime.Now;
            }

            _db.posts.Update(post);
            await _db.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("admin/post/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("admin/post/create")]
        public async Task<IActionResult> Create(Posts posts)
        {
            // Get the uploaded file from the request (if any)
            var featureImage = Request.Form.Files["featureImage"];

            // Remove validation for FeatureImageUrl to avoid conflicts
            ModelState.Remove("FeatureImageUrl");

            // Check if either file or URL is provided
            bool hasFile = featureImage != null && featureImage.Length > 0;
            bool hasUrl = !string.IsNullOrEmpty(posts.FeatureImageUrl);

            // Add custom validation
            if (!hasFile && !hasUrl)
            {
                ModelState.AddModelError("FeatureImageUrl", "You must upload an image or provide a URL.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle file upload
                    if (hasFile)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "FeatureImages");
                        Directory.CreateDirectory(uploadsFolder);

                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(featureImage.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await featureImage.CopyToAsync(stream);
                        }
                        posts.FeatureImageUrl = $"/uploads/FeatureImages/{fileName}";
                    }
                    // Validate URL format if used
                    else if (hasUrl && !Uri.IsWellFormedUriString(posts.FeatureImageUrl, UriKind.Absolute))
                    {
                        ModelState.AddModelError("FeatureImageUrl", "Invalid URL format.");
                        return View(posts);
                    }

                    // Save to database
                    posts.Slug = URLHelper.GeneratePostSlug(posts.Title);
                    posts.PublishedDate = DateTime.Now;
                    _db.Add(posts);
                    await _db.SaveChangesAsync();

                    // Send notifications
                    var subscribers = await _db.subscribers
                        .Where(s => s.IsActive)
                        .Select(s => s.Email)
                        .ToListAsync();

                    foreach (var email in subscribers)
                    {
                        await _emailService.SendNewPostNotificationAsync(email, posts.Title, posts.PostDescription, posts.Categories, posts.Slug);
                    }

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                }
            }
            return View(posts);
        }

        [HttpGet]
        [Route("admin/post/published")]
        public IActionResult Published()
        {
            List<Posts> posts = _db.posts
                .Where(p => p.PostStatus == "Published")
                .OrderByDescending(p => p.PublishedDate)
                .ToList();
            return View(posts);
        }

        [HttpGet]
        [Route("admin/post/draft")]
        public IActionResult Draft()
        {
            List<Posts> posts = _db.posts
                .Where(p => p.PostStatus == "Draft")
                .OrderByDescending(p => p.PublishedDate)
                .ToList();
            return View(posts);
        }

        [HttpGet]
        [Route("admin/post/edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var post = _db.posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        [Route("admin/post/edit/{id}")]
        public async Task<IActionResult> Edit(int id, Posts post)
        {
            if (id != post.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingPost = await _db.posts.FindAsync(id);
                if (existingPost == null)
                {
                    return NotFound();
                }

                // Track changes
                var hasChanges = !existingPost.Title.Equals(post.Title) ||
                                !existingPost.PostContent.Equals(post.PostContent) ||
                                !existingPost.PostDescription.Equals(post.PostDescription) ||
                                !existingPost.FeatureImageUrl.Equals(post.FeatureImageUrl) ||
                                !existingPost.Categories.Equals(post.Categories) ||
                                !existingPost.Tags.Equals(post.Tags);

                // Update each property from the form data
                existingPost.Title = post.Title;
                existingPost.PostContent = post.PostContent;
                existingPost.PostDescription = post.PostDescription;
                existingPost.Categories = post.Categories;
                existingPost.Tags = post.Tags;
                existingPost.FeatureImageUrl = post.FeatureImageUrl;
                existingPost.PostStatus = post.PostStatus;
                existingPost.Slug = URLHelper.GeneratePostSlug(post.Title);

                // Only update if changes detected
                if (hasChanges)
                {
                    existingPost.LastModifiedDate = DateTime.Now;
                }

                _db.posts.Update(existingPost);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(post);
        }

        [HttpGet]
        [Route("admin/post/delete/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var post = _db.posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        [Route("admin/post/delete/{id}")]
        public async Task<IActionResult> DeletePost(int? id)
        {
            var post = _db.posts.Find(id);

            if (post == null)
                return NotFound();

            _db.posts.Remove(post);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion

        #region public

        [HttpGet]
        [Route("{category}/{slug}")]
        public async Task<IActionResult> Single(string category, string slug)
        {
            if (string.IsNullOrEmpty(slug))
            {
                return NotFound();
            }

            // Retrieve the post by slug. You can also filter by category if needed.
            var post = await _db.posts
                .Include(p => p.PostReactionVotes)
                .Include(p => p.Comments)
                .ThenInclude(c => c.ReactionVotes)
                .Include(p => p.Comments)
                .ThenInclude(c => c.Replies) 
                .FirstOrDefaultAsync(p => p.Slug == slug);

            if (post == null)
            {
                return NotFound();
            }

            // Increment view count
            post.ViewCount++;
            _db.Update(post);
            await _db.SaveChangesAsync();

            // Get similar posts from the same category (excluding current post)
            var similarPosts = await _db.posts
                .Where(p => p.Categories == category && p.Slug != slug)
                .OrderByDescending(p => p.PublishedDate)
                .ToListAsync();

            // Get Most Popular Posts (Updated with Score Calculation)
            var popularPosts = await _db.posts
                .Select(p => new
                {
                    Post = p,
                    Score = p.ViewCount
                        + p.Comments.Count // Total comments
                        + p.Comments.SelectMany(c => c.Replies).Count() // Total replies
                        + p.Comments.SelectMany(c => c.ReactionVotes).Count() // Total comment reactions
                        //+ p.PostReactions.Sum(pr => pr.ReactionCount) // Total post reactions
                        + p.PostReactionVotes.Count()
                })
                .OrderByDescending(x => x.Score)
                .Take(4)
                .Select(x => x.Post)
                .ToListAsync();

            ViewBag.SimilarPosts = similarPosts;
            ViewBag.CurrentCategory = category;
            ViewBag.PopularPosts = popularPosts;

            return View(post);
        }

        [HttpGet]
        [Route("PostReact")]
        public async Task<IActionResult> PostReact(int postId, string reactionType)
        {
            // Validate reaction type
            var validReactions = new[] { "like", "love", "care", "angry", "support" };
            if (!validReactions.Contains(reactionType))
            {
                return BadRequest("Invalid reaction type.");
            }

            // Get or create user identifier from cookie
            string userIdentifier = Request.Cookies["UserIdentifier"];
            if (string.IsNullOrEmpty(userIdentifier))
            {
                userIdentifier = Guid.NewGuid().ToString();
                Response.Cookies.Append("UserIdentifier", userIdentifier, new CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1)
                });
            }

            // Check existing vote
            var existingVote = await _db.postReactionVotes
                .FirstOrDefaultAsync(pr => pr.PostId == postId && pr.UserIdentifier == userIdentifier);

            if (existingVote != null)
            {
                // Remove if same reaction clicked again (toggle)
                if (existingVote.ReactionType == reactionType)
                {
                    _db.postReactionVotes.Remove(existingVote);
                }
                else // Update to new reaction
                {
                    existingVote.ReactionType = reactionType;
                    existingVote.VoteDate = DateTime.Now;
                    _db.Update(existingVote);
                }
            }
            else
            {
                // Add new vote
                var newVote = new PostReactionVote
                {
                    PostId = postId,
                    UserIdentifier = userIdentifier,
                    ReactionType = reactionType,
                    VoteDate = DateTime.Now
                };
                _db.postReactionVotes.Add(newVote);
            }

            await _db.SaveChangesAsync();

            // Return JSON response
            var post = await _db.posts
                .Include(p => p.PostReactionVotes)
                .FirstOrDefaultAsync(p => p.PostId == postId);
            var reactionCounts = new Dictionary<string, int>
            {
                {"like", post.PostReactionVotes.Count(r => r.ReactionType == "like")},
                {"love", post.PostReactionVotes.Count(r => r.ReactionType == "love")},
                {"care", post.PostReactionVotes.Count(r => r.ReactionType == "care")},
                {"angry", post.PostReactionVotes.Count(r => r.ReactionType == "angry")},
                {"support", post.PostReactionVotes.Count(r => r.ReactionType == "support")},
            };

            return Json(new { reactionCounts });
        }


        [HttpPost]
        [Route("AddComment")]
        public async Task<IActionResult> AddComment(int PostId, string CommenterName, string CommentText, int? ParentCommentId)
        {
            if (string.IsNullOrEmpty(CommenterName) || string.IsNullOrEmpty(CommentText))
            {
                return BadRequest("Name and comment text are required.");
            }

            var comment = new Comment
            {
                PostId = PostId,
                CommenterName = CommenterName,
                ParentCommentId = ParentCommentId,
                CommentText = CommentText,
                CommentDate = DateTime.Now
            };

            _db.Comments.Add(comment);
            await _db.SaveChangesAsync();

            var post = await _db.posts.FirstOrDefaultAsync(p => p.PostId == PostId);
            if (post != null)
            {
                return RedirectToAction("Single", new { category = post.Categories, slug = post.Slug });
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("React")]
        public async Task<IActionResult> React(int commentId, string reactionType)
        {
            // Validate reaction type
            var validReactions = new[] { "like", "love", "care", "angry", "support" };
            if (!validReactions.Contains(reactionType))
            {
                return BadRequest("Invalid reaction type.");
            }

            // Get or create user identifier from cookie
            string userIdentifier = Request.Cookies["UserIdentifier"];
            if (string.IsNullOrEmpty(userIdentifier))
            {
                userIdentifier = Guid.NewGuid().ToString();
                Response.Cookies.Append("UserIdentifier", userIdentifier, new CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1)
                });
            }

            // Check existing vote
            var existingVote = await _db.ReactionVotes
                .FirstOrDefaultAsync(rv => rv.CommentId == commentId && rv.UserIdentifier == userIdentifier);

            if (existingVote != null)
            {
                // Remove if same reaction clicked again (toggle)
                if (existingVote.ReactionType == reactionType)
                {
                    _db.ReactionVotes.Remove(existingVote);
                }
                else // Update to new reaction
                {
                    existingVote.ReactionType = reactionType;
                    existingVote.VoteDate = DateTime.Now;
                    _db.Update(existingVote);
                }
            }
            else
            {
                // Add new vote
                var newVote = new ReactionVote
                {
                    CommentId = commentId,
                    UserIdentifier = userIdentifier,
                    ReactionType = reactionType,
                    VoteDate = DateTime.Now
                };
                _db.ReactionVotes.Add(newVote);
            }

            await _db.SaveChangesAsync();

            // Fetch the updated comment with its reaction votes
            var comment = await _db.Comments
                .Include(c => c.ReactionVotes)
                .FirstOrDefaultAsync(c => c.CommentId == commentId);

            if (comment == null)
            {
                return NotFound();
            }

            // Prepare reaction counts
            var reactionCounts = new Dictionary<string, int>
            {
                {"like", comment.ReactionVotes.Count(r => r.ReactionType == "like")},
                {"love", comment.ReactionVotes.Count(r => r.ReactionType == "love")},
                {"care", comment.ReactionVotes.Count(r => r.ReactionType == "care")},
                {"angry", comment.ReactionVotes.Count(r => r.ReactionType == "angry")},
                {"support", comment.ReactionVotes.Count(r => r.ReactionType == "support")},
            };

            return Json(new { reactionCounts });
        }

        #endregion
    }
}