using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhiteboardAPI.Models.Accounts {
    public class StudentAccount : AbstractAccount {
        public StudentAccount(string name) {
            Console.WriteLine(name);
        }
    }
}
