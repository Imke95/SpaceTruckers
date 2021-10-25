using DBContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace DBContext.Services
{
    public class ConnectionService
    {
        public static DatabaseContext _context;

        public ConnectionService(IServiceProvider serviceProvider)
        {
            _context = new DatabaseContext(serviceProvider.GetRequiredService<DbContextOptions<DatabaseContext>>());
        }

        public ConnectionService(DatabaseContext context)
        {
            _context = context;
        }
        public ConnectionService() { }

        public List<Connection> GetAllConnections()
        {

            return _context.Connections.Include("Owner").Include("ConnectedTo").ToList();
        }

        public Planet GetConnectedPlanets(int ownerId)
        {
            return _context.Planets.Where(p => p.PlanetId == ownerId).FirstOrDefault();
        }

        public Connection GetConnectionById(int id)
        {
            return (from c in GetAllConnections() where c.ConnectionId == id select c).FirstOrDefault();
        }

        public Planet FindPlanet(string name)
        {
            return (from p in _context.Planets.ToList() where (p.Name).Equals(name) select p).FirstOrDefault();
        }


        public void SaveNewConnection(Connection connection)
        {
            _context.Connections.Add(connection);
            _context.SaveChanges();
        }

        public void UpdateConnection(Connection connection)
        {
            Debug.WriteLine("update service");
            _context.Update(connection);
            _context.SaveChanges();
        }

        public void CreateConnectionBothWays(string planetA, string planetB, int weightAtoB,int weightBtoA)
        {
            Planet A = FindPlanet(planetA);
            Planet B = FindPlanet(planetB);
            CreateConnectionBothWays(A, B, weightAtoB, weightBtoA);
        }

        public void CreateConnectionBothWays(Planet A, Planet B, int weightAtoB, int weightBtoA)
        {
            SaveNewConnection(new Connection(A, B, weightAtoB));
            SaveNewConnection(new Connection(B, A, weightBtoA));
        }


        public void CreateConnectionBothWays(string planetA, string planetB, int weight)
        {
            CreateConnectionBothWays(planetA,planetB,weight,weight);
        }

        public void CreateConnectionBothWays(Planet planetA, Planet planetB, int weight)
        {
            CreateConnectionBothWays(planetA, planetB, weight, weight);
        }

        public void CreateConnectionOneWay(string planetA, string planetB, int weight)
        {
            Planet A = FindPlanet(planetA);
            Planet B = FindPlanet(planetB);
            CreateConnectionOneWay(A,B,weight);
        }
        public void CreateConnectionOneWay(Planet A, Planet B, int weight)
        {
            SaveNewConnection(new Connection(A, B, weight));

        }

        public void DeleteConnection(int id)
        {
            _context.Connections.Remove(GetConnectionById(id));
            _context.SaveChanges();
        }

        public void DeleteAllConnections()
        {
            _context.RemoveRange(_context.Connections);
            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT(Connections, RESEED, 0)");
            _context.SaveChanges();
        }

    }
}