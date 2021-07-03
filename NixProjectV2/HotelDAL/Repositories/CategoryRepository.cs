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
    class CategoryRepository : IRepository<Category>
    {
        private HotelContext db;

        public CategoryRepository(HotelContext db)
        {
            this.db = db;
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories;
        }

        public Category Get(int id)
        {
            return db.Categories.Find(id);
        }

        public void Create(Category category)
        {
            db.Categories.Add(category);
        }

        public void Delete(int id)
        {
            Category category = Get(id);
            if (category != null)
                db.Categories.Remove(category);
        }
    }
}
