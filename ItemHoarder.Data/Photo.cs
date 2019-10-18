using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data
{
    public enum FileType
    {
        Profile,
        Character,
        Monster,
        Item,
        Room,
        Characteristic,
        Other
    }
    public class Photo
    {
        [Key]
        public int PhotoID { get; set; }
        public string PhotoName { get; set; }
        public FileType FileType { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
