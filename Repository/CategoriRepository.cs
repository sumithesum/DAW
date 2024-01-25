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
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.ID == id).FirstOrDefault();
        }


        public ICollection<Game> GetGameByCategory(int categoryid)
        {
            return _context.GameCategories.Where(p  => p.CategoryID == categoryid).Select(c => c.Game).ToList();
        }
    }
}
