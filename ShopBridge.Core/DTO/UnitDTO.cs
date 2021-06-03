using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Core.DTO
{
    public class UnitDTO: IEntityBase<short>
    {
        public short Id { get; set; }
        public string UnitName { get; set; }        
    }
}
