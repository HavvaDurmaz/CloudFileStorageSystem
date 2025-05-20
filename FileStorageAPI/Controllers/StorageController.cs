using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using FileStorageAPI.Models;

namespace FileStorageAPI.Controllers
{
    [Route("api/storage")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public StorageController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            Console.WriteLine("Test metodu çalıştı");
            return Ok("Test başarılı");
        }

        [HttpGet("list")]
        public IActionResult ListFiles()
        {
            var uploadPath = Path.Combine(_env.ContentRootPath, "Uploads");

            if (!Directory.Exists(uploadPath))
                return Ok(new List<string>()); // klasör bile yoksa boş liste döner

            var files = Directory.GetFiles(uploadPath)
                                 .Select(Path.GetFileName)
                                 .ToList();

            return Ok(files);
        }



        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] FileUploadRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("Dosya yok.");

            var uploadPath = Path.Combine(_env.ContentRootPath, "Uploads");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, request.File.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            return Ok(new { message = "Dosya başarıyla yüklendi." });
        }


        [HttpGet("download")]
        public IActionResult Download([FromQuery] string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return BadRequest("Dosya adı belirtilmedi.");

            var uploadPath = Path.Combine(_env.ContentRootPath, "Uploads");
            var filePath = Path.Combine(uploadPath, filename);

            if (!System.IO.File.Exists(filePath))
                return NotFound("Dosya bulunamadı.");

            var mimeType = "application/octet-stream"; // dilersen MIME tipi de hesaplanabilir
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, mimeType, filename);
        }

        [HttpDelete("delete")]
        public IActionResult DeleteFile([FromQuery] string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return BadRequest("Dosya adı belirtilmedi.");

            var uploadPath = Path.Combine(_env.ContentRootPath, "Uploads");
            var filePath = Path.Combine(uploadPath, filename);

            if (!System.IO.File.Exists(filePath))
                return NotFound("Dosya bulunamadı.");

            System.IO.File.Delete(filePath);
            return Ok($"Dosya silindi: {filename}");
        }


    }

}

