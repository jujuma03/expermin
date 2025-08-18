using EXPERMIN.DATABASE.Data;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.Base.Implementations;
using EXPERMIN.REPOSITORY.Repositories.Portal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.REPOSITORY.Repositories.Portal.Implementations
{
    public class ProductRepository: Repository<Product>, IProductRepository
    {
        public ProductRepository(ExperminContext context) : base(context)
        {
        }
        public async Task<Product> GetProductDetail(Guid id)
            => await _context.Products.Include(x => x.MediaFile).Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}
