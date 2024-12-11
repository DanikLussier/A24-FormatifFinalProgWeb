
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using DixCordesServeur.Data;
using DixCordesServeur.Models;
using DixCordesServeur.Models.DisplayDTOs;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace DixCordesServeur.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReactionsController : ControllerBase
    {
        private readonly DixCordesServeurContext _context;

        public ReactionsController(DixCordesServeurContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReactionPicture(int id)
        {
            if(_context.Reactions == null) return Problem("Entity set 'Reactions' is null.");

            Reaction? reaction = await _context.Reactions.FindAsync(id);
            if(reaction == null) return NotFound(new { Message = "Cette réaction n'existe pas." });

            byte[] bytes = System.IO.File.ReadAllBytes(Directory.GetCurrentDirectory() + "/images/reactions/" + reaction.FileName);
            return File(bytes, reaction.MimeType);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> ToggleReaction(int id)
        {
            if (_context.Reactions == null) return Problem("Entity set 'Reactions' is null.");

            // À compléter


            return Ok(); // Remplacez ce return par celui en-dessous.
            //return Ok(new ReactionDisplayDTO(reaction, user));
        }

        [HttpPost("{messageId}")]
        [Authorize]
        public async Task<ActionResult<Reaction>> PostReaction(int messageId)
        {
            if (_context.Reactions == null || _context.Messages == null) return Problem("Entity set 'Reactions' is null.");

            User? user = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            Message? message = await _context.Messages.FindAsync(messageId);

            if(user == null || message == null)
            {
                return NotFound(new { Message = "Le message n'existe pas." });
            }

            try
            {
                IFormCollection formCollection = await Request.ReadFormAsync();
                IFormFile? file = formCollection.Files.GetFile("imageReaction");
                if(file != null)
                {
                    Image image = Image.Load(file.OpenReadStream());

                    Reaction reaction = new Reaction()
                    {
                        Id = 0,
                        MimeType = file.ContentType,
                        FileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName),
                        Message = message,
                        Users = new List<User>() { user }
                    };

                    image.Mutate(i => i.Resize(new ResizeOptions()
                    {
                        Mode = ResizeMode.Min,
                        Size = new Size() { Height = 30 }
                    }));
                    image.Save(Directory.GetCurrentDirectory() + "/images/reactions/" + reaction.FileName);

                    _context.Reactions.Add(reaction);
                    await _context.SaveChangesAsync();
                    return Ok(new ReactionDisplayDTO(reaction, user));
                }
                else
                {
                    return NotFound(new { Message = "Aucune image fournie." });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
