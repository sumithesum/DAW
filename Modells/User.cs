namespace Daw.Modells
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;

        public bool IsAdmin { get; set; } = false;

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string RefreshToken {  get; set; }= string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpired { get; set; }
    }
}
