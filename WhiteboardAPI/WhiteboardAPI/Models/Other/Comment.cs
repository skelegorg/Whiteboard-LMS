using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteboardAPI.Models.Accounts;
using WhiteboardAPI.Models.Assignments;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WhiteboardAPI.Models.Other
{
	public class CommentContext : DbContext {
		public CommentContext(DbContextOptions<CommentContext> options)
			: base(options) {
		}

		public DbSet<Comment> Comments { get; set; }
		// public DbSet<CommentEnabledAssignmentType> Assignments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Comment>()
				.HasOne(c => c.assignment)
				.WithMany(a => a._comments)
				.HasForeignKey(c => c.assignmentId);
		}
	}
	public class Comment
	{
		[Key]
		public int _id { get; set; }
		public DateTime _postTime { get; }
		public Account _studentAuthor { get; }
		public string _content { get; set; }
		// one-to-many db relationship
		public Announcement assignment { get; set; }
		public int assignmentId { get; set; }
		public Comment()
		{
			this._postTime = DateTime.Now;
		}
	}
}
