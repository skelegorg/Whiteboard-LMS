using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteboardAPI.Resources;

namespace WhiteboardAPI.Models.Other {
	public class PollOption {
		public string optName { get; set; }
		public int votes { get; set; }
		public List<stringContainer> voterNames { get; set; }
		//TODO: add color?
	}
}
