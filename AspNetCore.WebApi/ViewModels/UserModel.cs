using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreApi.WebApi.ViewModels
{
    public class UserModel
    {
        [Required(ErrorMessage = "Please provide your name")]
        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide an email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "a password is required with every account")]
        [DataType(dataType: DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "a password is required with every account")]
        [DataType(dataType: DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
