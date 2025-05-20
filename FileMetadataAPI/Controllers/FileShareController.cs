using System.Security.Claims;
using FileMetadataAPI.Application.Commands;
using FileMetadataAPI.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileMetadataAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/fileshare")]
    public class FileShareController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileShareController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("share")]
        public async Task<IActionResult> ShareFile([FromBody] ShareFileCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                if (result)
                    return Ok("Dosya başarıyla paylaşıldı.");
                return BadRequest("Paylaşım başarısız.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("InvalidOperationException yakalandı: " + ex.Message);
                return BadRequest(ex.Message); // Kullanıcıya özel hata mesajı döner
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Beklenmeyen bir hata oluştu.");
            }


        
        }

        [HttpGet("shared-with-me")]
        public async Task<IActionResult> GetSharedFiles()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized();

            var userId = int.Parse(userIdClaim.Value);
            var result = await _mediator.Send(new GetSharedFilesQuery { CurrentUserId = userId });
            return Ok(result);
        }
    }
}

