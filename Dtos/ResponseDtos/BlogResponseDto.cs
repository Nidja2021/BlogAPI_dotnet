using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Dtos.ResponseDtos
{
    public class BlogResponseDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}