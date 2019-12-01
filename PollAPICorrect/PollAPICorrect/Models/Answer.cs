using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PollAPICorrect.Models
{
    public class Answer
    {
        public int AnswerID { get; set; }
        public string Text { get; set; }
        public int PollID { get; set; }

        public Poll Poll { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}
