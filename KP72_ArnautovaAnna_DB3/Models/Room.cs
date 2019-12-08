using System;
using System.Collections.Generic;

namespace KP72_ArnautovaAnna_DB3
{
    public partial class Room
    {
        public Room()
        {
            Booking = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Number { get; set; }
        public bool OceanView { get; set; }

        public virtual RoomType Type { get; set; }
        public virtual ICollection<Booking> Booking { get; set; }
    }
}
