using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WhiteboardAPI.Models.Classrooms {
	public class Course {
		[Key]
		public long _id { get; set; }
		
		public List<long> joinedMemberIds { get; set; }
	}

	public class JoinedClassId {
		// This class allows a list of longs to exist in an albeit roundabout fashion -_-
		[Key]
		public long classIdNumber { get; set; }
	}

}
