
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DixCordesServeur.Data;
using Microsoft.AspNetCore.Authorization;
using DixCordesServeur.Models;

namespace DixCordesServeur.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChannelsController : ControllerBase
    {
        private readonly DixCordesServeurContext _context;

        public ChannelsController(DixCordesServeurContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Channel>>> GetChannel()
        {
            if (_context.Channels == null)
            {
                return NotFound();
            }
            return await _context.Channels.ToListAsync();
        }

        [HttpPost("{name}")]
        [Authorize(Roles = "moderator")]
        public async Task<ActionResult> PostChannel(string name)
        {
            if (name == null) return NotFound();

            if (_context.Channels == null) return StatusCode(StatusCodes.Status500InternalServerError,
                new { Message = "Veuillez réessayer plus tard" });

            Channel channel = new Channel()
            {
                Name = name
            };

            _context.Channels.Add(channel);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
