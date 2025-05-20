using System.Security.Claims;
using FileMetadataAPI.Application.Commands;
using FileMetadataAPI.Application.Dtos;
using FileMetadataAPI.Application.Queries;
using FileMetadataAPI.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FileMetadataAPI.Controllers
{
    [ApiController]
    //[Route("api/[controller]")]
    [Route("api/files")]
    [Authorize]
    public class FileController : ControllerBase
    {

        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;

        }

        [HttpGet("test-token")]
        public IActionResult TestToken()
        {
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("Kullanıcı token'ı çözülemedi.");

            return Ok("Token geçerli, kullanıcı ID: " + userIdClaim.Value);
        }


        [Authorize]
        [HttpGet("test-userid")]
        public IActionResult TestUserId()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            Console.WriteLine(">>> Auth Header: " + authHeader);
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("Kullanıcı kimliği bulunamadı.");

            var userIdValue = userIdClaim.Value;

            return Ok(new
            {
                UserIdClaimValue = userIdValue,
                UserIdClaimType = userIdValue.GetType().Name
            });
        }


        [HttpGet]
        public async Task<IActionResult> GetAllFiles()
        {
            var files = await _mediator.Send(new GetAllFilesQuery());
            return Ok(files);
        }


        [HttpPost]

        public async Task<IActionResult> Create([FromForm] CreateFileRequest request)
        {
            Console.WriteLine(">>> CONTROLLER çalıştı >>>");

            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("Kullanıcı kimliği bulunamadı.");

            var ownerId = int.Parse(userIdClaim.Value);

            var command = new CreateFileCommand
            {
                Name = request.Name,
                Description = request.Description,
                SharingType = request.SharingType,
                FileExtension = Path.GetExtension(request.File.FileName),
                OwnerId = ownerId
            };

            var result = await _mediator.Send(command);
            Console.WriteLine(">>> MEDIATOR çalıştı, sonuç: " + result);

            return Ok(result);

        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateFileCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID uyuşmuyor.");

            var updated = await _mediator.Send(command);
            return Ok(updated);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteFileCommand { Id = id });
            return NoContent();
        }

    }
}

