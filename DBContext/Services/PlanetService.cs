using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DBContext.Models;
using System.Linq;

namespace DBContext.Services
{
    public class PlanetService
    {
        public DatabaseContext _context;

        public PlanetService(IServiceProvider serviceProvider)
        {
            _context = new DatabaseContext(serviceProvider.GetRequiredService<DbContextOptions<DatabaseContext>>());
        }

        public PlanetService(DatabaseContext context)
        {
            _context = context;
        }

        public List<Planet> GetAllPlanets()
        {
            return _context.Planets.ToList();
        }

        public IEnumerable<Connection> GetConnectedPlanets(int ownerId)
        {
            return _context.Connections.Where(p=>p.Owner.PlanetId==ownerId);
        }

        public Planet GetPlanetById(int id)
        {
            return (from p in GetAllPlanets() where p.PlanetId == id select p).FirstOrDefault();

        }

        public void SaveNewPlanet(Planet planet)
        {
            _context.Add(planet);
            _context.SaveChanges();
        }

        public void SaveNewPlanets(List<Planet> planets)
        {
            _context.AddRange(planets);
            _context.SaveChanges();
        }

        public void UpdatePlanet(Planet planet)
        {
            _context.Update(planet);
            _context.SaveChanges();
        }

        public void DeletePlanet(int id)
        {
            //return (from p in GetAllPlanets() where p.PlanetId == id select p).FirstOrDefault();

            List<Connection> connectionIds = (from c in _context.Connections where c.Owner.PlanetId == id || c.ConnectedTo.PlanetId == id select c).ToList<Connection>();
            _context.Connections.RemoveRange(connectionIds);
            _context.SaveChanges();
            _context.Remove(_context.Planets.Find(id));
            _context.SaveChanges();
        }

        public void DeleteAllPlanets()
        {

                _context.RemoveRange(_context.Connections);
                _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT(Connections, RESEED, 0)");
                _context.SaveChanges();
                _context.RemoveRange(_context.Planets);
                _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT(Planets, RESEED, 0)");
                _context.SaveChanges();
            

        }
    }
}
