namespace ksi_project.Models.DTOs
{
    public class ApiResponseDTO
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public string errorDetails { get; set; }

        public static ApiResponseDTO Success(object data, string message = "Success", int statusCode = 200)
        {
            return new ApiResponseDTO { statusCode = statusCode, message = message, data = data };
        }

        public static ApiResponseDTO Failure(string message, string errorDetails = "", int statusCode = 500)
        {
            return new ApiResponseDTO { statusCode = statusCode, message = message, errorDetails = errorDetails };
        }
    }
}
