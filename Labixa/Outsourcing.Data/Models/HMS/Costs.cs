using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outsourcing.Data.Models.HMS
{
    public enum CostType
    {
        Others = 0,
        Income = 1,
        Outcome = 2,
    }
    public class Costs : BaseEntity
    {
        public Costs()
        {
            DateCreated = DateTime.Now;
            DateLogged = DateTime.Now;
        }
        [Required]
        public string Name { get; set; }
        public double Amount { get; set; }
        public CostType Type { get; set; }
        public DateTime DateLogged { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }

        public int CostCategoryId { get; set; }
        [ForeignKey("CostCategoryId")]
        public virtual CostCategory CostCategory { get; set; }

    }
}
