using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteboardAPI.Models.Assignments;

namespace WhiteboardAPI.Data.Data_Seeds {
	public class SeedPollData {
		public static void Initialize(PollContext context) {
			if(!context.Polls.Any()) {
				context.Polls.AddRange(
					new Poll {
						_id = 0,
						content = "seed",
						question = "seed"
					}
				);
				context.SaveChanges();
			}
		}
	}
}
