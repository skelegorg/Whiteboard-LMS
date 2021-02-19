using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WhiteboardAPI.Models.Classrooms;
using Microsoft.EntityFrameworkCore;
using System.Linq;

// This file creates a one-to-many relationship between many joined classes and one account.
// Unfortunately
// Theres no such thing as a dbset for longs :smh:

namespace WhiteboardAPI.Models.Accounts {

	public class AccountContext : DbContext {
		public AccountContext(DbContextOptions<AccountContext> options)
			: base(options) {
		}

		public DbSet<Account> Accounts { get; set; }
		public DbSet<JoinedClassId> JoinedClassIds { get; set; }
		// bc nothing can be simple, can't wait for efcore 5

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Account>()
				.HasMany(acc => acc.JoinedClasses)
				.WithOne(jcid => jcid.Account)
				.HasForeignKey(p => p.AccId);
		}
	}

	public class Account {
		// Actual account info
		[Key]
		public int _id { get; set; }

		public string _name { get; set; }
		public string _email { get; set; }

		public List<JoinedClassId> JoinedClasses { get; set; }


		public bool JoinClass(JoinedClassId course) {
			if (course == null) {
				return false;
			}

			if (JoinedClasses.Count == 0) {
				this.JoinedClasses.Add(course);
			} else if (!JoinedClasses.Contains(course)) {
				this.JoinedClasses.Add(course);
			} else {
				return false;
			}

			return true;
		}

		public bool LeaveClass(JoinedClassId course) {
			if (course == null) {
				return false;
			}

			if (this.JoinedClasses.Contains(course)) {

				this.JoinedClasses = new List<JoinedClassId>(this.JoinedClasses.Where(x => x != course));
				return true;

			} else {

				return false;
			}
		}

	}

	public class JoinedClassId {
		[Key]
		public int _id { get; set; }
		public int classIdNumber { get; set; }
		public int AccId { get; set; } // foreign key

		public Account Account { get; set; }
		// :middle_finger:
	}

	public class MemberAccountId {
		[Key]
		public int _id { get; set; }
		public int accountIdNumber { get; set; }
		public Course Course { get; set; }
		public int CourseId { get; set; }
	}
}

