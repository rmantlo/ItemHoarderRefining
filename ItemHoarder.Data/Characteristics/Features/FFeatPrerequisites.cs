using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data.Characteristics.Features
{
    public class FFeatPrerequisites
    {
        [Key]
        public int FFID { get; set; }
        [ForeignKey("Feature")]
        public int FeatureID { get; set; }
        public virtual Feature Feature { get; set; }
    }
}
