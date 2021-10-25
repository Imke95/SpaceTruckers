using DBContext.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace DBContext.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new DatabaseContext(
               serviceProvider.GetRequiredService<
                   DbContextOptions<DatabaseContext>>());

            PlanetService _planetService = new PlanetService(context);
            ConnectionService _connectionService = new ConnectionService(context);

            //delete all data and reset the indices
            _planetService.DeleteAllPlanets();

            //Add planets
            Planet A = new Planet("A",-350,50);
            Planet B = new Planet("B",-400,250);
            Planet C = new Planet("C",-300,400);
            Planet D = new Planet("D",-50,225);
            Planet E = new Planet("E",0,50);
            Planet F = new Planet("F",100,100);
            Planet G = new Planet("G",75,250);
            Planet H = new Planet("H",225,250);
            Planet I = new Planet("I",400,25);
            Planet J = new Planet("J",275,325);
            _planetService.SaveNewPlanets(new List<Planet>() { A, B, C, D, E, F, G, H, I, J });

            //Add connections
            _connectionService.CreateConnectionBothWays(A, B, 6);
            _connectionService.CreateConnectionBothWays(B, C, 5);
            _connectionService.CreateConnectionBothWays(A, D, 11);
            _connectionService.CreateConnectionBothWays(D, E, 7);
            _connectionService.CreateConnectionBothWays(D, F, 26);
            _connectionService.CreateConnectionBothWays(D, G, 5);
            _connectionService.CreateConnectionBothWays(E, F, 14);
            _connectionService.CreateConnectionBothWays(G, H, 3);
            _connectionService.CreateConnectionBothWays(G, J, 5);
            _connectionService.CreateConnectionBothWays(H, J, 4);
            _connectionService.CreateConnectionBothWays(F, H, 3);
            _connectionService.CreateConnectionBothWays(F, I, 17);
            _connectionService.CreateConnectionBothWays(H, I, 5);
        }
    }
}