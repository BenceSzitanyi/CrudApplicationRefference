using GraphicCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicCrud.Logic.Interfaces
{
    public interface IPublisherLogic
    {
        void Create(Publisher item);
        void Delete(int id);
        Publisher Read(int id);
        IQueryable<Publisher> ReadAll();
        void Update(Publisher item);
    }
}
