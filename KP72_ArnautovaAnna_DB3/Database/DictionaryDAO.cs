using KP72_ArnautovaAnna_DB3;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_DB.Database
{
    class DictionaryDAO
    {
        private HotelContext dbconnection;

        public DictionaryDAO(HotelContext db)
        {
            dbconnection = db;
        }

        public List<RoomType> GetRoomTypes()
        {
            return dbconnection.RoomType.ToList();
        }

        public List<GuestStatus> GetGuestStatus()
        {
            return dbconnection.GuestStatus.ToList();
        }
    }
}
