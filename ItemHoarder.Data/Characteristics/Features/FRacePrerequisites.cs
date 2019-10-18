using ItemHoarder.Data.Characteristics.Races;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.Characteristics.Features
{
    public class FRacePrerequisites
    {
        [Key]
        public int FRID { get; set; }
        [ForeignKey("Race")]
        public int RaceID { get; set; }
        public virtual Race Race { get; set; }
    }
}
