namespace ksi.Models.DTOs
{
    public class ApiResponseDTO
    {
        public int statusCode { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public string errorDetails { get; set; }

        public ApiResponseDTO() { }

        public ApiResponseDTO(int statusCode, string message, object data)
        {
            this.statusCode = statusCode;
            this.message = message;
            this.data = data;
            this.success = statusCode == 200;
        }

        public ApiResponseDTO(int statusCode, string message, object data, string errorDetails)
        {
            this.statusCode = statusCode;
            this.message = message;
            this.data = data;
            this.errorDetails = errorDetails;
            this.success = false;
        }
    }
}