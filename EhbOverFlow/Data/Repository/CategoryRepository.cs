using EhbOverFlow.Areas.Identity.Data;
using EhbOverFlow.Models;

namespace EhbOverFlow.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {

        private ApplicationDbContext _ehbOverFlowDbContext;

        public CategoryRepository(ApplicationDbContext ehbOverFlowDbContext)
        {
            _ehbOverFlowDbContext = ehbOverFlowDbContext;
        }
        public void AddCategory(Category category)
        {
            _ehbOverFlowDbContext.Categories.Add(category);
        }

        public List<Category> GetAllCategories()
        {
            return _ehbOverFlowDbContext.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _ehbOverFlowDbContext.Categories.FirstOrDefault(c => c.Id == id);
        }

        public void RemoveCategory(int id)
        {
            _ehbOverFlowDbContext.Remove(GetCategory(id));
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _ehbOverFlowDbContext.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public void UpdateCategory(Category category)
        {
            _ehbOverFlowDbContext.Update(category);
        }
    }
}
