using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialBookmarking_FM.Models
{
    public class CollectionCategory
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Numele categoriei este obligatoriu")]
        public string CategoryName { get; set; }
        public virtual ICollection<Collection> Collections { get; set; }
    }
}
