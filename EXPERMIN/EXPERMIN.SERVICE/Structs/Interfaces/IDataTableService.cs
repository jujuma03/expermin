using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EXPERMIN.SERIVICE.Structs.DataTablesStructs;

namespace EXPERMIN.SERIVICE.Services.Interfaces
{
    public interface IDataTableService
    {
        int GetDrawCounter();
        string GetOrderColumn();
        string GetOrderDirection();
        int GetPagingFirstRecord();
        int GetRecordsPerDraw();
        string GetSearchValue();
        SentDataTableParameters GetSentDataTableParameters();
        object GetPaginationObject<T>(int recordsFiltered, IEnumerable<T> data);
    }
}
