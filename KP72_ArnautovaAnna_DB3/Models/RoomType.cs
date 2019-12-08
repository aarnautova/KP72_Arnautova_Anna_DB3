using System;
using System.Collections.Generic;

namespace KP72_ArnautovaAnna_DB3
{
    public partial class RoomType
    {
        public RoomType()
        {
            Room = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string TypeName { get; set; }
        public short BedCount { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Room> Room { get; set; }
    }
}
