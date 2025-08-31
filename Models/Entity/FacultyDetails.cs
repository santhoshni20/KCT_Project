using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSI_Project.Models.Entity
{
    public class FacultyDetails
    {
        public int Id { get; set; }
        public string TeacherId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Expertise { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public bool BookAppointment { get; set; }
    }
}
