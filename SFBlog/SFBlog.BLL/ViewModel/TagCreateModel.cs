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
        [Required(ErrorMessage = "Поле \"Имя тега\" обязательно для заполнения")]
        [Display(Name = "Имя тега")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        public bool IsSelected { get; set; }
    }
}
