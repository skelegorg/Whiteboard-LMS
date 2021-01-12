using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhiteboardAPI.Models.Other {
	public class PollOption {
		public string optName { get; set; }
		public int votes { get; set; }
		public List<string> voterNames { get; set; }
		//TODO: add color?
	}
}
