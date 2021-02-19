using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteboardAPI.Models.Other;

namespace WhiteboardAPI.Data.Data_Seeds {
	public class SeedCommentData {
		public static void Initialize(CommentContext context) {
			if (!context.Comments.Any()) {
				context.Comments.AddRange(
					new Comment
					{
						_id = 0,
						_content = "content",
					}
				);
				context.SaveChanges();
			}
		}
	}
}
