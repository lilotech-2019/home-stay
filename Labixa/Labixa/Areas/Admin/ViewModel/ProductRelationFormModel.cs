using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Outsourcing.Data.Models;

namespace Labixa.Areas.Admin.ViewModel
{
    public class ProductRelationFormModel
    {
        [Key]
        public int Id { get; set; }

        [DisplayName(@"Sản Phẩm")]
        public int ProductId { get; set; }

        [DisplayName(@"Sản Phẩm Liên Quan")]
        public int? ProductId2 { get; set; }

        [DisplayName(@"Hiển Thị")]
        public bool IsAvailable { get; set; }

        public virtual Product ProductRelationship { get; set; }
        public virtual Product ProductRelated { get; set; }
    }
}