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

        public void Update(int categoryId, Category value)
        {
            var category = db.Categories.FirstOrDefault(m => m.Id == categoryId);

            if (category != null)
            {
                category.Name = value.Name ?? category.Name;
                category.Price = value.Price > 0 ? value.Price : category.Price;
                category.Bed = value.Bed > 0 ? value.Bed : category.Bed;
                category.ActionTime = value.ActionTime != null ? value.ActionTime : category.ActionTime;
                category.ActionType = value.ActionType ?? category.ActionType;
                category.ActionUserId = value.ActionUserId > 0 ? value.ActionUserId : category.ActionUserId;
            }
        }

        public void Delete(int id)
        {
            Category category = Get(id);

            if (category != null)
            {
                db.Categories.Remove(category);
            }
        }
    }
}
