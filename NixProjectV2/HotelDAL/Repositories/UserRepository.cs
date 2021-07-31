using HotelDAL.EF;
using HotelDAL.Entities;
using HotelDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDAL.Repositories
{
    class UserRepository : IRepository<User>
    {
        private HotelContext db;

        public UserRepository(HotelContext db)
        {
            this.db = db;
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Update(int categoryId, User value)
        {
            var user = db.Users.FirstOrDefault(m => m.Id == categoryId);
            if (user != null)
            {
                user.Login = value.Login ?? user.Login;
                user.Password = value.Password ?? user.Password;
                user.FullName = value.FullName ?? user.FullName;
            }

        }

        public void Delete(int id)
        {
            User user = Get(id);
            if (user != null)
                db.Users.Remove(user);
        }

    }
}
