using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridge.Core
{
    [Table("mstProduct", Schema = "dbo")]
    public class Product : EntityBase<int>
    {

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public short UnitsInStock { get; set; }

        [Required]
        public bool Discontinued { get; set; } = false;

        [Required]
        [ForeignKey("Unit")]
        public short UnitId { get; set; }

        public virtual Unit Unit { get; set; }

    }
}
