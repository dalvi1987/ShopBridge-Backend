using System;
using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Core.DTO
{
    public class ProductDTO : IEntityBase<int>
    {
      
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(0, Int16.MaxValue)]
        public short UnitsInStock { get; set; }

        [Required]
        public bool Discontinued { get; set; } = false;

        [Required]
        public int UnitId { get; set; }

    }
}
