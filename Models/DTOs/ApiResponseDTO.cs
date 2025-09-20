namespace KSI_Project.Models.DTOs
{
    public class APIResponseDTO
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public string ErrorDetails { get; set; }
    }
}

