using System;
using System.Collections.Generic;

namespace KP72_ArnautovaAnna_DB3
{
    public partial class Guest
    {
        public Guest()
        {
            Booking = new HashSet<Booking>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int StatusId { get; set; }

        public virtual GuestStatus Status { get; set; }
        public virtual ICollection<Booking> Booking { get; set; }
    }
}
