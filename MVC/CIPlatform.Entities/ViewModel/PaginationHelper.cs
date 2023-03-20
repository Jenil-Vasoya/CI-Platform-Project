using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModel
{
    public static class PaginationHelper
    {
        public static PaginationResult<T> GetPagedData<T>(IQueryable<T> query, PaginationRequest paginationRequest)
        {
            var result = new PaginationResult<T>
            {
                PageNumber = paginationRequest.PageNumber,
                PageSize = paginationRequest.PageSize,
                TotalCount = query.Count(),
                TotalPages = (int)Math.Ceiling(query.Count() / (double)paginationRequest.PageSize),
                Results = query.Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize).Take(paginationRequest.PageSize).ToList()
            };
            return result;
        }
    }
}

