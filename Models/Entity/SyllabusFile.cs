using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
//using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
//using Microsoft.AspNetCore.Http.HttpResults;

namespace KSI_Project.Models
{
    public class SyllabusFile
    {
        public int Id { get; set; }
        public string Batch { get; set; }
        public string DepartmentCode { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }
    }
}
