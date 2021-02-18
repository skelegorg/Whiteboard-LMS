using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhiteboardAPI.Models.Classrooms;
using WhiteboardAPI.Models.Assignments;
using WhiteboardAPI.Models.Accounts;

namespace WhiteboardAPI.Data.Other {
	public class Context : DbContext {
		public Context(DbContextOptions<Context> options)
			: base(options) {
		}

		public DbSet<Course> Courses { get; set; }

		public DbSet<Announcement> Announcements { get; set; }
		public DbSet<Poll> Polls { get; set; }

		public DbSet<Account> Accounts { get; set; }

		// because nothing can be easy
		public DbSet<JoinedClassId> JoinedClassIds { get; set; }
		public DbSet<MemberAccountId> MemberAccountIds { get; set; }
	}
}
