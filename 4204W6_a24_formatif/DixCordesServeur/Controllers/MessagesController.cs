
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DixCordesServeur.Data;
using DixCordesServeur.Models;
using DixCordesServeur.Models.DisplayDTOs;
using DixCordesServeur.Models.DTOs;

namespace DixCordesServeur.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly DixCordesServeurContext _context;

        public MessagesController(DixCordesServeurContext context)
        {
            _context = context;
        }

        [HttpGet("{channelId}")]
        public async Task<IActionResult> GetChannelMessages(int channelId)
        {
            if(_context.Messages == null || _context.Channels == null)
            {
                return Problem("Entity set 'Messages' is null.");
            }

            Channel? channel = await _context.Channels.FindAsync(channelId);
            if(channel == null || channel.Messages == null)
            {
                return Ok(new List<Message>());
            }
            else
            {
                User? user = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

                // Les messages du channel sont transformés en MessageDisplayDTOs, pour ajouter le nom de l'utilisateur pour chaque message.
                return Ok(channel.Messages.Select(x => new MessageDisplayDTO(x, user)));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostMessage(PostMessageDTO messageDTO)
        {
            if (_context.Messages == null || _context.Channels == null) return Problem("Entity set 'Messages' is null.");

            User? user = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Channel? channel = await _context.Channels.FindAsync(messageDTO.ChannelId);

            if(channel == null || user == null)
            {
                return NotFound(new { Message = "Ce channel n'existe pas." });
            }

            Message message = new Message() { Id = 0, Text = messageDTO.Text, Channel = channel, User = user, SentAt = DateTime.Now };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            MessageDisplayDTO returnMessage = new MessageDisplayDTO(message, user);

            return Ok(returnMessage);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            // À compléter

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User? user = await _context.Users.FindAsync(userId);

            if (user == null) return NotFound();

            Message message = _context.Messages.Where(m => m.Id == id).First();

            if (message == null) return NotFound();
            if (message.User != user) return NotFound();

            // À décommenter
            _context.Messages.Remove(message);

            if (message.Reactions != null)
            {
                for (int i = message.Reactions.Count - 1; i >= 0; i--)
                {
                    Reaction reaction = message.Reactions[i];
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + "/images/reactions/" + reaction.FileName);
                    _context.Reactions.Remove(reaction);
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
