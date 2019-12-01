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
    public class FriendshipsController : ControllerBase
    {
        private readonly UserContext _context;

        public FriendshipsController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Friendships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friendship>>> GetFriendships()
        {
            return await _context.Friendships.ToListAsync();
        }

        // GET: api/Friendships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Friendship>> GetFriendship(int id)
        {
            var friendship = await _context.Friendships.FindAsync(id);

            if (friendship == null)
            {
                return NotFound();
            }

            return friendship;
        }

        //Vrienden worden opgehaald van de persoon waarvan het UserID is
        [HttpGet ("getFriends/{userID}")]
        public async Task<ActionResult<IEnumerable<Friendship>>> GetFriends(int userID)
        {
            //return alle vriendschappen incl. de andere betrokken user waar de userId van in friendships overeenkomt met de opgegeven UserID en waar Status gelijk is aan 1 (1 staat gelijk aan vriendschapsverzoek geaccepteerd)
            return await _context.Friendships.Where(i => i.UserReceiveID == userID).Include(u => u.User).Where(s => s.Status == 1).ToListAsync();
        }
        //haalt de vrienden op wanneer jij een verzoek verstuurd hebt
        [HttpGet("getSentFriends/{userID}")]
        public async Task<ActionResult<IEnumerable<Friendship>>> GetSentFriends(int userID)
        {
            //return alle vriendschappen incl. de andere betrokken user waar de userId van in friendships overeenkomt met de opgegeven UserID en waar Status gelijk is aan 1 (1 staat gelijk aan vriendschapsverzoek geaccepteerd)
            return await _context.Friendships.Where(i => i.UserID == userID).Include(u => u.User).Where(s => s.Status == 1).ToListAsync();
        }
        //haalt de ontvangen friendrequests op
        [HttpGet("receivedRequest/{userID}")]
        public async Task<ActionResult<IEnumerable<Friendship>>> GetFriendRequest(int userID)
        {
            var users =  _context.Friendships.Where(i => i.UserReceiveID == userID).Include(u => u.User).Where(s=>s.Status==0);
            return await users.ToListAsync();
        }
        

        // PUT: api/Friendships/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriendship(int id, Friendship friendship)
        {
            if (id != friendship.FriendshipID)
            {
                return BadRequest();
            }

            _context.Entry(friendship).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendshipExists(id))
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

        // POST: api/Friendships
        [HttpPost]
        public async Task<ActionResult<Friendship>> PostFriendship(Friendship friendship)
        {
            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriendship", new { id = friendship.FriendshipID }, friendship);
        }

        [HttpPost("sendRequest/{status}/{sentID}/{receiverID}")]
        public async Task<ActionResult<Friendship>> sendRequest(int status, int sentID, int receiverID)
        {
            var friendship = new Friendship();
            friendship.Status = status;
            friendship.UserID= sentID;
            friendship.UserReceiveID = receiverID;
            

            _context.Friendships.Add(friendship);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriendship", new { id = friendship.FriendshipID }, friendship);
        }
        // DELETE: api/Friendships/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Friendship>> DeleteFriendship(int id)
        {
            var friendship = await _context.Friendships.FindAsync(id);
            if (friendship == null)
            {
                return NotFound();
            }

            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();

            return friendship;
        }

        private bool FriendshipExists(int id)
        {
            return _context.Friendships.Any(e => e.FriendshipID == id);
        }
    }
}
