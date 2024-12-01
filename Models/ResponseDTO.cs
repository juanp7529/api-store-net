namespace product_api.Models
{
    public class ResponseDTO
    {
        public required string Message { get; set; }
        public required bool IsSuccess { get; set; }
        public object? Result { get; set; }
    }
}
