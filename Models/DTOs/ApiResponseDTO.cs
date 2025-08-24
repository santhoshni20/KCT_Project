namespace KSI_Project.Models.DTOs
{
    public class ApiResponseDTO
    {
        public bool success { get; set; } = true;
        public string message { get; set; } = "Success";
        public object data { get; set; }
    }
}

