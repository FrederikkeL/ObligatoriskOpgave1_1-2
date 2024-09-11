using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ObligatoriskOpgave1
{
    public class TrophiesRepository
    {
        private List<Trophy> Trophies { get; set; } = new List<Trophy>();
        private int _nextId = 1;

        public TrophiesRepository()
        {
            Trophies.Add(new Trophy() { Competition = "World championship", Year = 2018 });
            Trophies.Add(new Trophy() { Competition = "European championship", Year = 2016});
            Trophies.Add(new Trophy() { Competition = "World cup Copenhagen", Year = 2020 });
            Trophies.Add(new Trophy() { Competition = "Abu Dabi Cup", Year = 2010 });
            Trophies.Add(new Trophy() { Competition = "Junior Championship", Year = 1980 });
        }

        public IEnumerable<Trophy?> Get(int? YearBefore=null, int? YearAfter=null, string? orderBy = null)
        {
            IEnumerable<Trophy> result = new List<Trophy>(Trophies);
            if (YearAfter != null)
            {
                result = result.Where(t => t.Year > YearAfter);
            }
            if (YearBefore != null)
            {
                result = result.Where(t => t.Year < YearAfter);
            }
          
            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    case "competition": // fall through to next case
                    case "competition_asc":
                        result = result.OrderBy(t => t.Competition);
                        break;
                    case "competition_desc":
                        result = result.OrderByDescending(t => t.Competition);
                        break;
                    case "year":
                    case "year_asc":
                        result = result.OrderBy(t => t.Year);
                        break;
                    case "year_desc":
                        result = result.OrderByDescending(t => t.Year);
                        break;
                    default:
                        break; // do nothing
                        //throw new ArgumentException("Unknown sort order: " + orderBy);
                }
            }
            return result;
        }

        public Trophy Add(Trophy trophy)
        {
            trophy.Validate();
            trophy.Id = _nextId++;
            Trophies.Add(trophy);
            return trophy;
        }
        public Trophy? Remove(int id)
        {
            Trophy? trophy = GetById(id);
            if (trophy == null)
            {
                return null;
            }
            Trophies.Remove(trophy);
            return trophy;

        }

        public Trophy? GetById(int id)
        {
            return Trophies.Find(trophy => trophy.Id == id);
        }

        public Trophy? Update(int id, Trophy data)
        {
            data.Validate();
            Trophy trophyToUpdate = GetById(id);
            if (trophyToUpdate == null)
            {
                return null;
            }
            trophyToUpdate.Competition = data.Competition;
            trophyToUpdate.Year = data.Year;
            return trophyToUpdate;

        }
    }
}
