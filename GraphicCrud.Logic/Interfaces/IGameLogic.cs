using GraphicCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicCrud.Logic.Interfaces
{
    public interface IGameLogic
    {
        //CRUD
        void Create(VideoGame item);
        void Delete(int id);
        VideoGame Read(int id);
        IQueryable<VideoGame> ReadAll();
        void Update(VideoGame item);

        //NON-CRUD
        public List<VideoGame> GetGamesByDeveloperName(string devName);
        public Publisher GetPublisherDetailsByGameTitle(string gameTitle);
        public List<VideoGame> GetGamesByPlatformAndPublisher(string platform, int publisherId);
    }
}
