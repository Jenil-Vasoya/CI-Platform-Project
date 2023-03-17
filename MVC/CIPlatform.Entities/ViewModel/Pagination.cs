using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModel
{
    public class Pagination
    {
        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }

        public long? TotalItems { get; set; }

        public int? TotalPages { get; set; }

        public int? ActivePage { get; set; }

        public List<MissionData> MissionDatas { get; set; }
    }
}
