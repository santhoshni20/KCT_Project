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

        // ================= ADD =================
        public async Task<bool> addBatchAsync(TimetableDTO dto, int createdBy)
        {
            var entity = new mstBatch
            {
                batchName = dto.name,
                createdBy = createdBy,
                createdDate = DateTime.Now,
                isActive = true
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
                createdDate = DateTime.Now,
                isActive = true
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
                createdDate = DateTime.Now,
                isActive = true
            };
            await _context.mstSection.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        // ================= GET =================
        public async Task<List<TimetableDTO>> getBatchesAsync()
        {
            return await _context.mstBatch
                .Select(x => new TimetableDTO
                {
                    id = x.batchId,
                    name = x.batchName,
                    isActive = x.isActive
                })
                .ToListAsync();
        }

        public async Task<List<TimetableDTO>> getDepartmentsAsync()
        {
            return await _context.mstDepartment
                .Select(x => new TimetableDTO
                {
                    id = x.departmentId,
                    name = x.departmentName,
                    isActive = x.isActive
                })
                .ToListAsync();
        }

        public async Task<List<TimetableDTO>> getSectionsAsync()
        {
            return await _context.mstSection
                .Select(x => new TimetableDTO
                {
                    id = x.sectionId,
                    name = x.sectionName,
                    isActive = x.isActive
                })
                .ToListAsync();
        }

        // ================= TOGGLE =================
        public async Task<bool> toggleBatchAsync(int id, bool isActive)
        {
            var entity = await _context.mstBatch.FindAsync(id);
            if (entity == null) return false;

            entity.isActive = isActive;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> toggleDepartmentAsync(int id, bool isActive)
        {
            var entity = await _context.mstDepartment.FindAsync(id);
            if (entity == null) return false;

            entity.isActive = isActive;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> toggleSectionAsync(int id, bool isActive)
        {
            var entity = await _context.mstSection.FindAsync(id);
            if (entity == null) return false;

            entity.isActive = isActive;
            return await _context.SaveChangesAsync() > 0;
        }

        // ================= SUBJECT =================
        public async Task<bool> addSubjectAsync(SubjectAddDTO dto)
        {
            var entity = new mstSubject
            {
                subjectName = dto.subjectName,
                batchId = dto.batchId,
                departmentId = dto.departmentId,
                sectionId = dto.sectionId,
                createdBy = dto.createdBy,
                createdDate = DateTime.Now,
                isActive = true
            };
            await _context.mstSubject.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        // ================= GET ONLY ACTIVE =================
        public async Task<List<TimetableDTO>> getActiveBatchesAsync()
        {
            return await _context.mstBatch
                .Where(x => x.isActive)      // filter only active
                .Select(x => new TimetableDTO
                {
                    id = x.batchId,
                    name = x.batchName
                })
                .ToListAsync();
        }

        public async Task<List<TimetableDTO>> getActiveDepartmentsAsync()
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

        public async Task<List<TimetableDTO>> getActiveSectionsAsync()
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
        public async Task<List<TimetableDTO>> getSubjectsAsync()
        {
            return await _context.mstSubject
                .Select(x => new TimetableDTO
                {
                    id = x.subjectId,
                    name = x.subjectName,
                    isActive = x.isActive
                })
                .ToListAsync();
        }

        public async Task<bool> toggleSubjectAsync(int id, bool isActive)
        {
            var entity = await _context.mstSubject.FindAsync(id);
            if (entity == null) return false;

            entity.isActive = isActive;
            return await _context.SaveChangesAsync() > 0;
        }
        // ================= ACTIVE SUBJECTS =================
        public async Task<List<TimetableDTO>> getActiveSubjectsAsync()
        {
            return await _context.mstSubject
                .Where(x => x.isActive)
                .Select(x => new TimetableDTO
                {
                    id = x.subjectId,
                    name = x.subjectName
                })
                .ToListAsync();
        }

        // ================= ACTIVE FACULTIES =================
        public async Task<List<TimetableDTO>> getActiveFacultiesAsync()
        {
            return await _context.Faculties
                .Where(x => x.IsActive)
                .Select(x => new TimetableDTO
                {
                    id = x.FacultyID,
                    name = x.FacultyName   // ✅ THIS LINE IS CRITICAL
                })
                .ToListAsync();
        }

        public async Task<bool> addTimetableAsync(TimetableDTO dto, int createdBy)
        {
            var entity = new mstTimetable
            {
                batchId = dto.batchId.Value,
                departmentId = dto.departmentId.Value,
                sectionId = dto.sectionId.Value,
                subjectId = dto.subjectId.Value,
                facultyId = dto.facultyId.Value,
                hourNo = dto.hourNo.Value,

                createdBy = createdBy,
                createdDate = DateTime.Now,
                isActive = true
            };

            await _context.mstTimetable.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<List<object>> getTimetableListAsync()
        {
            var data =
                from t in _context.mstTimetable
                join b in _context.mstBatch on t.batchId equals b.batchId
                join d in _context.mstDepartment on t.departmentId equals d.departmentId
                join s in _context.mstSection on t.sectionId equals s.sectionId
                join sub in _context.mstSubject on t.subjectId equals sub.subjectId
                join f in _context.Faculties on t.facultyId equals f.FacultyID
                where t.isActive
                select new
                {
                    batch = b.batchName,
                    department = d.departmentName,
                    section = s.sectionName,
                    subject = sub.subjectName,
                    hour = t.hourNo,
                    faculty = f.FacultyName
                };

            return await data.ToListAsync<object>();
        }

    }
}
