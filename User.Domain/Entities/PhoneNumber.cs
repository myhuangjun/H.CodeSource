using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Entities
{
    /// <summary>
    /// 电话号码
    /// </summary>
    /// <param name="RegionCode"></param>
    /// <param name="Number"></param>
    public record PhoneNumber(int RegionCode,string Number);    
    
}
