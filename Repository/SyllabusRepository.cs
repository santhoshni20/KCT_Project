using KSI_Project.Helpers.DbContexts;
using KSI_Project.Interfaces;
using KSI_Project.Models;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KSI_Project.Repositories
{
    public class SyllabusRepository : ISyllabusRepository
    {
        private readonly string _connectionString;

        public SyllabusRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySQLConnectionString");
        }

        public async Task<ApiResponseDTO> UploadAsync(SyllabusFile file)
        {
            var response = new ApiResponseDTO();
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "INSERT INTO syllabus_files (Batch, DepartmentCode, FileName, FileData) VALUES (@Batch, @Dept, @FileName, @FileData)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Batch", file.Batch);
                        cmd.Parameters.AddWithValue("@Dept", file.DepartmentCode);
                        cmd.Parameters.AddWithValue("@FileName", file.FileName);
                        cmd.Parameters.AddWithValue("@FileData", file.FileData);
                        var rowsAffected = await cmd.ExecuteNonQueryAsync();
                        response.success = rowsAffected > 0;
                        response.message = rowsAffected > 0 ? "Syllabus uploaded successfully" : "No changes were made";
                    }
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error uploading syllabus: {ex.Message}";
            }
            return response;
        }

        public async Task<ApiResponseDTO> GetFileAsync(string batch, string dept)
        {
            var response = new ApiResponseDTO();
            try
            {
                using (var conn = new MySqlConnection(_connectionString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT Id, Batch, DepartmentCode, FileName, FileData FROM syllabus_files WHERE Batch=@Batch AND DepartmentCode=@Dept LIMIT 1";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Batch", batch);
                        cmd.Parameters.AddWithValue("@Dept", dept);

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var syllabusFile = new SyllabusFile
                                {
                                    Id = reader.GetInt32(0),
                                    Batch = reader.GetString(1),
                                    DepartmentCode = reader.GetString(2),
                                    FileName = reader.GetString(3),
                                    FileData = (byte[])reader[4]
                                };
                                response.success = true;
                                response.data = syllabusFile;
                                response.message = "Syllabus file fetched successfully";
                            }
                            else
                            {
                                response.success = false;
                                response.message = "Syllabus file not found";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = $"Error fetching syllabus file: {ex.Message}";
            }
            return response;
        }
    }
}
