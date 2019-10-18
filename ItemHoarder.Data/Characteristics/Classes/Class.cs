using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.Characteristics.Classes
{
    public class Class
    {
        [Key]
        public int ClassID { get; set; }
        public Guid OwnerID { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }

        public double StrengthModifier { get; set; }
        public double DexterityModifier { get; set; }
        public double ConstitutionModifier { get; set; }
        public double IntelligenceModifier { get; set; }
        public double WisdomModifier { get; set; }
        public double CharismaModifier { get; set; }
        [DefaultValue(false)]
        public bool IsDeactivated { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
