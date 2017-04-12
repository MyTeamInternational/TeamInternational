using CONSTANTS;
using System.ComponentModel.DataAnnotations;
namespace BLL.ViewModels.Account

{
    public class RegisterModel
    {
        [Required]
        [RegularExpression (Regex_Constants.EMAIL,ErrorMessage ="Email имеет не верный формат")]
        public string Email { get; set; }

        [Required]
        [RegularExpression (Regex_Constants.SimplePWD ,ErrorMessage ="Слишком простой пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }



    }
}