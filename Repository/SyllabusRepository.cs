using KSI_Project.Models;
using KSI_Project.Models.Entity;
using MySql.Data.MySqlClient;

namespace KSI_Project.Repositories
{
    public class SyllabusRepository : ISyllabusRepository
    {
        private readonly string _connectionString;

        public SyllabusRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySQLConnectionString");
        }

        public async Task UploadAsync(SyllabusFile file)
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
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<SyllabusFile?> GetFileAsync(string batch, string dept)
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
                            return new SyllabusFile
                            {
                                Id = reader.GetInt32(0),                  
                                Batch = reader.GetString(1),             
                                DepartmentCode = reader.GetString(2),     
                                FileName = reader.GetString(3),            
                                FileData = (byte[])reader[4]         
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
