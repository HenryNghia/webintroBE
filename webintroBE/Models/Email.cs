using System.ComponentModel.DataAnnotations;

namespace webintroBE.Models
{
    public class Email
    {
        public int ID { get; set; }
        [MaxLength(100)]
        public required string Name { get; set; } = "";
        [MaxLength(100)]
        public string Surname { get; set; } = "";
        [MaxLength(100)]
        public required string Emailuser { get; set; } = "";
        [MaxLength]
        public required string Content { get; set; } = "";
        public DateTime CreatedAt { get; set; }
    }
}
