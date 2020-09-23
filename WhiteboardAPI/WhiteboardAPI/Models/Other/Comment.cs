using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteboardAPI.Models.Accounts;

namespace WhiteboardAPI.Models.Other
{
	public class Comment
	{
		//comment object
		//date and time posted
		public DateTime _postTime { get; }
		//user that posted the comment
		public StudentAccount _studentAuthor { get; }
		//content of the comment
		public string _content { get; set; }
		public Comment(DateTime postTime, string content, StudentAccount studentAuthor = null)
		{
			this._postTime = postTime;
			this._studentAuthor = studentAuthor;
			this._content = content;
		}
	}
}
