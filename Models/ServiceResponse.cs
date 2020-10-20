namespace dotnet_rzp.Models
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool isSuccess { get; set; }
        public string message { get; set; }
    }
}