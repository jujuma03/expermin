using EXPERMIN.ENTITIES.Models;
using EXPERMIN.SERIVICE.Structs;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.Banner;
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
        Task<OperationDto<List<BannerDto>>> GetAllBannersActive();
        Task<OperationDto<BannerDto>> GetBanner(Guid id);
        Task<OperationDto<bool>> InsertBanner(Banner banner);
        Task<OperationDto<bool>> DeleteBanner(Banner banner);
        Task<OperationDto<bool>> UpdateBanner(Banner banner);
    }
}
