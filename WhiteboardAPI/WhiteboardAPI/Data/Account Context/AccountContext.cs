﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WhiteboardAPI.Models.Accounts;
using Microsoft.EntityFrameworkCore;


namespace WhiteboardAPI.Data
{
	public class AccountContext : DbContext
	{
		public AccountContext(DbContextOptions<AccountContext> options)
			: base(options)
		{
		}

		public DbSet<Account> Accounts { get; set; }
	}
}