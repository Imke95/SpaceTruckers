using DBContext.Models;
using DBContext.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Space_Truckers.Controllers
{
    [Route("GetPath")]
    [ApiController]
    public class ShortestPathEndpoint : ControllerBase
    {
        private PlanetService _planetService;
        private ConnectionService _connectionService;

        public ShortestPathEndpoint(IServiceProvider serviceProvider)
        {
            _planetService = new PlanetService(serviceProvider);
            _connectionService = new ConnectionService(serviceProvider);
        }

        [HttpGet]
        public List<string> ShortestPath(string from, string to)
        {
            List<string> result=new List<string>();
            List<Planet> planets = _planetService.GetAllPlanets().ToList();
            Planet currentPlanet = _connectionService.FindPlanet(from);
            //Planet currentPlanet = (from planet in planets where from.Equals(p.Name) select p).FirstOrDefault();
            //Planet currentPlanet = (from p in planets where p.Name.Equals(from) select p).FirstOrDefault();
            //return (from p in _context.Planets.Include("ConnectedPlanets").ToList() where (p.Name).Equals(name) select p).FirstOrDefault();

            Planet destination = _connectionService.FindPlanet(to);
            currentPlanet.PathWeight = 0;
            currentPlanet.Visited = true;
            if (destination != null && currentPlanet != null)
            {
                // While the current planet isn't the destination keep checking if the weight of the current planet + the connection is lower than the current weight of the connected planet. If it is lower, then adjust the weight and change the planet from which this new weight originated. After every connection is handled check which planet was not visited yet and has the lowest weight. Hnadle this planet in the next iteration.
                while (currentPlanet.PlanetId != destination.PlanetId)
                {
                    currentPlanet.Visited = true;
                    HandleCurrentPlanet(currentPlanet);
                    currentPlanet = DetermineNextPlanetToCheck(currentPlanet, planets);
                }
                // Print all the planets with their weights and previous planet
                // PrintTheWeightOfEachPlanet(planets);

                // Print the shortest route
                result = currentPlanet.PrintPath(result);
                Debug.WriteLine("\nThe shortest path = " + result.Count.ToString());
            }
            else
            {
                Debug.WriteLine("The input was not valid");
            }
            // Reset the state for the next shortest path request
            ResetShortestPath(planets);
            return result;
            //return "From: " + currentPlanet.Name + ", To: " + destination.Name+", Count: "+planets.Count;
        }

        // Review all connections of the current planet and adjust their weight and previous planet if the path is shorter
        private void HandleCurrentPlanet(Planet currentPlanet)
        {
            List<Connection> connectedPlanets = _planetService.GetConnectedPlanets(currentPlanet.PlanetId).ToList();
            //currentPlanet.ConnectedPlanets;
            for (int i = 0; i < connectedPlanets.Count; i++)
            {

                if (currentPlanet.PathWeight + connectedPlanets[i].ConnectedWeight < connectedPlanets[i].GetPlanet().PathWeight)
                {
                    connectedPlanets[i].GetPlanet().PathWeight = currentPlanet.PathWeight + connectedPlanets[i].ConnectedWeight;
                    connectedPlanets[i].GetPlanet().PreviousPlanet = currentPlanet;

                }
            }
        }

        // Determine the next planet to check out. This is the planet with the lowest weight that has not been visited yet
        private Planet DetermineNextPlanetToCheck(Planet currentPlanet, List<Planet> planets)
        {
            // Initialize the minimum weight to the max value, so that there is always a planet with a lower weight
            int minWeight = int.MaxValue;
            foreach (Planet planet in planets)
            {

                if (!planet.Visited && planet.PathWeight < minWeight)
                {
                    currentPlanet = planet;
                }
            }
            return currentPlanet;
        }

        private void ResetShortestPath(List<Planet> planets)
        {
            foreach (Planet planet in planets)
            {
                planet.ResetShortestPath();
            }
        }

    }
}