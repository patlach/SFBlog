using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.BLL.ViewModel
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid UserID { get; set; }
        public List<Guid> TagIds { get; set; }
        public List<Guid> CommentIds { get; set; }
    }
}
