using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WhiteboardAPI.Data.Other;
using WhiteboardAPI.Models.Classrooms;

namespace WhiteboardAPI.Controllers.Other {
	[ApiController]
	[Route("[controller]")]
	public class CourseController : ControllerBase {

		private readonly Context _context;

		public CourseController(Context context) {
			_context = context;
		}

		// for token regen purposes
		string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		private static Random random = new Random();

		[HttpGet("{id}")]
		public async Task<ActionResult<Course>> GetById (long id) {

			var course = await _context.Courses.FindAsync(id);

			if (course == null) {
				return NotFound();
			}
			
			return Ok(course);
		}

		[HttpPost]
		public async Task<IActionResult> Create (string courseName) {
			// New object, assign all user-determined values
			var newCourse = new Course { className = courseName };

			// Generate new join code
			char[] stringChars = new char[8];

			for (int i = 0; i < stringChars.Length; i++) {
				stringChars[i] = chars[random.Next(chars.Length)];
			}

			string newCode = new string(stringChars);
			newCourse.joinCode = newCode;

			// Generate new ID
			int newIDAttempt = random.Next();

			// Check if it is unique
			var checkCourse = _context.Courses.FindAsync(newIDAttempt);
			while(checkCourse != null) {
				// Repeat as long as the id is not unique (the course exists)
				newIDAttempt = random.Next();
				checkCourse = _context.Courses.FindAsync(newIDAttempt);
			}

			newCourse._id = newIDAttempt;

			// Save changes
			_context.Courses.Add(newCourse);
			await _context.SaveChangesAsync();

			// Return 201
			return CreatedAtAction(nameof(GetById), newCourse);
		}

		[HttpPost("regenerateCode/{id}")]
		public async Task<IActionResult> RegenerateJoinCode (long id) {

			var course = await _context.Courses.FindAsync(id);
			string currentCode = course.joinCode;
			char[] stringChars = new char[8];

			for (int i = 0; i < stringChars.Length; i++) {
				// generate yourself a brand spanking new join code
				stringChars[i] = chars[random.Next(chars.Length)];
			}

			string newCode = new string(stringChars);

			// If the brand spanking new join code tm is not, in fact, new
			// reset that b*tch
			if(newCode != currentCode) {
				// Save the new code, save it and pop it back
				course.joinCode = newCode;
				await _context.SaveChangesAsync();
				return Ok(newCode);

			} else {
				await RegenerateJoinCode(id);
			}

			return Ok(newCode);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(long id, Course course) {
			if (id != course._id) {
				return BadRequest();
			}

			_context.Entry(course).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();

		}

		[HttpPut("{classId}/addMember/{accountId}")]
		public async Task<IActionResult> AddMember(long classId, long accountId) {

			var accountToAdd = await _context.MemberAccountIds.FindAsync(accountId);
			var classToJoin = await _context.Courses.FindAsync(classId);

			if(accountToAdd == null || classToJoin == null) {
				return NotFound();
			}

			bool result = classToJoin.UserJoinsClass(ref accountToAdd);

			_context.Entry(classToJoin).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			await _context.SaveChangesAsync();

			return Ok(result);
		}

		[HttpPut("{classId}/removeMember/{accountId}")]
		public async Task<IActionResult> RemoveMember(long classId, long accountId) {

			var accountToAdd = await _context.MemberAccountIds.FindAsync(accountId);
			var classToJoin = await _context.Courses.FindAsync(classId);

			if(accountToAdd == null || classToJoin == null) {
				return NotFound();
			}

			bool result = classToJoin.UserLeavesClass(ref accountToAdd);

			_context.Entry(classToJoin).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			await _context.SaveChangesAsync();

			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(long id) {
			var course = await _context.Courses.FindAsync(id);

			if (course == null) {
				return NotFound();
			}

			_context.Courses.Remove(course);

			return NoContent();
		}
	}
}
