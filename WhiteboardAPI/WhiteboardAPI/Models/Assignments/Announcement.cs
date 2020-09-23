using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WhiteboardAPI.Models.Accounts;
using WhiteboardAPI.Models.Other;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WhiteboardAPI.Models.Assignments {
	public class Announcement {
		public string _Title { get; set; }
		public string _Content { get; set; }
		public Stack<Comment> Responses;
		[Key]
		public long _id { get; set; }
	}
}
