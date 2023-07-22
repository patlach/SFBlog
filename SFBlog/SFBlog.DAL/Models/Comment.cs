using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.DAL.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public DateTime CreatedDate { get; set; }
        public Post Post { get; set; }

    }
}
