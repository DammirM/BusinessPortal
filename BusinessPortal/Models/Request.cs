using System.ComponentModel.DataAnnotations;

namespace BusinessPortal.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public int RequestTypeId { get; set; }
        public DateTime Period { get; set; }
        public DateTime RequestDate { get; set; }
        public int PersonalId { get; set; }
        public bool Status { get; set; }
    }
}
