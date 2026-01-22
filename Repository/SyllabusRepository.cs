using ksi.Interfaces;
using ksi.Models.DTOs;
using ksi.Models.Entity;
using KSI_Project.Helpers.DbContexts;

namespace ksi.Repositories
{
    public class syllabusRepository : iSyllabusRepository
    {
        private readonly ksiDbContext context;

        public syllabusRepository(ksiDbContext context)
        {
            this.context = context;
        }

        public List<syllabusDTO> getActiveBatches()
        {
            return context.mstBatch
                .Where(b => b.isActive)
                .Select(b => new syllabusDTO
                {
                    id = b.batchId,
                    name = b.batchName
                })
                .ToList();
        }

        public List<syllabusDTO> getActiveDepartments()
        {
            return context.mstDepartment
                .Where(d => d.isActive)
                .Select(d => new syllabusDTO
                {
                    id = d.departmentId,
                    name = d.departmentName
                })
                .ToList();
        }
    }
}
