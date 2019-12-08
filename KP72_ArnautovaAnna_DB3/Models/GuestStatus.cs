using System;
using System.Collections.Generic;

namespace KP72_ArnautovaAnna_DB3
{
    public partial class GuestStatus
    {
        public GuestStatus()
        {
            Guest = new HashSet<Guest>();
        }

        public int Id { get; set; }
        public string StatusName { get; set; }
        public short RequiredVisits { get; set; }

        public virtual ICollection<Guest> Guest { get; set; }
    }
}
