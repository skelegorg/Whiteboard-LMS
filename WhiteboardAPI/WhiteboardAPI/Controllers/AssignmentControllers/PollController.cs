using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhiteboardAPI.Data.Other;
using WhiteboardAPI.Models.Assignments;
using WhiteboardAPI.Resources;

namespace WhiteboardAPI.Controllers {

	[ApiController]
	[Route("[controller]")]
	public class PollController : ControllerBase {

		private readonly Context _context;

		public PollController(Context context) {
			_context = context;
		}

		static Random rand = new Random();

		// Adding/retracting a vote:
		public class PollVote {
			public string optName;
			public string voter;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Poll>> GetByID(int id) {

			var returnPoll = await _context.Polls.FindAsync(id);
			if (returnPoll == null) {
				return NotFound();
			}
			return returnPoll;
		}

		[HttpPost]
		public async Task<IActionResult> Create(Poll poll) {
			//TODO: code that generates id and stuff
			var newPoll = poll;
			int newIDAttempt = rand.Next();

			if (_context.Polls.Any(o => o._id == newIDAttempt)) {
				return BadRequest("Internal ID already exists- try again");
			}

			newPoll._id = newIDAttempt;

			try {
				_context.Polls.Add(newPoll);
				await _context.SaveChangesAsync();
			} catch (Exception e) {
				ErrorLogger.logError(e);
				return BadRequest(e);
			}

			return CreatedAtAction("Create", newPoll);
		}

		// TODO: decide if course obj should be replaced or if the poll settings should just be replaced

		[HttpPut("{id}")]
		public async Task<IActionResult> Edit(Poll poll, int id) {

			if (poll == null) {
				return NotFound();
			} else if (id != poll._id) {
				return BadRequest();
			}

			// why the hecc does the findasync method not return the actual object

			_context.Entry(poll).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// Adding/subtracting a vote
		[HttpPut("{id}/{func}")]
		public async Task<IActionResult> Vote(int id, string func, PollVote voteObj) {

			return Ok($"{voteObj.voter}'s vote {(func == "add" ? "added" : "removed")}");
		}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id) {
			var attemptedPoll = await _context.Polls.FindAsync(id);

			if (attemptedPoll == null) {
				return BadRequest("Poll not found");
			}

			_context.Polls.Remove(attemptedPoll);
			await _context.SaveChangesAsync();
			return Ok("Poll Deleted");
		}
	}
}
