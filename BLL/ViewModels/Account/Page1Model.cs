namespace BLL.ViewModels.Account
{
    public class Page1Model
    {
        public Page1Model() {
            LoginUser = new LoginModel { Name = "", Password = "" };
        }
        public LoginModel LoginUser { get; set; }
    }
}