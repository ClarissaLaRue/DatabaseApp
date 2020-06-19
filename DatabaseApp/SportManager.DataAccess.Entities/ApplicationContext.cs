using System.Data.Entity;
using Database.DataAccess.Entities.Trainers;
using SportManager.DataAccess.Entities.Base;
using SportManager.DataAccess.Entities.Building;
using SportManager.DataAccess.Entities.Competitions;
using SportManager.DataAccess.Entities.Competitions.Members;
using SportManager.DataAccess.Entities.Relations;
using SportManager.DataAccess.Entities.Sports;
using SportManager.DataAccess.Entities.SportsMans;

namespace SportManager.DataAccess.Entities
{
    public class ApplicationContext : DbContextBase
    {
        public DbSet<SportBuilding> SportBuildings { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<SportClub.SportClub> SportClubs { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<SportMan> SportMen { get; set; }
        public DbSet<Trainer> Trainers { get; set; } 
        
        
        public ApplicationContext()
            : base("DefaultConnection")
        {
            
        }

    }
}