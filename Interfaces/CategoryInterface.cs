using DAW.Modells;

namespace Daw.Interfaces
{
    public interface CategoryInterface
    {
        public ICollection<Category> GetCategories();

        Category GetCategory(int id);

        ICollection<Game>  GetGameByCategory(int categoryid);
        
        bool CategoriesExists(int categoryid);
        bool CreateCategorie(Category cat);

        bool Save();

        public bool UpdateCategorie(Category cat);

        public bool DeleteCategory(Category cat);
    }
}
