using GraphicCrud.Logic.Interfaces;
using GraphicCrud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GraphicCrud.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IGameLogic gameLogic;
        IDeveloperLogic devLogic;

        public StatController(IGameLogic gameLogic, IDeveloperLogic devLogic)
        {
            this.gameLogic = gameLogic;
            this.devLogic = devLogic;
        }

        //GameLogic Non-CRUD methods to the IO
        [HttpGet("{devName}")]
        public IEnumerable<VideoGame> GamesByDeveloperName(string devName)
        {
            return this.gameLogic.GetGamesByDeveloperName(devName);
        }

        [HttpGet("{platform&publisherId}")]
        public IEnumerable<VideoGame> GamesByPlatformAndPublisher(string platform, int publisherId)
        {
            return this.gameLogic.GetGamesByPlatformAndPublisher(platform, publisherId);
        }

        [HttpGet("{title}")]
        public Publisher PublisherDetailsByGameTitle(string title)
        {
            return this.gameLogic.GetPublisherDetailsByGameTitle(title);
        }

        //developerLogic non-CRUD methods to the io
        [HttpGet("{topcount}")]
        public IEnumerable<Developer> DevelopersWithMostGames(int topcount)
        {
            return this.devLogic.GetDevelopersWithMostGames(topcount);
        }

        [HttpGet("{publisherName}")]
        public IEnumerable<Developer> DevelopersByPublisherName(string publisherName)
        {
            return this.devLogic.GetDevelopersByPublisherName(publisherName);
        }
    }
}
