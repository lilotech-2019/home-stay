using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class Colors : BaseEntity
    {
        [Required][MaxLength(255)]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Note { get; set; }
        public bool isDelete { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
