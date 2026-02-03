using ksi.Interfaces;
using ksi.Models.DTOs;
using ksi.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ksi.Controllers
{
    public class TimetableController : Controller
    {
        #region Constructor and DI
        private readonly ITimetableRepository _repository;

        public TimetableController(ITimetableRepository repository)
        {
            _repository = repository;
        }
        #endregion

        // ===================== PAGE LOAD =====================

        #region Load Add Details Page
        public IActionResult AddDetails()
        {
            return View();
        }
        #endregion

        #region Load Add Timetable Page
        public IActionResult AddTimetable()
        {
            return View();
        }
        #endregion

        // ===================== DROPDOWNS =====================

        // ===================== ADD MASTER =====================

        #region Add Batch
        [HttpPost]
        public async Task<ApiResponseDTO> AddBatch([FromBody] TimetableDTO dto)
        {
            var result = await _repository.addBatchAsync(dto, 1);
            return new ApiResponseDTO
            {
                success = result,
                message = result ? "Batch added successfully" : "Failed"
            };
        }
        #endregion

        #region Add Department
        [HttpPost]
        public async Task<ApiResponseDTO> AddDepartment([FromBody] TimetableDTO dto)
        {
            var result = await _repository.addDepartmentAsync(dto, 1);
            return new ApiResponseDTO
            {
                success = result,
                message = result ? "Department added successfully" : "Failed"
            };
        }
        #endregion

        #region Add Section
        [HttpPost]
        public async Task<ApiResponseDTO> AddSection([FromBody] TimetableDTO dto)
        {
            var result = await _repository.addSectionAsync(dto, 1);
            return new ApiResponseDTO
            {
                success = result,
                message = result ? "Section added successfully" : "Failed"
            };
        }
        #endregion

        #region Add Subject
        [HttpPost]
        public async Task<ApiResponseDTO> AddSubject([FromBody] SubjectAddDTO dto)
        {
            var result = await _repository.addSubjectAsync(dto);
            return new ApiResponseDTO
            {
                success = result,
                message = result ? "Subject added successfully" : "Failed"
            };
        }
        #endregion

        // ===================== TOGGLE =====================

        #region Toggle Status
        [HttpPost]
        public async Task<ApiResponseDTO> ToggleStatus([FromBody] TimetableDTO dto)
        {
            bool result = false;

            if (dto.batchId.HasValue)
                result = await _repository.toggleBatchAsync(dto.batchId.Value, dto.isActive);
            else if (dto.departmentId.HasValue)
                result = await _repository.toggleDepartmentAsync(dto.departmentId.Value, dto.isActive);
            else if (dto.sectionId.HasValue)
                result = await _repository.toggleSectionAsync(dto.sectionId.Value, dto.isActive);
            else if (dto.subjectId.HasValue)
                result = await _repository.toggleSubjectAsync(dto.subjectId.Value, dto.isActive);
            else if (dto.blockId.HasValue)
                result = await _repository.toggleBlockAsync(dto.blockId.Value, dto.isActive);
            else if (dto.roomId.HasValue)
                result = await _repository.toggleRoomAsync(dto.roomId.Value, dto.isActive);

            return new ApiResponseDTO
            {
                success = result,
                message = result ? "Status updated" : "Update failed"
            };
        }
        #endregion

        // ===================== TIMETABLE SAVE =====================

        #region Save Timetable
        [HttpPost]
        public async Task<ApiResponseDTO> SaveTimetable([FromBody] TimetableDTO dto)
        {
            if (!dto.batchId.HasValue ||
            !dto.departmentId.HasValue ||
            !dto.sectionId.HasValue ||
            !dto.subjectId.HasValue ||
            !dto.facultyId.HasValue ||
            !dto.hourNo.HasValue ||
            string.IsNullOrEmpty(dto.day) ||
            !dto.blockId.HasValue ||
            !dto.roomId.HasValue)
            {
                return new ApiResponseDTO
                {
                    success = false,
                    message = "All fields are required"
                };
            }

            if (dto.hourNo < 1 || dto.hourNo > 7)
            {
                return new ApiResponseDTO
                {
                    success = false,
                    message = "Hour must be between 1 and 7"
                };
            }

            var result = await _repository.addTimetableAsync(dto, 1);

            return new ApiResponseDTO
            {
                success = result,
                message = result ? "Timetable saved successfully" : "Save failed"
            };
        }

        [HttpGet]
        public async Task<IActionResult> GetTimetableList()
        {
            try
            {
                var list = await _repository.getTimetableListAsync();
                return Json(new { success = true, data = list });
            }
            catch (Exception ex)
            {
                // consider logging ex
                return Json(new { success = false, message = ex.Message });
            }
        }


        #endregion

        [HttpPost]
        public async Task<ApiResponseDTO> AddBlock([FromBody] TimetableDTO dto)
        {
            var result = await _repository.addBlockAsync(dto, 1);
            return new ApiResponseDTO { success = result };
        }

        [HttpPost]
        public async Task<ApiResponseDTO> AddRoom([FromBody] TimetableDTO dto)
        {
            var result = await _repository.addRoomAsync(dto, 1);
            return new ApiResponseDTO { success = result };
        }
        [HttpGet]
        public async Task<IActionResult> GetMasterData()
        {
            try
            {
                var data = await _repository.getMasterDataAsync();

                return Json(new
                {
                    success = true,
                    data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // IMPORTANT
            }
        }
        [HttpGet]
        public async Task<ApiResponseDTO> GetSubjects(int batchId, int departmentId, int sectionId)
        {
            var data = await _repository.getSubjectsByClassAsync(batchId, departmentId, sectionId);

            return new ApiResponseDTO
            {
                success = true,
                data = data
            };
        }
        #region Add subject
        public IActionResult AddSubjects()
        {
            return View();
        }

        [HttpGet]
        public IActionResult getDropdowns()
        {
            try
            {
                var data = _repository.getDropdownData();
                return Ok(new ApiResponseDTO(200, "Success", data));
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    new ApiResponseDTO(500, "Error", null, ex.Message));
            }
        }

        // Update saveSubject action so it supports both create and update and returns meaningful message.
        [HttpPost]
        public IActionResult saveSubject([FromBody] subjectDTO subjectDto)
                {
                    try
                    {
                        if (subjectDto == null)
                            return BadRequest(new ApiResponseDTO(400, "Invalid request", null));

                        int userId = 1; // TODO: replace with actual user id retrieval

                        var isUpdate = subjectDto.subjectId > 0;
                        var result = _repository.saveSubject(subjectDto, userId);

                        return Ok(new ApiResponseDTO
                        {
                            statusCode = 200,
                            success = result,
                            message = result ? (isUpdate ? "Subject updated successfully" : "Subject saved successfully") : "Failed"
                        });
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500,
                            new ApiResponseDTO
                            {
                                statusCode = 500,
                                success = false,
                                message = "Error",
                                errorDetails = ex.Message
                            });
                    }
                }

                [HttpGet]
                public IActionResult getSubjectList()
                {
                    try
                    {
                        var data = _repository.getSubjects();
                        return Ok(new ApiResponseDTO(200, "Success", data));
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500,
                            new ApiResponseDTO(500, "Error", null, ex.Message));
                    }
                }
                #endregion

            }
        }
