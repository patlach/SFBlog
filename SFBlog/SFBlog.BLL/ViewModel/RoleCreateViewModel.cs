using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.BLL.ViewModel
{
    public class RoleCreateViewModel
    {
        [Required(ErrorMessage = "Поле \"Имя роли\" обязательно для заполнения")]
        [Display(Name = "Имя роли")]
        [DataType(DataType.Text)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Поле \"Описание роли\" обязательно для заполнения")]
        [Display(Name = "Описание роли")]
        [DataType(DataType.Text)]
        public string? Description { get; set; }
    }
}
