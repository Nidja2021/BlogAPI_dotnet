using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Models
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        // public List<string>? Tags { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}