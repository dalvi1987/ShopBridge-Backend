using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridge.Core
{
    public interface IEntityBase<TKey>
    {
        TKey Id { get; set; }
    }

    public interface IAuditEntity
    {
        DateTime CreatedDate { get; set; }
        int CreatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
        int? UpdatedBy { get; set; }
    }

    public abstract class EntityBase<TKey> : IEntityBase<TKey>, IAuditEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
