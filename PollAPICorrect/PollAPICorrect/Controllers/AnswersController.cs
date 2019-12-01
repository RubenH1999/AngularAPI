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
    public class AnswersController : ControllerBase
    {
        private readonly UserContext _context;

        public AnswersController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Answers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswers()
        {
            return await _context.Answers.ToListAsync();
        }

        // GET: api/Answers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> GetAnswer(int id)
        {
            var answer = await _context.Answers.FindAsync(id);

            if (answer == null)
            {
                return NotFound();
            }

            return answer;
        }

        // GET: api/Answers/5
        [HttpGet("countVotes/{answerID}")]
        public async Task<ActionResult<IEnumerable<Vote>>> CountAnswers(int answerID)
        {
            var answer = await _context.Votes.Where(a => a.AnswerID == answerID).ToListAsync();

            return answer;
        }

        // PUT: api/Answers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswer(int id, Answer answer)
        {
            if (id != answer.AnswerID)
            {
                return BadRequest();
            }

            _context.Entry(answer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswerExists(id))
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

        // POST: api/Answers
        [HttpPost]
        public async Task<ActionResult<Answer>> PostAnswer(Answer answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnswer", new { id = answer.AnswerID }, answer);
        }
        //Geeft het poll ID samen met het antwoord zodat de correcte antwoorden bij de correcte poll terrecht komen
        [HttpPost("{pollID}/{answer}")]
        public async Task<ActionResult<Answer>> addAnswer(int pollID, string answer)
        {
            //nieuw veriable antwoord aanmaken en hier de binnengekregen parameters met meegeven
            var antwoord = new Answer();
            antwoord.PollID = pollID;
            antwoord.Text = answer;
            //de variable word toegevoegd aan da databank
            _context.Answers.Add(antwoord);
            await _context.SaveChangesAsync();

            
            return CreatedAtAction("GetAnswer", new { id = antwoord.AnswerID}, antwoord);
        }

        // DELETE: api/Answers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Answer>> DeleteAnswer(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();

            return answer;
        }

        private bool AnswerExists(int id)
        {
            return _context.Answers.Any(e => e.AnswerID == id);
        }
    }
}
