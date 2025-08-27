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
    public class TestimonyRepository : Repository<Testimony>, ITestimonyRepository
    {
        public TestimonyRepository(ExperminContext context) : base(context)
        {
        }
        public async Task<Testimony> GetTestimonyDetail(Guid id)
            => await _context.Testimonies.Include(x => x.MediaFile).Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}
