using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WhiteboardAPI.Models.Accounts {
	public class JoinedClassId {
		[Key]
		public long classIdNumber { get; set; }
	}

	public class Account
	{
		public string _name { get; set; }
		public string _email { get; set; }
		// F*ck the garbage collector
		// Placeholder JoinedClassId is there to prevent the list from being deleted
		public List<JoinedClassId> _classesJoined { get; set; } = new List<JoinedClassId> { new JoinedClassId { classIdNumber = 999999999999999 } };
		//public List<long> _classesJoined { get; set; } = new List<long> { 99999999999999 };
		[Key]
		public long _id { get; set; }
		
		public bool joinClass(long classId)
		{
			this._classesJoined.ToList().Add(new JoinedClassId { classIdNumber = classId });
			// Delete 999999999999
			foreach (JoinedClassId board in this._classesJoined.ToList()) {
				if(board.classIdNumber == classId) {
					this._classesJoined.Add(board);
				}
			}
			return true;
		}
		
		public bool leaveClass(long classId)
		{
			bool leaveStatus = false;
			foreach(JoinedClassId board in this._classesJoined.ToList()) {
				if(board.classIdNumber == classId) {
					this._classesJoined.Remove(board);
					leaveStatus = true;
				}
			}
			return leaveStatus; 
		}
	}
}
