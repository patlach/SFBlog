using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.BLL.ViewModel
{
    public class CommentCreateViewModel
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public Guid AuthorId { get; set; }
        public Guid PostId { get; set; }
        
    }
}
