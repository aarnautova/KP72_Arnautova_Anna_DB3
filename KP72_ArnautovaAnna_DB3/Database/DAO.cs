using KP72_ArnautovaAnna_DB3;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3_DB.Database
{
    abstract class DAO<T>
    {
        protected HotelContext _db;

        public DAO(HotelContext db){
            _db = db;
        }

        public abstract void Create(T entity);
        public abstract T Get(int id);
        public abstract List<T> GetAll(int page);
        public abstract void Update(T entity);
        public abstract void Delete(int id);
    }
}
