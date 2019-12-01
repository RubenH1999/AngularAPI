using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPICorrect.Models
{
    public class Friendship
    {
        public int FriendshipID { get; set; }
        public int Status { get; set; }

        public int UserID { get; set; }
        public int UserReceiveID { get; set; }

        public User User { get; set; }
    }
}
