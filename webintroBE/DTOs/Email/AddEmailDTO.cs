using System.ComponentModel.DataAnnotations;

namespace webintroBE.DTOs.Email
{
    public class AddEmailDTO
    {
        public required string Name { get; set; } = "";
        [MaxLength(100)]
        public string Surname { get; set; } = "";
        [MaxLength(100)]
        public required string Emailuser { get; set; } = "";
        [MaxLength]
        public required string Content { get; set; } = "";
    }
}
