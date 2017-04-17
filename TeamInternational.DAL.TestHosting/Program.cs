using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamInternational.DAL.Entities;
using TeamInternational.DAL.Interfaces;

namespace TeamInternational.DAL.TestHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            ICinemaWork work = new CinemaWork();

            // After create we get entity with ID.
            var kokorin = work.Users.Create(new User() { Name = "Kokorin", Email = "Kokorin1506@gmail.com", Password="1234567" });
            var poymanov = work.Users.Create(new User() { Name = "Poymanov", Email = "YYYY@mail.ru", Password = "1234567" });
            var pavlenko = work.Users.Create(new User() { Name = "Pavlenko", Email = "XXXX@mail.ru", Password = "1234567" });

            Console.WriteLine($"Kokorin - {kokorin.ID}");
            Console.WriteLine($"Poymanov - {poymanov.ID}");
            Console.WriteLine($"Pavlenko - {pavlenko.ID}");

            Console.WriteLine();

            var query = work.Users.Where(x => x.Name[0] == 'P').ToList();
            query.ForEach(item => Console.WriteLine(item.Name));

            work.Users.Remove(kokorin);
            work.Users.Remove(poymanov);
            work.Users.Remove(pavlenko);

        }
    }
}
