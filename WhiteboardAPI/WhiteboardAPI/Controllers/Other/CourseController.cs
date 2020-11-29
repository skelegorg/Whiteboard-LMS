using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
		public async Task<ActionResult<Course>> GetById (int id) {

			var course = await _context.Courses.FindAsync(id);

			if (course == null) {
				return NotFound();
			}
			
			return Ok(course);
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create (Course course) {
			// New object, assign all user-determined values

			// Only allow the name to carry through to prevent a 
			//user-determined id or join code from being generated
			if(course.className == "" || course.className == null ) {
				return BadRequest("No classname provided");
			}
			var courseNameScreen = course.className;

			var newCourse = new Course { className = courseNameScreen };

			// Generate new join code
			char[] stringChars = new char[8];

			for (int i = 0; i < stringChars.Length; i++) {
				stringChars[i] = chars[random.Next(chars.Length)];
			}

			string newCode = new string(stringChars);
			newCourse.joinCode = newCode;

			// Generate new ID
			int newIDAttempt = random.Next();

			// Check if the ID is unique or already taken
			if(_context.Courses.Any(o => o._id == newIDAttempt)) {
				return BadRequest("Internal class ID already exists- try again");
			}

			newCourse._id = newIDAttempt;

			// Save changes
			try {
				_context.Courses.Add(newCourse);
				await _context.SaveChangesAsync();
			} catch (Exception e) {
				return BadRequest(e);
			}
			

			// Return 201
			return CreatedAtAction("Create", newCourse);
		}

		[HttpPost("regenerateCode/{id}")]
		public async Task<IActionResult> RegenerateJoinCode (int id) {

			var course = await _context.Courses.FindAsync(id);
			string currentCode = course.joinCode;
			char[] stringChars = new char[8];

			for (int i = 0; i < stringChars.Length; i++) {
				// generate yourself a brand spanking new join code
				stringChars[i] = chars[random.Next(chars.Length)];
			}

			string newCode = new string(stringChars);

			// If the brand spanking new join code tm is not, in fact, new
			// reset that rebellious little string of characters
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
		public async Task<IActionResult> Update(int id, Course course) {
			if (id != course._id) {
				return BadRequest();
			}

			_context.Entry(course).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();

		}

		[HttpPut("{classId}/addMember/{accountId}")]
		public async Task<IActionResult> AddMember(int classId, int accountId) {

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
		public async Task<IActionResult> RemoveMember(int classId, int accountId) {

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
		public async Task<IActionResult> Delete(int id) {
			var course = await _context.Courses.FindAsync(id);

			if (course == null) {
				return NotFound();
			}

			_context.Courses.Remove(course);

			return NoContent();
		}
	}
}
