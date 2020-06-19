using System.Data.Entity;
using Database.DataAccess.Entities.Base;
using Database.DataAccess.Entities.Building;
using Database.DataAccess.Entities.Competitions;
using Database.DataAccess.Entities.Competitions.Members;
using Database.DataAccess.Entities.Relations;
using Database.DataAccess.Entities.Sports;
using Database.DataAccess.Entities.SportsMans;
using Database.DataAccess.Entities.Trainers;

namespace Database.DataAccess.Entities
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