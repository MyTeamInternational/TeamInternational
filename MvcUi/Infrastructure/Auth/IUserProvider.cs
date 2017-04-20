using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TeamProject.DAL.Entities;

namespace MvcUi.Infrastructure.Auth
{
    public interface IUserProvider
    {
        User User { get; set; }
    }
}