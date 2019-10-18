using ItemHoarder.Data;
using ItemHoarder.Data.Characteristics.Races;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models.Characteristics.Race
{
    public class RaceIndex
    {
        public int RaceID { get; set; }
        public Guid OwnerID { get; set; }
        public GameType GameType { get; set; }
        public string RaceName { get; set; }
        public RaceType RaceType { get; set; }
        public string PhysicalDescription { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
