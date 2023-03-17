using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModel
{
    public class PaginationRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public string SearchValue { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }


    public class PaginationResult<T>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public int? TotalCount { get; set; }
        public int? TotalPages { get; set; }
        public List<T> Results { get; set; }
        //public string QuerySearch { get; set; }
    }
}
