using GraphicCrud.Models;
using GraphicCrud.Repository.Context;
using GraphicCrud.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicCrud.Repository
{
    public class PublisherRepository : Repository<Publisher>, IRepository<Publisher>
    {
        public PublisherRepository(GameDbContext ctx) : base(ctx) { }

        public override Publisher Read(int id)
        {
            return this.ctx.Publishers.FirstOrDefault(t => t.PublisherId == id);
        }

        public override void Update(Publisher item)
        {
            var old = (Publisher)(Read(item.PublisherId));
            foreach (var property in old.GetType().GetProperties())
            {
                if (property.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    var uuu = property.GetValue(item);
                    property.SetValue(old, uuu);
                }
            }
            ctx.SaveChanges();
        }
    }
}
