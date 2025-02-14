using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> AddBlogPost(BlogPost blogPost);
        Task<IEnumerable<BlogPost>> GetAllBlogPost();

        Task<BlogPost> GetBlogPostById(Guid id);
    }
}
