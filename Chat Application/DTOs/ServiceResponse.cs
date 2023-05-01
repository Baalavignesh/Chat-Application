namespace Chat_Application.DTOs
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
    }
}
