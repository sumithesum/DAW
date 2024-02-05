namespace Daw.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;

        public bool IsAdmin { get; set; } = false;

        public string Password { get; set; } = string.Empty;
    }
}
