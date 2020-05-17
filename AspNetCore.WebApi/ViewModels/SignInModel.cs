using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApi.WebApi.ViewModels
{
    public class SignInModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }    
    }
}
