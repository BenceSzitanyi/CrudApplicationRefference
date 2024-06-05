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
    public class PublisherLogic : IPublisherLogic
    {
        IRepository<Publisher> repo;
        public PublisherLogic(IRepository<Publisher> repo)
        {
            this.repo = repo;
        }

        public void Create(Publisher item)
        {
            repo.Create(item);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public Publisher Read(int id)
        {
            return repo.Read(id);
        }

        public IQueryable<Publisher> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(Publisher item)
        {
            repo.Update(item);
        }
    }
}
