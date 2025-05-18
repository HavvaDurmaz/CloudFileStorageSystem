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
    [Route("api/[controller]")]
    [Authorize]
    public class FileController : ControllerBase
    {

        private readonly IMediator _mediator;

        // ✅ Constructor ile IMediator enjekte ediliyor
        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Bu endpoint JWT gerektiriyor.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFiles()
        {
            var query = new GetAllFilesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromForm] CreateFileRequest request)
        //{
        //    // Gelen enum zaten tip olarak doğru olduğu için ekstra parse gerekmiyor

        //    var fileName = request.File.FileName;
        //    var fileExtension = Path.GetExtension(fileName);

        //    var command = new CreateFileCommand
        //    {
        //        Name = request.Name,
        //        Description = request.Description,
        //        SharingType = request.SharingType,  // enum direkt atanıyor
        //        FileExtension = fileExtension
        //    };

        //    var result = await _mediator.Send(command);

        //    // Dosyayı kaydetme işlemi, mesela FileStorageAPI’ye göndermek burada olabilir

        //    return Ok(result);
        //}


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateFileRequest request)
        {
            var command = new CreateFileCommand
            {
                Name = request.Name,
                Description = request.Description,
                SharingType = request.SharingType,
                FileExtension = Path.GetExtension(request.File.FileName)
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }



        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFileCommand command)
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

