using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pecanha.Domain.Entity;
using System;
using System.Linq;

namespace Pecanha.Repository.Context {
    public class SceneContext : DbContext, ISceneContext {
        private static string connectionString;        
        public SceneContext(DbContextOptions<SceneContext> options, IConfiguration configuration)
                : base(options) {            
            if (connectionString is null) {
                // Para acesso do migrations à connectionstring
                connectionString = configuration.GetSection("ConnectionStrings:sqlLiteConnection").Value;
            }           
        }

        public DbSet<Scene> Scene { get; set; }
        public DbSet<RecordHistory> RecordHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SceneContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {           
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlite(connectionString);
            }

            optionsBuilder.UseSqlite(connectionString);
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        }

        public override int SaveChanges() {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("RegisteringDate") != null)) {
                if (entry.State == EntityState.Added) {
                    entry.Property("RegisteringDate").CurrentValue = DateTime.Now;
                    entry.Property("LastUpdate").CurrentValue = DateTime.Now;
                } else if (entry.State == EntityState.Modified) {
                    entry.Property("RegisteringDate").IsModified = false;
                    entry.Property("Id").IsModified = false;
                    entry.Property("LastUpdate").CurrentValue = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }

    }
}
