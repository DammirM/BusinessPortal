using System.ComponentModel.DataAnnotations;

namespace BusinessPortal.Models
{
    public class RequestType
    {
        [Key]
        public int Id { get; set; }
        public string Vabb { get; set; }
        public string Sick { get; set; }
        public string Vacation { get; set; }

    }
}
