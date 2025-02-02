using BloggerApp.Entity;

namespace BloggerApp.Data.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        public void CreateUser(User user);
    }
}