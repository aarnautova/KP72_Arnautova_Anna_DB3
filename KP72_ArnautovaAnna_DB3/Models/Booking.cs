using System;
using System.Collections.Generic;

namespace KP72_ArnautovaAnna_DB3
{
    public partial class Booking
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }

        public virtual Guest Guest { get; set; }
        public virtual Room Room { get; set; }
    }
}
