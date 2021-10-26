using DBContext.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace DBContext.Models
{
    public class Planet
    {
        private int _planetId;
        public int PlanetId { get { return _planetId; } set { _planetId = value; } }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        private int _pathweight = int.MaxValue;

  //      [NotMapped]
//        public List<Connection> ConnectedPlanets { get; set; }

        // Variables used by dijkstra's algorithm
        [NotMapped]
        public bool Visited { get; set; }
        [NotMapped]
        public int PathWeight { get { return _pathweight; } set { _pathweight = value; } }
        [NotMapped]
        public Planet PreviousPlanet { get; set; }


        // Constructors
        public Planet() { }

        public Planet(string name, int x,int y)
        {
            if (!String.IsNullOrEmpty(name))
            {
                Name = name;
                X = x;
                Y = y;
            }
            else
            {
                Name = "Default";
            }
        }

        // Methods
        public void ResetShortestPath()
        {
            PreviousPlanet = null;
            PathWeight = int.MaxValue;
            Visited = false;
        }

        public List<string> PrintPath(List<string> list)
        {
            Debug.WriteLine("Previous planet = " + PreviousPlanet);
            list.Add(Name);
            if (PreviousPlanet != null)
            {
                PreviousPlanet.PrintPath(list);
            }
            return list;
        }

        //public override string ToString()
        //{
        //    StringBuilder answer = new StringBuilder(Name);
        //    answer.Append(":\tThe amount of connections = " + ConnectedPlanets.Count);
        //    for (int i = 0; i < ConnectedPlanets.Count; i++)
        //    {
        //        answer.Append("\n---" + ConnectedPlanets[i].GetPlanet().Name + "\t" + ConnectedPlanets[i].ConnectedWeight);
        //    }

        //    return answer.ToString();
        //}
    }
}
