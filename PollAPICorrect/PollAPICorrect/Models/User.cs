using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPICorrect.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [NotMapped]
        public string Token { get; set; }

        public ICollection<PollUser> PollUsers { get; set; }
        public ICollection<Vote> Votes { get; set; }

        public ICollection<Friendship> Friendships { get; set; }
        
    }
}
