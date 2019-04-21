using System.ComponentModel.DataAnnotations;

namespace Outsourcing.Data.Models
{
    public class ContactUs : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public ContactUsType Type { get; set; }
        public string Note { get; set; }
        public double Price { get; set; }
    }

    public enum ContactUsType
    {
        New = 0,
        InProgress = 1,
        Replied = 2,

    }
}