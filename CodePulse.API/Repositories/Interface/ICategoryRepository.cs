using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetCategoryById(Guid id);
        Task<Category?> UpdateCategoryById(Category category);
        Task<Category?> DeleteCategoryById(Guid id);
    
    }
}
