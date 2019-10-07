using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TotalSynergyWebApi.Models.DataModels;
using TotalSynergyWebApi.Models.Interfaces;

namespace TotalSynergyWebApi.Models
{
    public class ProjectDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectContactItem> ProjectContactItems { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public ProjectDbContext(DbContextOptions<ProjectDbContext> Option) : base(Option)
        {


        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectContactItem>().HasOne<Project>(pi => pi.Project as Project).WithMany(p => p.ProjectContactItems as IEnumerable<ProjectContactItem>).HasForeignKey(si => si.ProjectId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProjectContactItem>().HasOne<Contact>(pi => pi.Contact as Contact).WithMany(c => c.ProjectContactItems as IEnumerable<ProjectContactItem>).OnDelete(DeleteBehavior.Restrict);

        }


    }
}
