using System.ComponentModel.DataAnnotations;

namespace SocialBookmarking_FM.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Required(ErrorMessage = "Continutul este obligatoriu")]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public int BookmarkId { get; set; }
        public virtual Bookmark Bookmark { get; set; }
    }

}
