using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class FileController(IFileService service) : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var files = service.GetAllFiles();
            return Ok(files);
        }

        public IActionResult Index()
        {
            var files = service.GetAllFiles();
            return View(files);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var file = service.GetFileById(id);

            if (file == null)
            {
                return NotFound();
            }

            return Ok(file);
        }

        [HttpPost]
        public IActionResult Create([FromBody] FileModel file)
        {
            service.CreateFile(file);
            return CreatedAtAction(nameof(GetById), new { id = file.Id }, file);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] FileModel updatedFile)
        {
            service.UpdateFile(id, updatedFile);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            service.DeleteFile(id);
            return NoContent();
        }

        [HttpGet("search")]
        public IActionResult SearchFiles([FromQuery] string term)
        {
            var files = service.SearchFiles(term);
            return Ok(files);
        }
    }
}
