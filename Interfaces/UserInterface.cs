using Daw.Modells;

namespace Daw.Interfaces
{
    public interface UserInterface
    {
        bool Save();
        bool DeleteUser(User user);

        bool UpdateUser(User user);

        User GetUser(int userId);

        bool CreateUser(User user);

        bool UserExists(int userId);

        ICollection<User> GetUsers();

    }
}
