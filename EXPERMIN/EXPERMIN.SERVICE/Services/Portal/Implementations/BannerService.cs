using EXPERMIN.CORE.Extensions;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.Portal.Interfaces;
using EXPERMIN.SERIVICE.Structs;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.Banner;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Services.Portal.Implementations
{
    public class BannerService : IBannerService
    {
        public readonly IBannerRepository _bannerRepository;
        public BannerService(IBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }
        public async Task<OperationDto<DataTablesStructs.ReturnedData<BannerDto>>> GetAllBannerDatatable(DataTablesStructs.SentDataTableParameters sentParameters, string headline = null, byte? status = null)
        {
            var totalRecords = await _bannerRepository.GetAsQueryable().CountAsync();

            //validar si existe el banner
            if (totalRecords == 0)
                return new OperationDto<DataTablesStructs.ReturnedData<BannerDto>>(OperationCodeDto.DoesNotExist, "No existen Banners registrados.");

            var query = _bannerRepository.GetAsQueryable();

            if (!string.IsNullOrEmpty(headline))
                query = query.Where(q => q.Headline.Contains(headline));

            if (status.HasValue && status.Value != 0)
                query = query.Where(q => q.Status == status.Value);

            var recordsFiltered = await query.CountAsync();

            var data = await query
                .Skip(sentParameters.PagingFirstRecord)
                .Take(sentParameters.RecordsPerDraw)
                .Select(x => new BannerDto
                {
                    Id = x.Id.ToString(),
                    Headline = x.Headline,
                    UrlImage = x.UrlImage,
                    PublicationDate = x.PublicationDate.ToLocalDateFormat(),
                    Status = ConstantHelpers.BANNER.STATUS.VALUES[x.Status],
                    SequenceOrder = (x.SequenceOrder.HasValue && x.SequenceOrder.Value != 0) ? ConstantHelpers.SEQUENCE_ORDER.VALUES[x.SequenceOrder.Value] : "SIN ORDEN",
                    SequenceOrderId = x.SequenceOrder ?? 0
                }).ToListAsync();

            var result = new DataTablesStructs.ReturnedData<BannerDto>
            {
                Data = data,
                DrawCounter = sentParameters.DrawCounter,
                RecordsFiltered = recordsFiltered,
                TotalRecords = totalRecords
            };

            return new OperationDto<DataTablesStructs.ReturnedData<BannerDto>>(result);
        }
        public async Task<OperationDto<IEnumerable<KeyValuePair<int, string>>>> GetAvailableOrdersAndListSequenceOrder()
        {
            // Obtengo todos los SequenceOrder existentes en BD (solo los que no son null)
            var usedOrders = await _bannerRepository
                .GetAsQueryable()
                .Where(b => b.SequenceOrder.HasValue)
                .Select(b => (int)b.SequenceOrder.Value)
                .ToListAsync();

            var availableOrders = ConstantHelpers.SEQUENCE_ORDER.VALUES
                .Where(x => !usedOrders.Contains(x.Key));

            if (!availableOrders.Any())
                return new OperationDto<IEnumerable<KeyValuePair<int, string>>>(OperationCodeDto.DoesNotExist, "No hay orden disponible.");

            return new OperationDto<IEnumerable<KeyValuePair<int, string>>>(availableOrders);
        }
        public async Task<OperationDto<List<BannerDto>>> GetAllBannersActive()
        {
            var banners = await _bannerRepository
                .GetAsQueryable()
                .AsNoTracking()
                .Where(x => x.Status == ConstantHelpers.BANNER.STATUS.ACTIVE)
                .Select(x => new BannerDto
                {
                    Id = x.Id.ToString(),
                    Headline = x.Headline,
                    UrlImage = x.UrlImage,
                    PublicationDate = x.PublicationDate.ToLocalDateFormat(),
                    Status = ConstantHelpers.BANNER.STATUS.VALUES[x.Status],
                    SequenceOrder = (x.SequenceOrder.HasValue && x.SequenceOrder.Value != 0)
                ? ConstantHelpers.SEQUENCE_ORDER.VALUES[x.SequenceOrder.Value]
                : "SIN ORDEN",
                    SequenceOrderId = x.SequenceOrder ?? 0
                })
                .ToListAsync();

            if (!banners.Any())
                return new OperationDto<List<BannerDto>>(OperationCodeDto.DoesNotExist, "No hay banners activos.");

            return new OperationDto<List<BannerDto>>(banners);
        }
        public async Task<OperationDto<BannerDto>> GetBanner(Guid id)
        {
            if (id == Guid.Empty)
                return new OperationDto<BannerDto>(OperationCodeDto.InvalidParameters, "El ID es inválido.");

            var banner = await _bannerRepository.Get(id);

            if (banner == null)
                return new OperationDto<BannerDto>(OperationCodeDto.DoesNotExist, "El banner no existe.");

            var dto = new BannerDto
            {
                Id = banner.Id.ToString(),
                Headline = banner.Headline,
                UrlImage = banner.UrlImage,
                PublicationDate = banner.PublicationDate.ToLocalDateFormat(),
                Status = ConstantHelpers.BANNER.STATUS.VALUES[banner.Status],
                SequenceOrder = (banner.SequenceOrder.HasValue && banner.SequenceOrder.Value != 0)
            ? ConstantHelpers.SEQUENCE_ORDER.VALUES[banner.SequenceOrder.Value]
            : "SIN ORDEN",
                SequenceOrderId = banner.SequenceOrder ?? 0
            };

            return new OperationDto<BannerDto>(dto);
        }
        public async Task<OperationDto<bool>> InsertBanner(Banner banner)
        {
            if (banner == null)
                return new OperationDto<bool>(OperationCodeDto.InvalidParameters, "El banner no puede ser nulo.");

            await _bannerRepository.Insert(banner);
            await _bannerRepository.SaveChangesAsync();

            return new OperationDto<bool>(true, "Banner insertado correctamente.");
        }
        public async Task<OperationDto<bool>> DeleteBanner(Banner banner)
        {
            if (banner == null)
                return new OperationDto<bool>(OperationCodeDto.InvalidParameters, "El banner no puede ser nulo.");

            await _bannerRepository.Delete(banner);
            await _bannerRepository.SaveChangesAsync();

            return new OperationDto<bool>(true, "Banner eliminado correctamente.");
        }
        public async Task<OperationDto<bool>> UpdateBanner(Banner banner)
        {
            if (banner == null)
                return new OperationDto<bool>(OperationCodeDto.InvalidParameters, "El banner no puede ser nulo.");

            await _bannerRepository.Update(banner);
            await _bannerRepository.SaveChangesAsync();

            return new OperationDto<bool>(true, "Banner actualizado correctamente.");
        }
    }
}
