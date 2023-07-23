﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBlog.BLL.ViewModel
{
    public class TagViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
