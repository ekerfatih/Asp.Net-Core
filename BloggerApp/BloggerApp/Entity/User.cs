using System.ComponentModel.DataAnnotations;

namespace BloggerApp.Entity {
    public class User {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; } = null!;
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}