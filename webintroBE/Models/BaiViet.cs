using System.ComponentModel.DataAnnotations;

namespace webintroBE.Models
{
    public class BaiViet
    {

        public int Id { get; set; }

        [Required, MaxLength(300)]
        public string Title { get; set; }

        [Required, MaxLength(500)]
        public string Image { get; set; }

        [Required, MaxLength(5000)]
        public string Content { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
