using System.Net;
using Fleet.Files.Models;
using Fleet.Files.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fleet.Files.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FilesController(IFileService fileService)
        {
            _fileService = fileService;
        }
        [HttpPost]
        public async Task<ActionResult<Repository.Entities.Files>> UploadFile([FromForm] UploadFileModel model)
        {
            try
            {
                var res = await _fileService.UploadFile(model);
                return res;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<Repository.Entities.Files>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<Repository.Entities.Files>>> Get()
        {
            try
            {
                var res = await _fileService.GetAllUploadedFiles();
                return res;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
