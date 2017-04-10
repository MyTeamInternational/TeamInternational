namespace BLL.ViewModels.Account
{
    public class Page1Model
    {
        public bool isAutorized { get;  set; }
        public string UserName { get;  set; }
        public Page1Model() {
            isAutorized = false;
            UserName = null;
        }
    }
}