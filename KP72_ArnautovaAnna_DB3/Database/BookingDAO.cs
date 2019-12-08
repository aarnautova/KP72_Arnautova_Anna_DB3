using KP72_ArnautovaAnna_DB3;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_DB.Database
{
    class BookingDAO : DAO<Booking>
    {

        public BookingDAO(HotelContext db) : base(db) { }

        private bool dateCheck(Booking entity, bool selfCheck)
        {
            if (entity.Arrival >= entity.Departure) throw new Exception("Arrival can't be bigger than departure.");
            List<Booking> bl = _db.Booking.Where(b => b.RoomId == entity.RoomId).ToList();
            if (selfCheck)
            {
                Booking self = bl.Find(b => b.Id == entity.Id);
                if (self != null) bl.Remove(self);
            }
            foreach(Booking b in bl)
            {
                DateTime arrival = Convert.ToDateTime(b.Arrival);
                DateTime departure = Convert.ToDateTime(b.Departure);
                if (entity.Arrival >= arrival && entity.Arrival < departure
                    || entity.Departure <= departure && entity.Departure > arrival)
                    return false;
            }
            return true;
        }
        public override void Create(Booking entity)
        {
            _db.Booking.Add(entity);
            _db.SaveChanges();
        }

        public override void Delete(int id)
        {
            _db.Booking.Remove(_db.Booking.Single(x => x.Id == id));
            _db.SaveChanges();
        }

        public override Booking Get(int id)
        {
            return _db.Booking.Single(x => x.Id == id);
        }

        public override List<Booking> GetAll(int page)
        {
            int offset = page >= 0 ? page * 10 : 0;
            List<Booking> r_list = _db.Booking
                                      .Include(b => b.Room)
                                      .Include(b => b.Guest)
                                      .Include(b => b.Room.Type)
                                      .Include(b => b.Guest.Status)
                                      .Skip(offset).Take(10).ToList();
        
            return r_list;
        }

        public override void Update(Booking entity)
        {
            if (!dateCheck(entity, true)) throw new Exception("Wrong date");
            Booking upd = _db.Booking.Single(x => x.Id == entity.Id);
            upd.Arrival = entity.Arrival;
            upd.Departure = entity.Departure;
            upd.RoomId = entity.RoomId;
            upd.GuestId = entity.GuestId;
            _db.SaveChanges();
        }

        public List<Booking> StaticSearch(DateTime d1, DateTime d2, bool expected_ocean_view)
        {
            if (d1 >= d2) throw new Exception("Wrong date diapason");
            return _db.Booking
                      .Include(b => b.Room)
                      .Include(b => b.Guest)
                      .Include(b => b.Room.Type)
                      .Include(b => b.Guest.Status)
                      .Where(b => b.Arrival >= d1 && b.Departure <= d2 && b.Room.OceanView == expected_ocean_view).ToList();
        }
    }
}
