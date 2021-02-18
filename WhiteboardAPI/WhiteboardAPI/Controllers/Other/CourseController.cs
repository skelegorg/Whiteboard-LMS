using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using WhiteboardAPI.Models.Accounts;
using WhiteboardAPI.Data.Other;
using WhiteboardAPI.Resources;
using WhiteboardAPI.Models.Classrooms;
using Microsoft.AspNetCore.Mvc;

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

		[HttpGet("byname/{name}/{resultCount}")]
		public async Task<IActionResult> GetByName(string name, int resultCount) {
			//new dto that just includes course name, owner, and number of members
			//
			// only add the first [resultCount] results, if more requested, the program *should* not arbitrarily
			// determine the order of results, so the next set should follow the first logically. TODO: decide what
			// determines the order returned in. this should take place frontend to reduce request time.
			
			List<CourseDto> returnList = new List<CourseDto> { };

			var returns = _context.Courses.Where(course => course.className == name).Take(resultCount).ToList();
			 
			foreach(Course course in returns) {
				var ownerNameObj = await _context.Accounts.FindAsync(course.classOwnerAccId);
				var newDtoObj = new CourseDto { courseName = course.className, ownerName = ownerNameObj._name, courseMembercount = course.joinedMemberIds.Count };
				returnList.Add(newDtoObj);
			}

			return Ok(returnList);
		}

		[HttpPost("Create/{classOwner}")]
		public async Task<IActionResult> Create (Course course, int classOwner) {
			// New object, assign all user-determined values

			// Only allow the name to carry through to prevent a 
			// user-determined id or join code from being generated
			if(course.className == "" || course.className == null ) {
				return BadRequest("No classname provided");
			}
			var courseNameScreen = course.className;

			var newCourse = new Course { className = courseNameScreen };

			newCourse.classOwnerAccId = classOwner;

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
				return BadRequest("Internal ID already exists- try again");
			}
			
			newCourse._id = newIDAttempt;

			// Save changes
			try {
				_context.Courses.Add(newCourse);
				_context.JoinedClassIds.Add(new JoinedClassId { classIdNumber = newCourse._id });
				await _context.SaveChangesAsync();
			} catch (Exception e) {
				ErrorLogger.logError(e);
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
				if(accountToAdd == null) {
					return NotFound("Account not found");
				} else {
					return NotFound("Class not found");
				}
			}

			bool result = classToJoin.UserJoinsClass(ref accountToAdd);
			
			_context.Entry(classToJoin).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			try {
				await _context.SaveChangesAsync();
			} catch (Exception e) {
				ErrorLogger.logError(e);
			}

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

			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
