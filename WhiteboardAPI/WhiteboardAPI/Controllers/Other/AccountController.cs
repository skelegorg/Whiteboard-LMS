﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteboardAPI.Data;
using WhiteboardAPI.Models.Assignments;
using Microsoft.EntityFrameworkCore;
using WhiteboardAPI.Models.Accounts;
using System.Net.Sockets;

namespace WhiteboardAPI.Controllers.Other
{
	[ApiController]
	[Route("[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly AccountContext _context;

		public AccountController (AccountContext context)
		{
			_context = context;
		}

		[HttpGet]
		public ActionResult<List<Account>> GetAll() =>
			_context.Accounts.ToList();

		[HttpGet("{id}")]
		public async Task<ActionResult<Account>> GetByID(long id)
		{
			var returnAccount = await _context.Accounts.FindAsync(id);

			if (returnAccount == null)
			{
				return NotFound();
			}

			return returnAccount;
		}
		
		[HttpGet("classes/{id}")]
		public async Task<IActionResult> GetClassesJoined(long id) {
			var returnAccount = await _context.Accounts.FindAsync(id);
			List<JoinedClassId> returnList;
			returnList = returnAccount._classesJoined;
			return Ok(returnList);
		}
		
		[HttpPost]
		public async Task<IActionResult> Create(Account account)
		{
			_context.Accounts.Add(account);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetByID), new { id = account._id }, account);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(long id, Account account) {
			if (id != account._id) {
				return BadRequest();
			}

			_context.Entry(account).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}
		
		[HttpPut("join/{id}/{classId}")]
		public async Task<IActionResult> JoinClass(long id, long classId) {

			var account = await _context.Accounts.FindAsync(id);
			//TODO add account to member list in class object

			account.joinClass(classId);

			_context.Entry(account).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return Ok(account);
		}
		
		[HttpPut("leave/{id}/{classId}")]
		public async Task<IActionResult> LeaveClass(long id, long classId)
		{
			var account = await _context.Accounts.FindAsync(id);
			//TODO remove account from member list in class object
			
			account.leaveClass(classId);
			
			_context.Entry(account).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			
			return Ok(account);
		}
		
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(long id)
		{
			var announce = await _context.Accounts.FindAsync(id);

			if (announce == null)
			{
				return NotFound();
			}

			_context.Accounts.Remove(announce);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}