using Daw.Interfaces;
using DAW.Data;
using DAW.Modells;

namespace Daw.Repository
{
    public class CategoryRepository : CategoryInterface
    {
        private DataContext _context;

        public CategoryRepository(DataContext context) {
            _context = context;
        }

        public bool CategoriesExists(int categoryid)
        {
            return _context.Categories.Any( c => c.ID == categoryid);
        }

        public ICollection<Category> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Category GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Category GetCategory(string name)
        {
            throw new NotImplementedException();
        }

        public ICollection<Game> GetGameByCategory(int categoryid)
        {
            throw new NotImplementedException();
        }
    }
}
