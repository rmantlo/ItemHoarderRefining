using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ItemHoarder.Models.Profile
{
    public class ProfileUpdate
    {
        public Guid UserID { get; set; }
        public byte[] Photo { get; set; }
        public HttpPostedFileBase PhotoUpload { get; set; }
        public string About { get; set; }
    }
}
