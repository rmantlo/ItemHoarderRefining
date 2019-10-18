using ItemHoarder.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Models
{
    public class PhotoDisplay
    {
        public int PhotoID { get; set; }
        public string PhotoName { get; set; }
        public FileType FileType { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}
