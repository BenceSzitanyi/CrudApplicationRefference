using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphicCrud.Repository.Context;

namespace GraphicCrud.Repository.Generic
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected GameDbContext ctx;

        public Repository(GameDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Create(T item)
        {
            ctx.Set<T>().Add(item);
            ctx.SaveChanges();
        }

        public void Delete(int id)
        {
            var set = ctx.Set<T>();
            set.Remove(Read(id));
            ctx.SaveChanges();
        }

        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>();
        }

        public abstract T Read(int id);
        public abstract void Update(T item);
    }
}
