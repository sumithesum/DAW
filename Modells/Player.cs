namespace DAW.Modells
{
    public class Player
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public SERVER Server { get; set; }

        public ICollection<GamePlayer> gamePlayers { get; set; }


    }
}
