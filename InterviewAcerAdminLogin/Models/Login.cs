using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InterviewAcerAdminLogin.Models
{
    public class Login
    {
        [DataType(DataType.EmailAddress)]
        [Required]      
        [Display(Name = "Email Id")]  
        public string LoginUserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [MinLength(8)]
        [Display(Name = "Password")]
        public string LoginPassword { get; set; }
    }
}