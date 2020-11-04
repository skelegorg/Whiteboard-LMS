// Using directives
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

// This file creates a one-to-many relationship between many joined classes and one account.
// Unfortunately
// Theres no such thing as a dbset for longs :smh:

namespace WhiteboardAPI.Models.Accounts {
	public class Account {
		// Actual account info
		[Key]
		public long _id { get; set; }

		public string _name { get; set; }
		public string _email { get; set; }

		// TODO: figure out how to do a one-to-many class relationship
		// public virtual ICollection<JoinedClassId> JoinedClassIds { get; set; } = new List<JoinedClassId>{ new JoinedClassId { classIdNumber = 00000 } };
		// :middle_finger:
	}
}
