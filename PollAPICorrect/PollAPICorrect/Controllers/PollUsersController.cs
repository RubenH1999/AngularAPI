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
    public class PollUsersController : ControllerBase
    {
        private readonly UserContext _context;

        public PollUsersController(UserContext context)
        {
            _context = context;
        }

        // GET: api/PollUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PollUser>>> GetPollUsers()
        {
            return await _context.PollUsers.ToListAsync();
        }

        // GET: api/PollUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PollUser>> GetPollUser(int id)
        {
            var pollUser = await _context.PollUsers.FindAsync(id);

            if (pollUser == null)
            {
                return NotFound();
            }

            return pollUser;
        }

        [HttpGet("getUserPolls/{userID}")]
        //returnt alle polls waar de User die ingelogt is aan deel kan nemen
        public async Task<ActionResult<IEnumerable<PollUser>>> GetUserPolls(int userID)
        {
            //returns de pollUsers Incl de polls waar ze aan meedoen waar de userID gelijk is aan de user ID die is meegegeven (current user)ook word de user megegeven
            var pollUsers = _context.PollUsers.Include(p => p.Poll).ThenInclude(a => a.Answers).ThenInclude(v => v.Votes).Where(c => c.UserID == userID);
            

            return await pollUsers.ToListAsync();
            
        }




        // PUT: api/PollUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPollUser(int id, PollUser pollUser)
        {
            if (id != pollUser.PollUserID)
            {
                return BadRequest();
            }

            _context.Entry(pollUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollUserExists(id))
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

        // POST: api/PollUsers
        [HttpPost]
        public async Task<ActionResult<PollUser>> PostPollUser(PollUser pollUser)
        {
            _context.PollUsers.Add(pollUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPollUser", new { id = pollUser.PollUserID }, pollUser);
        }

        //hier word een pollUser toegevoegd wanneer hij een nieuwe poll heeft aangemaakt of een user die word uitgenodigd
        [HttpPost ("addPollUser/{userID}/{pollID}")]
        public async Task<ActionResult<PollUser>> addPollUser(int userID, int pollID)
        {
            var pollUser = new PollUser();
            pollUser.UserID = userID;
            pollUser.PollID = pollID;

            _context.PollUsers.Add(pollUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPollUser", new { id = pollUser.PollUserID }, pollUser);
        }

        // DELETE: api/PollUsers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PollUser>> DeletePollUser(int id)
        {
            var pollUser = await _context.PollUsers.FindAsync(id);
            if (pollUser == null)
            {
                return NotFound();
            }

            _context.PollUsers.Remove(pollUser);
            await _context.SaveChangesAsync();

            return pollUser;
        }

        private bool PollUserExists(int id)
        {
            return _context.PollUsers.Any(e => e.PollUserID == id);
        }
    }
}
