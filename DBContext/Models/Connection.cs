using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBContext.Models
{
    public class Connection
    {
        public int ConnectionId { get; set; }
        public Planet ConnectedTo { get; set; }
        //[InverseProperty("ConnectedPlanets")]
        public Planet Owner { get; set; }
        public int ConnectedWeight { get; set; }

        public Connection() { }

        public Connection(Planet planetFrom, Planet planetTo,int weight)
        {
            Owner = planetFrom;
            ConnectedTo = planetTo;
            ConnectedWeight = weight;
        }

        public Planet GetPlanet()
        {
            return ConnectedTo;
        }
    }
}
