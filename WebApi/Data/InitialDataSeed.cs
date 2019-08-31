using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Data
{
    public class InitialDataSeed
    {
        public static IEnumerable<Player> Players => new[]
        {
            new Player{ Id=Guid.NewGuid().ToString(), Name = "Laz Ziya", Level=9 },
            new Player{ Id=Guid.NewGuid().ToString(), Name = "Ali", Level=3 },
            new Player{ Id=Guid.NewGuid().ToString(), Name = "Joe", Level=2 },
            new Player{ Id=Guid.NewGuid().ToString(), Name = "Jasmin", Level=0 },
            new Player{ Id=Guid.NewGuid().ToString(), Name = "Michale", Level=2 },
            new Player{ Id=Guid.NewGuid().ToString(), Name = "Lokman", Level=8 },
            new Player{ Id=Guid.NewGuid().ToString(), Name = "Fantom", Level=7 },
            new Player{ Id=Guid.NewGuid().ToString(), Name = "DarkNight", Level=6 },
        };

        public static IEnumerable<Treasure> Treasures => new[]
        {
            new Treasure{ Id=1, Name = "Crown", Kind="Gold and diamond", Price=999 },
            new Treasure{ Id = 2,Name = "Sword", Kind="Silver", Price=700 },
            new Treasure{ Id=3, Name = "Lamp", Kind="Glass", Price=5 },
        };
    }
}
