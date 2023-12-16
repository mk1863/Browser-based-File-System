// Controllers/FolderController.cs

using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class FolderController(IFolderService folderService) : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var folders = folderService.GetAllFolders();
            return Ok(folders);
        }

        public IActionResult Index()
        {
            var files = folderService.GetAllFolders();
            return View(files);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var folder = folderService.GetFolderById(id);

            if (folder == null)
            {
                return NotFound();
            }

            return Ok(folder);
        }

        [HttpPost]
        public IActionResult Create([FromBody] FolderModel folder)
        {
            folderService.CreateFolder(folder);
            return CreatedAtAction(nameof(GetById), new { id = folder.Id }, folder);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] FolderModel updatedFolder)
        {
            folderService.UpdateFolder(id, updatedFolder);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            folderService.DeleteFolder(id);
            return NoContent();
        }

        [HttpGet("{folderName}")]
        public IActionResult GetFolderContents(string folderName)
        {
            try
            {
                var folderContents = folderService.GetFolderContents(folderName);
                return Ok(folderContents);
            }
            catch (Exception ex)
            {
                // Log the error
                return BadRequest(new { message = $"Failed to get folder contents: {ex.Message}" });
            }
        }
    }

}
