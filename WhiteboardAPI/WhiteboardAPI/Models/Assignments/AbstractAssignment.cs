using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WhiteboardAPI.Models.Accounts;
using WhiteboardAPI.Models.Other;

namespace WhiteboardAPI.Models.Assignments {
	public abstract class AbstractAssignment : IComparable {
		//_dueDate set in constructor
		//list of assignees and whether or not they completed the assignment
		//list of submitted assignees and their grade (null until graded)
		//stack of comments, organized by time made
		public DateTime _dueDate { get; }
		public Dictionary<Account, bool> assigneeStatus;
		public Dictionary<Account, decimal> completedGrades;
		public Stack<Comment> comments;
		
		// The Assignment model follows the CRUD model.
		// In this case, I do not have a delete method and instead I make the object invisible. After 

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
