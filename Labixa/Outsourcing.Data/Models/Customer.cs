using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Outsourcing.Data.Models
{
    public class Customer : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}