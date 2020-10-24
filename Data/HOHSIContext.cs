using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HOHSI.Areas.Identity.Data;
using HOHSI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HOHSI.Data
{
    public class HOHSIContext : IdentityDbContext<HOHSIUser>
    {
        //Business Logic dbset entities definition
        //public virtual DbSet<Entity> Entities { get; set; }

        public HOHSIContext(DbContextOptions<HOHSIContext> options)
            : base(options)
        {
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptedExercise> PrescriptedExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<PrescriptedExercise>().HasKey(pe => new { pe.PrescriptionId, pe.ExerciseId });
                    }
    }
}

