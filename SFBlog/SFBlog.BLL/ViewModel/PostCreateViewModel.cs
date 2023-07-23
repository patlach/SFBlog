using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.BLL.ViewModel
{
    public class PostCreateViewModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public Guid Author { get; set; }
        public List<TagViewModel> Tags { get; set; }
    }
}
