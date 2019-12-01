using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPICorrect.Models
{
    public class Vriendschap
    {
        public int VriendschapID { get; set; }
        public int Status { get; set; }

        public int UserSentID { get; set; }
        public int UserReceiveID { get; set; }

        public User User { get; set; }
    }
}
