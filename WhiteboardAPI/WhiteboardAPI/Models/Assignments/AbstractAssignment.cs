using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WhiteboardAPI.Models.Accounts;

namespace WhiteboardAPI.Models.Assignments {
	public abstract class AbstractAssignment : IComparable {
		//_dueDate set in constructor
		public DateTime _dueDate { get; }
		//list of assignees and whether or not they completed the assignment
		public Dictionary<StudentAccount, bool> assigneeStatus;
		//list of submitted assignees and their grade (null until graded)
		public Dictionary<StudentAccount, decimal> completedGrades;
		
		// The Assignment model follows the CRUD model.
		// In this case, I do not have a delete method and instead 
		public abstract void DeleteAssignment() {
			//set all values to zero in case of failure
			_dueDate = null;
		}

		//IComparable interface for urgency algorithm
		public int CompareTo(object obj) {
			if (obj == null) {
				return 1;
			}
			AbstractAssignment otherAssignment = obj as AbstractAssignment;
			if(otherAssignment != null) {
				//algorithm
				return 1;
			} else {
				throw new ArgumentException("AbstractAssignment CompareTo failed: object not an AbstractAssignment");
			}
		}
	}
}
