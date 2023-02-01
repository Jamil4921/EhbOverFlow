using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EhbOverFlow.Areas.Identity.Data;
using EhbOverFlow.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace EhbOverFlow.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainCommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MainCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "UserAdministrator, User")]

        // GET: api/MainComments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MainComment>>> GetmainComments()
        {
            return await _context.mainComments.ToListAsync();
        }

        // GET: api/MainComments/5
        [Authorize(Roles = "UserAdministrator, User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<MainComment>> GetMainComment(int id)
        {
            var mainComment = await _context.mainComments.FindAsync(id);

            if (mainComment == null)
            {
                return NotFound();
            }

            return mainComment;
        }

        // PUT: api/MainComments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "UserAdministrator, User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMainComment(int id, MainComment mainComment)
        {
            if (id != mainComment.Id)
            {
                return BadRequest();
            }

            _context.Entry(mainComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MainCommentExists(id))
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

        // POST: api/MainComments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "UserAdministrator, User")]
        [HttpPost]
        public async Task<ActionResult<MainComment>> PostMainComment(MainComment mainComment)
        {
            _context.mainComments.Add(mainComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMainComment", new { id = mainComment.Id }, mainComment);
        }

        // DELETE: api/MainComments/5
        [Authorize(Roles = "UserAdministrator, User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMainComment(int id)
        {
            var mainComment = await _context.mainComments.FindAsync(id);
            if (mainComment == null)
            {
                return NotFound();
            }

            _context.mainComments.Remove(mainComment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MainCommentExists(int id)
        {
            return _context.mainComments.Any(e => e.Id == id);
        }
    }
}
