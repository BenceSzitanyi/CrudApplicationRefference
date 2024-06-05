using GraphicCrud.Endpoint.Services;
using GraphicCrud.Logic.Interfaces;
using GraphicCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace GraphicCrud.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        IGameLogic logic;
        IHubContext<SignalRHub> hub; 
        public GameController(IGameLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<VideoGame> ReadAll()
        {
            return this.logic.ReadAll();
        }
        [HttpGet("{id}")]
        public VideoGame Read(int id)
        {
            return this.logic.Read(id);
        }
        [HttpPost]
        public void Create([FromBody] VideoGame value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("VideoGameCreated", value);
        }
        [HttpPut]
        public void Update([FromBody] VideoGame value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("VideoGameUpdated", value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var gameToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("VideoGameDeleted", gameToDelete);
        }
    }
}
