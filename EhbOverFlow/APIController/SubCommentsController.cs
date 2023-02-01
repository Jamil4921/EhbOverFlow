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
    public class SubCommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SubComments
        [Authorize(Roles = "UserAdministrator, User")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubComment>>> GetsubComments()
        {
            return await _context.subComments.ToListAsync();
        }

        // GET: api/SubComments/5
        [Authorize(Roles = "UserAdministrator, User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<SubComment>> GetSubComment(int id)
        {
            var subComment = await _context.subComments.FindAsync(id);

            if (subComment == null)
            {
                return NotFound();
            }

            return subComment;
        }

        // PUT: api/SubComments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "UserAdministrator, User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubComment(int id, SubComment subComment)
        {
            if (id != subComment.Id)
            {
                return BadRequest();
            }

            _context.Entry(subComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCommentExists(id))
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

        // POST: api/SubComments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "UserAdministrator, User")]
        [HttpPost]
        public async Task<ActionResult<SubComment>> PostSubComment(SubComment subComment)
        {
            _context.subComments.Add(subComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubComment", new { id = subComment.Id }, subComment);
        }

        // DELETE: api/SubComments/5
        [Authorize(Roles = "UserAdministrator, User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubComment(int id)
        {
            var subComment = await _context.subComments.FindAsync(id);
            if (subComment == null)
            {
                return NotFound();
            }

            _context.subComments.Remove(subComment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubCommentExists(int id)
        {
            return _context.subComments.Any(e => e.Id == id);
        }
    }
}
