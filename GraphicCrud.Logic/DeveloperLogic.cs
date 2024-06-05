using GraphicCrud.Logic.Interfaces;
using GraphicCrud.Models;
using GraphicCrud.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicCrud.Logic
{
    public class DeveloperLogic : IDeveloperLogic
    {
        IRepository<Developer> repo;
        public DeveloperLogic(IRepository<Developer> repo)
        {
            this.repo = repo;
        }

        public void Create(Developer item)
        {
            repo.Create(item);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public Developer Read(int id)
        {
            return repo.Read(id);
        }

        public IQueryable<Developer> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Developer item)
        {
            repo.Update(item);
        }

        //non-crud
        public List<Developer> GetDevelopersWithMostGames(int topCount)
        {
            var a = this.repo.ReadAll()
                .OrderByDescending(d => d.Games.Count)
                .Take(topCount)
                .ToList();
            return a;
        }
        public List<Developer> GetDevelopersByPublisherName(string publisherName)
        {
            return this.repo.ReadAll()
                .Where(d => d.Publisher.PublisherName == publisherName)
                .ToList();
        }
    }
}
