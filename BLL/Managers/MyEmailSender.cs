using BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Managers
{
    public class MyEmailSender: IEmailService
    {
        public void Send(string userEmail,string message)
        {
            //:TODO - можно сделать настройку из xml
            MailAddress from = new MailAddress("pauluxxx@mail.ru", "Web Registration");
            // кому отправляем
            MailAddress to = new MailAddress(userEmail);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = "Email confirmation";
            // текст письма - включаем в него ссылку
            m.Body = message;
            m.IsBodyHtml = true;
            // адрес smtp-сервера, с которого мы и будем отправлять письмо
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 2525);
            // логин и пароль
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("pauluxxx@mail.ru", "5898044p");
            smtp.Send(m);
        }
    }
}
