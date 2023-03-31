using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Entities.ViewModel
{
    public class ImgUploadForStoryAdd
    {
        public List<IFormFile>? files { get; set; }
        public long StoryId { get; set; }
        public string? GetUrlPath { get; set; }

    }
}
