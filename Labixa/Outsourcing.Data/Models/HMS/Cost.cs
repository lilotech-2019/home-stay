using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsourcing.Data.Models.HMS
{
    public class Cost : BaseEntity
    {
        public Cost()
        {
            DateCreated = DateTime.Now;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public string Note { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public Double Price { get; set; }
        public double OrginalPrice { get; set; }

        public DateTime DateEnd { get; set; }
        public bool IsImport { get; set; }
        public bool IsDelete { get; set; }

        public int CostCategoryId { get; set; }

        [ForeignKey("CostCategoryId")]
        public virtual CostCategory CostCategory { get; set; }

        public virtual ICollection<CostOrderItem> CostOrderItem { get; set; }
    }
}