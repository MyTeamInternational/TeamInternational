namespace CONSTANTS
{
    public static class Constans_Cinema
    {
        public readonly static string ACCOUNT_CONTROLLER = "Account";
        public readonly static string ACCOUNT_CONFIRM = "Confirm";
        public readonly static string HOME_CONTROLLER = "Home";
        public readonly static string HOME_INDEX = "Page1";
        public readonly static string HOME_PAGE2 = "Page2";
        public readonly static string MOVIE_CONTROLLER = "Movie";
        public readonly static string MOVIE_INDEX = HOME_PAGE2;
        public static readonly string ACCOUNT_LOGOUT = "LogOut";
        public static readonly string ACCOUNT_REGISTRATION = "Register";
    }
    public enum MyStatusFlow {
        Registred,
        Not_Registred,
        Smile
    }
    public static class MyStatusFlowExtention {
        public static MyStatusFlow ParseUserAuth(this MyStatusFlow flow,bool authificated) {
            return (authificated) ? MyStatusFlow.Registred : MyStatusFlow.Not_Registred;
        }
    }

}