using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SomeWebShowroom.MVC.Services.Models
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Username is required")]
        //[MinLength(5, ErrorMessage = "Incorrect username or password")]
        //[MaxLength(20, ErrorMessage = "Incorrect username or password")]
        //[RegularExpression(@"^[a-zA-Z0-9_.-]*$", ErrorMessage = "Incorrect username or password")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        //[MinLength(8, ErrorMessage = "Incorrect username or password")]
        //[MaxLength(16, ErrorMessage = "Incorrect username or password")]
        public string Password { get; set; }
    }
}
