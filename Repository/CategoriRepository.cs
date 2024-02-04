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

        public bool CreateCategorie(Category cat)
        {
            _context.Add(cat);

            return Save();
        }

        public bool DeleteCategory(Category cat)
        {
            _context.Remove(cat);
            return Save();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategorie(Category cat)
        {
            _context.Update(cat);
            return Save();
        }
    }
}
