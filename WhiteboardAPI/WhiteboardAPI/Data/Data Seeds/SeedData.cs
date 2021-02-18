using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WhiteboardAPI.Models.Assignments;
using WhiteboardAPI.Data.Other;

namespace WhiteboardAPI.Data
{
	public static class SeedData
	{
		public static void Initialize(AnnouncementContext context)
		{
			if (!context.Announcements.Any())
			{
				context.Announcements.AddRange(
					new Announcement
					{
						_id = 00000000000000000000,
						_Title = "first title ahhh",
						_Content = "hello, world",
					},
					new Announcement
					{
						_id = 00000000000000000001,
						_Title = "hooray",
						_Content = "yipee",
					}
				);

				context.SaveChanges();
			}
		}
	}
}