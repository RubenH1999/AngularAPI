using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PollAPICorrect.Models;

namespace PollAPICorrect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly UserContext _context;

        public VotesController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Votes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vote>>> GetVotes()
        {
            return await _context.Votes.ToListAsync();
        }

        // GET: api/Votes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vote>> GetVote(int id)
        {
            var vote = await _context.Votes.FindAsync(id);

            if (vote == null)
            {
                return NotFound();
            }

            return vote;
        }
        [HttpGet("countVotes/{answerID}")]
        public async Task<ActionResult<IEnumerable<Vote>>> countVotes(int answerID)
        {
            var vote = await _context.Votes.Where(a => a.AnswerID == answerID).Include(a => a.Answer).ToListAsync();

            
            return vote;
        }



        // PUT: api/Votes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVote(int id, Vote vote)
        {
            if (id != vote.VoteID)
            {
                return BadRequest();
            }

            _context.Entry(vote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Votes
        [HttpPost]
        public async Task<ActionResult<Vote>> PostVote(Vote vote)
        {
            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVote", new { id = vote.VoteID }, vote);
        }
        [HttpPost ("addVote/{answerID}/{userID}")]
        public async Task<ActionResult<Vote>> addVote(int answerID, int userID)
        {
            var vote = new Vote();
            vote.AnswerID = answerID;
            vote.UserID = userID;
            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetVotes", new { id = vote.VoteID }, vote);
        }

        // DELETE: api/Votes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vote>> DeleteVote(int id)
        {
            var vote = await _context.Votes.FindAsync(id);
            if (vote == null)
            {
                return NotFound();
            }

            _context.Votes.Remove(vote);
            await _context.SaveChangesAsync();

            return vote;
        }

        private bool VoteExists(int id)
        {
            return _context.Votes.Any(e => e.VoteID == id);
        }
    }
}
