using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
       private readonly ApplicationDbContext _context;

        public BlogPostRepository(ApplicationDbContext context) {
            _context = context;
        }
        public async Task<BlogPost> AddBlogPost(BlogPost blogPost)
        {
            await _context.BlogPosts.AddAsync(blogPost);
            _context.SaveChanges();
            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPost()
        {
           return await _context.BlogPosts.Include(x=>x.Categories).ToListAsync();
        }

        public async Task<BlogPost?> GetBlogPostById(Guid id)
        {
            return await _context.BlogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
