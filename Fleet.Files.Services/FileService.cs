using Fleet.Files.Models;
using Fleet.Files.Repository.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Fleet.Files.Services
{
    public interface IFileService
    {
        Task<Repository.Entities.Files?> UploadFile(UploadFileModel model);
        Task<List<Repository.Entities.Files>> GetAllUploadedFiles();
    }

    public class FileService : IFileService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IFilesRepository _filesRepository;

        public FileService(IHostingEnvironment hostingEnvironment, IFilesRepository filesRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _filesRepository = filesRepository;
        }
        public async Task<Repository.Entities.Files?> UploadFile(UploadFileModel model)
        {
            var directory = Path.Combine(_hostingEnvironment.ContentRootPath, "app_data");
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            var file = Path.Combine(directory, model.File.FileName);
            await using var fs = new FileStream(file, FileMode.Create);
            await model.File.CopyToAsync(fs);
            var entity = await _filesRepository.Insert(new Repository.Entities.Files()
            {
                Name = model.File.Name,
                CreatedAt = DateTime.Now,
            });
            return entity;
        }

        public async Task<List<Repository.Entities.Files>> GetAllUploadedFiles()
        {
            return await _filesRepository.GetAll().ToListAsync();
        }
    }
}
