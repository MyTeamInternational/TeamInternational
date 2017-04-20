using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject.DAL.Entities.Interfaces;

namespace TeamProject.DAL.Entities
{
    public class User : IEntity
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ConfirmedEmail { get; set; }
        public bool InRoles(string roles)
        {
            if (string.IsNullOrWhiteSpace(roles))
            {
                return false;
            }

            var rolesArray = roles.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var role in rolesArray)
            {
              //  var hasRole = UserRoles.Any(p => string.Compare(p.Role.Code, role, true) == 0);
                if (true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
