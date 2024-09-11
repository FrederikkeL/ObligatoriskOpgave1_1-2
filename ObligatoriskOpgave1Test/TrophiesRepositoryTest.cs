using ObligatoriskOpgave1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatoriskOpgave1Test
{
    [TestClass]
    public class TrophiesRepositoryTest
    {

        private TrophiesRepository repo = new();
        private readonly Trophy goodTrophy = new Trophy() { Id = 1, Competition = "nam", Year = 1980 };
        private readonly Trophy BadCompTrophy = new Trophy() { Id = 2, Competition = "na", Year = 1980 };
        private readonly Trophy NullCompTrophy = new Trophy() { Id = 3, Competition = null, Year = 1980 };
        private readonly Trophy BadYearTrophy = new Trophy() { Id = 4, Competition = "name", Year = 1969 };

        [TestMethod]
        public void GetTest()
        {
            IEnumerable<Trophy> trophies = repo.Get(orderBy: "Competition_asc");

            Assert.AreEqual("Abu Dabi Cup",trophies.First().Competition);

            trophies = repo.Get(orderBy: "Year");
            Assert.AreEqual("Junior Championship",trophies.First().Competition);

            trophies = repo.Get(YearAfter: 2000);
            Assert.AreEqual(4, trophies.Count());
            Assert.AreEqual("World championship", trophies.First().Competition);
        }
        [TestMethod]
        public void AddTest()
        {
            Trophy t = repo.Add(new Trophy() { Competition = "Test", Year = 2021 });
            Assert.IsTrue(t.Id >= 0);
            Assert.AreEqual(6, repo.Get().Count());

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                           () => repo.Add(new Trophy { Competition = "Good", Year = 1969 }));
            Assert.ThrowsException<ArgumentNullException>(
                           () => repo.Add(new Trophy { Competition = null, Year = 1980 }));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                           () => repo.Add(new Trophy { Competition = "B", Year = 1980 }));

        }
        [TestMethod]
        public void GetByIdTest()
        {
            Trophy t = repo.Add(new Trophy { Competition = "Denmark open", Year = 2000 });
            Trophy? trophy = repo.GetById(t.Id);
            Assert.IsNotNull(trophy);
            Assert.AreEqual("Denmark open", trophy.Competition);
            Assert.AreEqual(2000, trophy.Year);

            Assert.IsNull(repo.GetById(-1));
        }

        [TestMethod]
        public void RemoveTest()
        {
            Trophy t = repo.Add(new Trophy { Competition = "Oslo open", Year = 2007 });
            Trophy trophy = repo.Remove(t.Id);
            Assert.IsNotNull(trophy);
            Assert.AreEqual("Oslo open", trophy.Competition);

            Trophy? trophy2 = repo.Remove(t.Id);
            Assert.IsNull(trophy2);
        }
        [TestMethod]
        public void UpdateTest()
        {
            Trophy t = repo.Add(new Trophy { Competition = "Dallas Open", Year = 2001 });
            Trophy? trophy = repo.Update(t.Id, new Trophy { Competition = "New York open", Year = 2000 });
            Assert.IsNotNull(trophy);
            Trophy? trophy2 = repo.GetById(t.Id);
            Assert.AreEqual("New York open", trophy.Competition);

            Assert.IsNull(
                repo.Update(-1, new Trophy { Competition = "Nulltest", Year = 2000 }));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => repo.Update(t.Id, new Trophy { Competition = "", Year = 2000 }));
        }
    }
}
