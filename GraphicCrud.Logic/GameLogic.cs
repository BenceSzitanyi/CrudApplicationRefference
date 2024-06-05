using GraphicCrud.Logic.Interfaces;
using GraphicCrud.Models;
using GraphicCrud.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphicCrud.Logic
{
    public class GameLogic : IGameLogic
    {
        IRepository<VideoGame> repo;
        public GameLogic(IRepository<VideoGame> repo)
        {
            this.repo = repo;
        }

        public void Create(VideoGame item)
        {
            if (item.Title.Length < 3)
            {
                throw new ArgumentException("title too short...");
            }
            else if (item.MetacriticsScore < 0)
            {
                throw new ArgumentException("score cannot be negative");
            }
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            var game = this.repo.Read(id);
            this.repo.Delete(id);
        }



        public VideoGame Read(int id)
        {
            var game = this.repo.Read(id);
            if (game == null)
            {
                throw new ArgumentException("Game does not exist");
            }
            return game;
        }

        public IQueryable<VideoGame> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(VideoGame item)
        {
            var game = this.repo.Read(item.VideoGameId);
            this.repo.Update(item);
        }

        public List<VideoGame> GetGamesByDeveloperName(string devName)
        {
            return this.repo.ReadAll()
                .Where(t => t.Developer.DeveloperName == devName)
                .ToList();
        }

        public List<VideoGame> GetGamesByPlatformAndPublisher(string platform, int publisherId)
        {
            return this.repo.ReadAll()
                .Where(t => t.Platform == platform && t.PublisherId == publisherId)
                .ToList();
        }

        public Publisher GetPublisherDetailsByGameTitle(string gameTitle)
        {
            return this.repo.ReadAll()
                .Where(t => t.Title == gameTitle)
                .Select(t => t.Publisher)
                .FirstOrDefault();
        }
    }
}
