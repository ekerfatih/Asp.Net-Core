using BloggerApp.Entity;

namespace BloggerApp.Data.Abstract {
    public interface IPostRepository {
        IQueryable<Post> Posts { get; }
        void CreatePost(Post post);
        void EditPost(Post post);
        void EditPost(Post post,int[] tagIds);
    }
}