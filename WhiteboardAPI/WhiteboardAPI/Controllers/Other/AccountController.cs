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
		private readonly AccountContext _context;

		public AccountController (AccountContext context)
		{
			_context = context;
		}

		private static Random random = new Random();

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
			if(account._email == null || account._name == null || account._email == "" || account._name == "") {
				return BadRequest("No name/email provided.");
			}

			var accNameScreen = account._name;

			var newAccount = new Account { _name = accNameScreen };
			newAccount._email = account._email;

			int newIdAttempt = random.Next();

			// Check if id is unique
			if (_context.Accounts.Any(o => o._id == newIdAttempt)) {
				return BadRequest("Internal account ID already exists, try again.");
			}

			newAccount._id = newIdAttempt;
			var newMemberAccId = new MemberAccountId { accountIdNumber = newAccount._id };

			// Save
			try {
				_context.Accounts.Add(newAccount);
				//_context.MemberAccountIds.Add(newMemberAccId);
				await _context.SaveChangesAsync();
			} catch (Exception e) {
				return BadRequest(e);
			}

			return CreatedAtAction("Create", newAccount);
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
			var retStatus = false;

			if (accToJoin == null || courseToJoin == null) {
				return NotFound();
			}
			// TODO REMOVE COMMENT
			/*
			if (accToJoin.JoinedClasses.Count == 0) {
				accToJoin.JoinedClasses.Enqueue(courseToJoin);
				retStatus = true;
			} else if (!accToJoin.JoinedClasses.Contains(courseToJoin)) {
				accToJoin.JoinedClasses.Enqueue(courseToJoin);
				retStatus = true;
			}
			*/
			//_context.Entry(accToJoin).State = EntityState.Modified;
			// this shit breaks it for some reason ^
			await _context.SaveChangesAsync();

			return Ok(retStatus);
		}
		
		[HttpPut("leave/{accId}/{classId}")]
		public async Task<IActionResult> LeaveClass (int classId, int accId)	{

			var accToLeave = await _context.Accounts.FindAsync(accId);
			var courseToLeave = await _context.JoinedClassIds.FindAsync(classId);

			if (accToLeave == null || courseToLeave == null) {
				return NotFound();
			}

			//accToLeave.LeaveClass(courseToLeave);

			_context.Entry(accToLeave).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return Ok(classId);
		}
		/*
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
		*/
	}
}
