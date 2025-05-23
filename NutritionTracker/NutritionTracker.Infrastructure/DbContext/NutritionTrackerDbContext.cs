﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutritionTracker.Domain.DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutritionTracker.Infrastructure.DbContext
{
    public class NutritionTrackerDbContext : IdentityDbContext<IdentityUser>
    {
		public NutritionTrackerDbContext(DbContextOptions<NutritionTrackerDbContext> options) : base(options)
		{
			
		}

		public DbSet<DomainEvent> Events { get; set; }

		public DbSet<FoodEntity> Foods { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<FoodEntity>()
				.HasIndex(f => f.Name);
		}
	}
}
