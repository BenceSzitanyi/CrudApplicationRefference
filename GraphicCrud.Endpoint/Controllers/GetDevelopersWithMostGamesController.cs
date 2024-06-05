using GraphicCrud.Logic.Interfaces;
using GraphicCrud.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphicCrud.Endpoint.Controllers
{
    [ApiController]
    [Route("api/GetDevelopersWithMostGames")]
    public class GetDevelopersWithMostGamesController:ControllerBase
    {
        public IDeveloperLogic DeveloperLogic { get; set; }

        public GetDevelopersWithMostGamesController(IDeveloperLogic devLog)
        {
            DeveloperLogic = devLog;
        }

        [HttpGet()]
        public List<Developer> GetDevelopersWithMostGames()
        {
            try
            {
                return DeveloperLogic.GetDevelopersWithMostGames(1);
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }

    
}
