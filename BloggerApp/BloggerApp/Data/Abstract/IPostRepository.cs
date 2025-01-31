using BloggerApp.Entity;

namespace BloggerApp.Data.Abstract {
    public interface IPostRepository {
        IQueryable<Post> Posts { get; }
        void CreatePost(Post post);
    }
}