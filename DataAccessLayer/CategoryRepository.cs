using BusinessObjects;

namespace Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetCategories()
        {
            using var dbContext = new ProductStoreDbContext();
            return dbContext.Categories.ToList();
        }
    }
}
