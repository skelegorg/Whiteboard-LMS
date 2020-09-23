using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhiteboardAPI.Models.Accounts {
    public abstract class AbstractAccount {
        public string type {get;}
        public string Name { get; }
    }
}