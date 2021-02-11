using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhiteboardAPI.Models.Classrooms;
using WhiteboardAPI.Models.Accounts;

namespace WhiteboardAPI.Data.Other {
	public class CourseContext : DbContext {
		public CourseContext(DbContextOptions<CourseContext> options)
			: base(options) {
		}

		public DbSet<Course> Courses { get; set; }
		public DbSet<Account> Accounts { get; set; }

		public DbSet<JoinedClassId> JoinedClassIds { get; set; }
		public DbSet<MemberAccountId> MemberAccountIds { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<JoinedClassId>()
				.HasNoKey();
			modelBuilder.Entity<MemberAccountId>()
				.HasNoKey();
			modelBuilder.Entity<Course>(
				eb =>
				{
					eb.HasMany(co => co.joinedMemberIds);
				}
			);
			modelBuilder.Entity<Account>()
				.HasMany(acc => acc.JoinedClasses);
		}
	}
}
