using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPICorrect.Models
{
    public class PollUser
    {
        public int PollUserID { get; set; }
        public int PollID { get; set; }
        public int UserID { get; set; }
        public Poll Poll { get; set; }
        public User User { get; set; }
    }
}
