using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Outsourcing.Data.Models
{
    public class Vendors :BaseEntity
    {
        [Required][MaxLength(255)]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
        [Range(0, 100)]
        public int Percent { get; set; }
        public bool IsDelete { get; set; }
       
        public Double Price { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
