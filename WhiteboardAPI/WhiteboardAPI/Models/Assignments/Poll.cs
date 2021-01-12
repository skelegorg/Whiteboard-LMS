using System;
using System.Collections.Generic;
using WhiteboardAPI.Models.Other;
using System.Linq;
using System.Threading.Tasks;

namespace WhiteboardAPI.Models.Assignments {
	public class Poll {
		// This assignment type is a simple poll, any number of options up to 10
		// can be anonymous or not (to teacher only- students can only see the number of choices on each option, 
		// not who voted which)

		/*
		/-----------------------------------\
		|  QUESTION                         |
		|  000000000000000 - opt1           |
		|  000000 - opt2                    |
		\-----------------------------------/
		*/
		public int _id { get; set; }

		public bool anonymous { get; set; }
		public DateTime lockDate { get; set; }
		// question asked
		public string question { get; set; }
		public string content { get; set; }
		public Queue<PollOption> options { get; set; }
		 
	}
}
