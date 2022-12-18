using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogLab.Models
{
    public class ApplicationUserLogin
    {
        [Required(ErrorMessage ="Username is required")]
        [MinLength(5,ErrorMessage ="must be at least 5 characters")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [MinLength(10, ErrorMessage = "must be at least 10 characters")]
        [MaxLength(50, ErrorMessage = "must be at most 10 characters")]

        public string Password { get; set; }

    }
}
