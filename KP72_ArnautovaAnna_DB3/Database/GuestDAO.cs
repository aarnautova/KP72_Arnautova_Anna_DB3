using KP72_ArnautovaAnna_DB3;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3_DB.Database
{
    class GuestDAO : DAO<Guest>
    {
        public GuestDAO(HotelContext db) : base(db) { }

        public override void Create(Guest entity)
        {
            _db.Guest.Add(entity);
            _db.SaveChanges();
        }

        public override void Delete(int id)
        {
            _db.Guest.Remove(_db.Guest.Single(x => x.Id == id));
            _db.SaveChanges();
        }

        public override Guest Get(int id)
        {
            return _db.Guest.Single(x => x.Id == id);
        }

        public override List<Guest> GetAll(int page)
        {
            int offset = page >= 0 ? page * 10 : 0;
            List<Guest> r_list = _db.Guest.Include(guest => guest.Status).Skip(offset).Take(10).ToList();
            return r_list;
        }

        public override void Update(Guest entity)
        {
            Guest upd = _db.Guest.Single(x => x.Id == entity.Id);
            upd.Name = entity.Name;
            upd.Surname = entity.Surname;
            upd.StatusId = entity.StatusId;
            _db.SaveChanges();
        }
    }
}
