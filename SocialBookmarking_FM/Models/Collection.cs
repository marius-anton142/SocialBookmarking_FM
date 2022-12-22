using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialBookmarking_FM.Models
{
    public class Collection
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Categoria este obligatorie")]
        [ForeignKey("CollectionCategory")]
        public int CategoryId { get; set; }
        public virtual CollectionCategory Category { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
