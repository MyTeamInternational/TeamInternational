using BLL.Abstract;

using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using TeamProject.DAL;
using TeamProject.DAL.Entities;
using TeamProject.DAL.Repositories;

namespace BLL.Managers
{
    public class AccountManager : IAccountManager
    {
        [Inject]
        ICinemaWork work;
        [Inject]
        IEmailService sender;

        public AccountManager(ICinemaWork work, IEmailService sender)
        {
            this.work = work;
            this.sender = sender;
        }
        public AccountManager(ICinemaWork work)
        {
            this.work = work;
        }
        public User CreateUser(string email, string password)
        {
            IEnumerable<char> cutName = email.TakeWhile(e => e != '@');
            User user = new User { Email = email, ConfirmedEmail = false, Name = new String(cutName.ToList().ToArray()), Password = password, Views = null };
            work.Users.Create(user);
            work.Save();
            return user;
        }

        public User GetUser(string input, string password)
        {
            if (work.Users.Items.Select(e => (e.Name == input || e.Email == input) && e.Password == password).Count(e => e == true) > 1)
            {
                throw new NotSupportedException();//when two equals user finded
            }
            //does not recorgonize sql to 
            return work.Users.Items.FirstOrDefault(e => ((e.Name == input || e.Email == input) && e.Password == password));//can throw an Exeprion if in db will ne 2 same users it is incorrect 
        }
        public User GetUser(string input)
        {
            return work.Users.Items.FirstOrDefault(e => e.Email == input || e.Name == input);
        }
        public User GetUser(int token)
        {
            return work.Users.Items.FirstOrDefault(e => e.ID == (token));
        }
        public void SendEmailToUser(User user, string message = "")
        {
            sender.Send(user.Email, message);
        }
        public void UpdateUser(User user)
        {
            work.Users.Update(user);
            work.Save();
        }
    }
}
