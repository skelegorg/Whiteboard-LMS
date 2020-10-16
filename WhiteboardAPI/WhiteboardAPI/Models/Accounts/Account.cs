using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Security.Policy;

namespace WhiteboardAPI.Models.Accounts {
	public class JoinedClassId {
		// This class allows a list of longs to exist in an albeit roundabout fashion -_-
		[Key]
		public long classIdNumber { get; set; }
		// F*ck the garbage collector
		//   |
		//   v
		public Account Account { get; set; }
	}

	public class Account {
		// Actual account info
		[Key]
		public long _id { get; set; }

		public string _name { get; set; }
		public string _email { get; set; }

		public virtual ICollection<JoinedClassId> JoinedClassId { get; set; }
	}

}
