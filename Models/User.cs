using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string?  UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Roles roles { get; set; } = Roles.User;
        public ICollection<Blog>? Blogs { get; set; }
    }
}