using BloggerApp.Data.Abstract;
using BloggerApp.Entity;

namespace BloggerApp.Data.Concrete.EfCore{
    public class EfUserRepository(BlogContext context) : IUserRepository {

        private readonly BlogContext _context = context;
        public IQueryable<User> Users => _context.Users;

        public void CreateUser(User user) {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}