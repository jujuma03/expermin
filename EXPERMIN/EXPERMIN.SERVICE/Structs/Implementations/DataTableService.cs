using EXPERMIN.CORE.Helpers;
using EXPERMIN.SERIVICE.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EXPERMIN.SERIVICE.Structs.DataTablesStructs;

namespace EXPERMIN.SERIVICE.Services.Implementations
{
    public class DataTableService : IDataTableService
    {
        public IHttpContextAccessor _httpContextAccessor;

        public DataTableService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

        }

        public int GetDrawCounter()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            int.TryParse(request.Query[ConstantHelpers.DATATABLE.SERVER_SIDE.SENT_PARAMETERS.DRAW_COUNTER], out int result);
            return result;
        }

        public string GetOrderColumn()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            return request.Query.ContainsKey(ConstantHelpers.DATATABLE.SERVER_SIDE.SENT_PARAMETERS.ORDER_COLUMN) ?
                request.Query[ConstantHelpers.DATATABLE.SERVER_SIDE.SENT_PARAMETERS.ORDER_COLUMN].ToString() :
                null;
        }

        public string GetOrderDirection()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            return request.Query.ContainsKey(ConstantHelpers.DATATABLE.SERVER_SIDE.SENT_PARAMETERS.ORDER_DIRECTION) ?
                request.Query[ConstantHelpers.DATATABLE.SERVER_SIDE.SENT_PARAMETERS.ORDER_DIRECTION].ToString().ToUpper() :
                ConstantHelpers.DATATABLE.SERVER_SIDE.DEFAULT.ORDER_DIRECTION;
        }

        public int GetPagingFirstRecord()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            int.TryParse(request.Query[ConstantHelpers.DATATABLE.SERVER_SIDE.SENT_PARAMETERS.PAGING_FIRST_RECORD], out int result);
            return result;
        }

        public int GetRecordsPerDraw()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            int.TryParse(request.Query[ConstantHelpers.DATATABLE.SERVER_SIDE.SENT_PARAMETERS.RECORDS_PER_DRAW], out int result);
            return result;
        }

        public string GetSearchValue()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            return request.Query[ConstantHelpers.DATATABLE.SERVER_SIDE.SENT_PARAMETERS.SEARCH_VALUE];
        }

        public SentDataTableParameters GetSentDataTableParameters()
        {
            return new SentDataTableParameters
            {
                DrawCounter = GetDrawCounter(),
                OrderColumn = GetOrderColumn(),
                OrderDirection = GetOrderDirection(),
                PagingFirstRecord = GetPagingFirstRecord(),
                RecordsPerDraw = GetRecordsPerDraw()
            };
        }

        public object GetPaginationObject<T>(int recordsFiltered, IEnumerable<T> data)
        {
            return new ReturnedData<T>
            {
                Data = data,
                DrawCounter = GetDrawCounter(),
                RecordsFiltered = recordsFiltered,
                TotalRecords = recordsFiltered
            };
        }

    }
}
