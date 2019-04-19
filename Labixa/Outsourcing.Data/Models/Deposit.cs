using System.ComponentModel.DataAnnotations;

namespace Outsourcing.Data.Models
{
    public class Deposit : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public string Email { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
    }
}