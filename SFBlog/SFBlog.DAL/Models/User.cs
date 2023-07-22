using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SFBlog.DAL.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public List<Post> Posts { get; set; }
        public List<Role> Roles { get; set; }
    }
}
