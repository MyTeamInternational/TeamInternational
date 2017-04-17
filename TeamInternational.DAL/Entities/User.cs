using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamInternational.DAL.Entities.Interfaces;

namespace TeamInternational.DAL.Entities
{
    public class User : IEntity
    {
        [Key]
        public int ID { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ConfirmedEmail { get; set; }
    }
}
