// Using directives
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WhiteboardAPI.Models.Accounts;

namespace WhiteboardAPI.Models.Classrooms {
	public class CourseContext : DbContext {
		public CourseContext(DbContextOptions<CourseContext> options)
			: base(options) {
		}

		public DbSet<Course> Courses { get; set; }
		public DbSet<Account> Accounts { get; set; }

		public DbSet<JoinedClassId> JoinedClassIds { get; set; }
		public DbSet<MemberAccountId> MemberAccountIds { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Course>()
				.HasMany(c => c.joinedMemberIds)
				.WithOne(jmi => jmi.Course)
				.HasForeignKey(jmi => jmi.CourseId);
		}
	}

	public class Course {
		[Key]
		public int _id { get; set; }
		
		public List<MemberAccountId> joinedMemberIds { get; set; }

		// Basic information
		public string className { get; set; }
		public int classOwnerAccId { get; set; }

		// 8 letter/number long code
		public string joinCode { get; set; }

		public bool UserJoinsClass (ref MemberAccountId accountToAdd) {

			if(accountToAdd == null) {
				return false;
			}

			this.joinedMemberIds.Add(accountToAdd);

			return true;
		}

		public bool UserLeavesClass (ref MemberAccountId accountToRemove) {
			if (accountToRemove == null) {
				
				return false;
			}

			if (this.joinedMemberIds.Contains(accountToRemove)) {
				
				this.joinedMemberIds.Remove(accountToRemove);
				return true;

			} else {
				return false;
			}
		}
	}

	public class CourseDto {
		public string courseName { get; set; }
		public string ownerName { get; set; }
		public int courseMembercount { get; set; }
	}
}
