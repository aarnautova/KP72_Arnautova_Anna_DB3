using KP72_ArnautovaAnna_DB3;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Lab3_DB.Database
{
    class RoomDAO : DAO<Room>
    {

        public RoomDAO(HotelContext db) : base(db) { }

        public override void Create(Room entity)
        {
            _db.Room.Add(entity);
            _db.SaveChanges();
        }

        public override void Delete(int id)
        {
            _db.Room.Remove(_db.Room.Single(x => x.Id == id));
            _db.SaveChanges();
        }

        public override Room Get(int id)
        {
            return  _db.Room.Single(x => x.Id == id);
        }

        public override List<Room> GetAll(int page)
        {
            int offset = page >= 0 ? page * 10 : 0;
            List<Room> r_list = _db.Room.Include(room => room.Type).Skip(offset).Take(10).ToList();
            return r_list;
        }

        public override void Update(Room entity)
        {
            Room upd = _db.Room.Single(x => x.Id == entity.Id);
            upd.Number = entity.Number;
            upd.OceanView = entity.OceanView;
            upd.TypeId = entity.TypeId;
            _db.SaveChanges();
        }
    }
}
