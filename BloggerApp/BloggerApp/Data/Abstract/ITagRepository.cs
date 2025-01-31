using BloggerApp.Entity;

namespace BloggerApp.Data.Abstract {
    public interface ITagRepository {
        IQueryable<Tag> Tags { get; }
        void CreateTag(Tag tag);
    }
}