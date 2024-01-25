namespace DAW.Modells
{
    public class Review
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public int rating { get; set; } 

        public ICollection<Review> Reviews { get; set; }

        public Reviewer Reviewer { get; set; }

        public Game Game { get; set; }

    }
}
