using System;
using System.Linq;
using WhiteboardAPI.Models.Accounts;
using WhiteboardAPI.Data.Other;

namespace WhiteboardAPI.Data
{
	public class SeedAccountData
	{
		public static void Initialize(AccountContext context)
		{
			if (!context.Accounts.Any())
			{
				context.Accounts.AddRange(
					new Account
					{
						_id = 00000000000000000000,
						_name = "first title ahhh",
						_email = "hello, world",
					},
					new Account
					{
						_id = 00000000000000000001,
						_name = "hooray",
						_email = "yipee",
					}
				);

				context.SaveChanges();
			}
		}
	}
}
