using ObligatoriskOpgave1;

namespace ObligatoriskOpgave1Test
{
    [TestClass]
    public class TrophyTest
    {
        private readonly Trophy goodTrophy = new Trophy() { Id = 1, Competition = "nam", Year = 1980 };
        private readonly Trophy BadCompTrophy = new Trophy() { Id = 2, Competition = "na", Year = 1980 };
        private readonly Trophy NullCompTrophy = new Trophy() { Id = 3, Competition = null, Year = 1980 };
        private readonly Trophy BadYearTrophy = new Trophy() { Id = 4, Competition = "name", Year = 1969 };
        
        [TestMethod]
        public void ObjectTest()
        {
            Assert.IsNotNull(goodTrophy);
        }

        [TestMethod]
        public void ValidateCompetitionTest()
        {

            goodTrophy.ValidateCompetition();
            Assert.ThrowsException<ArgumentNullException>(() => NullCompTrophy.ValidateCompetition());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => BadCompTrophy.ValidateCompetition());


        }
       
        [TestMethod]
        [DataRow(1969)]
        [DataRow(2025)]
        public void ValidateYearBAdTest(int year)
        {
            BadYearTrophy.Year = year;
            
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => BadYearTrophy.Validate());

        }
        [TestMethod]
        [DataRow(1970)]
        [DataRow(2024)]
        public void ValidateYearTest(int year)
        { 
        
            goodTrophy.Year=year;
            goodTrophy.Validate();
            
        }
        [TestMethod]
        public void ValidateTest()
        {
            goodTrophy.Validate();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => BadYearTrophy.Validate());
            Assert.ThrowsException<ArgumentNullException>(() => NullCompTrophy.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => BadCompTrophy.Validate());

        }
        [TestMethod]
        public void ToStringTest()
        {
            Assert.AreEqual("{Id=1, Competition=nam, Year=1980}", goodTrophy.ToString());
        }

    }
}