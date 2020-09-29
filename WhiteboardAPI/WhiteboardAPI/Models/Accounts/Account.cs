using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WhiteboardAPI.Models.Accounts {
    public class Account {
        public string _name { get; set; }
        public string _email { get; set; }
        //private List<long> classesJoined;
        [Key]
        public long _id { get; set; }
    }
}
