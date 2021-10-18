using DBContext.Models;
using DBContext.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Space_Truckers.Controllers
{
    [Route("Connection")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        private ConnectionService _service;

        public ConnectionController(IServiceProvider serviceProvider)
        {
            _service = new ConnectionService(serviceProvider);
        }

        [HttpGet]
        public List<Connection> Get()
        {
            Debug.WriteLine("get");
            return _service.GetAllConnections();
        }
        [HttpGet("{id}")]
        public Connection Get(int id)
        {
            Debug.WriteLine("get id");
            return _service.GetConnectionById(id);
        }

        [HttpPost]
        public void Post(string planetA, string planetB, int? weightAtoB, int? weightBtoA, bool? both)
        {
            Debug.WriteLine("post");
            if (planetA != null && planetB != null && weightAtoB.HasValue && both.HasValue)
            {
                Debug.WriteLine("Check");
                if (both.Value)
                {

                    if (weightBtoA.HasValue)
                    {

                        _service.CreateConnectionBothWays(planetA, planetB, weightAtoB.Value, weightBtoA.Value);
                    }
                    else
                    {
                        _service.CreateConnectionBothWays(planetA, planetB, weightAtoB.Value);
                    }
                }
                else
                {
                    _service.CreateConnectionOneWay(planetA, planetB, weightAtoB.Value);
                }
            }
            else
            {
                Debug.WriteLine("ChecknotOkay");
            }


        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Connection connection)
        {
            connection.ConnectionId = id;
            _service.UpdateConnection(connection);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.DeleteConnection(id);
        }

        [HttpDelete]
        public void DeleteAll()
        {
            _service.DeleteAllConnections();
        }
    }
}