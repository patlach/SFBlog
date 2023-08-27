using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.BLL.ViewModel
{
    public class PostCreateViewModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Поле \"Заголовок\" обязательно для заполнения")]
        [Display(Name = "Заголовок")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле \"Текст\" обязательно для заполнения")]
        [Display(Name = "Текст")]
        [DataType(DataType.Text)]
        public string Text { get; set; }

        public string Author { get; set; }

        [Display(Name = "Теги")]
        public List<TagViewModel> Tags { get; set; }
    }
}
