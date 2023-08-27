using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.BLL.ViewModel
{
    public class TagEditViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Поле \"Имя тега\" обязательно для заполнения")]
        [Display(Name = "Имя тега")]
        public string Name { get; set; }
    }
}
