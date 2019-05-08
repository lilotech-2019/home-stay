using System.ComponentModel.DataAnnotations;

namespace Outsourcing.Data.Models
{
    public class Message : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        [Display(Name = "Subject")]
        public string Name { get; set; }
        public string Email { get; set; }

        [DataType(DataType.MultilineText)]
        public string Answer { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public MessageType Type { get; set; }
        public string Note { get; set; }

        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
    }

    public enum MessageType
    {
        New = 0,
        Replied = 1
    }
}