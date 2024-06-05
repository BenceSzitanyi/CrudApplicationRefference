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
    public class GameRepository : Repository<VideoGame>, IRepository<VideoGame>
    {
        public GameRepository(GameDbContext ctx) : base(ctx)
        {
        }

        public override VideoGame Read(int id)
        {
            VideoGame outp = this.ctx.Games.FirstOrDefault(t => t.VideoGameId == id);
            return outp;
        }

        public override void Update(VideoGame item)
        {
            var old = (VideoGame)(Read(item.VideoGameId));
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
