namespace DAW.Modells
{
    public class GamePlayer
    {
        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public Game Game { set; get; }

        public Player Player { get; set; }
    }
}
