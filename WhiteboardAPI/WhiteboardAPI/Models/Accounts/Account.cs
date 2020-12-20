// Using directives
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WhiteboardAPI.Models.Classrooms;

// This file creates a one-to-many relationship between many joined classes and one account.
// Unfortunately
// Theres no such thing as a dbset for longs :smh:

namespace WhiteboardAPI.Models.Accounts {
	public class Account {
		// Actual account info
		[Key]
		public int _id { get; set; }

		public string _name { get; set; }
		public string _email { get; set; }

		public List<JoinedClassId> JoinedClasses { get; set; } = new List<JoinedClassId> { };
		// TODO: figure out how to do a one-to-many class relationship
		// :middle_finger:

		public bool JoinClass (JoinedClassId course) {	
			if(course == null) {
				return false;
			}

			if(JoinedClasses.Count == 0) {
				this.JoinedClasses.Add(course);
			} else if (!JoinedClasses.Contains(course)) {
				this.JoinedClasses.Add(course);
			} else {
				return false;
			}

			return true;
		}

		public bool LeaveClass (JoinedClassId course) {
			if(course == null) {
				return false;
			}

			if (this.JoinedClasses.Contains(course)) {
				
				this.JoinedClasses.Remove(course);
				return true;

			} else {

				return false;
			}
		}
	}

	public class MemberAccountId {
		// Unfortunately, security is a thing
		// And you shouldn't just be able to join a random board
		//
		// f*ck
		[Key]
		public int accountIdNumber { get; set; }
	}
}
