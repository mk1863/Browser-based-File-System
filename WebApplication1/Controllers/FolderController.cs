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

        [HttpGet("/api/Folder/Open")]
        public IActionResult OpenFolder(string folderName)
        {
            var folderContents = folderService.GetFolderContents(folderName);
            return Json(folderContents);
        }

        [HttpGet("/api/Folder/GoBack")]
        public IActionResult GoBack()
        {
            try
            {
                var folderContents = folderService.GoBack();
                return Json(folderContents);
            }
            catch (Exception ex)
            {
                // Handle exceptions if necessary
                Console.WriteLine($"Error going back to the previous folder: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            };
        }
    }

}
