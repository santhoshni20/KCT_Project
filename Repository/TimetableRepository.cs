using ksi.Interfaces;
using ksi.Models.DTOs;
using ksi.Models.Entity;
using KSI_Project.Helpers.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ksi.Repository
{
    public class TimetableRepository : ITimetableRepository
    {
        private readonly ksiDbContext _context;

        public TimetableRepository(ksiDbContext context)
        {
            _context = context;
        }

        public async Task<bool> addBatchAsync(TimetableDTO dto, int createdBy)
        {
            var entity = new mstBatch
            {
                batchName = dto.name,
                createdBy = createdBy,
                createdDate = DateTime.Now
            };

            await _context.mstBatch.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> addDepartmentAsync(TimetableDTO dto, int createdBy)
        {
            var entity = new mstDepartment
            {
                departmentName = dto.name,
                createdBy = createdBy,
                createdDate = DateTime.Now
            };

            await _context.mstDepartment.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> addSectionAsync(TimetableDTO dto, int createdBy)
        {
            var entity = new mstSection
            {
                sectionName = dto.name,
                createdBy = createdBy,
                createdDate = DateTime.Now
            };

            await _context.mstSection.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<TimetableDTO>> getBatchesAsync()
        {
            return await _context.mstBatch
                .Where(x => x.isActive)
                .Select(x => new TimetableDTO
                {
                    id = x.batchId,
                    name = x.batchName
                })
                .ToListAsync();
        }

        public async Task<List<TimetableDTO>> getDepartmentsAsync()
        {
            return await _context.mstDepartment
                .Where(x => x.isActive)
                .Select(x => new TimetableDTO
                {
                    id = x.departmentId,
                    name = x.departmentName
                })
                .ToListAsync();
        }

        public async Task<List<TimetableDTO>> getSectionsAsync()
        {
            return await _context.mstSection
                .Where(x => x.isActive)
                .Select(x => new TimetableDTO
                {
                    id = x.sectionId,
                    name = x.sectionName
                })
                .ToListAsync();
        }

        public async Task<bool> addSubjectAsync(SubjectAddDTO dto)
        {
            var entity = new mstSubject
            {
                subjectName = dto.subjectName,
                batchId = dto.batchId,
                departmentId = dto.departmentId,
                sectionId = dto.sectionId,
                createdBy = dto.createdBy,
                createdDate = DateTime.Now
            };

            await _context.mstSubject.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
