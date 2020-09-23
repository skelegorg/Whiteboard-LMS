using System;
using Microsoft.EntityFrameworkCore;
using WhiteboardAPI.Models.Assignments;
using System.Collections.Generic;

namespace WhiteboardAPI.Data
{
	public class AnnouncementContext : DbContext
	{
		public AnnouncementContext(DbContextOptions<AnnouncementContext> options)
			: base(options)
		{
		}

		public DbSet<Announcement> Announcements { get; set; }
	}
}
