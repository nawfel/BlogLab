using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogLab.Models
{
    public class ApplicationUserCreate : ApplicationUserLogin
    {
        [MinLength(10, ErrorMessage = "must be at least 10 characters")]
        [MaxLength(50, ErrorMessage = "must be at most 10 characters")]
        public string Fullname { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress( ErrorMessage = "invalid Email format")]
        [MaxLength(30, ErrorMessage = "must be at most 10 characters")]
        public string Email { get; set; }

    }
}
