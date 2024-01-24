namespace DAW.Modells
{
    public class Category
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public ICollection<GameCategory> GameCategories { get; set; }
    }
}
