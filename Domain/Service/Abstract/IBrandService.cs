using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Service.Abstract
{
   public interface IBrandService
   {
       IEnumerable<Brand> GellAllBrands();
   }
}
