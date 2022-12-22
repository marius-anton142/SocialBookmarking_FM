using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialBookmarking_FM.Models
{
    public class BookmarkCollection
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Collection")]
        public int CollectionId { get; set; }
        public virtual ICollection<Collection> Collections { get; set; }
        [ForeignKey("Bookmark")]
        public int BookmarkId { get; set; }
        public virtual ICollection<Bookmark> Bookmarks { get; set; }

    }
}
