using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.Collaborator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Services.Portal.Interfaces
{
    public interface ICollaboratorService
    {
        Task<OperationDto<List<CollaboratorDto>>> GetAllCollaborators(string userLoggedId);
        Task<OperationDto<List<CollaboratorDto>>> GetAllCollaboratorsActive();
        Task<OperationDto<CollaboratorDto>> GetCollaborator(string userLoggedId, Guid id);
        Task<OperationDto<CollaboratorDto>> GetCollaboratorActive(Guid id);
        Task<OperationDto<ResponseDto>> InsertCollaborator(string userLoggedId, CollaboratorRegisterDto model);
        Task<OperationDto<ResponseDto>> UpdateCollaborator(string userLoggedId, Guid collaboratorId, CollaboratorUpdateDto model);
        Task<OperationDto<ResponseDto>> DeleteCollaborator(string userLoggedId, Guid collaboratorId);
    }
}
