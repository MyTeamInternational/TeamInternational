namespace CONSTANTS
{
    public static class Constans_Cinema
    {
        public const string HOME_CONTROLLER = "Home";
        public const string MOVIE_CONTROLLER = "Movie";
        public const string ACCOUNT_CONTROLLER = "Account";
        public const string LAST_PAGE_CONTROLLER = "LastPage";

        public const string ACCOUNT_CONFIRM = "Confirm";
        public const string ACCOUNT_LOGOUT = "LogOut";
        public const string ACCOUNT_REGISTRATION = "Register";

        public const string HOME_INDEX = "Page1";
        public const string HOME_PAGE2 = "Page2";

        public const string MOVIE_INDEX = HOME_PAGE2;
        public const string MOVIE_UPDATE = "Update";
        public const string MOVIE_EDIT = "Page3";

        public const string LAST_PAGE_INDEX = "Page4";


        public static string DEFAULT_ROUTE = "Default";
        public static string STANDARD_LAYOUT = "~/Views/Shared/_Layout.cshtml";

    }
    public enum MyStatusFlow
    {
        Registred,
        Not_Registred,
        Smile
    }
    public static class MyStatusFlowExtention
    {
        public static MyStatusFlow ParseUserAuth(this MyStatusFlow flow, bool authificated)
        {
            return (authificated) ? MyStatusFlow.Registred : MyStatusFlow.Not_Registred;
        }
    }
    public static class Regex_Constants
    {
        public const string EMAIL = "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$";
        public const string PWD = "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=])(?=\\S+$).{6,}$";
        public const string SimplePWD = "(\\w+){4}";
    }

}