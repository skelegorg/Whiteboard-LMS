using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WhiteboardAPI.Models.Accounts;
using WhiteboardAPI.Models.Other;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WhiteboardAPI.Models.Assignments {
	public class AnnouncementContext : DbContext {
		public AnnouncementContext(DbContextOptions<AnnouncementContext> options)
			: base(options) {
		}

		public DbSet<Announcement> Announcements { get; set; }
	}

	public class Announcement {
		public string _Title { get; set; }
		public string _Content { get; set; }
		public Stack<Comment> _comments { get; set; }

		[Key]
		public int _id { get; set; }
	}
}
