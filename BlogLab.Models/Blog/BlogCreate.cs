using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogLab.Models.Blog
{
    public class BlogCreate
    {
        public int BlogId { get; set; }
        
        [Required(ErrorMessage = "Title is required")]
        [MinLength(10,ErrorMessage = "must be at least 10 characters")]
        [MaxLength(50, ErrorMessage = "must be at most 50 characters")]
        public string Title { get; set; }
        [Required(ErrorMessage = "content is required")]
        [MinLength(200, ErrorMessage = "must be at least 200 characters")]        
        public string Content { get; set; }

        public int? PhotoId { get; set; }
    }
}
