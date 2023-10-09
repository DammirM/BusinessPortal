using System.Net;

namespace BusinessPortal.Models
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            ErrorMessages = new List<string>();
        }
        public Object Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
