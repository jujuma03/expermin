using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.REPOSITORY.Repositories.Portal.Interfaces
{
    public interface ITestimonyRepository : IRepository<Testimony>
    {
        Task<Testimony> GetTestimonyDetail(Guid id);
    }
}
