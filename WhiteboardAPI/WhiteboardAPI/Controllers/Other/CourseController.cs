using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using WhiteboardAPI.Data;
using WhiteboardAPI.Models.Classrooms;
using Microsoft.EntityFrameworkCore;
using WhiteboardAPI.Models.Accounts;
using WhiteboardAPI.Data.Other;
using Microsoft.AspNetCore.Identity;
using System.Configuration;
using Microsoft.AspNetCore.SignalR;

namespace WhiteboardAPI.Controllers.Other {
	[ApiController]
	[Route("[controller]")]
	public class CourseController : ControllerBase {

		private readonly CourseContext _context;

		public CourseController(CourseContext context) {
			_context = context;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Course>> GetById (long id) {

			var course = await _context.Courses.FindAsync(id);

			if (course == null) {
				return NotFound();
			}
			
			return Ok(course);
		}

		[HttpPost]
		public async Task<IActionResult> Create (Course course) {
			// Add JoinedClassId
			_context.Courses.Add(course);
			_context.JoinedClassIds.Add(new JoinedClassId { classIdNumber = course._id });

			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetById), new { id = course._id }, course);
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
