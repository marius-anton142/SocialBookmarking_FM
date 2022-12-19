using System.ComponentModel.DataAnnotations;

namespace SocialBookmarking_FM.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Numele categoriei este obligatoriu")]
        public string CategoryName { get; set; }
        public virtual ICollection<Bookmark> Bookmarks { get; set; }
    }

}
