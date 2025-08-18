using EXPERMIN.ENTITIES.Models;
using EXPERMIN.SERIVICE.Structs;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.Banner;
using EXPERMIN.SERVICE.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Services.Portal.Interfaces
{
    public interface IBannerService
    {
        Task<OperationDto<DataTablesStructs.ReturnedData<BannerDto>>> GetAllBannerDatatable(DataTablesStructs.SentDataTableParameters sentParameters, string headline = null, byte? status = null);
        Task<OperationDto<IEnumerable<KeyValuePair<int, string>>>> GetAvailableOrdersAndListSequenceOrder();
        Task<OperationDto<List<BannerDto>>> GetAllBanners(string userLoggedId);
        Task<OperationDto<List<BannerDto>>> GetAllBannersActive();
        Task<OperationDto<BannerDto>> GetBanner(string userLoggedId, Guid id);
        Task<OperationDto<BannerDto>> GetBannerActive(Guid id);
        Task<OperationDto<ResponseDto>> InsertBanner(string userLoggedId, BannerRegisterDto model);
        Task<OperationDto<ResponseDto>> UpdateBanner(string userLoggedId, Guid bannerId, BannerUpdateDto model);
        Task<OperationDto<ResponseDto>> DeleteBanner(string userLoggedId, Guid bannerId);
    }
}
