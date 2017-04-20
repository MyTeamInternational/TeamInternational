using CONSTANTS;
using MvcUi.Controllers;
using System.Security.Principal;
using TeamProject.DAL.Entities;
using TeamProject.DAL.Repositories;

namespace MvcUi.Infrastructure.Auth
{
    public class UserIndentity : IIdentity, IUserProvider
        /// current user
    {
        public User User { get; set; }

        public string AuthenticationType
        {
            get
            {
                return typeof(User).ToString();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return User != null;
            }
        }

        public string Name
        {
            get
            {
                if (User != null)
                {
                    return User.Email;
                }
                //иначе аноним
                return "anonym";
            }
        }

        public void Init(string email, UserRepository repository)
        {
            if (!string.IsNullOrEmpty(email))
            {               
                User = repository.GetByEmail(email);
            }
        }
    }
}