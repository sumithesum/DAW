namespace DAW.Modells
{
    public class GameCategory
    {
        public int GameId { get; set; }

        public int CategoryID { get; set; }

        public Game Game { get; set; }

        public Category Category { get; set; }
    }
}
