namespace DAW.Modells
{
    public class SERVER
    {
        public int ID { get; set; }

        public string Region { get; set; }

        public ICollection<Player> Players { get; set; }

       
    }
}
