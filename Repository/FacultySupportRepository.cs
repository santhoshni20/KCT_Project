using KSI_Project.Models;
using KSI_Project.Models.DTOs;
using KSI_Project.Models.Entity;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using KSI_Project.Interfaces;

namespace KSI_Project.Repositories
{
    public class FacultySupportRepository : IFacultySupportRepository
    {
        public async Task<IEnumerable<string>> GetFacultyDetailsAsync()
        {
            return new List<string> { "Faculty1", "Faculty2" };
        }
    }
}
