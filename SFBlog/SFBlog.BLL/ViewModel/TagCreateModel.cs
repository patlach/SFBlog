using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.BLL.ViewModel
{
    public class TagCreateViewModel
    {
        [Required(ErrorMessage = "Укажите название тега")]
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Название")]
        public string Name { get; set; }
    }
}
