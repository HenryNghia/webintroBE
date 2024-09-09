using System.ComponentModel.DataAnnotations;

namespace webintroBE.DTOs.BaiViet
{
    public class UpdateBaiVietDTO
    {
        [MaxLength(300)]
        public required string Title { get; set; } = "";

        [MaxLength(500)]
        public required string Image { get; set; } = "";

        [MaxLength(5000)]
        public required string Content { get; set; } = "";
    }
}
