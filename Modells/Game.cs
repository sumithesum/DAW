namespace DAW.Modells
{
    public class Game
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime PublishDATE { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<GamePlayer> GamePlayers { get; set; }

        public ICollection<GameCategory> GameCategories { get; set; }
    }
}
