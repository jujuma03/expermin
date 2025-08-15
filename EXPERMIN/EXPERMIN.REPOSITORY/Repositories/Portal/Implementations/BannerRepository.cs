using EXPERMIN.DATABASE.Data;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.Base.Implementations;
using EXPERMIN.REPOSITORY.Repositories.Portal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.REPOSITORY.Repositories.Portal.Implementations
{
    public class BannerRepository : Repository<Banner>, IBannerRepository
    {
        public BannerRepository(ExperminContext context) : base(context)
        {
        }
    }
}
