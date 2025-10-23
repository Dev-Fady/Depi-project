namespace DEPI_PROJECT.BLL.DTOs.Response
{
    public class ResponseDto<T>
    {
        public T? Data { get; set; }
        public string message { get; set; }
        public bool IsSuccess { get; set; }
    }
}