using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Dtos.RequestDtos
{
    public class BlogRequestDto
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}