using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteboardAPI.Data;
using WhiteboardAPI.Models.Assignments;
using Microsoft.EntityFrameworkCore;
using WhiteboardAPI.Models.Accounts;
using System.Net.Sockets;
using WhiteboardAPI.Data.Other;

namespace WhiteboardAPI.Controllers.Other
{
	[ApiController]
	[Route("[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly Context _context;

		public AccountController (Context context)
		{
			_context = context;
		}

		[HttpGet]
		public ActionResult<List<Account>> GetAll() =>
			_context.Accounts.ToList();

		[HttpGet("{id}")]
		public async Task<ActionResult<Account>> GetByID(int id)
		{
			var returnAccount = await _context.Accounts.FindAsync(id);

			if (returnAccount == null)
			{
				return NotFound();
			}

			return returnAccount;
		}
		
		[HttpPost]
		public async Task<IActionResult> Create(Account account)
		{
			_context.Accounts.Add(account);
			_context.MemberAccountIds.Add(new MemberAccountId { accountIdNumber = account._id });

			await _context.SaveChangesAsync();
			
			return CreatedAtAction(nameof(GetByID), new { id = account._id }, account);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, Account account) {
			if (id != account._id) {
				return BadRequest();
			}

			_context.Entry(account).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		[HttpPut("join/{accId}/{classId}")]
		public async Task<IActionResult> JoinClass(int classId, int accId) {

			var courseToJoin = await _context.JoinedClassIds.FindAsync(classId);
			var accToJoin = await _context.Accounts.FindAsync(accId);

			if (accToJoin == null || courseToJoin == null) {
				return NotFound();
			}

			accToJoin.JoinClass(ref courseToJoin);

			_context.Entry(accToJoin).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return Ok(classId);
		}
		
		[HttpPut("leave/{accId}/{classId}")]
		public async Task<IActionResult> LeaveClass (int classId, int accId) {

			var accToLeave = await _context.Accounts.FindAsync(accId);
			var courseToLeave = await _context.JoinedClassIds.FindAsync(classId);

			if (accToLeave == null || courseToLeave == null) {
				return NotFound();
			}

			accToLeave.LeaveClass(ref courseToLeave);

			_context.Entry(accToLeave).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return Ok(classId);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var announce = await _context.MemberAccountIds.FindAsync(id);

			if (announce == null)
			{
				return NotFound();
			}

			_context.MemberAccountIds.Remove(announce);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
