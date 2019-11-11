using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.Characteristics.Proficiencies
{
    public class Proficiency
    {
        [Key]
        public int ProficiencyID { get; set; }
        public string ProName { get; set; }
        public ProficiencyType Type { get; set; }
        public Guid OwnerID { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
