using EhbOverFlow.Models;

namespace EhbOverFlow.Data.Repository
{
    public interface ICategoryRepository
    {
        void AddCategory(Category category);
        List<Category> GetAllCategories();
        Category GetCategory(int id);
        void RemoveCategory(int id);
        void UpdateCategory(Category category);

        Task<bool> SaveChangesAsync();
    }
}
