using TeamProject.DAL.Entities;
namespace BLL.Abstract
{
    public interface IAccountManager
    {
        //если я могу общатся с уровнем после и до меня то почему Visual studio не дает мне подключить зависимость
        //   User GetUser(LoginModel model);
        User GetUser(string name, string password);
        User GetUser(string email);
        User CreateUser(string email, string password);
        User GetUser(int token);
        void UpdateUser(User user);
        void SendEmailToUser(User user,string message);
    }
}
