using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItemHoarder.Data
{
    public enum GameType
    {
        DungeonsAndDragons,
        Pathfinder
    }
    public class Profile
    {
        [Key]
        public Guid UserID { get; set; }
        public string Username { get; set; }
        public string About { get; set; }
        public ICollection<Photo> Photo { get; set; }
        public DateTimeOffset DateOfCreation { get; set; }
        public DateTimeOffset? DateOfModification { get; set; }
    }
}
