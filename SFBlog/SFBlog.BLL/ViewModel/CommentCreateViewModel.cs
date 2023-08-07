using SFBlog.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.BLL.ViewModel
{
    public class CommentCreateViewModel
    {
        [Required(ErrorMessage = "Поле комментарий обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Комментарий")]
        public string Text { get; set; }

        public User User { get; set; }
        public Guid PostId { get; set; }
        
    }
}
