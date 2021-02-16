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

	public class JoinedClassId {
		[Key]
		public int _id { get; set; }
		public int classIdNumber { get; set; }
		public int AccId { get; set; } // foreign key

		public Account Account { get; set; }
		// :middle_finger:
	}

	public class CourseDto {
		public string courseName { get; set; }
		public string ownerName { get; set; }
		public int courseMembercount { get; set; }
	}
}
