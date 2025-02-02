using BloggerApp.Entity;

namespace BloggerApp.Data.Abstract
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }
        void CreateComment(Comment comment);
    }
}