using KSI_Project.Models;
using KSI_Project.Models.Entity;

namespace KSI_Project.Repositories
{
    public interface ISyllabusRepository
    {
        Task UploadAsync(SyllabusFile file);
        Task<SyllabusFile?> GetFileAsync(string batch, string dept);
    }
}
