// Using directives
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WhiteboardAPI.Models.Accounts;

namespace WhiteboardAPI.Models.Classrooms {
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


	// Primitive data types need a table for themselves this is rediculous
	// Also for my future self I need to gripe about keyless data types which are
	// in the EFCore documentation - a preemptively documented a feature that won't be added until
	// version 5 of EFCore, as of now EFCore 3.1.9 is the latest version (11 / 5 / 2020)
	//
	// its stupid
	[Keyless]
	public class JoinedClassId {
		// This class allows a list of longs to exist in an albeit roundabout fashion -_-
		public int classIdNumber { get; set; }
		// :middle_finger:
	}

	public class CourseDto {
		public string courseName { get; set; }
		public string ownerName { get; set; }
		public int courseMembercount { get; set; }
	}
}
