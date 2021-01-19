using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhiteboardAPI.Data.Other;
using WhiteboardAPI.Models.Assignments;

namespace WhiteboardAPI.Controllers {
	public class PollController {
		[ApiController]
		[Route("[controller]")]
		public class PollController : ControllerBase {

			private readonly Context _context;

			public PollController(Context context) {
				_context = context;
			}
		}
	}
}
