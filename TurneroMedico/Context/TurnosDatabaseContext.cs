using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TurneroMedico.Models;
namespace TurneroMedico.Context
{
    public class TurnosDatabaseContext : DbContext
    {
        public TurnosDatabaseContext(DbContextOptions<TurnosDatabaseContext> options) : base(options)
        {
        }
        public DbSet<TurnosModel> Turnos { get; set; }
    }
}