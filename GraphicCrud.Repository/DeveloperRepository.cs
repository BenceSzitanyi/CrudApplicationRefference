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
    public class DeveloperRepository : Repository<Developer>, IRepository<Developer>
    {
        public DeveloperRepository(GameDbContext ctx) : base(ctx) { }

        public override Developer Read(int id)
        {
            return this.ctx.Developers.FirstOrDefault(t => t.DeveloperId == id);
        }

        public override void Update(Developer item)
        {
            var old = (Developer)(Read(item.DeveloperId));
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
