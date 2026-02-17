using ksi.Interfaces;
using ksi.Models.DTOs;
using ksi.Models.Entity;
using KSI_Project.Helpers.DbContexts;

namespace ksi.Repositories
{
    public class SyllabusRepository : iSyllabusRepository
    {
        private readonly ksiDbContext _context;

        public SyllabusRepository(ksiDbContext context)
        {
            _context = context;
        }

        public List<syllabusDTO> getAllSyllabus()
        {
            return (from s in _context.mstSyllabus
                    join b in _context.mstBatch on s.batchId equals b.batchId
                    join d in _context.mstDepartment on s.departmentId equals d.departmentId
                    where s.isActive
                    select new syllabusDTO
                    {
                        syllabusId = s.syllabusId,
                        batchId = b.batchId,
                        batchName = b.batchName,
                        departmentId = d.departmentId,
                        departmentName = d.departmentName,
                        syllabusDriveLink = s.syllabusDriveLink
                    }).ToList();
        }

        public List<syllabusDTO> getBatchList()
        {
            return _context.mstBatch
                .Where(x => x.isActive)
                .Select(x => new syllabusDTO
                {
                    batchId = x.batchId,
                    batchName = x.batchName
                }).ToList();
        }

        public List<syllabusDTO> getDepartmentList()
        {
            return _context.mstDepartment
                .Where(x => x.isActive)
                .Select(x => new syllabusDTO
                {
                    departmentId = x.departmentId,
                    departmentName = x.departmentName
                }).ToList();
        }
        public bool updateSyllabus(syllabusDTO dto, int updatedBy)
        {
            var entity = _context.mstSyllabus.Find(dto.syllabusId);
            if (entity == null) return false;

            entity.batchId = dto.batchId;
            entity.departmentId = dto.departmentId;
            entity.syllabusDriveLink = dto.syllabusDriveLink;
            entity.updatedBy = updatedBy;
            entity.updatedDate = DateTime.Now;

            return _context.SaveChanges() > 0;
        }

        public bool deleteSyllabus(int syllabusId, int deletedBy)
        {
            var entity = _context.mstSyllabus.Find(syllabusId);
            if (entity == null) return false;

            entity.isActive = false;
            entity.deletedBy = deletedBy;
            entity.deletedDate = DateTime.Now;

            return _context.SaveChanges() > 0;
        }

        public bool addSyllabus(syllabusDTO dto, int createdBy)
        {
            var entity = new mstSyllabus
            {
                batchId = dto.batchId,
                departmentId = dto.departmentId,
                syllabusDriveLink = dto.syllabusDriveLink,
                createdBy = createdBy,
                createdDate = DateTime.Now
            };

            _context.mstSyllabus.Add(entity);
            return _context.SaveChanges() > 0;
        }
    }
}