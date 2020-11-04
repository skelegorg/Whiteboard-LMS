using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WhiteboardAPI.Models.Classrooms;

namespace WhiteboardAPI.Data.Other {
	public class CourseContext : DbContext {
		public CourseContext(DbContextOptions<CourseContext> options)
			: base(options) {
		}

		public DbSet<Course> Courses { get; set; }

		public DbSet<JoinedClassId> JoinedClassIds { get; set; }
	}
}
