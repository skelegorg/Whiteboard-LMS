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
		public Account _studentAuthor { get; }
		//content of the comment
		public string _content { get; set; }
		public Comment(string content, Account author = null)
		{
			this._postTime = DateTime.Now;
			this._studentAuthor = author;
			this._content = content;
		}
	}
}
