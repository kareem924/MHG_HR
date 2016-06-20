using Domain.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Service
{
    public class BrandService : IBrandService
    {
        private readonly EfRepository<Brand> _brandRepository;
        public BrandService(EfRepository<Brand> brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public IEnumerable<Brand> GellAllBrands()
        {
            return _brandRepository.Table;
        }
    }
}
