using GraphicCrud.Endpoint.Services;
using GraphicCrud.Logic.Interfaces;
using GraphicCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace GraphicCrud.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        IDeveloperLogic logic;
        IHubContext<SignalRHub> hub;
        public DeveloperController(IDeveloperLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Developer> ReadAll()
        {
            return this.logic.ReadAll();
        }
        [HttpGet("{id}")]
        public Developer Read(int id)
        {
            return this.logic.Read(id);
        }
        [HttpPost]
        public void Create([FromBody] Developer value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("DeveloperCreated",value);
        }
        [HttpPut]
        public void Update([FromBody] Developer value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("DeveloperUpdated",value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var developerToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("DeveloperDeleted",developerToDelete);
        }
    }
}
