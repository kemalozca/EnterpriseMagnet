using EnterpriseMagnet.Dto.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseMagnet.Dto.Abtract
{
    public class ProductWithCategoryDto:ProductDto 
    {
        public CategoryDto Category { get; set; }
    }
}
