using FirstWebAPI.Data;
using JobTaskProject.Interface;
using JobTaskProject.Models;

namespace JobTaskProject.Repository
{
    public class UserRepository : IUserRepository
    {
        #region
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        #endregion

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        } 

        public User GetUserById(string id)
        {
            return _context.Users.FirstOrDefault();
        }

        public bool UserExists(string UserId)
        {
            return _context.Categories.Any(p => p.Id.Equals (UserId));
        }
    }
}
