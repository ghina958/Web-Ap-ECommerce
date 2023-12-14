using JobTaskProject.Models;

namespace JobTaskProject.Interface
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUserById(string id);
        bool UserExists(string UserId);
    }
}
