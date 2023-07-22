using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.BLL.ViewModel
{
    public class RoleEditViewModel
    {
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Название")]
        public string? Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Описание роли", Prompt = "Описание")]
        public string? Description { get; set; } = null;
    }
}
