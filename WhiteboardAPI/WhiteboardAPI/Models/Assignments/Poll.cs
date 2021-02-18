using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WhiteboardAPI.Models.Classrooms;
using WhiteboardAPI.Models.Other;
using System.Linq;
using System.Threading.Tasks;

namespace WhiteboardAPI.Models.Assignments {

	public class PollContext : DbContext {
		public PollContext(DbContextOptions<PollContext> options)
			: base(options) {
		}

		public DbSet<Poll> Polls { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<PollOption> PollOptions { get; set; }
		// TODO: check if a member is enrolled in the class that the poll is part of

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<Poll>()
				.HasMany(poll => poll.options)
				.WithOne(jcid => jcid.Poll)
				.HasForeignKey(p => p.PollId);

			modelBuilder.Entity<PollOption>()
				.HasMany(pollOpt => pollOpt.voterNames)	
				.WithOne(vname => vname.PollOption)
				.HasForeignKey(v => v.PollOptId);				
	    }
	}

	public class Poll {

		// This assignment type is a simple poll, any number of options up to 10
		// can be anonymous or not (to teacher only- students can only see the number of choices on each option, 
		// not who voted which)

		/*
		/-----------------------------------\
		|  QUESTION                         |
		|  000000000000000 - opt1           |
		|  000000 - opt2                    |
		\-----------------------------------/
		*/
		[Key]
		public int _id { get; set; }

		public int classId { get; set; }
		public bool anonymous { get; set; }
		public DateTime lockDate { get; set; }
		// question asked
		public string question { get; set; }
		public string content { get; set; }
		public Queue<PollOption> options { get; set; }
		 
	}
	
	public class voterName {
		[Key]
		public int _id { get; set; }
		// for the one-to-many relationship
		public PollOption PollOption { get; set; }
		public int PollOptId { get; set; }
		public string name { get; set; }

	}
	
	public class PollOption {
		[Key]
		public int _id { get; set; } 
		public string optName { get; set; }
		public int votes { get; set; }
		// for the one-to-many relationship
		public Poll Poll { get; set; }
		public int PollId { get; set; }
		public List<voterName> voterNames { get; set; }
		//TODO: add color?
	}
}
