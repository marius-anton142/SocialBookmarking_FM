using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialBookmarking_FM.Models
{
    public class Vote
    {
        public string UserId { get; set; }
        public int BookmarkId { get; set; }
    }
}
