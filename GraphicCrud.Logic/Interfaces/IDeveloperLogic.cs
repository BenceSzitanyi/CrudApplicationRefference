using GraphicCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicCrud.Logic.Interfaces
{
    public interface IDeveloperLogic
    {
        void Create(Developer item);
        void Delete(int id);
        Developer Read(int id);
        IQueryable<Developer> ReadAll();
        void Update(Developer item);

        public List<Developer> GetDevelopersWithMostGames(int topCount);
        public List<Developer> GetDevelopersByPublisherName(string publisherName);
    }
}
