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
                createdBy = 1,                 // ✅ FIXED
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
            // 🔴 STEP 1: CHECK FOR CLASH
            bool exists = await _context.mstTimetable.AnyAsync(x =>
                x.isActive &&
                x.day == dto.day &&
                x.hourNo == dto.hourNo &&
                x.blockId == dto.blockId &&
                x.roomId == dto.roomId
            );

            if (exists)
                return false;   // ❌ Clash found (controller will handle response)

            // ✅ STEP 2: CREATE ENTITY
            var entity = new mstTimetable
            {
                batchId = dto.batchId!.Value,
                departmentId = dto.departmentId!.Value,
                sectionId = dto.sectionId!.Value,
                subjectId = dto.subjectId!.Value,
                facultyId = dto.facultyId!.Value,
                hourNo = dto.hourNo!.Value,

                day = dto.day,
                blockId = dto.blockId!.Value,
                roomId = dto.roomId!.Value,

                createdBy = createdBy == 0 ? 1 : createdBy, // ✅ SAFE DEFAULT
                createdDate = DateTime.Now,
                isActive = true
            };

            // ✅ STEP 3: SAVE
            await _context.mstTimetable.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<object>> getTimetableListAsync()
        {
            var result =
                from t in _context.mstTimetable

                join b in _context.mstBatch
                    on t.batchId equals b.batchId

                join d in _context.mstDepartment
                    on t.departmentId equals d.departmentId

                join s in _context.mstSection
                    on t.sectionId equals s.sectionId

                join sub in _context.mstSubject
                    on t.subjectId equals sub.subjectId

                join bl in _context.mstBlock
                    on t.blockId equals bl.blockId

                join r in _context.mstRoom
                    on t.roomId equals r.roomId

                join f in _context.Faculties
                    on t.facultyId equals f.FacultyID

                where t.isActive == true

                select new
                {
                    day = t.day,
                    batch = b.batchName,
                    department = d.departmentName,
                    section = s.sectionName,
                    subject = sub.subjectName,   
                    block = bl.blockName,        
                    room = r.roomNumber,     
                    hour = t.hourNo,
                    faculty = f.FacultyName
                };

            return await result.ToListAsync<object>();
        }


        public async Task<bool> addBlockAsync(TimetableDTO dto, int createdBy)
        {
            var entity = new mstBlock
            {
                blockName = dto.name,
                createdBy = createdBy,
                createdDate = DateTime.Now,
                isActive = true
            };
            await _context.mstBlock.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> addRoomAsync(TimetableDTO dto, int createdBy)
        {
            var entity = new mstRoom
            {
                roomNumber = dto.name,
                createdBy = createdBy,
                createdDate = DateTime.Now,
                isActive = true
            };
            await _context.mstRoom.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<List<TimetableDTO>> getBlocksAsync()
        {
            return await _context.mstBlock
                .Select(x => new TimetableDTO
                {
                    id = x.blockId,
                    name = x.blockName,
                    isActive = x.isActive
                })
                .ToListAsync();
        }
        public async Task<List<TimetableDTO>> getRoomsAsync()
        {
            return await _context.mstRoom
                .Select(x => new TimetableDTO
                {
                    id = x.roomId,
                    name = x.roomNumber,
                    isActive = x.isActive
                })
                .ToListAsync();
        }
        public async Task<bool> toggleBlockAsync(int id, bool isActive)
        {
            var entity = await _context.mstBlock.FindAsync(id);
            if (entity == null) return false;

            entity.isActive = isActive;
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> toggleRoomAsync(int id, bool isActive)
        {
            var entity = await _context.mstRoom.FindAsync(id);
            if (entity == null) return false;

            entity.isActive = isActive;
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<List<TimetableDTO>> getActiveBlocksAsync()
        {
            return await _context.mstBlock
                .Where(x => x.isActive)
                .Select(x => new TimetableDTO
                {
                    id = x.blockId,
                    name = x.blockName
                })
                .ToListAsync();
        }
        public async Task<List<TimetableDTO>> getActiveRoomsAsync()
        {
            return await _context.mstRoom
                .Where(x => x.isActive)
                .Select(x => new TimetableDTO
                {
                    id = x.roomId,
                    name = x.roomNumber
                })
                .ToListAsync();
        }
        public async Task<object> getMasterDataAsync()
        {
            var batches = await _context.mstBatch
                .Select(x => new TimetableDTO
                {
                    id = x.batchId,
                    name = x.batchName,
                    isActive = x.isActive
                }).ToListAsync();

            var departments = await _context.mstDepartment
                .Select(x => new TimetableDTO
                {
                    id = x.departmentId,
                    name = x.departmentName,
                    isActive = x.isActive
                }).ToListAsync();

            var sections = await _context.mstSection
                .Select(x => new TimetableDTO
                {
                    id = x.sectionId,
                    name = x.sectionName,
                    isActive = x.isActive
                }).ToListAsync();

            var subjects = await _context.mstSubject
                .Select(x => new TimetableDTO
                {
                    id = x.subjectId,
                    name = x.subjectName,
                    isActive = x.isActive
                }).ToListAsync();

            var blocks = await _context.mstBlock
                .Select(x => new TimetableDTO
                {
                    id = x.blockId,
                    name = x.blockName,
                    isActive = x.isActive
                }).ToListAsync();

            var rooms = await _context.mstRoom
                .Select(x => new TimetableDTO
                {
                    id = x.roomId,
                    name = x.roomNumber,
                    isActive = x.isActive
                }).ToListAsync();

            return new
            {
                batches,
                departments,
                sections,
                subjects,
                blocks,
                rooms
            };
        }
        public async Task<List<TimetableDTO>> getSubjectsByClassAsync(int batchId, int departmentId, int sectionId)
        {
            return await _context.mstSubject
                .Where(x =>
                    x.isActive &&
                    x.batchId == batchId &&
                    x.departmentId == departmentId &&
                    x.sectionId == sectionId)
                .Select(x => new TimetableDTO
                {
                    id = x.subjectId,
                    name = x.subjectName
                })
                .ToListAsync();
        }

    }
}
