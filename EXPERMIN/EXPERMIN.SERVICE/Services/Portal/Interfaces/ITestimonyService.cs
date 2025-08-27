using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.Testimony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Services.Portal.Interfaces
{
    public interface ITestimonyService
    {
        Task<OperationDto<List<TestimonyDto>>> GetAllTestimonies(string userLoggedId);
        Task<OperationDto<List<TestimonyDto>>> GetAllTestimoniesActive();
        Task<OperationDto<TestimonyDto>> GetTestimony(string userLoggedId, Guid id);
        Task<OperationDto<TestimonyDto>> GetTestimonyActive(Guid id);
        Task<OperationDto<ResponseDto>> InsertTestimony(string userLoggedId, TestimonyRegisterDto model);
        Task<OperationDto<ResponseDto>> UpdateTestimony(string userLoggedId, Guid testimonyId, TestimonyUpdateDto model);
        Task<OperationDto<ResponseDto>> DeleteTestimony(string userLoggedId, Guid testimonyId);
    }
}
