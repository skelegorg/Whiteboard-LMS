using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WhiteboardAPI.Resources;
using WhiteboardAPI.Models.Accounts;
using WhiteboardAPI.Models.Other;

namespace WhiteboardAPI.Models.Assignments {
	public abstract class AbstractAssignment : IComparable {
		//_dueDate set in constructor
		//list of assignees and whether or not they completed the assignment
		//list of submitted assignees and their grade (null until graded)
		//stack of comments, organized by time made
		public DateTime _assignedDate { get; set; }
		public DateTime _dueDate { get; set; }
		public Dictionary<MemberAccountId, bool> _assigneeStatus;
		public Dictionary<MemberAccountId, decimal> _completedGrades;
		public Stack<Comment> _comments;
		public int _pointsOutOf;
		
		// The Assignment model follows the CRUD model.
		// In this case, I do not have a delete method and instead I make the object invisible. After 

		// IComparable interface for urgency algorithm
		public int CompareTo(object obj) {
			if (obj == null) {
				return 1;
			}
			AbstractAssignment otherAssignment = obj as AbstractAssignment;
			int compDate = DateTime.Compare(this._dueDate, otherAssignment._dueDate);
			if(otherAssignment != null) {
				if(compDate < 0) {
					// this assignment obj is more urgent/due sooner
					if (this._pointsOutOf > otherAssignment._pointsOutOf) {
						// this assignment obj is worth more points than the other
						// most urgent, due sooner and more points
						return 2;
					} else if (this._pointsOutOf == otherAssignment._pointsOutOf) {
						return 1;
					} else {
						// return a slightly more urgent
						return 1;
					}
				} else if (compDate > 0) {
					if(this._pointsOutOf < otherAssignment._pointsOutOf) {
						// least urgent
						return -2;
					} else {
						// slightly less urgent
						return -1;
						}
				} else {
					if (this._pointsOutOf == otherAssignment._pointsOutOf) {
						// equally urgent
						return 0;
					} else if (this._pointsOutOf > otherAssignment._pointsOutOf) {
						// slightly more urgent
						return 1;
					} else {
						// slightly less urgent
						return -1;
					}
				}
				
			} else {
				ErrorLogger.logError(new ArgumentException("AbstractAssignment CompareTo failed: object not an AbstractAssignment"));
				return 0;
			}
		}
	}
}
