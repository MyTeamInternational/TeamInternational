using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject.DAL;
using Moq;
using TeamProject.DAL.Entities;
using System.Text.RegularExpressions;

namespace BLL.Managers.Tests
{
    [TestClass()]
    public class MovieManagerTests
    {

        Mock<ICinemaWork> fakeWork = new Mock<ICinemaWork>();
        [TestMethod()]
        public void GetMoviesTake5AllHaveTest()
        {
            fakeWork.Setup(m => m.Movies.Items).Returns(new List<Movie> { new Movie { ID = 1 }, new Movie { ID = 2 }, new Movie { ID = 3 }, new Movie { ID = 4 }, new Movie { ID = 5 }, new Movie { ID = 6 } }.AsQueryable());
            MovieManager manager = new MovieManager(fakeWork.Object);
            var res = manager.GetMovies(5);
            Assert.AreEqual(5, res.Count());
        }
        [TestMethod()]
        public void GetMoviesTakeLessThanHaveTest()
        {
            fakeWork.Setup(m => m.Movies.Items).Returns(new List<Movie> { new Movie { ID = 1 }, new Movie { ID = 2 }, new Movie { ID = 3 }, new Movie { ID = 4 }, new Movie { ID = 5 }, new Movie { ID = 6 } }.AsQueryable());
            MovieManager manager = new MovieManager(fakeWork.Object);
            var res = manager.GetMovies(10);
            Assert.AreEqual(6, res.Count());
        }
        [TestMethod()]
        public void GetMoviesTakeEmptyListTest()
        {
            fakeWork.Setup(m => m.Movies.Items).Returns(new List<Movie> { }.AsQueryable());
            MovieManager manager = new MovieManager(fakeWork.Object);
            var res = manager.GetMovies(5);
            Assert.AreEqual(0, res.Count());
        }
        [TestMethod()]
        public void GetMoviesRegExpxTakeA()
        {
            fakeWork.Setup(m => m.Movies.Items).Returns(new List<Movie> { new Movie { Name = "Abrio" }, new Movie { Name = "Baria" } }.AsQueryable());
            MovieManager manager = new MovieManager(fakeWork.Object);
            string target = "Abrio";
            string name = "^a[\\w]*";
            Regex regex = new Regex(name, RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(target);
            if (matches.Count > 0)
            {
                foreach (System.Text.RegularExpressions.Match match in matches)
                    Console.WriteLine(match.Value);
            }
            else
            {
                Console.WriteLine("Совпадений не найдено");
            }
            target = "Baria";
            matches = regex.Matches(target);
            if (matches.Count > 0)
            {
                foreach (System.Text.RegularExpressions.Match match in matches)
                    Console.WriteLine(match.Value);
            }
            else
            {
                Console.WriteLine("Совпадений не найдено");
            }

            var res = manager.GetMovies(name);

            //Assert.AreEqual(0, res);
        }
        [TestMethod()]
        public void GetMoviesRegExpxLetter()
        {
            fakeWork.Setup(m => m.Movies.Items).Returns(new List<Movie> { new Movie { Name = "Abrio" }, new Movie { Name = "Baria" },new Movie {Name="Abakan" } }.AsQueryable());
            MovieManager manager = new MovieManager(fakeWork.Object);
            string name = "A";
            var resList = manager.GetMovies(name);
            Assert.AreEqual(2,resList.Count());
        }
        [TestMethod()]
        public void GetMoviesRegExpxWord()
        {
            fakeWork.Setup(m => m.Movies.Items).Returns(new List<Movie> { new Movie { Name = "Abrio" }, new Movie { Name = "Baria" }, new Movie { Name = "Abakan" } }.AsQueryable());
            MovieManager manager = new MovieManager(fakeWork.Object);
            string name = "Abr";
            var resList = manager.GetMovies(name);
            Assert.AreEqual(1, resList.Count());
        }
        [TestMethod()]
        public void GetMoviesRegExpxNoCorrect()
        {
            fakeWork.Setup(m => m.Movies.Items).Returns(new List<Movie> { new Movie { Name = "Abrio" }, new Movie { Name = "Baria" }, new Movie { Name = "Abakan" } }.AsQueryable());
            MovieManager manager = new MovieManager(fakeWork.Object);
            string name = "br";
            var resList = manager.GetMovies(name);
            Assert.AreEqual(0, resList.Count());
        }
        [TestMethod()]
        public void GetMoviesRegExpxComplainWord()
        {
            fakeWork.Setup(m => m.Movies.Items).Returns(new List<Movie> { new Movie { Name = "Abrio Curia" }, new Movie { Name = "Baria" }, new Movie { Name = "Abakan Kils" } }.AsQueryable());
            MovieManager manager = new MovieManager(fakeWork.Object);
            string name = "Cu";
            var resList = manager.GetMovies(name);
            Assert.AreEqual(1, resList.Count());
        }

    }
}