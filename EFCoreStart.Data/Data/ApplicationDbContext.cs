using System;
using EFCoreStart.Data.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace EFCoreStart.Data.Data
{
	public class ApplicationDbContext : DbContext
	{

		public ApplicationDbContext()
		{
		}

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		public DbSet<ProjectPlanDto> ProjectPlan { get; set; }
		public DbSet<ProjectPlanDetailsDto> ProjectPlanDetails { get; set; }
	}
}

