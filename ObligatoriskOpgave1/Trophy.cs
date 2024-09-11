using System.Security.Principal;

namespace ObligatoriskOpgave1
{
    public class Trophy
    {
        public int Id { get; set; }
        public string Competition { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Competition)}={Competition}, {nameof(Year)}={Year.ToString()}}}";
        }

        public void ValidateCompetition()
        {
            if (Competition == null)
            {
                throw new ArgumentNullException("Må ikke være null");

            }
            if (Competition.Length < 3)
            {
                throw new ArgumentOutOfRangeException("Competition skal være 3 karaktere lang");

            }
        }
        public void ValidateYear()
        {
            if (Year <1970 || Year > 2024)
            {
                throw new ArgumentOutOfRangeException("Årstal skal være mellem 1970 og 2024");
            }
        }
        public void Validate()
        {
            ValidateYear();
            ValidateCompetition();
        }
        //        Id, et tal
        //Competition, tekst-streng, min 3 tegn langt, må ikke være null
        //Year, tal, 1970 <= year  <= 2024


    }
}
