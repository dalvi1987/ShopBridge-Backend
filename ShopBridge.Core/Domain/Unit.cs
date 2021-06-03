using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridge.Core
{
    [Table("mstUnit", Schema = "dbo")]
    public class Unit : EntityBase<short>
    {       
        public string UnitName { get; set; }

    }
}
