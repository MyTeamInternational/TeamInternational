using System.ComponentModel.DataAnnotations;
//some comments
namespace BLL.ViewModels.Account
{
    public class LoginModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}