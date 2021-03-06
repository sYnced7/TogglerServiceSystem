﻿using TogglerApi.Models.Toggle;
using TogglerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace TogglerApi.Context
{
    public class ToggleContext : DbContext
    {
        public ToggleContext(DbContextOptions<ToggleContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Toggles in system
        /// </summary>
        /// <value></value>
        public DbSet<Toggle> Toggles { get; set; }

        /// <summary>
        /// Services in system
        /// </summary>
        /// <value></value>
        public DbSet<Service> Services { get; set; }

        /// <summary>
        /// Toggle states in system
        /// </summary>
        /// <value></value>
        public DbSet<ToggleState> States { get; set; }

        /// <summary>
        /// Save Changes
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Alters the instance to add tracking columns
        /// </summary>
        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            TrackableUtils.manageTrackableColumns(entries);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Toggle>()
                .HasAlternateKey(a => a.Key);
            modelBuilder.Entity<Service>()
                .HasAlternateKey(a => a.Key);
        }
    }
}
