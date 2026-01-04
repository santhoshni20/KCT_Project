namespace ksi.Models.DTOs
{
    public class ApiResponseDTO
    {
        public int statusCode { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public string errorDetails { get; set; }
    }
}
