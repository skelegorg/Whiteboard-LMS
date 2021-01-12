using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteboardAPI.Data.Other;
using WhiteboardAPI.Models.Assignments;
using Microsoft.EntityFrameworkCore;

namespace WhiteboardAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AnnouncementController : ControllerBase
	{
		private readonly Context _context;

		public AnnouncementController(Context context)
		{
			_context = context;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Announcement>> GetByID(long id)
		{
			var returnAnnouncement = await _context.Announcements.FindAsync(id);

			if(returnAnnouncement == null)
			{
				return NotFound();
			}

			return returnAnnouncement;
		}

		[HttpPost]
		public async Task<IActionResult> Create(Announcement announce)
		{
			_context.Announcements.Add(announce);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetByID), new { id = announce._id }, announce);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(long id, Announcement announce)
		{
			if (id != announce._id)
			{
				return BadRequest();
			}

			_context.Entry(announce).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(long id)
		{
			var announce = await _context.Announcements.FindAsync(id);

			if (announce == null)
			{
				return NotFound();
			}

			_context.Announcements.Remove(announce);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
