using DAW.Modells;

namespace Daw.Interfaces
{
    public interface CategoryInterface
    {
        public ICollection<Category> GetCategories();

        Category GetCategory(int id);
        Category GetCategory(string name);

        ICollection<Game>  GetGameByCategory(int categoryid);
        
        bool CategoriesExists(int categoryid);
    }
}
